// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using Depra.Sound.Clip;

namespace Depra.Sound.Configuration
{
	public static class AudioClipLibraryExtensions
	{
		public static bool TryGetClip(this IAudioBank self, SoundId id, out IAudioClip clip)
		{
			if (self.Contains(id))
			{
				clip = self.GetClip(id);
				return true;
			}

			clip = null;
			return false;
		}
	}
}