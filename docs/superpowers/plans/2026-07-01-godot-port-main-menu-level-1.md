# Godot Port Main Menu And Level 1 Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Create a separate Godot 4.7 GDScript repository named `gd-005-p1-numbers-to-100` with a Web-safe Main Menu -> Loading Screen -> Level 1 vertical slice.

**Architecture:** The Godot project mirrors the Unity project by responsibility and naming while using Godot-native scenes, nodes, signals, resources, and Autoloads. The first slice uses real imported assets selected from Main Menu, Loading Screen, and Level 1, with traceability documents recording Unity-to-Godot mappings and intentional engine-specific deviations.

**Tech Stack:** Godot 4.7 stable, GDScript, Compatibility renderer, Git, PowerShell, Windows, Web export target.

---

## Source And Target Paths

- Unity source repo: `C:\Users\My PC\Documents\GitHub\MathSpaceGame`
- Godot target repo: `C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100`
- Godot executable: `C:\Program Files (x86)\Steam\steamapps\common\Godot Engine\godot.windows.opt.tools.64.exe`
- Design spec: `C:\Users\My PC\Documents\GitHub\MathSpaceGame\docs\superpowers\specs\2026-07-01-godot-port-design.md`

Because the Godot target repo is outside the current writable workspace, execution will need approval before creating or editing files in `C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100`.

## File Structure To Create

```text
C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100\
  .gitignore
  README.md
  project.godot
  export_presets.cfg
  assets\
    audio\
    images\
    materials\
    models\
    textures\
  docs\
    migration\
      scene-map.md
      script-map.md
      asset-map.md
      web-export.md
  scenes\
    main_menu\main.tscn
    loading_screen\loading_screen.tscn
    levels\level_1\level_1.tscn
    level_sections\combat_section.tscn
    level_sections\minigame_section_bridge.tscn
    level_sections\collect_section.tscn
    level_sections\end_section.tscn
    player\player.tscn
    projectiles\player_projectile.tscn
    ui\pause_canvas.tscn
    ui\score_canvas.tscn
    ui\player_health_canvas.tscn
    ui\power_up_canvas.tscn
  scripts\
    autoload\audio_manager.gd
    autoload\game_manager.gd
    autoload\onboarding_manager.gd
    loading_screen\loading_screen_controller.gd
    levels\level_manager.gd
    levels\level_section.gd
    levels\move_forward.gd
    main_menu\main_menu.gd
    player\player_manager.gd
    player\player_script.gd
    player\spaceship_attack.gd
    player\spaceship_movement.gd
    projectiles\projectile_behaviour.gd
    systems\damageable.gd
    ui\pause_manager.gd
    ui\score_manager.gd
    ui\power_up_manager.gd
  tests\
    smoke\project_smoke_test.gd
```

## Task 1: Create The Godot Repository Shell

**Files:**
- Create: `C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100\.gitignore`
- Create: `C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100\README.md`
- Create: `C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100\project.godot`
- Create: `C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100\export_presets.cfg`

- [ ] **Step 1: Create target repo directory**

Run from `C:\Users\My PC\Documents\GitHub\MathSpaceGame` with approval for the sibling path:

```powershell
New-Item -ItemType Directory -Force "C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100"
```

Expected: directory exists at `C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100`.

- [ ] **Step 2: Initialize Git**

```powershell
git init "C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100"
```

Expected: `Initialized empty Git repository`.

- [ ] **Step 3: Create `.gitignore`**

Create `C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100\.gitignore`:

```gitignore
.godot/
.import/
export/
exports/
*.tmp
*.translation
*.import
*.uid
*.godot.uid
```

- [ ] **Step 4: Create `README.md`**

Create `C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100\README.md`:

```markdown
# gd-005-p1-numbers-to-100

Godot 4.7 GDScript port of the Unity `MathSpaceGame` Main Menu -> Loading Screen -> Level 1 vertical slice.

## Targets

- Engine: Godot 4.7 stable
- Language: GDScript
- Renderer: Compatibility
- Primary export: Web

## Source Reference

Unity source repository:

`C:\Users\My PC\Documents\GitHub\MathSpaceGame`

The Unity project remains the behavioral reference for scene flow, player movement, shooting, UI, audio, and Level 1 sequencing.
```

- [ ] **Step 5: Create `project.godot`**

Create `C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100\project.godot`:

```ini
; Engine configuration file.
; Best edited using the editor UI.

config_version=5

[application]

config/name="gd-005-p1-numbers-to-100"
run/main_scene="res://scenes/main_menu/main.tscn"
config/features=PackedStringArray("4.7")
config/icon="res://icon.svg"

[autoload]

GameManager="*res://scripts/autoload/game_manager.gd"
AudioManager="*res://scripts/autoload/audio_manager.gd"
OnboardingManager="*res://scripts/autoload/onboarding_manager.gd"

[display]

window/size/viewport_width=1920
window/size/viewport_height=1080
window/stretch/mode="canvas_items"
window/stretch/aspect="expand"

[input]

move_left={
"deadzone": 0.5,
"events": [Object(InputEventKey,"resource_local_to_scene":false,"resource_name":"","device":-1,"window_id":0,"alt_pressed":false,"shift_pressed":false,"ctrl_pressed":false,"meta_pressed":false,"pressed":false,"keycode":65,"physical_keycode":0,"key_label":0,"unicode":0,"location":0,"echo":false,"script":null)]
}
move_right={
"deadzone": 0.5,
"events": [Object(InputEventKey,"resource_local_to_scene":false,"resource_name":"","device":-1,"window_id":0,"alt_pressed":false,"shift_pressed":false,"ctrl_pressed":false,"meta_pressed":false,"pressed":false,"keycode":68,"physical_keycode":0,"key_label":0,"unicode":0,"location":0,"echo":false,"script":null)]
}
move_up={
"deadzone": 0.5,
"events": [Object(InputEventKey,"resource_local_to_scene":false,"resource_name":"","device":-1,"window_id":0,"alt_pressed":false,"shift_pressed":false,"ctrl_pressed":false,"meta_pressed":false,"pressed":false,"keycode":87,"physical_keycode":0,"key_label":0,"unicode":0,"location":0,"echo":false,"script":null)]
}
move_down={
"deadzone": 0.5,
"events": [Object(InputEventKey,"resource_local_to_scene":false,"resource_name":"","device":-1,"window_id":0,"alt_pressed":false,"shift_pressed":false,"ctrl_pressed":false,"meta_pressed":false,"pressed":false,"keycode":83,"physical_keycode":0,"key_label":0,"unicode":0,"location":0,"echo":false,"script":null)]
}
shoot={
"deadzone": 0.5,
"events": [Object(InputEventMouseButton,"resource_local_to_scene":false,"resource_name":"","device":-1,"window_id":0,"alt_pressed":false,"shift_pressed":false,"ctrl_pressed":false,"meta_pressed":false,"button_mask":0,"position":Vector2(0, 0),"global_position":Vector2(0, 0),"factor":1.0,"button_index":1,"canceled":false,"pressed":false,"double_click":false,"script":null)]
}
pause={
"deadzone": 0.5,
"events": [Object(InputEventKey,"resource_local_to_scene":false,"resource_name":"","device":-1,"window_id":0,"alt_pressed":false,"shift_pressed":false,"ctrl_pressed":false,"meta_pressed":false,"pressed":false,"keycode":4194305,"physical_keycode":0,"key_label":0,"unicode":0,"location":0,"echo":false,"script":null)]
}

[rendering]

renderer/rendering_method="gl_compatibility"
renderer/rendering_method.mobile="gl_compatibility"
textures/canvas_textures/default_texture_filter=1
```

