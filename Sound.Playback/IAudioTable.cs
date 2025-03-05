// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Depra <n.melnikov@depra.org>

namespace Depra.Sound.Playback
{
	public interface IAudioTable
	{
		IAudioTrack Get(TrackId id);
	}
}