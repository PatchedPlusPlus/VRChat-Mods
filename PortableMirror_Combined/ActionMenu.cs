using System.Reflection;
using MelonLoader;
using UnityEngine;
using UnhollowerRuntimeLib;
using System.IO;
using ActionMenuApi.Api;

namespace PortableMirror
{
    //I could do this better and have less duplicate code, but copy pasting is so much faster... // I gotcha mate! - Davi
    public class CustomActionMenu
    {
        public static AssetBundle assetBundleIcons;
        public static Texture2D DistMinus, DistPlus, SizeMinus, SizePlus, Grab, MirrorBase, Mirror45, MirrorCeil, MirrorCut, MirrorFull, MirrorMicro, MirrorOpt, MirrorTrans, MirrorRuler, MirrorOptions, SettingsGear, TransPlus, TransMinus, CameraMirror, DistAdjIcon, GrabDistPlus, GrabDistMinus;

        private static void loadAssets()
        {
            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PortableMirrorMod.mirroricons"))
            {
                using (var tempStream = new MemoryStream((int)assetStream.Length))
                {
                    assetStream.CopyTo(tempStream);
                    assetBundleIcons = AssetBundle.LoadFromMemory_Internal(tempStream.ToArray(), 0);
                    assetBundleIcons.hideFlags |= HideFlags.DontUnloadUnusedAsset;
                }
            }

            if (assetBundleIcons != null)
            {
                try { DistMinus = LoadTexture("icons8-distance-128-Distance-Minus.png"); } catch { MelonLogger.Error("Failed to load image from asset bundle"); }
                try { DistPlus = LoadTexture("icons8-distance-128-Distance-Plus.png"); } catch { MelonLogger.Error("Failed to load image from asset bundle"); }
                try { SizeMinus = LoadTexture("icons8-distance-128-Size-Minus.png"); } catch { MelonLogger.Error("Failed to load image from asset bundle"); }
                try { SizePlus = LoadTexture("icons8-distance-128-Size-Plus.png"); } catch { MelonLogger.Error("Failed to load image from asset bundle"); }
                try { Grab = LoadTexture("icons8-grab-100-edit.png"); } catch { MelonLogger.Error("Failed to load image from asset bundle"); }
                try { MirrorBase = LoadTexture("icons8-rectangular-mirror-128-BW-Edit.png"); } catch { MelonLogger.Error("Failed to load image from asset bundle"); }
                try { Mirror45 = LoadTexture("icons8-rectangular-mirror-128-BW-Edit-45.png"); } catch { MelonLogger.Error("Failed to load image from asset bundle"); }
                try { MirrorCeil = LoadTexture("icons8-rectangular-mirror-128-BW-Edit-Ceiling.png"); } catch { MelonLogger.Error("Failed to load image from asset bundle"); }
                try { MirrorCut = LoadTexture("icons8-rectangular-mirror-128-BW-Edit-Cut.png"); } catch { MelonLogger.Error("Failed to load image from asset bundle"); }
                try { MirrorFull = LoadTexture("icons8-rectangular-mirror-128-BW-Edit-Full.png"); } catch { MelonLogger.Error("Failed to load image from asset bundle"); }
                try { MirrorMicro = LoadTexture("icons8-rectangular-mirror-128-BW-Edit-Micro.png"); } catch { MelonLogger.Error("Failed to load image from asset bundle"); }
                try { MirrorOpt = LoadTexture("icons8-rectangular-mirror-128-BW-Edit-Opt.png"); } catch { MelonLogger.Error("Failed to load image from asset bundle"); }
                try { MirrorTrans = LoadTexture("icons8-rectangular-mirror-128-BW-Edit-Trans.png"); } catch { MelonLogger.Error("Failed to load image from asset bundle"); }
                try { MirrorRuler = LoadTexture("icons8-ruler-128-edit.png"); } catch { MelonLogger.Error("Failed to load image from asset bundle"); }
                try { MirrorOptions = LoadTexture("icons8-rectangular-mirror-128-BW-Edit-Options.png"); } catch { MelonLogger.Error("Failed to load image from asset bundle"); }
                try { SettingsGear = LoadTexture("icons8-automation-100-edit.png"); } catch { MelonLogger.Error("Failed to load image from asset bundle"); }
                try { TransPlus = LoadTexture("icons8-rectangular-mirror-128-BW-Edit-TransPlus.png"); } catch { MelonLogger.Error("Failed to load image from asset bundle"); }
                try { TransMinus = LoadTexture("icons8-rectangular-mirror-128-BW-Edit-TransMinus.png"); } catch { MelonLogger.Error("Failed to load image from asset bundle"); }
                try { CameraMirror = LoadTexture("icons8-rectangular-mirror-128-BW-Camera.png"); } catch { MelonLogger.Error("Failed to load image from asset bundle"); }
                try { DistAdjIcon = LoadTexture("icons8-distance-128-Distance-Adj.png"); } catch { MelonLogger.Error("Failed to load image from asset bundle"); }
                try { GrabDistPlus = LoadTexture("icons8-grab-100-edit-Distance_Plus.png"); } catch { MelonLogger.Error("Failed to load image from asset bundle"); }
                try { GrabDistMinus = LoadTexture("icons8-grab-100-edit-Distance_Minus.png"); } catch { MelonLogger.Error("Failed to load image from asset bundle"); }
            }
            else MelonLogger.Error("Bundle was null");
        }

