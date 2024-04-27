// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using Depra.Sound.Clip;

namespace Depra.Sound.Configuration
{
	public interface IAudioClipLibrary
	{
		bool Contains(SoundId id);

		IAudioClip Get(SoundId id);

		IEnumerable<IAudioClip> GetAll();
	}
}