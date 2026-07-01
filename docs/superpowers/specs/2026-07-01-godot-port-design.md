# Godot Port Design: gd-005-p1-numbers-to-100

## Purpose

Build a separate Godot repository that mirrors this Unity project as closely as Godot reasonably allows. The port should preserve player-facing behavior, visuals, scene flow, and internal responsibilities, while using Godot-native scenes, nodes, signals, resources, exported variables, and GDScript.

The first playable milestone is a vertical slice:

```text
Main Menu -> Loading Screen -> Level 1 -> core spaceship gameplay loop
```

The new repository will be named:

```text
gd-005-p1-numbers-to-100
```

## Engine And Export Target

- Godot 4.7 stable.
- GDScript only.
- Web export is a primary target.
- Use the Compatibility renderer for WebGL 2.0 support.
- Do not use C#, GDExtension, Forward+/Mobile rendering, or Web-unsupported audio/rendering features for the first milestone.
- Treat mobile browser performance as a requirement, because the Unity project already has mobile-oriented joystick and onboarding logic.

## Repository Structure

The Godot repository should use a traceable, Godot-native structure:

```text
addons/
assets/
  audio/
  images/
  materials/
  models/
  textures/
docs/
  migration/
scenes/
  main_menu/
  loading_screen/
  levels/level_1/
  player/
  projectiles/
  ui/
  level_sections/
scripts/
  autoload/
  main_menu/
  loading_screen/
  levels/
  player/
  projectiles/
  ui/
  systems/
tests/
```

The main scene must be named `main.tscn` so Godot can run the project cleanly:

```text
MainMenu.unity -> scenes/main_menu/main.tscn
```

`project.godot` must set:

```ini
run/main_scene="res://scenes/main_menu/main.tscn"
```

## Unity To Godot Mapping

The port should preserve Unity names and responsibilities when doing so improves traceability. Literal one-to-one engine concepts are not required when Godot has a better native equivalent.

Unity scenes map to Godot scenes:

```text
Assets/Scenes/MainMenu.unity      -> scenes/main_menu/main.tscn
Assets/Scenes/LoadingScreen.unity -> scenes/loading_screen/loading_screen.tscn
Assets/Scenes/Level 1.unity       -> scenes/levels/level_1/level_1.tscn
```

Unity singletons map to either Autoloads or scene-local controller nodes:

```text
GameManager.cs       -> scripts/autoload/game_manager.gd
AudioManager.cs      -> scripts/autoload/audio_manager.gd
OnboardingManager.cs -> scripts/autoload/onboarding_manager.gd

LevelManager.cs      -> scripts/levels/level_manager.gd
PauseManager.cs      -> scripts/ui/pause_manager.gd
ScoreManager.cs      -> scripts/ui/score_manager.gd
PowerUpManager.cs    -> scripts/ui/power_up_manager.gd
PlayerManager.cs     -> scripts/player/player_manager.gd or a scene-local player registry
```

Unity prefabs become Godot `PackedScene` files. Unity Resources-style objects, such as `ScoreCanvas`, `PauseCanvas`, `AudioManager`, projectiles, and level sections, become explicit `.tscn` scenes or `.tres` resources instead of hidden string loads.

MonoBehaviour fields become exported GDScript variables. UnityEvents and direct inspector references become Godot signals, node paths, exported `PackedScene` references, or resource references.

## First Milestone Flow

The first milestone mirrors Unity's current Level 1 path:

```text
main.tscn
  topic button: "Counting up to 100"
  -> GameManager.select_topic_and_load("Counting up to 100")
  -> loading_screen.tscn
  -> maps topic to scenes/levels/level_1/level_1.tscn
  -> Level 1 gameplay
```

The Unity topic mapping must be preserved:

```text
"Counting up to 100" -> Level 1
"Number Patterns" -> Level 2
"Comparing and Ordering Numbers" -> Level 3
```

