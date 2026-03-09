# SP26-Spatial-Multitasking
# Designing for Multitasking: Spatial Object Placement in Virtual 3D Interfaces

**Team:** Spatial Multitasking Workspace  
**Course:** CS465 Multimodal Interaction for 3D Interfaces  

## Overview

This project explores how workspace layout design in virtual reality affects multitasking performance. We compare two versions of the same VR workflow to study whether users perform better when tools follow realistic desk placement rules or when they can be placed freely in 3D space.

Our prototype is a **spaceport inspection booth** where users must process incoming ships by checking their information, verifying cargo, and making approval decisions. The main goal is to measure how **spatial freedom**, **gravity**, and **tool placement rules** influence:

- task completion time
- error rate
- workload
- user preference

This project is relevant to VR training, simulation, and operational environments such as security checks, monitoring stations, and maintenance workflows, where speed, accuracy, and usability matter.

---

## Research Question

How does spatial object placement in a virtual 3D workspace affect multitasking performance?

More specifically, we compare:

- **Desk / Gravity Mode:** tools rest on a desk with gravity enabled and realistic placement constraints
- **Zero-G Mode:** tools can be placed and kept anywhere around the user in 3D space

By keeping the workflow the same across both conditions, we can directly compare how workspace design impacts usability and efficiency.

---

## Prototype Description

The user works in a **spaceport inspection booth** and processes incoming ships requesting entry.

For each ship case, the user must:

1. Use a **radio** to receive the ship’s request and verification code  
2. Enter the code on a **keypad** to pull up ship information  
3. Review the **manifest screen** to see the claimed cargo  
4. Use a **scanner** or **binocular-style tool** to inspect the ship and cargo  
5. Compare the observed cargo against the manifest  
6. Make a final **approve / deny** decision  

The prototype includes two versions of this same workflow:

### Version A: Realistic Desk Mode
- tools sit on the desk
- gravity is enabled
- placement behaves more like a real-world workstation

### Version B: Zero-G Mode
- tools can be placed anywhere in the environment
- no fixed desk layout is required
- users can organize their workspace spatially around themselves

---

## Study Design

This project uses a **within-subject study design**, meaning each participant experiences both interaction conditions.

### Planned participants
- 8–10 participants

### Controlled factors
- same tasks
- same ship cases
- same instructions
- same practice round
- same tools and workflow in both modes

### Measured variables
- task completion time
- time per subtask
- number of errors
- correctness of final decisions
- ease of use
- perceived workload
- user preference

### Procedure
1. Participants receive a short explanation of the study  
2. Participants review instructions for the VR task  
3. Participants complete a short practice/tutorial round  
4. Participants perform the inspection workflow in both conditions  
5. Data is logged during play  
6. Participants complete a short exit questionnaire and provide feedback  

---

## Hardware and Software

### Hardware
- Meta Quest / Oculus Quest headset

### Software
- Unity
- VR interaction packages / SDKs used in this project
- data logging tools built into the Unity prototype

> Update this section with the exact Unity version and SDK packages used in the repo.

---

## Repository Goals

This repository contains the development of a Unity VR prototype that supports:

- a complete spaceport inspection workflow
- two workspace conditions:
  - Desk / Gravity Mode
  - Zero-G Mode
- shared task logic across both conditions
- timing and error logging
- practice/tutorial flow
- experiment-ready prototype for user testing

---

## Expected Deliverables

- working Unity VR prototype
- two complete workspace modes
- implemented logging for time and errors
- short post-task survey materials
- study data and analysis summary
- final report
- final presentation/demo
