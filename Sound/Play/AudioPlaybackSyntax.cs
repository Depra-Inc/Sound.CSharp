// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Runtime.CompilerServices;
using Depra.Sound.Clip;
using Depra.Sound.Parameter;
using Depra.Sound.Source;

namespace Depra.Sound.Play
{
	public static class AudioPlaybackSyntax
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Play(this IAudioPlayback self, IAudioClipContainer container,
			params IAudioClipParameter[] args) =>
			self.Play(container, args);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Play(this IAudioPlayback self, IAudioClipContainer container, IAudioSource source,
			params IAudioClipParameter[] args) =>
			self.Play(container, source, args);
	}
}