// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Collections.Generic;
using Depra.Sound.Clip;
using Depra.Sound.Parameter;

namespace Depra.Sound.Source
{
	public sealed class NullAudioSource : IAudioSource
	{
		public event IAudioSource.PlayDelegate Started;
		public event IAudioSource.StopDelegate Stopped;

		bool IAudioSource.IsPlaying => throw new NullAudioSourceException();
		IAudioClipParameters IAudioSource.Parameters => new NullAudioClipParameters();

		void IAudioSource.Play(IAudioClip clip) => throw new NullAudioSourceException();
		void IAudioSource.Stop() => throw new NullAudioSourceException();

		internal sealed class NullAudioSourceException : Exception
		{
			public NullAudioSourceException() : base("Null audio source!") { }
		}

		private sealed class NullAudioClipParameters : IAudioClipParameters
		{
			public IEnumerable<Type> SupportedTypes() => throw new NullAudioSourceException();
			public IAudioClipParameter Get(Type type) => throw new NullAudioSourceException();
			public void Set(IAudioClipParameter parameter) => throw new NullAudioSourceException();
		}
	}
}