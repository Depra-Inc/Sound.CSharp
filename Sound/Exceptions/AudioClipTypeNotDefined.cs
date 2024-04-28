// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Reflection;

namespace Depra.Sound.Exceptions
{
	internal sealed class AudioClipTypeNotDefined : Exception
	{
		public AudioClipTypeNotDefined(MemberInfo clipType) : base($"Clip type {clipType.Name} not defined!") { }
	}
}