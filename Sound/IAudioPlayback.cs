// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Depra <n.melnikov@depra.org>

using System;

namespace Depra.Sound.Playback
{
	public interface IAudioPlayback
	{
		void Stop();
		bool Play(AudioEventId eventId);
		bool Play(AudioEventId eventId, IAudioSource source);
		bool Play(AudioEventId eventId, ReadOnlySpan<AudioParam> parameters);
		bool Play(AudioEventId eventId, ReadOnlySpan<AudioParam> parameters, IAudioSource source);
	}
}