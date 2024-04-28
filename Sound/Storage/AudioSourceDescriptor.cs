// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Sound.Storage
{
	public readonly struct AudioSourceDescriptor
	{
		public readonly Type Type;
		public readonly AudioSourceLifetime Lifetime;

		public AudioSourceDescriptor(Type type, AudioSourceLifetime lifetime = AudioSourceLifetime.TRANSIENT)
		{
			Type = type;
			Lifetime = lifetime;
		}
	}
}