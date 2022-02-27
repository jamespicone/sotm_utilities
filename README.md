What is this?
====

This is a library of utilities for mods for the Sentinels of the Multiverse videogame. It was developed by Jp for the [Parahumans of the Wormverse](https://github.com/jamespicone/potw) mod, and also for [Dino Fancie](https://steamcommunity.com/sharedfiles/filedetails/?id=2665501622).

What features does it provide?
====

Current features are:
- A unified mechanism for testing card alignment that resembles English; allows writing `somecard.Is().Hero().Target()`.
- A status effect and helper for doing damage at a later phase
- A helper for having multiple targets meeting some criteria deal damage to a target with specific criteria (or do something else)
- A helper for determining if a phase has occurred this turn or not.

Why should I use it?
====

Some of these features are theoretically present in the engine already; so what's the benefit of this library? Well there's a few things.

Consistency
----

This code has a bug:
```csharp
// This is wrong; need to use AskIfIsVillainTarget
somecard.IsVillainTarget
```

This is buggy too:
```csharp
// This is wrong; need to use IsEnvironmentTarget
somecard.IsEnvironment && somecard.IsTarget
```

This is fine with base game content, but could theoretically be buggy with mod content:
```csharp
// This is the best the base game can do, but mod content could work badly with it
// (but base game content won't treat it properly)
somecard.IsHero && somecard.IsTarget
```

There are three different ways to test if a card is a specific type of target, and if you use the wrong one you'll have subtle bugs. This is how you'd do it with this library:

```csharp
// This works
somecard.Is().Villain().Target().AccordingTo(someController);

// This works too
somecard.Is().Environment().Target();

// This works as well (including for mod content that uses hero targetKinds)
somecard.Is().Hero().Target();
```

Being able to have a consistent way of checking card alignments, and knowing that it's correct at a glance, is very useful.

Engine Bugs
----

Sentinels is a complicated game, and the engine was designed only to work with base game content. Mods can do a lot of things it was never tested against. There's no guarantees any particular issue will be fixed, especially if it only affects mod content. They've got deadlines to meet and limited resources to apply to an old game. Some of the tools in this library are intended to address engine bugs.

For example, at time of writing, `Card.IsEnvironmentTarget` will incorrectly return `true` for targets that are of environment kind, but have a different targetKind.

How do I use it?
====

Simply add the package to your mod via NuGet. There are some consequences for unit testing, unfortunately, as the assembly is strongly named so that different mods can load different versions of the utility library at once. Your unit test application will likely refuse to load the assembly, complaining that SentinelsEngine.dll is not strongly named.

You can solve this by using ILRepack to pack the utility library into your mod for unit tests. There is a NuGet package that adds an MSBuild task to run ILRepack; I would recommend using that. The unit tests for the library demonstrate how to do this; Parahumans of the Wormverse also does the same thing.