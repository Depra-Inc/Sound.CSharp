// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Depra <n.melnikov@depra.org>

namespace Depra.Sound.Playback
{
	public sealed class AudioPlayback : IAudioPlayback
	{
		private readonly IAudioTable _table;
		private readonly IAudioSource _defaultSource;

		public event AudioPlaybackStarted Started;
		public event AudioPlaybackStopped Stopped;

		public AudioPlayback(IAudioTable table, IAudioSource defaultSource)
		{
			_table = table;
			_defaultSource = defaultSource;
		}

		public void Play(TrackId id) => Play(_table.Get(id));

		public void Play(IAudioTrack track) => Play(track, _defaultSource);

		public void Play(IAudioTrack track, IAudioSource source)
		{
			source.Started += OnStart;
			source.Play(track);
			source.Stopped += OnStop;

			void OnStart()
			{
				source.Started -= OnStart;
				Started?.Invoke(track);
			}

			void OnStop(AudioStopReason reason)
			{
				source.Stopped -= OnStop;
				Stopped?.Invoke(track, reason);
			}
		}

		public void Stop(IAudioTrack track)
		{
			Stopped?.Invoke(track, AudioStopReason.STOPPED);
		}
	}
}