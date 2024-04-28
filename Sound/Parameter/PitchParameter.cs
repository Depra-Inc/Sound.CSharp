// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Sound.Parameter
{
	[Serializable]
	public struct PitchParameter : IAudioClipParameter
	{
		public float Value;

		public PitchParameter(float value) => Value = value;
	}
}