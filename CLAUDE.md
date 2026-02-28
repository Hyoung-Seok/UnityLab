# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Unity 6 (6000.3.10f1 LTS) sandbox for researching game development algorithms, design patterns, and extensible C# architecture patterns. The project is in its scaffolding phase — folder structure and packages are set up, but no custom game scripts exist yet.

## Unity Editor Commands

- **Build:** File > Build Settings (no CLI build scripts)
- **Run Tests:** Window > General > Test Runner (Unity Test Framework 1.6.0 installed, no tests written yet)
- **Play Mode:** Enter Play Mode Options enabled with Domain Reload and Scene Reload both disabled for fast iteration

## Architecture & Conventions

### Folder Structure (numbered prefix ordering)
- `Assets/1_Scenes/` — Scene files
- `Assets/2_Scripts/` — Game scripts (all custom code goes here)
- `Assets/3_Prefabs/` — Prefab assets
- `Assets/4_ModelsTextures/` — 3D models and textures
- `Assets/5_Animations/` — Animation clips and controllers
- `Assets/6_Data/` — Data assets / ScriptableObjects
- `Assets/Plugins/` — Third-party code plugins (editor-only)
- `Assets/Thirdparty/` — Third-party art/content assets
- `Assets/Settings/` — URP pipeline and volume profile assets

### Rendering
URP 17.3.0 with two quality tiers: **Mobile** (`Mobile_RPAsset`) and **PC** (`PC_RPAsset`). Never use Built-in Render Pipeline APIs.

### Key Installed Packages
- **Input:** New Input System 1.18.0 — use `UnityEngine.InputSystem`, not legacy `UnityEngine.Input`
- **Camera:** Cinemachine 3.1.6
- **Assets:** Addressables 2.9.1
- **Animation:** Animation Rigging 1.4.1, Timeline 1.8.11
- **AI:** AI Navigation 2.0.10
- **DOTS:** Entities 1.4.2, Burst 1.8.28, Collections 2.6.2, Mathematics 1.3.3, Physics 1.4.2 (transitive via Character Controller 1.4.2)
- **MCP:** unity-mcp (git, for Claude Code integration)

## Critical Development Notes

- **Fast Play Mode is ON (Domain Reload disabled):** Static fields are NOT reset between play sessions. Always initialize statics explicitly (e.g., `[RuntimeInitializeOnLoadMethod]` or `Awake`/`OnEnable`). Never assume static state is clean on play.
- **Force Text serialization:** All `.unity`, `.prefab`, `.asset` files are YAML — safe for version control diffs.
- **No root namespace configured:** Establish a namespace convention when creating the first scripts.
- **No assembly definitions (.asmdef) for game code yet:** Create them when adding script modules.
- **Commit messages are in Korean.**
- **Unity 6 APIs:** Use current Unity 6 APIs. Avoid deprecated Unity 5/2017-era patterns.
