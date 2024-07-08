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
		public event IAudioSource.PlayDelegate Started
		{
			add => throw new NullAudioSourceException();
			remove => throw new NullAudioSourceException();
		}

		public event IAudioSource.StopDelegate Stopped
		{
			add => throw new NullAudioSourceException();
			remove => throw new NullAudioSourceException();
		}

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
			IEnumerable<Type> IAudioClipParameters.SupportedTypes() => throw new NullAudioSourceException();
			IAudioClipParameter IAudioClipParameters.GetOrDefault(Type type) => throw new NullAudioSourceException();
			IEnumerable<IAudioClipParameter> IAudioClipParameters.GetAll() => throw new NullAudioSourceException();
			void IAudioClipParameters.AddOrUpdate(IAudioClipParameter parameter) => throw new NullAudioSourceException();
		}
	}
}