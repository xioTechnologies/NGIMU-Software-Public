NGIMU-Software
==============

v1.17 (May 03 2020)
-------------------
* Update firmware version

v1.15 (Aug 25 2020)
-------------------
* Update firmware version

v1.14 (Mar 27 2019)
-------------------
* Update firmware version

v1.13 (Dec 19 2018)
-------------------
* Fix bug that caused constant warnings about Synchronisation master
* Remove invalid 5 GHz channels from Wi-Fi settings
* Fix graph centring issue when paused in horizontal auto-scale mode
* Fix bug affecting conversion of rotation matrix to quaternion

v1.12 (May 18 2018)
-------------------
* Redesign wireless settings configuration tool in Synchronised Network Manager
* Change 3D model colour to match actual product
* Auto-populate the firmware uploader hex file path
* Firmware uploading will retry 3 times
* Do not create a Settings.txt file if it will be empty
* Disable command and settings read/write controls when Synchronised Network Manager not connected
* Modify some error messages to be more meaningful
* Remove processor temperature
* Add help text to send rates window in Synchronised Network Manager
* Fix bug caused duplicated text when editing device name in Synchronised Network Manager

v1.11 (Jan 10 2018)
-------------------
* Allow device name to be edited in NGIMU Synchronised Network Manager
* Fix bug affecting SLIP packets greater than 1024 bytes
* Fix message filter tab bug

v1.10 (Dec 29 2017)
-------------------
* Increase ahoy service expiry to 5 seconds to handle Wi-Fi send delays of firmware
* Rename NGIMU GUI column title "Address" menu to "OSC Address"
* Rename NGIMU GUI "Command" menu to "Send Command"
* NGIMU Synchronised Network Manager displays communication errors an icon in the device column
* NGIMU Synchronised Network Manager displays received error messages an icon in the device column

v1.9 (Dec 01 2017)
------------------
* CSV files represent infinity and NaN as zero
* Battery CSV files includes "Is Charger Connected" column
* Data Forwarding to local host is enabled by default
* Fix issue with Data Forwarding radio buttons
* NGIMU Synchronised Network Manager displays firmware version warning an icon in the device column
* Adjust GUI default options
* Handle exception when serial hardware flow control permanently inhibit sending
* Graphs represent infinity and NaN as zero
* Fix bug that meant Data Forwarding on local host may not work

v1.8 (Oct 30 2017)
------------------
* Rename "Load defaults" to "Restore defaults"
* Disable commands in NGIMU GUI when not connected
* Add commands to start/stop logging to SD card
* Graphs always in roll mode
* Graph vertical autoscale now applies to visible data only
* Redesign graph context menu and keyboard/mouse controls
* Fix bug that meant graph window position would be forgotten if any part was off screen
* Fix bug that meant extremely high values could cause the graph to crash

v1.7 (Sep 19 2017)
------------------
* Fix bug in API affecting SD card file conversion
* Add send rate information to settings documentation

v1.6 (May 19 2017)
------------------
* Fix bug in NGIMU GUI that could result in messages being permanently muted
* Synchronised Network Manager displays error message if a rogue synchronisation master is detected
* Add Synchronised Network Manager tool for forwarding data

v1.5 (May 03 2017)
------------------
* Add tool to configure wireless settings via USB in Synchronised Network Manager
* Improvements to Send Rate dialogue window in Synchronised Network Manager
* Disable Cancel button when disconnecting with Synchronised Network Manager

v1.4 (Mar 06 2017)
------------------
* Add setting for external power wakeup
* Fix bug that prevented the About dialogue from closing first time
* Redesign send rates dialogue in Synchronised Network Manager
* Add setting for USB/external power wakeup

v1.3 (Feb 14 2017)
------------------
* Introduce Synchronised Network Manager application
* Introduce SD Card File Converter application
* Update expected firmware version
* Update 3D models for latest hardware
* Add network latency setting for synchronisation mater
* Esc key can be used to close Received Error dialogue
* Ensure data logger stopwatch final value matches logging time if specified
* Refactor Data Logger for improved behaviour when device information unknown
* Add keyboard shortcut to clear graph
* Ignore negative timestamps when writing CSV files
* Do not create CSV file for error messages
* Rename Thermometers graph to temperature
* Rename ConnectionSearchInfo to ConnectionSearchResult in API
* Make quaternions IEquatable in API

v1.2 (Nov 20 2016)
------------------
* Abandon use of compression tool which may be flagged by security software
* Add 'Open Directory' button to data logger
* Change names of thermometers
* Graph settings are saved and restored
* Remove unused methods and operators from Quaternion.cs
* Add About dialogue
* Add check for compatible firmware version
* Fix bug that caused unhandled exception when application closed during communication process
* Fix bug in API that failed to display correct result of communication process

v1.1 (Oct 19 2016)
------------------
* Add setting for wake up time
* Fix bug in search for connections that affecting non Windows platforms
* FIx bug that could causes the zero line to flicker in graphs
* Fix bug affecting SD card file conversions

