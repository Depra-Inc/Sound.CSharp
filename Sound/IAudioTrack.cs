// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Depra <n.melnikov@depra.org>

using System.Collections.Generic;

namespace Depra.Sound
{
	public interface IAudioTrack
	{
		void ExtractSegments(IList<AudioTrackSegment> segments);
	}
}