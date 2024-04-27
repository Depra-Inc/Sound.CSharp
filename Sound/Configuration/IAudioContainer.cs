// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using Depra.Sound.Clip;

namespace Depra.Sound.Configuration
{
	public interface IAudioContainer
	{
		IAudioClip Next();
	}
}