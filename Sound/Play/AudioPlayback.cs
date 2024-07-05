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
		private readonly AudioSourceFactoryLookup _factories;
		private readonly Dictionary<IAudioClip, Queue<IAudioSource>> _activeSources = new();

		public event IAudioPlayback.PlayDelegate Started;
		public event IAudioPlayback.StopDelegate Stopped;

		public AudioPlayback(AudioTypeLookup types, AudioSourceFactoryLookup factories)
		{
			Guard.AgainstNull(types, nameof(types));
			Guard.AgainstNull(factories, nameof(factories));

			_types = types;
			_factories = factories;
		}

		public void Play(IAudioClip clip, params IAudioClipParameter[] parameters) =>
			Play(clip, RequestSource(clip), parameters);

		public void Play(IAudioClip clip, IAudioSource source, params IAudioClipParameter[] parameters)
		{
			Guard.AgainstNull(clip, nameof(clip));

			source.Play(clip, parameters);
			source.Stopped += OnStop;

			Add(clip, source);
			Started?.Invoke(clip);

			void OnStop(AudioStopReason reason)
			{
				source.Stopped -= OnStop;
				_activeSources.Remove(clip);
				Stopped?.Invoke(clip, reason);
			}
		}

		public void Stop(IAudioClip clip)
		{
			Guard.AgainstNull(clip, nameof(clip));
			if (_activeSources.Remove(clip, out var sources) == false)
			{
				return;
			}

			foreach (var source in sources)
			{
				source.Stop();
			}

			Stopped?.Invoke(clip, AudioStopReason.STOPPED);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Add(IAudioClip clip, IAudioSource source)
		{
			if (_activeSources.TryGetValue(clip, out var sources))
			{
				sources.Enqueue(source);
			}
			else
			{
				_activeSources.Add(clip, new Queue<IAudioSource>(new[] { source }));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private IAudioSource RequestSource(IAudioClip clip)
		{
			if (_activeSources.TryGetValue(clip, out var sources))
			{
				return ReuseSource(sources);
			}

			var sourceType = _types.Resolve(clip.GetType());
			var factory = _factories.Resolve(sourceType);
			return factory.Create();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private IAudioSource ReuseSource(Queue<IAudioSource> sources)
		{
			var source = sources.Peek();
			if (source.IsPlaying)
			{
				source.Stop();
			}

			return source;
		}
	}
}