// SPDX-License-Identifier: Apache-2.0
// © 2024-2026 Depra <n.melnikov@depra.org>

namespace Depra.Sound
{
	public interface IAudioEventDescription
	{
		IAudioClip Clip { get; }
		IAudioEventContract Contract { get; }
	}

	/// <summary>
	/// Optional runtime contract for descriptions that can expand into multiple clips in a single play request.
	/// </summary>
	public interface IAudioEventBatchDescription
	{
		int EventCount { get; }
		IAudioEventDescription GetEvent(int index);
	}
}