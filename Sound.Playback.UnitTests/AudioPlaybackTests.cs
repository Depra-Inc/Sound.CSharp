// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Depra <n.melnikov@depra.org>

using FluentAssertions;
using NSubstitute;

namespace Depra.Sound.Playback.UnitTests;

public sealed class AudioPlaybackTests
{
	private readonly IAudioSource _sourceMock;
	private readonly IAudioPlayback _playback;
	private readonly IAudioTrack _trackMock = Substitute.For<IAudioTrack>();

	public AudioPlaybackTests()
	{
		var trackId = TrackId.ValueOf("Test");
		var clipMock = Substitute.For<IAudioClip>();
		clipMock.Name.Returns(trackId.ToString());
		clipMock.Duration.Returns(0);
		var tableMock = Substitute.For<IAudioTable>();
		tableMock.Get(trackId).Returns(_trackMock);
		_sourceMock = new StubAudioSource([clipMock.GetType()]);
		_playback = new AudioPlayback(tableMock, _sourceMock);
	}

	[Fact]
	public void Play_WhenClipIsNotPlaying_ShouldStartPlayback()
	{
		// Arrange:
		var started = false;
		_playback.Started += _ => started = true;

		// Act:
		_playback.Play(_trackMock, _sourceMock);

		// Assert:
		started.Should().BeTrue();
	}

	[Fact]
	public void Stop_WhenClipIsPlaying_ShouldStopPlayback()
	{
		// Arrange:
		var stopped = false;
		_playback.Stopped += (_, _) => stopped = true;
		_playback.Play(_trackMock, _sourceMock);

		// Act:
		_playback.Stop(_trackMock);

		// Assert:
		stopped.Should().BeTrue();
	}

	[Fact]
	public void Stop_WhenClipIsNotPlaying_ShouldNotThrow()
	{
		// Arrange:

		// Act:
		var act = () => _playback.Stop(_trackMock);

		// Assert:
		act.Should().NotThrow();
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
}