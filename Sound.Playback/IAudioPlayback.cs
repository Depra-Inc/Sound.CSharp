﻿// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Depra <n.melnikov@depra.org>

namespace Depra.Sound.Playback
{
	public interface IAudioPlayback
	{
		void Play(TrackId id);
		void Play(IAudioTrack track);
		void Play(IAudioTrack track, IAudioSource source);
		void Stop(IAudioTrack track, IAudioSource source = null);
	}
}