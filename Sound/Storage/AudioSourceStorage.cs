// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using Depra.Sound.Source;

namespace Depra.Sound.Storage
{
	public sealed class AudioSourceStorage
	{
		private readonly IAudioSourceFactory _factory;

		public AudioSourceStorage(IAudioSourceFactory factory) => _factory = factory;

		public IAudioSource Request(AudioSourceDescriptor descriptor)
		{
			var source = _factory.Create(descriptor.Type);
			return source;
		}
	}
}