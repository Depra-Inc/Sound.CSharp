// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Collections.Generic;
using Depra.Sound.Exceptions;

namespace Depra.Sound.Storage
{
	public sealed class AudioTypeContainer
	{
		private readonly Dictionary<Type, Type> _lookup = new();

		public Type Resolve(Type clipType) => _lookup.TryGetValue(clipType, out var sourceType)
			? sourceType
			: throw new AudioClipTypeNotDefined(clipType);

		public void Register(Type clipType, Type sourceType)
		{
			if (_lookup.TryAdd(clipType, sourceType) == false)
			{
				throw new AudioClipTypeAlreadyDefined(clipType);
			}
		}
	}
}