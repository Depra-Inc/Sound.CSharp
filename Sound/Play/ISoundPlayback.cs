// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using Depra.Sound.Configuration;

namespace Depra.Sound.Play
{
	public interface ISoundPlayback
	{
		void Play(SoundId id);

		void Stop(SoundId id);
	}
}