- [ ] **Step 6: Create `export_presets.cfg`**

Create `C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100\export_presets.cfg`:

```ini
[preset.0]

name="Web"
platform="Web"
runnable=true
dedicated_server=false
custom_features=""
export_filter="all_resources"
include_filter=""
exclude_filter=""
export_path="exports/web/index.html"
encryption_include_filters=""
encryption_exclude_filters=""
encrypt_pck=false
encrypt_directory=false

[preset.0.options]

custom_template/debug=""
custom_template/release=""
variant/extensions_support=false
variant/thread_support=false
vram_texture_compression/for_desktop=true
vram_texture_compression/for_mobile=true
html/export_icon=true
html/custom_html_shell=""
html/head_include=""
html/canvas_resize_policy=2
html/focus_canvas_on_start=true
html/experimental_virtual_keyboard=false
progressive_web_app/enabled=false
```

- [ ] **Step 7: Create folders**

```powershell
Set-Location "C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100"
New-Item -ItemType Directory -Force assets\audio,assets\images,assets\materials,assets\models,assets\textures,docs\migration,scenes\main_menu,scenes\loading_screen,scenes\levels\level_1,scenes\level_sections,scenes\player,scenes\projectiles,scenes\ui,scripts\autoload,scripts\loading_screen,scripts\levels,scripts\main_menu,scripts\player,scripts\projectiles,scripts\systems,scripts\ui,tests\smoke
```

Expected: each directory exists.

- [ ] **Step 8: Commit repo shell**

```powershell
Set-Location "C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100"
git add .gitignore README.md project.godot export_presets.cfg
git commit -m "chore: create Godot project shell"
```

Expected: commit succeeds.

## Task 2: Add Migration Documentation

**Files:**
- Create: `docs/migration/scene-map.md`
- Create: `docs/migration/script-map.md`
- Create: `docs/migration/asset-map.md`
- Create: `docs/migration/web-export.md`

- [ ] **Step 1: Create scene map**

Create `docs/migration/scene-map.md`:

```markdown
# Scene Map

| Unity Scene | Godot Scene | First Slice Status |
| --- | --- | --- |
| `Assets/Scenes/MainMenu.unity` | `res://scenes/main_menu/main.tscn` | Required |
| `Assets/Scenes/LoadingScreen.unity` | `res://scenes/loading_screen/loading_screen.tscn` | Required |
| `Assets/Scenes/Level 1.unity` | `res://scenes/levels/level_1/level_1.tscn` | Required |
| `Assets/Scenes/Level 2.unity` | `res://scenes/levels/level_2/level_2.tscn` | Future |
| `Assets/Scenes/Level 3.unity` | `res://scenes/levels/level_3/level_3.tscn` | Future |
| `Assets/Scenes/TensAndOnesMinigame.unity` | `res://scenes/minigames/tens_and_ones/main.tscn` | Future |
| `Assets/Scenes/FillinMinigame.unity` | `res://scenes/minigames/fillin/main.tscn` | Future |
| `Assets/Scenes/DragAndDropMinigame.unity` | `res://scenes/minigames/drag_and_drop/main.tscn` | Future |

The Godot main scene is intentionally named `main.tscn` and assigned in `project.godot`.
```

- [ ] **Step 2: Create script map**

Create `docs/migration/script-map.md`:

```markdown
# Script Map

| Unity Script | Godot Script | Scope |
| --- | --- | --- |
| `GameManager.cs` | `res://scripts/autoload/game_manager.gd` | First slice |
| `AudioManager.cs` | `res://scripts/autoload/audio_manager.gd` | First slice |
| `OnboardingManager.cs` | `res://scripts/autoload/onboarding_manager.gd` | First slice |
| `LoadingScreenController.cs` | `res://scripts/loading_screen/loading_screen_controller.gd` | First slice |
| `LevelManager.cs` | `res://scripts/levels/level_manager.gd` | First slice |
| `LevelSection.cs` | `res://scripts/levels/level_section.gd` | First slice |
| `MoveForward.cs` | `res://scripts/levels/move_forward.gd` | First slice |
| `PlayerManager.cs` | `res://scripts/player/player_manager.gd` | First slice |
| `PlayerScript.cs` | `res://scripts/player/player_script.gd` | First slice |
| `SpaceshipMovement.cs` | `res://scripts/player/spaceship_movement.gd` | First slice |
| `SpaceshipAttack.cs` | `res://scripts/player/spaceship_attack.gd` | First slice |
| `ProjectileBehaviour.cs` | `res://scripts/projectiles/projectile_behaviour.gd` | First slice |
| `PauseManager.cs` | `res://scripts/ui/pause_manager.gd` | First slice |
| `ScoreManager.cs` | `res://scripts/ui/score_manager.gd` | First slice |
| `PowerUpManager.cs` | `res://scripts/ui/power_up_manager.gd` | First slice |

