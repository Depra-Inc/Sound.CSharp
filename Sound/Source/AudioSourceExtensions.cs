// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Runtime.CompilerServices;
using Depra.Sound.Clip;
using Depra.Sound.Parameter;

namespace Depra.Sound.Source
{
	public static class AudioSourceExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Play(this IAudioSource self, IAudioClip clip, params IAudioClipParameter[] parameters)
		{
			self.Play(clip);
			self.SetParameters(parameters);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TParameter GetParameter<TParameter>(this IAudioSource self)
			where TParameter : IAudioClipParameter =>
			self.Parameters.Get<TParameter>();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetParameter<TParameter>(this IAudioSource self, TParameter parameter)
			where TParameter : IAudioClipParameter =>
			self.Parameters.Set(parameter);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetParameters(this IAudioSource self, params IAudioClipParameter[] parameters) =>
			self.Parameters.Set(parameters);
	}
}