        private static Texture2D LoadTexture(string Texture)
        { // https://github.com/KortyBoi/VRChat-TeleporterVR/blob/59bdfb200497db665621b519a9ff9c3d1c3f2bc8/Utils/ResourceManager.cs#L32
            Texture2D Texture2 = assetBundleIcons.LoadAsset_Internal(Texture, Il2CppType.Of<Texture2D>()).Cast<Texture2D>();
            Texture2.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            //Texture2.hideFlags = HideFlags.HideAndDontSave;
            return Texture2;
        }

        private static void MirrorMenu(string MirrorType)
        {
            string MirrorScaleType = "MirrorScale" + (MirrorType == "PortableMirrorCeiling" ? "Z" : "Y");
            float Scale = (MirrorType == "PortableMirrorMicro" ? .01f : .25f);

            CustomSubMenu.AddSubMenu("Mirror Type", () =>
            {
                CustomSubMenu.AddButton("Full", () =>
                {
                    MelonPreferences.SetEntryValue<string>(MirrorType, "MirrorState", "MirrorFull");
                    Main main = new Main(); main.OnPreferencesSaved();
                }, MirrorFull);
                CustomSubMenu.AddButton("Optimized", () =>
                {
                    MelonPreferences.SetEntryValue<string>(MirrorType, "MirrorState", "MirrorOpt");
                    Main main = new Main(); main.OnPreferencesSaved();
                }, MirrorOpt);
                CustomSubMenu.AddButton("Cutout", () =>
                {
                    MelonPreferences.SetEntryValue<string>(MirrorType, "MirrorState", "MirrorCutout");
                    Main main = new Main(); main.OnPreferencesSaved();
                }, MirrorCut);
                CustomSubMenu.AddButton("Transparent", () =>
                {
                    MelonPreferences.SetEntryValue<string>(MirrorType, "MirrorState", "MirrorTransparent");
                    Main main = new Main(); main.OnPreferencesSaved();
                }, MirrorTrans);
            }, MirrorOptions);

            CustomSubMenu.AddToggle("Can Pickup", MelonPreferences.GetEntryValue<bool>(MirrorType, "CanPickupMirror"), (action) =>
            {
                MelonPreferences.SetEntryValue<bool>(MirrorType, "CanPickupMirror", !MelonPreferences.GetEntryValue<bool>(MirrorType, "CanPickupMirror"));
                Main main = new Main(); main.OnPreferencesSaved();
                AMUtils.RefreshActionMenu();
            }, Grab);

            CustomSubMenu.AddSubMenu("Location & Size", () =>
            {
                if (MirrorType != "PortableMirrorMicro")
                {
                    CustomSubMenu.AddButton($"Distance +\n{MelonPreferences.GetEntryValue<float>(MirrorType, "MirrorDistance").ToString("F2").TrimEnd('0')}", () =>
                    {
                        MelonPreferences.SetEntryValue<float>(MirrorType, "MirrorDistance", MelonPreferences.GetEntryValue<float>(MirrorType, "MirrorDistance") + Main._mirrorDistAdj);
                        Main main = new Main(); main.OnPreferencesSaved();
                        AMUtils.RefreshActionMenu();
                    }, DistPlus);
                    CustomSubMenu.AddButton($"Distance -\n{MelonPreferences.GetEntryValue<float>(MirrorType, "MirrorDistance").ToString("F2").TrimEnd('0')}", () =>
                    {
                        MelonPreferences.SetEntryValue<float>(MirrorType, "MirrorDistance", MelonPreferences.GetEntryValue<float>(MirrorType, "MirrorDistance") - Main._mirrorDistAdj);
                        Main main = new Main(); main.OnPreferencesSaved();
                        AMUtils.RefreshActionMenu();
                    }, DistMinus);
                }
                CustomSubMenu.AddButton("Larger", () =>
                {
                    MelonPreferences.SetEntryValue<float>(MirrorType, "MirrorScaleX", MelonPreferences.GetEntryValue<float>(MirrorType, "MirrorScaleX") + Scale);
                    MelonPreferences.SetEntryValue<float>(MirrorType, MirrorScaleType, MelonPreferences.GetEntryValue<float>(MirrorType, MirrorScaleType) + Scale);
                    Main main = new Main(); main.OnPreferencesSaved();
                    AMUtils.RefreshActionMenu();
                }, SizePlus);
                CustomSubMenu.AddButton("Smaller", () =>
                {
                    if ((MelonPreferences.GetEntryValue<float>(MirrorType, "MirrorScaleX") > Scale * (MirrorType == "PortableMirrorMicro" ? 2f : 1f)) && MelonPreferences.GetEntryValue<float>(MirrorType, MirrorScaleType) > Scale * (MirrorType == "PortableMirrorMicro" ? 2f : 1f))
                    {
                        MelonPreferences.SetEntryValue<float>(MirrorType, "MirrorScaleX", MelonPreferences.GetEntryValue<float>(MirrorType, "MirrorScaleX") - Scale);
                        MelonPreferences.SetEntryValue<float>(MirrorType, MirrorScaleType, MelonPreferences.GetEntryValue<float>(MirrorType, MirrorScaleType) - Scale);
                        Main main = new Main(); main.OnPreferencesSaved();
                        AMUtils.RefreshActionMenu();
                    }
                }, SizeMinus);
            }, MirrorRuler);
        }

