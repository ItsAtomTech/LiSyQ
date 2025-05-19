# LiSyQ 
## _An Open Source Light Sequencer Project_



![LiSyQ Logo](https://raw.githubusercontent.com/ItsAtomTech/LiSyQ/refs/heads/master/Scriptor/Timeline%20interface/images/logo.png)

The LiSyQ Project is a light sequencing system that runs on Windows and will be released as open-source software. 
It is designed to work with the Arduino Ecosystem to control custom-made light fixtures that are controlled by or connected to an Arduino MCU. It was inspired by the well-known DMX512 light rig controls, but this one comes with its own protocols. Timeline can sync to music, and you can control your Arduino lights with your computer keyboard, among other things.

### There are 3 main folders of these Project

- **Arduino_ino** - this is where all Arduino sketches are placed
- **DesktopApp** - This is where the VB Project is Located
- **Scriptor** - If you are planning to build differnt native environment for the Project, you can use the source code of the web based only source files

## Features

- **Timeline Editor**: Run your sequences with a timeline editor along with an audio/video media sampling player to time your sequences at millisecond precision.

- **Open and Save Light Sequences to A file**: After you have finished composing your timeline, you can save it to a file and be ready to play the sequences at your will.

- **Directly Connect using USB Ports**: Using the USB Ports on your PC, connect the Arduino-based Light Fixtures directly (daisy Chaining is supported!). Check out the tutorial on how this is done.

### Other Features:

- **Live Templates Player**: If you need to manually control fixtures, you can use this feature, which lets you configure hotkeys to control them with.




## Tech

The LiSyQ Project is using:

- Javascript - we are using the vanilla flavor.
- HTML5 and CSS - we have written every single bit in pure code from this, not bloated, web standards.
- MicrosoftEdge Webview2 - A Microsoft chromium based web engine  
- [Viselect](https://simonwep.github.io/selection) - A high performance and lightweight library to add a visual way of selecting elements, just like on your Desktop. We have used it for the Drag Select feature on the Timeline.
- _we are still documenting them so keep up!_




## Installation

##### For Non-Coding users 
- You can download the Installer at this link:  [Release](https://github.com/ItsAtomTech/LiSyQ/releases) 

Install the dependencies before running, we included the last version used to build the Project, only install dependencies if running Win 7 or 8.1, dependencies are already baked in to Win10 and 11 Clean Install, but if that's not the case we got you covered. Links would be updated as well.

- Dependencies Installer: [Mediafire Folder]
(https://www.mediafire.com/folder/ecxlt9b815f7u/Dependencies)

##### For Coders and Devs

A Project Build is under this directories:
  
     DesktopApp\Lisyq\Lisyq\bin\Release

Open the VB Project Solution at

    DesktopApp\Lisyq\Lisyq\Lisyq.sln

The Arduino Sketches are also provided in the repositories.

_We are writing more..._