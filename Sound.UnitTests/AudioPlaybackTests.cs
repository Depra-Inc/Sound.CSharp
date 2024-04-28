// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using Depra.Sound.Clip;
using Depra.Sound.Parameter;
using Depra.Sound.Play;
using Depra.Sound.Source;
using Depra.Sound.Storage;
using FluentAssertions;

namespace Depra.Sound.UnitTests;

public sealed class AudioPlaybackTests
{
	private readonly AudioPlayback _playback;

	public AudioPlaybackTests()
	{
		var factory = new LambdaBasedAudioSourceFactory(_ => new StubAudioSource(), _ => { });
		var storage = new AudioSourceStorage(factory);
		var types = new AudioTypeContainerBuilder()
			.Register<StubAudioClip, StubAudioSource>()
			.Build();

		_playback = new AudioPlayback(types, storage);
	}

	[Fact]
	public void Play_WhenClipIsNotPlaying_ShouldStartPlayback()
	{
		// Arrange:
		var clip = new StubAudioClip();
		var started = false;
		_playback.Started += _ => started = true;

		// Act:
		_playback.Play(clip);

		// Assert:
		started.Should().BeTrue();
	}

	[Fact]
	public void Stop_WhenClipIsPlaying_ShouldStopPlayback()
	{
		// Arrange:
		var clip = new StubAudioClip();
		var stopped = false;
		_playback.Stopped += (_, _) => stopped = true;
		_playback.Play(clip);

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
		string IAudioClip.Name => nameof(StubAudioClip);
		float IAudioClip.Duration => 0;
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