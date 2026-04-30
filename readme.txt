Project Name:
Designing for Multitasking: Spatial Object Placement in Virtual 3D Interfaces

Group Name:
Team Spatial Multitasking Workspace

Group Members:
Marissa Graham
Michael Farrell

Project Summary:
This project compares two VR workspace layouts for a multitasking spaceport inspection task. Participants completed the same workflow in a gravity-based desk condition and a zero-gravity condition. We measured completion time, task timing, wrong code submissions, and decision accuracy.

Links to Repo, Paper, and Videos

GitHub Repository:
https://github.com/csu-hci-projects/SP26-Spatial-Multitasking/tree/main

Short Overview Video:
View link: https://youtu.be/xK0uqYhxbMA
Download link: https://drive.google.com/file/d/121-iF6emJma_dzfSWN9TtiQYVGgpoEGs/view?usp=sharing 

Presentation Video:
View link:  https://youtu.be/yod3MRW_lEs
Download link: https://drive.google.com/file/d/1CV8OiHWJktfAB9qXr7LqXYuq-42Z02S4/view?usp=sharing 

Programming Video and Who Did What:
View link:  https://youtu.be/pNFRs1Yq2uw
Download link: https://drive.google.com/file/d/1_-X3UVGoUqqsZu4zG1rROGdVrbQhSUUW/view?usp=sharing 

Research Paper:
- LaTeX/Overleaf Project: https://www.overleaf.com/read/qcsnddnhwgqp#66f0d5 


Instructions for Running the App:

Project Type:
This project is a Unity VR research prototype built for the Meta Quest headset. The project contains two experimental workspace conditions: Gravity and Zero-G. Both conditions use the same spaceport inspection workflow so participant performance can be compared across workspace layouts.

Required Hardware:
- Meta Quest headset
- Computer capable of running the Unity project
- USB-C cable for Quest Link, or a working Air Link setup
- Enough open space for the participant to safely stand or sit while using VR

Opening the Unity Project:
1. Download or clone the GitHub repository listed in this README.
2. If using a downloaded ZIP from GitHub, extract the ZIP first.
3. Open Unity Hub.
4. Click “Add” or “Add project from disk.”
5. Select the main Unity project folder.
6. Wait for Unity to open the project and import the files. This may take several minutes the first time.
7. If Unity asks to use a different editor version, use the closest compatible version available.
8. After the project opens, locate the Scenes folder in the Project window.

Before Running:
1. Make sure the Meta Quest headset is charged.
2. Connect the headset to the computer using Quest Link or Air Link.
3. Confirm that the headset is recognized by the computer.
4. Make sure the participant has enough space and is not near obstacles.
5. Choose which condition to run first: Gravity or Zero-G.

Running the Gravity Condition:
1. In Unity, open the scene for the Gravity condition.
2. Press the Play button in Unity, or build and run the scene on the headset.
3. The participant should begin inside the spaceport inspection booth.
4. The tools should be arranged around the desk.
5. In this condition, the objects are organized like a realistic desk workspace and use gravity-based placement.
6. The participant should complete the inspection cases by following the in-game prompts.

Running the Zero-G Condition:
1. In Unity, open the scene for the Zero-G condition.
2. Press the Play button in Unity, or build and run the scene on the headset.
3. The participant should begin inside the same spaceport inspection booth.
4. The same task workflow is used as the Gravity condition.
5. In this condition, objects are not constrained in the same way by normal desk gravity and can float or be positioned more freely in 3D space.
6. The participant should complete the inspection cases by following the in-game prompts.

Task Workflow:
Each case follows the same sequence:

1. Keycard step:
   The participant uses the keycard to log in or activate the station.

2. Radio step:
   The participant uses the radio to receive the ship verification code.

3. Keypad step:
   The participant enters the verification code on the keypad with right index finger.

4. Manifest step:
   After the correct code is entered, the manifest screen displays the ship or cargo information.

5. Scanner step:
   The participant uses the scanner to inspect the ship or cargo information.

6. Decision step:
   The participant decides whether the ship should be approved or denied.

7. Button step:
   The participant presses the green Approve button or the red Deny button to finalize the case.

8. Next case:
   The system advances to the next ship case until the 15 conditions are complete.

Important Interaction Notes:
- The task steps are meant to be completed in order.
- Later steps should not count until the earlier required steps are completed.
- The keycard is used first to begin the workflow.
- The radio provides the verification code.
- The keypad checks whether the entered code is correct.
- The manifest screen gives the participant information needed to judge the case.
- The scanner is used to verify the ship/cargo information.
- The approve/deny buttons finalize the participant’s decision.
- If the participant enters the wrong code, the system records the wrong code submission.
- The participant should continue until all ship cases in the condition are completed.

Data Logging:
The project includes an ExperimentLogger script that automatically records experiment data while the participant completes the task. The logger saves data to CSV files. The recorded information includes:

- Participant ID
- Condition name
- Case number
- Ship ID
- Total case completion time
- Keycard time
- Radio time
- Keypad time
- Scan time
- Decision time
- Wrong code submissions
- Final approve/deny decision
- Whether the final decision was correct
- Total condition summary data

The CSV files are saved using Unity’s Application.persistentDataPath. The exact save location depends on whether the project is run through the Unity Editor, on a computer, or directly on the headset.

Troubleshooting:
If the project does not run correctly, check the following:

1. Make sure the Meta Quest headset is connected and active.
2. Make sure Quest Link or Air Link is running if using PC VR.
3. Make sure the correct Unity scene is open before pressing Play.
5. Make sure Unity has finished importing assets before running.
6. Make sure the necessary XR and Meta packages are installed.

Study Purpose:
This project was created as a research prototype for comparing two VR workspace layouts. The goal is to evaluate whether a realistic gravity-based desk layout or a zero-gravity spatial layout better supports multitasking performance, accuracy, and usability in a virtual workspace.



