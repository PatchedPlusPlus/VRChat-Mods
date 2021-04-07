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


[assembly: MelonModInfo(typeof(PortableMirror.Main), "PortableMirrorMod", "1.2.9", "M-oons,Nirvash")] //Name changed to break auto update
[assembly: MelonModGame("VRChat", "VRChat")]

namespace PortableMirror
{
    public static class ModInfo
    {
        public const string NAME = "PortableMirror";
        public const string VERSION = "1.2.9";
    }

    public class Main : MelonMod
    {
        public override void OnApplicationStart()
        {

            loadAssets();


            ModPrefs.RegisterCategory("PortableMirror", "PortableMirror");
            ModPrefs.RegisterPrefFloat("PortableMirror", "MirrorScaleX", 5f, "Mirror Scale X");
            ModPrefs.RegisterPrefFloat("PortableMirror", "MirrorScaleY", 3f, "Mirror Scale Y");
            ModPrefs.RegisterPrefFloat("PortableMirror", "MirrorDistance", 0f, "Mirror Distance");
            ModPrefs.RegisterPrefBool("PortableMirror", "OptimizedMirror", false, "Optimized Mirror");
            ModPrefs.RegisterPrefBool("PortableMirror", "CanPickupMirror", false, "Can Pickup Mirror");
            ModPrefs.RegisterPrefString("PortableMirror", "MirrorKeybind", "Alpha1", "Toggle Mirror Keybind");
            ModPrefs.RegisterPrefBool("PortableMirror", "QuickMenuOptions", true, "Quick Menu Settings Button");

            ModPrefs.RegisterCategory("PortableMirror45", "PortableMirror45");
            ModPrefs.RegisterPrefFloat("PortableMirror45", "MirrorScaleX", 5f, "Mirror Scale X");
            ModPrefs.RegisterPrefFloat("PortableMirror45", "MirrorScaleY", 3f, "Mirror Scale Y");
            ModPrefs.RegisterPrefFloat("PortableMirror45", "MirrorDistance", 0f, "Mirror Distance");
            ModPrefs.RegisterPrefBool("PortableMirror45", "OptimizedMirror45", false, "Optimized Mirror 45");
            ModPrefs.RegisterPrefBool("PortableMirror45", "CanPickup45Mirror", false, "Can Pickup 45 Mirror");
            ModPrefs.RegisterPrefBool("PortableMirror45", "enable45", true, "Enable 45 Mirror");

            ModPrefs.RegisterCategory("PortableMirrorCeiling", "PortableMirrorCeiling");
            ModPrefs.RegisterPrefFloat("PortableMirrorCeiling", "MirrorScaleX", 5f, "Mirror Scale X");
            ModPrefs.RegisterPrefFloat("PortableMirrorCeiling", "MirrorScaleZ", 3f, "Mirror Scale Z");
            ModPrefs.RegisterPrefFloat("PortableMirrorCeiling", "MirrorDistance", 2, "Mirror Distance");
            ModPrefs.RegisterPrefBool("PortableMirrorCeiling", "OptimizedMirrorCeiling", false, "Optimized Mirror Ceiling");
            ModPrefs.RegisterPrefBool("PortableMirrorCeiling", "CanPickupCeilingMirror", false, "Can Pickup Ceiling Mirror");
            ModPrefs.RegisterPrefBool("PortableMirrorCeiling", "enableCeiling", true, "Enable Ceiling Mirror");

            ModPrefs.RegisterCategory("PortableMirrorMicro", "PortableMirrorMicro");
            ModPrefs.RegisterPrefFloat("PortableMirrorMicro", "MirrorScaleX", .05f, "Mirror Scale X");
            ModPrefs.RegisterPrefFloat("PortableMirrorMicro", "MirrorScaleY", .1f, "Mirror Scale Y");
            ModPrefs.RegisterPrefFloat("PortableMirrorMicro", "GrabRange", .1f, "GrabRange");
            ModPrefs.RegisterPrefBool("PortableMirrorMicro", "OptimizedMirrorMicro", false, "Optimized MirrorMicro");
            ModPrefs.RegisterPrefBool("PortableMirrorMicro", "CanPickupMirrorMicro", false, "Can Pickup MirrorMicro");
            ModPrefs.RegisterPrefBool("PortableMirrorMicro", "enableMicro", true, "Enable Micro Mirror");


            ModPrefs.RegisterCategory("PortableMirrorTrans", "PortableMirrorTransparent");
            ModPrefs.RegisterPrefFloat("PortableMirrorTrans", "MirrorScaleX", 5f, "Mirror Scale X");
            ModPrefs.RegisterPrefFloat("PortableMirrorTrans", "MirrorScaleY", 3f, "Mirror Scale Y");
            ModPrefs.RegisterPrefFloat("PortableMirrorTrans", "MirrorDistance", 0f, "Mirror Distance");
            ModPrefs.RegisterPrefBool("PortableMirrorTrans", "OptimizedMirror", false, "Will other mirrors be turned Optimized");
            //ModPrefs.RegisterPrefBool("PortableMirrorTrans", "CanPickupMirror", false, "Can Pickup Mirror");
            //ModPrefs.RegisterPrefString("PortableMirrorTrans", "MirrorKeybind", "Alpha1", "Toggle Mirror Keybind");
            ModPrefs.RegisterPrefBool("PortableMirrorTrans", "enableTrans", true, "Enable Transparent Mirror");


            _mirrorScaleXBase = ModPrefs.GetFloat("PortableMirror", "MirrorScaleX");
            _mirrorScaleYBase = ModPrefs.GetFloat("PortableMirror", "MirrorScaleY");
            _MirrorDistance = ModPrefs.GetFloat("PortableMirror", "MirrorDistance");
            _optimizedMirrorBase = ModPrefs.GetBool("PortableMirror", "OptimizedMirror");
            _canPickupMirrorBase = ModPrefs.GetBool("PortableMirror", "CanPickupMirror");
            _mirrorKeybindBase = Utils.GetMirrorKeybind();
            _quickMenuOptions = ModPrefs.GetBool("PortableMirror", "QuickMenuOptions");

            _mirrorScaleX45 = ModPrefs.GetFloat("PortableMirror45", "MirrorScaleX");
            _mirrorScaleY45 = ModPrefs.GetFloat("PortableMirror45", "MirrorScaleY");
            _MirrorDistance45 = ModPrefs.GetFloat("PortableMirror45", "MirrorDistance");
            _optimizedMirror45 = ModPrefs.GetBool("PortableMirror45", "OptimizedMirror45");
            _CanPickup45Mirror = ModPrefs.GetBool("PortableMirror45", "CanPickup45Mirror");
            _enable45 = ModPrefs.GetBool("PortableMirror45", "enable45");

            _mirrorScaleXCeiling = ModPrefs.GetFloat("PortableMirrorCeiling", "MirrorScaleX");
            _mirrorScaleZCeiling = ModPrefs.GetFloat("PortableMirrorCeiling", "MirrorScaleZ");
            _MirrorDistanceCeiling = ModPrefs.GetFloat("PortableMirrorCeiling", "MirrorDistance");
            _optimizedMirrorCeiling = ModPrefs.GetBool("PortableMirrorCeiling", "OptimizedMirrorCeiling");
            _canPickupCeilingMirror = ModPrefs.GetBool("PortableMirrorCeiling", "CanPickupCeilingMirror");
            _enableCeiling = ModPrefs.GetBool("PortableMirrorCeiling", "enableCeiling");

            _mirrorScaleXMicro = ModPrefs.GetFloat("PortableMirrorMicro", "MirrorScaleX");
            _mirrorScaleMicro = ModPrefs.GetFloat("PortableMirrorMicro", "MirrorScaleY");
            _grabRangeMicro = ModPrefs.GetFloat("PortableMirrorMicro", "GrabRange"); 
            _optimizedMirrorMicro = ModPrefs.GetBool("PortableMirrorMicro", "OptimizedMirrorMicro");
            _canPickupMirrorMicro = ModPrefs.GetBool("PortableMirrorMicro", "CanPickupMirrorMicro");
            _enableMicro = ModPrefs.GetBool("PortableMirrorMicro", "enableMicro");

            _mirrorScaleXTrans = ModPrefs.GetFloat("PortableMirrorTrans", "MirrorScaleX");
            _mirrorScaleYTrans = ModPrefs.GetFloat("PortableMirrorTrans", "MirrorScaleY");
            _MirrorDistanceTrans = ModPrefs.GetFloat("PortableMirrorTrans", "MirrorDistance");
            _optimizedMirrorTrans = ModPrefs.GetBool("PortableMirrorTrans", "OptimizedMirror");


            MelonModLogger.Log("Base mod made by M-oons, modifcations by Nirvash");
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
            _enable45 = ModPrefs.GetBool("PortableMirror45", "enable45");
            _enableCeiling = ModPrefs.GetBool("PortableMirrorCeiling", "enableCeiling");
            _enableMicro = ModPrefs.GetBool("PortableMirrorMicro", "enableMicro");
            _enableTrans = ModPrefs.GetBool("PortableMirrorTrans", "enableTrans");
            _quickMenuOptions = ModPrefs.GetBool("PortableMirror", "QuickMenuOptions");
            if (_enable45 && ButtonList["45"] != null) ButtonList["45"].gameObject.active = true;
            else ButtonList["45"].gameObject.active = false;
            if (_enableCeiling && ButtonList["Ceiling"] != null) ButtonList["Ceiling"].gameObject.active = true;
            else ButtonList["Ceiling"].gameObject.active = false;
            if (_enableMicro && ButtonList["Micro"] != null) ButtonList["Micro"].gameObject.active = true;
            else ButtonList["Micro"].gameObject.active = false;
            if (_enableTrans && ButtonList["Trans"] != null) ButtonList["Trans"].gameObject.active = true;
            else ButtonList["Trans"].gameObject.active = false;
            if (_quickMenuOptions && ButtonList["Settings"] != null) ButtonList["Settings"].gameObject.active = true;
            else ButtonList["Settings"].gameObject.active = false;


            _oldMirrorScaleYBase = _mirrorScaleYBase;
            _oldMirrorDistance = _MirrorDistance;
            _mirrorScaleXBase = ModPrefs.GetFloat("PortableMirror", "MirrorScaleX");
            _mirrorScaleYBase = ModPrefs.GetFloat("PortableMirror", "MirrorScaleY");
            _MirrorDistance = ModPrefs.GetFloat("PortableMirror", "MirrorDistance");
            _optimizedMirrorBase = ModPrefs.GetBool("PortableMirror", "OptimizedMirror");
            _canPickupMirrorBase = ModPrefs.GetBool("PortableMirror", "CanPickupMirror");
            _mirrorKeybindBase = Utils.GetMirrorKeybind();

            if (_mirrorBase != null && Utils.GetVRCPlayer() != null)
            {
                _mirrorBase.transform.localScale = new Vector3(_mirrorScaleXBase, _mirrorScaleYBase, 1f);
                _mirrorBase.transform.position = new Vector3(_mirrorBase.transform.position.x, _mirrorBase.transform.position.y + ((_mirrorScaleYBase - _oldMirrorScaleYBase) / 2), _mirrorBase.transform.position.z  );
                _mirrorBase.transform.position += _mirrorBase.transform.forward * (_MirrorDistance - _oldMirrorDistance);
                _mirrorBase.GetOrAddComponent<VRC_MirrorReflection>().m_ReflectLayers = new LayerMask
                {
                    value = _optimizedMirrorBase ? optMirrorMask : fullMirrorMask
                };
                _mirrorBase.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupMirrorBase;
                _mirrorBase.layer = _canPickupMirrorBase ? 0 : 10;
            }

            //_oldMirrorScaleZ45 = _mirrorScaleZ45;
            _oldMirrorScaleY45 = _mirrorScaleY45;
            _oldMirrorDistance45 = _MirrorDistance45;
            _mirrorScaleX45 = ModPrefs.GetFloat("PortableMirror45", "MirrorScaleX");
            _mirrorScaleY45 = ModPrefs.GetFloat("PortableMirror45", "MirrorScaleY");
            _MirrorDistance45 = ModPrefs.GetFloat("PortableMirror45", "MirrorDistance");
            _optimizedMirror45 = ModPrefs.GetBool("PortableMirror45", "OptimizedMirror45");
            _CanPickup45Mirror = ModPrefs.GetBool("PortableMirror45", "CanPickup45Mirror");

            if (_mirror45 != null && Utils.GetVRCPlayer() != null)
            {
                //math here may or maynot be wrong, was using stuff from the ceiling mirror
                _mirror45.transform.localScale = new Vector3(_mirrorScaleX45, _mirrorScaleY45, 1f);
                _mirror45.transform.rotation = _mirror45.transform.rotation * Quaternion.AngleAxis(-45, Vector3.left);  

                //_mirror45.transform.position = new Vector3(_mirror45.transform.position.x, _mirror45.transform.position.y + ((_mirrorScaleZ45 - _oldMirrorScaleZ45) / _mirrorScaleY45), _mirror45.transform.position.z);
                _mirror45.transform.position = new Vector3(_mirror45.transform.position.x, _mirror45.transform.position.y + (_mirrorScaleY45 - _oldMirrorScaleY45), _mirror45.transform.position.z  );
                _mirror45.transform.position += _mirror45.transform.forward * (_MirrorDistance45 - _oldMirrorDistance45);
                _mirror45.transform.rotation = _mirror45.transform.rotation * Quaternion.AngleAxis(45, Vector3.left); 

                _mirror45.GetOrAddComponent<VRC_MirrorReflection>().m_ReflectLayers = new LayerMask
                {
                    value = _optimizedMirror45 ? optMirrorMask : fullMirrorMask
                };
                _mirror45.GetOrAddComponent<VRC_Pickup>().pickupable = _CanPickup45Mirror;
                _mirror45.layer = _CanPickup45Mirror ? 0 : 10;
            }

            _oldMirrorScaleZCeiling = _mirrorScaleZCeiling;
            _oldMirrorDistanceCeiling = _MirrorDistanceCeiling;
            _mirrorScaleXCeiling = ModPrefs.GetFloat("PortableMirrorCeiling", "MirrorScaleX");
            _mirrorScaleZCeiling = ModPrefs.GetFloat("PortableMirrorCeiling", "MirrorScaleZ");
            _MirrorDistanceCeiling = ModPrefs.GetFloat("PortableMirrorCeiling", "MirrorDistance");
            _optimizedMirrorCeiling = ModPrefs.GetBool("PortableMirrorCeiling", "OptimizedMirrorCeiling");
            _canPickupCeilingMirror = ModPrefs.GetBool("PortableMirrorCeiling", "CanPickupCeilingMirror");

            if (_mirrorCeiling != null && Utils.GetVRCPlayer() != null)
            {
                _mirrorCeiling.transform.localScale = new Vector3(_mirrorScaleXCeiling, _mirrorScaleZCeiling, 1f);
                //_mirrorCeiling.transform.position = new Vector3(_mirrorCeiling.transform.position.x, _mirrorCeiling.transform.position.y + ((_mirrorScaleZCeiling - _oldMirrorScaleZCeiling) / _MirrorDistanceCeiling), _mirrorCeiling.transform.position.z);
                _mirrorCeiling.transform.position = new Vector3(_mirrorCeiling.transform.position.x, _mirrorCeiling.transform.position.y + (_MirrorDistanceCeiling - _oldMirrorDistanceCeiling), _mirrorCeiling.transform.position.z);
                _mirrorCeiling.GetOrAddComponent<VRC_MirrorReflection>().m_ReflectLayers = new LayerMask
                {
                    value = _optimizedMirrorCeiling ? optMirrorMask : fullMirrorMask
                };
                _mirrorCeiling.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupCeilingMirror;
                _mirrorCeiling.layer = _canPickupCeilingMirror ? 0 : 10;
            }

            _oldMirrorScaleYMicro = _mirrorScaleMicro;
            _mirrorScaleXMicro = ModPrefs.GetFloat("PortableMirrorMicro", "MirrorScaleX");
            _mirrorScaleMicro = ModPrefs.GetFloat("PortableMirrorMicro", "MirrorScaleY");
            _grabRangeMicro = ModPrefs.GetFloat("PortableMirrorMicro", "GrabRange");
            _optimizedMirrorMicro = ModPrefs.GetBool("PortableMirrorMicro", "OptimizedMirrorMicro");
            _canPickupMirrorMicro = ModPrefs.GetBool("PortableMirrorMicro", "CanPickupMirrorMicro");


            if (_mirrorMicro != null && Utils.GetVRCPlayer() != null)
            {
                _mirrorMicro.GetOrAddComponent<VRC_Pickup>().proximity = _grabRangeMicro;

                _mirrorMicro.transform.localScale = new Vector3(_mirrorScaleXMicro, _mirrorScaleMicro, 1f);
                _mirrorMicro.transform.position = new Vector3(_mirrorMicro.transform.position.x, _mirrorMicro.transform.position.y + ((_mirrorScaleMicro - _oldMirrorScaleYMicro) / 2), _mirrorMicro.transform.position.z);
                _mirrorMicro.GetOrAddComponent<VRC_MirrorReflection>().m_ReflectLayers = new LayerMask
                {
                    value = _optimizedMirrorMicro ? optMirrorMask : fullMirrorMask
                };
                _mirrorMicro.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupMirrorMicro;
                _mirrorMicro.layer = _canPickupMirrorMicro ? 0 : 10;
            }



            _oldMirrorScaleYTrans = _mirrorScaleYTrans;
            _oldMirrorDistanceTrans = _MirrorDistanceTrans;
            _mirrorScaleXTrans = ModPrefs.GetFloat("PortableMirrorTrans", "MirrorScaleX");
            _mirrorScaleYTrans = ModPrefs.GetFloat("PortableMirrorTrans", "MirrorScaleY");
            _MirrorDistanceTrans = ModPrefs.GetFloat("PortableMirrorTrans", "MirrorDistance");
            _optimizedMirrorTrans = ModPrefs.GetBool("PortableMirrorTrans", "OptimizedMirror");

            if (_mirrorTrans != null && Utils.GetVRCPlayer() != null)
            {
                _mirrorTrans.transform.localScale = new Vector3(_mirrorScaleXTrans, _mirrorScaleYTrans, 1f);
                //_mirrorTrans.transform.position = new Vector3(_mirrorTrans.transform.position.x, _mirrorTrans.transform.position.y + ((_mirrorScaleYTrans - _oldMirrorScaleYTrans) / 2), _mirrorTrans.transform.position.z);
                _mirrorTrans.transform.position += _mirrorTrans.transform.forward * (_MirrorDistanceTrans - _oldMirrorDistanceTrans);
            }


        }

