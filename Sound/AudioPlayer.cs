// SPDX-License-Identifier: Apache-2.0
// © 2024-2026 Depra <n.melnikov@depra.org>

using System;
using System.Runtime.CompilerServices;

namespace Depra.Sound
{
	public sealed class AudioPlayer : IAudioPlayer
	{
		private readonly IAudioLibrary _library;
		private readonly IAudioSource _defaultSource;

		public AudioPlayer(IAudioLibrary library, IAudioSource defaultSource)
		{
			_library = library ?? throw new ArgumentNullException(nameof(library));
			_defaultSource = defaultSource ?? throw new ArgumentNullException(nameof(defaultSource));
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
				PlayBatch(batch, baseSource, parameters);
				return new PlayHandle(eventId, true);
			}

			baseSource.Play(description.Clip, description.Contract.Merge(parameters));
			return new PlayHandle(eventId, true);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void PlayBatch(IAudioEventBatchDescription batch, IAudioSource source, ReadOnlySpan<AudioParam> parameters)
		{
			for (int index = 0, count = batch.EventCount; index < count; index++)
			{
				var nested = batch.GetEvent(index);
				if (nested?.Clip != null)
				{
					source.Play(nested.Clip, nested.Contract.Merge(parameters));
				}
			}
		}
	}

	public static class AudioPlaybackExtensions
	{
		public static PlayHandle Play(this IAudioPlayer player, AudioEventId eventId,
			params AudioParam[] parameters) =>
			player.Play(eventId, parameters.AsSpan());

		public static PlayHandle Play(this IAudioPlayer player, AudioEventId eventId, IAudioSource source,
			params AudioParam[] parameters) =>
			player.Play(eventId, parameters.AsSpan(), source);
	}
}