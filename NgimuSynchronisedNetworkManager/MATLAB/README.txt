The following steps describe how to measure the synchronisation error of each device.

1) Connect a 1 Hz square wave to the CTS input of the serial interface on each device.
2) Use the NGIMU Synchronised Network Manager to open connections to all devices and enable a synchronisation master.
3) Begin logging data with a session name of "Synchronisation Test" in the same directory as synchronisationTestScript.m.
3) Adjust the value of masterName in script.m to match that of the synchronisation master.
4) Run the script.  It is not necessary to stop logging first.
