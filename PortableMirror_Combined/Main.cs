using System;
using System.Linq;
using System.Collections;
using System.Reflection;
using MelonLoader;
using UIExpansionKit.API;
using UnityEngine;
using VRCSDK2;
using System.Collections.Generic;
using UnhollowerRuntimeLib;
using System.IO;


[assembly: MelonModInfo(typeof(PortableMirror.Main), "PortableMirrorMod", "1.4.0", "M-oons, Nirvash")] //Name changed to break auto update
[assembly: MelonModGame("VRChat", "VRChat")]

namespace PortableMirror
{

    public class Main : MelonMod
    {
        public override void OnApplicationStart()
        {
            loadAssets();

            ModPrefs.RegisterCategory("PortableMirror", "PortableMirror");
            ModPrefs.RegisterPrefFloat("PortableMirror", "MirrorScaleX", 5f, "Mirror Scale X");
            ModPrefs.RegisterPrefFloat("PortableMirror", "MirrorScaleY", 3f, "Mirror Scale Y");
            ModPrefs.RegisterPrefFloat("PortableMirror", "MirrorDistance", 0f, "Mirror Distance");
            ModPrefs.RegisterPrefString("PortableMirror", "MirrorState", "MirrorFull", "Mirror Type");
            ExpansionKitApi.RegisterSettingAsStringEnum("PortableMirror", "MirrorState", new[] { ("MirrorFull", "Full"), ("MirrorOpt", "Optimized"), ("MirrorCutout", "Cutout"), ("MirrorTransparent", "Transparent") });
            ModPrefs.RegisterPrefBool("PortableMirror", "CanPickupMirror", false, "Can Pickup Mirror");
            ModPrefs.RegisterPrefBool("PortableMirror", "enableBase", true, "Enable Portable Mirror QM Button");
            ModPrefs.RegisterPrefString("PortableMirror", "MirrorKeybind", "Alpha1", "Toggle Mirror Keybind");
            ModPrefs.RegisterPrefBool("PortableMirror", "QuickMenuOptions", true, "Quick Menu Settings Button");
            ModPrefs.RegisterPrefFloat("PortableMirror", "TransMirrorTrans", .4f, "Transparent Mirror transparency - Higher is more transparent - Global for all mirrors");


            ModPrefs.RegisterCategory("PortableMirror45", "PortableMirror 45");
            ModPrefs.RegisterPrefFloat("PortableMirror45", "MirrorScaleX", 5f, "Mirror Scale X");
            ModPrefs.RegisterPrefFloat("PortableMirror45", "MirrorScaleY", 3f, "Mirror Scale Y");
            ModPrefs.RegisterPrefFloat("PortableMirror45", "MirrorDistance", 0f, "Mirror Distance");
            ModPrefs.RegisterPrefString("PortableMirror45", "MirrorState", "MirrorFull", "Mirror Type");
            ExpansionKitApi.RegisterSettingAsStringEnum("PortableMirror45", "MirrorState", new[] { ("MirrorFull", "Full"), ("MirrorOpt", "Optimized"), ("MirrorCutout", "Cutout"), ("MirrorTransparent", "Transparent") });
            ModPrefs.RegisterPrefBool("PortableMirror45", "CanPickup45Mirror", false, "Can Pickup 45 Mirror");
            ModPrefs.RegisterPrefBool("PortableMirror45", "enable45", true, "Enable 45 Mirror QM Button");
            
            ModPrefs.RegisterCategory("PortableMirrorCeiling", "PortableMirror Ceiling");
            ModPrefs.RegisterPrefFloat("PortableMirrorCeiling", "MirrorScaleX", 5f, "Mirror Scale X");
            ModPrefs.RegisterPrefFloat("PortableMirrorCeiling", "MirrorScaleZ", 3f, "Mirror Scale Z");
            ModPrefs.RegisterPrefFloat("PortableMirrorCeiling", "MirrorDistance", 2, "Mirror Distance");
            ModPrefs.RegisterPrefString("PortableMirrorCeiling", "MirrorState", "MirrorFull", "Mirror Type");
            ExpansionKitApi.RegisterSettingAsStringEnum("PortableMirrorCeiling", "MirrorState", new[] { ("MirrorFull", "Full"), ("MirrorOpt", "Optimized"), ("MirrorCutout", "Cutout"), ("MirrorTransparent", "Transparent") });
            ModPrefs.RegisterPrefBool("PortableMirrorCeiling", "CanPickupCeilingMirror", false, "Can Pickup Ceiling Mirror");
            ModPrefs.RegisterPrefBool("PortableMirrorCeiling", "enableCeiling", true, "Enable Ceiling Mirror QM Button");

            ModPrefs.RegisterCategory("PortableMirrorMicro", "PortableMirror Micro");
            ModPrefs.RegisterPrefFloat("PortableMirrorMicro", "MirrorScaleX", .05f, "Mirror Scale X");
            ModPrefs.RegisterPrefFloat("PortableMirrorMicro", "MirrorScaleY", .1f, "Mirror Scale Y");
            ModPrefs.RegisterPrefFloat("PortableMirrorMicro", "GrabRange", .1f, "GrabRange");
            ModPrefs.RegisterPrefString("PortableMirrorMicro", "MirrorState", "MirrorFull", "Mirror Type");
            ExpansionKitApi.RegisterSettingAsStringEnum("PortableMirrorMicro", "MirrorState", new[] { ("MirrorFull", "Full"), ("MirrorOpt", "Optimized"), ("MirrorCutout", "Cutout"), ("MirrorTransparent", "Transparent") });
            ModPrefs.RegisterPrefBool("PortableMirrorMicro", "CanPickupMirrorMicro", false, "Can Pickup MirrorMicro");
            ModPrefs.RegisterPrefBool("PortableMirrorMicro", "enableMicro", true, "Enable Micro Mirror QM Button");
            
            ModPrefs.RegisterCategory("PortableMirrorTrans", "PortableMirror Transparent");
            ModPrefs.RegisterPrefFloat("PortableMirrorTrans", "MirrorScaleX", 5f, "Mirror Scale X");
            ModPrefs.RegisterPrefFloat("PortableMirrorTrans", "MirrorScaleY", 3f, "Mirror Scale Y");
            ModPrefs.RegisterPrefFloat("PortableMirrorTrans", "MirrorDistance", 0f, "Mirror Distance");
            ModPrefs.RegisterPrefString("PortableMirrorTrans", "MirrorState", "MirrorTransparent", "Mirror Type - Resets to Transparent on load");
            ExpansionKitApi.RegisterSettingAsStringEnum("PortableMirrorTrans", "MirrorState", new[] { ("MirrorFull", "Full"), ("MirrorOpt", "Optimized"), ("MirrorCutout", "Cutout"), ("MirrorTransparent", "Transparent") });
            ModPrefs.SetString("PortableMirrorTrans", "MirrorState", "MirrorTransparent");//Force to Transparent every load
            ModPrefs.RegisterPrefBool("PortableMirrorTrans", "CanPickupMirror", false, "Can Pickup Mirror");
            ModPrefs.RegisterPrefBool("PortableMirrorTrans", "enableTrans", true, "Enable Transparent Mirror QM Button");

            OnModSettingsApplied();

            MelonModLogger.Log("Base mod made by M-oons, modifications by Nirvash");
            MelonModLogger.Log("Settings can be configured in UserData\\modprefs.ini");
            MelonModLogger.Log($"[{_mirrorKeybindBase}] -> Toggle portable mirror");

            MelonMod uiExpansionKit = MelonLoader.Main.Mods.Find(m => m.InfoAttribute.Name == "UI Expansion Kit");
            if (uiExpansionKit != null)
            {
                uiExpansionKit.InfoAttribute.SystemType.Assembly.GetTypes().First(t => t.FullName == "UIExpansionKit.API.ExpansionKitApi").GetMethod("RegisterWaitConditionBeforeDecorating", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Invoke(null, new object[]
                {
                    CreateQuickMenuButton()
                });
            }
        }

        public override void VRChat_OnUiManagerInit()
        {
            MelonCoroutines.Start(ButtonState());
        }

        private IEnumerator ButtonState()
        {//Give UIX a few seconds to make buttons, then set the state of the Toggle 
            yield return new WaitForSeconds(5f);
            OnModSettingsApplied();
        }


        public override void OnModSettingsApplied()
        {
            _enableBase = ModPrefs.GetBool("PortableMirror", "enableBase");
            _enable45 = ModPrefs.GetBool("PortableMirror45", "enable45");
            _enableCeiling = ModPrefs.GetBool("PortableMirrorCeiling", "enableCeiling");
            _enableMicro = ModPrefs.GetBool("PortableMirrorMicro", "enableMicro");
            _enableTrans = ModPrefs.GetBool("PortableMirrorTrans", "enableTrans");
            _quickMenuOptions = ModPrefs.GetBool("PortableMirror", "QuickMenuOptions");
            if (_enableBase && ButtonList.ContainsKey("Base") && ButtonList["Base"] != null) ButtonList["Base"].gameObject.active = true;
            else if (ButtonList.ContainsKey("Base")) ButtonList["Base"].gameObject.active = false;
            if (_enable45 && ButtonList.ContainsKey("45") && ButtonList["45"] != null) ButtonList["45"].gameObject.active = true;
            else if (ButtonList.ContainsKey("45")) ButtonList["45"].gameObject.active = false;
            if (_enableCeiling && ButtonList.ContainsKey("Ceiling") && ButtonList["Ceiling"] != null) ButtonList["Ceiling"].gameObject.active = true;
            else if (ButtonList.ContainsKey("Ceiling")) ButtonList["Ceiling"].gameObject.active = false;
            if (_enableMicro && ButtonList.ContainsKey("Micro") && ButtonList["Micro"] != null) ButtonList["Micro"].gameObject.active = true;
            else if (ButtonList.ContainsKey("Micro")) ButtonList["Micro"].gameObject.active = false;
            if (_enableTrans && ButtonList.ContainsKey("Trans") && ButtonList["Trans"] != null) ButtonList["Trans"].gameObject.active = true;
            else if (ButtonList.ContainsKey("Trans")) ButtonList["Trans"].gameObject.active = false;
            if (_quickMenuOptions && ButtonList.ContainsKey("Settings") && ButtonList["Settings"] != null) ButtonList["Settings"].gameObject.active = true;
            else if(ButtonList.ContainsKey("Settings")) ButtonList["Settings"].gameObject.active = false;

            _MirrorTransValue = ModPrefs.GetFloat("PortableMirror", "TransMirrorTrans");


            _oldMirrorScaleYBase = _mirrorScaleYBase;
            _oldMirrorDistance = _MirrorDistance;
            _mirrorScaleXBase = ModPrefs.GetFloat("PortableMirror", "MirrorScaleX");
            _mirrorScaleYBase = ModPrefs.GetFloat("PortableMirror", "MirrorScaleY");
            _MirrorDistance = ModPrefs.GetFloat("PortableMirror", "MirrorDistance");
            _canPickupMirrorBase = ModPrefs.GetBool("PortableMirror", "CanPickupMirror");
            _mirrorKeybindBase = Utils.GetMirrorKeybind();
            _mirrorStateBase = ModPrefs.GetString("PortableMirror", "MirrorState");

            if (_mirrorBase != null && Utils.GetVRCPlayer() != null)
            {
                _mirrorBase.transform.localScale = new Vector3(_mirrorScaleXBase, _mirrorScaleYBase, 1f);
                _mirrorBase.transform.position = new Vector3(_mirrorBase.transform.position.x, _mirrorBase.transform.position.y + ((_mirrorScaleYBase - _oldMirrorScaleYBase) / 2), _mirrorBase.transform.position.z  );
                _mirrorBase.transform.position += _mirrorBase.transform.forward * (_MirrorDistance - _oldMirrorDistance);

                _mirrorBase.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupMirrorBase;
 
                if (_mirrorStateBase == "MirrorCutout" || _mirrorStateBase == "MirrorTransparent") SetAllMirrorsToIgnoreShader();
                if (_mirrorStateBase == "MirrorTransparent") _mirrorBase.transform.Find(_mirrorStateBase).GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue);
                for (int i = 0; i < _mirrorBase.transform.childCount; i++)
                    _mirrorBase.transform.GetChild(i).gameObject.active = false;
                var childMirror = _mirrorBase.transform.Find(_mirrorStateBase);
                childMirror.gameObject.active = true;
            }


            _oldMirrorScaleY45 = _mirrorScaleY45;
            _oldMirrorDistance45 = _MirrorDistance45;
            _mirrorScaleX45 = ModPrefs.GetFloat("PortableMirror45", "MirrorScaleX");
            _mirrorScaleY45 = ModPrefs.GetFloat("PortableMirror45", "MirrorScaleY");
            _MirrorDistance45 = ModPrefs.GetFloat("PortableMirror45", "MirrorDistance");
            _CanPickup45Mirror = ModPrefs.GetBool("PortableMirror45", "CanPickup45Mirror");
            _mirrorState45 = ModPrefs.GetString("PortableMirror45", "MirrorState");

            if (_mirror45 != null && Utils.GetVRCPlayer() != null)
            {
                _mirror45.transform.localScale = new Vector3(_mirrorScaleX45, _mirrorScaleY45, 1f);
                _mirror45.transform.rotation = _mirror45.transform.rotation * Quaternion.AngleAxis(-45, Vector3.left);  

                _mirror45.transform.position = new Vector3(_mirror45.transform.position.x, _mirror45.transform.position.y + (_mirrorScaleY45 - _oldMirrorScaleY45), _mirror45.transform.position.z  );
                _mirror45.transform.position += _mirror45.transform.forward * (_MirrorDistance45 - _oldMirrorDistance45);
                _mirror45.transform.rotation = _mirror45.transform.rotation * Quaternion.AngleAxis(45, Vector3.left);

                _mirror45.GetOrAddComponent<VRC_Pickup>().pickupable = _CanPickup45Mirror;

                if (_mirrorState45 == "MirrorCutout" || _mirrorState45 == "MirrorTransparent") SetAllMirrorsToIgnoreShader();
                if (_mirrorState45 == "MirrorTransparent") _mirror45.transform.Find(_mirrorState45).GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue);
                for (int i = 0; i < _mirror45.transform.childCount; i++)
                    _mirror45.transform.GetChild(i).gameObject.active = false;
                var childMirror = _mirror45.transform.Find(_mirrorState45);
                childMirror.gameObject.active = true;
            }


