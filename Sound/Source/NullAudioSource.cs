// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using Depra.Sound.Clip;
using Depra.Sound.Parameter;

namespace Depra.Sound.Source
{
	public sealed class NullAudioSource : IAudioSource
	{
		public event IAudioSource.PlayDelegate Started;
		public event IAudioSource.StopDelegate Stopped;

		bool IAudioSource.IsPlaying => throw new NullAudioSourceException();

		void IAudioSource.Play(IAudioClip clip) => throw new NullAudioSourceException();
		void IAudioSource.Stop() => throw new NullAudioSourceException();

		public IAudioClipParameter GetParameter(Type type) => throw new NullAudioSourceException();
		public void SetParameter(IAudioClipParameter parameter) => throw new NullAudioSourceException();

		internal sealed class NullAudioSourceException : Exception
		{
			public NullAudioSourceException() : base("Null audio source!") { }
		}
	}
}