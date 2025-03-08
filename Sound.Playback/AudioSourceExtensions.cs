// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Depra <n.melnikov@depra.org>

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Depra.Sound.Playback
{
	public static class AudioSourceExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Play(this IAudioSource self, IAudioTrack track)
		{
			var segments = new List<AudioTrackSegment>();
			track.ExtractSegments(segments);

			foreach (var segment in segments)
			{
				if (segment.IsValid())
				{
					self.Play(segment.Clip, segment.Parameters);
				}
			}
		}
	}
}