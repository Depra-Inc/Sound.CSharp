// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Collections.Generic;

namespace Depra.Sound.Parameter
{
	public interface IAudioClipParameters
	{
		IEnumerable<Type> SupportedTypes();

		IAudioClipParameter Get(Type type);

		void Set(IAudioClipParameter parameter);
	}
}