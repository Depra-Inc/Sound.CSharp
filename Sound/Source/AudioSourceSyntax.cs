// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Depra.Sound.Parameter;

namespace Depra.Sound.Source
{
	public static class AudioSourceSyntax
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TParameter GetParameter<TParameter>(this IAudioSource self)
			where TParameter : IAudioClipParameter =>
			self.Parameters.GetOrDefault<TParameter>();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddOrUpdate<TParameter>(this IAudioSource self, TParameter parameter)
			where TParameter : IAudioClipParameter =>
			self.Parameters.AddOrUpdate(parameter);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddOrUpdate(this IAudioSource self, IEnumerable<IAudioClipParameter> parameters) =>
			self.Parameters.AddOrUpdate(parameters);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddOrUpdate(this IAudioSource self, params IAudioClipParameter[] parameters) =>
			self.Parameters.AddOrUpdate(parameters);
	}
}