        public static void InitUi()
        {
            loadAssets();
            ///Main menu, List all mirrors
            ///Sub menu, Toggle,Opt,Pickup,Move+,Move-,Size+,Size-

            VRCActionMenuPage.AddSubMenu(ActionMenuPage.Main, "<color=#ff00ff>Portable Mirror</color>", () =>
            {

                CustomSubMenu.AddSubMenu("Portable Mirror", () =>
                {
                    CustomSubMenu.AddToggle("Enable", (Main._mirrorBase != null), (action) =>
                    {
                        if (Utils.GetVRCPlayer() != null) Main.ToggleMirror();
                        AMUtils.RefreshActionMenu();
                    }, MirrorBase);

                    MirrorMenu("PortableMirror");

                }, MirrorBase);

                CustomSubMenu.AddSubMenu("45 Mirror", () =>
                {
                    CustomSubMenu.AddToggle("Enable", (Main._mirror45 != null), (action) =>
                    {
                        if (Utils.GetVRCPlayer() != null) Main.ToggleMirror45();
                        AMUtils.RefreshActionMenu();
                    }, Mirror45);

                    MirrorMenu("PortableMirror45");

                }, Mirror45);

                CustomSubMenu.AddSubMenu("Ceiling Mirror", () =>
                {
                    CustomSubMenu.AddToggle("Enable", (Main._mirrorCeiling != null), (action) =>
                    {
                        if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorCeiling();
                        AMUtils.RefreshActionMenu();
                    }, MirrorCeil);

                    MirrorMenu("PortableMirrorCeiling");

                }, MirrorCeil);

                CustomSubMenu.AddSubMenu("Micro Mirror", () =>
                {
                    CustomSubMenu.AddToggle("Enable", (Main._mirrorMicro != null), (action) =>
                    {
                        if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorMicro();
                        AMUtils.RefreshActionMenu();
                    }, MirrorMicro);

                    MirrorMenu("PortableMirrorMicro");

                }, MirrorMicro);

                CustomSubMenu.AddSubMenu("Transparent Mirror", () =>
                {
                    CustomSubMenu.AddToggle("Enable", (Main._mirrorTrans != null), (action) =>
                    {
                        if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorTrans();
                        AMUtils.RefreshActionMenu();
                    }, MirrorTrans);

                    MirrorMenu("PortableMirrorTrans");

                }, MirrorTrans);



                CustomSubMenu.AddSubMenu("Extras", () =>
                {
                    CustomSubMenu.AddButton($"Transparency:\n{MelonPreferences.GetEntryValue<float>("PortableMirror", "TransMirrorTrans")}", () =>
                    {
                        MelonPreferences.SetEntryValue<float>("PortableMirror", "TransMirrorTrans", MelonPreferences.GetEntryValue<float>("PortableMirror", "TransMirrorTrans") + .1f);
                        Main main = new Main(); main.OnPreferencesSaved();
                        AMUtils.RefreshActionMenu();
                    }, TransPlus);
                    CustomSubMenu.AddButton($"Transparency:\n{MelonPreferences.GetEntryValue<float>("PortableMirror", "TransMirrorTrans")}", () =>
                    {
                        MelonPreferences.SetEntryValue<float>("PortableMirror", "TransMirrorTrans", MelonPreferences.GetEntryValue<float>("PortableMirror", "TransMirrorTrans") - .1f);
                        Main main = new Main(); main.OnPreferencesSaved();
                        AMUtils.RefreshActionMenu();
                    }, TransMinus);
                    CustomSubMenu.AddToggle("Mirrors Show In Camera", MelonPreferences.GetEntryValue<bool>("PortableMirror", "MirrorsShowInCamera"), (action) =>
                    {
                        MelonPreferences.SetEntryValue<bool>("PortableMirror", "MirrorsShowInCamera", !MelonPreferences.GetEntryValue<bool>("PortableMirror", "MirrorsShowInCamera"));
                        Main main = new Main(); main.OnPreferencesSaved();
                        AMUtils.RefreshActionMenu();
                    }, CameraMirror);
                    CustomSubMenu.AddToggle("High Precision Adjust", Main._mirrorDistHighPrec, (action) =>
                    {
                        Main._mirrorDistHighPrec = !Main._mirrorDistHighPrec;
                        Main main = new Main(); main.OnPreferencesSaved();
                        AMUtils.RefreshActionMenu();
                    }, DistAdjIcon);

                    CustomSubMenu.AddButton($"ColliderDepth:\n{MelonPreferences.GetEntryValue<float>("PortableMirror", "ColliderDepth").ToString("F2").TrimEnd('0')}", () =>
                    {
                        MelonPreferences.SetEntryValue<float>("PortableMirror", "ColliderDepth", MelonPreferences.GetEntryValue<float>("PortableMirror", "ColliderDepth") + .1f);
                        Main main = new Main(); main.OnPreferencesSaved();
                        AMUtils.RefreshActionMenu();
                    }, GrabDistPlus);
                    CustomSubMenu.AddButton($"ColliderDepth:\n{MelonPreferences.GetEntryValue<float>("PortableMirror", "ColliderDepth").ToString("F2").TrimEnd('0')}", () =>
                    {
                        if (MelonPreferences.GetEntryValue<float>("PortableMirror", "ColliderDepth") > .1f)
                        {
                            MelonPreferences.SetEntryValue<float>("PortableMirror", "ColliderDepth", MelonPreferences.GetEntryValue<float>("PortableMirror", "ColliderDepth") - .1f);
                            Main main = new Main(); main.OnPreferencesSaved();
                            AMUtils.RefreshActionMenu();
                        }
                    }, GrabDistMinus);

                    CustomSubMenu.AddToggle("Pickups snap to hand", MelonPreferences.GetEntryValue<bool>("PortableMirror", "PickupToHand"), (action) =>
                    {
                        MelonPreferences.SetEntryValue<bool>("PortableMirror", "PickupToHand", !MelonPreferences.GetEntryValue<bool>("PortableMirror", "PickupToHand"));
                        Main main = new Main(); main.OnPreferencesSaved();
                        AMUtils.RefreshActionMenu();
                    } );

                    CustomSubMenu.AddToggle("Auto Hold Pickups", MelonPreferences.GetEntryValue<bool>("PortableMirror", "AutoHold"), (action) =>
                    {
                        MelonPreferences.SetEntryValue<bool>("PortableMirror", "AutoHold", !MelonPreferences.GetEntryValue<bool>("PortableMirror", "AutoHold"));
                        Main main = new Main(); main.OnPreferencesSaved();
                        AMUtils.RefreshActionMenu();
                    });
                }, SettingsGear);


            }, MirrorOpt);
        }
    }
}
