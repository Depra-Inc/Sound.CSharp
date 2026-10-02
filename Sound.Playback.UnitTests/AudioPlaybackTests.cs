using FluentAssertions;

namespace Depra.Sound.Playback.UnitTests;

public sealed class AudioPlaybackTests
{
	[Fact]
	public void Play_AppliesDescriptionAndDynamicArgumentsBeforeStartingClip()
	{
		var clip = new StubClip();
		var source = new StubAudioSource();
		var description = new StubDescription(clip);
		var eventId = new AudioEventId(42);
		var table = new AudioLibrary([new StubBank(eventId, description)]);
		var playback = new AudioPlayback(table, source);
		var parameters = new[] { AudioParam.Float(AudioParamId.Pitch, 1.25f) };

		playback.Play(eventId, parameters).Should().BeTrue();

		source.Operations.Should().Equal("parameter", "parameter", "play");
		source.PlayedClip.Should().BeSameAs(clip);
		source.Parameters[AudioParamId.Volume.Value].FloatValue.Should().Be(0.75f);
		source.Parameters[AudioParamId.Pitch.Value].FloatValue.Should().Be(1.25f);
	}

	[Fact]
	public void Play_UnknownEventId_ReturnsFalseWithoutStartingSource()
	{
		var source = new StubAudioSource();
		var table = new AudioLibrary([new StubBank(new AudioEventId(42), new StubDescription(new StubClip()))]);
		var playback = new AudioPlayback(table, source);

		playback.Play(new AudioEventId(7)).Should().BeFalse();
		source.PlayedClip.Should().BeNull();
	}

	[Fact]
	public void Play_ForwardsReferenceParameter()
	{
		var source = new StubAudioSource();
		var eventId = new AudioEventId(42);
		var playback = new AudioPlayback(
			new AudioLibrary([new StubBank(eventId, new StubDescription(new StubClip()))]), source);
		var target = new object();
		var parameters = new[] { AudioParam.Ref(new AudioParamId(15), target) };

		playback.Play(eventId, parameters).Should().BeTrue();

		source.Parameters[15].ReferenceValue.Should().BeSameAs(target);
	}

	[Fact]
	public void Play_ParamsOverload_AcceptsInlineParameterArguments()
	{
		var source = new StubAudioSource();
		var eventId = new AudioEventId(42);
		var playback = new AudioPlayback(
			new AudioLibrary([new StubBank(eventId, new StubDescription(new StubClip()))]), source);

		playback.Play(eventId,
			AudioParam.Float(AudioParamId.Pitch, 1.25f),
			AudioParam.Bool(AudioParamId.Loop, true)).Should().BeTrue();

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

	private sealed class StubDescription(IAudioClip clip) : IAudioEventDescription
	{
		public IAudioClip Clip { get; } = clip;
		public IAudioEventContract Contract { get; } = new StubContract();

		private readonly AudioParam[] _staticParameters =
		{
			AudioParam.Float(AudioParamId.Volume, 0.75f),
		};

		public ReadOnlySpan<AudioParam> StaticParameters => _staticParameters;
	}

	private sealed class StubContract : IAudioEventContract
	{
		public bool Validate(ReadOnlySpan<AudioParam> parameters, out string error)
		{
			error = null;
			return true;
		}
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

		public void Play(IAudioClip clip, ReadOnlySpan<AudioParam> staticParams,
			ReadOnlySpan<AudioParam> dynamicParams)
		{
			PlayedClip = clip;
			for (int index = 0; index < staticParams.Length; index++)
			{
				SetParameter(in staticParams[index]);
			}

			for (int index = 0; index < dynamicParams.Length; index++)
			{
				SetParameter(in dynamicParams[index]);
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