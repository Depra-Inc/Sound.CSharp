// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using Depra.Sound.Configuration;
using Depra.Sound.Storage;

namespace Depra.Sound.Play
{
	public sealed class SoundPlayback : ISoundPlayback
	{
		private readonly AudioTypeContainer _types;
		private readonly IAudioClipLibrary _clipLibrary;
		private readonly AudioSourceStorage _sourceStorage;

		public SoundPlayback(IAudioClipLibrary clipLibrary, AudioTypeContainer types, AudioSourceStorage sourceStorage)
		{
			_types = types;
			_clipLibrary = clipLibrary;
			_sourceStorage = sourceStorage;
		}

		public void Play(SoundId id)
		{
			if (_clipLibrary.TryGet(id, out var audioClip) == false)
			{
				return;
			}

			var sourceType = _types.Resolve(audioClip.GetType());
			var descriptor = new AudioSourceDescriptor(id, sourceType);
			var audioSource = _sourceStorage.Request(descriptor);
			audioSource.Play(audioClip);
		}

		public void Stop(SoundId id)
		{
			if (_clipLibrary.TryGet(id, out var audioClip) == false)
			{
				return;
			}

			var sourceType = _types.Resolve(audioClip.GetType());
		}
	}
}