Unity singleton access maps to Godot Autoloads only for global lifetime services. Scene-specific managers remain nodes inside the scene that owns them.
```

- [ ] **Step 3: Create asset map**

Create `docs/migration/asset-map.md`:

```markdown
# Asset Map

## Copy Rules

Copied into Godot:

- `.png`
- `.jpg`
- `.wav`
- `.mp3`
- `.ogg`
- `.fbx`
- font files

Reference only:

- Unity `.meta`
- Unity `.mat`
- Unity `.prefab`
- Unity `.unity`
- TextMesh Pro package resources
- URP renderer settings
- DOTween runtime/editor files

## First Slice Asset Groups

| Unity Asset Area | Godot Area | Notes |
| --- | --- | --- |
| `Assets/Images` | `res://assets/images` | Main menu, UI, bars, topic art |
| `Assets/3D` | `res://assets/models` and `res://assets/textures` | Player ship, obstacles, rocks, planets, sky assets |
| `Assets/Resources/Audio` | `res://assets/audio` | BGM and SFX for first slice |
| `Assets/TextMesh Pro` | `res://assets/fonts` | Copy font files only when matching Unity UI text requires them; otherwise use Godot default UI fonts for the first slice |

Asset imports are scoped to Main Menu, Loading Screen, and Level 1 first.
```

- [ ] **Step 4: Create Web export notes**

Create `docs/migration/web-export.md`:

```markdown
# Web Export Notes

- Use Godot 4.7 stable.
- Use GDScript only.
- Use Compatibility renderer.
- Main scene: `res://scenes/main_menu/main.tscn`.
- Export path: `exports/web/index.html`.
- Avoid C#, GDExtension, Forward+, Mobile renderer, heavy post-processing, and unsupported audio effects.
- Test in Chromium-based browsers and Firefox.
- Keep mobile browser performance in mind when adding particles, textures, lighting, and audio.
```

- [ ] **Step 5: Commit migration docs**

```powershell
git add docs/migration
git commit -m "docs: add Unity to Godot migration maps"
```

Expected: commit succeeds.

## Task 3: Add Core Autoloads

**Files:**
- Create: `scripts/autoload/game_manager.gd`
- Create: `scripts/autoload/audio_manager.gd`
- Create: `scripts/autoload/onboarding_manager.gd`

- [ ] **Step 1: Create `game_manager.gd`**

```gdscript
extends Node

const TOPIC_COUNTING_UP_TO_100 := "Counting up to 100"
const TOPIC_NUMBER_PATTERNS := "Number Patterns"
const TOPIC_COMPARING_ORDERING := "Comparing and Ordering Numbers"

const LOADING_SCREEN_PATH := "res://scenes/loading_screen/loading_screen.tscn"

var selected_topic: String = ""

func select_topic_and_load(topic_name: String) -> void:
	selected_topic = topic_name
	var error := get_tree().change_scene_to_file(LOADING_SCREEN_PATH)
	if error != OK:
		push_error("Failed to load loading screen: %s" % error)

func get_selected_topic() -> String:
	return selected_topic
```

- [ ] **Step 2: Create `onboarding_manager.gd`**

```gdscript
extends Node

var knows_how_to_play := false
var is_using_mobile_device := false
```

- [ ] **Step 3: Create `audio_manager.gd`**

```gdscript
extends Node

var current_volume := 0.8

@onready var bgm_player: AudioStreamPlayer = AudioStreamPlayer.new()
@onready var sfx_player: AudioStreamPlayer = AudioStreamPlayer.new()

func _ready() -> void:
	add_child(bgm_player)
	add_child(sfx_player)
	set_volume(current_volume)

func play_bgm(stream: AudioStream, volume: float = 0.6) -> void:
	if stream == null:
		return
	bgm_player.stream = stream
	bgm_player.volume_db = linear_to_db(clamp(volume, 0.0, 1.0))
	bgm_player.play()

func play_sfx(stream: AudioStream) -> void:
	if stream == null:
		return
	sfx_player.stream = stream
	sfx_player.play()

func set_volume(value: float) -> void:
	current_volume = clamp(value, 0.0, 1.0)
	var volume_db := -80.0
	if current_volume > 0.0:
		volume_db = linear_to_db(current_volume)
	bgm_player.volume_db = volume_db
	sfx_player.volume_db = volume_db

func play_ui_hover_sfx() -> void:
	pass

func play_ui_click_button_sfx() -> void:
	pass

func play_player_shoot_sfx() -> void:
	pass

func play_hit_sfx(_world_position: Vector3) -> void:
	pass
```

- [ ] **Step 4: Validate project parses**

```powershell
& "C:\Program Files (x86)\Steam\steamapps\common\Godot Engine\godot.windows.opt.tools.64.exe" --headless --path "C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100" --quit
```

Expected: process exits with code `0` and no script parse errors.

- [ ] **Step 5: Commit autoloads**

```powershell
git add project.godot scripts/autoload
git commit -m "feat: add core autoload managers"
```

Expected: commit succeeds.

## Task 4: Build Main Menu And Loading Screen

**Files:**
- Create: `scripts/main_menu/main_menu.gd`
- Create: `scenes/main_menu/main.tscn`
- Create: `scripts/loading_screen/loading_screen_controller.gd`
- Create: `scenes/loading_screen/loading_screen.tscn`

- [ ] **Step 1: Create `main_menu.gd`**

```gdscript
extends Control

@onready var counting_button: Button = %CountingButton

func _ready() -> void:
	counting_button.pressed.connect(_on_counting_button_pressed)

func _on_counting_button_pressed() -> void:
	AudioManager.play_ui_click_button_sfx()
	GameManager.select_topic_and_load(GameManager.TOPIC_COUNTING_UP_TO_100)
```

- [ ] **Step 2: Create `main.tscn`**

Create `scenes/main_menu/main.tscn`:

```ini
[gd_scene load_steps=2 format=3]

[ext_resource type="Script" path="res://scripts/main_menu/main_menu.gd" id="1_main_menu"]

[node name="MainMenu" type="Control"]
layout_mode = 3
anchors_preset = 15
anchor_right = 1.0
anchor_bottom = 1.0
grow_horizontal = 2
grow_vertical = 2
script = ExtResource("1_main_menu")

