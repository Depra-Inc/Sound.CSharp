// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Runtime.CompilerServices;
using Depra.Sound.Clip;
using Depra.Sound.Source;

namespace Depra.Sound.Storage
{
	public sealed class AudioTypeLookup : AudioLookup<Type, Type>
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public AudioTypeLookup Register<TClip, TSource>()
			where TClip : IAudioClip
			where TSource : IAudioSource
		{
			Register(typeof(TClip), typeof(TSource));
			return this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Type Resolve<TClip>() => Resolve(typeof(TClip));
	}
}