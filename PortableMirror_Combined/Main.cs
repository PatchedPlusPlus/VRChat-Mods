﻿using System;
using System.Linq;
using System.Collections;
using System.Reflection;
using MelonLoader;
using UIExpansionKit.API;
using UnityEngine;
using VRC.SDKBase;
using System.Collections.Generic;
using UnhollowerRuntimeLib;
using System.IO;



[assembly: MelonInfo(typeof(PortableMirror.Main), "PortableMirrorMod", "1.4.6", "M-oons, Nirvash")] //Name changed to break auto update
[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonOptionalDependencies("ActionMenuApi")]

namespace PortableMirror
{

    public class Main : MelonMod
    {
        public override void OnApplicationStart()
        {
            loadAssets();

            MelonPreferences.CreateCategory("PortableMirror", "PortableMirror");
            MelonPreferences.CreateEntry<float>("PortableMirror", "MirrorScaleX", 5f, "Mirror Scale X");
            MelonPreferences.CreateEntry<float>("PortableMirror", "MirrorScaleY", 3f, "Mirror Scale Y");
            MelonPreferences.CreateEntry<float>("PortableMirror", "MirrorDistance", 0f, "Mirror Distance");
            MelonPreferences.CreateEntry<string>("PortableMirror", "MirrorState", "MirrorFull", "Mirror Type");
            ExpansionKitApi.RegisterSettingAsStringEnum("PortableMirror", "MirrorState", new[] { ("MirrorFull", "Full"), ("MirrorOpt", "Optimized"), ("MirrorCutout", "Cutout"), ("MirrorTransparent", "Transparent") });
            MelonPreferences.CreateEntry<bool>("PortableMirror", "CanPickupMirror", false, "Can Pickup Mirror");
            MelonPreferences.CreateEntry<bool>("PortableMirror", "enableBase", true, "Enable Portable Mirror Quick Menu Button");
            MelonPreferences.CreateEntry<string>("PortableMirror", "MirrorKeybind", "Alpha1", "Toggle Mirror Keybind");
            MelonPreferences.CreateEntry<bool>("PortableMirror", "MirrorKeybindEnabled", true, "Enabled Mirror Keybind");
            MelonPreferences.CreateEntry<bool>("PortableMirror", "QuickMenuOptions", true, "Enable Settings Quick Menu Button");
            MelonPreferences.CreateEntry<bool>("PortableMirror", "OpenLastQMpage", false, "Quick Menu Settings remembers last page opened");
            MelonPreferences.CreateEntry<float>("PortableMirror", "TransMirrorTrans", .4f, "Transparent Mirror transparency - Higher is more transparent - Global for all mirrors");
            MelonPreferences.CreateEntry<bool>("PortableMirror", "MirrorsShowInCamera", false, "Mirrors show in Cameras - Global for all mirrors");
            MelonPreferences.CreateEntry<bool>("PortableMirror", "ActionMenu", true, "Enable Controls on Action Menu (Requires Restart)");

            MelonPreferences.CreateCategory("PortableMirror45", "PortableMirror 45 Degree");
            MelonPreferences.CreateEntry<float>("PortableMirror45", "MirrorScaleX", 5f, "Mirror Scale X");
            MelonPreferences.CreateEntry<float>("PortableMirror45", "MirrorScaleY", 4f, "Mirror Scale Y");
            MelonPreferences.CreateEntry<float>("PortableMirror45", "MirrorDistance", 0f, "Mirror Distance");
            MelonPreferences.CreateEntry<string>("PortableMirror45", "MirrorState", "MirrorFull", "Mirror Type");
            ExpansionKitApi.RegisterSettingAsStringEnum("PortableMirror45", "MirrorState", new[] { ("MirrorFull", "Full"), ("MirrorOpt", "Optimized"), ("MirrorCutout", "Cutout"), ("MirrorTransparent", "Transparent") });
            MelonPreferences.CreateEntry<bool>("PortableMirror45", "CanPickup45Mirror", false, "Can Pickup 45 Mirror");
            MelonPreferences.CreateEntry<bool>("PortableMirror45", "enable45", true, "Enable 45 Mirror QM Button");
            
            MelonPreferences.CreateCategory("PortableMirrorCeiling", "PortableMirror Ceiling");
            MelonPreferences.CreateEntry<float>("PortableMirrorCeiling", "MirrorScaleX", 5f, "Mirror Scale X");
            MelonPreferences.CreateEntry<float>("PortableMirrorCeiling", "MirrorScaleZ", 5f, "Mirror Scale Z");
            MelonPreferences.CreateEntry<float>("PortableMirrorCeiling", "MirrorDistance", 2, "Mirror Distance");
            MelonPreferences.CreateEntry<string>("PortableMirrorCeiling", "MirrorState", "MirrorFull", "Mirror Type");
            ExpansionKitApi.RegisterSettingAsStringEnum("PortableMirrorCeiling", "MirrorState", new[] { ("MirrorFull", "Full"), ("MirrorOpt", "Optimized"), ("MirrorCutout", "Cutout"), ("MirrorTransparent", "Transparent") });
            MelonPreferences.CreateEntry<bool>("PortableMirrorCeiling", "CanPickupCeilingMirror", false, "Can Pickup Ceiling Mirror");
            MelonPreferences.CreateEntry<bool>("PortableMirrorCeiling", "enableCeiling", true, "Enable Ceiling Mirror QM Button");

            MelonPreferences.CreateCategory("PortableMirrorMicro", "PortableMirror Micro");
            MelonPreferences.CreateEntry<float>("PortableMirrorMicro", "MirrorScaleX", .05f, "Mirror Scale X");
            MelonPreferences.CreateEntry<float>("PortableMirrorMicro", "MirrorScaleY", .1f, "Mirror Scale Y");
            MelonPreferences.CreateEntry<float>("PortableMirrorMicro", "GrabRange", .1f, "GrabRange");
            MelonPreferences.CreateEntry<string>("PortableMirrorMicro", "MirrorState", "MirrorFull", "Mirror Type");
            ExpansionKitApi.RegisterSettingAsStringEnum("PortableMirrorMicro", "MirrorState", new[] { ("MirrorFull", "Full"), ("MirrorOpt", "Optimized"), ("MirrorCutout", "Cutout"), ("MirrorTransparent", "Transparent") });
            MelonPreferences.CreateEntry<bool>("PortableMirrorMicro", "CanPickupMirrorMicro", false, "Can Pickup MirrorMicro");
            MelonPreferences.CreateEntry<bool>("PortableMirrorMicro", "enableMicro", true, "Enable Micro Mirror QM Button");
            
            MelonPreferences.CreateCategory("PortableMirrorTrans", "PortableMirror Transparent");
            MelonPreferences.CreateEntry<float>("PortableMirrorTrans", "MirrorScaleX", 5f, "Mirror Scale X");
            MelonPreferences.CreateEntry<float>("PortableMirrorTrans", "MirrorScaleY", 3f, "Mirror Scale Y");
            MelonPreferences.CreateEntry<float>("PortableMirrorTrans", "MirrorDistance", 0f, "Mirror Distance");
            MelonPreferences.CreateEntry<string>("PortableMirrorTrans", "MirrorState", "MirrorTransparent", "Mirror Type - Resets to Transparent on load");
            ExpansionKitApi.RegisterSettingAsStringEnum("PortableMirrorTrans", "MirrorState", new[] { ("MirrorFull", "Full"), ("MirrorOpt", "Optimized"), ("MirrorCutout", "Cutout"), ("MirrorTransparent", "Transparent") });
            MelonPreferences.SetEntryValue<string>("PortableMirrorTrans", "MirrorState", "MirrorTransparent");//Force to Transparent every load
            MelonPreferences.CreateEntry<bool>("PortableMirrorTrans", "CanPickupMirror", false, "Can Pickup Mirror");
            MelonPreferences.CreateEntry<bool>("PortableMirrorTrans", "enableTrans", true, "Enable Transparent Mirror QM Button");

            OnPreferencesSaved();

            MelonLogger.Msg("Base mod made by M-oons, modifications by Nirvash");
            MelonLogger.Msg($"[{_mirrorKeybindBase}] -> Toggle portable mirror");

            MelonMod uiExpansionKit = MelonHandler.Mods.Find(m => m.Info.Name == "UI Expansion Kit");
            if (uiExpansionKit != null)
            {
                uiExpansionKit.Info.SystemType.Assembly.GetTypes().First(t => t.FullName == "UIExpansionKit.API.ExpansionKitApi").GetMethod("RegisterWaitConditionBeforeDecorating", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Invoke(null, new object[]
                {
                    CreateQuickMenuButton()
                });
            }

            if (MelonHandler.Mods.Any(m => m.Info.Name == "ActionMenuApi") && MelonPreferences.GetEntryValue<bool>("PortableMirror", "ActionMenu"))
            {
                CustomActionMenu.InitUi();
            }
            else MelonLogger.Msg("ActionMenuApi is missing, or setting is toggled off in Mod Settings - Not adding controls to ActionMenu");


        }

        public override void OnPreferencesSaved()
        {
            _enableBase = MelonPreferences.GetEntryValue<bool>("PortableMirror", "enableBase");
            _enable45 = MelonPreferences.GetEntryValue<bool>("PortableMirror45", "enable45");
            _enableCeiling = MelonPreferences.GetEntryValue<bool>("PortableMirrorCeiling", "enableCeiling");
            _enableMicro = MelonPreferences.GetEntryValue<bool>("PortableMirrorMicro", "enableMicro");
            _enableTrans = MelonPreferences.GetEntryValue<bool>("PortableMirrorTrans", "enableTrans");
            _quickMenuOptions = MelonPreferences.GetEntryValue<bool>("PortableMirror", "QuickMenuOptions");
            if (ButtonList.ContainsKey("Base") && ButtonList["Base"] != null) ButtonList["Base"].gameObject.SetActive(_enableBase);
            if (ButtonList.ContainsKey("45") && ButtonList["45"] != null) ButtonList["45"].gameObject.SetActive(_enable45);
            if (ButtonList.ContainsKey("Ceiling") && ButtonList["Ceiling"] != null) ButtonList["Ceiling"].gameObject.SetActive(_enableCeiling);
            if (ButtonList.ContainsKey("Micro") && ButtonList["Micro"] != null) ButtonList["Micro"].gameObject.SetActive(_enableMicro);
            if (ButtonList.ContainsKey("Trans") && ButtonList["Trans"] != null) ButtonList["Trans"].gameObject.SetActive(_enableTrans);
            if (ButtonList.ContainsKey("Settings") && ButtonList["Settings"] != null) ButtonList["Settings"].gameObject.SetActive(_quickMenuOptions);

            _MirrorTransValue = MelonPreferences.GetEntryValue<float>("PortableMirror", "TransMirrorTrans");
            _MirrorsShowInCamera = MelonPreferences.GetEntryValue<bool>("PortableMirror", "MirrorsShowInCamera");
            _openLastQMpage = MelonPreferences.GetEntryValue<bool>("PortableMirror", "OpenLastQMpage");

            _oldMirrorScaleYBase = _mirrorScaleYBase;
            _oldMirrorDistance = _MirrorDistance;
            _mirrorScaleXBase = MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorScaleX");
            _mirrorScaleYBase = MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorScaleY");
            _MirrorDistance = MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorDistance");
            _canPickupMirrorBase = MelonPreferences.GetEntryValue<bool>("PortableMirror", "CanPickupMirror");
            _mirrorKeybindBase = Utils.GetMirrorKeybind();
            _mirrorKeybindEnabled = MelonPreferences.GetEntryValue<bool>("PortableMirror", "MirrorKeybindEnabled");
            _mirrorStateBase = MelonPreferences.GetEntryValue<string>("PortableMirror", "MirrorState");

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
                childMirror.gameObject.layer = _MirrorsShowInCamera ? 4 : 10;
            }


            _oldMirrorScaleY45 = _mirrorScaleY45;
            _oldMirrorDistance45 = _MirrorDistance45;
            _mirrorScaleX45 = MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorScaleX");
            _mirrorScaleY45 = MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorScaleY");
            _MirrorDistance45 = MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorDistance");
            _CanPickup45Mirror = MelonPreferences.GetEntryValue<bool>("PortableMirror45", "CanPickup45Mirror");
            _mirrorState45 = MelonPreferences.GetEntryValue<string>("PortableMirror45", "MirrorState");

            if (_mirror45 != null && Utils.GetVRCPlayer() != null)
            {
                _mirror45.transform.localScale = new Vector3(_mirrorScaleX45, _mirrorScaleY45, 1f);
                _mirror45.transform.rotation = _mirror45.transform.rotation * Quaternion.AngleAxis(-45, Vector3.left);
                _mirror45.transform.position = new Vector3(_mirror45.transform.position.x, _mirror45.transform.position.y + ((_mirrorScaleY45 - _oldMirrorScaleY45)/2.5f), _mirror45.transform.position.z  );
                _mirror45.transform.position += _mirror45.transform.forward * (_MirrorDistance45 - _oldMirrorDistance45);
                _mirror45.transform.rotation = _mirror45.transform.rotation * Quaternion.AngleAxis(45, Vector3.left);

                _mirror45.GetOrAddComponent<VRC_Pickup>().pickupable = _CanPickup45Mirror;

                if (_mirrorState45 == "MirrorCutout" || _mirrorState45 == "MirrorTransparent") SetAllMirrorsToIgnoreShader();
                if (_mirrorState45 == "MirrorTransparent") _mirror45.transform.Find(_mirrorState45).GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue);
                for (int i = 0; i < _mirror45.transform.childCount; i++)
                    _mirror45.transform.GetChild(i).gameObject.active = false;
                var childMirror = _mirror45.transform.Find(_mirrorState45);
                childMirror.gameObject.active = true;
                childMirror.gameObject.layer = _MirrorsShowInCamera ? 4 : 10;
            }


