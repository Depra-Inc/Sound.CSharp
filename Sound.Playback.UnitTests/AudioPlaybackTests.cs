// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Depra <n.melnikov@depra.org>

using FluentAssertions;
using NSubstitute;

namespace Depra.Sound.Playback.UnitTests;

public sealed class AudioPlaybackTests
{
	private readonly IAudioClip _clipMock;
	private readonly IAudioSource _sourceMock;
	private readonly IAudioPlayback _playback;

	public AudioPlaybackTests()
	{
		var trackId = TrackId.ValueOf("Test");
		_clipMock = Substitute.For<IAudioClip>();
		_clipMock.Name.Returns(trackId.ToString());
		_clipMock.Duration.Returns(0);
		_sourceMock = new StubAudioSource([_clipMock.GetType()]);
		_playback = new AudioPlayback(_sourceMock);
	}

	[Fact]
	public void Stop_WhenClipIsNotPlaying_ShouldNotThrow()
	{
		// Arrange:

		// Act:
		var act = () => _playback.Stop();

		// Assert:
		act.Should().NotThrow();
	}

	[Fact]
	public void Play_WhenClipIsNotPlaying_ShouldNotThrow()
	{
		// Arrange:
		var track = new StubTrack(() => [new AudioTrackSegment(new IAudioClip.Null(), [])]);

		// Act:
		var act = () => _playback.Play(track);

		// Assert:
		act.Should().NotThrow();
	}

	[Fact]
	public void Play_WhenClipIsNotPlaying_ShouldInvokeStarted()
	{
		// Arrange:
		var started = false;
		var track = new StubTrack(() => [new AudioTrackSegment(_clipMock, [])]);
		_sourceMock.Started += () => started = true;

		// Act:
		_playback.Play(track);

		// Assert:
		started.Should().BeTrue();
	}

	private sealed class StubAudioSource(IEnumerable<Type> supportedClips) : IAudioSource
	{
		public event Action? Started;
		public event Action<AudioStopReason>? Stopped;

		bool IAudioSource.IsPlaying => false;
		IAudioClip IAudioSource.Current => new IAudioClip.Null();
		IEnumerable<Type> IAudioSource.SupportedClips { get; } = supportedClips;

		void IAudioSource.Stop() => Stopped?.Invoke(AudioStopReason.FINISHED);
		void IAudioSource.Play(IAudioClip clip, IEnumerable<IAudioSourceParameter> parameters) => Started?.Invoke();

		IAudioSourceParameter IAudioSource.Read(Type parameterType) => new EmptyParameter();
		IEnumerable<IAudioSourceParameter> IAudioSource.EnumerateParameters() => Array.Empty<IAudioSourceParameter>();
	}

	private sealed class StubTrack(Func<IEnumerable<AudioTrackSegment>> factory) : IAudioTrack
	{
		void IAudioTrack.ExtractSegments(IList<AudioTrackSegment> segments)
		{
			var result = factory();
			foreach (var segment in result)
			{
				segments.Add(segment);
			}
		}
	}
}