        private IEnumerator CreateQuickMenuButton()
        {
            while (QuickMenu.prop_QuickMenu_0 == null) yield return null;

            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Toggle\nPortable\nMirror", () =>
            {
                if (Utils.GetVRCPlayer() == null) return;
                ToggleMirror();
            });
            
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

        private void QuickMenuOptions()
        {
            var mirrorMenu = ExpansionKitApi.CreateCustomQuickMenuPage(LayoutDescriptionCustom.QuickMenu3Column);
            mirrorMenu.AddToggleButton("Portable Mirror", (action) =>
            {
                if (Utils.GetVRCPlayer() == null) return;
                ToggleMirror();
                //mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions();
            }, () => _mirrorBase != null);
            mirrorMenu.AddSimpleButton(ModPrefs.GetBool("PortableMirror", "OptimizedMirror") ?  "Optimized Mirror" : "Full Mirror", () => {
                ModPrefs.SetBool("PortableMirror", "OptimizedMirror", !ModPrefs.GetBool("PortableMirror", "OptimizedMirror"));
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
                //MelonModLogger.Log("distnace " + ModPrefs.GetFloat("PortableMirror", "MirrorDistance"));
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
                    //mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions();
                }, () => _mirror45 != null);
                mirrorMenu.AddSimpleButton(ModPrefs.GetBool("PortableMirror45", "OptimizedMirror45") ? "Optimized Mirror" : "Full Mirror", () =>
                {
                    ModPrefs.SetBool("PortableMirror45", "OptimizedMirror45", !ModPrefs.GetBool("PortableMirror45", "OptimizedMirror45"));
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
                    //mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions();
                }, () => _mirrorCeiling != null);
                mirrorMenu.AddSimpleButton(ModPrefs.GetBool("PortableMirrorCeiling", "OptimizedMirrorCeiling") ? "Optimized Mirror" : "Full Mirror", () =>
                {
                    ModPrefs.SetBool("PortableMirrorCeiling", "OptimizedMirrorCeiling", !ModPrefs.GetBool("PortableMirrorCeiling", "OptimizedMirrorCeiling"));
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
                    //mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions();
                }, () => _mirrorMicro != null);
                mirrorMenu.AddSimpleButton(ModPrefs.GetBool("PortableMirrorMicro", "OptimizedMirrorMicro") ? "Optimized Mirror" : "Full Mirror", () =>
                {
                    ModPrefs.SetBool("PortableMirrorMicro", "OptimizedMirrorMicro", !ModPrefs.GetBool("PortableMirrorMicro", "OptimizedMirrorMicro"));
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

        private void QuickMenuOptions2Sep()
        {
            //Mirror - Wider - Narrower
            //Taller - Shorter 
            //45
            //Ceiling

            //Close - Page 1

            var mirrorMenu = ExpansionKitApi.CreateCustomQuickMenuPage(LayoutDescriptionCustom.QuickMenu3Column);
            mirrorMenu.AddLabel("Mirror");
            mirrorMenu.AddSimpleButton("Wider", () => {
                ModPrefs.SetFloat("PortableMirror", "MirrorScaleX", ModPrefs.GetFloat("PortableMirror", "MirrorScaleX") + .5f);
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
            });
            mirrorMenu.AddSimpleButton("Narrower", () => {
                ModPrefs.SetFloat("PortableMirror", "MirrorScaleX", ModPrefs.GetFloat("PortableMirror", "MirrorScaleX") -.5f);
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2(); ;
            });
            mirrorMenu.AddSpacer();
            mirrorMenu.AddSimpleButton("Taller", () => {
                ModPrefs.SetFloat("PortableMirror", "MirrorScaleY", ModPrefs.GetFloat("PortableMirror", "MirrorScaleY") + .5f);
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2(); ;
            });
            mirrorMenu.AddSimpleButton("Shorter", () => {
                ModPrefs.SetFloat("PortableMirror", "MirrorScaleY", ModPrefs.GetFloat("PortableMirror", "MirrorScaleY") - .5f);
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
            });
            //
            mirrorMenu.AddLabel("45 Mirror");
            mirrorMenu.AddSimpleButton("Wider", () => {
                ModPrefs.SetFloat("PortableMirror45", "MirrorScaleX", ModPrefs.GetFloat("PortableMirror45", "MirrorScaleX") + .5f);
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
            });
            mirrorMenu.AddSimpleButton("Narrower", () => {
                ModPrefs.SetFloat("PortableMirror45", "MirrorScaleX", ModPrefs.GetFloat("PortableMirror45", "MirrorScaleX") - .5f);
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
            });
            mirrorMenu.AddSpacer();
            mirrorMenu.AddSimpleButton("Taller", () => {
                ModPrefs.SetFloat("PortableMirror45", "MirrorScaleY", ModPrefs.GetFloat("PortableMirror45", "MirrorScaleY") + .5f);
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
            });
            mirrorMenu.AddSimpleButton("Shorter", () => {
                ModPrefs.SetFloat("PortableMirror45", "MirrorScaleY", ModPrefs.GetFloat("PortableMirror45", "MirrorScaleY") - .5f);
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
            });
            //
            mirrorMenu.AddLabel("Ceiling Mirror");
            mirrorMenu.AddSimpleButton("Wider", () => {
                ModPrefs.SetFloat("PortableMirrorCeiling", "MirrorScaleX", ModPrefs.GetFloat("PortableMirrorCeiling", "MirrorScaleX") + .5f);
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
            });
            mirrorMenu.AddSimpleButton("Narrower", () => {
                ModPrefs.SetFloat("PortableMirrorCeiling", "MirrorScaleX", ModPrefs.GetFloat("PortableMirrorCeiling", "MirrorScaleX") - .5f);
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
            });
            mirrorMenu.AddSpacer();
            mirrorMenu.AddSimpleButton("Taller", () => {
                ModPrefs.SetFloat("PortableMirrorCeiling", "MirrorScaleZ", ModPrefs.GetFloat("PortableMirrorCeiling", "MirrorScaleZ") + .5f);
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2(); ;
            });
            mirrorMenu.AddSimpleButton("Shorter", () => {
                ModPrefs.SetFloat("PortableMirrorCeiling", "MirrorScaleZ", ModPrefs.GetFloat("PortableMirrorCeiling", "MirrorScaleZ") - .5f);
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
            });
            //
            mirrorMenu.AddSimpleButton($"Transparent\n{(_mirrorTrans != null ? "-Enabled-" : "-Disabled-")}", () =>
            {
                if (Utils.GetVRCPlayer() == null) return;
                ToggleMirrorTrans();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
            });
            mirrorMenu.AddSimpleButton(ModPrefs.GetBool("PortableMirrorTrans", "OptimizedMirror") ? "Others Optimized" : "Others Full", () =>
            {
                ModPrefs.SetBool("PortableMirrorTrans", "OptimizedMirror", !ModPrefs.GetBool("PortableMirrorTrans", "OptimizedMirror"));
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
            });
            
            mirrorMenu.AddLabel($"Distance: {ModPrefs.GetFloat("PortableMirrorTrans", "MirrorDistance")}");

            mirrorMenu.AddSimpleButton($"Page 1", () => {
                mirrorMenu.Hide();
                QuickMenuOptions();
            });

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


            mirrorMenu.Show();
        }
        private void QuickMenuOptions2()
        {
            //Mirror - Wider - Narrower
            //Taller - Shorter 
            //45
            //Ceiling

            //Close - Page 1

            //1
            var mirrorMenu = ExpansionKitApi.CreateCustomQuickMenuPage(LayoutDescriptionCustom.QuickMenu3Column);
            mirrorMenu.AddToggleButton("Portable Mirror", (action) =>
            {
                if (Utils.GetVRCPlayer() == null) return;
                ToggleMirror();
                //mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions();
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
                //mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions();
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
                //mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions();
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
                //mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions();
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
                //mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
            }, () => _mirrorTrans != null);
            mirrorMenu.AddSimpleButton(ModPrefs.GetBool("PortableMirrorTrans", "OptimizedMirror") ? "Others Optimized" : "Others Full", () =>
            {
                ModPrefs.SetBool("PortableMirrorTrans", "OptimizedMirror", !ModPrefs.GetBool("PortableMirrorTrans", "OptimizedMirror"));
                OnModSettingsApplied();
                mirrorMenu.Hide(); mirrorMenu = null; QuickMenuOptions2();
            });
            mirrorMenu.AddSpacer();
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
                ToggleMirrorTrans();
            }
        }

        private void ToggleMirror()
        {
            if (_mirrorBase != null)
            {
                UnityEngine.Object.Destroy(_mirrorBase);
                _mirrorBase = null;
            }
            else
            {
                VRCPlayer player = Utils.GetVRCPlayer();
                Vector3 pos = player.transform.position + player.transform.forward + (player.transform.forward * _MirrorDistance);
                pos.y += _mirrorScaleYBase / 2;
                GameObject mirror = GameObject.CreatePrimitive(PrimitiveType.Quad);
                mirror.transform.position = pos;
                mirror.transform.rotation = player.transform.rotation;
                mirror.transform.localScale = new Vector3(_mirrorScaleXBase, _mirrorScaleYBase, 1f);
                mirror.name = "PortableMirror";
                UnityEngine.Object.Destroy(mirror.GetComponent<Collider>());
                mirror.GetOrAddComponent<BoxCollider>().size = new Vector3(1f, 1f, 0.05f);
                mirror.GetOrAddComponent<BoxCollider>().isTrigger = true;
                mirror.GetOrAddComponent<MeshRenderer>().material.shader = Shader.Find("FX/MirrorReflection");
                mirror.GetOrAddComponent<VRC_MirrorReflection>().m_ReflectLayers = new LayerMask
                {
                    value = _optimizedMirrorBase ? optMirrorMask : fullMirrorMask
                };
                mirror.GetOrAddComponent<VRC_Pickup>().proximity = 3f;
                mirror.layer = _canPickupMirrorBase ? 0 : 10; //10 - Hides the new mirror from reflecting in other mirrors. 0 - Needs to be in an interactable layer when picked up
                mirror.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupMirrorBase;
                mirror.GetOrAddComponent<VRC_Pickup>().allowManipulationWhenEquipped = false;
                mirror.GetOrAddComponent<Rigidbody>().useGravity = false;
                mirror.GetOrAddComponent<Rigidbody>().isKinematic = true;
                _mirrorBase = mirror;
            }
        }

        private void ToggleMirror45()
        {
            if (_mirror45 != null)
            {
                UnityEngine.Object.Destroy(_mirror45);
                _mirror45 = null;
            }
            else
            {
                VRCPlayer player = Utils.GetVRCPlayer();
                Vector3 pos = player.transform.position + player.transform.forward + (player.transform.forward * _MirrorDistance45);
                //pos.y += _mirrorScaleZ45 / _mirrorScaleY45;
                //pos.y +=  _mirrorScaleY45;
                pos.y += _mirrorScaleY45 / 2;  //Switch to using method from first mirror, may switch back to raw distance? 
                GameObject mirror = GameObject.CreatePrimitive(PrimitiveType.Quad);
                mirror.transform.position = pos;
                mirror.transform.rotation = player.transform.rotation;
                mirror.transform.rotation = mirror.transform.rotation * Quaternion.AngleAxis(45, Vector3.left);  // Sets the transform's current rotation to a new rotation that rotates 30 degrees around the y-axis(Vector3.up)
                //mirror.transform.rotation = Quaternion.AngleAxis(180, Vector3.up);  // Sets the transform's current rotation to a new rotation that rotates 30 degrees around the y-axis(Vector3.up)
                mirror.transform.localScale = new Vector3(_mirrorScaleX45, _mirrorScaleY45, 1f);
                mirror.name = "PortableMirror45";
                UnityEngine.Object.Destroy(mirror.GetComponent<Collider>());
                mirror.GetOrAddComponent<BoxCollider>().size = new Vector3(1f, 1f, 0.05f);
                mirror.GetOrAddComponent<BoxCollider>().isTrigger = true;
                mirror.GetOrAddComponent<MeshRenderer>().material.shader = Shader.Find("FX/MirrorReflection");
                mirror.GetOrAddComponent<VRC_MirrorReflection>().m_ReflectLayers = new LayerMask
                {
                    // value = _optimizedMirror45 ? optMirrorMask : -1025
                    value = _optimizedMirror45 ? optMirrorMask : fullMirrorMask
                };
                // mirror.GetOrAddComponent<VRC_MirrorReflection>().m_ReflectLayers = -1 & ~UiLayer & ~UiMenuLayer & ~PlayerLocalLayer;
                //mirror.layer = 10; //set to PlayerLocalLayer
                mirror.layer = _CanPickup45Mirror ? 0 : 10;
                mirror.GetOrAddComponent<VRC_Pickup>().proximity = 3.0f; // Made higher
                mirror.GetOrAddComponent<VRC_Pickup>().pickupable = _CanPickup45Mirror;
                mirror.GetOrAddComponent<VRC_Pickup>().allowManipulationWhenEquipped = false;
                mirror.GetOrAddComponent<Rigidbody>().useGravity = false;
                mirror.GetOrAddComponent<Rigidbody>().isKinematic = true;
                _mirror45 = mirror;
                //MelonModLogger.Log("valie"+ (-1 & ~UiLayer & ~UiMenuLayer & ~PlayerLocalLayer));
            }
        }

        private void ToggleMirrorCeiling()
        {
            
            if (_mirrorCeiling != null)
            {
                UnityEngine.Object.Destroy(_mirrorCeiling);
                _mirrorCeiling = null;
            }
            else
            {
                VRCPlayer player = Utils.GetVRCPlayer();
                //Vector3 pos = player.transform.position + player.transform.up;//Probably shouldnt have changed the second transform?

                Vector3 pos = GameObject.Find(player.gameObject.name + "/AnimationController/HeadAndHandIK/HipTarget").transform.position + (player.transform.up); // Bases mirror position off of hip, to allow for play space moving 
                MelonModLogger.Log($"x:{GameObject.Find(player.gameObject.name + "/AnimationController/HeadAndHandIK/HipTarget").transform.position.x}, y:{GameObject.Find(player.gameObject.name + "/AnimationController/HeadAndHandIK/HipTarget").transform.position.y}, z:{GameObject.Find(player.gameObject.name + "/AnimationController/HeadAndHandIK/HipTarget").transform.position.z}");
                //pos.y += _mirrorScaleZCeiling / _MirrorDistanceCeiling;
                pos.y += _MirrorDistanceCeiling;
                GameObject mirror = GameObject.CreatePrimitive(PrimitiveType.Quad);
                mirror.transform.position = pos;
                mirror.transform.rotation = player.transform.rotation;
                mirror.transform.rotation = Quaternion.AngleAxis(90, Vector3.left);  // Sets the transform's current rotation to a new rotation that rotates 30 degrees around the y-axis(Vector3.up)
                mirror.transform.localScale = new Vector3(_mirrorScaleXCeiling, _mirrorScaleZCeiling, 1f);
                mirror.name = "PortableMirrorCeiling";
                UnityEngine.Object.Destroy(mirror.GetComponent<Collider>());
                mirror.GetOrAddComponent<BoxCollider>().size = new Vector3(1f, 1f, 0.05f);
                mirror.GetOrAddComponent<BoxCollider>().isTrigger = true;
                mirror.GetOrAddComponent<MeshRenderer>().material.shader = Shader.Find("FX/MirrorReflection");
                mirror.GetOrAddComponent<VRC_MirrorReflection>().m_ReflectLayers = new LayerMask
                {
                    // value = _optimizedMirrorCeiling ? optMirrorMask : -1025
                    value = _optimizedMirrorCeiling ? optMirrorMask : fullMirrorMask
                };
                // mirror.GetOrAddComponent<VRC_MirrorReflection>().m_ReflectLayers = -1 & ~UiLayer & ~UiMenuLayer & ~PlayerLocalLayer;
                //mirror.layer = 10; //set to PlayerLocalLayer
                mirror.layer = _canPickupCeilingMirror ? 0 : 10;
                mirror.GetOrAddComponent<VRC_Pickup>().proximity = 3.0f; // Made higher
                mirror.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupCeilingMirror;
                mirror.GetOrAddComponent<VRC_Pickup>().allowManipulationWhenEquipped = false;
                mirror.GetOrAddComponent<Rigidbody>().useGravity = false;
                mirror.GetOrAddComponent<Rigidbody>().isKinematic = true;
                _mirrorCeiling = mirror;
                //MelonModLogger.Log("valie"+ (-1 & ~UiLayer & ~UiMenuLayer & ~PlayerLocalLayer));
            }
        }

        private void ToggleMirrorMicro()
        {
            if (_mirrorMicro != null)
            {
                UnityEngine.Object.Destroy(_mirrorMicro);
                _mirrorMicro = null;
            }
            else
            {
                VRCPlayer player = Utils.GetVRCPlayer();
                // Vector3 pos = player.transform.position + player.transform.forward;
                //Vector3 pos = player.transform.Find("Avatar").transform.position;
                // Vector3 pos = player.field_Internal_GameObject_0.transform.position; //Gets position of avatar not pill
                //Vector3 pos = player.field_Internal_GameObject_0.transform.position + ( player.field_Internal_GameObject_0.transform.forward * _mirrorScaleMicro) ; //Gets position of avatar not pill
                Vector3 pos = GameObject.Find(player.gameObject.name + "/AnimationController/HeadAndHandIK/HeadEffector").transform.position + (player.transform.forward * _mirrorScaleMicro); // Gets position of Head and moves mirror forward by the Y scale.
                pos.y -= _mirrorScaleMicro / 4;
                GameObject mirror = GameObject.CreatePrimitive(PrimitiveType.Quad);
                mirror.transform.position = pos;
                mirror.transform.rotation = player.transform.rotation;
                mirror.transform.localScale = new Vector3(_mirrorScaleXMicro, _mirrorScaleMicro, 1f);
                mirror.name = "PortableMirror";
                UnityEngine.Object.Destroy(mirror.GetComponent<Collider>());
                mirror.GetOrAddComponent<BoxCollider>().size = new Vector3(10f, 10f, 0.05f);//Originallly 1f, 1f, set larger to make easier to grab
                mirror.GetOrAddComponent<BoxCollider>().isTrigger = true;
                mirror.GetOrAddComponent<MeshRenderer>().material.shader = Shader.Find("FX/MirrorReflection");
                mirror.GetOrAddComponent<VRC_MirrorReflection>().m_ReflectLayers = new LayerMask
                {
                    value = _optimizedMirrorMicro ? optMirrorMask : fullMirrorMask
                };
                mirror.GetOrAddComponent<VRC_Pickup>().proximity = _grabRangeMicro; // 100f; //Set to a large number cause play space moving
                mirror.layer = _canPickupMirrorMicro ? 0 : 10; //10 - Hides the new mirror from reflecting in other mirrors. 0 - Needs to be in an interactable layer when picked up
                mirror.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupMirrorMicro;
                mirror.GetOrAddComponent<VRC_Pickup>().allowManipulationWhenEquipped = false;
                mirror.GetOrAddComponent<Rigidbody>().useGravity = false;
                mirror.GetOrAddComponent<Rigidbody>().isKinematic = true;
                _mirrorMicro = mirror;
            }
        }

        private void ToggleMirrorTrans()
        {
            if (_mirrorTrans != null)
            {
                UnityEngine.Object.Destroy(_mirrorTrans);
                _mirrorTrans = null;
            }
            else
            {
                foreach (var vrcMirrorReflection in UnityEngine.Object.FindObjectsOfType<VRC_MirrorReflection>()) // https://github.com/knah/VRCMods/blob/master/MirrorResolutionUnlimiter/UiExtensionsAddon.cs
                    if (vrcMirrorReflection.isActiveAndEnabled)
                        vrcMirrorReflection.m_ReflectLayers = //Force all mirrors to not reflect "Mirror/TransparentBackground" - Set all mirrors to Optimized or Full
                             _optimizedMirrorTrans ? optMirrorMask : fullMirrorMask;

                VRCPlayer player = Utils.GetVRCPlayer();
                Vector3 pos = player.transform.position + player.transform.forward + (player.transform.forward * _MirrorDistanceTrans);
                //pos.y += _mirrorScaleYBase / 2;
                GameObject mirror = GameObject.Instantiate(mirrorPrefab);
                //GameObject mirror = GameObject.CreatePrimitive(PrimitiveType.Quad);
                mirror.transform.position = pos;
                mirror.transform.rotation = player.transform.rotation;
                mirror.transform.localScale = new Vector3(_mirrorScaleXTrans, _mirrorScaleYTrans, 1f);
                mirror.name = "PortableMirrorTrans";
                //UnityEngine.Object.Destroy(mirror.GetComponent<Collider>());
                //mirror.GetOrAddComponent<BoxCollider>().size = new Vector3(1f, 1f, 0.05f);
                //mirror.GetOrAddComponent<BoxCollider>().isTrigger = true;
                //mirror.GetOrAddComponent<MeshRenderer>().material.shader = Shader.Find("Mirror/VRCPlayersOnlyMirrorCutout");
                //mirror.GetOrAddComponent<VRC_MirrorReflection>().m_ReflectLayers = new LayerMask
                //{
                //    value = _optimizedMirrorBase ? optMirrorMask : fullMirrorMask
                //    value = 786944 //1 << 18 | 1 << 9 | 1 << 19 = 786944 - MirrorReflectionLayer, PlayerLayer, reserved2
                // };
                // mirror.GetOrAddComponent<VRC_Pickup>().proximity = 3f;
                //mirror.layer = _canPickupMirrorBase ? 0 : 10; //10 - Hides the new mirror from reflecting in other mirrors. 0 - Needs to be in an interactable layer when picked up
                //mirror.GetOrAddComponent<VRC_Pickup>().pickupable = _canPickupMirrorBase;
                //mirror.GetOrAddComponent<VRC_Pickup>().allowManipulationWhenEquipped = false;
                //mirror.GetOrAddComponent<Rigidbody>().useGravity = false;
                //mirror.GetOrAddComponent<Rigidbody>().isKinematic = true;

                //GameObject transBackground = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //transBackground.transform.parent = mirror.transform;
                // transBackground.transform.localScale = new Vector3(10, 10, 10);
                //UnityEngine.Object.Destroy(transBackground.GetComponent<Collider>());
                // transBackground.GetOrAddComponent<MeshRenderer>().material.shader = Shader.Find("Mirror/TransparentBackground");
                //transBackground.layer = 18; // MirrorReflectionLayer 

                _mirrorTrans = mirror;
            }
        }



        
        private void loadAssets()
        {//https://github.com/ddakebono/BTKSASelfPortrait/blob/master/BTKSASelfPortrait.cs
            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PortableMirror_Combined.transmirror"))
            {//Load shaders first, without this doesn't seem to work. 
                //Log("Loaded Embedded resource", true);
                using (var tempStream = new MemoryStream((int)assetStream.Length))
                {
                    assetStream.CopyTo(tempStream);
                    assetBundle2 = AssetBundle.LoadFromMemory_Internal(tempStream.ToArray(), 0);
                    assetBundle2.hideFlags |= HideFlags.DontUnloadUnusedAsset;
                }
            }

            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PortableMirror_Combined.transcutoutmirrorprefab"))
            {
                //Log("Loaded Embedded resource", true);
                using (var tempStream = new MemoryStream((int)assetStream.Length))
                {
                    assetStream.CopyTo(tempStream);
                    assetBundle = AssetBundle.LoadFromMemory_Internal(tempStream.ToArray(), 0);
                    assetBundle.hideFlags |= HideFlags.DontUnloadUnusedAsset;
                }
            }

            if (assetBundle != null)
            {
                mirrorPrefab = assetBundle.LoadAsset_Internal("VRCPlayersOnlyMirrorCutout", Il2CppType.Of<GameObject>()).Cast<GameObject>();
                mirrorPrefab.hideFlags |= HideFlags.DontUnloadUnusedAsset;
                //MelonModLogger.Log(ConsoleColor.Red, $"{(mirrorPrefab is null ? "mirrorPrefab is null" : "mirrorPrefab is !null")}");
            }
            else MelonModLogger.Log(ConsoleColor.Red, "Bundle was null");
            //Log("Loaded Assets Successfully!", true);

        }

        public Dictionary<string, Transform> ButtonList = new Dictionary<string, Transform>();

        private static int PlayerLayer = 1 << 9; // https://github.com/knah/VRCMods/blob/master/MirrorResolutionUnlimiter/UiExtensionsAddon.cs
        private static int PlayerLocalLayer = 1 << 10;
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
        private bool _optimizedMirrorBase;
        private bool _canPickupMirrorBase;
        private KeyCode _mirrorKeybindBase;
        private bool _quickMenuOptions;

        private GameObject _mirror45;
        private float _mirrorScaleX45;
        //private float _mirrorScaleZ45;
        private float _mirrorScaleY45;
        private float _MirrorDistance45;
        private float _oldMirrorDistance45;
        //private float _oldMirrorScaleZ45;
        private float _oldMirrorScaleY45;
        private bool _optimizedMirror45;
        private bool _CanPickup45Mirror;
        private bool _enable45;

        private GameObject _mirrorCeiling;
        private float _mirrorScaleXCeiling;
        private float _mirrorScaleZCeiling;
        private float _MirrorDistanceCeiling;
        private float _oldMirrorDistanceCeiling;
        private float _oldMirrorScaleZCeiling;
        private bool _optimizedMirrorCeiling;
        private bool _canPickupCeilingMirror;
        private bool _enableCeiling;

        private GameObject _mirrorMicro;
        private float _mirrorScaleXMicro;
        private float _mirrorScaleMicro;
        private float _grabRangeMicro;
        private float _oldMirrorScaleYMicro;
        private bool _optimizedMirrorMicro;
        private bool _canPickupMirrorMicro;
        private bool _enableMicro;


        private GameObject _mirrorTrans;
        private float _mirrorScaleXTrans;
        private float _mirrorScaleYTrans;
        private float _MirrorDistanceTrans;
        private float _oldMirrorDistanceTrans;
        private float _oldMirrorScaleYTrans;
        private bool _optimizedMirrorTrans;
        //private bool _canPickupMirrorTrans;
        private bool _enableTrans;


    }
}


namespace UIExpansionKit.API
{

    public struct LayoutDescriptionCustom
    {
        public static LayoutDescription QuickMenu3Column = new LayoutDescription { NumColumns = 3, RowHeight = 380 / 8, NumRows = 8 };
    }
}