## PortableMirror
This mod allows the user to locally spawn a mirror for themselves in any VRChat world.

The mirror can be configured:
  * Allow/disallow mirror pickup
  * Toggle full/optimized/cutout/transparent mirror
  * Configurable mirror size and distance from you
  * 45 degree, ceiling, small and transparent mirrors
  * [UIX](https://github.com/knah/VRCMods/releases/latest/download/UIExpansionKit.dll) menu to control settings
	
![image](https://user-images.githubusercontent.com/81605232/113796149-2cb2fd80-9714-11eb-8c25-a340b6f2e849.png)
![image](https://user-images.githubusercontent.com/81605232/114292424-272b1f80-9a54-11eb-9a8e-8583b8b3a836.png)Cutout mirror
![image](https://user-images.githubusercontent.com/81605232/113796268-73a0f300-9714-11eb-919a-a08e644b8b04.png)
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

