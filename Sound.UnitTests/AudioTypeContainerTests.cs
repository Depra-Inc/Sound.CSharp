// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using Depra.Sound.Clip;
using Depra.Sound.Source;
using Depra.Sound.Storage;

namespace Depra.Sound.UnitTests;

public sealed class AudioTypeContainerTests
{
	private readonly AudioTypeContainer _container = new();

	[Fact]
	public void Register_WhenCalled_ShouldAddToLookup()
	{
		// Arrange:
		var clipType = typeof(IAudioClip);
		var sourceType = typeof(IAudioSource);

		// Act:
		_container.Register(clipType, sourceType);
		var resolvedSourceType = _container.Resolve(clipType);

		// Assert:
		Assert.Equal(sourceType, resolvedSourceType);
	}

	[Fact]
	public void Register_WhenCalledWithSameClipType_ShouldThrowArgumentException()
	{
		// Arrange:
		var clipType = typeof(IAudioClip);
		var sourceType = typeof(IAudioSource);

		// Act:
		_container.Register(clipType, sourceType);

		// Assert:
		Assert.Throws<ArgumentException>(() => _container.Register(clipType, sourceType));
	}

	[Fact]
	public void TryResolve_WhenCalledWithUnregisteredClipType_ShouldReturnFalse()
	{
		// Arrange:
		var clipType = typeof(IAudioClip);

		// Act:
		var act = () => _container.Resolve(clipType);

		// Assert:
		Assert.Throws<ArgumentException>(act);
	}

	[Fact]
	public void TryResolve_WhenCalledWithRegisteredClipType_ShouldReturnTrue()
	{
		// Arrange:
		var clipType = typeof(IAudioClip);
		var sourceType = typeof(IAudioSource);
		_container.Register(clipType, sourceType);

		// Act:
		var resolvedSourceType = _container.Resolve(clipType);

		// Assert:
		Assert.Equal(sourceType, resolvedSourceType);
	}
}