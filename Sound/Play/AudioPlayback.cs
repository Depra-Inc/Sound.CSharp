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
		private readonly AudioTypeContainer _types;
		private readonly IAudioSourceFactory _factory;
		private readonly Dictionary<IAudioClip, Queue<IAudioSource>> _lookup = new();

		public event IAudioPlayback.PlayDelegate Started;
		public event IAudioPlayback.StopDelegate Stopped;

		public AudioPlayback(AudioTypeContainer types, IAudioSourceFactory factory)
		{
			Guard.AgainstNull(types, nameof(types));
			Guard.AgainstNull(factory, nameof(factory));

			_types = types;
			_factory = factory;
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
				_lookup.Remove(clip);
				Stopped?.Invoke(clip, reason);
			}
		}

		public void Stop(IAudioClip clip)
		{
			Guard.AgainstNull(clip, nameof(clip));
			if (_lookup.Remove(clip, out var sources) == false)
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
			if (_lookup.TryGetValue(clip, out var sources))
			{
				sources.Enqueue(source);
			}
			else
			{
				_lookup.Add(clip, new Queue<IAudioSource>(new[] { source }));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private IAudioSource RequestSource(IAudioClip clip)
		{
			IAudioSource source;
			if (_lookup.TryGetValue(clip, out var sources))
			{
				source = sources.Peek();
				if (source.IsPlaying)
				{
					source.Stop();
				}
			}
			else
			{
				var sourceType = _types.Resolve(clip.GetType());
				source = _factory.Create(sourceType);
			}

			return source;
		}
	}
}