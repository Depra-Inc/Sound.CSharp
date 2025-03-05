// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Depra <n.melnikov@depra.org>

using System;

namespace Depra.Sound
{
	public readonly struct AudioTrackSegment
	{
		public readonly IAudioClip Clip;
		public readonly IAudioSourceParameter[] Parameters;

		public AudioTrackSegment(IAudioClip clip, IAudioSourceParameter[] parameters)
		{
			Clip = clip;
			Parameters = parameters;
		}
	}

	public static class AudioTrackExtensions
	{
		public static IAudioTrack ModifyParameters(this IAudioTrack self,
			Func<IAudioSourceParameter, IAudioSourceParameter> modifier)
		{
			var sourceSegments = self.Segments();
			var segments = new AudioTrackSegment[sourceSegments.Length];
			for (var i = 0; i < sourceSegments.Length; i++)
			{
				var segment = sourceSegments[i];
				var parameters = new IAudioSourceParameter[segment.Parameters.Length];
				for (var j = 0; j < parameters.Length; j++)
				{
					parameters[j] = modifier(segment.Parameters[j]);
				}

				segments[i] = new AudioTrackSegment(segment.Clip, parameters);
			}

			return new SegmentedAudioTrack(segments);
		}

		private readonly struct SegmentedAudioTrack : IAudioTrack
		{
			private readonly AudioTrackSegment[] _segments;
			public SegmentedAudioTrack(params AudioTrackSegment[] segments) => _segments = segments;

			AudioTrackSegment[] IAudioTrack.Segments() => _segments;
		}
	}
}