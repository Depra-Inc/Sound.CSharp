// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Depra <n.melnikov@depra.org>

using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ListPool;

namespace Depra.Sound.Playback
{
	public sealed class AudioPlayback : IAudioPlayback
	{
		private static ArrayPool<IAudioSourceParameter> ParameterPool => ArrayPool<IAudioSourceParameter>.Shared;
		
		private readonly IAudioSource _defaultSource;

		public AudioPlayback(IAudioSource defaultSource) => _defaultSource = defaultSource;

		public void Stop() => _defaultSource.Stop();

		public void Play(IAudioTrack track, ParameterConverter converter = null) => Play(track, _defaultSource, converter);

		public void Play(IAudioTrack track, IAudioSource source, ParameterConverter converter = null)
		{
			using var sourceSegments = new ListPool<AudioTrackSegment>();
			using var convertedSegments = new ListPool<AudioTrackSegment>();
			track.ExtractSegments(sourceSegments);

			if (converter != null)
			{
				ConvertSegments(sourceSegments, convertedSegments, converter);
			}
			else
			{
				convertedSegments.AddRange(sourceSegments);
			}

			Play(source ?? _defaultSource, convertedSegments);

			if (converter != null)
			{
				ReleaseSegments(convertedSegments);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Play(IAudioSource source, IList<AudioTrackSegment> segments)
		{
			foreach (var segment in segments)
			{
				if (segment.IsValid())
				{
					source.Play(segment.Clip, segment.Parameters);
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void ConvertSegments(IList<AudioTrackSegment> source,
			IList<AudioTrackSegment> result, ParameterConverter converter)
		{
			foreach (var segment in source)
			{
				if (!segment.IsValid())
				{
					continue;
				}

				var sourceParams = segment.Parameters;
				var convertedParams = ParameterPool.Rent(sourceParams.Length);
				for (var j = 0; j < sourceParams.Length; j++)
				{
					convertedParams[j] = converter(sourceParams[j]);
				}

				result.Add(new AudioTrackSegment(segment.Clip, convertedParams));
			}
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void ReleaseSegments(IList<AudioTrackSegment> segments)
		{
			foreach (var segment in segments)
			{
				if (segment.Parameters != null)
				{
					ParameterPool.Return(segment.Parameters);
				}
			}
		}
	}
}