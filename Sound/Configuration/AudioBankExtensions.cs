// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Runtime.CompilerServices;
using Depra.Sound.Clip;

namespace Depra.Sound.Configuration
{
	public static class AudioBankExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetClip(this IAudioBank self, SoundId id, out IAudioClip clip)
		{
			if (self.Contains(id))
			{
				clip = self.Get(id);
				return true;
			}

			clip = null;
			return false;
		}
	}
}