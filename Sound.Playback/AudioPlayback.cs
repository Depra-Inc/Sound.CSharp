// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Depra <n.melnikov@depra.org>

namespace Depra.Sound.Playback
{
	public sealed class AudioPlayback : IAudioPlayback
	{
		private readonly IAudioTable _table;
		private readonly IAudioSource _defaultSource;

		public AudioPlayback(IAudioTable table, IAudioSource defaultSource)
		{
			_table = table;
			_defaultSource = defaultSource;
		}

		public void Play(TrackId id) => Play(_table.Get(id));

		public void Play(IAudioTrack track) => Play(track, _defaultSource);

		public void Play(IAudioTrack track, IAudioSource source)
		{
			if (source == null)
			{
				_defaultSource.Play(track);
			}
			else
			{
				source.Play(track);
			}
		}

		public void Stop(IAudioTrack track, IAudioSource source = null)
		{
			if (source == null)
			{
				_defaultSource.Stop();
			}
			else
			{
				source.Stop();
			}
		}
	}
}