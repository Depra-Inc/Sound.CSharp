// SPDX-License-Identifier: Apache-2.0
// © 2024-2026 Depra <n.melnikov@depra.org>

using System.Runtime.CompilerServices;

namespace Depra.Sound
{
	public readonly struct AudioParam
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AudioParam Bool(AudioParamId id, bool value) =>
			new(id, AudioParamType.BOOL, 0, 0, 0, 0, value ? 1 : 0, null);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AudioParam Int(AudioParamId id, int value) =>
			new(id, AudioParamType.INT, 0, 0, 0, 0, value, null);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AudioParam NamedInt(AudioParamId id, string label, int value) =>
			new(id, AudioParamType.NAMED_INT, 0, 0, 0, value, null, label);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AudioParam Float(AudioParamId id, float value) =>
			new(id, AudioParamType.FLOAT, value, 0, 0, 0, 0, null);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AudioParam Float3(AudioParamId id, float x, float y, float z) =>
			new(id, AudioParamType.FLOAT3, x, y, z, 0, 0, null);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AudioParam NamedFloat(AudioParamId id, string label, float value) =>
			new(id, AudioParamType.NAMED_FLOAT, value, 0, 0, 0, null, label);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AudioParam Ref<T>(AudioParamId id, T value) where T : class =>
			new(id, AudioParamType.REFERENCE, 0, 0, 0, 0, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AudioParam String(AudioParamId id, string value) => Ref(id, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AudioParam NamedString(AudioParamId id, string label, string value) =>
			new(id, AudioParamType.NAMED_STRING, 0, 0, 0, 0, value, label);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AudioParam Custom(AudioParamId id,
			float float0 = 0, float float1 = 0, float float2 = 0, long integerValue = 0) =>
			new(id, AudioParamType.CUSTOM, float0, float1, float2, integerValue, null);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AudioParam CustomRef<T>(AudioParamId id, T value,
			float float0 = 0, float float1 = 0, float float2 = 0, long integerValue = 0)
			where T : class => new(id, AudioParamType.CUSTOM, float0, float1, float2, integerValue, value);

		public readonly AudioParamId Id;
		public readonly AudioParamType Type;

		public readonly float Float0;
		public readonly float Float1;
		public readonly float Float2;
		public readonly long IntegerValue;

		public readonly string Name;
		public readonly object ReferenceValue;

		public float FloatValue
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => Float0;
		}

		public int IntValue
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => (int)IntegerValue;
		}

		public string StringValue
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => ReferenceValue as string;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private AudioParam(AudioParamId id, AudioParamType type,
			float float0, float float1, float float2, long integerValue,
			object referenceValue, string name = "")
		{
			Id = id;
			Type = type;
			Name = name;
			Float0 = float0;
			Float1 = float1;
			Float2 = float2;
			IntegerValue = integerValue;
			ReferenceValue = referenceValue;
		}

		public struct Builder
		{
			private AudioParamSet _set;

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public AudioParamSet Build() => _set;

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
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
}