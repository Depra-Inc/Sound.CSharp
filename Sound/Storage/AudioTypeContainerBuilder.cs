// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using Depra.Sound.Clip;
using Depra.Sound.Source;

namespace Depra.Sound.Storage
{
	public sealed class AudioTypeContainerBuilder
	{
		private readonly AudioTypeContainer _container = new();

		public AudioTypeContainerBuilder Register<TClip, TSource>()
			where TClip : IAudioClip
			where TSource : IAudioSource
		{
			_container.Register(typeof(TClip), typeof(TSource));
			return this;
		}

		public AudioTypeContainer Build() => _container;
	}
}