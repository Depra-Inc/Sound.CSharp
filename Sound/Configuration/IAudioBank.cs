// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using Depra.Sound.Clip;
using Depra.Sound.Parameter;

namespace Depra.Sound.Configuration
{
	public interface IAudioBank
	{
		bool Contains(SoundId id);

		IAudioClip GetClip(SoundId id);

		IEnumerable<IAudioClip> GetAllClips();

		IAudioClipParameter[] GetParameters(SoundId id);
	}
}