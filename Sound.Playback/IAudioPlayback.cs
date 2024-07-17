// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Sound.Playback
{
	public interface IAudioPlayback
	{
		event Action<IAudioTrack> Started;
		event Action<IAudioTrack, AudioStopReason> Stopped;

		void Stop(IAudioTrack track);
		void Play(IAudioTrack track, IAudioSource source);
	}
}