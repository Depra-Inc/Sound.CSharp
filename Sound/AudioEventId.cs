// SPDX-License-Identifier: Apache-2.0
// © 2024-2026 Depra <n.melnikov@depra.org>

namespace Depra.Sound
{
	public readonly struct AudioEventId : System.IEquatable<AudioEventId>
	{
		public readonly int Value;

		public AudioEventId(int value) => Value = value;

		public bool Equals(AudioEventId other) => Value == other.Value;

		public override bool Equals(object obj) => obj is AudioEventId other && Equals(other);

		public override int GetHashCode() => Value;

		public static bool operator ==(AudioEventId left, AudioEventId right) => left.Equals(right);

		public static bool operator !=(AudioEventId left, AudioEventId right) => !left.Equals(right);
	}
}