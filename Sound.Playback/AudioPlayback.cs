// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Depra <n.melnikov@depra.org>

using System.Buffers;
using System.Runtime.CompilerServices;

namespace Depra.Sound.Playback
{
	public sealed class AudioPlayback : IAudioPlayback
	{
		private readonly IAudioSource _defaultSource;

		public AudioPlayback(IAudioSource defaultSource) => _defaultSource = defaultSource;

		public void Stop() => _defaultSource.Stop();

		public void Play(IAudioTrack track, ParameterConverter converter = null) =>
			Play(track, _defaultSource, converter);

		public void Play(IAudioTrack track, IAudioSource source, ParameterConverter converter = null)
		{
			var sourceSegments = track.Request();
			var segments = converter == null ? sourceSegments : ConvertSegments(sourceSegments, converter);

			Play(source ?? _defaultSource, segments);
			track.Release(sourceSegments);
			if (converter != null)
			{
				ReleaseSegments(segments);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Play(IAudioSource source, AudioTrackSegment[] segments)
		{
			foreach (var segment in segments)
			{
				source.Play(segment.Clip, segment.Parameters);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private AudioTrackSegment[] ConvertSegments(AudioTrackSegment[] sourceSegments, ParameterConverter converter)
		{
			var convertedSegments = ArrayPool<AudioTrackSegment>.Shared.Rent(sourceSegments.Length);
			for (var i = 0; i < sourceSegments.Length; i++)
			{
				var segment = sourceSegments[i];
				var parameters = ArrayPool<IAudioSourceParameter>.Shared.Rent(segment.Parameters.Length);
				for (var j = 0; j < parameters.Length; j++)
				{
					parameters[j] = converter(segment.Parameters[j]);
				}

				convertedSegments[i] = new AudioTrackSegment(segment.Clip, parameters);
			}

			return convertedSegments;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ReleaseSegments(AudioTrackSegment[] segments)
		{
			foreach (var segment in segments)
			{
				ArrayPool<IAudioSourceParameter>.Shared.Return(segment.Parameters);
			}

			ArrayPool<AudioTrackSegment>.Shared.Return(segments);
		}
	}
}