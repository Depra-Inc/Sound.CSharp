// SPDX-License-Identifier: Apache-2.0
// © 2024-2026 Depra <n.melnikov@depra.org>

using System;

namespace Depra.Sound
{
	public interface IAudioSource
	{
		event Action Started;
		event Action<AudioStopReason> Stopped;

		bool IsPlaying { get; }
		IAudioClip Current { get; }

		void Play(IAudioClip clip);

		void Play(IAudioClip clip, ReadOnlySpan<AudioParam> parameters);

		void Stop();
	}

	public enum AudioStopReason
	{
		ERROR,
		STOPPED,
		FINISHED,
	}
}