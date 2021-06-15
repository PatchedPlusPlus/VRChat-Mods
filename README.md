## PortableMirror
This mod allows the user to locally spawn mirrors for themselves in any VRChat world.

The mirror can be configured:
  * Allow/disallow mirror pickup
  * Toggle between full/optimized/cutout/transparent mirrors
  * Configurable mirror size and distance from you
  * Standard, 45 degree, ceiling, small and transparent mirrors
  * [UIX](https://github.com/knah/VRCMods/releases/latest/download/UIExpansionKit.dll) menu to control settings  
  * Show/Hide spawned mirrors in Camera photos
  
### Screenshots
__Quick Menu buttons can be enabled/disabled individually with the "Enable [X] Mirror QM Button" toggles in Mod Settings__   

![image](https://user-images.githubusercontent.com/81605232/113796149-2cb2fd80-9714-11eb-8c25-a340b6f2e849.png)

__Portable Mirror Settings__   

![image](https://user-images.githubusercontent.com/81605232/121982852-f3bb8880-cd55-11eb-90a2-f7d06b5ff528.png)

__Transparent and Cutout Mirror Examples__    
  
![image](https://user-images.githubusercontent.com/81605232/115629313-bbed1300-a2c7-11eb-83f9-dc6888e94256.png)  
__Action Menu__  
![image](https://user-images.githubusercontent.com/81605232/121983290-b3a8d580-cd56-11eb-8008-92b6abddc8ca.png)  
You need the latest ActionMenuApi.dll from https://github.com/gompocp/ActionMenuApi/releases Put it in your mods folder for this feature to work.    


### Changelog
* v1.5.0
	* Added buttons for 'Position based on View' and 'Anchor to World/Tracking' to page 2 of the QuickMenu Settings. 
	* Code Cleanup & Added Loader Integrity Check
* v1.4.8
	* Per request added a 'Grab Distance +/-' that changes the box collider size on mirrors. - Only in AMAPI so far
	* Added a toggle for 'Pickups snap to hand' _- Only in AMAPI so far_
	* Addded "Position & Rotation from View" this places the mirror based on the angle you are looking _- Only in AMAPI so far_
	* "Mirror follows you" mirror follows your local tracking space instead of being locked to the world. _- Only in AMAPI so far_
	* Added Solo (local player only) Cutout and Transparent Mirrors. Useful if you just want to see yourself for positioning
* v1.4.7
	* Added a 'High Precision' mode for adjusting the mirror distance in the menus. It is on the bottom right of page 1 for the QM. And in the extras menu of the Action Menu. The High Precision value is adjustable in Mod Settings
	* Code cleanup, much in part to Davi
* v1.4.6
	* Added a toggle to disable keybinds for spawning the Portable Mirror
	* Fixed a bug where using a cutout or transparent mirror would cause a NRE in certain worlds
* v1.4.5
	* Changed around Icons to make it more clear what menu you are in. The enabled buttons now match the icon for the Submenu
* v1.4.4
	* Added support for [ActionMenuApi](https://github.com/gompocp/ActionMenuApi) this is an **optional feature** that puts the controls for the mirror on your radial menu. 
* v1.4.3
  * Changed how the default state of the QuickMenu buttons gets set as some other mods may delay UIX from decorating the menus for a while
* v1.4.2
  * Minor Adjustments to Menus and wording
  * Added an option to remember what QuickMenu page was open last
  * Fixed Transparent Mirror not keeping it's Y position when scaling
  * Fixed the transparent/cutout mirror not probably setting the layer exclusions in SDK3 worlds
* v1.4.0
  * Changed all mirrors to be togglable between Full/Optimized/Cutout/Transparent
    * Left the Transparent mirror in the mod, it can be toggled like the others, but defaults to transparent every game load
  * Changed how we handle other mirrors so now we only exclude layer 19 from their reflection mask if the portable mirror is Cutout or Transparent, not changing their masks completely
  * Added an option to allow the Portable Mirrors to show in cameras
  * Cleaned up code and minor bug fixes
    * Tweaked 45 mirror's height math
* v1.3.0
	* Added a Transparent Mirror
		* When enabled the Transparent Mirror will force all mirrors to Optimized or Full, this is configurable in settings. 
		* This is using [VRCPlayersOnlyMirror](https://github.com/acertainbluecat/VRCPlayersOnlyMirror)
	* Added size controls for all mirrors (Page 2 of Mirror Settings)
	* Pickup range for MicroMirror is now Configurable, defaults to .1f
	* Mirror buttons in Settings are now toggles so you can tell what is enabled
* v1.2.8
	* Now can adjust the distance of the mirror live with UIX menu
	* The options for disabling and enabling the separate mirrors should update live instead of needing a restart 

### Known Bugs
* Enabling a world mirror after a cutout or transparent mirror exists may not properly hide the layer the effect is on. Disabling and Enabling the transparent mirror, or toggling between the states will fix this. 

### Planned Changes
* Ability to change world mirrors to Cutout
* Mirror only grabable when hotkey is pressed on controller

### License
The majority of this code does not have a license specified and should be assumed to be All Rights Reserved. I have received permission from the original author of PortableMirror to modify their code and make releases based off of it.  
The assetbundle mirrorprefab contains aacertainbluecat/VRCPlayersOnlyMirror which is licensed under the MIT License
