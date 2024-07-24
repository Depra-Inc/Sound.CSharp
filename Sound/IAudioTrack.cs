// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Sound
{
	public interface IAudioTrack<in TSource> : IAudioTrack where TSource : IAudioSource
	{
		void Play(TSource source);
	}

	public interface IAudioTrack
	{
		void Play(IAudioSource source);

		AudioTrackSegment[] Deconstruct();
	}
}