// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using Depra.Sound.Source;
using FluentAssertions;

namespace Depra.Sound.UnitTests;

public sealed class NullAudioSourceTests
{
	private readonly IAudioSource _source = new NullAudioSource();

	[Fact]
	public void IsPlaying_ThrowsException()
	{
		// Arrange, Act:
		var act = () => _source.IsPlaying;

		// Assert:
		act.Should().Throw<NullAudioSource.NullAudioSourceException>();
	}

	[Fact]
	public void Play_ThrowsException()
	{
		// Arrange, Act:
		var act = () => _source.Play(null!);

		// Assert:
		act.Should().Throw<NullAudioSource.NullAudioSourceException>();
	}

	[Fact]
	public void Stop_ThrowsException()
	{
		// Arrange, Act:
		var act = () => _source.Stop();

		// Assert:
		act.Should().Throw<NullAudioSource.NullAudioSourceException>();
	}
}