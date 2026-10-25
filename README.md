# Sound

## Sound events

ECS playback passes a value-type `AudioEventId` to `IAudioPlayback`; the service
resolves it through an `AudioTable` containing banks. Dynamic parameters use a contiguous collection of `AudioParameter` values:

```csharp
AudioParameter[] parameters =
{
	AudioParameter.Float(AudioParameterId.Pitch, 1.1f),
	AudioParameter.Bool(AudioParameterId.Loop, true),
	AudioParameter.Vector3(positionId, position.x, position.y, position.z),
	AudioParameter.Reference(targetId, unityObject),
};
playback.Play(eventId, parameters);
```

For object-oriented call sites, the `params` overload lets you pass parameters
inline:

```csharp
playback.Play(eventId,
	AudioParameter.Reference(targetId, transform),
	AudioParameter.Float(AudioParameterId.Pitch, pitch));
```

That syntax creates a parameter array on each call. Use it for occasional
playback; for frequently called or ECS code, pass a reusable array/span as
shown below.

`AudioParameter` can hold an object reference (for example, a Unity
`Transform`), so it is a managed type and cannot be used with `stackalloc`.
Keep one array per caller/component and overwrite the used entries when playing
repeatedly instead of allocating a new array each time:

```csharp
private readonly AudioParameter[] _parameters = new AudioParameter[2];

void PlayAt(Transform target, float pitch)
{
	_parameters[0] = AudioParameter.Reference(targetId, target);
	_parameters[1] = AudioParameter.Float(AudioParameterId.Pitch, pitch);
	playback.Play(eventId, _parameters.AsSpan(0, 2));
}
```

An `IAudioBank` resolves IDs to event data (`IAudioEventDescription`), which
holds the clip and static parameters. The playback service validates dynamic
parameters, then calls the source's configured `Play` overload with static and
dynamic parameter spans separately. The source creates its backend instance,
applies both spans, then starts playback; dynamic values override static ones.
No merged parameter array or per-play managed wrapper is needed. Direct source
playback remains a single `source.Play(clip)` call.

Descriptions can be stored in Unity `ScriptableObject` assets by a Unity
integration; the core library does not reference `UnityEngine`. Integrations
can add their own parameter IDs and custom type IDs. `AudioParameter.Custom`
provides four inline floats and one inline integer; `CustomReference<T>` also
stores an existing reference for integration-specific data such as FMOD labels.

`Play` accepts `ReadOnlySpan<AudioParameter>`: passing an existing array or
span does not allocate a parameter collection. Building a new array for every
call still allocates. Constructing parameter values, including `Vector3` and
reference-valued parameters, does not allocate; a reference-valued parameter
stores the existing reference. Avoid passing value types to `Reference`,
because that would box them; its `class` constraint catches this at compile
time. Since the array retains its object references, overwrite or clear entries
that are no longer used so they do not keep objects alive unnecessarily.
`Play`, the source, and description must also avoid allocating internally.
