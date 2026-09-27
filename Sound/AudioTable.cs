// SPDX-License-Identifier: Apache-2.0
// © 2024-2026 Depra <n.melnikov@depra.org>

using System.Collections.Generic;

namespace Depra.Sound
{
	public interface IAudioTable
	{
		bool TryResolve(AudioEventId eventId, out IAudioEventDescription description);
	}

	public sealed class AudioTable : IAudioTable
	{
		private readonly IList<IAudioBank> _banks;

		public AudioTable(IList<IAudioBank> banks) => _banks = banks;

		bool IAudioTable.TryResolve(AudioEventId eventId, out IAudioEventDescription description)
		{
			for (int index = 0, count = _banks.Count; index < count; index++)
			{
				if (_banks[index].TryGet(eventId, out description))
				{
					return true;
				}
			}

			description = null;
			return false;
		}
	}
}