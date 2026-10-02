// SPDX-License-Identifier: Apache-2.0
// © 2024-2026 Depra <n.melnikov@depra.org>

using System;
using System.Runtime.CompilerServices;

namespace Depra.Sound
{
	public sealed class AudioPlayback : IAudioPlayback
	{
		private readonly IAudioLibrary _library;
		private readonly IAudioSource _defaultSource;

		public AudioPlayback(IAudioLibrary library, IAudioSource defaultSource)
		{
			_library = library;
			_defaultSource = defaultSource;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Stop() => _defaultSource.Stop();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public PlayHandle Play(AudioEventId eventId) =>
			Play(eventId, ReadOnlySpan<AudioParam>.Empty, _defaultSource);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public PlayHandle Play(AudioEventId eventId, IAudioSource source) =>
			Play(eventId, ReadOnlySpan<AudioParam>.Empty, source);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public PlayHandle Play(AudioEventId eventId, ReadOnlySpan<AudioParam> parameters) =>
			Play(eventId, parameters, _defaultSource);

		public PlayHandle Play(AudioEventId eventId, ReadOnlySpan<AudioParam> parameters, IAudioSource source)
		{
			if (!_library.TryResolve(eventId, out var description))
			{
				return new PlayHandle(eventId, false);
			}

			var baseSource = source ?? _defaultSource;
			if (description is IAudioEventBatchDescription batch)
			{
				for (int index = 0, count = batch.EventCount; index < count; index++)
				{
					var nested = batch.GetEvent(index);
					if (nested?.Clip != null)
					{
						baseSource?.Play(nested.Clip,
							nested.Contract.GetDefaultParameters(),
							nested.Contract.Apply(parameters));
					}
				}

				return new PlayHandle(eventId, true);
			}

			var defaultParams = description.Contract.GetDefaultParameters();
			var optionalParams = description.Contract.Apply(parameters);
			baseSource.Play(description.Clip, defaultParams, optionalParams);

			return new PlayHandle(eventId, true);
		}
	}

	public static class AudioPlaybackExtensions
	{
		public static PlayHandle Play(this IAudioPlayback playback, AudioEventId eventId,
			params AudioParam[] parameters) =>
			playback.Play(eventId, parameters.AsSpan());

		public static PlayHandle Play(this IAudioPlayback playback, AudioEventId eventId, IAudioSource source,
			params AudioParam[] parameters) =>
			playback.Play(eventId, parameters.AsSpan(), source);
	}
}