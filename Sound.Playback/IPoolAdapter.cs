// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Depra <n.melnikov@depra.org>

using System.Collections.Generic;

namespace Depra.Sound.Playback
{
	public interface IPoolAdapter
	{
		IList<T> Rent<T>(int count);

		void Return<T>(IList<T> items);
	}
}