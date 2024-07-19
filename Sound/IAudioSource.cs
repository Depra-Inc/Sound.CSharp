// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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

		void Stop();
		void Play(IAudioTrack track);
		void Play(IAudioClip clip, IEnumerable<IAudioSourceParameter> parameters);

		IAudioSourceParameter Read(Type parameterType);
		IEnumerable<IAudioSourceParameter> EnumerateParameters();
	}

	public enum AudioStopReason
	{
		ERROR,
		STOPPED,
		FINISHED,
	}

	public static class AudioSourceExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TParameter Read<TParameter>(this IAudioSource self)
			where TParameter : IAudioSourceParameter =>
			(TParameter) self.Read(typeof(TParameter));
	}
}