// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

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

		void Play(IAudioClip clip, params IAudioClipParameter[] parameters);
		void Play(IAudioClip clip, IAudioSource source, params IAudioClipParameter[] parameters);

		public delegate void PlayDelegate(IAudioClip clip);
		public delegate void StopDelegate(IAudioClip clip, AudioStopReason reason);
	}
}