[node name="Panel" type="Panel" parent="."]
layout_mode = 1
anchors_preset = 15
anchor_right = 1.0
anchor_bottom = 1.0
grow_horizontal = 2
grow_vertical = 2

[node name="Title" type="Label" parent="Panel"]
layout_mode = 1
anchors_preset = 5
anchor_left = 0.5
anchor_right = 0.5
offset_left = -360.0
offset_top = 120.0
offset_right = 360.0
offset_bottom = 190.0
grow_horizontal = 2
text = "Numbers to 100"
horizontal_alignment = 1

[node name="CountingButton" type="Button" parent="Panel"]
unique_name_in_owner = true
layout_mode = 1
anchors_preset = 8
anchor_left = 0.5
anchor_top = 0.5
anchor_right = 0.5
anchor_bottom = 0.5
offset_left = -260.0
offset_top = -55.0
offset_right = 260.0
offset_bottom = 25.0
grow_horizontal = 2
grow_vertical = 2
text = "Counting up to 100"
```

- [ ] **Step 3: Create `loading_screen_controller.gd`**

```gdscript
extends Control

const MIN_LOADING_TIME := 1.0

@onready var progress_bar: ProgressBar = %ProgressBar
@onready var topic_label: Label = %TopicLabel

var topic_to_scene := {
	GameManager.TOPIC_COUNTING_UP_TO_100: "res://scenes/levels/level_1/level_1.tscn",
	GameManager.TOPIC_NUMBER_PATTERNS: "res://scenes/levels/level_2/level_2.tscn",
	GameManager.TOPIC_COMPARING_ORDERING: "res://scenes/levels/level_3/level_3.tscn",
}

func _ready() -> void:
	var topic := GameManager.get_selected_topic()
	if topic.is_empty():
		topic = GameManager.TOPIC_COUNTING_UP_TO_100
	topic_label.text = topic
	_load_after_minimum_delay(topic)

func _load_after_minimum_delay(topic: String) -> void:
	progress_bar.value = 0.0
	var tween := create_tween()
	tween.tween_property(progress_bar, "value", 100.0, MIN_LOADING_TIME)
	await tween.finished
	var target_path: String = topic_to_scene.get(topic, "res://scenes/levels/level_1/level_1.tscn")
	var error := get_tree().change_scene_to_file(target_path)
	if error != OK:
		push_error("Failed to load scene '%s': %s" % [target_path, error])
```

- [ ] **Step 4: Create `loading_screen.tscn`**

```ini
[gd_scene load_steps=2 format=3]

[ext_resource type="Script" path="res://scripts/loading_screen/loading_screen_controller.gd" id="1_loading"]

[node name="LoadingScreen" type="Control"]
layout_mode = 3
anchors_preset = 15
anchor_right = 1.0
anchor_bottom = 1.0
grow_horizontal = 2
grow_vertical = 2
script = ExtResource("1_loading")

[node name="TopicLabel" type="Label" parent="."]
unique_name_in_owner = true
layout_mode = 1
anchors_preset = 5
anchor_left = 0.5
anchor_right = 0.5
offset_left = -400.0
offset_top = 360.0
offset_right = 400.0
offset_bottom = 420.0
grow_horizontal = 2
text = "Counting up to 100"
horizontal_alignment = 1

[node name="ProgressBar" type="ProgressBar" parent="."]
unique_name_in_owner = true
layout_mode = 1
anchors_preset = 8
anchor_left = 0.5
anchor_top = 0.5
anchor_right = 0.5
anchor_bottom = 0.5
offset_left = -420.0
offset_top = 90.0
offset_right = 420.0
offset_bottom = 130.0
grow_horizontal = 2
grow_vertical = 2
max_value = 100.0
```

- [ ] **Step 5: Run parse check**

```powershell
& "C:\Program Files (x86)\Steam\steamapps\common\Godot Engine\godot.windows.opt.tools.64.exe" --headless --path "C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100" --quit
```

Expected: process exits with code `0`.

- [ ] **Step 6: Commit menu and loading screen**

```powershell
git add scenes/main_menu scenes/loading_screen scripts/main_menu scripts/loading_screen
git commit -m "feat: add main menu and loading flow"
```

Expected: commit succeeds.

## Task 5: Build Player, Projectile, And Damage Contracts

**Files:**
- Create: `scripts/systems/damageable.gd`
- Create: `scripts/player/player_script.gd`
- Create: `scripts/player/player_manager.gd`
- Create: `scripts/player/spaceship_movement.gd`
- Create: `scripts/player/spaceship_attack.gd`
- Create: `scripts/projectiles/projectile_behaviour.gd`
- Create: `scenes/player/player.tscn`
- Create: `scenes/projectiles/player_projectile.tscn`

- [ ] **Step 1: Create `damageable.gd`**

```gdscript
extends Area3D
class_name Damageable

signal died
signal damaged(amount: int)

@export var max_health := 10
var health := 10

func _ready() -> void:
	health = max_health

func take_damage(amount: int) -> void:
	health = max(health - amount, 0)
	damaged.emit(amount)
	if health == 0:
		died.emit()
		queue_free()
```

- [ ] **Step 2: Create `spaceship_movement.gd`**

```gdscript
extends Node
class_name SpaceshipMovement

@export var move_speed := 5.0
@export var x_limit := 5.0
@export var y_limit := 5.0
@export var target_path: NodePath

@onready var target: Node3D = get_node(target_path)

func _process(delta: float) -> void:
	if get_tree().paused:
		return
	var input_vector := Vector2(
		Input.get_action_strength("move_right") - Input.get_action_strength("move_left"),
		Input.get_action_strength("move_up") - Input.get_action_strength("move_down")
	).limit_length(1.0)
	target.position.x = clamp(target.position.x + input_vector.x * move_speed * delta, -x_limit, x_limit)
	target.position.y = clamp(target.position.y + input_vector.y * move_speed * delta, -y_limit, y_limit)
```

- [ ] **Step 3: Create `projectile_behaviour.gd`**

```gdscript
extends Area3D
class_name ProjectileBehaviour

@export var projectile_speed := 30.0
@export var damage := 10

var move_dir := Vector3.ZERO
var target: Node3D

