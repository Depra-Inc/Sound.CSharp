// SPDX-License-Identifier: Apache-2.0
// © 2024-2026 Depra <n.melnikov@depra.org>

using System;

namespace Depra.Sound
{
	/// <summary>
	/// Runtime contract for events that can be configured with parameters.
	/// </summary>
	public interface IAudioEventDescription
	{
		IAudioClip Clip { get; }
		ReadOnlySpan<AudioParam> Overlay(ReadOnlySpan<AudioParam> parameters);
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