// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Collections.Generic;

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
		IEnumerable<Type> SupportedTracks { get; }

		void Play(IAudioTrack track);
		void Stop();

		IAudioClipParameter Read(Type parameterType);
		TParameter Read<TParameter>() where TParameter : IAudioClipParameter;

		IEnumerable<IAudioClipParameter> EnumerateParameters();
	}

	public enum AudioStopReason
	{
		ERROR,
		STOPPED,
		FINISHED,
	}
}