func _ready() -> void:
	body_entered.connect(_on_body_entered)
	area_entered.connect(_on_area_entered)

func _process(delta: float) -> void:
	if get_tree().paused:
		return
	if target != null and is_instance_valid(target):
		global_position += global_position.direction_to(target.global_position) * projectile_speed * delta
	else:
		if move_dir == Vector3.ZERO:
			queue_free()
			return
		global_position += move_dir.normalized() * projectile_speed * delta

func _on_body_entered(body: Node) -> void:
	_hit_node(body)

func _on_area_entered(area: Area3D) -> void:
	_hit_node(area)

func _hit_node(node: Node) -> void:
	if node.has_method("take_damage"):
		node.take_damage(damage)
	AudioManager.play_hit_sfx(global_position)
	queue_free()
```

- [ ] **Step 4: Create `spaceship_attack.gd`**

```gdscript
extends Node
class_name SpaceshipAttack

@export var bullet_spawn_path: NodePath
@export var projectile_scene: PackedScene
@export var fire_rate := 0.12
@export var damage := 10
@export var range := 1000.0

@onready var bullet_spawn: Node3D = get_node(bullet_spawn_path)

var fire_cooldown := 0.0

func _process(delta: float) -> void:
	if get_tree().paused:
		return
	fire_cooldown -= delta
	if Input.is_action_pressed("shoot") and fire_cooldown <= 0.0:
		normal_shoot()

func normal_shoot() -> void:
	if projectile_scene == null:
		return
	var camera := get_viewport().get_camera_3d()
	if camera == null:
		return
	var mouse_position := get_viewport().get_mouse_position()
	var ray_origin := camera.project_ray_origin(mouse_position)
	var ray_direction := camera.project_ray_normal(mouse_position)
	var aim_point := ray_origin + ray_direction * range
	var projectile := projectile_scene.instantiate() as ProjectileBehaviour
	get_tree().current_scene.add_child(projectile)
	projectile.global_position = bullet_spawn.global_position
	projectile.damage = damage
	projectile.move_dir = bullet_spawn.global_position.direction_to(aim_point)
	AudioManager.play_player_shoot_sfx()
	fire_cooldown = fire_rate

func get_damage() -> int:
	return damage

func set_fire_rate(new_fire_rate: float) -> void:
	fire_rate = new_fire_rate
```

- [ ] **Step 5: Create `player_script.gd`**

```gdscript
extends CharacterBody3D
class_name PlayerScript

@export var max_health := 10

@onready var movement: SpaceshipMovement = %SpaceshipMovement
@onready var attack: SpaceshipAttack = %SpaceshipAttack

var health := 10
var player_level := 1
var max_level := 4
var player_deaths := 0

func _ready() -> void:
	health = max_health

func get_damage() -> int:
	return attack.get_damage()

func get_max_health() -> int:
	return max_health

func add_max_health(added_value: int) -> void:
	max_health += added_value
	health = max_health

func level_up_player() -> void:
	if player_level < max_level:
		player_level += 1
```

- [ ] **Step 6: Create `player_manager.gd`**

```gdscript
extends Node
class_name PlayerManager

@export var player_path: NodePath
@onready var player: PlayerScript = get_node(player_path)

func get_player() -> PlayerScript:
	return player
```

- [ ] **Step 7: Create `player_projectile.tscn`**

```ini
[gd_scene load_steps=3 format=3]

[ext_resource type="Script" path="res://scripts/projectiles/projectile_behaviour.gd" id="1_projectile"]

[sub_resource type="SphereShape3D" id="SphereShape3D_projectile"]
radius = 0.15

[node name="PlayerProjectile" type="Area3D"]
script = ExtResource("1_projectile")

[node name="CollisionShape3D" type="CollisionShape3D" parent="."]
shape = SubResource("SphereShape3D_projectile")

[node name="MeshInstance3D" type="MeshInstance3D" parent="."]
```

- [ ] **Step 8: Create `player.tscn`**

```ini
[gd_scene load_steps=6 format=3]

[ext_resource type="Script" path="res://scripts/player/player_script.gd" id="1_player"]
[ext_resource type="Script" path="res://scripts/player/spaceship_movement.gd" id="2_movement"]
[ext_resource type="Script" path="res://scripts/player/spaceship_attack.gd" id="3_attack"]
[ext_resource type="PackedScene" path="res://scenes/projectiles/player_projectile.tscn" id="4_projectile"]

[sub_resource type="BoxShape3D" id="BoxShape3D_player"]
size = Vector3(1, 1, 1)

[node name="Player" type="CharacterBody3D"]
script = ExtResource("1_player")

[node name="CollisionShape3D" type="CollisionShape3D" parent="."]
shape = SubResource("BoxShape3D_player")

[node name="MeshParent" type="Node3D" parent="."]

[node name="BulletSpawn" type="Marker3D" parent="."]
transform = Transform3D(1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, -1.4)

[node name="SpaceshipMovement" type="Node" parent="."]
unique_name_in_owner = true
script = ExtResource("2_movement")
target_path = NodePath("..")

[node name="SpaceshipAttack" type="Node" parent="."]
unique_name_in_owner = true
script = ExtResource("3_attack")
bullet_spawn_path = NodePath("../BulletSpawn")
projectile_scene = ExtResource("4_projectile")
```

- [ ] **Step 9: Run parse check and commit**

```powershell
& "C:\Program Files (x86)\Steam\steamapps\common\Godot Engine\godot.windows.opt.tools.64.exe" --headless --path "C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100" --quit
git add scripts/systems scripts/player scripts/projectiles scenes/player scenes/projectiles
git commit -m "feat: add player movement and projectile combat"
```

Expected: Godot exits with code `0`, then commit succeeds.

## Task 6: Build Level 1 And Section Loop

**Files:**
- Create: `scripts/levels/move_forward.gd`
- Create: `scripts/levels/level_section.gd`
- Create: `scripts/levels/level_manager.gd`
- Create: `scenes/level_sections/combat_section.tscn`
- Create: `scenes/level_sections/minigame_section_bridge.tscn`
- Create: `scenes/level_sections/collect_section.tscn`
- Create: `scenes/level_sections/end_section.tscn`
- Create: `scenes/levels/level_1/level_1.tscn`

- [ ] **Step 1: Create `move_forward.gd`**

```gdscript
extends Node
class_name MoveForward

