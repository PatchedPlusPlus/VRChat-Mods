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

![image](https://user-images.githubusercontent.com/81605232/114292424-272b1f80-9a54-11eb-9a8e-8583b8b3a836.png)

  __Transparent and Cutout Mirror Examples__    
  
![image](https://user-images.githubusercontent.com/81605232/115629313-bbed1300-a2c7-11eb-83f9-dc6888e94256.png)  

### Changelog
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

### License
The majority of this code does not have a license specified and should be assumed to be All Rights Reserved. I have received permission from the original author of PortableMirror to modify their code and make releases based off of it.  
The assetbundle mirrorprefab contains aacertainbluecat/VRCPlayersOnlyMirror which is licensed under the MIT License
