# 🎮 Turn-Based RPG — Full Stack Challenge (Nordeus 2026)

## 📌 Overview

A full-stack turn-based RPG prototype featuring a server-driven combat system, run-based progression, and modular game architecture built in Unity.

The player progresses through a 5-node encounter map, fighting monsters in sequential battles, learning new abilities, leveling up, and managing their loadout between encounters.

---

## 🎮 Gameplay Loop

1. Start new run → fetch configuration from server  
2. Traverse encounter map (5 nodes)  
3. Enter turn-based combat  
4. Resolve battle via server-driven logic  
5. Learn new monster abilities on win  
6. Level up and progress  
7. Repeat until final encounter  

---

## 🗺️ Map System

- 5-node encounter map (designed for future procedural generation)
- Node states:
  - 🔒 Locked
  - ⚪ Available next encounter
  - 🟢 Completed
- Re-playable encounters for XP and move farming
- Move management available between battles

📽️ **Demo: Map Navigation & Node States**  
<!-- Insert video/GIF link here -->

---

## ⚔️ Combat System

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
<!-- Insert video/GIF link here -->

---

## 🧠 Event-Based Combat Reconciliation

Instead of directly applying results, the server returns structured events:

- Damage events
- Heal events
- Status application events

Client responsibility:
- Replay events sequentially
- Reconstruct battle visualization deterministically

📽️ **Demo: Battle Log + Event Replay**  
<!-- Insert video/GIF link here -->

---

## 📈 Progression System

- XP-based leveling system
- Flat stat scaling per level:
  - Attack
  - Defense
  - Magic
  - Health (+max HP scaling)
- Monster kill → XP gain
- Level-up triggers stat increase

📽️ **Demo: Level Up Sequence**  
<!-- Insert video/GIF link here -->

---

## 🧙 Move System

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
<!-- Insert video/GIF link here -->

---

## 💾 Save System

- Single-slot save system
- Save & Exit flow from map
- Load disabled if no save exists
- Overwrites previous run intentionally

📽️ **Demo: Save/Load Flow**  
<!-- Insert video/GIF link here -->

---

## 🌐 Backend Architecture

- Server-driven run configuration (5 encounters)
- Monster move selection endpoint (per turn)
- Result pattern for structured responses
- Custom validation pipeline (rule-based system)

---

## 🧱 Architecture Overview

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

## 🚧 Known Limitations

- No final victory screen after last encounter
- No visible XP/level UI during run
- Single save slot only
- Map is static (procedural generation planned)
- Move system is not drag-and-drop yet

---

## 🔮 Planned Improvements

- Procedural map generation
- Drag & drop move system
- Status effects expansion (poison, bleed, etc.)
- Resource system (mana/energy)
- Improved battle animations
- Smarter AI (context-aware decisions)
- End-game victory screen
- Hero class system

---

## 🎥 Full Playthrough Demo

📌 Full gameplay recording:  
https://youtu.be/XRCgYCd0nDU

Includes:
- Start new run
- Map navigation
- Multiple battles
- Level-up showcase
- Move management usage
- Save & exit flow

---

## 🧑‍💻 Author

**Name:** David Novaković  
**LinkedIn:** www.linkedin.com/in/novakovic-david
