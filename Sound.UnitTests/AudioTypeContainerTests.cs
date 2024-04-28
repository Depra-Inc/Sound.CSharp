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
	public void Resolve_WhenCalledWithNotRegisteredClipType_ShouldThrowNotDefinedException()
	{
		// Arrange:
		var clipType = typeof(IAudioClip);

		// Act:
		var act = () => _container.Resolve(clipType);

		// Assert:
		act.Should().Throw<AudioClipTypeNotDefined>();
	}

	[Fact]
	public void Resolve_WhenCalledWithRegisteredClipType_ShouldReturnSourceType()
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

	[Fact]
	public void Resolve_WhenRegisteredMultipleClipTypes_ShouldReturnCorrectSourceTypes()
	{
		// Arrange:
		var clipType1 = typeof(AudioClipA);
		var sourceType1 = typeof(IAudioSource);
		_container.Register(clipType1, sourceType1);

		var clipType2 = typeof(AudioClipB);
		var sourceType2 = typeof(IAudioSource);
		_container.Register(clipType2, sourceType2);

		// Act:
		var resolvedSourceType1 = _container.Resolve(clipType1);
		var resolvedSourceType2 = _container.Resolve(clipType2);

		// Assert:
		resolvedSourceType1.Should().Be(sourceType1);
		resolvedSourceType2.Should().Be(sourceType2);
	}

	private sealed class AudioClipA : IAudioClip
	{
		string IAudioClip.Name => nameof(AudioClipA);
		float IAudioClip.Duration => 0;
	}

	private sealed class AudioClipB : IAudioClip
	{
		string IAudioClip.Name => nameof(AudioClipB);
		float IAudioClip.Duration => 0;
	}
}