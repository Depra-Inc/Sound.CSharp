// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Depra <n.melnikov@depra.org>

using System;

namespace Depra.Sound
{
	public interface IAudioSource<out TClip> : IAudioSource where TClip : IAudioClip
	{
		new TClip Current { get; }
	}

	public interface IAudioSource
	{
		event Action Started;
		event Action<AudioStopReason> Stopped;

		bool IsPlaying { get; }
		IAudioClip Current { get; }

		void Stop();
		void Play(IAudioClip clip);
		void SetParameter(in AudioParameter parameter);
	}

	public enum AudioStopReason
	{
		ERROR,
		STOPPED,
		FINISHED,
	}

}