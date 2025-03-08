// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Depra <n.melnikov@depra.org>

using System.Collections.Generic;
using System.Runtime.CompilerServices;

// ReSharper disable ForCanBeConvertedToForeach
namespace Depra.Sound.Playback
{
	public sealed class AudioPlayback : IAudioPlayback
	{
		private readonly IPoolAdapter _pool;
		private readonly IAudioSource _defaultSource;

		public AudioPlayback(IPoolAdapter pool, IAudioSource defaultSource)
		{
			_pool = pool;
			_defaultSource = defaultSource;
		}

		public void Stop() => _defaultSource.Stop();

		public void Play(IAudioTrack track, ParameterConverter converter = null) =>
			Play(track, _defaultSource, converter);

		public void Play(IAudioTrack track, IAudioSource source, ParameterConverter converter = null)
		{
			var sourceSegments = _pool.Rent<AudioTrackSegment>(8);
			var convertedSegments = _pool.Rent<AudioTrackSegment>(8);
			track.ExtractSegments(sourceSegments);

			if (converter == null)
			{
				for (var index = 0; index < sourceSegments.Count; index++)
				{
					convertedSegments.Add(sourceSegments[index]);
				}
			}
			else
			{
				ConvertSegments(sourceSegments, convertedSegments, converter);
			}

			Play(source ?? _defaultSource, convertedSegments);

			_pool.Return(sourceSegments);
			_pool.Return(convertedSegments);

			for (var index = 0; index < convertedSegments.Count; index++)
			{
				var segment = convertedSegments[index];
				if (segment.Parameters != null)
				{
					_pool.Return(segment.Parameters);
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Play(IAudioSource source, IList<AudioTrackSegment> segments)
		{
			for (var index = 0; index < segments.Count; index++)
			{
				var segment = segments[index];
				if (segment.IsValid())
				{
					source.Play(segment.Clip, segment.Parameters);
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ConvertSegments(IList<AudioTrackSegment> source, IList<AudioTrackSegment> converted,
			ParameterConverter converter)
		{
			for (var i = 0; i < source.Count; i++)
			{
				var segment = source[i];
				if (!segment.IsValid())
				{
					continue;
				}

				var sourceParams = segment.Parameters;
				var convertedParams = _pool.Rent<IAudioSourceParameter>(sourceParams.Count);
				for (var j = 0; j < sourceParams.Count; j++)
				{
					convertedParams.Add(converter(sourceParams[j]));
				}

				converted.Add(new AudioTrackSegment(segment.Clip, convertedParams));
			}
		}
	}
}