# 🎮 Turn-Based RPG — Full Stack Challenge (Nordeus 2026)

<p align="center">
  <b>Full-stack turn-based RPG with server-authoritative combat and event-driven battle replay</b>
</p>

<p align="center">
  <a href="https://youtu.be/XRCgYCd0nDU">▶️ Watch Full Playthrough (3 min)</a>
</p>

## 📑 Table of Contents

- [Overview](#overview)
- [Gameplay Loop](#gameplay-loop)
- [Map System](#map-system)
- [Combat System](#combat-system)
- [Event-Based Combat Reconciliation](#event-based-combat-reconciliation)
- [Progression System](#progression-system)
- [Move System](#move-system)
- [Save System](#save-system)
- [Backend Architecture](#backend-architecture)
- [Architecture Overview](#architecture-overview)
- [How to Run](#how-to-run-the-project)
- [Known Limitations](#known-limitations)
- [Planned Improvements](#planned-improvements)

---

## 📌 Project Summary

A full-stack turn-based RPG with server-authoritative combat, event-based reconciliation, and run-based progression built in Unity + ASP.NET Core.

---

## Overview

A full-stack turn-based RPG prototype featuring a server-driven combat system, run-based progression, and modular game architecture built in Unity.

The player progresses through a 5-node encounter map, fighting monsters in sequential battles, learning new abilities, leveling up, and managing their loadout between encounters.

---

## Gameplay Loop

1. Start new run → fetch configuration from server  
2. Traverse encounter map (5 nodes)  
3. Enter turn-based combat  
4. Resolve battle via server-driven logic  
5. Learn new monster abilities on win  
6. Level up and progress  
7. Repeat until final encounter  

---

## Map System

- 5-node encounter map (designed for future procedural generation)
- Node states:
  - 🔒 Locked
  - ⚪ Available next encounter
  - 🟢 Completed
- Re-playable encounters for XP and move farming
- Move management available between battles

📽️ **Demo: Map Navigation & Node States**  
<p align="center">
  <img src="README-assets/MapDemo.gif" width="500" />
</p>

---

## Combat System

- Turn-based combat (player vs monster)
- Server-authoritative turn resolution
- Client sends action → server returns:
  - updated battle state
  - event list (damage/heal/status)
- Client reconstructs battle visually from event stream

### Features:
- Physical & Magic scaling system
- Defense mitigation system
- Status effects system (extensible)
- Battle log (color-coded actions)
- Floating combat text (damage/heal feedback)

📽️ **Demo: Full Battle Showcase**  
<p align="center">
  <img src="README-assets/CombatDemo.gif" width="500" />
</p>

---

## Event-Based Combat Reconciliation

Instead of directly applying results, the server returns structured events:

- Damage events
- Heal events
- Status application events

Client responsibility:
- Replay events sequentially
- Reconstruct battle visualization deterministically

📽️ **Demo: Battle Log + Event Replay**  
<p align="center">
  <img src="README-assets/BattleLogDemo.gif" width="500" />
</p>

---

## Progression System

- XP-based leveling system
- Flat stat scaling per level:
  - Attack
  - Defense
  - Magic
  - Health (+max HP scaling)
- Monster kill → XP gain
- Level-up triggers stat increase

---

## Move System

- 4 equipped move slots
- Full move library (learned from monsters)
- Move Manager UI:
  - equip / swap system
  - disables already-equipped moves
- ScriptableObject-based asset system:
  - Move definitions
  - Hero sprites (idle/attack/hit)
  - Effect sprites

📽️ **Demo: Move Management System**  
<p align="center">
  <img src="README-assets/MoveDemo.gif" width="500" />
</p>

---

## Save System

- Single-slot save system
- Save & Exit flow from map
- Load disabled if no save exists
- Overwrites previous run intentionally

📽️ **Demo: Save/Load Flow**  
<p align="center">
  <img src="README-assets/SaveDemo.gif" width="500" />
</p>

---

## Backend Architecture

- Server-driven run configuration (5 encounters)
- Monster move selection endpoint (per turn)
- Result pattern for structured responses
- Custom validation pipeline (rule-based system)

---

## Architecture Overview

### Client
- Unity (C#)
- GameManager (global state singleton)
- BattleManager (scene-scoped singleton)
- ApiClient (global singleton)

#### Systems:
- Combat System
- Progression System
- Map System
- UI System

### Server
- Stateless battle logic
- Config-driven encounter generation
- Turn resolution engine
  
---

## How to Run the Project

### 🎮 Unity Client

1. Open Unity Hub
2. Click **Open Project**
3. Select: `/Unity`
4. Open scene: `MainMenu`
5. Press Play ▶️
---

### 🌐 Backend Server (.NET 9 / ASP.NET Core)

1. Navigate to server project folder: `/TurnBasedRPG.API`

2. Restore dependencies: `dotnet restore`
   
3. Run the API: `dotnet run`

4. API will be available at: `https://localhost:7203` or `http://localhost:5192`
---

### ⚠️ Important

Make sure the backend is running before starting the Unity client, as the game depends on runtime API calls for:
- run configuration
- combat resolution
---

### 🧪 Recommended Flow

1. Start backend (`dotnet run`)
2. Open Unity project
3. Press Play
4. Click "Start New Run"
---

## Known Limitations

- No final victory screen after last encounter
- No visible XP/level UI during run
- Single save slot only
- Map is static (procedural generation planned)
- Move system is not drag-and-drop yet

---

## Planned Improvements

- Procedural map generation
- Drag & drop move system
- Status effects expansion (poison, bleed, etc.)
- Resource system (mana/energy)
- Improved battle animations
- Smarter AI (context-aware decisions)
- End-game victory screen
- Hero class system

---

## Full Playthrough Demo

Full demo included above

Includes:
- Start new run
- Map navigation
- Multiple battles
- Level-up showcase
- Move management usage
- Save & exit flow

---

## 🧑‍💻 Author

**David Novaković**  
**LinkedIn:** https://www.linkedin.com/in/novakovic-david
