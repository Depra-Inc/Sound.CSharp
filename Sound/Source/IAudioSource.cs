// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using Depra.Sound.Clip;
using Depra.Sound.Parameter;

namespace Depra.Sound.Source
{
	public interface IAudioSource
	{
		event PlayDelegate Started;
		event StopDelegate Stopped;

		bool IsPlaying { get; }
		IAudioClipParameters Parameters { get; }

		void Play(IAudioClip clip);
		void Stop();

		public delegate void PlayDelegate();
		public delegate void StopDelegate(AudioStopReason reason);
	}
}