            _oldMirrorDistanceCeiling = _MirrorDistanceCeiling;
            _mirrorScaleXCeiling = ModPrefs.GetFloat("PortableMirrorCeiling", "MirrorScaleX");
            _mirrorScaleZCeiling = ModPrefs.GetFloat("PortableMirrorCeiling", "MirrorScaleZ");
            _MirrorDistanceCeiling = ModPrefs.GetFloat("PortableMirrorCeiling", "MirrorDistance");
            _canPickupCeilingMirror = ModPrefs.GetBool("PortableMirrorCeiling", "CanPickupCeilingMirror");
            _mirrorStateCeiling = ModPrefs.GetString("PortableMirrorCeiling", "MirrorState");

            if (_mirrorCeiling != null && Utils.GetVRCPlayer() != null)
            {
                _mirrorCeiling.transform.localScale = new Vector3(_mirrorScaleXCeiling, _mirrorScaleZCeiling, 1f);
                _mirrorCeiling.transform.position = new Vector3(_mirrorCeiling.transform.position.x, _mirrorCeiling.transform.position.y + (_MirrorDistanceCeiling - _oldMirrorDistanceCeiling), _mirrorCeiling.transform.position.z);

                _mirrorCeiling.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupCeilingMirror;

                if (_mirrorStateCeiling == "MirrorCutout" || _mirrorStateCeiling == "MirrorTransparent") SetAllMirrorsToIgnoreShader();
                if (_mirrorStateCeiling == "MirrorTransparent") _mirrorCeiling.transform.Find(_mirrorStateCeiling).GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue);
                for (int i = 0; i < _mirrorCeiling.transform.childCount; i++)
                    _mirrorCeiling.transform.GetChild(i).gameObject.active = false;
                var childMirror = _mirrorCeiling.transform.Find(_mirrorStateCeiling);
                childMirror.gameObject.active = true;
            }


