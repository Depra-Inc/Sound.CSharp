// SPDX-License-Identifier: Apache-2.0
// © 2024-2026 Depra <n.melnikov@depra.org>

using System;

namespace Depra.Sound
{
	public interface IAudioEventDescription
	{
		IAudioClip Clip { get; }
		IAudioEventContract Contract { get; }
		ReadOnlySpan<AudioParam> StaticParameters { get; }
	}

	public interface IAudioEventContract
	{
		/// <summary>
		/// Validates dynamic parameters against this event's parameter contract.
		/// </summary>
		bool Validate(ReadOnlySpan<AudioParam> parameters, out string error);
	}
}