@export var speed := 5.0
@export var target_path: NodePath

var stop_moving := false
@onready var target: Node3D = get_node(target_path)

func _process(delta: float) -> void:
	if get_tree().paused or stop_moving:
		return
	target.global_position += target.global_transform.basis.z * speed * delta

func stop_movement() -> void:
	stop_moving = true

func start_movement() -> void:
	stop_moving = false
```

- [ ] **Step 2: Create `level_section.gd`**

```gdscript
extends Node3D
class_name LevelSection

@export var z_position_max := -80.0
@onready var move_forward: MoveForward = %MoveForward

func _process(_delta: float) -> void:
	if global_position.z <= z_position_max:
		queue_free()

func stop_movement() -> void:
	move_forward.stop_movement()

func start_movement() -> void:
	move_forward.start_movement()
```

- [ ] **Step 3: Create `level_manager.gd`**

```gdscript
extends Node
class_name LevelManager

@export var combat_section_scene: PackedScene
@export var minigame_section_scene: PackedScene
@export var collect_section_scene: PackedScene
@export var end_section_scene: PackedScene
@export var section_parent_path: NodePath

var level_state := 0
var timing := 1
var laps := 0
var current_sections: Array[LevelSection] = []

@onready var section_parent: Node3D = get_node(section_parent_path)

func spawn_next_section(offset: float = -50.0) -> void:
	var scene_to_spawn: PackedScene
	if timing == 1:
		scene_to_spawn = combat_section_scene
	elif timing == 2:
		scene_to_spawn = minigame_section_scene
		laps += 1
	elif laps >= 3:
		scene_to_spawn = end_section_scene
	else:
		scene_to_spawn = collect_section_scene
	if scene_to_spawn == null:
		return
	var new_section := scene_to_spawn.instantiate() as LevelSection
	section_parent.add_child(new_section)
	new_section.global_position = Vector3(0, 0, offset)
	current_sections.append(new_section)
	timing += 1
	if timing > 2:
		timing = 0

func stop_sections_from_moving() -> void:
	for section in current_sections:
		if is_instance_valid(section):
			section.stop_movement()

func start_sections_movement() -> void:
	for section in current_sections:
		if is_instance_valid(section):
			section.start_movement()
```

- [ ] **Step 4: Create the four section scenes**

Use this exact structure for each section, replacing the root `name` with `CombatSection`, `MinigameSectionBridge`, `CollectSection`, or `EndSection`.

```ini
[gd_scene load_steps=5 format=3]

[ext_resource type="Script" path="res://scripts/levels/level_section.gd" id="1_section"]
[ext_resource type="Script" path="res://scripts/levels/move_forward.gd" id="2_move"]
[ext_resource type="Script" path="res://scripts/systems/damageable.gd" id="3_damageable"]

[sub_resource type="BoxShape3D" id="BoxShape3D_target"]
size = Vector3(2, 2, 2)

[node name="CombatSection" type="Node3D"]
script = ExtResource("1_section")

[node name="MoveForward" type="Node" parent="."]
unique_name_in_owner = true
script = ExtResource("2_move")
target_path = NodePath("..")

[node name="Target" type="Area3D" parent="."]
script = ExtResource("3_damageable")

[node name="CollisionShape3D" type="CollisionShape3D" parent="Target"]
shape = SubResource("BoxShape3D_target")
```

- [ ] **Step 5: Create `level_1.tscn`**

```ini
[gd_scene load_steps=9 format=3]

[ext_resource type="PackedScene" path="res://scenes/player/player.tscn" id="1_player"]
[ext_resource type="Script" path="res://scripts/player/player_manager.gd" id="2_player_manager"]
[ext_resource type="Script" path="res://scripts/levels/level_manager.gd" id="3_level_manager"]
[ext_resource type="PackedScene" path="res://scenes/level_sections/combat_section.tscn" id="4_combat"]
[ext_resource type="PackedScene" path="res://scenes/level_sections/minigame_section_bridge.tscn" id="5_minigame"]
[ext_resource type="PackedScene" path="res://scenes/level_sections/collect_section.tscn" id="6_collect"]
[ext_resource type="PackedScene" path="res://scenes/level_sections/end_section.tscn" id="7_end"]

[node name="Level1" type="Node3D"]

[node name="Camera3D" type="Camera3D" parent="."]
transform = Transform3D(1, 0, 0, 0, 0.866025, 0.5, 0, -0.5, 0.866025, 0, 8, 14)
current = true

[node name="DirectionalLight3D" type="DirectionalLight3D" parent="."]
transform = Transform3D(1, 0, 0, 0, 0.707107, 0.707107, 0, -0.707107, 0.707107, 0, 8, 6)

[node name="Player" parent="." instance=ExtResource("1_player")]

[node name="Sections" type="Node3D" parent="."]

[node name="PlayerManager" type="Node" parent="."]
script = ExtResource("2_player_manager")
player_path = NodePath("../Player")

[node name="LevelManager" type="Node" parent="."]
script = ExtResource("3_level_manager")
combat_section_scene = ExtResource("4_combat")
minigame_section_scene = ExtResource("5_minigame")
collect_section_scene = ExtResource("6_collect")
end_section_scene = ExtResource("7_end")
section_parent_path = NodePath("../Sections")
```

- [ ] **Step 6: Run parse check and commit**

```powershell
& "C:\Program Files (x86)\Steam\steamapps\common\Godot Engine\godot.windows.opt.tools.64.exe" --headless --path "C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100" --quit
git add scripts/levels scenes/level_sections scenes/levels
git commit -m "feat: add Level 1 section loop"
```

Expected: Godot exits with code `0`, then commit succeeds.

## Task 7: Add UI Managers And Pause Behavior

**Files:**
- Create: `scripts/ui/pause_manager.gd`
- Create: `scripts/ui/score_manager.gd`
- Create: `scripts/ui/power_up_manager.gd`
- Create: `scenes/ui/pause_canvas.tscn`
- Create: `scenes/ui/score_canvas.tscn`
- Create: `scenes/ui/player_health_canvas.tscn`
- Create: `scenes/ui/power_up_canvas.tscn`
- Modify: `scenes/levels/level_1/level_1.tscn`

- [ ] **Step 1: Create `pause_manager.gd`**

```gdscript
extends CanvasLayer
class_name PauseManager

