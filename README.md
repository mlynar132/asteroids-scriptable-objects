**New editor for thorttle and rotation**

Added ShipInfo.cs, ShipInfoUIController.cs, UI for ship info and added public seter for FloatVariable.

ShipInfo is a scriptable object that uses ShipInfoUIController for overloading the editor to display custom UI.
UI consists of 2 sliders one for thorttle and one for rotation.
Controller script loads the refferences for the FloatVariables responsible for Throttle and speed and then on registering a change in the custom UI applies it to the original Scriptable Objects.

this solution extends current project with a UI that can have multiple variables in one place without a need to change the main code and refferences (besieds adding a set function for FloatVariable)
