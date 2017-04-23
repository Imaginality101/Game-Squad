Before playing the game, run the external tool. One way is by opening the project in Visual Studio and setting ExternalTool to 
StartUp Project. Check the checkboxes you want, then press "Run Game!". This saves the data to the file.
If you have startup issues because the test computer doesn't have OpenAL, there should be an installer for it in here. The sound uses OpenAL and it explodes if those drivers
are missing.


----------EXTERNAL TOOL-------------

Our tool is an external settings application which applies its settings to the game. Not every piece of information it saves
is used by the game itself meaningfully yet (such as easy mode), but the tool itself is complete on that front.

If selecting timed mode (which enables a lose state ingame if you run out of time), it's necessary to put in a value. Same thing with
custom resolutions.

Using it is pretty straightforward, it's just point-and-click. Just check the boxes for settings you want on and hit "save settings" to
generate the file. The game will take care of the rest, no need to move the file anywhere.
---------GAME INFORMATION-----------
Basic Movement and Interaction
---------------------------
Move with the W,A,S,D keys. When close enough to an Interactable object to use it,
you'll see a prompt appear above the character's head and the object will highlight.
Press E to interact with objects in this manner.
---------------------------
Menu Usage
---------------------------
Press TAB to open the menu. This will bring you to a main phone screen.
Pressing TAB at any time in the menu will bring you back to this screen.
Press 2 on the keyboard to view the inventory of clues you've picked up.
- WASD will let you select from this list, keep going down to scroll pages.
- Press ENTER to select and open the highlighted clue.
	- On text clues, this will display its text to a box. To exit, close the menu or press TAB.
	- On image clues, press ENTER again to close the clue.
Press 3 on the keyboard to view a display of the currently selected settings, not implemented yet but this will show things like your resolution.
Pressing 4 will save and close the game.

To exit the menu at any time, press LSHIFT.
--------------------------
GENERAL TIPS
--------------------------
To reach our win state currently requires you to take the door to the bathroom, in the top right of the bedroom.
To get the bathroom key, you need to get into the closet. This can either be reached
by getting its key from exploring the bedroom (if a highlighted object does nothing you don't have the key or required clues for it)
and then taking the door at the bottom of the left wall, or taking the shortcut door we've implemented for this milestone's development purposes
at the top of the left wall immediately.



