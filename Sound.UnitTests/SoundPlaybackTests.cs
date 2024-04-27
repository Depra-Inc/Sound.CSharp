// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using Depra.Sound.Clip;
using Depra.Sound.Configuration;
using Depra.Sound.Play;
using Depra.Sound.Source;
using Depra.Sound.Storage;

namespace Depra.Sound.UnitTests;

public sealed class SoundPlaybackTests
{
	public SoundPlaybackTests()
	{
		var library = new StubAudioClipLibrary();
		var factory = new LambdaBasedAudioSourceFactory(type => new StubAudioSource());
		var storage = new AudioSourceStorage(factory);
		var types = new AudioTypeContainerBuilder()
			.Register<StubAudioClip, StubAudioSource>()
			.Build();

		var soundPlayer = new SoundPlayback(library, types, storage);
	}

	private sealed class StubAudioClipLibrary : IAudioClipLibrary
	{
		bool IAudioClipLibrary.Contains(SoundId id) => false;

		IAudioClip IAudioClipLibrary.Get(SoundId id) => new StubAudioClip();

		IEnumerable<IAudioClip> IAudioClipLibrary.GetAll()
		{
			yield return new StubAudioClip();
		}
	}

	private sealed class StubAudioClip : IAudioClip
	{
		string IAudioClip.Name => nameof(StubAudioClip);

		float IAudioClip.Duration => 0;
	}

	private sealed class StubAudioSource : IAudioSource
	{
		float IAudioSource.Volume { get; set; }
		bool IAudioSource.IsPlaying => false;

		void IAudioSource.Play(IAudioClip clip) { }
		void IAudioSource.Stop() { }
	}
}