// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Sound.Clip
{
	[Serializable]
	internal sealed class AudioPack
	{
		public float Volume;
		public IAudioClip[] Clips;
		public AudioClipPlayMode PlayMode;

		public AudioPack(float volume = 1f, AudioClipPlayMode mode = AudioClipPlayMode.ONE_TIME, params IAudioClip[] clips)
		{
			Clips = clips;
			Volume = volume;
			PlayMode = mode;
		}
	}
}