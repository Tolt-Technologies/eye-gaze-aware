# Gaze Aware Specification

Jay Beavers, Tolt Technologies, May 2023

## Abstract

This document describes the process by which Microsoft Windows eye gaze aware applications can recognize, cooperate, and interact with each other.  As a novel technology which does not have established interoperability semantics, guidelines and interaction patterns are needed to ensure an integrated outcome for users of eye gaze systems who have multiple products from different authors and companies installed on the same device.

## Status of This Memo

*Draft Status*, currently under active editing, pending initial distribution for comments and contributions from other design stakeholders.

## Copyright

This document and all associated information and source code in this repository are licensed with the Creative Commons Zero license (CC0 1.0 Universal) which grants extensive public rights to these works, as described in the file [LICENSE](LICENSE).  All contributions to this repository are governed by [CONTRIBUTING](CONTRIBUTING.md) which includes providing contributions under the same terms documented in [LICENSE](LICENSE).

## Table of Contents

1. [Introduction](#introduction)
2. [Definitions](#definitions)
3. [Marking an application as Gaze Aware](#marking-gaze-aware)
4. [Detecting a running Gaze Aware application](#detecting-gaze-aware)
   - [Tobii-Dynavox Variation](#tobii-dynavox-variation)
5. [Discovery of Installed Gaze Aware Applications](#discovery)
6. [Starting Gaze Aware Applications](#starting)
7. [Advanced Scenarios: Calibration, Positioning, and Gaze Bar/Keyboard control](#advanced)
8. [Example Implementations](#example-implementations)
9. [Gaze Aware Products on the Market](#gaze-aware-products)

### 1. Introduction<a name="introduction"></a>

Eye gaze is an important alternative control system which allows people who cannot use their hands to interact with their computers and their world.  In many cases dexterity disabilities are co-present with speech disabilities, so one of the more common uses of eye gaze is to control a speech generating device.

Using eye gaze to control a computer comes in two forms: applications that understand eye gaze natively as an input and respond directly to gaze (e.g. gaze aware applications) and applications that are controlled via eye gaze emulating some other form of input device, e.g. a keyboard or mouse.  These input emulators commonly take the form of a gaze aware on screen keyboard and overlay toolbar that offers buttons to perform mouse-like interactions such as click, drag, and scroll.

When more than one application shares the same computer or tablet system, two interoperability challenges commonly present: sharing the eye gaze camera and overlay windows (keyboard and toolbar) obscuring the application.  Therefore, we need some form of cooperation standard that lets different applications inform and share their eye gaze behavior and expectations with each other.

This specification documents the *areas of conflict* between eye gaze aware applications and offers an approach to resolve these conflicts through awareness and cooperation.

This specification is *not* about the communications protocol or API for eye gaze cameras.  Each eye gaze camera manufacturer offers their own API/SDK, and outside of a [USB HID Eye Tracker Usage Page](https://usb.org/sites/default/files/hutrr74_-_usage_page_for_head_and_eye_trackers_0.pdf), no hardware/interoperability standards for eye gaze cameras exist today.

### 2. Definitions<a name="definitions"></a>

#### Gaze Aware Application

A Gaze Aware Application integrates one or more eye gaze camera APIs, consumes, and responds directly to eye gaze input.

#### Gaze Shell

A Gaze Shell is a full screen application which is meant to replace the device's overall experience, generally focusing on a simplified experience with larger interaction targets which are easier to use with eye gaze cameras (which are generally less precise than a mouse or touchscreen).

#### Gaze Bar

A Gaze Bar is an overlay and/or docking toolbar which presents mouse emulation buttons (e.g. click, drag, scroll) along with other common functionality shortcut buttons (e.g. show on screen keyboard, quick launch an app, etc.).

#### Gaze Keyboard

An Eye Gaze Keyboard is an on screen keyboard that typically overlays and/or docks to allow a person to input text to another application using eye gaze.

#### URI Invocation

URI invocation is a [technique on Microsoft Windows](https://learn.microsoft.com/en-us/previous-versions/windows/internet-explorer/ie-developer/platform-apis/aa767914(v=vs.85)) where an app is associated with a URL (e.g. https://, eyedrive://, etc.) so that when the URL is opened, the associated application is launched. This is helpful to allow security sandboxed applications, such as [Universal Windows Platform](https://learn.microsoft.com/en-us/windows/uwp/) apps, to interact with each other.

### 3. Marking an application as Gaze Aware<a name="marking-gaze-aware"></a>

A Windows application is marked as Gaze Aware in two ways: at runtime via a [Window Property](https://learn.microsoft.com/en-us/windows/win32/winmsg/using-window-properties) and in the registry to add itself to a 'directory' of installed Gaze Aware applications.

#### Gaze Aware Window Property

The Window Property is used to inform Gaze Shells, Bars, and Keyboards that the running application is Gaze Aware and so they should release the eye gaze camera (if it cannot be shared between multiple processes/applications) and they should hide/minimize overlay windows so as not to occlude the Gaze Aware application that does not need their input emulation services.

This window property is set via User32 SetProp with string "eyegaze:aware", e.g.:

``` C#
   [DllImport("user32.dll", SetLastError = true)]
   public static extern bool SetProp(IntPtr hWnd, string lpString, IntPtr hData);

   private const string EyeGazeAware = "eyegaze:aware";
   private static readonly IntPtr pStr = Marshal.StringToHGlobalUni(EyeGazeAware);

   var hwnd = new WindowInteropHelper(window).Handle;
   User32.SetProp(hwnd, EyeGazeAware, pStr)
```

> Note: Should we set the pStr to the URL Protocol (e.g. "eyedrive") so when a gaze aware app is detected by the gaze shell, it knows the uri to send notifications to per section 7?

See [examples](examples) for complete sample code in C#.

#### Gaze Aware Registry Entries (e.g. Application Directory)

The Gaze Aware registry entries are used to publish the fact that the Gaze Aware application is installed on this computer.  This can be used to facilitate discovery of installed applications so that eye gaze shells can find, list, and launch available apps.

Gaze Aware apps should list themselves in the registry under \Software\Classes\eyegaze\applications.  This can be under HKEY_LOCAL_MACHINE for machine-wide installations or under HKEY_CURRENT_USER for user-scope installed apps (e.g. does not need administrator permissions).  Gaze Shells should check both HKLM and HKCU registry keys to find all installed gaze aware applications.  This registry entry should contain the application's Name, Path, Icon, Description, and URL Protocol.

For example, the Wix code to register Ability Drive as an installed eyegaze application is:

``` wix
<RegistryKey Root='HKCU' Key='Software\Classes\eyegaze\applications'>
   <RegistryKey Key='$(var.ProductName)'>
      <RegistryValue Name='URL Protocol' Value='eyedrive' Type='string' />
      <RegistryValue Name='Icon' Value='$(var.ExeTargetDir)Icons\mark.ico' Type='string' />
      <RegistryValue Name='Path' Value='[#Application.exe]' Type='string' />
      <RegistryValue Name='Name' Value='$(var.ProductName)' Type='string' />
      <RegistryValue Name='Description' Value='$(var.Description)' Type='string' />
   </RegistryKey>
</RegistryKey>
```

The application should also register itself so it can be launched via URI invocation.  This enables Modern Windows Applications (e.g. packaged Windows Store apps) to be able to find and launch the application.

For example, the Wix code to register a protocol handler for Ability Drive to launch using "eyedrive:" is:

``` wix
<RegistryKey Root='HKCU' Key='Software\Classes\eyedrive'>
   <RegistryValue Value='URL:Eye Drive' Type='string' />
   <RegistryValue Name='URL Protocol' Value='' Type='string' />
   <RegistryKey Key='shell\open\command'>
      <RegistryValue Value='[#Application.exe]' Type='string' />
   </RegistryKey>
</RegistryKey>
```

### 4. Detecting a running Gaze Aware application<a name="detecting-gaze-aware"></a>

Gaze Shells, Bars, and Keyboards can detect a running Gaze Aware application by searching for the appropriate Window Property on the foreground window:

``` C#
public static bool IsGazeAware()
{
   var foregroundWindowHwnd = User32.GetForegroundWindow();
   if (foregroundWindowHwnd == IntPtr.Zero)
   {
         // The foreground window can be NULL in certain circumstances, such as when a window is losing activation.
         return false;
   }

   var eyeGazeAware = User32.GetProp(foregroundWindowHwnd, EyeGazeAware) != IntPtr.Zero;

   return eyeGazeAware;
}
```

#### Tobii-Dynavox Variation<a name="tobii-dynavox-variation"></a>

Tobii-Dynavox uses a slight variation on this Window Property:

``` C#
private const uint WS_TOBIIGAZEAWARE = 0x2;
var tobiiGazeAware = (User32.GetWindowLong(foregroundWindowHwnd, User32.GWL_EXSTYLE) & WS_TOBIIGAZEAWARE) == WS_TOBIIGAZEAWARE;
```

This is needed for interoperability with TD Communicator 5 and TD Control, but is not generally recommended because it assumes that this bit of Window Style is not being used for another purpose, and this may conflict with other uses of the Window Style flag.

### 5. Discovery of Installed Gaze Aware Applications<a name="discovery"></a>

See section on Gaze Aware Registry Entries above (e.g. `Software\Classes\eyegaze\applications\...`)

### 6. Starting Gaze Aware Applications<a name="starting"></a>

URI Invocation allows starting a gaze aware app via a similar method to opening a URL to a website.  For example, instead of opening `https://google.com`, open `eyedrive:` to launch Ability Drive.

 - appuri:start
 - appuri:end

### 7. Advanced Scenarios: Calibration, Positioning, and Gaze Bar/Keyboard control<a name="advanced"></a>

Discovery of supported uris via registry entry

#### URI Invocation

##### Controlling the eye gaze system

These are suggested URIs that allow a gaze aware app to interact with the gaze shell for a better overall customer experience.

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

##### Status Notifications from the eye gaze system

These are 

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

```
ToltTech.Integration.GazeAware library
FindGazeAware sample app
MarkedGazeAware sample app
"Gaze Aware Calculator" (e.g. sample registry entries to interact with Windows Calculator)
```

### 9. Gaze Aware Products on the Market<a name="gaze-aware-products"></a>

See [gaze-aware-products.md](gaze-aware-products.md) for a list of known products on the market which implement some or all of this specification.

#### References

#### Acknowledgements

Initial concepts for eye gaze awareness came out of the work of the Microsoft Research Enable Team & Microsoft Windows Input Team.  Thank you to Tobii-Dynavox for working with Microsoft Research to develop the initial implementations of eye gaze awareness.

#### Contributors

Jay Beavers, 2023

#### Authors' Addresses

Jay Beavers, jay@tolttechnologies.com
