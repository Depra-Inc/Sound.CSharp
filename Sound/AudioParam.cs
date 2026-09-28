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

		public static bool operator ==(AudioParamId left, AudioParamId right) => left.Equals(right);
		public static bool operator !=(AudioParamId left, AudioParamId right) => !left.Equals(right);
		public static implicit operator AudioParamId(int value) => new(value);
		public static implicit operator int(AudioParamId id) => id.Value;

		public readonly int Value;
		public AudioParamId(int value) => Value = value;

		public bool Equals(AudioParamId other) => Value == other.Value;
		public override bool Equals(object obj) => obj is AudioParamId other && Equals(other);

		public override int GetHashCode() => Value;
	}

	public readonly struct AudioParam
	{
		public static AudioParam Float(AudioParamId id, float value) =>
			new(id, AudioParamType.FLOAT, 0, value, 0, 0, 0, 0, null);

		public static AudioParam Int(AudioParamId id, int value) =>
			new(id, AudioParamType.INT, 0, 0, 0, 0, 0, value, null);

		public static AudioParam Bool(AudioParamId id, bool value) =>
			new(id, AudioParamType.BOOL, 0, 0, 0, 0, 0, value ? 1 : 0, null);

		public static AudioParam Vector3(AudioParamId id, float x, float y, float z) =>
			new(id, AudioParamType.VECTOR3, 0, x, y, z, 0, 0, null);

		public static AudioParam Ref<T>(AudioParamId id, T value) where T : class =>
			new(id, AudioParamType.REFERENCE, 0, 0, 0, 0, 0, 0, value);

		public static AudioParam String(AudioParamId id, string value) => Ref(id, value);

		public static AudioParam Custom(int customTypeId, AudioParamId id,
			float float0 = 0, float float1 = 0, float float2 = 0, float float3 = 0, long integerValue = 0) =>
			new(id, AudioParamType.CUSTOM, customTypeId, float0, float1, float2, float3, integerValue, null);

		public static AudioParam CustomRef<T>(int customTypeId, AudioParamId id, T value,
			float float0 = 0, float float1 = 0, float float2 = 0, float float3 = 0, long integerValue = 0)
			where T : class => new(id, AudioParamType.CUSTOM, customTypeId, float0, float1, float2, float3,
			integerValue, value);

		public readonly AudioParamId Id;
		public readonly AudioParamType Type;
		public readonly int CustomTypeId;

		public readonly float Float0;
		public readonly float Float1;
		public readonly float Float2;
		public readonly float Float3;
		public readonly long IntegerValue;
		public readonly object ReferenceValue;

		public float FloatValue => Float0;
		public int IntValue => (int)IntegerValue;

		private AudioParam(AudioParamId id, AudioParamType type, int customTypeId,
			float float0, float float1, float float2, float float3, long integerValue, object referenceValue)
		{
			Id = id;
			Type = type;
			CustomTypeId = customTypeId;
			Float0 = float0;
			Float1 = float1;
			Float2 = float2;
			Float3 = float3;
			IntegerValue = integerValue;
			ReferenceValue = referenceValue;
		}

		public struct Builder
		{
			private AudioParamSet _set;

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public AudioParamSet Build() => _set;

			public Builder Add(AudioParam parameter)
			{
				_set.TryAdd(parameter);
				return this;
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public Builder Volume(float value) => Add(Float(AudioParamId.Volume, value));

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public Builder Loop(bool value) => Add(Bool(AudioParamId.Loop, value));

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public Builder Pan(float value) => Add(Float(AudioParamId.Pan, value));

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public Builder Pitch(float value) => Add(Float(AudioParamId.Pitch, value));
		}
	}

	public enum AudioParamType : byte
	{
		FLOAT,
		INT,
		BOOL,
		VECTOR3,
		REFERENCE,
		CUSTOM
	}

	public struct AudioParamSet
	{
		private AudioParam _p0;
		private AudioParam _p1;
		private AudioParam _p2;
		private AudioParam _p3;
		private AudioParam _p4;
		private AudioParam _p5;
		private AudioParam _p6;
		private AudioParam _p7;

		public byte Count { get; private set; }

		public bool TryAdd(AudioParam parameter)
		{
			switch (Count)
			{
				case 0: _p0 = parameter; break;
				case 1: _p1 = parameter; break;
				case 2: _p2 = parameter; break;
				case 3: _p3 = parameter; break;
				case 4: _p4 = parameter; break;
				case 5: _p5 = parameter; break;
				case 6: _p6 = parameter; break;
				case 7: _p7 = parameter; break;
				default: return false;
			}

			Count++;
			return true;
		}

		public readonly void CopyTo(AudioParam[] destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException(nameof(destination));
			}

			if (destination.Length < Count)
			{
				throw new ArgumentException("Destination array is smaller than AudioParamSet size.",
					nameof(destination));
			}

			switch (Count)
			{
				case 8:
					destination[7] = _p7;
					goto case 7;
				case 7:
					destination[6] = _p6;
					goto case 6;
				case 6:
					destination[5] = _p5;
					goto case 5;
				case 5:
					destination[4] = _p4;
					goto case 4;
				case 4:
					destination[3] = _p3;
					goto case 3;
				case 3:
					destination[2] = _p2;
					goto case 2;
				case 2:
					destination[1] = _p1;
					goto case 1;
				case 1:
					destination[0] = _p0;
					break;
			}
		}
	}
}