// SPDX-License-Identifier: Apache-2.0
// © 2024-2026 Depra <n.melnikov@depra.org>

using System;

namespace Depra.Sound
{
	public struct AudioParamSet
	{
		private AudioParam _p0;
		private AudioParam _p1;
		private AudioParam _p2;
		private AudioParam _p3;
		private AudioParam _p4;
		private AudioParam _p5;
		private AudioParam _p6;
		private AudioParam _p7;

		public byte Count { get; private set; }

		public bool TryAdd(AudioParam parameter)
		{
			switch (Count)
			{
				case 0: _p0 = parameter; break;
				case 1: _p1 = parameter; break;
				case 2: _p2 = parameter; break;
				case 3: _p3 = parameter; break;
				case 4: _p4 = parameter; break;
				case 5: _p5 = parameter; break;
				case 6: _p6 = parameter; break;
				case 7: _p7 = parameter; break;
				default: return false;
			}

			Count++;
			return true;
		}

		public readonly void CopyTo(AudioParam[] destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException(nameof(destination));
			}

			if (destination.Length < Count)
			{
				throw new ArgumentException("Destination array is smaller than AudioParamSet size.",
					nameof(destination));
			}

			switch (Count)
			{
				case 8:
					destination[7] = _p7;
					goto case 7;
				case 7:
					destination[6] = _p6;
					goto case 6;
				case 6:
					destination[5] = _p5;
					goto case 5;
				case 5:
					destination[4] = _p4;
					goto case 4;
				case 4:
					destination[3] = _p3;
					goto case 3;
				case 3:
					destination[2] = _p2;
					goto case 2;
				case 2:
					destination[1] = _p1;
					goto case 1;
				case 1:
					destination[0] = _p0;
					break;
			}
		}
	}
}