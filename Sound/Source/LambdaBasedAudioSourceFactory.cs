// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using Depra.Sound.Exceptions;

namespace Depra.Sound.Source
{
	public sealed class LambdaBasedAudioSourceFactory : IAudioSourceFactory
	{
		private readonly Func<Type, IAudioSource> _factory;

		public LambdaBasedAudioSourceFactory(Func<Type, IAudioSource> factory)
		{
			Guard.AgainstNull(factory, nameof(factory));
			_factory = factory ?? throw new ArgumentNullException(nameof(factory));
		}

		IAudioSource IAudioSourceFactory.Create(Type type) => _factory(type);

		void IAudioSourceFactory.Destroy(IAudioSource source)
		{
			if (source is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}
	}
}