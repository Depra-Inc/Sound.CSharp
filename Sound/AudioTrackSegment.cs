// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Depra <n.melnikov@depra.org>

using System;

namespace Depra.Sound
{
	public readonly struct AudioTrackSegment
	{
		public readonly IAudioClip Clip;
		private readonly IAudioSourceParameter[] _parameters;

		public AudioTrackSegment(IAudioClip clip, IAudioSourceParameter[] parameters)
		{
			Clip = clip;
			_parameters = parameters;
		}

		public IAudioSourceParameter[] Parameters => _parameters ?? Array.Empty<IAudioSourceParameter>();

		public bool IsValid() => Clip != null;
	}
}