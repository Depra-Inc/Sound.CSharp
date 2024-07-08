// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using Depra.Sound.Clip;
using Depra.Sound.Source;

namespace Depra.Sound.Play
{
	internal sealed class AudioSourceCache
	{
		private readonly Dictionary<IAudioClip, Queue<IAudioSource>> _active = new();

		public void Add(IAudioClip clip, IAudioSource source)
		{
			if (_active.TryGetValue(clip, out var sources))
			{
				sources.Enqueue(source);
			}
			else
			{
				_active.Add(clip, new Queue<IAudioSource>(new[] { source }));
			}
		}

		public void Remove(IAudioClip clip) => _active.Remove(clip);

		public bool Request(IAudioClip clip, out IAudioSource source)
		{
			if (_active.TryGetValue(clip, out var sources))
			{
				source = sources.Peek();
				return true;
			}

			source = null;
			return false;
		}

		public void Return(IAudioClip clip)
		{
			if (_active.Remove(clip, out var sources) == false)
			{
				return;
			}

			foreach (var source in sources)
			{
				source.Stop();
			}
		}
	}
}