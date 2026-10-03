using FluentAssertions;

namespace Depra.Sound.Playback.UnitTests;

public sealed class AudioPlayerTests
{
	[Fact]
	public void Play_AppliesDescriptionAndDynamicArgumentsBeforeStartingClip()
	{
		var clip = new StubClip();
		var source = new StubAudioSource();
		var description = new StubEventDescription(clip);
		var eventId = new AudioEventId(42);
		var table = new AudioLibrary([new StubBank(eventId, description)]);
		var playback = new AudioPlayer(table, source);
		var parameters = new[] { AudioParam.Float(AudioParamId.Pitch, 1.25f) };

		playback.Play(eventId, parameters).Result.Should().BeTrue();

		source.Operations.Should().Equal("parameter", "parameter", "play");
		source.PlayedClip.Should().BeSameAs(clip);
		source.Parameters[AudioParamId.Volume.Value].FloatValue.Should().Be(0.75f);
		source.Parameters[AudioParamId.Pitch.Value].FloatValue.Should().Be(1.25f);
	}

	[Fact]
	public void Play_BatchDescription_PlaysEachNestedDescription()
	{
		var firstClip = new StubClip();
		var secondClip = new StubClip();
		var source = new StubAudioSource();
		var eventId = new AudioEventId(42);
		var batch = new StubBatch(
			new StubEventDescription(firstClip),
			new StubEventDescription(secondClip));
		var playback = new AudioPlayer(
			new AudioLibrary([new StubBank(eventId, batch)]), source);

		playback.Play(eventId).Result.Should().BeTrue();

		source.StartedCount.Should().Be(2);
		source.PlayedClip.Should().BeSameAs(secondClip);
	}

	[Fact]
	public void Play_UnknownEventId_ReturnsFalseWithoutStartingSource()
	{
		var source = new StubAudioSource();
		var table = new AudioLibrary([new StubBank(new AudioEventId(42), new StubEventDescription(new StubClip()))]);
		var playback = new AudioPlayer(table, source);

		playback.Play(new AudioEventId(7)).Result.Should().BeFalse();
		source.PlayedClip.Should().BeNull();
	}

	[Fact]
	public void Play_ForwardsReferenceParameter()
	{
		var source = new StubAudioSource();
		var eventId = new AudioEventId(42);
		var playback = new AudioPlayer(
			new AudioLibrary([new StubBank(eventId, new StubEventDescription(new StubClip()))]), source);
		var target = new object();
		var parameters = new[] { AudioParam.Ref(new AudioParamId(15), target) };

		playback.Play(eventId, parameters).Result.Should().BeTrue();

		source.Parameters[15].ReferenceValue.Should().BeSameAs(target);
	}

	[Fact]
	public void Play_ParamsOverload_AcceptsInlineParameterArguments()
	{
		var source = new StubAudioSource();
		var eventId = new AudioEventId(42);
		var playback = new AudioPlayer(
			new AudioLibrary([new StubBank(eventId, new StubEventDescription(new StubClip()))]), source);

		playback.Play(eventId,
			AudioParam.Float(AudioParamId.Pitch, 1.25f),
			AudioParam.Bool(AudioParamId.Loop, true)).Result.Should().BeTrue();

		source.Parameters[AudioParamId.Pitch.Value].FloatValue.Should().Be(1.25f);
		source.Parameters[AudioParamId.Loop.Value].IntegerValue.Should().Be(1);
	}

	[Fact]
	public void SourcePlay_StartsClipInOneCall()
	{
		var clip = new StubClip();
		var source = new StubAudioSource();

		source.Play(clip);
		source.PlayedClip.Should().BeSameAs(clip);
		source.StartedCount.Should().Be(1);
	}

	private sealed class StubClip : IAudioClip
	{
		public string Name => "stub";
		public float Duration => 1f;
	}

	private sealed class StubEventDescription(IAudioClip clip) : IAudioEventDescription
	{
		public IAudioClip Clip { get; } = clip;
		public IAudioEventContract Contract { get; } = new StubContract();
	}

	private sealed class StubBatch(params IAudioEventDescription[] events) : IAudioEventDescription, IAudioEventBatchDescription
	{
		public IAudioClip Clip => null;
		public IAudioEventContract Contract { get; } = new StubContract();
		public int EventCount => events.Length;
		public IAudioEventDescription GetEvent(int index) => events[index];
	}

	private sealed class StubContract : IAudioEventContract
	{
		ReadOnlySpan<AudioParam> IAudioEventContract.Merge(ReadOnlySpan<AudioParam> parameters) =>
			ReadOnlySpan<AudioParam>.Empty;
	}

	private sealed class StubBank(AudioEventId eventId, IAudioEventDescription description) : IAudioBank
	{
		public bool TryGet(AudioEventId requestedId, out IAudioEventDescription result)
		{
			result = description;
			return requestedId == eventId;
		}
	}

	private sealed class StubAudioSource : IAudioSource
	{
		public event Action Started;
		public event Action<AudioStopReason> Stopped;

		public bool IsPlaying => PlayedClip != null;
		public IAudioClip PlayedClip { get; private set; }
		public IAudioClip Current => PlayedClip!;
		public Dictionary<int, AudioParam> Parameters { get; } = new();
		public List<string> Operations { get; } = new();
		public int StartedCount { get; private set; }

		public void Play(IAudioClip clip)
		{
			PlayedClip = clip;
			StartedCount++;
			Started?.Invoke();
		}

		public void Play(IAudioClip clip, ReadOnlySpan<AudioParam> parameters)
		{
			PlayedClip = clip;
			foreach (var param in parameters)
			{
				SetParameter(in param);
			}

			Operations.Add("play");
			StartedCount++;
			Started?.Invoke();
		}

		private void SetParameter(in AudioParam param)
		{
			Operations.Add("parameter");
			Parameters[param.Id.Value] = param;
		}

		public void Stop()
		{
			PlayedClip = null;
			Stopped?.Invoke(AudioStopReason.STOPPED);
		}
	}

	private sealed class AudioLibrary(IList<IAudioBank> banks) : IAudioLibrary
	{
		bool IAudioLibrary.TryResolve(AudioEventId eventId, out IAudioEventDescription description)
		{
			for (int index = 0, count = banks.Count; index < count; index++)
			{
				if (banks[index].TryGet(eventId, out description))
				{
					return true;
				}
			}

			description = null;
			return false;
		}
	}
}