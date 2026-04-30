# Designing for Multitasking: Spatial Object Placement in Virtual 3D Interfaces

**Team:** Spatial Multitasking Workspace  
**Course:** CS465 Multimodal Interaction for 3D Interfaces  
**Group Members:** Marissa Graham and Michael Farrell  

## Overview

This project is a Unity VR research prototype that compares two workspace layouts for multitasking in virtual reality. The goal is to study whether users perform better in a realistic gravity-based desk workspace or in a zero-gravity workspace where tools can be arranged more freely in 3D space.

The prototype places the user in a **spaceport inspection booth**. Participants process incoming ships by logging in, receiving a verification code, entering the code on a keypad, checking a manifest screen, scanning ship/cargo information, and making an approve or deny decision.

The project measures how workspace layout affects:

- task completion time
- time spent on individual task steps
- wrong code submissions
- decision accuracy
- usability and participant preference

This project is relevant to VR training, simulation, and operational workspaces where users must switch between multiple tools while maintaining speed and accuracy.

---

## Research Question

When users must switch repeatedly among several tools and displays in a VR workspace, is performance and usability better with a gravity-constrained desk layout or with a zero-gravity free-placement layout?

---

## Workspace Conditions

### Gravity Condition

In the Gravity condition, tools are arranged around a realistic desk workspace. Objects use gravity-based placement and behave more like physical objects on a desk. This condition represents a more traditional workstation layout inside VR.

### Zero-G Condition

In the Zero-G condition, the same tools and workflow are used, but objects are not constrained by normal desk gravity in the same way. Tools can float or be positioned more freely in 3D space. This condition explores whether spatial freedom improves or hurts multitasking in VR.

---

## Task Workflow

Each ship case follows the same sequence:

1. Use the keycard to log in.
2. Use the radio to receive the ship verification code.
3. Enter the verification code on the keypad.
4. Read the ship/cargo information on the manifest screen.
5. Use the scanner to scan the ship/cargo.
6. Decide whether the ship should be approved or denied.
7. Press the green Approve button or the red Deny button.
8. Continue to the next case until the condition is complete.

The same workflow is used in both conditions so performance can be compared directly.

---

## Study Design

This project uses a within-subject study design. Each participant completes both the Gravity and Zero-G conditions.

### Measures Collected

The prototype records:

- participant ID
- condition name
- case number
- ship ID
- total case completion time
- keycard time
- radio time
- keypad time
- scan time
- decision time
- wrong code submissions
- final approve/deny decision
- whether the final decision was correct

The project also supports post-task feedback and subjective comparison between the two workspace conditions.

---

## Hardware and Software

### Hardware

- Meta Quest headset
- VR-capable computer
- Quest Link, Air Link, or compatible Quest development setup

### Software

- Unity
- Meta XR / Oculus VR setup
- XR interaction components
- TextMeshPro
- C# scripts for task flow, interaction logic, and data logging

---

## Main Scripts

### `ShipCaseManager.cs`

Controls the main experiment flow. This script manages the current ship case, controls the required task sequence, updates the manifest screen, checks progress, and advances the participant through the inspection workflow.

### `ExperimentLogger.cs`

Records participant data and saves results to CSV files. It logs case-level timing data, wrong code submissions, final decisions, and condition-level summary data.

### `KeyCardTrigger.cs`

Detects when the keycard interaction has been completed and allows the participant to progress to the next step.

### `KeypadButton.cs`

Handles keypad button presses, number entry, clear/enter behavior, code validation, and wrong code submissions.

### `ScannerTrigger.cs`

Handles scanner interactions and verifies that the participant has completed the scan step.

### `DecisionButton.cs`

Handles the approve and deny buttons and records the participant’s final decision for each case.

---

## Running the Project

1. Download or clone this GitHub repository.
2. Open Unity Hub.
3. Click **Add** or **Add project from disk**.
4. Select the main Unity project folder.
5. Wait for Unity to import the project files.
6. Open either the Gravity scene or the Zero-G scene.
7. Connect the Meta Quest headset.
8. Start Quest Link or Air Link if running through a computer.
9. Press Play in Unity, or build and run the project on the headset.
10. Follow the in-game workflow.

---

## Running the Gravity Scene

1. Open the scene for the Gravity condition.
2. Press Play in Unity or build the scene to the headset.
3. The participant begins inside the spaceport inspection booth.
4. Tools should be arranged around the desk.
5. Complete the ship inspection cases by following the prompts.

---

## Running the Zero-G Scene

1. Open the scene for the Zero-G condition.
2. Press Play in Unity or build the scene to the headset.
3. The participant begins inside the same spaceport inspection booth.
4. Complete the same ship inspection workflow.
5. Objects are not constrained in the same way by normal desk gravity.

---

## Data Logging

Data is saved automatically through the `ExperimentLogger` script. CSV files are written using Unity’s `Application.persistentDataPath`.

The exact save location depends on whether the project is run in the Unity Editor, on a computer, or directly on the headset.

The logger creates case-level and summary-level data for later analysis.

---

## Final Submission Materials

The final Canvas submission includes:

- Final Report PDF
- LaTeX source files
- Literature survey PDFs
- GitHub/source code link
- Video links
- Downloadable video file links
- Additional study materials and data

The README included in the Canvas ZIP contains the final video links, Overleaf link, GitHub link, and detailed grading notes.

---

## Project Status

This project was completed as a CS465 semester project. It is a research prototype designed to compare VR workspace layouts, not a commercial application. The focus is on experimental design, prototype implementation, data collection, and analysis of multitasking performance in VR.
