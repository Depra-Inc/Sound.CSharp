// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Sound.Parameter
{
	[Serializable]
	public struct PanParameter : IAudioClipParameter
	{
		public float Value;

		public PanParameter(float value) => Value = value;
	}
}