// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using Depra.Sound.Clip;
using Depra.Sound.Parameter;
using Depra.Sound.Source;
using Depra.Sound.Storage;

namespace Depra.Sound.Play
{
	public sealed class AudioPlayback : IAudioPlayback
	{
		private readonly AudioTypeContainer _types;
		private readonly AudioSourceStorage _storage;
		private readonly Dictionary<IAudioClip, IAudioSource> _lookup = new();

		public event IAudioPlayback.PlayDelegate Started;
		public event IAudioPlayback.StopDelegate Stopped;

		public AudioPlayback(AudioTypeContainer types, AudioSourceStorage storage)
		{
			_types = types;
			_storage = storage;
		}

		public void Play(IAudioClip clip, params IAudioClipParameter[] parameters)
		{
			var sourceType = _types.Resolve(clip.GetType());
			var descriptor = new AudioSourceDescriptor(sourceType);
			var source = _storage.Request(descriptor);
			source.Play(clip, parameters);
			source.Stopped += OnStop;

			_lookup.Add(clip, source);
			Started?.Invoke(clip);

			void OnStop(AudioStopReason reason)
			{
				source.Stopped -= OnStop;
				_lookup.Remove(clip);
				Stopped?.Invoke(clip, reason);
			}
		}

		public void Stop(IAudioClip clip)
		{
			if (_lookup.Remove(clip, out var source) == false)
			{
				return;
			}

			source.Stop();
			Stopped?.Invoke(clip, AudioStopReason.STOPPED);
		}
	}
}