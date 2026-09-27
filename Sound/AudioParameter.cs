// SPDX-License-Identifier: Apache-2.0
// © 2024-2026 Depra <n.melnikov@depra.org>

namespace Depra.Sound
{
	public readonly struct AudioParameterId
	{
		public static readonly AudioParameterId Volume = new(1);
		public static readonly AudioParameterId Loop = new(2);
		public static readonly AudioParameterId Pan = new(3);
		public static readonly AudioParameterId Pitch = new(4);

		public readonly int Value;

		public AudioParameterId(int value) => Value = value;
	}

	public enum AudioParameterType : byte
	{
		FLOAT,
		INT,
		BOOL,
		STRING,
	}

	public readonly struct AudioParameter
	{
		public readonly AudioParameterId Id;
		public readonly AudioParameterType Type;
		public readonly float FloatValue;
		public readonly int IntValue;
		public readonly string StringValue;

		private AudioParameter(AudioParameterId id, AudioParameterType type, float floatValue,
			int intValue, string stringValue)
		{
			Id = id;
			Type = type;
			FloatValue = floatValue;
			IntValue = intValue;
			StringValue = stringValue;
		}

		public static AudioParameter Float(AudioParameterId id, float value) =>
			new(id, AudioParameterType.FLOAT, value, 0, null);

		public static AudioParameter Int(AudioParameterId id, int value) =>
			new(id, AudioParameterType.INT, 0, value, null);

		public static AudioParameter Bool(AudioParameterId id, bool value) =>
			new(id, AudioParameterType.BOOL, 0, value ? 1 : 0, null);

		public static AudioParameter String(AudioParameterId id, string value) =>
			new(id, AudioParameterType.STRING, 0, 0, value);
	}
}
