// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Depra.Sound.Parameter
{
	public static class AudioClipParametersSyntax
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TParameter GetOrDefault<TParameter>(this IAudioClipParameters self)
			where TParameter : IAudioClipParameter =>
			(TParameter) self.GetOrDefault(typeof(TParameter));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddOrUpdate(this IAudioClipParameters self, IEnumerable<IAudioClipParameter> parameters)
		{
			foreach (var parameter in parameters)
			{
				self.AddOrUpdate(parameter);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddOrUpdate(this IAudioClipParameters self, params IAudioClipParameter[] parameters)
		{
			foreach (var parameter in parameters)
			{
				self.AddOrUpdate(parameter);
			}
		}
	}
}