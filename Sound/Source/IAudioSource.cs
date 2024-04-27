// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using Depra.Sound.Clip;

namespace Depra.Sound.Source
{
	public interface IAudioSource
	{
		bool IsPlaying { get; }

		float Volume { get; set; }

		void Play(IAudioClip clip);

		void Stop();
	}
}