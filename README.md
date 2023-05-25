# Gaze Aware Specification

Jay Beavers, Tolt Technologies, May 2023

## Abstract

This document describes the process by which Microsoft Windows eye gaze aware applications can recognize, cooperate, and interact with each other.  As a novel technology which does not have established interoperability semantics established, additional guidelines and interaction patterns are needed to ensure a cooperative, integrated outcome for users of eye gaze systems who have multiple products from different authors and companies installed on the same system or device.

## Status of This Memo

## Copyright

This document and all associated information and source code in this repository are licensed with the Creative Commons Zero license (CC0 1.0 Universal) which grants extensive public rights to these works, as described in the file [LICENSE](LICENSE).  All contributions to this repository are governed by [CONTRIBUTING](CONTRIBUTING.md) which includes releasing these contributions under the same terms documented in [LICENSE}(LICENSE).

## Table of Contents

1. [Introduction](#introduction)
2. [Definitions](#definitions)
3. [Marking an application as Gaze Aware](#marking-gaze-aware)
4. [Detecting a Gaze Aware application](#detecting-gaze-aware)
   - [Tobii-Dynavox Variation](#tobii-dynavox-variation)
5. [Discovery of Installed Gaze Aware Applications](#discovery)
6. [Starting Gaze Aware Applications](#starting)
7. [Advanced Scenarios: Calibration, Positioning, and Gaze Bar/Keyboard control](#advanced)
8. [Example Implementations](#example-implementations)
9. [Gaze Aware Products on the Market](#gaze-aware-products)

### 1. Introduction<a name="introduction"></a>

### 2. Definitions<a name="definitions"></a>

 - Eye Gaze Aware Application
 - Eye Gaze Shell
 - Eye Gaze Bar
 - Eye Gaze Keyboard
 - URI Invocation

### 3. Marking an application as Gaze Aware<a name="marking-gaze-aware"></a>

 - publish appuri on window and in registry

### 4. Detecting a Gaze Aware application<a name="detecting-gaze-aware"></a>

#### Tobii-Dynavox Variation<a name="tobii-dynavox-variation"></a>

### 5. Discovery of Installed Gaze Aware Applications<a name="discovery"></a>

HKCU & HKLM registry entries

### 6. Starting Gaze Aware Applications<a name="starting"></a>

URI Invocation

 - appuri:start
 - appuri:end

### 7. Advanced Scenarios: Calibration, Positioning, and Gaze Bar/Keyboard control<a name="advanced"></a>

Discovery of supported uris via registry entry

#### URI Invocation

Controlling the eye gaze system

 - eyegaze:startcalibration
 - eyegaze:endcalibration
 
 - eyegaze:showpositioning
 - eyegaze:hidepositioning
 
 - eyegaze:showgazebar
 - eyegaze:showgazebartop
 - eyegaze:showgazebarbottom
 - eyegaze:showgazebarright
 - eyegaze:showgazebarleft
 - eyegaze:hidegazebar
 
 - eyegaze:showkeyboard
 - eyegaze:showkeyboardtop
 - eyegaze:showkeyboardbottom
 - eyegaze:hidekeyboard

Status Notifications from the eye gaze system

 - appuri:calibrationstarted
 - appuri:calibrationended
 
 - appuri:positioningshowing
 - appuri:positioninghidden
 
 - appuri:gazebartop
 - appuri:gazebarbottom
 - appuri:gazebarright
 - appuri:gazebarleft
 - appuri:gazebarhidden
 
 - appuri:keyboardtop
 - appuri:keyboardbottom
 - appuri:keyboardhidden

### 8. Example Implementations<a name="example-implementations"></a>

ToltTech.Integration.GazeAware library
FindGazeAware sample app
MarkedGazeAware sample app
"Gaze Aware Calculator" (e.g. sample registry entries to interact with Windows Calculator)

### 9. Gaze Aware Products on the Market<a name="gaze-aware-products"></a>

See [gaze-aware-products.md](gaze-aware-products.md) for a list of known products on the market which implement some or all of this specification.

#### Acknowledgements

Initial concepts for eye gaze awareness came out of the work of the Microsoft Research Enable Team & Microsoft Windows Input Team.

#### Contributors

Jay Beavers, 2023

#### Authors' Addresses

Jay Beavers, jay@tolttechnologies.com
