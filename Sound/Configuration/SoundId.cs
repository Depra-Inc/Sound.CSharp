// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Runtime.CompilerServices;

namespace Depra.Sound.Configuration
{
	public readonly struct SoundId
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SoundId ValueOf(string key) => new(key);

		private readonly string _key;

		private SoundId(string key) => _key = key;

		public override string ToString() => _key;
	}
}