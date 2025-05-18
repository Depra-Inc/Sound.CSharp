// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Depra <n.melnikov@depra.org>

using System;

namespace Depra.Sound
{
	public interface IAudioSourceParameter { }

	[Serializable]
	public sealed class GlobalAudioSourceParameter : IAudioSourceParameter { }

	[Serializable]
	public sealed class NullParameter : IAudioSourceParameter { }

	[Serializable]
	public sealed class EmptyParameter : IAudioSourceParameter { }

	[Serializable]
	public sealed class VolumeParameter : IAudioSourceParameter
	{
		public float Value;

		public VolumeParameter(float value) => Value = value;
	}

	[Serializable]
	public sealed class LoopParameter : IAudioSourceParameter
	{
		public bool Value;

		public LoopParameter(bool value) => Value = value;
	}

	[Serializable]
	public sealed class PanParameter : IAudioSourceParameter
	{
		public float Value;

		public PanParameter(float value) => Value = value;
	}

	[Serializable]
	public sealed class PitchParameter : IAudioSourceParameter
	{
		public float Value;

		public PitchParameter(float value) => Value = value;
	}
}