// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Runtime.CompilerServices;

namespace Depra.Sound.Storage
{
	public static class AudioTypeContainerExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Type Resolve<TSource>(this AudioTypeContainer self) =>
			self.Resolve(typeof(TSource));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Register<TClip, TSource>(this AudioTypeContainer self) =>
			self.Register(typeof(TClip), typeof(TSource));
	}
}