// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Sound.Clip
{
	public sealed class NullAudioClip : IAudioClip
	{
		AudioClipMetadata IAudioClip.Metadata() => throw new NullAudioClipException();

		private sealed class NullAudioClipException : Exception
		{
			public NullAudioClipException() : base("Null audio clip") { }
		}
	}
}