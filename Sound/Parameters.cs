// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Sound
{
	public interface IAudioClipParameter { }

	[Serializable]
	public readonly struct NullParameter : IAudioClipParameter { }

	[Serializable]
	public readonly struct EmptyParameter : IAudioClipParameter { }

	[Serializable]
	public struct VolumeParameter : IAudioClipParameter
	{
		public float Value;

		public VolumeParameter(float value) => Value = value;
	}

	[Serializable]
	public struct LoopParameter : IAudioClipParameter
	{
		public bool Value;

		public LoopParameter(bool value) => Value = value;
	}

	[Serializable]
	public struct PanParameter : IAudioClipParameter
	{
		public float Value;

		public PanParameter(float value) => Value = value;
	}

	[Serializable]
	public struct PitchParameter : IAudioClipParameter
	{
		public float Value;

		public PitchParameter(float value) => Value = value;
	}
}