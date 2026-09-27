// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Depra <n.melnikov@depra.org>

using System;
using System.Runtime.CompilerServices;

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

		public void Stop() => _defaultSource.Stop();

		public bool Play(AudioEventId eventId) => Play(eventId, ReadOnlySpan<AudioParameter>.Empty, _defaultSource);

		public bool Play(AudioEventId eventId, IAudioSource source) =>
			Play(eventId, ReadOnlySpan<AudioParameter>.Empty, source);

		public bool Play(AudioEventId eventId, ReadOnlySpan<AudioParameter> parameters) =>
			Play(eventId, parameters, _defaultSource);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Play(AudioEventId eventId, ReadOnlySpan<AudioParameter> parameters, IAudioSource source)
		{
			if (!_table.TryResolve(eventId, out var description))
			{
				return false;
			}

			description.ApplyStaticParameters(source);
			for (int index = 0, count = parameters.Length; index < count; index++)
			{
				source.SetParameter(in parameters[index]);
			}

			source.Play(description.Clip);
			return true;
		}
	}
}