@onready var content: Control = %Content

func _ready() -> void:
	content.visible = false

func pause() -> void:
	get_tree().paused = true

func unpause() -> void:
	get_tree().paused = false

func show_screen() -> void:
	content.visible = true
	pause()
	AudioManager.play_ui_click_button_sfx()

func hide_screen() -> void:
	content.visible = false
	unpause()
	AudioManager.play_ui_click_button_sfx()

func retry() -> void:
	get_tree().paused = false
	get_tree().reload_current_scene()

func quit_to_menu() -> void:
	get_tree().paused = false
	get_tree().change_scene_to_file("res://scenes/main_menu/main.tscn")
```

- [ ] **Step 2: Create `score_manager.gd`**

```gdscript
extends CanvasLayer
class_name ScoreManager

@export var milestones: Array[int] = [25, 50, 75]
@onready var score_label: Label = %ScoreLabel

var current_score := 0

func _ready() -> void:
	update_score_ui()

func reset_score() -> void:
	current_score = 0
	update_score_ui()

func add_score(value: int) -> void:
	current_score += value
	update_score_ui()

func update_score_ui() -> void:
	score_label.text = "%s/%s" % [current_score, get_current_milestone()]

func get_current_milestone() -> int:
	for milestone in milestones:
		if current_score < milestone:
			return milestone
	return milestones[milestones.size() - 1]
```

- [ ] **Step 3: Create `power_up_manager.gd`**

```gdscript
extends CanvasLayer
class_name PowerUpManager

const HEALTH_BOOST := 1
const DOUBLE_BULLETS := 2
const FASTER_FIRE_RATE := 4
const HEAL_PER_HIT := 8

@export var health_bonus := 5
@export var upgraded_fire_rate := 0.08

var power_ups_received := 0

func receive_power_up(power_up: int, player: PlayerScript) -> void:
	if power_up == HEALTH_BOOST and not has_health_boost():
		player.add_max_health(health_bonus)
	elif power_up == FASTER_FIRE_RATE and not has_faster_fire_rate():
		player.attack.set_fire_rate(upgraded_fire_rate)
	power_ups_received |= power_up
	player.level_up_player()

func has_health_boost() -> bool:
	return (power_ups_received & HEALTH_BOOST) == HEALTH_BOOST

func has_double_bullets() -> bool:
	return (power_ups_received & DOUBLE_BULLETS) == DOUBLE_BULLETS

func has_faster_fire_rate() -> bool:
	return (power_ups_received & FASTER_FIRE_RATE) == FASTER_FIRE_RATE

func has_heal_per_hit() -> bool:
	return (power_ups_received & HEAL_PER_HIT) == HEAL_PER_HIT
```

- [ ] **Step 4: Create UI scenes**

Create simple Control-based scenes with the relevant root scripts:

`pause_canvas.tscn` root:

```ini
[gd_scene load_steps=2 format=3]
[ext_resource type="Script" path="res://scripts/ui/pause_manager.gd" id="1_pause"]
[node name="PauseCanvas" type="CanvasLayer"]
script = ExtResource("1_pause")
[node name="Content" type="Control" parent="."]
unique_name_in_owner = true
[node name="ContinueButton" type="Button" parent="Content"]
text = "Continue"
[node name="RetryButton" type="Button" parent="Content"]
offset_top = 60.0
offset_bottom = 91.0
text = "Retry"
[node name="ExitButton" type="Button" parent="Content"]
offset_top = 120.0
offset_bottom = 151.0
text = "Exit"
```

`score_canvas.tscn` root:

```ini
[gd_scene load_steps=2 format=3]
[ext_resource type="Script" path="res://scripts/ui/score_manager.gd" id="1_score"]
[node name="ScoreCanvas" type="CanvasLayer"]
script = ExtResource("1_score")
[node name="ScoreLabel" type="Label" parent="."]
unique_name_in_owner = true
text = "0/25"
```

`player_health_canvas.tscn` root:

```ini
[gd_scene format=3]
[node name="PlayerHealthCanvas" type="CanvasLayer"]
[node name="HealthLabel" type="Label" parent="."]
text = "Health"
```

`power_up_canvas.tscn` root:

```ini
[gd_scene load_steps=2 format=3]
[ext_resource type="Script" path="res://scripts/ui/power_up_manager.gd" id="1_power"]
[node name="PowerUpCanvas" type="CanvasLayer"]
script = ExtResource("1_power")
[node name="Panel" type="Panel" parent="."]
visible = false
```

- [ ] **Step 5: Instance UI scenes in `level_1.tscn`**

Add these ext_resources and node instances to `scenes/levels/level_1/level_1.tscn`:

```ini
[ext_resource type="PackedScene" path="res://scenes/ui/pause_canvas.tscn" id="8_pause"]
[ext_resource type="PackedScene" path="res://scenes/ui/score_canvas.tscn" id="9_score"]
[ext_resource type="PackedScene" path="res://scenes/ui/player_health_canvas.tscn" id="10_health"]
[ext_resource type="PackedScene" path="res://scenes/ui/power_up_canvas.tscn" id="11_power"]

