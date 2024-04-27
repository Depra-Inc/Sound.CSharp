// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using Depra.Sound.Clip;

namespace Depra.Sound.Source
{
	public sealed class NullAudioSource : IAudioSource
	{
		float IAudioSource.Volume
		{
			get => throw new NullAudioSourceException();
			set => throw new NullAudioSourceException();
		}

		bool IAudioSource.IsPlaying => throw new NullAudioSourceException();

		void IAudioSource.Play(IAudioClip clip) => throw new NullAudioSourceException();

		void IAudioSource.Stop() => throw new NullAudioSourceException();

		internal sealed class NullAudioSourceException : Exception
		{
			public NullAudioSourceException() : base("Null audio source!") { }
		}
	}
}