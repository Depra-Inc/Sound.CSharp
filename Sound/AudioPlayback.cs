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
		public bool Play(AudioEventId eventId) =>
			Play(eventId, ReadOnlySpan<AudioParam>.Empty, _defaultSource);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Play(AudioEventId eventId, IAudioSource source) =>
			Play(eventId, ReadOnlySpan<AudioParam>.Empty, source);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Play(AudioEventId eventId, ReadOnlySpan<AudioParam> parameters) =>
			Play(eventId, parameters, _defaultSource);

		public bool Play(AudioEventId eventId, ReadOnlySpan<AudioParam> parameters, IAudioSource source)
		{
			if (!_library.TryResolve(eventId, out var description))
			{
				return false;
			}

			var defaultParams = description.Contract.GetDefaultParameters();
			var optionalParams = description.Contract.Apply(parameters);

			source ??= _defaultSource;
			source.Play(description.Clip, defaultParams, optionalParams);

			return true;
		}
	}

	public static class AudioPlaybackExtensions
	{
		public static bool Play(this IAudioPlayback playback, AudioEventId eventId,
			params AudioParam[] parameters) =>
			playback.Play(eventId, parameters.AsSpan());

		public static bool Play(this IAudioPlayback playback, AudioEventId eventId, IAudioSource source,
			params AudioParam[] parameters) =>
			playback.Play(eventId, parameters.AsSpan(), source);
	}

	internal sealed class AudioEventContractException : Exception
	{
		public AudioEventContractException(AudioEventId eventId, string error) : base(
			$"Invalid parameters for audio event {eventId.Value}: {error}") { }
	}
}