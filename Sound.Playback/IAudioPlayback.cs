// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Depra <n.melnikov@depra.org>

namespace Depra.Sound.Playback
{
	public interface IAudioPlayback
	{
		void Stop();

		void Play(IAudioTrack track, ParameterConverter converter = null);
		void Play(IAudioTrack track, IAudioSource source, ParameterConverter converter = null);
	}
}