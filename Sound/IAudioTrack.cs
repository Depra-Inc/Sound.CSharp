// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Sound
{
	public interface IAudioTrack<in TSource> : IAudioTrack where TSource : IAudioSource
	{
		IAudioClip Play(TSource source);
	}

	public interface IAudioTrack
	{
		IAudioClip Play(IAudioSource source);

		void Deconstruct(out IAudioClip clip, out IAudioSourceParameter[] parameters);
	}
}