# FromCraddleToGrave (Unity)

A day-by-day life simulation where you juggle **Time**, **Health**, **Satisfaction**, and **Money** from youth to retirement.  
Make choices, react to random events, and see how small daily decisions compound over a lifetime.  
The game can be played **manually (day by day)** or in **Auto** mode where you lock in rules and fast-forward years while still reacting to surprises.

---

## 🧠 Core Idea

Treat life as an **optimization game**:  
too much fun drains your budget; bad education choices limit jobs; neglecting relationships hurts satisfaction and ultimately health.  
The loop is simple: plan, act, adjust pace, and watch outcomes across life stages (youth → adulthood → retirement).

---

## ⚙️ Key Systems

- **Four primary stats:**  
  - **Time** (24h/day)  
  - **Health** (decays with age)  
  - **Satisfaction** (low satisfaction hurts health)  
  - **Money** (needed for survival and lifestyle)

- **Statuses & milestones:**  
  Education, injury, illness, marriage, child, loss of a loved one, “motivated” or “good relationships” multipliers.

- **Random events:**  
  Inflation, injury, disease, found money, promotion, help from a friend, etc.  
  Events require player decisions and can affect avatar behavior or pose.

- **Room-based interactions:**  
  Walk to objects and press **E** to open contextual UIs (budget, wardrobe, stats, activities).

- **Time handling:**  
  - **Manual mode:** play one day at a time, choose actions manually.  
  - **Auto mode:** lock daily/weekly/monthly rules (budget, job, free time) and fast-forward the simulation.

- **End & summary:**  
  When health reaches zero, the game ends with a lifetime report showing  
  health history, savings, satisfaction, rest time, and key milestones.

---

## 🎮 Game Modes

- **Sandbox:** open-ended life simulation.  
- **Challenges:** predefined tough scenarios.  
- **Story:** educational narrative with branching decisions.

---

## 🧩 Tech Stack

- **Unity 6** (Input System, CharacterController)
- **ScriptableObjects** for data-driven design:
  - Simulation config  
  - Player attributes  
  - Actions  
  - Random events  

---

### ScriptableObjects

| ScriptableObject | Description |
|------------------|-------------|
| **PlayerAttributesSO** | Defines starting stats. |
| **SimulationConfigSO** | Health decay, min/max values, random event frequency. |
| **GameActionSO** | Represents daily activities with stat changes and time cost. |
| **RandomEventSO** | Probabilistic events that apply effects instantly or over time. |

### Core Components

| Script | Description |
|--------|-------------|
| **SimulationManager** | Core of the system. Simulates each day, updates player stats, triggers events, applies effects, and ends simulation when health ≤ 0. |
| **PlayerController3D** | Movement, camera look, interaction (E key), and animation logic with temporary input lock. |
| **UIManager** | Handles showing/hiding different UI windows when interacting with objects. |

---

## 🎮 Controls

| Action | Key |
|--------|-----|
| Move | **WASD** |
| Look | **Mouse** |
| Interact | **E** |

Movement and look can be temporarily locked when performing actions or opening UIs.

---

## ⏳ Simulation Flow

Each simulated day consists of:
1. **Random event checks** – roll probabilities for available events.  
2. **Apply active effects** – ongoing stat modifiers (with remaining duration).  
3. **Aging tick** – apply daily health decay based on active multipliers.  
4. **Clamp player values** and increment age/day counter.  
5. **Auto mode** can loop multiple days, pausing for events or critical decisions.

---

## 📸 Screenshots

<img width="434" height="235" alt="FCTG_2" src="https://github.com/user-attachments/assets/bc48bd4e-f89e-4124-99c6-ff68927e3738" />

<img width="633" height="437" alt="FCTG_3" src="https://github.com/user-attachments/assets/845c2c07-5061-48f8-940e-0c5164cde1c6" />

<img width="697" height="465" alt="FCTG_4" src="https://github.com/user-attachments/assets/b6d9f3ad-655c-4ef5-82f1-b53fea314143" />

<img width="647" height="501" alt="FCTG_5" src="https://github.com/user-attachments/assets/bc0d2d88-51f1-4b72-b92c-d10ccc1f4097" />

<img width="995" height="539" alt="FCTG_7" src="https://github.com/user-attachments/assets/635e6c9d-b244-4009-97f2-1557d3bb6879" />

---

## 🚀 Getting Started

1. Open the Unity project.  
2. Create assets via **Create > LifeSim > ...** for player stats, actions, and events.  
3. Add `SimulationManager` and `PlayerState` to the scene.  
4. Assign your ScriptableObjects and link UIManager windows.  
5. Press Play and step through the simulation day by day — or enable **Auto** to run faster.

---

## 🧭 Roadmap

- Add career and education trees with dependencies.  
- Expand random events and lifestyle items (home, car, travel, hobbies).  
- Introduce relationship mechanics with emotional effects.  
- Challenge and story-driven modes.
- Implement full economy and tweak it for optimal gameplay
- Full gameloop implementation

---

## 👥 Credits

Created in **Unity 6**  
Design & Implementation: **Paweł Dzikiewicz**
Documentation based on the *HackYeah 2025* project materials.

---


