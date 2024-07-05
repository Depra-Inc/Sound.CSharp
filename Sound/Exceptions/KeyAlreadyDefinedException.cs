// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Sound.Exceptions
{
	internal sealed class KeyAlreadyDefinedException : Exception
	{
		public KeyAlreadyDefinedException(object key) : base($"Key {key} already defined!") { }
	}
}