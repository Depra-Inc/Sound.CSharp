// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Sound.Clip
{
	public interface IAudioClipContainer
	{
		IAudioClip Next();
	}
}