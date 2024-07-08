// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using Depra.Sound.Clip;
using Depra.Sound.Parameter;
using Depra.Sound.Play;
using Depra.Sound.Source;
using Depra.Sound.Storage;
using FluentAssertions;
using NSubstitute;

namespace Depra.Sound.UnitTests;

public sealed class AudioPlaybackTests
{
	private readonly AudioPlayback _playback;

	public AudioPlaybackTests()
	{
		var types = new AudioTypeLookup()
			.Register<StubAudioClip, StubAudioSource>();

		var factories = new AudioSourceFactoryLookup()
			.Register<StubAudioSource>(new LambdaBasedAudioSourceFactory(
				createFunc: () => new StubAudioSource(),
				destroyFunc: _ => { }));

		_playback = new AudioPlayback(types, factories);
	}

	[Fact]
	public void Play_WhenClipIsNotPlaying_ShouldStartPlayback()
	{
		// Arrange:
		var clip = new StubAudioClip();
		var container = Substitute.For<IAudioClipContainer>();
		container.Next().Returns(clip);
		var started = false;
		_playback.Started += _ => started = true;

		// Act:
		_playback.Play(container);

		// Assert:
		started.Should().BeTrue();
	}

	[Fact]
	public void Stop_WhenClipIsPlaying_ShouldStopPlayback()
	{
		// Arrange:
		var clip = new StubAudioClip();
		var container = Substitute.For<IAudioClipContainer>();
		container.Next().Returns(clip);
		var stopped = false;
		_playback.Stopped += (_, _) => stopped = true;
		_playback.Play(container);

		// Act:
		_playback.Stop(clip);

		// Assert:
		stopped.Should().BeTrue();
	}

	[Fact]
	public void Stop_WhenClipIsNotPlaying_ShouldNotThrow()
	{
		// Arrange:
		var clip = new StubAudioClip();

		// Act:
		var act = () => _playback.Stop(clip);

		// Assert:
		act.Should().NotThrow();
	}

	private sealed class StubAudioClip : IAudioClip
	{
		AudioClipMetadata IAudioClip.Metadata() => new(nameof(StubAudioClip), 0);
	}

	private sealed class StubAudioSource : IAudioSource
	{
		public event IAudioSource.PlayDelegate? Started;
		public event IAudioSource.StopDelegate? Stopped;

		bool IAudioSource.IsPlaying => false;
		IAudioClipParameters IAudioSource.Parameters => null!;

		void IAudioSource.Play(IAudioClip clip) => Started?.Invoke();
		void IAudioSource.Stop() => Stopped?.Invoke(AudioStopReason.FINISHED);
	}
}