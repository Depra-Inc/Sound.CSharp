// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Depra <n.melnikov@depra.org>

namespace Depra.Sound
{
	public interface IAudioTrack
	{
		AudioTrackSegment[] Request();
		void Release(AudioTrackSegment[] segments);
	}
}