# Eye Gaze Aware Specification

Jay Beavers, Tolt Technologies, May 2023

## Abstract

This document describes the process by which Microsoft Windows eye gaze aware applications can recognize, cooperate, and interact with each other.  As a novel technology which does not have established interoperability semantics established, additional guidelines and interaction patterns are needed to ensure a cooperative, integrated outcome for users of eye gaze systems who have multiple products from different authors and companies installed on the same system or device.

## Status of This Memo

*Draft Status*, currently under active editing, pending initial distribution for comments and contributions from other design stakeholders.

## Copyright

This document and all associated information and source code in this repository are licensed with the Creative Commons Zero license (CC0 1.0 Universal) which grants extensive public rights to these works, as described in the file [LICENSE](LICENSE).  All contributions to this repository are governed by [CONTRIBUTING](CONTRIBUTING.md) which includes providing contributions under the same terms documented in [LICENSE](LICENSE).

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

Eye gaze is an important alternative control system which allows people who cannot use their hands to interact with their computers and their world.  In many cases dexterity disabilities are co-present with speech disabilities, so one of the more common uses of eye gaze is to control a speech generating device.

Using eye gaze to control a computer comes in two forms: applications that understand eye gaze natively as an input and respond directly to gaze (e.g. eye gaze aware applications) and applications that are controlled via eye gaze emulating some other form of input device, e.g. a keyboard or mouse.  These input emulators commonly take the form of an on screen keyboard which is eye gaze aware and an overlay toolbar that offers mouse-like behavior buttons such as click, drag, and scroll.

When more than one application shares the same computer or tablet system, two interoperability challenges commonly present: sharing the eye gaze camera and overlay windows conflicting with viewing/controlling the application.  Therefore, we need some form of cooperation standard that lets different applications inform and share their eye gaze behavior and expectations with each other.

This specification documents the 'areas of conflict' between eye gaze aware applications and offers an approach to resolve these conflicts through awareness and cooperation.

This specification is *not* about the communications protocol or API for eye gaze cameras.  Each eye gaze camera manufacturer offers their own API/SDK, and outside of a [USB HID Eye Tracker Usage Page](https://usb.org/sites/default/files/hutrr74_-_usage_page_for_head_and_eye_trackers_0.pdf), no hardware/interoperability standards for eye gaze cameras exist today.

### 2. Definitions<a name="definitions"></a>

#### Eye Gaze Aware Application

An Eye Gaze Aware Application integrates one or more eye gaze camera APIs, consumes, and responds directly to eye gaze input.

#### Eye Gaze Shell

An Eye Gaze Shell is a full screen application which is meant to replace the device's overall experience, generally focusing on a simplified experience with larger interaction targets which are easier to use with eye gaze cameras (which are generally less precise than a mouse or touchscreen).

#### Eye Gaze Bar

An Eye Gaze Bar is an overlay and/or docking toolbar which presents mouse emulation buttons (e.g. click, drag, scroll) along with other common functionality shortcut buttons (e.g. show on screen keyboard, quick launch an app, etc.).

#### Eye Gaze Keyboard

An Eye Gaze Keyboard is an on screen keyboard that typically overlays and/or docks to allow a person to input text to another application using eye gaze.

#### URI Invocation

URI invocation is a [technique on Microsoft Windows](https://learn.microsoft.com/en-us/previous-versions/windows/internet-explorer/ie-developer/platform-apis/aa767914(v=vs.85)) where an app is associated with a URL (e.g. https://, eyedrive://, etc.) so that when the URL is opened, the associated application is launched. This is helpful to allow security sandboxed applications, such as [Universal Windows Platform](https://learn.microsoft.com/en-us/windows/uwp/) apps, to interact with each other.

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

#### References



#### Acknowledgements

Initial concepts for eye gaze awareness came out of the work of the Microsoft Research Enable Team & Microsoft Windows Input Team.  Thank you to Tobii-Dynavox for working with Microsoft Research to develop the initial implementations of eye gaze awareness.

#### Contributors

Jay Beavers, 2023

#### Authors' Addresses

Jay Beavers, jay@tolttechnologies.com
