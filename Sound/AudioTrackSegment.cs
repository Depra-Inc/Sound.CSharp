// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Sound
{
	public struct AudioTrackSegment
	{
		public readonly IAudioClip Clip;
		public IAudioSourceParameter[] Parameters;

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
			var sourceSegments = self.Deconstruct();
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

			void IAudioTrack.Play(IAudioSource source)
			{
				foreach (var segment in _segments)
				{
					source.Play(segment.Clip, segment.Parameters);
				}
			}

			AudioTrackSegment[] IAudioTrack.Deconstruct() => _segments;
		}
	}
}