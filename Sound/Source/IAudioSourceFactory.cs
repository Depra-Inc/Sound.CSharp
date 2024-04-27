// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Sound.Source
{
	public interface IAudioSourceFactory
	{
		IAudioSource Create(Type type);

		void Destroy(IAudioSource source);
	}
}