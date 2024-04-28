// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Runtime.CompilerServices;

namespace Depra.Sound.Parameter
{
	public static class AudioClipParametersExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TParameter Get<TParameter>(this IAudioClipParameters self)
			where TParameter : IAudioClipParameter =>
			(TParameter) self.Get(typeof(TParameter));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Set(this IAudioClipParameters self, params IAudioClipParameter[] parameters)
		{
			foreach (var parameter in parameters)
			{
				self.Set(parameter);
			}
		}
	}
}