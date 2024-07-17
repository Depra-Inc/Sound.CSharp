// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Sound.Playback
{
	public sealed class AudioPlayback : IAudioPlayback
	{
		public event Action<IAudioTrack> Started;
		public event Action<IAudioTrack, AudioStopReason> Stopped;

		public void Play(IAudioTrack track, IAudioSource source)
		{
			source.Started += OnStart;
			_ = track.Play(source);
			source.Stopped += OnStop;

			void OnStart()
			{
				source.Started -= OnStart;
				Started?.Invoke(track);
			}

			void OnStop(AudioStopReason reason)
			{
				source.Stopped -= OnStop;
				Stopped?.Invoke(track, reason);
			}
		}

		public void Stop(IAudioTrack track) => Stopped?.Invoke(track, AudioStopReason.STOPPED);
	}
}