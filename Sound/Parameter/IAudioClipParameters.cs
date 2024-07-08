// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Collections.Generic;

namespace Depra.Sound.Parameter
{
	public interface IAudioClipParameters
	{
		IEnumerable<Type> SupportedTypes();

		IAudioClipParameter GetOrDefault(Type type);

		IEnumerable<IAudioClipParameter> GetAll();

		void AddOrUpdate(IAudioClipParameter parameter);
	}
}