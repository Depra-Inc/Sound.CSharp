// SPDX-License-Identifier: Apache-2.0
// © 2024-2026 Depra <n.melnikov@depra.org>

using System;
using System.Runtime.CompilerServices;

namespace Depra.Sound
{
	public readonly struct AudioParamId : IEquatable<AudioParamId>
	{
		public static readonly AudioParamId Unknown = new(0);
		public static readonly AudioParamId Volume = new(1);
		public static readonly AudioParamId Loop = new(2);
		public static readonly AudioParamId Pan = new(3);
		public static readonly AudioParamId Pitch = new(4);
		public static readonly AudioParamId Custom = new(9);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(AudioParamId left, AudioParamId right) => left.Equals(right);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(AudioParamId left, AudioParamId right) => !left.Equals(right);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator AudioParamId(int value) => new(value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int(AudioParamId id) => id.Value;

		public readonly int Value;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public AudioParamId(int value) => Value = value;

		public bool Equals(AudioParamId other) => Value == other.Value;
		public override bool Equals(object obj) => obj is AudioParamId other && Equals(other);
		public override int GetHashCode() => Value;
	}
}