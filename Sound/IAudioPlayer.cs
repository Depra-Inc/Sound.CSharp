// SPDX-License-Identifier: Apache-2.0
// © 2024-2026 Depra <n.melnikov@depra.org>

using System;

namespace Depra.Sound
{
	public interface IAudioPlayer
	{
		void Stop();
		PlayHandle Play(AudioEventId eventId);
		PlayHandle Play(AudioEventId eventId, IAudioSource source);
		PlayHandle Play(AudioEventId eventId, ReadOnlySpan<AudioParam> parameters);
		PlayHandle Play(AudioEventId eventId, ReadOnlySpan<AudioParam> parameters, IAudioSource source);
	}
}