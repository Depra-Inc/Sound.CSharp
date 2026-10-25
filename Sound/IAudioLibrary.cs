// SPDX-License-Identifier: Apache-2.0
// © 2024-2026 Depra <n.melnikov@depra.org>

namespace Depra.Sound
{
	public interface IAudioLibrary
	{
		bool TryResolve(AudioEventId eventId, out IAudioEventDescription description);
	}
}