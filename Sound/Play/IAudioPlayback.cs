// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using Depra.Sound.Clip;
using Depra.Sound.Parameter;
using Depra.Sound.Source;

namespace Depra.Sound.Play
{
	public interface IAudioPlayback
	{
		event PlayDelegate Started;
		event StopDelegate Stopped;

		void Stop(IAudioClip clip);

		void Play(IAudioClipContainer container, IEnumerable<IAudioClipParameter> parameters);
		void Play(IAudioClipContainer container, IAudioSource source, IEnumerable<IAudioClipParameter> parameters);

		public delegate void PlayDelegate(IAudioClip clip);
		public delegate void StopDelegate(IAudioClip clip, AudioStopReason reason);
	}
}