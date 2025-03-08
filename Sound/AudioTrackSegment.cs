// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Depra <n.melnikov@depra.org>

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Depra.Sound
{
	public readonly struct AudioTrackSegment
	{
		public readonly IAudioClip Clip;
		private readonly IList<IAudioSourceParameter> _parameters;

		public AudioTrackSegment(IAudioClip clip, IList<IAudioSourceParameter> parameters)
		{
			Clip = clip;
			_parameters = parameters;
		}

		public IList<IAudioSourceParameter> Parameters => _parameters ?? Array.Empty<IAudioSourceParameter>();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsValid() => Clip != null;
	}
}