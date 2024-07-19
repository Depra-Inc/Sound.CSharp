// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Sound
{
	public interface IAudioSourceParameter { }

	[Serializable]
	public readonly struct NullParameter : IAudioSourceParameter { }

	[Serializable]
	public readonly struct EmptyParameter : IAudioSourceParameter { }

	[Serializable]
	public struct VolumeParameter : IAudioSourceParameter
	{
		public float Value;

		public VolumeParameter(float value) => Value = value;
	}

	[Serializable]
	public struct LoopParameter : IAudioSourceParameter
	{
		public bool Value;

		public LoopParameter(bool value) => Value = value;
	}

	[Serializable]
	public struct PanParameter : IAudioSourceParameter
	{
		public float Value;

		public PanParameter(float value) => Value = value;
	}

	[Serializable]
	public struct PitchParameter : IAudioSourceParameter
	{
		public float Value;

		public PitchParameter(float value) => Value = value;
	}
}