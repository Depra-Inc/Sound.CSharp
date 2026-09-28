// SPDX-License-Identifier: Apache-2.0
// © 2024-2026 Depra <n.melnikov@depra.org>

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Depra.Sound
{
	[Serializable]
	[DebuggerDisplay("AudioEventId: {Value}")]
	public struct AudioEventId : IEquatable<AudioEventId>
	{
		public static readonly AudioEventId NULL = new(0);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(AudioEventId left, AudioEventId right) => left.Equals(right);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(AudioEventId left, AudioEventId right) => !left.Equals(right);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator AudioEventId(ulong value) => new(value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator ulong(AudioEventId id) => id.Value;

		public ulong Value;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public AudioEventId(ulong value) => Value = value;

		public bool Equals(AudioEventId other) => Value == other.Value;
		public override bool Equals(object obj) => obj is AudioEventId other && Equals(other);

		public override int GetHashCode() => Value.GetHashCode();
	}
}