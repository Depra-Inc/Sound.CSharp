// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Depra.Sound.Clip;
using Depra.Sound.Exceptions;
using Depra.Sound.Parameter;
using Depra.Sound.Source;
using Depra.Sound.Storage;

namespace Depra.Sound.Play
{
	public sealed class AudioPlayback : IAudioPlayback
	{
		private readonly AudioTypeLookup _types;
		private readonly AudioSourceCache _cache;
		private readonly AudioSourceFactoryLookup _factories;

		public event IAudioPlayback.PlayDelegate Started;
		public event IAudioPlayback.StopDelegate Stopped;

		public AudioPlayback(AudioTypeLookup types, AudioSourceFactoryLookup factories)
		{
			Guard.AgainstNull(types, nameof(types));
			Guard.AgainstNull(factories, nameof(factories));

			_types = types;
			_factories = factories;
			_cache = new AudioSourceCache();
		}

		public void Play(IAudioClipContainer container, IEnumerable<IAudioClipParameter> parameters)
		{
			var clip = container.Next();
			Play(container, RequestSource(clip), parameters);
		}

		public void Play(IAudioClipContainer container, IAudioSource source, IEnumerable<IAudioClipParameter> parameters)
		{
			var clip = container.Next();
			Guard.AgainstNull(clip, nameof(clip));

			source.Play(clip);
			source.AddOrUpdate(parameters);
			source.Stopped += OnStop;
			_cache.Add(clip, source);

			Started?.Invoke(clip);

			void OnStop(AudioStopReason reason)
			{
				source.Stopped -= OnStop;
				_cache.Remove(clip);
				Stopped?.Invoke(clip, reason);
			}
		}

		public void Stop(IAudioClip clip)
		{
			Guard.AgainstNull(clip, nameof(clip));

			_cache.Return(clip);
			Stopped?.Invoke(clip, AudioStopReason.STOPPED);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private IAudioSource RequestSource(IAudioClip clip)
		{
			if (_cache.Request(clip, out var source))
			{
				if (source.IsPlaying)
				{
					source.Stop();
				}

				return source;
			}

			var sourceType = _types.Resolve(clip.GetType());
			var factory = _factories.Resolve(sourceType);
			return factory.Create();
		}
	}
}