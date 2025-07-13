# Project Description

The AR Piano is an augmented reality application built for the Meta Quest 3 that helps users learn the piano in an immersive and intuitive way. 
The app overlays real-time visual guidance onto a physical piano using spatial mapping and augmented elements. It includes features like a virtual 
note guide aligned with the physical keys, a song selection interface, and visual MIDI note bars to aid in playing songs. The aim is to make learning 
piano more accessible and engaging, especially for beginners, by combining AR with interactive music training.


# How to run the code:
1) Use Unity Version 6000.0.37f1 install with all the android packages
2) Install these Unity Packages: 
	* Maestro - Midi Player Tool Kit - Free
	* Meta XR Core SDK
	* Meta XR All-in-One SDK
	* Oculus Integration (Deprecated)
3) If the sample scene is not visible in the hierarchy, drag and drop the "SampleScene" from Projects within Assets>Scenes>SampleScene
4) Edit>Project Settings>XR Plug in Management select Oculus
5) Build and Run


# Steps to Access and Use the Application 

1. Calibrate the space: Press and hold the Meta button on the controller to set up world coordinates in your real environment.
2. Place 4 spatial anchors around the 4 edges of the piano. This ensures everything stays aligned even if you move around.
3. A reference plane gets generated from the placed anchors. This is the surface where virtual notes will appear aligned with your physical piano.
4. A virtual grid also comes up that spans the width of 88 piano keys, aligning the virtual notes with your real piano.
5. Browse songs using the song menu on the right-hand side. Select a song with the trigger button.
6. Follow the MIDI bars that show incoming notes up to five seconds ahead, helping you anticipate and play them in real time.
7. Switch songs anytime using the same interface and continue learning with visual assistance.
8. Use color cues: light blue for white keys and dark blue for black keys to match notes intuitively with keys.
9. Recalibrate anytime by pressing the reset button to reset the plane by once again placing the 4 spatial anchors.

