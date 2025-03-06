// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Depra <n.melnikov@depra.org>

namespace Depra.Sound
{
	public readonly struct AudioTrackSegment
	{
		public readonly IAudioClip Clip;
		public readonly IAudioSourceParameter[] Parameters;

		public AudioTrackSegment(IAudioClip clip, IAudioSourceParameter[] parameters)
		{
			Clip = clip;
			Parameters = parameters;
		}
	}
}