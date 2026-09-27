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
		var table = new AudioTable([new StubBank(eventId, description)]);
		var playback = new AudioPlayback(table, source);
		var parameters = new[] { AudioParameter.Float(AudioParameterId.Pitch, 1.25f) };

		playback.Play(eventId, parameters).Should().BeTrue();

		source.PlayedClip.Should().BeSameAs(clip);
		source.Parameters[AudioParameterId.Volume.Value].FloatValue.Should().Be(0.75f);
		source.Parameters[AudioParameterId.Pitch.Value].FloatValue.Should().Be(1.25f);
	}

	[Fact]
	public void Play_UnknownEventId_ReturnsFalseWithoutStartingSource()
	{
		var source = new StubAudioSource();
		var table = new AudioTable([new StubBank(new AudioEventId(42), new StubDescription(new StubClip()))]);
		var playback = new AudioPlayback(table, source);

		playback.Play(new AudioEventId(7)).Should().BeFalse();
		source.PlayedClip.Should().BeNull();
	}

	private sealed class StubClip : IAudioClip
	{
		public string Name => "stub";
		public float Duration => 1f;
	}

	private sealed class StubDescription(IAudioClip clip) : IAudioEventDescription
	{
		public IAudioClip Clip { get; } = clip;

		public void ApplyStaticParameters(IAudioSource source) =>
			source.SetParameter(AudioParameter.Float(AudioParameterId.Volume, 0.75f));
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
		public event Action? Started;
		public event Action<AudioStopReason>? Stopped;

		public bool IsPlaying => PlayedClip != null;
		public IAudioClip? PlayedClip { get; private set; }
		public IAudioClip Current => PlayedClip!;
		public Dictionary<int, AudioParameter> Parameters { get; } = new();

		public void SetParameter(in AudioParameter parameter) => Parameters[parameter.Id.Value] = parameter;

		public void Stop()
		{
			PlayedClip = null;
			Stopped?.Invoke(AudioStopReason.STOPPED);
		}

		public void Play(IAudioClip clip)
		{
			PlayedClip = clip;
			Started?.Invoke();
		}
	}
}