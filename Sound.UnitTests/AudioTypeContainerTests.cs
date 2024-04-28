// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using Depra.Sound.Clip;
using Depra.Sound.Exceptions;
using Depra.Sound.Source;
using Depra.Sound.Storage;
using FluentAssertions;

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
		resolvedSourceType.Should().Be(sourceType);
	}

	[Fact]
	public void Register_WhenCalledWithSameClipType_ShouldThrowAlreadyDefinedException()
	{
		// Arrange:
		var clipType = typeof(IAudioClip);
		var sourceType = typeof(IAudioSource);
		_container.Register(clipType, sourceType);

		// Act:
		var act = () => _container.Register(clipType, sourceType);

		// Assert:
		act.Should().Throw<AudioClipTypeAlreadyDefined>();
	}

	[Fact]
	public void TryResolve_WhenCalledWithUnregisteredClipType_ShouldThrowNotDefinedException()
	{
		// Arrange:
		var clipType = typeof(IAudioClip);

		// Act:
		var act = () => _container.Resolve(clipType);

		// Assert:
		act.Should().Throw<AudioClipTypeNotDefined>();
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
		resolvedSourceType.Should().Be(sourceType);
	}
}