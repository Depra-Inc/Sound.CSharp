﻿// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Depra <n.melnikov@depra.org>

using System.Runtime.CompilerServices;

namespace Depra.Sound
{
	public readonly struct TrackId
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TrackId ValueOf(string key) => new(key);

		private readonly string _key;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private TrackId(string key) => _key = key;

		public override string ToString() => _key;
	}
}