v1.0 (Sep 23 2016)
------------------
* Add SD card settings
* Fix bug that cause the 3D view to display some rotation matrix values as NaNs
* Fix some bugs that could cause the graphs to crash
* Change default 3D model is the NGIMU housing
* Fix bug that meant the displayed timestamp could be incorrect
* RSSI OSC message now includes percentage
* GUI displays error message is command cannot be confirmed
* Add option for graph buffer size
* Redesign graph view controls

v0.13 (Jul 24 2016)
-------------------
* Adjust axes directions of 3D models to match PCB v1.3
* Rename "listen" to "receive" in UDP port setting
* Data logger now logs unknown OSC messages to CSV files
* Add commands for AHRS initialise and AHRS zero yaw
* Add setting for muted on startup
* Introduce custom 2D graphing library
* Improve file/directory path selection form control
* Fix bug that could result in commands blocking forever

v0.12 (Jun 07 2016)
-------------------
* Renamed application
* Received error messages displayed in custom form instead of messages boxes
* Add setting and graph for altitude data

v0.11 (May 19 2016)
-------------------
* Add setting for gyroscope bias correction
* Search for connections runs quicker and smoother
* Add setting for magnitudes message send rate
* Add setting for RSSI message send rate

v0.10 (Apr 26 2016)
-------------------
* Search for devices will expire connections if they disappear
* Fix bug with 3D view that could cause crash when computer sleeps
* GUI tools renamed for consistency
* Complete command/setting process once confirmations received instead of waiting for timeout
* Fix bug when 3D view window is reduced to minimise size
* Change behaviour of mute/solo all check boxes
* Allow Euler angles to be hidden in 3D view

v0.9 (Mar 23 2016)
------------------
* Allow Euler angles to be hidden in 3D view
* Do not load read-only settings from file
* Fix bug that meant battery time displayed in GUI may be incorrect
* Add compass image to 3D view
* Reorganise options window
* "Auto Connect" renamed to "Search For Connections"
* Numerous minor bug/stability fixes
* Add setting for LEDs enabled
* Add setting for CPU idle mode
* Fix but that meant some computers may use a comma decimal place for timestamps
* Auto connector searches all communication channels in parallel
* Add setting for Wi-Fi region
* Improve error messages when using data logger
* Apply command sent after settings written

v0.8 (Feb 18 2016)
------------------
* Fix bug with default adapter address
* Revise the process for configuring a unique UDP connection
* Add checkbox for broadcast IP address in UDP connection dialogue
* Add low-power Wi-Fi setting
* Mute and unmute commands are no longer sent when reading or writing settings
* Add camera controls and Earth coordinate frame to 3D view
* Standard date format used in logged XML files
* Fix bug that may occur when Windows enumerates Bluetooth serial ports

v0.7 (Feb 04 2016)
------------------
* Invalid file name characters are replaced with underscores when saving files
* Fix bug that meant Windows would stay on top of main Window
* Use default file name when saving settings
* New 3D view window

v0.6 (Jan 14 2016)
------------------
* Add context menu to copy setting documentation text from GUI
* Settings can be read automatically after connection opened
* Wi-Fi connections renamed to UDP
* Data logger and SD card file converter redesigned
* Device send IP and send port are changed after auto connect to ensure a unique connection
* Add mute/solo all checkboxes to column headers
* Fix bugs in SD card file converter tool

v0.5 (Oct 09 2015)
------------------
* Calibration settings are not read/written by GUI
* Auxiliary serial data logged as either hex or string
* New file/directory organisation for logged data
* Fix issue that means Convert SD Card File window state would not be remembered

v0.4 (Sep 18 2015)
------------------
* Add SD card file converter tool
* Fix render bug affecting auxiliary serial terminal RTS/CTS controls
* Add calibration parameters to settings in API
* Esc key can be used to cancel progress dialogs.

v0.3 (Jul 31 2015)
------------------
* Save/load settings to/from file
* Auxiliary terminal allows manual control of RTS/CTS and displays byte throughput
* Fix bug that meant drop down list of sent messages could be in red text
* All projects in the solution use the same version number
* Loading default GUI options no longer closes the options form

v0.2 (Jul 03 2015)
------------------
* Prevent undefined settings from being written to device
* Removed non-UTF8 characters from CSV files
* Fixed bug that meant file paths would not be accepted in the axillary serial terminal
* Dragging and dropping file into terminal copies file path to text box
* Serial port list is ordered
* Refactor settings for improved behaviour
* Change "General" "settings category name to "Device Information"

v0.1 (Jun 17 2015)
------------------
* Migrate to Visual Studio Community 2013
* Auxiliary serial terminal has black background
* Improved auto connect dialog
* Reorder of Wi-Fi mode enumeration
* Remove Wi-Fi security setting
* Refactor process for sending commands

v0.0 (Jun 08 2015)
------------------
* Initial release