Only the first topic needs to be playable in the first milestone. Other topic buttons can exist as disabled, stubbed, or documented follow-up entries.

## Gameplay State

`LevelManager.LevelState` should remain a recognizable contract:

```text
0 = Normal gameplay
1 = UI / power-up screen
2 = Minigame gameplay
3 = Game over
```

For the first playable slice:

- State `0` is required.
- State `1` is included if score milestones and the power-up screen are reached.
- State `2` can be stubbed or lightly represented until minigames are ported.
- State `3` is included as a simple end/failure path if the Unity dependencies are clear enough.

## Player And Combat

The player loop should preserve the Unity behavior:

- Bounded spaceship movement on local X/Y.
- Desktop keyboard input.
- Touch/mobile-ready input abstraction.
- Pause-aware movement and shooting.
- Mouse/touch raycast aiming.
- Projectile spawning.
- Projectile movement toward either a direction or a target.
- Hit detection against damageable targets.
- Damage application.
- VFX and audio hooks.

Manual shooting is part of the first milestone. Auto-shoot can be ported after manual shooting unless it is essential to Level 1's first playable path.

## Level Sections

The first milestone should preserve the Unity section sequencing model:

```text
combat section -> minigame section -> collect section -> repeat -> end section
```

If a minigame section is not fully ported yet, it should be represented by a traceable stub scene so the Level 1 loop remains intact without pretending the minigame is complete.

`LevelSection`, `MoveForward`, and section removal should be recreated as Godot scripts attached to section scenes. Section movement must pause when the game is paused.

## UI And Audio

Unity UGUI and TextMesh Pro UI should be rebuilt with Godot `Control` scenes. DOTween animations should be recreated with Godot `Tween` or `AnimationPlayer`.

Required first-milestone UI:

- Main menu topic selection.
- Loading screen with progress feedback.
- Score/XP display if Level 1 reaches score changes.
- Player health display.
- Pause screen with continue, retry, and exit.
- Power-up screen if score milestones are reachable.

`AudioManager` should become an Autoload that owns BGM and SFX players. Web audio limitations mean the first milestone should avoid relying on unsupported effects.

## Asset Import Scope

Real assets should be copied/imported immediately, but scoped to the first vertical slice:

- Main menu UI images, button sprites, topic art, logo/title art.
- Loading screen art and the topic image for `Counting up to 100`.
- Level 1 player ship, projectiles, core enemies/obstacles, rocks/sections, and skybox/background assets.
- SFX/BGM used by main menu, shooting, hit, pause, hover/click, and level flow.
- Fonts needed to approximate TextMesh Pro output in Godot Labels or RichTextLabels.

Godot import rules:

- Do not copy Unity `.meta` files.
- Copy portable source assets: `.png`, `.jpg`, `.wav`, `.mp3`, `.ogg`, `.fbx`, and font files.
- Do not copy Unity materials, prefabs, scenes, TMP assets, URP settings, or DOTween behavior as functional runtime assets.
- Use Unity-specific files as migration references only.
- Recreate materials as Godot resources using the Compatibility renderer.
- Leave large unused asset packs out until the scene that needs them is ported.

## Documentation

The Godot repo should include `docs/migration/` files that record traceability:

- Unity scene to Godot scene mapping.
- Unity script to GDScript mapping.
- Unity prefab/resource to Godot scene/resource mapping.
- Known deviations where Godot uses a different structure intentionally.
- Web export constraints and verification notes.

## Verification

Each milestone must be verified against these checks:

- Godot project opens without missing-script errors.
- `res://scenes/main_menu/main.tscn` runs as the main scene.
- Main menu topic selection reaches loading screen.
- Loading screen reaches Level 1.
- Player can move within bounds.
- Player can shoot and hit a target.
- Pause blocks gameplay.
- Core UI updates without null-reference-style errors.
- Web export launches in a browser.

The Unity project remains the behavioral reference during implementation.