            _oldMirrorScaleYMicro = _mirrorScaleMicro;
            _mirrorScaleXMicro = ModPrefs.GetFloat("PortableMirrorMicro", "MirrorScaleX");
            _mirrorScaleMicro = ModPrefs.GetFloat("PortableMirrorMicro", "MirrorScaleY");
            _grabRangeMicro = ModPrefs.GetFloat("PortableMirrorMicro", "GrabRange");
            _canPickupMirrorMicro = ModPrefs.GetBool("PortableMirrorMicro", "CanPickupMirrorMicro");
            _mirrorStateMicro = ModPrefs.GetString("PortableMirrorMicro", "MirrorState");

            if (_mirrorMicro != null && Utils.GetVRCPlayer() != null)
            {
                _mirrorMicro.transform.localScale = new Vector3(_mirrorScaleXMicro, _mirrorScaleMicro, 1f);
                _mirrorMicro.transform.position = new Vector3(_mirrorMicro.transform.position.x, _mirrorMicro.transform.position.y + ((_mirrorScaleMicro - _oldMirrorScaleYMicro) / 2), _mirrorMicro.transform.position.z);

                _mirrorMicro.GetOrAddComponent<VRC_Pickup>().proximity = _grabRangeMicro;
                _mirrorMicro.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupMirrorMicro;

                if (_mirrorStateMicro == "MirrorCutout" || _mirrorStateMicro == "MirrorTransparent") SetAllMirrorsToIgnoreShader();
                if (_mirrorStateMicro == "MirrorTransparent") _mirrorMicro.transform.Find(_mirrorStateMicro).GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue);
                for (int i = 0; i < _mirrorMicro.transform.childCount; i++)
                    _mirrorMicro.transform.GetChild(i).gameObject.active = false;
                var childMirror = _mirrorMicro.transform.Find(_mirrorStateMicro);
                childMirror.gameObject.active = true;
            }


            _oldMirrorScaleYTrans = _mirrorScaleYTrans;
            _oldMirrorDistanceTrans = _MirrorDistanceTrans;
            _mirrorScaleXTrans = ModPrefs.GetFloat("PortableMirrorTrans", "MirrorScaleX");
            _mirrorScaleYTrans = ModPrefs.GetFloat("PortableMirrorTrans", "MirrorScaleY");
            _MirrorDistanceTrans = ModPrefs.GetFloat("PortableMirrorTrans", "MirrorDistance");
            _canPickupMirrorTrans = ModPrefs.GetBool("PortableMirrorTrans", "CanPickupMirror");
            _mirrorStateTrans = ModPrefs.GetString("PortableMirrorTrans", "MirrorState");

            if (_mirrorTrans != null && Utils.GetVRCPlayer() != null)
            {
                _mirrorTrans.transform.localScale = new Vector3(_mirrorScaleXTrans, _mirrorScaleYTrans, 1f);
                _mirrorTrans.transform.position += _mirrorTrans.transform.forward * (_MirrorDistanceTrans - _oldMirrorDistanceTrans);

                _mirrorTrans.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupMirrorTrans;

                if (_mirrorStateTrans == "MirrorCutout" || _mirrorStateTrans == "MirrorTransparent") SetAllMirrorsToIgnoreShader();
                if (_mirrorStateTrans == "MirrorTransparent") _mirrorTrans.transform.Find(_mirrorStateTrans).GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue);
                for (int i = 0; i < _mirrorTrans.transform.childCount; i++)
                    _mirrorTrans.transform.GetChild(i).gameObject.active = false;
                var childMirror = _mirrorTrans.transform.Find(_mirrorStateTrans);
                childMirror.gameObject.active = true;
            }


        }

        private IEnumerator CreateQuickMenuButton()
        {
            while (QuickMenu.prop_QuickMenu_0 == null) yield return null;

            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Toggle\nPortable\nMirror", () =>
            {
                if (Utils.GetVRCPlayer() == null) return;
                ToggleMirror();
            }, (button) => ButtonList["Base"] = button.transform);
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Toggle\nPortable\nMirror 45", () =>
            {
                if (Utils.GetVRCPlayer() == null) return;
                ToggleMirror45();
            }, (button) => ButtonList["45"] = button.transform);                  
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Toggle\nCeiling\nMirror", () =>
            {
                if (Utils.GetVRCPlayer() == null) return;
                ToggleMirrorCeiling();
            }, (button) => ButtonList["Ceiling"] = button.transform);
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Toggle\nMicro\nMirror", () =>
            {
                if (Utils.GetVRCPlayer() == null) return;
                ToggleMirrorMicro();
            }, (button) => ButtonList["Micro"] = button.transform);
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Toggle\nTransparent\nMirror", () =>
            {
                if (Utils.GetVRCPlayer() == null) return;
                ToggleMirrorTrans();
            }, (button) => ButtonList["Trans"] = button.transform);
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Portable\nMirror\nSettings", () =>
            {
                QuickMenuOptions();
            }, (button) => ButtonList["Settings"] = button.transform);
        }

        private string StateText(string stateRaw)
        {
            switch (stateRaw)
            {
                case "MirrorFull": return "Full";
                case "MirrorOpt": return "Optimized";
                case "MirrorCutout": return "Cutout";
                case "MirrorTransparent": return "Transparent";
                default: return "Something Broke";
            }
        }
        private void QuickMenuOptions()
        {
            var mirrorMenu = ExpansionKitApi.CreateCustomQuickMenuPage(LayoutDescriptionCustom.QuickMenu3Column);
            
            mirrorMenu.AddToggleButton("Portable Mirror", (action) =>
            {
                if (Utils.GetVRCPlayer() == null) return;
                ToggleMirror();
            }, () => _mirrorBase != null);
            mirrorMenu.AddSimpleButton(StateText(_mirrorStateBase), () =>
            {
                if (_mirrorStateBase == "MirrorFull")  ModPrefs.SetString("PortableMirror", "MirrorState", "MirrorOpt");  
                else if (_mirrorStateBase == "MirrorOpt")  ModPrefs.SetString("PortableMirror", "MirrorState", "MirrorCutout"); 
                else if (_mirrorStateBase == "MirrorCutout")  ModPrefs.SetString("PortableMirror", "MirrorState", "MirrorTransparent");
                else if (_mirrorStateBase == "MirrorTransparent") ModPrefs.SetString("PortableMirror", "MirrorState", "MirrorFull");
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions();
            });
            mirrorMenu.AddSimpleButton(ModPrefs.GetBool("PortableMirror", "CanPickupMirror") ? "Pickupable" : "Not Pickupable", () => {
                ModPrefs.SetBool("PortableMirror", "CanPickupMirror", !ModPrefs.GetBool("PortableMirror", "CanPickupMirror"));
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions();
            });
            mirrorMenu.AddLabel($"Distance: {ModPrefs.GetFloat("PortableMirror", "MirrorDistance")}");
            mirrorMenu.AddSimpleButton("+", () => {
                ModPrefs.SetFloat("PortableMirror", "MirrorDistance", ModPrefs.GetFloat("PortableMirror", "MirrorDistance") + .25f );
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions();
            });
            mirrorMenu.AddSimpleButton("-", () => {
                ModPrefs.SetFloat("PortableMirror", "MirrorDistance", ModPrefs.GetFloat("PortableMirror", "MirrorDistance") - .25f);
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions();
            });
            if (true)//(_enable45)
            {
                mirrorMenu.AddToggleButton("45 Mirror", (action) =>
                {
                    if (Utils.GetVRCPlayer() == null) return;
                    ToggleMirror45();
                }, () => _mirror45 != null);
                mirrorMenu.AddSimpleButton(StateText(_mirrorState45), () =>
                {
                    if (_mirrorState45 == "MirrorFull") ModPrefs.SetString("PortableMirror45", "MirrorState", "MirrorOpt");
                    else if (_mirrorState45 == "MirrorOpt") ModPrefs.SetString("PortableMirror45", "MirrorState", "MirrorCutout");
                    else if (_mirrorState45 == "MirrorCutout") ModPrefs.SetString("PortableMirror45", "MirrorState", "MirrorTransparent");
                    else if (_mirrorState45 == "MirrorTransparent") ModPrefs.SetString("PortableMirror45", "MirrorState", "MirrorFull");
                    OnModSettingsApplied();
                    mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions();
                });
                mirrorMenu.AddSimpleButton(ModPrefs.GetBool("PortableMirror45", "CanPickup45Mirror") ? "Pickupable" : "Not Pickupable", () =>
                {
                    ModPrefs.SetBool("PortableMirror45", "CanPickup45Mirror", !ModPrefs.GetBool("PortableMirror45", "CanPickup45Mirror"));
                    OnModSettingsApplied();
                    mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions();
                });
                mirrorMenu.AddLabel($"Distance: {ModPrefs.GetFloat("PortableMirror45", "MirrorDistance")}");
                mirrorMenu.AddSimpleButton("+", () => {
                    ModPrefs.SetFloat("PortableMirror45", "MirrorDistance", ModPrefs.GetFloat("PortableMirror45", "MirrorDistance") + .25f);
                    OnModSettingsApplied();
                    mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions();
                });
                mirrorMenu.AddSimpleButton("-", () => {
                    ModPrefs.SetFloat("PortableMirror45", "MirrorDistance", ModPrefs.GetFloat("PortableMirror45", "MirrorDistance") - .25f);
                    OnModSettingsApplied();
                    mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions();
                });
            }
            if (true)//(_enableCeiling)
            {
                mirrorMenu.AddToggleButton("Ceiling Mirror", (action) =>
                {
                    if (Utils.GetVRCPlayer() == null) return;
                    ToggleMirrorCeiling();
                }, () => _mirrorCeiling != null);
                mirrorMenu.AddSimpleButton(StateText(_mirrorStateCeiling), () =>
                {
                    if (_mirrorStateCeiling == "MirrorFull") ModPrefs.SetString("PortableMirrorCeiling", "MirrorState", "MirrorOpt");
                    else if (_mirrorStateCeiling == "MirrorOpt") ModPrefs.SetString("PortableMirrorCeiling", "MirrorState", "MirrorCutout");
                    else if (_mirrorStateCeiling == "MirrorCutout") ModPrefs.SetString("PortableMirrorCeiling", "MirrorState", "MirrorTransparent");
                    else if (_mirrorStateCeiling == "MirrorTransparent") ModPrefs.SetString("PortableMirrorCeiling", "MirrorState", "MirrorFull");
                    OnModSettingsApplied();
                    mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions();
                });
                mirrorMenu.AddSimpleButton(ModPrefs.GetBool("PortableMirrorCeiling", "CanPickupCeilingMirror") ? "Pickupable" : "Not Pickupable", () =>
                {
                    ModPrefs.SetBool("PortableMirrorCeiling", "CanPickupCeilingMirror", !ModPrefs.GetBool("PortableMirrorCeiling", "CanPickupCeilingMirror"));
                    OnModSettingsApplied();
                    mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions();
                });
                mirrorMenu.AddLabel($"Distance: {ModPrefs.GetFloat("PortableMirrorCeiling", "MirrorDistance")}");
                mirrorMenu.AddSimpleButton("+", () => {
                    ModPrefs.SetFloat("PortableMirrorCeiling", "MirrorDistance", ModPrefs.GetFloat("PortableMirrorCeiling", "MirrorDistance") + .25f);
                    OnModSettingsApplied();
                    mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions();
                });
                mirrorMenu.AddSimpleButton("-", () => {
                    ModPrefs.SetFloat("PortableMirrorCeiling", "MirrorDistance", ModPrefs.GetFloat("PortableMirrorCeiling", "MirrorDistance") - .25f);
                    OnModSettingsApplied();
                    mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions();
                });
            }
            if (true)//(_enableMicro)
            {
                mirrorMenu.AddToggleButton("Micro Mirror", (action) =>
                {
                    if (Utils.GetVRCPlayer() == null) return;
                    ToggleMirrorMicro();
                }, () => _mirrorMicro != null);
                mirrorMenu.AddSimpleButton(StateText(_mirrorStateMicro), () =>
                {
                    if (_mirrorStateMicro == "MirrorFull") ModPrefs.SetString("PortableMirrorMicro", "MirrorState", "MirrorOpt");
                    else if (_mirrorStateMicro == "MirrorOpt") ModPrefs.SetString("PortableMirrorMicro", "MirrorState", "MirrorCutout");
                    else if (_mirrorStateMicro == "MirrorCutout") ModPrefs.SetString("PortableMirrorMicro", "MirrorState", "MirrorTransparent");
                    else if (_mirrorStateMicro == "MirrorTransparent") ModPrefs.SetString("PortableMirrorMicro", "MirrorState", "MirrorFull");
                    OnModSettingsApplied();
                    mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions();
                });
                mirrorMenu.AddSimpleButton(ModPrefs.GetBool("PortableMirrorMicro", "CanPickupMirrorMicro") ? "Pickupable" : "Not Pickupable", () =>
                {
                    ModPrefs.SetBool("PortableMirrorMicro", "CanPickupMirrorMicro", !ModPrefs.GetBool("PortableMirrorMicro", "CanPickupMirrorMicro"));
                    OnModSettingsApplied();
                    mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions();
                });
            }
            mirrorMenu.AddSimpleButton($"Close", () => {
                mirrorMenu.Hide();
            });
            mirrorMenu.AddSimpleButton($"Page 2", () => {
                mirrorMenu.Hide();
                QuickMenuOptions2();
            });

            mirrorMenu.Show();
        }

        private void QuickMenuOptions2()
        {
            var mirrorMenu = ExpansionKitApi.CreateCustomQuickMenuPage(LayoutDescriptionCustom.QuickMenu3Column);
            //Row 1
            mirrorMenu.AddToggleButton("Portable Mirror", (action) =>
            {
                if (Utils.GetVRCPlayer() == null) return;
                ToggleMirror();
            }, () => _mirrorBase != null);
            mirrorMenu.AddSimpleButton("Larger", () => {
                ModPrefs.SetFloat("PortableMirror", "MirrorScaleX", ModPrefs.GetFloat("PortableMirror", "MirrorScaleX") + .25f);
                ModPrefs.SetFloat("PortableMirror", "MirrorScaleY", ModPrefs.GetFloat("PortableMirror", "MirrorScaleY") + .25f);
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
            });
            mirrorMenu.AddSimpleButton("Smaller", () => {
                if (ModPrefs.GetFloat("PortableMirror", "MirrorScaleX") > .25 && ModPrefs.GetFloat("PortableMirror", "MirrorScaleY") > .25)
                {
                    ModPrefs.SetFloat("PortableMirror", "MirrorScaleX", ModPrefs.GetFloat("PortableMirror", "MirrorScaleX") - .25f);
                    ModPrefs.SetFloat("PortableMirror", "MirrorScaleY", ModPrefs.GetFloat("PortableMirror", "MirrorScaleY") - .25f);
                    OnModSettingsApplied();
                    mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
                }
            });

            //2
            mirrorMenu.AddToggleButton("45 Mirror", (action) =>
            {
                if (Utils.GetVRCPlayer() == null) return;
                ToggleMirror45();
            }, () => _mirror45 != null);
            mirrorMenu.AddSimpleButton("Larger", () => {
                ModPrefs.SetFloat("PortableMirror45", "MirrorScaleX", ModPrefs.GetFloat("PortableMirror45", "MirrorScaleX") + .25f);
                ModPrefs.SetFloat("PortableMirror45", "MirrorScaleY", ModPrefs.GetFloat("PortableMirror45", "MirrorScaleY") + .25f);
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
            });
            mirrorMenu.AddSimpleButton("Smaller", () => {
                if (ModPrefs.GetFloat("PortableMirror45", "MirrorScaleX") > .25 && ModPrefs.GetFloat("PortableMirror45", "MirrorScaleY") > .25)
                {
                    ModPrefs.SetFloat("PortableMirror45", "MirrorScaleX", ModPrefs.GetFloat("PortableMirror45", "MirrorScaleX") - .25f);
                    ModPrefs.SetFloat("PortableMirror45", "MirrorScaleY", ModPrefs.GetFloat("PortableMirror45", "MirrorScaleY") - .25f);

                    OnModSettingsApplied();
                    mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
                }
            });
            //3
            mirrorMenu.AddToggleButton("Ceiling Mirror", (action) =>
            {
                if (Utils.GetVRCPlayer() == null) return;
                ToggleMirrorCeiling();
            }, () => _mirrorCeiling != null);
            mirrorMenu.AddSimpleButton("Larger", () => {
                ModPrefs.SetFloat("PortableMirrorCeiling", "MirrorScaleX", ModPrefs.GetFloat("PortableMirrorCeiling", "MirrorScaleX") + .25f);
                ModPrefs.SetFloat("PortableMirrorCeiling", "MirrorScaleZ", ModPrefs.GetFloat("PortableMirrorCeiling", "MirrorScaleZ") + .25f);

                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
            });
            mirrorMenu.AddSimpleButton("Smaller", () => {
                if (ModPrefs.GetFloat("PortableMirrorCeiling", "MirrorScaleX") > .25 && ModPrefs.GetFloat("PortableMirrorCeiling", "MirrorScaleZ") > .25)
                {
                    ModPrefs.SetFloat("PortableMirrorCeiling", "MirrorScaleX", ModPrefs.GetFloat("PortableMirrorCeiling", "MirrorScaleX") - .25f);
                    ModPrefs.SetFloat("PortableMirrorCeiling", "MirrorScaleZ", ModPrefs.GetFloat("PortableMirrorCeiling", "MirrorScaleZ") - .25f);

                    OnModSettingsApplied();
                    mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
                }
            });
            //4
            mirrorMenu.AddToggleButton("Micro Mirror", (action) =>
            {
                if (Utils.GetVRCPlayer() == null) return;
                ToggleMirrorMicro();
            }, () => _mirrorMicro != null);
            
            mirrorMenu.AddSimpleButton("Larger", () => {
                ModPrefs.SetFloat("PortableMirrorMicro", "MirrorScaleX", ModPrefs.GetFloat("PortableMirrorMicro", "MirrorScaleX") + .01f);
                ModPrefs.SetFloat("PortableMirrorMicro", "MirrorScaleY", ModPrefs.GetFloat("PortableMirrorMicro", "MirrorScaleY") + .01f);
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
            });
         
            mirrorMenu.AddSimpleButton("Smaller", () => {
                if (ModPrefs.GetFloat("PortableMirrorMicro", "MirrorScaleX") > .02 && ModPrefs.GetFloat("PortableMirrorMicro", "MirrorScaleY") > .02)
                {
                    ModPrefs.SetFloat("PortableMirrorMicro", "MirrorScaleX", ModPrefs.GetFloat("PortableMirrorMicro", "MirrorScaleX") - .01f);
                    ModPrefs.SetFloat("PortableMirrorMicro", "MirrorScaleY", ModPrefs.GetFloat("PortableMirrorMicro", "MirrorScaleY") - .01f);

                    OnModSettingsApplied();
                    mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
                }
            });

            //5
            mirrorMenu.AddToggleButton("Transparent", (action) =>
            {
                if (Utils.GetVRCPlayer() == null) return;
                ToggleMirrorTrans();
            }, () => _mirrorTrans != null);
           
            mirrorMenu.AddSimpleButton(StateText(_mirrorStateTrans), () =>
            {
                if (_mirrorStateTrans == "MirrorFull") ModPrefs.SetString("PortableMirrorTrans", "MirrorState", "MirrorOpt");
                else if (_mirrorStateTrans == "MirrorOpt") ModPrefs.SetString("PortableMirrorTrans", "MirrorState", "MirrorCutout");
                else if (_mirrorStateTrans == "MirrorCutout") ModPrefs.SetString("PortableMirrorTrans", "MirrorState", "MirrorTransparent");
                else if (_mirrorStateTrans == "MirrorTransparent") ModPrefs.SetString("PortableMirrorTrans", "MirrorState", "MirrorFull");
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
            });
         
            mirrorMenu.AddSimpleButton(ModPrefs.GetBool("PortableMirrorTrans", "CanPickupMirror") ? "Pickupable" : "Not Pickupable", () =>
            {
                ModPrefs.SetBool("PortableMirrorTrans", "CanPickupMirror", !ModPrefs.GetBool("PortableMirrorTrans", "CanPickupMirror"));
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
            });
            //6
            mirrorMenu.AddLabel($"Distance: {ModPrefs.GetFloat("PortableMirrorTrans", "MirrorDistance")}");
            mirrorMenu.AddSimpleButton("+", () => {
                ModPrefs.SetFloat("PortableMirrorTrans", "MirrorDistance", ModPrefs.GetFloat("PortableMirrorTrans", "MirrorDistance") + .25f);
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
            });
           
            mirrorMenu.AddSimpleButton("-", () => {
                ModPrefs.SetFloat("PortableMirrorTrans", "MirrorDistance", ModPrefs.GetFloat("PortableMirrorTrans", "MirrorDistance") - .25f);
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
            });
           //7
            mirrorMenu.AddSpacer();
            
            mirrorMenu.AddSimpleButton("Larger", () => {
                ModPrefs.SetFloat("PortableMirrorTrans", "MirrorScaleX", ModPrefs.GetFloat("PortableMirrorTrans", "MirrorScaleX") + .25f);
                ModPrefs.SetFloat("PortableMirrorTrans", "MirrorScaleY", ModPrefs.GetFloat("PortableMirrorTrans", "MirrorScaleY") + .25f);
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
            });
            
            mirrorMenu.AddSimpleButton("Smaller", () => {
                if (ModPrefs.GetFloat("PortableMirrorTrans", "MirrorScaleX") > .25 && ModPrefs.GetFloat("PortableMirrorTrans", "MirrorScaleY") > .25)
                {
                    ModPrefs.SetFloat("PortableMirrorTrans", "MirrorScaleX", ModPrefs.GetFloat("PortableMirrorTrans", "MirrorScaleX") - .25f);
                    ModPrefs.SetFloat("PortableMirrorTrans", "MirrorScaleY", ModPrefs.GetFloat("PortableMirrorTrans", "MirrorScaleY") - .25f);
                    OnModSettingsApplied();
                    mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
                }
            });

            //8
            mirrorMenu.AddSimpleButton($"Close", () => {
                mirrorMenu.Hide();
            });
            mirrorMenu.AddSimpleButton($"Page 1", () => {
                mirrorMenu.Hide();
                QuickMenuOptions();
            });

            mirrorMenu.Show();
        }

        public override void OnUpdate()
        {
            if (Utils.GetVRCPlayer() == null) return;
            // Toggle portable mirror
            if (Utils.GetKeyDown(_mirrorKeybindBase))
            {
                ToggleMirror();
            }
        }

        private void SetAllMirrorsToIgnoreShader()
        {
            foreach (var vrcMirrorReflection in UnityEngine.Object.FindObjectsOfType<VRC_MirrorReflection>()) // https://github.com/knah/VRCMods/blob/master/MirrorResolutionUnlimiter/UiExtensionsAddon.cs
                if (vrcMirrorReflection.isActiveAndEnabled && vrcMirrorReflection.gameObject.transform.parent.gameObject != (_mirrorBase || _mirror45 || _mirrorCeiling || _mirrorMicro || _mirrorTrans))
                    vrcMirrorReflection.m_ReflectLayers = vrcMirrorReflection.m_ReflectLayers.value ^ reserved2; //Force all mirrors to not reflect "Mirror/TransparentBackground" - Set all mirrors to exclude reserved2
        }

            private void ToggleMirror()
        {
            if (_mirrorBase != null)
            {
                try{ UnityEngine.Object.Destroy(_mirrorBase); } catch (System.Exception ex) { MelonModLogger.Log(ConsoleColor.DarkRed, ex.ToString()); }
                _mirrorBase = null;
            }
            else
            {
                if (_mirrorStateBase == "MirrorCutout" || _mirrorStateBase == "MirrorTransparent") SetAllMirrorsToIgnoreShader();
                VRCPlayer player = Utils.GetVRCPlayer();
                Vector3 pos = player.transform.position + player.transform.forward + (player.transform.forward * _MirrorDistance);
                pos.y += .5f;
                pos.y += (_mirrorScaleYBase - 1)  / 2;

                GameObject mirror = GameObject.Instantiate(mirrorPrefab);
                mirror.transform.position = pos;
                mirror.transform.rotation = player.transform.rotation;
                mirror.transform.localScale = new Vector3(_mirrorScaleXBase, _mirrorScaleYBase, 1f);
                mirror.name = "PortableMirror";

                var childMirror = mirror.transform.Find(_mirrorStateBase);
                childMirror.gameObject.active = true;
                if (_mirrorStateBase == "MirrorTransparent") childMirror.GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue);
                mirror.GetOrAddComponent<VRC_Pickup>().proximity = 3f;
                mirror.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupMirrorBase;
                mirror.GetOrAddComponent<VRC_Pickup>().allowManipulationWhenEquipped = false;

                _mirrorBase = mirror;
            }
        }

        private void ToggleMirror45()
        {
            if (_mirror45 != null)
            {
                try{ UnityEngine.Object.Destroy(_mirror45); } catch (System.Exception ex) { MelonModLogger.Log(ConsoleColor.DarkRed, ex.ToString()); }
                _mirror45 = null;
            }
            else
            {
                if (_mirrorState45 == "MirrorCutout" || _mirrorState45 == "MirrorTransparent") SetAllMirrorsToIgnoreShader();
                VRCPlayer player = Utils.GetVRCPlayer();
                Vector3 pos = player.transform.position + player.transform.forward + (player.transform.forward * _MirrorDistance45);
                pos.y += .5f;
                pos.y += (_mirrorScaleY45 - 1) / 2;
                GameObject mirror = GameObject.Instantiate(mirrorPrefab);
                mirror.transform.position = pos;
                mirror.transform.rotation = player.transform.rotation;
                mirror.transform.rotation = mirror.transform.rotation * Quaternion.AngleAxis(45, Vector3.left);  // Sets the transform's current rotation to a new rotation that rotates 30 degrees around the y-axis(Vector3.up)
                mirror.transform.localScale = new Vector3(_mirrorScaleX45, _mirrorScaleY45, 1f);
                mirror.name = "PortableMirror45";

                var childMirror = mirror.transform.Find(_mirrorState45);
                childMirror.gameObject.active = true;
                if (_mirrorState45 == "MirrorTransparent") childMirror.GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue);
                mirror.GetOrAddComponent<VRC_Pickup>().proximity = 3f;
                mirror.GetOrAddComponent<VRC_Pickup>().pickupable = _CanPickup45Mirror;
                mirror.GetOrAddComponent<VRC_Pickup>().allowManipulationWhenEquipped = false;
                _mirror45 = mirror;

            }
        }

        private void ToggleMirrorCeiling()
        {
            
            if (_mirrorCeiling != null)
            {
                try { UnityEngine.Object.Destroy(_mirrorCeiling); } catch (System.Exception ex) { MelonModLogger.Log(ConsoleColor.DarkRed, ex.ToString()); }
                _mirrorCeiling = null;
            }
            else
            {
                if (_mirrorStateCeiling == "MirrorCutout" || _mirrorStateCeiling == "MirrorTransparent") SetAllMirrorsToIgnoreShader();
                VRCPlayer player = Utils.GetVRCPlayer();
                Vector3 pos = GameObject.Find(player.gameObject.name + "/AnimationController/HeadAndHandIK/HipTarget").transform.position + (player.transform.up); // Bases mirror position off of hip, to allow for play space moving 
                MelonModLogger.Log($"x:{GameObject.Find(player.gameObject.name + "/AnimationController/HeadAndHandIK/HipTarget").transform.position.x}, y:{GameObject.Find(player.gameObject.name + "/AnimationController/HeadAndHandIK/HipTarget").transform.position.y}, z:{GameObject.Find(player.gameObject.name + "/AnimationController/HeadAndHandIK/HipTarget").transform.position.z}");
                pos.y += _MirrorDistanceCeiling;
                GameObject mirror = GameObject.Instantiate(mirrorPrefab);
                mirror.transform.position = pos;
                mirror.transform.rotation = player.transform.rotation;
                mirror.transform.rotation = Quaternion.AngleAxis(90, Vector3.left);  // Sets the transform's current rotation to a new rotation that rotates 30 degrees around the y-axis(Vector3.up)
                mirror.transform.localScale = new Vector3(_mirrorScaleXCeiling, _mirrorScaleZCeiling, 1f);
                mirror.name = "PortableMirrorCeiling";

                var childMirror = mirror.transform.Find(_mirrorStateCeiling);
                childMirror.gameObject.active = true;
                if (_mirrorStateCeiling == "MirrorTransparent") childMirror.GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue); 
                mirror.GetOrAddComponent<VRC_Pickup>().proximity = 3f;
                mirror.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupCeilingMirror;
                mirror.GetOrAddComponent<VRC_Pickup>().allowManipulationWhenEquipped = false;
                _mirrorCeiling = mirror;

            }
        }

        private void ToggleMirrorMicro()
        {
            if (_mirrorMicro != null)
            {
                try{ UnityEngine.Object.Destroy(_mirrorMicro); } catch (System.Exception ex) { MelonModLogger.Log(ConsoleColor.DarkRed, ex.ToString()); }
                _mirrorMicro = null;
            }
            else
            {
                if (_mirrorStateMicro == "MirrorCutout" || _mirrorStateMicro == "MirrorTransparent") SetAllMirrorsToIgnoreShader();
                VRCPlayer player = Utils.GetVRCPlayer();
                Vector3 pos = GameObject.Find(player.gameObject.name + "/AnimationController/HeadAndHandIK/HeadEffector").transform.position + (player.transform.forward * _mirrorScaleMicro); // Gets position of Head and moves mirror forward by the Y scale.
                pos.y -= _mirrorScaleMicro / 4;///This will need turning
                GameObject mirror = GameObject.Instantiate(mirrorPrefab);
                mirror.transform.position = pos;
                mirror.transform.rotation = player.transform.rotation;
                mirror.transform.localScale = new Vector3(_mirrorScaleXMicro, _mirrorScaleMicro, 1f);
                mirror.name = "PortableMirrorMicro";

                var childMirror = mirror.transform.Find(_mirrorStateMicro);
                childMirror.gameObject.active = true;
                if (_mirrorStateMicro == "MirrorTransparent") childMirror.GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue);
                mirror.GetOrAddComponent<VRC_Pickup>().proximity = _grabRangeMicro;
                mirror.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupMirrorMicro;
                mirror.GetOrAddComponent<VRC_Pickup>().allowManipulationWhenEquipped = false;
                _mirrorMicro = mirror;
            }
        }

        private void ToggleMirrorTrans()
        {
            if (_mirrorTrans != null)
            {
                try{ UnityEngine.Object.Destroy(_mirrorTrans); } catch (System.Exception ex) { MelonModLogger.Log(ConsoleColor.DarkRed, ex.ToString()); }
                _mirrorTrans = null;
            }
            else
            {
                if(_mirrorStateTrans == "MirrorCutout" || _mirrorStateTrans == "MirrorTransparent") SetAllMirrorsToIgnoreShader();
                VRCPlayer player = Utils.GetVRCPlayer();
                Vector3 pos = player.transform.position + player.transform.forward + (player.transform.forward * _MirrorDistanceTrans);
                pos.y += .5f;
                pos.y += (_mirrorScaleYTrans - 1) / 2;
                GameObject mirror = GameObject.Instantiate(mirrorPrefab);
                mirror.transform.position = pos;
                mirror.transform.rotation = player.transform.rotation;
                mirror.transform.localScale = new Vector3(_mirrorScaleXTrans, _mirrorScaleYTrans, 1f);
                mirror.name = "PortableMirrorTrans";

                var childMirror = mirror.transform.Find(_mirrorStateTrans);
                childMirror.gameObject.active = true;
                if (_mirrorStateTrans == "MirrorTransparent") childMirror.GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue);
                mirror.GetOrAddComponent<VRC_Pickup>().proximity = 3f;
                mirror.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupMirrorTrans;
                mirror.GetOrAddComponent<VRC_Pickup>().allowManipulationWhenEquipped = false;

                _mirrorTrans = mirror;
            }
        }

        
        private void loadAssets()
        {//https://github.com/ddakebono/BTKSASelfPortrait/blob/master/BTKSASelfPortrait.cs
            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PortableMirror_Combined.transmirror"))
            {//Load shaders first, without this doesn't seem to work. 
                using (var tempStream = new MemoryStream((int)assetStream.Length))
                {
                    assetStream.CopyTo(tempStream);
                    assetBundle2 = AssetBundle.LoadFromMemory_Internal(tempStream.ToArray(), 0);
                    assetBundle2.hideFlags |= HideFlags.DontUnloadUnusedAsset;
                }
            }

            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PortableMirror_Combined.mirrorprefab"))
            {
                using (var tempStream = new MemoryStream((int)assetStream.Length))
                {
                    assetStream.CopyTo(tempStream);
                    assetBundle = AssetBundle.LoadFromMemory_Internal(tempStream.ToArray(), 0);
                    assetBundle.hideFlags |= HideFlags.DontUnloadUnusedAsset;
                }
            }

            if (assetBundle != null)
            {
                mirrorPrefab = assetBundle.LoadAsset_Internal("MirrorPrefab", Il2CppType.Of<GameObject>()).Cast<GameObject>();
                mirrorPrefab.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            }
            else MelonModLogger.LogError("Bundle was null");
        }

        public Dictionary<string, Transform> ButtonList = new Dictionary<string, Transform>();

        private static int PlayerLayer = 1 << 9; // https://github.com/knah/VRCMods/blob/master/MirrorResolutionUnlimiter/UiExtensionsAddon.cs
        private static int PlayerLocalLayer = 1 << 10; //Mainly just here as a refernce now
        private static int UiLayer = 1 << 5;
        private static int UiMenuLayer = 1 << 12;
        private static int MirrorReflectionLayer = 1 << 18;
        private static int reserved2 = 1 << 19;

        private int optMirrorMask = PlayerLayer | MirrorReflectionLayer;
        private int fullMirrorMask = -1 & ~UiLayer & ~UiMenuLayer & ~PlayerLocalLayer & ~reserved2;

        private AssetBundle assetBundle;
        private AssetBundle assetBundle2;
        private GameObject mirrorPrefab;

        private GameObject _mirrorBase;
        private float _mirrorScaleXBase;
        private float _mirrorScaleYBase;
        private float _MirrorDistance;
        private float _oldMirrorDistance;
        private float _oldMirrorScaleYBase;
        private bool _canPickupMirrorBase;
        private KeyCode _mirrorKeybindBase;
        private bool _quickMenuOptions;
        private float _MirrorTransValue;
        private bool _enableBase;
        private string _mirrorStateBase;

        private GameObject _mirror45;
        private float _mirrorScaleX45;
        private float _mirrorScaleY45;
        private float _MirrorDistance45;
        private float _oldMirrorDistance45;
        private float _oldMirrorScaleY45;
        private bool _CanPickup45Mirror;
        private bool _enable45;
        private string _mirrorState45;

        private GameObject _mirrorCeiling;
        private float _mirrorScaleXCeiling;
        private float _mirrorScaleZCeiling;
        private float _MirrorDistanceCeiling;
        private float _oldMirrorDistanceCeiling;
        private bool _canPickupCeilingMirror;
        private bool _enableCeiling;
        private string _mirrorStateCeiling;

        private GameObject _mirrorMicro;
        private float _mirrorScaleXMicro;
        private float _mirrorScaleMicro;
        private float _grabRangeMicro;
        private float _oldMirrorScaleYMicro;
        private bool _canPickupMirrorMicro;
        private bool _enableMicro;
        private string _mirrorStateMicro;

        private GameObject _mirrorTrans;
        private float _mirrorScaleXTrans;
        private float _mirrorScaleYTrans;
        private float _MirrorDistanceTrans;
        private float _oldMirrorDistanceTrans;
        private float _oldMirrorScaleYTrans;
        private bool _canPickupMirrorTrans;
        private bool _enableTrans;
        private string _mirrorStateTrans;


    }
}


namespace UIExpansionKit.API
{

    public struct LayoutDescriptionCustom
    {
        public static LayoutDescription QuickMenu3Column = new LayoutDescription { NumColumns = 3, RowHeight = 380 / 8, NumRows = 8 };
    }
}