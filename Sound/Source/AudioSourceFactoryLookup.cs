// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Runtime.CompilerServices;
using Depra.Sound.Storage;

namespace Depra.Sound.Source
{
	public sealed class AudioSourceFactoryLookup : AudioLookup<Type, IAudioSourceFactory>
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public AudioSourceFactoryLookup Register<TSource>(IAudioSourceFactory factory)
			where TSource : IAudioSource
		{
			Register(typeof(TSource), factory);
			return this;
		}
	}
}