# AGENTS.md

Utility library for Sentinels of the Multiverse mods (see README.md), plus an NUnit test project that also holds
regression tests for bugs in Handelabra's engine.

## Layout

- `JpSOTMUtilities.csproj` - the library (.NET Framework 4.8, strongly named with `sotm_key.snk`, shipped as a NuGet
  package via `JpSOTMUtilities.nuspec`).
- `JpSOTMUtilitiesTests/` - SDK-style NUnit 4 test project. It references `SentinelsEngine.dll` and friends straight
  from the Steam install, so the game has to be installed at the default Steam path. `ILRepack.targets` merges the
  library into the test assembly (see README.md for why).
- `JpSOTMUtilitiesTests/TestMod/` - a small test mod (alignment test decks, Powerless Test Hero) loaded by the tests.
- `JpSOTMUtilitiesTests/HandleabraBugTests/` (sic) - tests for engine bugs reported to Handelabra, not for this library.
- `JpSOTMUtilitiesTests/BaseTest.cs` - Handelabra's test base class, shared with the Parahumans of the Wormverse
  (`../potw/Test/BaseTest.cs`) and Dino Fancie (`../dinofancie/UnitTests/DinoFancieUnitTests/BaseTest.cs`) repos.
  Keep the copies in sync rather than letting them diverge; if you change one, port the change to the others.

## Building and testing

```
dotnet test
```

All test classes are in the `Jp.SOTMUtilities.UnitTest` namespace regardless of folder, so filter by class name. To
run only the library's own tests (these should all pass):

```
dotnet test --filter "FullyQualifiedName!~MiscellaneousTests&FullyQualifiedName!~HeroicInfinitorTest&FullyQualifiedName!~GetOutOfTheWayTest"
```

## Expected test failures

The Handelabra bug tests assert the correct behaviour, so a test for a bug that's still in the engine fails. As of the
Dec 2024 engine, these 13 tests in `HandleabraBugTests/MiscTests.cs` are expected to fail:

- `TestPompadourDestructionVsTrigger`
- `TestBattleDroneVsChokepoint`
- `TestBattleDroneVsChokepoint2`
- `TestReturnPhaseActionGranterToHand`
- `TestSelectHeroToDrawCardsRequiredDraws`
- `TestCalledToJudgementWithPowerlessHero`
- `TestEmptyHandPlayPhaseWithAndWithoutPhaseOrdering`
- `TestGuiseCopiesShockingAnimation`
- `TestPowerOverwhelmingMovedDuringPowerPhase`
- `TestMostCardsTieBreakWithInhibitedSource`
- `TestGuiseTakesSkyScraperSizeCardForAnotherHero`
- `TestZealousOffenseAfterEnvironmentDamage`
- `TestLivingWeaponAfterEnvironmentDamage`

`TestAdvancedArgoLemmeSeeThat` is also still broken, but it's `[Explicit]` because its StackOverflowException kills the
test host, so it doesn't run as part of `dotnet test`. Run it on its own.

Any other failure is a real regression. If one of the tests above starts passing, the engine has probably been
updated; check what changed, then update the test's comment and this list.

## Writing Handelabra bug tests

Each test in `HandleabraBugTests/` has a comment above it covering:

- where the bug came from (who reported or found it, and when it was reported to Handelabra),
- the cards and rules text involved, and what goes wrong,
- the expected crash, if the bug is a crash,
- its status: "Fixed as of the Dec 2024 engine" or "Still broken as of the Dec 2024 engine".

Follow the same format for new tests. When a test documents a still-broken bug, add it to the list above.

## Inspecting the engine

`ilspycmd` can decompile the game's assemblies, e.g.
`ilspycmd -t <FullTypeName> -r "<Managed dir>" "<dll>"`, where the Managed dir is
`C:\Program Files (x86)\Steam\steamapps\common\Sentinels of the Multiverse\Sentinels_Data\Managed`.

- Engine logic (GameController, card controllers) is in `SentinelsEngine.dll`. Card text is in its embedded
  `Handelabra.Sentinels.Engine.DeckLists.*.json` resources (promo/variant cards are under `promoCards`; some files
  need trailing commas stripped before parsing).
- The game client UI is in `Assembly-CSharp.dll`. It updates separately from the engine, so a UI-only bug can be fixed
  without an engine change, and unit tests can't reach UI code.