            _oldMirrorDistanceCeiling = _MirrorDistanceCeiling;
            _mirrorScaleXCeiling = MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleX");
            _mirrorScaleZCeiling = MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleZ");
            _MirrorDistanceCeiling = MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorDistance");
            _canPickupCeilingMirror = MelonPreferences.GetEntryValue<bool>("PortableMirrorCeiling", "CanPickupCeilingMirror");
            _mirrorStateCeiling = MelonPreferences.GetEntryValue<string>("PortableMirrorCeiling", "MirrorState");

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
                childMirror.gameObject.layer = _MirrorsShowInCamera ? 4 : 10;
            }


            _oldMirrorScaleYMicro = _mirrorScaleMicro;
            _mirrorScaleXMicro = MelonPreferences.GetEntryValue<float>("PortableMirrorMicro", "MirrorScaleX");
            _mirrorScaleMicro = MelonPreferences.GetEntryValue<float>("PortableMirrorMicro", "MirrorScaleY");
            _grabRangeMicro = MelonPreferences.GetEntryValue<float>("PortableMirrorMicro", "GrabRange");
            _canPickupMirrorMicro = MelonPreferences.GetEntryValue<bool>("PortableMirrorMicro", "CanPickupMirrorMicro");
            _mirrorStateMicro = MelonPreferences.GetEntryValue<string>("PortableMirrorMicro", "MirrorState");

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
                childMirror.gameObject.layer = _MirrorsShowInCamera ? 4 : 10;
            }


            _oldMirrorScaleYTrans = _mirrorScaleYTrans;
            _oldMirrorDistanceTrans = _MirrorDistanceTrans;
            _mirrorScaleXTrans = MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorScaleX");
            _mirrorScaleYTrans = MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorScaleY");
            _MirrorDistanceTrans = MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorDistance");
            _canPickupMirrorTrans = MelonPreferences.GetEntryValue<bool>("PortableMirrorTrans", "CanPickupMirror");
            _mirrorStateTrans = MelonPreferences.GetEntryValue<string>("PortableMirrorTrans", "MirrorState");

            if (_mirrorTrans != null && Utils.GetVRCPlayer() != null)
            {
                _mirrorTrans.transform.localScale = new Vector3(_mirrorScaleXTrans, _mirrorScaleYTrans, 1f);
                _mirrorTrans.transform.position = new Vector3(_mirrorTrans.transform.position.x, _mirrorTrans.transform.position.y + ((_mirrorScaleYTrans - _oldMirrorScaleYTrans) / 2), _mirrorTrans.transform.position.z);
                _mirrorTrans.transform.position += _mirrorTrans.transform.forward * (_MirrorDistanceTrans - _oldMirrorDistanceTrans);

                _mirrorTrans.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupMirrorTrans;

                if (_mirrorStateTrans == "MirrorCutout" || _mirrorStateTrans == "MirrorTransparent") SetAllMirrorsToIgnoreShader();
                if (_mirrorStateTrans == "MirrorTransparent") _mirrorTrans.transform.Find(_mirrorStateTrans).GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue);
                for (int i = 0; i < _mirrorTrans.transform.childCount; i++)
                    _mirrorTrans.transform.GetChild(i).gameObject.active = false;
                var childMirror = _mirrorTrans.transform.Find(_mirrorStateTrans);
                childMirror.gameObject.active = true;
                childMirror.gameObject.layer = _MirrorsShowInCamera ? 4 : 10;
            }


        }

        private IEnumerator CreateQuickMenuButton()
        {
            while (QuickMenu.prop_QuickMenu_0 == null) yield return null;

            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Toggle\nPortable\nMirror", () =>
            {
                if (Utils.GetVRCPlayer() != null) ToggleMirror();
            }, (button) => { ButtonList["Base"] = button.transform; button.gameObject.SetActive(_enableBase); });
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Toggle\nPortable\nMirror 45", () =>
            {
                if (Utils.GetVRCPlayer() != null) ToggleMirror45();
            }, (button) => { ButtonList["45"] = button.transform; button.gameObject.SetActive(_enable45); });                  
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Toggle\nCeiling\nMirror", () =>
            {
                if (Utils.GetVRCPlayer() != null) ToggleMirrorCeiling(); ;
            }, (button) => { ButtonList["Ceiling"] = button.transform; button.gameObject.SetActive(_enableCeiling); });
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Toggle\nMicro\nMirror", () =>
            {
                if (Utils.GetVRCPlayer() != null) ToggleMirrorMicro();   
            }, (button) => { ButtonList["Micro"] = button.transform; button.gameObject.SetActive(_enableMicro); });
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Toggle\nTransparent\nMirror", () =>
            {
                if (Utils.GetVRCPlayer() != null) ToggleMirrorTrans();
            }, (button) => { ButtonList["Trans"] = button.transform; button.gameObject.SetActive(_enableTrans); });
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Portable\nMirror\nSettings", () =>
            {
                if (_openLastQMpage && _qmOptionsLastPage == 2) QuickMenuOptions2();
                    else QuickMenuOptions();
            }, (button) => { ButtonList["Settings"] = button.transform; button.gameObject.SetActive(_quickMenuOptions); });
        }

        public static string StateText(string stateRaw)
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
        public static void ToggleMirrorState(string MelonPrefCat, string mirrorState)
        {
            if (mirrorState == "MirrorFull") MelonPreferences.SetEntryValue<string>(MelonPrefCat, "MirrorState", "MirrorOpt");
            else if (mirrorState == "MirrorOpt") MelonPreferences.SetEntryValue<string>(MelonPrefCat, "MirrorState", "MirrorCutout");
            else if (mirrorState == "MirrorCutout") MelonPreferences.SetEntryValue<string>(MelonPrefCat, "MirrorState", "MirrorTransparent");
            else if (mirrorState == "MirrorTransparent") MelonPreferences.SetEntryValue<string>(MelonPrefCat, "MirrorState", "MirrorFull");
        }

        private void QuickMenuOptions()
        {
            var mirrorMenu = ExpansionKitApi.CreateCustomQuickMenuPage(LayoutDescriptionCustom.QuickMenu3Column);
            _qmOptionsLastPage = 1;

            mirrorMenu.AddToggleButton("Portable Mirror", (action) =>
            {
                if (Utils.GetVRCPlayer() != null) ToggleMirror();
            }, () => _mirrorBase != null);
            mirrorMenu.AddSimpleButton(StateText(_mirrorStateBase), () =>
            {
                ToggleMirrorState("PortableMirror", _mirrorStateBase);
                OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions();
            });
            mirrorMenu.AddSimpleButton(MelonPreferences.GetEntryValue<bool>("PortableMirror", "CanPickupMirror") ? "Pickupable" : "Not Pickupable", () => {
                MelonPreferences.SetEntryValue<bool>("PortableMirror", "CanPickupMirror", !MelonPreferences.GetEntryValue<bool>("PortableMirror", "CanPickupMirror"));
                OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions();
            });
            mirrorMenu.AddLabel($"Distance: {MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorDistance")}");
            mirrorMenu.AddSimpleButton("+", () => {
                MelonPreferences.SetEntryValue<float>("PortableMirror", "MirrorDistance", MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorDistance") + .25f );
                OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions();
            });
            mirrorMenu.AddSimpleButton("-", () => {
                MelonPreferences.SetEntryValue<float>("PortableMirror", "MirrorDistance", MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorDistance") - .25f);
                OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions();
            });
            if (true)//(_enable45)
            {
                mirrorMenu.AddToggleButton("45 Mirror", (action) =>
                {
                    if (Utils.GetVRCPlayer() != null) ToggleMirror45();
                }, () => _mirror45 != null);
                mirrorMenu.AddSimpleButton(StateText(_mirrorState45), () =>
                {
                    ToggleMirrorState("PortableMirror45", _mirrorState45);
                    OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
                mirrorMenu.AddSimpleButton(MelonPreferences.GetEntryValue<bool>("PortableMirror45", "CanPickup45Mirror") ? "Pickupable" : "Not Pickupable", () =>
                {
                    MelonPreferences.SetEntryValue<bool>("PortableMirror45", "CanPickup45Mirror", !MelonPreferences.GetEntryValue<bool>("PortableMirror45", "CanPickup45Mirror"));
                    OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
                mirrorMenu.AddLabel($"Distance: {MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorDistance")}");
                mirrorMenu.AddSimpleButton("+", () => {
                    MelonPreferences.SetEntryValue<float>("PortableMirror45", "MirrorDistance", MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorDistance") + .25f);
                    OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
                mirrorMenu.AddSimpleButton("-", () => {
                    MelonPreferences.SetEntryValue<float>("PortableMirror45", "MirrorDistance", MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorDistance") - .25f);
                    OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
            }
            if (true)//(_enableCeiling)
            {
                mirrorMenu.AddToggleButton("Ceiling Mirror", (action) =>
                {
                    if (Utils.GetVRCPlayer() != null) ToggleMirrorCeiling();
                }, () => _mirrorCeiling != null);
                mirrorMenu.AddSimpleButton(StateText(_mirrorStateCeiling), () =>
                {
                    ToggleMirrorState("PortableMirrorCeiling", _mirrorStateCeiling);
                    OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
                mirrorMenu.AddSimpleButton(MelonPreferences.GetEntryValue<bool>("PortableMirrorCeiling", "CanPickupCeilingMirror") ? "Pickupable" : "Not Pickupable", () =>
                {
                    MelonPreferences.SetEntryValue<bool>("PortableMirrorCeiling", "CanPickupCeilingMirror", !MelonPreferences.GetEntryValue<bool>("PortableMirrorCeiling", "CanPickupCeilingMirror"));
                    OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
                mirrorMenu.AddLabel($"Distance: {MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorDistance")}");
                mirrorMenu.AddSimpleButton("+", () => {
                    MelonPreferences.SetEntryValue<float>("PortableMirrorCeiling", "MirrorDistance", MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorDistance") + .25f);
                    OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
                mirrorMenu.AddSimpleButton("-", () => {
                    MelonPreferences.SetEntryValue<float>("PortableMirrorCeiling", "MirrorDistance", MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorDistance") - .25f);
                    OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
            }
            if (true)//(_enableMicro)
            {
                mirrorMenu.AddToggleButton("Micro Mirror", (action) =>
                {
                    if (Utils.GetVRCPlayer() != null) ToggleMirrorMicro();
                }, () => _mirrorMicro != null);
                mirrorMenu.AddSimpleButton(StateText(_mirrorStateMicro), () =>
                {
                    ToggleMirrorState("PortableMirrorMicro", _mirrorStateMicro);
                    OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
                mirrorMenu.AddSimpleButton(MelonPreferences.GetEntryValue<bool>("PortableMirrorMicro", "CanPickupMirrorMicro") ? "Pickupable" : "Not Pickupable", () =>
                {
                    MelonPreferences.SetEntryValue<bool>("PortableMirrorMicro", "CanPickupMirrorMicro", !MelonPreferences.GetEntryValue<bool>("PortableMirrorMicro", "CanPickupMirrorMicro"));
                    OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
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
            _qmOptionsLastPage = 2;
            //Row 1
            mirrorMenu.AddToggleButton("Portable Mirror", (action) =>
            {
                if (Utils.GetVRCPlayer() != null) ToggleMirror();
            }, () => _mirrorBase != null);
            mirrorMenu.AddSimpleButton("Larger", () => {
                MelonPreferences.SetEntryValue<float>("PortableMirror", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorScaleX") + .25f);
                MelonPreferences.SetEntryValue<float>("PortableMirror", "MirrorScaleY", MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorScaleY") + .25f);
                OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });
            mirrorMenu.AddSimpleButton("Smaller", () => {
                if (MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorScaleX") > .25 && MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorScaleY") > .25)
                {
                    MelonPreferences.SetEntryValue<float>("PortableMirror", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorScaleX") - .25f);
                    MelonPreferences.SetEntryValue<float>("PortableMirror", "MirrorScaleY", MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorScaleY") - .25f);
                    OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions2();
                }
            });

            //2
            mirrorMenu.AddToggleButton("45 Mirror", (action) =>
            {
                if (Utils.GetVRCPlayer() != null) ToggleMirror45();
            }, () => _mirror45 != null);
            mirrorMenu.AddSimpleButton("Larger", () => {
                MelonPreferences.SetEntryValue<float>("PortableMirror45", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorScaleX") + .25f);
                MelonPreferences.SetEntryValue<float>("PortableMirror45", "MirrorScaleY", MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorScaleY") + .25f);
                OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });
            mirrorMenu.AddSimpleButton("Smaller", () => {
                if (MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorScaleX") > .25 && MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorScaleY") > .25)
                {
                    MelonPreferences.SetEntryValue<float>("PortableMirror45", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorScaleX") - .25f);
                    MelonPreferences.SetEntryValue<float>("PortableMirror45", "MirrorScaleY", MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorScaleY") - .25f);

                    OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions2();
                }
            });
            //3
            mirrorMenu.AddToggleButton("Ceiling Mirror", (action) =>
            {
                if (Utils.GetVRCPlayer() != null) ToggleMirrorCeiling();
            }, () => _mirrorCeiling != null);
            mirrorMenu.AddSimpleButton("Larger", () => {
                MelonPreferences.SetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleX") + .25f);
                MelonPreferences.SetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleZ", MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleZ") + .25f);

                OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });
            mirrorMenu.AddSimpleButton("Smaller", () => {
                if (MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleX") > .25 && MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleZ") > .25)
                {
                    MelonPreferences.SetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleX") - .25f);
                    MelonPreferences.SetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleZ", MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleZ") - .25f);

                    OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions2();
                }
            });
            //4
            mirrorMenu.AddToggleButton("Micro Mirror", (action) =>
            {
                if (Utils.GetVRCPlayer() != null) ToggleMirrorMicro();
            }, () => _mirrorMicro != null);
            
            mirrorMenu.AddSimpleButton("Larger", () => {
                MelonPreferences.SetEntryValue<float>("PortableMirrorMicro", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirrorMicro", "MirrorScaleX") + .01f);
                MelonPreferences.SetEntryValue<float>("PortableMirrorMicro", "MirrorScaleY", MelonPreferences.GetEntryValue<float>("PortableMirrorMicro", "MirrorScaleY") + .01f);
                OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });
         
            mirrorMenu.AddSimpleButton("Smaller", () => {
                if (MelonPreferences.GetEntryValue<float>("PortableMirrorMicro", "MirrorScaleX") > .02 && MelonPreferences.GetEntryValue<float>("PortableMirrorMicro", "MirrorScaleY") > .02)
                {
                    MelonPreferences.SetEntryValue<float>("PortableMirrorMicro", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirrorMicro", "MirrorScaleX") - .01f);
                    MelonPreferences.SetEntryValue<float>("PortableMirrorMicro", "MirrorScaleY", MelonPreferences.GetEntryValue<float>("PortableMirrorMicro", "MirrorScaleY") - .01f);

                    OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions2();
                }
            });

            //5
            mirrorMenu.AddToggleButton("Transparent", (action) =>
            {
                if (Utils.GetVRCPlayer() != null) ToggleMirrorTrans();
            }, () => _mirrorTrans != null);
           
            mirrorMenu.AddSimpleButton(StateText(_mirrorStateTrans), () =>
            {
                ToggleMirrorState("PortableMirrorTrans", _mirrorStateTrans);      
                OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });
         
            mirrorMenu.AddSimpleButton(MelonPreferences.GetEntryValue<bool>("PortableMirrorTrans", "CanPickupMirror") ? "Pickupable" : "Not Pickupable", () =>
            {
                MelonPreferences.SetEntryValue<bool>("PortableMirrorTrans", "CanPickupMirror", !MelonPreferences.GetEntryValue<bool>("PortableMirrorTrans", "CanPickupMirror"));
                OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });
            //6
            mirrorMenu.AddLabel($"Distance: {MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorDistance")}");
            mirrorMenu.AddSimpleButton("+", () => {
                MelonPreferences.SetEntryValue<float>("PortableMirrorTrans", "MirrorDistance", MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorDistance") + .25f);
                OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });
           
            mirrorMenu.AddSimpleButton("-", () => {
                MelonPreferences.SetEntryValue<float>("PortableMirrorTrans", "MirrorDistance", MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorDistance") - .25f);
                OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });
           //7
            mirrorMenu.AddSpacer();
            
            mirrorMenu.AddSimpleButton("Larger", () => {
                MelonPreferences.SetEntryValue<float>("PortableMirrorTrans", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorScaleX") + .25f);
                MelonPreferences.SetEntryValue<float>("PortableMirrorTrans", "MirrorScaleY", MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorScaleY") + .25f);
                OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });
            
            mirrorMenu.AddSimpleButton("Smaller", () => {
                if (MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorScaleX") > .25 && MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorScaleY") > .25)
                {
                    MelonPreferences.SetEntryValue<float>("PortableMirrorTrans", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorScaleX") - .25f);
                    MelonPreferences.SetEntryValue<float>("PortableMirrorTrans", "MirrorScaleY", MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorScaleY") - .25f);
                    OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions2();
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
            if (_mirrorKeybindEnabled && Utils.GetKeyDown(_mirrorKeybindBase))
            {
                ToggleMirror();
            }
        }

        private static void SetAllMirrorsToIgnoreShader()
        {
            foreach (var vrcMirrorReflection in UnityEngine.Object.FindObjectsOfType<VRC_MirrorReflection>())
            { // https://github.com/knah/VRCMods/blob/master/MirrorResolutionUnlimiter/UiExtensionsAddon.cs
                try
                {
                    //MelonLogger.Msg($"-----");
                    //MelonLogger.Msg($"{vrcMirrorReflection.gameObject.name}");
                    GameObject othermirror = vrcMirrorReflection?.gameObject?.transform?.parent?.gameObject; // Question marks are always the answer
                    //MelonLogger.Msg($"othermirror is null:{othermirror is null}, !=base:{othermirror != _mirrorBase}, !=45:{othermirror != _mirror45}, !=Micro:{othermirror != _mirrorCeiling}, !=trans:{othermirror != _mirrorTrans}");
                    if (othermirror is null || (othermirror != _mirrorBase && othermirror != _mirror45 && othermirror != _mirrorCeiling && othermirror != _mirrorMicro && othermirror != _mirrorTrans))
                    {
                        //MelonLogger.Msg($"setting layers");
                        vrcMirrorReflection.m_ReflectLayers = vrcMirrorReflection.m_ReflectLayers.value & ~reserved2; //Force all mirrors to not reflect "Mirror/TransparentBackground" - Set all mirrors to exclude reserved2                                                                                             
                    }
                }
                catch (System.Exception ex) { MelonLogger.Msg(ConsoleColor.DarkRed, ex.ToString()); }
            }

        }
        public static void ToggleMirror()
        {
            if (_mirrorBase != null)
            {
                try{ UnityEngine.Object.Destroy(_mirrorBase); } catch (System.Exception ex) { MelonLogger.Msg(ConsoleColor.DarkRed, ex.ToString()); }
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
                childMirror.gameObject.layer = _MirrorsShowInCamera ? 4 : 10; //Default prefab 4:Water - 10:Playerlocal 
                if (_mirrorStateBase == "MirrorTransparent") childMirror.GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue);
                mirror.GetOrAddComponent<VRC_Pickup>().proximity = 3f;
                mirror.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupMirrorBase;
                mirror.GetOrAddComponent<VRC_Pickup>().allowManipulationWhenEquipped = false;

                _mirrorBase = mirror;
            }
        }

        public static void ToggleMirror45()
        {
            if (_mirror45 != null)
            {
                try{ UnityEngine.Object.Destroy(_mirror45); } catch (System.Exception ex) { MelonLogger.Msg(ConsoleColor.DarkRed, ex.ToString()); }
                _mirror45 = null;
            }
            else
            {
                if (_mirrorState45 == "MirrorCutout" || _mirrorState45 == "MirrorTransparent") SetAllMirrorsToIgnoreShader();
                VRCPlayer player = Utils.GetVRCPlayer();
                Vector3 pos = player.transform.position + player.transform.forward + (player.transform.forward * _MirrorDistance45);
                pos.y += .5f;
                pos.y += (_mirrorScaleY45 - 1) / 2;
                //pos.y += 2;
                GameObject mirror = GameObject.Instantiate(mirrorPrefab);
                mirror.transform.position = pos;
                mirror.transform.rotation = player.transform.rotation;
                mirror.transform.rotation = mirror.transform.rotation * Quaternion.AngleAxis(45, Vector3.left);  // Sets the transform's current rotation to a new rotation that rotates 30 degrees around the y-axis(Vector3.up)
                mirror.transform.localScale = new Vector3(_mirrorScaleX45, _mirrorScaleY45, 1f);
                mirror.name = "PortableMirror45";

                var childMirror = mirror.transform.Find(_mirrorState45);
                childMirror.gameObject.active = true;
                childMirror.gameObject.layer = _MirrorsShowInCamera ? 4 : 10;
                if (_mirrorState45 == "MirrorTransparent") childMirror.GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue);
                mirror.GetOrAddComponent<VRC_Pickup>().proximity = 3f;
                mirror.GetOrAddComponent<VRC_Pickup>().pickupable = _CanPickup45Mirror;
                mirror.GetOrAddComponent<VRC_Pickup>().allowManipulationWhenEquipped = false;
                _mirror45 = mirror;

            }
        }

        public static void ToggleMirrorCeiling()
        {
            
            if (_mirrorCeiling != null)
            {
                try { UnityEngine.Object.Destroy(_mirrorCeiling); } catch (System.Exception ex) { MelonLogger.Msg(ConsoleColor.DarkRed, ex.ToString()); }
                _mirrorCeiling = null;
            }
            else
            {
                if (_mirrorStateCeiling == "MirrorCutout" || _mirrorStateCeiling == "MirrorTransparent") SetAllMirrorsToIgnoreShader();
                VRCPlayer player = Utils.GetVRCPlayer();
                Vector3 pos = GameObject.Find(player.gameObject.name + "/AnimationController/HeadAndHandIK/HipTarget").transform.position + (player.transform.up); // Bases mirror position off of hip, to allow for play space moving 
                //MelonLogger.Msg($"x:{GameObject.Find(player.gameObject.name + "/AnimationController/HeadAndHandIK/HipTarget").transform.position.x}, y:{GameObject.Find(player.gameObject.name + "/AnimationController/HeadAndHandIK/HipTarget").transform.position.y}, z:{GameObject.Find(player.gameObject.name + "/AnimationController/HeadAndHandIK/HipTarget").transform.position.z}");
                pos.y += _MirrorDistanceCeiling;
                GameObject mirror = GameObject.Instantiate(mirrorPrefab);
                mirror.transform.position = pos;
                mirror.transform.rotation = player.transform.rotation;
                mirror.transform.rotation = Quaternion.AngleAxis(90, Vector3.left);  // Sets the transform's current rotation to a new rotation that rotates 30 degrees around the y-axis(Vector3.up)
                mirror.transform.localScale = new Vector3(_mirrorScaleXCeiling, _mirrorScaleZCeiling, 1f);
                mirror.name = "PortableMirrorCeiling";

                var childMirror = mirror.transform.Find(_mirrorStateCeiling);
                childMirror.gameObject.active = true;
                childMirror.gameObject.layer = _MirrorsShowInCamera ? 4 : 10;
                if (_mirrorStateCeiling == "MirrorTransparent") childMirror.GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue); 
                mirror.GetOrAddComponent<VRC_Pickup>().proximity = 3f;
                mirror.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupCeilingMirror;
                mirror.GetOrAddComponent<VRC_Pickup>().allowManipulationWhenEquipped = false;
                _mirrorCeiling = mirror;

            }
        }

        public static void ToggleMirrorMicro()
        {
            if (_mirrorMicro != null)
            {
                try{ UnityEngine.Object.Destroy(_mirrorMicro); } catch (System.Exception ex) { MelonLogger.Msg(ConsoleColor.DarkRed, ex.ToString()); }
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
                childMirror.gameObject.layer = _MirrorsShowInCamera ? 4 : 10;
                if (_mirrorStateMicro == "MirrorTransparent") childMirror.GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue);
                mirror.GetOrAddComponent<VRC_Pickup>().proximity = _grabRangeMicro;
                mirror.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupMirrorMicro;
                mirror.GetOrAddComponent<VRC_Pickup>().allowManipulationWhenEquipped = false;
                _mirrorMicro = mirror;
            }
        }

        public static void ToggleMirrorTrans()
        {
            if (_mirrorTrans != null)
            {
                try{ UnityEngine.Object.Destroy(_mirrorTrans); } catch (System.Exception ex) { MelonLogger.Msg(ConsoleColor.DarkRed, ex.ToString()); }
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
                childMirror.gameObject.layer = _MirrorsShowInCamera ? 4 : 10;
                if (_mirrorStateTrans == "MirrorTransparent") childMirror.GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue);
                mirror.GetOrAddComponent<VRC_Pickup>().proximity = 3f;
                mirror.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupMirrorTrans;
                mirror.GetOrAddComponent<VRC_Pickup>().allowManipulationWhenEquipped = false;

                _mirrorTrans = mirror;
            }
        }

        
        private void loadAssets()
        {//https://github.com/ddakebono/BTKSASelfPortrait/blob/master/BTKSASelfPortrait.cs
            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PortableMirrorMod.mirrorprefab"))
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
            else MelonLogger.Error("Bundle was null");
        }

        public Dictionary<string, Transform> ButtonList = new Dictionary<string, Transform>();

        //PlayerLayer = 1 << 9; // https://github.com/knah/VRCMods/blob/master/MirrorResolutionUnlimiter/UiExtensionsAddon.cs
        //PlayerLocalLayer = 1 << 10; //Mainly just here as a refernce now
        //UiLayer = 1 << 5;
        //UiMenuLayer = 1 << 12;
        //MirrorReflectionLayer = 1 << 18;
        public static int reserved2 = 1 << 19;
        //int optMirrorMask = PlayerLayer | MirrorReflectionLayer;
        //int fullMirrorMask = -1 & ~UiLayer & ~UiMenuLayer & ~PlayerLocalLayer & ~reserved2;

        public static AssetBundle assetBundle;
        public static GameObject mirrorPrefab;

        public static GameObject _mirrorBase;
        public static float _mirrorScaleXBase;
        public static float _mirrorScaleYBase;
        public static float _MirrorDistance;
        public static float _oldMirrorDistance;
        public static float _oldMirrorScaleYBase;
        public static bool _canPickupMirrorBase;
        public static KeyCode _mirrorKeybindBase;
        public static bool _quickMenuOptions;
        public static bool _openLastQMpage;
        public static int _qmOptionsLastPage = 1;
        public static float _MirrorTransValue;
        public static bool _enableBase;
        public static string _mirrorStateBase;
        public static bool _mirrorKeybindEnabled;
        public static bool _MirrorsShowInCamera;

        public static GameObject _mirror45;
        public static float _mirrorScaleX45;
        public static float _mirrorScaleY45;
        public static float _MirrorDistance45;
        public static float _oldMirrorDistance45;
        public static float _oldMirrorScaleY45;
        public static bool _CanPickup45Mirror;
        public static bool _enable45;
        public static string _mirrorState45;

        public static GameObject _mirrorCeiling;
        public static float _mirrorScaleXCeiling;
        public static float _mirrorScaleZCeiling;
        public static float _MirrorDistanceCeiling;
        public static float _oldMirrorDistanceCeiling;
        public static bool _canPickupCeilingMirror;
        public static bool _enableCeiling;
        public static string _mirrorStateCeiling;

        public static GameObject _mirrorMicro;
        public static float _mirrorScaleXMicro;
        public static float _mirrorScaleMicro;
        public static float _grabRangeMicro;
        public static float _oldMirrorScaleYMicro;
        public static bool _canPickupMirrorMicro;
        public static bool _enableMicro;
        public static string _mirrorStateMicro;

        public static GameObject _mirrorTrans;
        public static float _mirrorScaleXTrans;
        public static float _mirrorScaleYTrans;
        public static float _MirrorDistanceTrans;
        public static float _oldMirrorDistanceTrans;
        public static float _oldMirrorScaleYTrans;
        public static bool _canPickupMirrorTrans;
        public static bool _enableTrans;
        public static string _mirrorStateTrans;
    }
}


namespace UIExpansionKit.API
{
    public struct LayoutDescriptionCustom
    {
        public static LayoutDescription QuickMenu3Column = new LayoutDescription { NumColumns = 3, RowHeight = 380 / 8, NumRows = 8 };
    }
}