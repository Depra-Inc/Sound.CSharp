// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using Depra.Sound.Configuration;

namespace Depra.Sound.Storage
{
	public readonly struct AudioSourceDescriptor
	{
		public readonly Type Type;
		public readonly SoundId Id;
		public readonly AudioSourceLifetime Lifetime;

		public AudioSourceDescriptor(SoundId id, Type type, AudioSourceLifetime lifetime = AudioSourceLifetime.TRANSIENT)
		{
			Id = id;
			Type = type;
			Lifetime = lifetime;
		}
	}
}