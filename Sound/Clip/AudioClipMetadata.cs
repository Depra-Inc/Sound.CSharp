// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Sound.Clip
{
	public readonly struct AudioClipMetadata
	{
		public readonly string Name;
		public readonly float Duration;

		public AudioClipMetadata(string name, float duration)
		{
			Name = name;
			Duration = duration;
		}
	}
}