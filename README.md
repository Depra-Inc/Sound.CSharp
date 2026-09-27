# Sound

## Sound events

ECS playback passes a value-type `AudioEventId` to `IAudioPlayback`; the service
resolves it through an `AudioTable` containing banks. Dynamic parameters use a
contiguous collection of `AudioParameter` values:

```csharp
AudioParameter[] parameters =
{
	AudioParameter.Float(AudioParameterId.Pitch, 1.1f),
	AudioParameter.Bool(AudioParameterId.Loop, true),
};
playback.Play(eventId, parameters);
```

An `IAudioBank` resolves IDs to event data (`IAudioEventDescription`), which
holds the clip and applies static parameters. Playback applies description
parameters first, then dynamic parameters, then starts the clip. This lets
dynamic values override static ones.

Descriptions can be stored in Unity `ScriptableObject` assets by a Unity
integration; the core library does not reference `UnityEngine`. Integrations
can assign parameter IDs for integration-specific values. For example, an
FMOD label parameter can use an integer ID and a pre-existing string value,
without reflection, delegates, boxing, or runtime registration.

`Play` accepts `ReadOnlySpan<AudioParameter>`: passing an existing array or
span does not allocate a parameter collection. Building a new array for every
call still allocates; ECS buffers or reusable storage can be passed directly
when exposed as a span. The source and description implementations must also
avoid allocating internally.
