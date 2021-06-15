using System;
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



[assembly: MelonInfo(typeof(PortableMirror.Main), "PortableMirrorMod", "1.5", "M-oons, Nirvash")] //Name changed to break auto update
[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonOptionalDependencies("ActionMenuApi")]

namespace PortableMirror
{

    public class Main : MelonMod
    { 
        public Main()
        { LoaderIntegrityCheck.CheckIntegrity(); }
        public override void OnApplicationStart()
        {
            loadAssets();

            MelonPreferences.CreateCategory("PortableMirror", "PortableMirror");
            MelonPreferences.CreateEntry<float>("PortableMirror", "MirrorScaleX", 5f, "Mirror Scale X");
            MelonPreferences.CreateEntry<float>("PortableMirror", "MirrorScaleY", 3f, "Mirror Scale Y");
            MelonPreferences.CreateEntry<float>("PortableMirror", "MirrorDistance", 0f, "Mirror Distance");
            MelonPreferences.CreateEntry<string>("PortableMirror", "MirrorState", "MirrorFull", "Mirror Type");
            ExpansionKitApi.RegisterSettingAsStringEnum("PortableMirror", "MirrorState", new[] { ("MirrorFull", "Full"), ("MirrorOpt", "Optimized"), ("MirrorCutout", "Cutout"), ("MirrorTransparent", "Transparent"), ("MirrorCutoutSolo", "Cutout LocalOnly"), ("MirrorTransparentSolo", "Transparent LocalOnly") });
            MelonPreferences.CreateEntry<bool>("PortableMirror", "CanPickupMirror", false, "Can Pickup Mirror");
            MelonPreferences.CreateEntry<bool>("PortableMirror", "enableBase", true, "Enable Portable Mirror Quick Menu Button");
            MelonPreferences.CreateEntry<bool>("PortableMirror", "PositionOnView", false, "Position mirror based on view angle");
            MelonPreferences.CreateEntry<bool>("PortableMirror", "AnchorToTracking", false, "Mirror Follows You");
            MelonPreferences.CreateEntry<string>("PortableMirror", "MirrorKeybind", "Alpha1", "Toggle Mirror Keybind");
            MelonPreferences.CreateEntry<bool>("PortableMirror", "MirrorKeybindEnabled", true, "Enabled Mirror Keybind");
            MelonPreferences.CreateEntry<bool>("PortableMirror", "QuickMenuOptions", true, "Enable Settings Quick Menu Button");
            MelonPreferences.CreateEntry<bool>("PortableMirror", "OpenLastQMpage", false, "Quick Menu Settings remembers last page opened");
            MelonPreferences.CreateEntry<float>("PortableMirror", "TransMirrorTrans", .4f, "Transparent Mirror transparency - Higher is more transparent - Global for all mirrors");
            MelonPreferences.CreateEntry<bool>("PortableMirror", "MirrorsShowInCamera", false, "Mirrors show in Cameras - Global for all mirrors");
            MelonPreferences.CreateEntry<float>("PortableMirror", "MirrorDistAdjAmmount", .05f, "High Precision Distance Adjustment - Global for all mirrors");
            MelonPreferences.CreateEntry<bool>("PortableMirror", "ActionMenu", true, "Enable Controls on Action Menu (Requires Restart)");
            MelonPreferences.CreateEntry<float>("PortableMirror", "ColliderDepth", 0.01f, "Collider Depth - Global for all mirrors");
            MelonPreferences.CreateEntry<bool>("PortableMirror", "PickupToHand", true, "Pickups snap to hand - Global for all mirrors");


            MelonPreferences.CreateCategory("PortableMirror45", "PortableMirror 45 Degree");
            MelonPreferences.CreateEntry<float>("PortableMirror45", "MirrorScaleX", 5f, "Mirror Scale X");
            MelonPreferences.CreateEntry<float>("PortableMirror45", "MirrorScaleY", 4f, "Mirror Scale Y");
            MelonPreferences.CreateEntry<float>("PortableMirror45", "MirrorDistance", 0f, "Mirror Distance");
            MelonPreferences.CreateEntry<string>("PortableMirror45", "MirrorState", "MirrorFull", "Mirror Type");
            ExpansionKitApi.RegisterSettingAsStringEnum("PortableMirror45", "MirrorState", new[] { ("MirrorFull", "Full"), ("MirrorOpt", "Optimized"), ("MirrorCutout", "Cutout"), ("MirrorTransparent", "Transparent"), ("MirrorCutoutSolo", "Cutout LocalOnly"), ("MirrorTransparentSolo", "Transparent LocalOnly") });
            MelonPreferences.CreateEntry<bool>("PortableMirror45", "CanPickupMirror", false, "Can Pickup 45 Mirror");
            MelonPreferences.CreateEntry<bool>("PortableMirror45", "enable45", true, "Enable 45 Mirror QM Button");
            MelonPreferences.CreateEntry<bool>("PortableMirror45", "AnchorToTracking", false, "Mirror Follows You");


            MelonPreferences.CreateCategory("PortableMirrorCeiling", "PortableMirror Ceiling");
            MelonPreferences.CreateEntry<float>("PortableMirrorCeiling", "MirrorScaleX", 5f, "Mirror Scale X");
            MelonPreferences.CreateEntry<float>("PortableMirrorCeiling", "MirrorScaleZ", 5f, "Mirror Scale Z");
            MelonPreferences.CreateEntry<float>("PortableMirrorCeiling", "MirrorDistance", 2, "Mirror Distance");
            MelonPreferences.CreateEntry<string>("PortableMirrorCeiling", "MirrorState", "MirrorFull", "Mirror Type");
            ExpansionKitApi.RegisterSettingAsStringEnum("PortableMirrorCeiling", "MirrorState", new[] { ("MirrorFull", "Full"), ("MirrorOpt", "Optimized"), ("MirrorCutout", "Cutout"), ("MirrorTransparent", "Transparent"), ("MirrorCutoutSolo", "Cutout LocalOnly"), ("MirrorTransparentSolo", "Transparent LocalOnly") });
            MelonPreferences.CreateEntry<bool>("PortableMirrorCeiling", "CanPickupMirror", false, "Can Pickup Ceiling Mirror");
            MelonPreferences.CreateEntry<bool>("PortableMirrorCeiling", "enableCeiling", true, "Enable Ceiling Mirror QM Button");
            MelonPreferences.CreateEntry<bool>("PortableMirrorCeiling", "AnchorToTracking", false, "Mirror Follows You");


            MelonPreferences.CreateCategory("PortableMirrorMicro", "PortableMirror Micro");
            MelonPreferences.CreateEntry<float>("PortableMirrorMicro", "MirrorScaleX", .05f, "Mirror Scale X");
            MelonPreferences.CreateEntry<float>("PortableMirrorMicro", "MirrorScaleY", .1f, "Mirror Scale Y");
            MelonPreferences.CreateEntry<float>("PortableMirrorMicro", "GrabRange", .1f, "GrabRange");
            MelonPreferences.CreateEntry<string>("PortableMirrorMicro", "MirrorState", "MirrorFull", "Mirror Type");
            ExpansionKitApi.RegisterSettingAsStringEnum("PortableMirrorMicro", "MirrorState", new[] { ("MirrorFull", "Full"), ("MirrorOpt", "Optimized"), ("MirrorCutout", "Cutout"), ("MirrorTransparent", "Transparent"), ("MirrorCutoutSolo", "Cutout LocalOnly"), ("MirrorTransparentSolo", "Transparent LocalOnly") });
            MelonPreferences.CreateEntry<bool>("PortableMirrorMicro", "CanPickupMirror", false, "Can Pickup MirrorMicro");
            MelonPreferences.CreateEntry<bool>("PortableMirrorMicro", "enableMicro", true, "Enable Micro Mirror QM Button");
            MelonPreferences.CreateEntry<bool>("PortableMirrorMicro", "PositionOnView", false, "Position mirror based on view angle");
            MelonPreferences.CreateEntry<bool>("PortableMirrorMicro", "AnchorToTracking", false, "Mirror Follows You");


            MelonPreferences.CreateCategory("PortableMirrorTrans", "PortableMirror Transparent");
            MelonPreferences.CreateEntry<float>("PortableMirrorTrans", "MirrorScaleX", 5f, "Mirror Scale X");
            MelonPreferences.CreateEntry<float>("PortableMirrorTrans", "MirrorScaleY", 3f, "Mirror Scale Y");
            MelonPreferences.CreateEntry<float>("PortableMirrorTrans", "MirrorDistance", 0f, "Mirror Distance");
            MelonPreferences.CreateEntry<string>("PortableMirrorTrans", "MirrorState", "MirrorTransparent", "Mirror Type - Resets to Transparent on load");
            ExpansionKitApi.RegisterSettingAsStringEnum("PortableMirrorTrans", "MirrorState", new[] { ("MirrorFull", "Full"), ("MirrorOpt", "Optimized"), ("MirrorCutout", "Cutout"), ("MirrorTransparent", "Transparent"), ("MirrorCutoutSolo", "Cutout LocalOnly"), ("MirrorTransparentSolo", "Transparent LocalOnly") });
            MelonPreferences.SetEntryValue<string>("PortableMirrorTrans", "MirrorState", "MirrorTransparent");//Force to Transparent every load
            MelonPreferences.CreateEntry<bool>("PortableMirrorTrans", "CanPickupMirror", false, "Can Pickup Mirror");
            MelonPreferences.CreateEntry<bool>("PortableMirrorTrans", "enableTrans", true, "Enable Transparent Mirror QM Button");
            MelonPreferences.CreateEntry<bool>("PortableMirrorTrans", "PositionOnView", false, "Position mirror based on view angle");
            MelonPreferences.CreateEntry<bool>("PortableMirrorTrans", "AnchorToTracking", false, "Mirror Follows You");


            OnPreferencesSaved();

            MelonLogger.Msg("Base mod made by M-oons, modifications by Nirvash");
            MelonLogger.Msg($"[{_mirrorKeybindBase}] -> Toggle portable mirror");

            MelonMod uiExpansionKit = MelonHandler.Mods.Find(m => m.Info.Name == "UI Expansion Kit");
            if (uiExpansionKit != null)
            {
                uiExpansionKit.Info.SystemType.Assembly.GetTypes().First(t => t.FullName == "UIExpansionKit.API.ExpansionKitApi").GetMethod("RegisterWaitConditionBeforeDecorating", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Invoke(null, new object[]
                {
                    UIX_QM.CreateQuickMenuButton()
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
            _mirrorDistAdj = _mirrorDistHighPrec ? MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorDistAdjAmmount") : .25f;
            _colliderDepth = MelonPreferences.GetEntryValue<float>("PortableMirror", "ColliderDepth");
            _pickupOrient = MelonPreferences.GetEntryValue<bool>("PortableMirror", "PickupToHand");
            _posOnViewBase = MelonPreferences.GetEntryValue<bool>("PortableMirror", "PositionOnView");
            _anchorTrackingBase = MelonPreferences.GetEntryValue<bool>("PortableMirror", "AnchorToTracking");

            if (_mirrorBase != null && Utils.GetVRCPlayer() != null)
            {
                _mirrorBase.transform.SetParent(null);
                _mirrorBase.transform.localScale = new Vector3(_mirrorScaleXBase, _mirrorScaleYBase, 1f);
                _mirrorBase.transform.position = new Vector3(_mirrorBase.transform.position.x, _mirrorBase.transform.position.y + ((_mirrorScaleYBase - _oldMirrorScaleYBase) / 2), _mirrorBase.transform.position.z  );
                _mirrorBase.transform.position += _mirrorBase.transform.forward * (_MirrorDistance - _oldMirrorDistance);

                _mirrorBase.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupMirrorBase;
                _mirrorBase.GetOrAddComponent<VRC_Pickup>().orientation = _pickupOrient ? VRC_Pickup.PickupOrientation.Any : VRC_Pickup.PickupOrientation.Grip;

                if (_mirrorStateBase == "MirrorCutout" || _mirrorStateBase == "MirrorTransparent" || _mirrorStateBase == "MirrorCutoutSolo" || _mirrorStateBase == "MirrorTransparentSolo") SetAllMirrorsToIgnoreShader();
                if (_mirrorStateBase == "MirrorTransparent" || _mirrorStateBase == "MirrorTransparentSolo") _mirrorBase.transform.Find(_mirrorStateBase).GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue);
                for (int i = 0; i < _mirrorBase.transform.childCount; i++)
                    _mirrorBase.transform.GetChild(i).gameObject.active = false;
                var childMirror = _mirrorBase.transform.Find(_mirrorStateBase);
                childMirror.gameObject.active = true;
                childMirror.gameObject.layer = _MirrorsShowInCamera ? 4 : 10;
                _mirrorBase.GetComponent<BoxCollider>().size = new Vector3(1f, 1f, _colliderDepth);
                if (_anchorTrackingBase) _mirrorBase.transform.SetParent(GameObject.Find("_Application/TrackingVolume/PlayerObjects").transform, true);
            }


            _oldMirrorScaleY45 = _mirrorScaleY45;
            _oldMirrorDistance45 = _MirrorDistance45;
            _mirrorScaleX45 = MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorScaleX");
            _mirrorScaleY45 = MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorScaleY");
            _MirrorDistance45 = MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorDistance");
            _CanPickup45Mirror = MelonPreferences.GetEntryValue<bool>("PortableMirror45", "CanPickupMirror");
            _mirrorState45 = MelonPreferences.GetEntryValue<string>("PortableMirror45", "MirrorState");
            _anchorTracking45 = MelonPreferences.GetEntryValue<bool>("PortableMirror45", "AnchorToTracking");

            if (_mirror45 != null && Utils.GetVRCPlayer() != null)
            {
                _mirror45.transform.SetParent(null);
                _mirror45.transform.localScale = new Vector3(_mirrorScaleX45, _mirrorScaleY45, 1f);
                _mirror45.transform.rotation = _mirror45.transform.rotation * Quaternion.AngleAxis(-45, Vector3.left);
                _mirror45.transform.position = new Vector3(_mirror45.transform.position.x, _mirror45.transform.position.y + ((_mirrorScaleY45 - _oldMirrorScaleY45)/2.5f), _mirror45.transform.position.z  );
                _mirror45.transform.position += _mirror45.transform.forward * (_MirrorDistance45 - _oldMirrorDistance45);
                _mirror45.transform.rotation = _mirror45.transform.rotation * Quaternion.AngleAxis(45, Vector3.left);

                _mirror45.GetOrAddComponent<VRC_Pickup>().pickupable = _CanPickup45Mirror;
                _mirror45.GetOrAddComponent<VRC_Pickup>().orientation = _pickupOrient ? VRC_Pickup.PickupOrientation.Any : VRC_Pickup.PickupOrientation.Grip;

                if (_mirrorState45 == "MirrorCutout" || _mirrorState45 == "MirrorTransparent" || _mirrorState45 == "MirrorCutoutSolo" || _mirrorState45 == "MirrorTransparentSolo") SetAllMirrorsToIgnoreShader();
                if (_mirrorState45 == "MirrorTransparent" || _mirrorState45 == "MirrorTransparentSolo") _mirror45.transform.Find(_mirrorState45).GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue);
                for (int i = 0; i < _mirror45.transform.childCount; i++)
                    _mirror45.transform.GetChild(i).gameObject.active = false;
                var childMirror = _mirror45.transform.Find(_mirrorState45);
                childMirror.gameObject.active = true;
                childMirror.gameObject.layer = _MirrorsShowInCamera ? 4 : 10;
                _mirror45.GetComponent<BoxCollider>().size = new Vector3(1f, 1f, _colliderDepth);
                if (_anchorTracking45) _mirror45.transform.SetParent(GameObject.Find("_Application/TrackingVolume/PlayerObjects").transform, true);
            }


            _oldMirrorDistanceCeiling = _MirrorDistanceCeiling;
            _mirrorScaleXCeiling = MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleX");
            _mirrorScaleZCeiling = MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleZ");
            _MirrorDistanceCeiling = MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorDistance");
            _canPickupCeilingMirror = MelonPreferences.GetEntryValue<bool>("PortableMirrorCeiling", "CanPickupMirror");
            _mirrorStateCeiling = MelonPreferences.GetEntryValue<string>("PortableMirrorCeiling", "MirrorState");
            _anchorTrackingCeiling = MelonPreferences.GetEntryValue<bool>("PortableMirrorCeiling", "AnchorToTracking");

            if (_mirrorCeiling != null && Utils.GetVRCPlayer() != null)
            {
                _mirrorCeiling.transform.SetParent(null);
                _mirrorCeiling.transform.localScale = new Vector3(_mirrorScaleXCeiling, _mirrorScaleZCeiling, 1f);
                _mirrorCeiling.transform.position = new Vector3(_mirrorCeiling.transform.position.x, _mirrorCeiling.transform.position.y + (_MirrorDistanceCeiling - _oldMirrorDistanceCeiling), _mirrorCeiling.transform.position.z);

                _mirrorCeiling.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupCeilingMirror;
                _mirrorCeiling.GetOrAddComponent<VRC_Pickup>().orientation = _pickupOrient ? VRC_Pickup.PickupOrientation.Any : VRC_Pickup.PickupOrientation.Grip;

                if (_mirrorStateCeiling == "MirrorCutout" || _mirrorStateCeiling == "MirrorTransparent" || _mirrorStateCeiling == "MirrorCutoutSolo" || _mirrorStateCeiling == "MirrorTransparentSolo") SetAllMirrorsToIgnoreShader();
                if (_mirrorStateCeiling == "MirrorTransparent" || _mirrorStateCeiling == "MirrorTransparentSolo") _mirrorCeiling.transform.Find(_mirrorStateCeiling).GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue);
                for (int i = 0; i < _mirrorCeiling.transform.childCount; i++)
                    _mirrorCeiling.transform.GetChild(i).gameObject.active = false;
                var childMirror = _mirrorCeiling.transform.Find(_mirrorStateCeiling);
                childMirror.gameObject.active = true;
                childMirror.gameObject.layer = _MirrorsShowInCamera ? 4 : 10;
                _mirrorCeiling.GetComponent<BoxCollider>().size = new Vector3(1f, 1f, _colliderDepth);
                if (_anchorTrackingCeiling)  _mirrorCeiling.transform.SetParent(GameObject.Find("_Application/TrackingVolume/PlayerObjects").transform, true);
            }


            _oldMirrorScaleYMicro = _mirrorScaleMicro;
            _mirrorScaleXMicro = MelonPreferences.GetEntryValue<float>("PortableMirrorMicro", "MirrorScaleX");
            _mirrorScaleMicro = MelonPreferences.GetEntryValue<float>("PortableMirrorMicro", "MirrorScaleY");
            _grabRangeMicro = MelonPreferences.GetEntryValue<float>("PortableMirrorMicro", "GrabRange");
            _canPickupMirrorMicro = MelonPreferences.GetEntryValue<bool>("PortableMirrorMicro", "CanPickupMirror");
            _mirrorStateMicro = MelonPreferences.GetEntryValue<string>("PortableMirrorMicro", "MirrorState");
            _posOnViewMicro = MelonPreferences.GetEntryValue<bool>("PortableMirrorMicro", "PositionOnView");
            _anchorTrackingMicro = MelonPreferences.GetEntryValue<bool>("PortableMirrorMicro", "AnchorToTracking");

            if (_mirrorMicro != null && Utils.GetVRCPlayer() != null)
            {
                _mirrorMicro.transform.SetParent(null);
                _mirrorMicro.transform.localScale = new Vector3(_mirrorScaleXMicro, _mirrorScaleMicro, 1f);
                _mirrorMicro.transform.position = new Vector3(_mirrorMicro.transform.position.x, _mirrorMicro.transform.position.y + ((_mirrorScaleMicro - _oldMirrorScaleYMicro) / 2), _mirrorMicro.transform.position.z);

                _mirrorMicro.GetOrAddComponent<VRC_Pickup>().proximity = _grabRangeMicro;
                _mirrorMicro.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupMirrorMicro;
                _mirrorMicro.GetOrAddComponent<VRC_Pickup>().orientation = _pickupOrient ? VRC_Pickup.PickupOrientation.Any : VRC_Pickup.PickupOrientation.Grip;

                if (_mirrorStateMicro == "MirrorCutout" || _mirrorStateMicro == "MirrorTransparent" || _mirrorStateMicro == "MirrorCutoutSolo" || _mirrorStateMicro == "MirrorTransparentSolo") SetAllMirrorsToIgnoreShader();
                if (_mirrorStateMicro == "MirrorTransparent" || _mirrorStateMicro == "MirrorTransparentSolo") _mirrorMicro.transform.Find(_mirrorStateMicro).GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue);
                for (int i = 0; i < _mirrorMicro.transform.childCount; i++)
                    _mirrorMicro.transform.GetChild(i).gameObject.active = false;
                var childMirror = _mirrorMicro.transform.Find(_mirrorStateMicro);
                childMirror.gameObject.active = true;
                childMirror.gameObject.layer = _MirrorsShowInCamera ? 4 : 10;
                if (_anchorTrackingMicro) _mirrorMicro.transform.SetParent(GameObject.Find("_Application/TrackingVolume/PlayerObjects").transform, true);
            }


            _oldMirrorScaleYTrans = _mirrorScaleYTrans;
            _oldMirrorDistanceTrans = _MirrorDistanceTrans;
            _mirrorScaleXTrans = MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorScaleX");
            _mirrorScaleYTrans = MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorScaleY");
            _MirrorDistanceTrans = MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorDistance");
            _canPickupMirrorTrans = MelonPreferences.GetEntryValue<bool>("PortableMirrorTrans", "CanPickupMirror");
            _mirrorStateTrans = MelonPreferences.GetEntryValue<string>("PortableMirrorTrans", "MirrorState");
            _posOnViewTrans = MelonPreferences.GetEntryValue<bool>("PortableMirrorTrans", "PositionOnView");
            _anchorTrackingTrans = MelonPreferences.GetEntryValue<bool>("PortableMirrorTrans", "AnchorToTracking");

            if (_mirrorTrans != null && Utils.GetVRCPlayer() != null)
            {
                _mirrorTrans.transform.SetParent(null);
                _mirrorTrans.transform.localScale = new Vector3(_mirrorScaleXTrans, _mirrorScaleYTrans, 1f);
                _mirrorTrans.transform.position = new Vector3(_mirrorTrans.transform.position.x, _mirrorTrans.transform.position.y + ((_mirrorScaleYTrans - _oldMirrorScaleYTrans) / 2), _mirrorTrans.transform.position.z);
                _mirrorTrans.transform.position += _mirrorTrans.transform.forward * (_MirrorDistanceTrans - _oldMirrorDistanceTrans);

                _mirrorTrans.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupMirrorTrans;
                _mirrorTrans.GetOrAddComponent<VRC_Pickup>().orientation = _pickupOrient ? VRC_Pickup.PickupOrientation.Any : VRC_Pickup.PickupOrientation.Grip;

                if (_mirrorStateTrans == "MirrorCutout" || _mirrorStateTrans == "MirrorTransparent" || _mirrorStateTrans == "MirrorCutoutSolo" || _mirrorStateTrans == "MirrorTransparentSolo") SetAllMirrorsToIgnoreShader();
                if (_mirrorStateTrans == "MirrorTransparent" || _mirrorStateTrans == "MirrorTransparentSolo") _mirrorTrans.transform.Find(_mirrorStateTrans).GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue);
                for (int i = 0; i < _mirrorTrans.transform.childCount; i++)
                    _mirrorTrans.transform.GetChild(i).gameObject.active = false;
                var childMirror = _mirrorTrans.transform.Find(_mirrorStateTrans);
                childMirror.gameObject.active = true;
                childMirror.gameObject.layer = _MirrorsShowInCamera ? 4 : 10;
                _mirrorTrans.GetComponent<BoxCollider>().size = new Vector3(1f, 1f, _colliderDepth);
                if (_anchorTrackingTrans) _mirrorTrans.transform.SetParent(GameObject.Find("_Application/TrackingVolume/PlayerObjects").transform, true);

            }
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
                if (_mirrorStateBase == "MirrorCutout" || _mirrorStateBase == "MirrorTransparent" || _mirrorStateBase == "MirrorCutoutSolo" || _mirrorStateBase == "MirrorTransparentSolo") SetAllMirrorsToIgnoreShader();
                VRCPlayer player = Utils.GetVRCPlayer();
                Vector3 pos = player.transform.position + player.transform.forward + (player.transform.forward * _MirrorDistance);
                pos.y += .5f;
                pos.y += (_mirrorScaleYBase - 1)  / 2;

