# AR Piano Assistant - Setup Guide

## Overview
This AR piano assistant helps users learn to play keyboard by visualizing MIDI notes as approaching bars on a target plane. The system uses Meta Quest 3 controllers to create anchor points that define the target plane.

## Components

### 1. AnchorSpawner (AnchorScript.cs)
- **Purpose**: Creates a target plane from 4 anchor points
- **Usage**: 
  - In VR: Press the A button (OVRInput.Button.One) to place anchor points
  - In Editor: Automatically creates 4 anchors in a rectangular formation
- **Output**: Creates a plane and triggers the `OnPlaneCreated` event

### 2. MidiNoteBarVisualizer (MidiNoteBarVisualizer.cs)
- **Purpose**: Spawns and animates MIDI note bars that approach the target plane
- **Features**:
  - Supports 88 keys (full piano range)
  - 5-second approach time for visual preparation
  - Color coding for white/black keys
  - Visual feedback as bars approach the target
  - Flash effect when bars reach the target plane

## Setup Instructions

### 1. Scene Setup
1. Add `AnchorSpawner` component to a GameObject in your scene
2. Add `MidiNoteBarVisualizer` component to another GameObject
3. Ensure you have a `MidiFilePlayer` component in the scene with a MIDI file loaded

### 2. Required Prefabs
- **Anchor Prefab**: A simple visual indicator for anchor points
- **Controller Indicator Prefab**: Visual indicator attached to the controller
- **Bar Prefab** (optional): Custom prefab for note bars (uses default cube if not set)

### 3. Configuration

#### AnchorSpawner Settings
- `anchorPrefab`: Visual prefab for anchor points
- `controllerIndicatorPrefab`: Visual indicator for controller

#### MidiNoteBarVisualizer Settings
- **Bar Settings**:
  - `barLength`: Length of note bars (default: 0.2f)
  - `barHeight`: Height of note bars (default: 0.05f)
  - `spawnDistance`: Distance from plane where bars spawn (default: 0.15f)
  - `approachTime`: Seconds before note is played (default: 5.0f)
  - `numberOfLanes`: Number of note lanes (default: 88 for full piano)
  - `centerMidiNote`: Middle MIDI note (default: 60 = middle C)

- **Visual Settings**:
  - `enableColorCoding`: Enable white/black key color coding
  - `enableIntensityFeedback`: Enable brightness changes as bars approach
  - `enableFlashEffect`: Enable flash when reaching target
  - `whiteKeyColor`: Color for white keys
  - `blackKeyColor`: Color for black keys
  - `flashColor`: Color for flash effect

- **Debug**:
  - `showDebugInfo`: Enable debug logging

## Usage

### In VR (Meta Quest 3)
1. Start the application
2. Use the controller to place 4 anchor points by pressing the A button
3. The system will automatically create a target plane
4. MIDI playback will begin and note bars will spawn
5. Watch the bars approach the plane over 5 seconds
6. Play the corresponding notes when bars reach the target plane

### In Editor
1. The system automatically creates 4 anchor points in a rectangular formation
2. MIDI playback begins immediately
3. Note bars spawn and approach the target plane

## Visual Feedback

- **White Keys**: White bars
- **Black Keys**: Black bars
- **Approach Effect**: Bars become brighter as they approach the target
- **Flash Effect**: Bars flash yellow when reaching the target plane

## Tips for Best Experience

1. **Plane Size**: Create a plane that matches your actual keyboard size for accurate positioning
2. **Approach Time**: Adjust `approachTime` based on your playing speed (longer for beginners)
3. **Bar Size**: Adjust `barLength` and `barHeight` for better visibility
4. **Spawn Distance**: Increase `spawnDistance` if bars appear too close initially

## Troubleshooting

- **No MIDI playback**: Ensure `MidiFilePlayer` has a MIDI file loaded
- **Bars not spawning**: Check that `AnchorSpawner` is present and plane is created
- **Poor alignment**: Verify anchor points create a properly sized plane
- **Performance issues**: Reduce `numberOfLanes` or adjust visual effects

## Future Enhancements

- Support for different key sizes (white vs black keys)
- Multiple difficulty levels
- Score tracking
- Haptic feedback
- Multiplayer support 