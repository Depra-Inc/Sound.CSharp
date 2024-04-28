// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Sound.Parameter
{
	[Serializable]
	public struct LoopParameter : IAudioClipParameter
	{
		public bool Value;

		public LoopParameter(bool value) => Value = value;
	}
}