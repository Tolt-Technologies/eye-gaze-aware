# Gaze Aware Specification

Jay Beavers, Tolt Technologies, May 2023

## Abstract

This document describes the process by which Microsoft Windows eye gaze aware applications can recognize, cooperate, and interact with each other.  As a novel technology which does not have established interoperability semantics established, additional guidelines and interaction patterns are needed to ensure a cooperative, integrated outcome for users of eye gaze systems who have multiple products from different authors and companies installed on the same system or device.

## Status of This Memo

## Copyright

This document and all associated information and source code in this repository are licensed with the Creative Commons Zero license (CC0 1.0 Universal) which grants extensive public rights to these works, as described in the file [LICENSE](LICENSE).  All contributions to this repository are governed by [CONTRIBUTING](CONTRIBUTING.md) which includes releasing these contributions under the same terms documented in [LICENSE}(LICENSE).

## Table of Contents

1. [Introduction](#introduction)
2. [Marking an application as Gaze Aware](#marking-gaze-aware)
3. [Detecting a Gaze Aware application](#detecting-gaze-aware)
   - [Tobii-Dynavox Variation](#tobii-dynavox-variation)
4. [Discovery of Installed Gaze Aware Applications](#discovery)
5. [Starting Gaze Aware Applications](#starting)
6. [Advanced Scenarios: Calibration, Positioning, and Gaze Bar/Keyboard control](#advanced)
7. [Example Implementations](#example-implementations)
8. [Gaze Aware Products on the Market](#gaze-aware-products)

### 1. Introduction<a name="introduction"></a>

### 2. Marking an application as Gaze Aware<a name="marking-gaze-aware"></a>

### 3. Detecting a Gaze Aware application<a name="detecting-gaze-aware"></a>

#### Tobii-Dynavox Variation<a name="tobii-dynavox-variation"></a>

### 4. Discovery of Installed Gaze Aware Applications<a name="discovery"></a>

HKCU & HKLM registry entries

### 5. Starting Gaze Aware Applications<a name="starting"></a>

URI Invocation

### 6. Advanced Scenarios: Calibration, Positioning, and Gaze Bar/Keyboard control<a name="advanced"></a>

Discovery via registry entry
URI Invocation (eyegazecalibrate://)

### 7. Example Implementations<a name="example-implementations"></a>

ToltTech.Integration.GazeAware library
FindGazeAware sample app
MarkedGazeAware sample app

### 8. Gaze Aware Products on the Market<a name="gaze-aware-products"></a>

See [gaze-aware-products.md](gaze-aware-products.md) for a list of known products on the market which implement some or all of this specification.

#### Acknowledgements

Initial concepts for eye gaze awareness came out of the work of the Microsoft Research Enable Team & Microsoft Windows Input Team, specifically influenced by
team members Harish Kulkarni, Jay Beavers, and Eric Badger.

#### Contributors

Jay Beavers, 2023

#### Authors' Addresses

Jay Beavers, jay@tolttechnologies.com
