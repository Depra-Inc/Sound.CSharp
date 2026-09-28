using FluentAssertions;

namespace Depra.Sound.Playback.UnitTests;

public sealed class AudioParamTests
{
	[Fact]
	public void Vector3_KeepsComponentsInline()
	{
		var parameter = AudioParam.Vector3(new AudioParamId(17), 1f, 2f, 3f);

		parameter.Type.Should().Be(AudioParamType.VECTOR3);
		parameter.Float0.Should().Be(1f);
		parameter.Float1.Should().Be(2f);
		parameter.Float2.Should().Be(3f);
	}

	[Fact]
	public void Reference_KeepsExistingObjectReference()
	{
		var value = new object();

		var parameter = AudioParam.Ref(new AudioParamId(18), value);

		parameter.Type.Should().Be(AudioParamType.REFERENCE);
		parameter.ReferenceValue.Should().BeSameAs(value);
	}

	[Fact]
	public void Custom_CarriesCustomTypeAndInlinePayload()
	{
		var parameter = AudioParam.Custom(123, new AudioParamId(19),
			float0: 1f, float1: 2f, integerValue: 3);

		parameter.Type.Should().Be(AudioParamType.CUSTOM);
		parameter.CustomTypeId.Should().Be(123);
		parameter.Float0.Should().Be(1f);
		parameter.Float1.Should().Be(2f);
		parameter.IntegerValue.Should().Be(3);
	}

	[Fact]
	public void CustomReference_KeepsExistingObjectReferenceAndInlinePayload()
	{
		var value = new object();

		var parameter = AudioParam.CustomRef(123, new AudioParamId(19), value,
			float0: 1f, integerValue: 3);

		parameter.Type.Should().Be(AudioParamType.CUSTOM);
		parameter.CustomTypeId.Should().Be(123);
		parameter.ReferenceValue.Should().BeSameAs(value);
		parameter.Float0.Should().Be(1f);
		parameter.IntegerValue.Should().Be(3);
	}
}
