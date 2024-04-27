using System;
using System.Reflection;

namespace Depra.Sound.Exceptions
{
	public sealed class AudioClipTypeAlreadyDefined : Exception
	{
		public AudioClipTypeAlreadyDefined(MemberInfo clipType) : base($"Clip type {clipType.Name} already defined!") { }
	}
}