[node name="PauseCanvas" parent="." instance=ExtResource("8_pause")]
[node name="ScoreCanvas" parent="." instance=ExtResource("9_score")]
[node name="PlayerHealthCanvas" parent="." instance=ExtResource("10_health")]
[node name="PowerUpCanvas" parent="." instance=ExtResource("11_power")]
```

- [ ] **Step 6: Run parse check and commit**

```powershell
& "C:\Program Files (x86)\Steam\steamapps\common\Godot Engine\godot.windows.opt.tools.64.exe" --headless --path "C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100" --quit
git add scripts/ui scenes/ui scenes/levels/level_1/level_1.tscn
git commit -m "feat: add first slice UI managers"
```

Expected: Godot exits with code `0`, then commit succeeds.

## Task 8: Copy First-Slice Assets

**Files:**
- Modify: files under `assets/audio`
- Modify: files under `assets/images`
- Modify: files under `assets/models`
- Modify: files under `assets/textures`
- Modify: `docs/migration/asset-map.md`

- [ ] **Step 1: Copy portable images**

Copy selected first-slice image folders and files from Unity:

```powershell
Set-Location "C:\Users\My PC\Documents\GitHub\MathSpaceGame"
Copy-Item -Recurse -Force "Assets\Images" "C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100\assets\images\Images"
Copy-Item -Force "Assets\TopicDescriptionWithText.png" "C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100\assets\images\TopicDescriptionWithText.png"
```

Expected: image files exist in the Godot repo.

- [ ] **Step 2: Copy selected 3D assets**

```powershell
Copy-Item -Recurse -Force "Assets\3D\Mesh" "C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100\assets\models\Mesh"
Copy-Item -Recurse -Force "Assets\3D\Skyboxes" "C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100\assets\textures\Skyboxes"
```

Expected: `.fbx`, texture, and skybox source files exist in the Godot repo.

- [ ] **Step 3: Copy audio if present**

```powershell
if (Test-Path "Assets\Resources\Audio") {
  Copy-Item -Recurse -Force "Assets\Resources\Audio" "C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100\assets\audio\Audio"
}
```

Expected: audio files exist if Unity source folder exists.

- [ ] **Step 4: Remove Unity metadata from copied assets**

```powershell
Get-ChildItem -Path "C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100\assets" -Recurse -Filter "*.meta" | Remove-Item
```

Expected: `Get-ChildItem -Path "C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100\assets" -Recurse -Filter "*.meta"` returns no files.

- [ ] **Step 5: Update `docs/migration/asset-map.md` with copied roots**

Append:

```markdown
## Imported In First Slice

- `Assets/Images` -> `res://assets/images/Images`
- `Assets/TopicDescriptionWithText.png` -> `res://assets/images/TopicDescriptionWithText.png`
- `Assets/3D/Mesh` -> `res://assets/models/Mesh`
- `Assets/3D/Skyboxes` -> `res://assets/textures/Skyboxes`
- `Assets/Resources/Audio` -> `res://assets/audio/Audio` when present
```

- [ ] **Step 6: Open project once for imports**

```powershell
& "C:\Program Files (x86)\Steam\steamapps\common\Godot Engine\godot.windows.opt.tools.64.exe" --headless --path "C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100" --quit
```

Expected: import completes without fatal errors.

- [ ] **Step 7: Commit assets**

```powershell
git add assets docs/migration/asset-map.md
git commit -m "chore: import first slice source assets"
```

Expected: commit succeeds.

## Task 9: Add Smoke Test Script

**Files:**
- Create: `tests/smoke/project_smoke_test.gd`

- [ ] **Step 1: Create smoke test**

```gdscript
extends SceneTree

const REQUIRED_FILES := [
	"res://project.godot",
	"res://scenes/main_menu/main.tscn",
	"res://scenes/loading_screen/loading_screen.tscn",
	"res://scenes/levels/level_1/level_1.tscn",
	"res://scripts/autoload/game_manager.gd",
	"res://scripts/player/spaceship_movement.gd",
	"res://scripts/player/spaceship_attack.gd",
]

func _init() -> void:
	var failed := false
	for path in REQUIRED_FILES:
		if not FileAccess.file_exists(path):
			push_error("Missing required file: %s" % path)
			failed = true
	if failed:
		quit(1)
	else:
		print("Smoke test passed.")
		quit(0)
```

- [ ] **Step 2: Run smoke test**

```powershell
& "C:\Program Files (x86)\Steam\steamapps\common\Godot Engine\godot.windows.opt.tools.64.exe" --headless --path "C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100" --script tests/smoke/project_smoke_test.gd
```

Expected: output includes `Smoke test passed.` and exits with code `0`.

- [ ] **Step 3: Commit smoke test**

```powershell
git add tests/smoke/project_smoke_test.gd
git commit -m "test: add Godot project smoke test"
```

Expected: commit succeeds.

## Task 10: Verify Play And Web Export

**Files:**
- Modify: `docs/migration/web-export.md`

- [ ] **Step 1: Run main scene headless parse**

```powershell
& "C:\Program Files (x86)\Steam\steamapps\common\Godot Engine\godot.windows.opt.tools.64.exe" --headless --path "C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100" --quit
```

Expected: process exits with code `0`.

- [ ] **Step 2: Run smoke test**

```powershell
& "C:\Program Files (x86)\Steam\steamapps\common\Godot Engine\godot.windows.opt.tools.64.exe" --headless --path "C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100" --script tests/smoke/project_smoke_test.gd
```

Expected: output includes `Smoke test passed.`.

- [ ] **Step 3: Export Web build**

```powershell
New-Item -ItemType Directory -Force "C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100\exports\web"
& "C:\Program Files (x86)\Steam\steamapps\common\Godot Engine\godot.windows.opt.tools.64.exe" --headless --path "C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100" --export-release "Web" "C:\Users\My PC\Documents\GitHub\gd-005-p1-numbers-to-100\exports\web\index.html"
```

Expected: `exports/web/index.html` exists. If export templates are missing, install Godot 4.7 export templates through the editor and rerun this step.

- [ ] **Step 4: Record verification**

Append to `docs/migration/web-export.md`:

```markdown
## First Slice Verification

- Headless project parse: passed.
- Smoke test: passed.
- Web export generated at `exports/web/index.html`.
- Manual browser playtest remains required for input, scene flow, and Web audio.
```

- [ ] **Step 5: Commit verification notes**

```powershell
git add docs/migration/web-export.md exports/web
git commit -m "test: verify first Godot web export"
```

Expected: commit succeeds if generated export files are intended to be versioned. If export files should not be versioned, commit only `docs/migration/web-export.md` and keep `exports/` ignored.

## Final Acceptance Criteria

- The sibling repo `gd-005-p1-numbers-to-100` exists and is a Git repo.
- Godot 4.7 opens the project without script parse errors.
- `project.godot` uses `res://scenes/main_menu/main.tscn` as the main scene.
- `main.tscn` can route through loading screen toward Level 1.
- Level 1 contains a player, camera, light, section parent, LevelManager, and UI scenes.
- Player movement and projectile scripts are present and parse.
- Real first-slice assets are copied without Unity `.meta` files.
- Migration docs exist.
- Smoke test passes.
- Web export command either succeeds or identifies missing export templates as the only blocker.
