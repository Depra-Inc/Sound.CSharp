using System.Runtime.CompilerServices;

namespace Depra.Sound
{
	public readonly ref struct PlayHandle
	{
		public readonly bool Result;
		public readonly AudioEventId EventId;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public PlayHandle(AudioEventId id, bool result)
		{
			EventId = id;
			Result = result;
		}
	}
}