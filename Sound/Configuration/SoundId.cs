// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Sound.Configuration
{
	public readonly struct SoundId
	{
		public static SoundId ValueOf(string key) => new(key);

		private readonly string _key;

		private SoundId(string key) => _key = key;

		public override string ToString() => _key;
	}
}