                GameObject mirror = GameObject.Instantiate(mirrorPrefab);
                mirror.transform.position = pos;
                mirror.transform.rotation = player.transform.rotation;
                mirror.transform.localScale = new Vector3(_mirrorScaleXBase, _mirrorScaleYBase, 1f);
                mirror.name = "PortableMirror";

                if (_posOnViewBase)
                {
                    GameObject IKEffector = GameObject.Find(player.gameObject.name + "/AnimationController/HeadAndHandIK/HeadEffector");
                    mirror.transform.position = IKEffector.transform.position + IKEffector.transform.forward + (IKEffector.transform.forward * _MirrorDistance);
                    mirror.transform.rotation = IKEffector.transform.rotation;
                }

                var childMirror = mirror.transform.Find(_mirrorStateBase);
                childMirror.gameObject.active = true;
                childMirror.gameObject.layer = _MirrorsShowInCamera ? 4 : 10; //Default prefab 4:Water - 10:Playerlocal 
                if (_mirrorStateBase == "MirrorTransparent" || _mirrorStateBase == "MirrorTransparentSolo") childMirror.GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue);
                mirror.GetOrAddComponent<VRC_Pickup>().proximity = 3f;
                mirror.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupMirrorBase;
                mirror.GetOrAddComponent<VRC_Pickup>().allowManipulationWhenEquipped = false;
                mirror.GetOrAddComponent<VRC_Pickup>().orientation = _pickupOrient ? VRC_Pickup.PickupOrientation.Any : VRC_Pickup.PickupOrientation.Grip;
                mirror.GetComponent<BoxCollider>().size = new Vector3(1f, 1f, _colliderDepth);
                if (!_anchorTrackingBase) mirror.transform.SetParent(null);
                else mirror.transform.SetParent(GameObject.Find("_Application/TrackingVolume/PlayerObjects").transform, true);

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
                if (_mirrorState45 == "MirrorCutout" || _mirrorState45 == "MirrorTransparent" || _mirrorState45 == "MirrorCutoutSolo" || _mirrorState45 == "MirrorTransparentSolo") SetAllMirrorsToIgnoreShader();
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
                if (_mirrorState45 == "MirrorTransparent" || _mirrorState45 == "MirrorTransparentSolo") childMirror.GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue);
                mirror.GetOrAddComponent<VRC_Pickup>().proximity = 3f;
                mirror.GetOrAddComponent<VRC_Pickup>().pickupable = _CanPickup45Mirror;
                mirror.GetOrAddComponent<VRC_Pickup>().allowManipulationWhenEquipped = false;
                mirror.GetOrAddComponent<VRC_Pickup>().orientation = _pickupOrient ? VRC_Pickup.PickupOrientation.Any : VRC_Pickup.PickupOrientation.Grip;
                mirror.GetComponent<BoxCollider>().size = new Vector3(1f, 1f, _colliderDepth);
                if (!_anchorTracking45) mirror.transform.SetParent(null);
                else mirror.transform.SetParent(GameObject.Find("_Application/TrackingVolume/PlayerObjects").transform, true);
               
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
                if (_mirrorStateCeiling == "MirrorCutout" || _mirrorStateCeiling == "MirrorTransparent" || _mirrorStateCeiling == "MirrorCutoutSolo" || _mirrorStateCeiling == "MirrorTransparentSolo") SetAllMirrorsToIgnoreShader();
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
                if (_mirrorStateCeiling == "MirrorTransparent" || _mirrorStateCeiling == "MirrorTransparentSolo") childMirror.GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue); 
                mirror.GetOrAddComponent<VRC_Pickup>().proximity = 3f;
                mirror.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupCeilingMirror;
                mirror.GetOrAddComponent<VRC_Pickup>().allowManipulationWhenEquipped = false;
                mirror.GetOrAddComponent<VRC_Pickup>().orientation = _pickupOrient ? VRC_Pickup.PickupOrientation.Any : VRC_Pickup.PickupOrientation.Grip;
                mirror.GetComponent<BoxCollider>().size = new Vector3(1f, 1f, _colliderDepth);
                if (!_anchorTrackingCeiling) mirror.transform.SetParent(null);
                else mirror.transform.SetParent(GameObject.Find("_Application/TrackingVolume/PlayerObjects").transform, true);

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
                if (_mirrorStateMicro == "MirrorCutout" || _mirrorStateMicro == "MirrorTransparent" || _mirrorStateMicro == "MirrorCutoutSolo" || _mirrorStateMicro == "MirrorTransparentSolo") SetAllMirrorsToIgnoreShader();
                VRCPlayer player = Utils.GetVRCPlayer();
                Vector3 pos = GameObject.Find(player.gameObject.name + "/AnimationController/HeadAndHandIK/HeadEffector").transform.position + (player.transform.forward * _mirrorScaleMicro); // Gets position of Head and moves mirror forward by the Y scale.
                pos.y -= _mirrorScaleMicro / 4;///This will need turning
                GameObject mirror = GameObject.Instantiate(mirrorPrefab);
                mirror.transform.position = pos;
                mirror.transform.rotation = player.transform.rotation;
                mirror.transform.localScale = new Vector3(_mirrorScaleXMicro, _mirrorScaleMicro, 1f);
                mirror.name = "PortableMirrorMicro";

                if (_posOnViewMicro)
                {
                    GameObject IKEffector = GameObject.Find(player.gameObject.name + "/AnimationController/HeadAndHandIK/HeadEffector");
                    mirror.transform.position = IKEffector.transform.position + (IKEffector.transform.forward * _mirrorScaleMicro);
                    mirror.transform.rotation = IKEffector.transform.rotation;
                }

                var childMirror = mirror.transform.Find(_mirrorStateMicro);
                childMirror.gameObject.active = true;
                childMirror.gameObject.layer = _MirrorsShowInCamera ? 4 : 10;
                if (_mirrorStateMicro == "MirrorTransparent" || _mirrorStateMicro == "MirrorTransparentSolo") childMirror.GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue);
                mirror.GetOrAddComponent<VRC_Pickup>().proximity = _grabRangeMicro;
                mirror.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupMirrorMicro;
                mirror.GetOrAddComponent<VRC_Pickup>().allowManipulationWhenEquipped = false;
                mirror.GetOrAddComponent<VRC_Pickup>().orientation = _pickupOrient ? VRC_Pickup.PickupOrientation.Any : VRC_Pickup.PickupOrientation.Grip;
                if (!_anchorTrackingMicro) mirror.transform.SetParent(null);
                else mirror.transform.SetParent(GameObject.Find("_Application/TrackingVolume/PlayerObjects").transform, true);

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
                if(_mirrorStateTrans == "MirrorCutout" || _mirrorStateTrans == "MirrorTransparent" || _mirrorStateTrans == "MirrorCutoutSolo" || _mirrorStateTrans == "MirrorTransparentSolo") SetAllMirrorsToIgnoreShader();
                VRCPlayer player = Utils.GetVRCPlayer();
                Vector3 pos = player.transform.position + player.transform.forward + (player.transform.forward * _MirrorDistanceTrans);
                pos.y += .5f;
                pos.y += (_mirrorScaleYTrans - 1) / 2;
                GameObject mirror = GameObject.Instantiate(mirrorPrefab);
                mirror.transform.position = pos;
                mirror.transform.rotation = player.transform.rotation;
                mirror.transform.localScale = new Vector3(_mirrorScaleXTrans, _mirrorScaleYTrans, 1f);
                mirror.name = "PortableMirrorTrans";

                if (_posOnViewTrans)
                {
                    GameObject IKEffector = GameObject.Find(player.gameObject.name + "/AnimationController/HeadAndHandIK/HeadEffector");
                    mirror.transform.position = IKEffector.transform.position + IKEffector.transform.forward + (IKEffector.transform.forward * _MirrorDistanceTrans);
                    mirror.transform.rotation = IKEffector.transform.rotation;
                }

                var childMirror = mirror.transform.Find(_mirrorStateTrans);
                childMirror.gameObject.active = true;
                childMirror.gameObject.layer = _MirrorsShowInCamera ? 4 : 10;
                if (_mirrorStateTrans == "MirrorTransparent" || _mirrorStateTrans == "MirrorTransparentSolo") childMirror.GetComponent<Renderer>().material.SetFloat("_Transparency", _MirrorTransValue);
                mirror.GetOrAddComponent<VRC_Pickup>().proximity = 3f;
                mirror.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupMirrorTrans;
                mirror.GetOrAddComponent<VRC_Pickup>().allowManipulationWhenEquipped = false;
                mirror.GetOrAddComponent<VRC_Pickup>().orientation = _pickupOrient ? VRC_Pickup.PickupOrientation.Any : VRC_Pickup.PickupOrientation.Grip;
                mirror.GetComponent<BoxCollider>().size = new Vector3(1f, 1f, _colliderDepth);
                if (!_anchorTrackingTrans) mirror.transform.SetParent(null);
                else mirror.transform.SetParent(GameObject.Find("_Application/TrackingVolume/PlayerObjects").transform, true);

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

        public static Dictionary<string, Transform> ButtonList = new Dictionary<string, Transform>();

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
        public static float _mirrorDistAdj;
        public static bool _mirrorDistHighPrec = false;
        public static float _colliderDepth;
        public static bool _pickupOrient;
        public static bool _posOnViewBase;
        public static bool _anchorTrackingBase;

        public static GameObject _mirror45;
        public static float _mirrorScaleX45;
        public static float _mirrorScaleY45;
        public static float _MirrorDistance45;
        public static float _oldMirrorDistance45;
        public static float _oldMirrorScaleY45;
        public static bool _CanPickup45Mirror;
        public static bool _enable45;
        public static string _mirrorState45;
        public static bool _anchorTracking45;

        public static GameObject _mirrorCeiling;
        public static float _mirrorScaleXCeiling;
        public static float _mirrorScaleZCeiling;
        public static float _MirrorDistanceCeiling;
        public static float _oldMirrorDistanceCeiling;
        public static bool _canPickupCeilingMirror;
        public static bool _enableCeiling;
        public static string _mirrorStateCeiling;
        public static bool _anchorTrackingCeiling;

        public static GameObject _mirrorMicro;
        public static float _mirrorScaleXMicro;
        public static float _mirrorScaleMicro;
        public static float _grabRangeMicro;
        public static float _oldMirrorScaleYMicro;
        public static bool _canPickupMirrorMicro;
        public static bool _enableMicro;
        public static string _mirrorStateMicro;
        public static bool _posOnViewMicro;
        public static bool _anchorTrackingMicro;

        public static GameObject _mirrorTrans;
        public static float _mirrorScaleXTrans;
        public static float _mirrorScaleYTrans;
        public static float _MirrorDistanceTrans;
        public static float _oldMirrorDistanceTrans;
        public static float _oldMirrorScaleYTrans;
        public static bool _canPickupMirrorTrans;
        public static bool _enableTrans;
        public static string _mirrorStateTrans;
        public static bool _posOnViewTrans;
        public static bool _anchorTrackingTrans;
    }
}


