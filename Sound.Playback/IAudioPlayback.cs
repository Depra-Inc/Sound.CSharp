// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Depra <n.melnikov@depra.org>

namespace Depra.Sound.Playback
{
	public interface IAudioPlayback
	{
		event AudioPlaybackStarted Started;
		event AudioPlaybackStopped Stopped;

		void Stop(IAudioTrack track);

		void Play(TrackId id);
		void Play(IAudioTrack track);
		void Play(IAudioTrack track, IAudioSource source);
	}

	public delegate void AudioPlaybackStarted(IAudioTrack track);
	public delegate void AudioPlaybackStopped(IAudioTrack track, AudioStopReason reason);
}