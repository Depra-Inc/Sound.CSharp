using System;
using System.Reflection;

namespace Depra.Sound.Exceptions
{
	public sealed class AudioClipTypeNotDefined : Exception
	{
		public AudioClipTypeNotDefined(MemberInfo clipType) : base($"Clip type {clipType.Name} not defined!") { }
	}
}