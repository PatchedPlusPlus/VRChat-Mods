using System.Reflection;
using MelonLoader;
using UnityEngine;
using UnhollowerRuntimeLib;
using System.IO;
using ActionMenuApi.Api;

namespace PortableMirror
{
    //I could do this better and have less duplicate code, but copy pasting is so much faster...
    public class CustomActionMenu
    {
        public static AssetBundle assetBundleIcons;
        public static Texture2D DistMinus, DistPlus, SizeMinus, SizePlus, Grab, MirrorBase, Mirror45, MirrorCeil, MirrorCut, MirrorFull, MirrorMicro, MirrorOpt, MirrorTrans, MirrorRuler, MirrorOptions, SettingsGear, TransPlus, TransMinus, CameraMirror;

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

                    CustomSubMenu.AddSubMenu("Mirror Type", () =>
                    {
                        CustomSubMenu.AddButton("Full", () =>
                        {
                            MelonPreferences.SetEntryValue<string>("PortableMirror", "MirrorState", "MirrorFull");
                            Main main = new Main(); main.OnPreferencesSaved();
                        }, MirrorFull);
                        CustomSubMenu.AddButton("Optimzied", () =>
                        {
                            MelonPreferences.SetEntryValue<string>("PortableMirror", "MirrorState", "MirrorOpt");
                            Main main = new Main(); main.OnPreferencesSaved();
                        }, MirrorOpt);
                        CustomSubMenu.AddButton("Cutout", () =>
                        {
                            MelonPreferences.SetEntryValue<string>("PortableMirror", "MirrorState", "MirrorCutout");
                            Main main = new Main(); main.OnPreferencesSaved();
                        }, MirrorCut);
                        CustomSubMenu.AddButton("Transparent", () =>
                        {
                            MelonPreferences.SetEntryValue<string>("PortableMirror", "MirrorState", "MirrorTransparent");
                            Main main = new Main(); main.OnPreferencesSaved();
                        }, MirrorTrans);
                    }, MirrorOptions);

                    CustomSubMenu.AddToggle("Can Pickup", MelonPreferences.GetEntryValue<bool>("PortableMirror", "CanPickupMirror"), (action) =>
                    {
                        MelonPreferences.SetEntryValue<bool>("PortableMirror", "CanPickupMirror", !MelonPreferences.GetEntryValue<bool>("PortableMirror", "CanPickupMirror"));
                        Main main = new Main(); main.OnPreferencesSaved();
                        AMUtils.RefreshActionMenu();
                    }, Grab);

                    CustomSubMenu.AddSubMenu("Location & Size", () =>
                    {
                        CustomSubMenu.AddButton($"Distance +\n{MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorDistance")}", () =>
                        {
                            MelonPreferences.SetEntryValue<float>("PortableMirror", "MirrorDistance", MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorDistance") + .25f);
                            Main main = new Main(); main.OnPreferencesSaved();
                            AMUtils.RefreshActionMenu();
                        }, DistPlus);
                        CustomSubMenu.AddButton($"Distance -\n{MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorDistance")}", () =>
                        {
                            MelonPreferences.SetEntryValue<float>("PortableMirror", "MirrorDistance", MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorDistance") - .25f);
                            Main main = new Main(); main.OnPreferencesSaved();
                            AMUtils.RefreshActionMenu();
                        }, DistMinus);
                        CustomSubMenu.AddButton("Larger", () =>
                        {
                            MelonPreferences.SetEntryValue<float>("PortableMirror", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorScaleX") + .25f);
                            MelonPreferences.SetEntryValue<float>("PortableMirror", "MirrorScaleY", MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorScaleY") + .25f);
                            Main main = new Main(); main.OnPreferencesSaved();
                            AMUtils.RefreshActionMenu();
                        }, SizePlus);
                        CustomSubMenu.AddButton("Smaller", () =>
                        {
                            if (MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorScaleX") > .25 && MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorScaleY") > .25)
                            {
                                MelonPreferences.SetEntryValue<float>("PortableMirror", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorScaleX") - .25f);
                                MelonPreferences.SetEntryValue<float>("PortableMirror", "MirrorScaleY", MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorScaleY") - .25f);
                                Main main = new Main(); main.OnPreferencesSaved();
                                AMUtils.RefreshActionMenu();
                            }
                        }, SizeMinus);

                    }, MirrorRuler);

                }, MirrorBase);

                CustomSubMenu.AddSubMenu("45 Mirror", () =>
                {
                    CustomSubMenu.AddToggle("Enable", (Main._mirror45 != null), (action) =>
                    {
                        if (Utils.GetVRCPlayer() != null) Main.ToggleMirror45();
                        AMUtils.RefreshActionMenu();
                    }, MirrorBase);

                    CustomSubMenu.AddSubMenu("Mirror Type", () =>
                    {
                        CustomSubMenu.AddButton("Full", () =>
                        {
                            MelonPreferences.SetEntryValue<string>("PortableMirror45", "MirrorState", "MirrorFull");
                            Main main = new Main(); main.OnPreferencesSaved();
                        }, MirrorFull);
                        CustomSubMenu.AddButton("Optimzied", () =>
                        {
                            MelonPreferences.SetEntryValue<string>("PortableMirror45", "MirrorState", "MirrorOpt");
                            Main main = new Main(); main.OnPreferencesSaved();
                        }, MirrorOpt);
                        CustomSubMenu.AddButton("Cutout", () =>
                        {
                            MelonPreferences.SetEntryValue<string>("PortableMirror45", "MirrorState", "MirrorCutout");
                            Main main = new Main(); main.OnPreferencesSaved();
                        }, MirrorCut);
                        CustomSubMenu.AddButton("Transparent", () =>
                        {
                            MelonPreferences.SetEntryValue<string>("PortableMirror45", "MirrorState", "MirrorTransparent");
                            Main main = new Main(); main.OnPreferencesSaved();
                        }, MirrorTrans);
                    }, MirrorOptions);

                    CustomSubMenu.AddToggle("Can Pickup", MelonPreferences.GetEntryValue<bool>("PortableMirror45", "CanPickup45Mirror"), (action) =>
                    {
                        MelonPreferences.SetEntryValue<bool>("PortableMirror45", "CanPickup45Mirror", !MelonPreferences.GetEntryValue<bool>("PortableMirror45", "CanPickup45Mirror"));
                        Main main = new Main(); main.OnPreferencesSaved();
                        AMUtils.RefreshActionMenu();
                    }, Grab);

                    CustomSubMenu.AddSubMenu("Location & Size", () =>
                    {
                        CustomSubMenu.AddButton($"Distance +\n{MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorDistance")}", () =>
                        {
                            MelonPreferences.SetEntryValue<float>("PortableMirror45", "MirrorDistance", MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorDistance") + .25f);
                            Main main = new Main(); main.OnPreferencesSaved();
                            AMUtils.RefreshActionMenu();
                        }, DistPlus);
                        CustomSubMenu.AddButton($"Distance -\n{MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorDistance")}", () =>
                        {
                            MelonPreferences.SetEntryValue<float>("PortableMirror45", "MirrorDistance", MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorDistance") - .25f);
                            Main main = new Main(); main.OnPreferencesSaved();
                            AMUtils.RefreshActionMenu();
                        }, DistMinus);
                        CustomSubMenu.AddButton("Larger", () =>
                        {
                            MelonPreferences.SetEntryValue<float>("PortableMirror45", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorScaleX") + .25f);
                            MelonPreferences.SetEntryValue<float>("PortableMirror45", "MirrorScaleY", MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorScaleY") + .25f);
                            Main main = new Main(); main.OnPreferencesSaved();
                            AMUtils.RefreshActionMenu();
                        }, SizePlus);
                        CustomSubMenu.AddButton("Smaller", () =>
                        {
                            if (MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorScaleX") > .25 && MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorScaleY") > .25)
                            {
                                MelonPreferences.SetEntryValue<float>("PortableMirror45", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorScaleX") - .25f);
                                MelonPreferences.SetEntryValue<float>("PortableMirror45", "MirrorScaleY", MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorScaleY") - .25f);
                                Main main = new Main(); main.OnPreferencesSaved();
                                AMUtils.RefreshActionMenu();
                            }
                        }, SizeMinus);

                    }, MirrorRuler);
                }, Mirror45);

                CustomSubMenu.AddSubMenu("Ceiling Mirror", () =>
                {
                    CustomSubMenu.AddToggle("Enable", (Main._mirrorCeiling != null), (action) =>
                    {
                        if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorCeiling();
                        AMUtils.RefreshActionMenu();
                    }, MirrorBase);

                    CustomSubMenu.AddSubMenu("Mirror Type", () =>
                    {
                        CustomSubMenu.AddButton("Full", () =>
                        {
                            MelonPreferences.SetEntryValue<string>("PortableMirrorCeiling", "MirrorState", "MirrorFull");
                            Main main = new Main(); main.OnPreferencesSaved();
                        }, MirrorFull);
                        CustomSubMenu.AddButton("Optimzied", () =>
                        {
                            MelonPreferences.SetEntryValue<string>("PortableMirrorCeiling", "MirrorState", "MirrorOpt");
                            Main main = new Main(); main.OnPreferencesSaved();
                        }, MirrorOpt);
                        CustomSubMenu.AddButton("Cutout", () =>
                        {
                            MelonPreferences.SetEntryValue<string>("PortableMirrorCeiling", "MirrorState", "MirrorCutout");
                            Main main = new Main(); main.OnPreferencesSaved();
                        }, MirrorCut);
                        CustomSubMenu.AddButton("Transparent", () =>
                        {
                            MelonPreferences.SetEntryValue<string>("PortableMirrorCeiling", "MirrorState", "MirrorTransparent");
                            Main main = new Main(); main.OnPreferencesSaved();
                        }, MirrorTrans);
                    }, MirrorOptions);

                    CustomSubMenu.AddToggle("Can Pickup", MelonPreferences.GetEntryValue<bool>("PortableMirrorCeiling", "CanPickupCeilingMirror"), (action) =>
                    {
                        MelonPreferences.SetEntryValue<bool>("PortableMirrorCeiling", "CanPickupCeilingMirror", !MelonPreferences.GetEntryValue<bool>("PortableMirrorCeiling", "CanPickupCeilingMirror"));
                        Main main = new Main(); main.OnPreferencesSaved();
                        AMUtils.RefreshActionMenu();
                    }, Grab);

                    CustomSubMenu.AddSubMenu("Location & Size", () =>
                    {
                        CustomSubMenu.AddButton($"Distance +\n{MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorDistance")}", () =>
                        {
                            MelonPreferences.SetEntryValue<float>("PortableMirrorCeiling", "MirrorDistance", MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorDistance") + .25f);
                            Main main = new Main(); main.OnPreferencesSaved();
                            AMUtils.RefreshActionMenu();
                        }, DistPlus);
                        CustomSubMenu.AddButton($"Distance -\n{MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorDistance")}", () =>
                        {
                            MelonPreferences.SetEntryValue<float>("PortableMirrorCeiling", "MirrorDistance", MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorDistance") - .25f);
                            Main main = new Main(); main.OnPreferencesSaved();
                            AMUtils.RefreshActionMenu();
                        }, DistMinus);
                        CustomSubMenu.AddButton("Larger", () =>
                        {
                            MelonPreferences.SetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleX") + .25f);
                            MelonPreferences.SetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleZ", MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleZ") + .25f);
                            Main main = new Main(); main.OnPreferencesSaved();
                            AMUtils.RefreshActionMenu();
                        }, SizePlus);
                        CustomSubMenu.AddButton("Smaller", () =>
                        {
                            if (MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleX") > .25 && MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleZ") > .25)
                            {
                                MelonPreferences.SetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleX") - .25f);
                                MelonPreferences.SetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleZ", MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleZ") - .25f);
                                Main main = new Main(); main.OnPreferencesSaved();
                                AMUtils.RefreshActionMenu();
                            }
                        }, SizeMinus);

                    }, MirrorRuler);
                }, MirrorCeil);

                CustomSubMenu.AddSubMenu("Micro Mirror", () =>
                {
                    CustomSubMenu.AddToggle("Enable", (Main._mirrorMicro != null), (action) =>
                    {
                        if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorMicro();
                        AMUtils.RefreshActionMenu();
                    }, MirrorBase);

                    CustomSubMenu.AddSubMenu("Mirror Type", () =>
                    {
                        CustomSubMenu.AddButton("Full", () =>
                        {
                            MelonPreferences.SetEntryValue<string>("PortableMirrorMicro", "MirrorState", "MirrorFull");
                            Main main = new Main(); main.OnPreferencesSaved();
                        }, MirrorFull);
                        CustomSubMenu.AddButton("Optimzied", () =>
                        {
                            MelonPreferences.SetEntryValue<string>("PortableMirrorMicro", "MirrorState", "MirrorOpt");
                            Main main = new Main(); main.OnPreferencesSaved();
                        }, MirrorOpt);
                        CustomSubMenu.AddButton("Cutout", () =>
                        {
                            MelonPreferences.SetEntryValue<string>("PortableMirrorMicro", "MirrorState", "MirrorCutout");
                            Main main = new Main(); main.OnPreferencesSaved();
                        }, MirrorCut);
                        CustomSubMenu.AddButton("Transparent", () =>
                        {
                            MelonPreferences.SetEntryValue<string>("PortableMirrorMicro", "MirrorState", "MirrorTransparent");
                            Main main = new Main(); main.OnPreferencesSaved();
                        }, MirrorTrans);
                    }, MirrorOptions);

                    CustomSubMenu.AddToggle("Can Pickup", MelonPreferences.GetEntryValue<bool>("PortableMirrorMicro", "CanPickupMirrorMicro"), (action) =>
                    {
                        MelonPreferences.SetEntryValue<bool>("PortableMirrorMicro", "CanPickupMirrorMicro", !MelonPreferences.GetEntryValue<bool>("PortableMirrorMicro", "CanPickupMirrorMicro"));
                        Main main = new Main(); main.OnPreferencesSaved();
                        AMUtils.RefreshActionMenu();
                    }, Grab);

                    CustomSubMenu.AddSubMenu("Location & Size", () =>
                    {

                        CustomSubMenu.AddButton("Larger", () =>
                        {
                            MelonPreferences.SetEntryValue<float>("PortableMirrorMicro", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirrorMicro", "MirrorScaleX") + .01f);
                            MelonPreferences.SetEntryValue<float>("PortableMirrorMicro", "MirrorScaleY", MelonPreferences.GetEntryValue<float>("PortableMirrorMicro", "MirrorScaleY") + .01f);
                            Main main = new Main(); main.OnPreferencesSaved();
                            AMUtils.RefreshActionMenu();
                        }, SizePlus);
                        CustomSubMenu.AddButton("Smaller", () =>
                        {
                            if (MelonPreferences.GetEntryValue<float>("PortableMirrorMicro", "MirrorScaleX") > .02 && MelonPreferences.GetEntryValue<float>("PortableMirrorMicro", "MirrorScaleY") > .02)
                            {
                                MelonPreferences.SetEntryValue<float>("PortableMirrorMicro", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirrorMicro", "MirrorScaleX") - .01f);
                                MelonPreferences.SetEntryValue<float>("PortableMirrorMicro", "MirrorScaleY", MelonPreferences.GetEntryValue<float>("PortableMirrorMicro", "MirrorScaleY") - .01f);
                                Main main = new Main(); main.OnPreferencesSaved();
                                AMUtils.RefreshActionMenu();
                            }
                        }, SizeMinus);

                    }, MirrorRuler);
                }, MirrorMicro);

                CustomSubMenu.AddSubMenu("Transparent Mirror", () =>
                {
                    CustomSubMenu.AddToggle("Enable", (Main._mirrorTrans != null), (action) =>
                    {
                        if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorTrans();
                        AMUtils.RefreshActionMenu();
                    }, MirrorBase);

                    CustomSubMenu.AddSubMenu("Mirror Type", () =>
                    {
                        CustomSubMenu.AddButton("Full", () =>
                        {
                            MelonPreferences.SetEntryValue<string>("PortableMirrorTrans", "MirrorState", "MirrorFull");
                            Main main = new Main(); main.OnPreferencesSaved();
                        }, MirrorFull);
                        CustomSubMenu.AddButton("Optimzied", () =>
                        {
                            MelonPreferences.SetEntryValue<string>("PortableMirrorTrans", "MirrorState", "MirrorOpt");
                            Main main = new Main(); main.OnPreferencesSaved();
                        }, MirrorOpt);
                        CustomSubMenu.AddButton("Cutout", () =>
                        {
                            MelonPreferences.SetEntryValue<string>("PortableMirrorTrans", "MirrorState", "MirrorCutout");
                            Main main = new Main(); main.OnPreferencesSaved();
                        }, MirrorCut);
                        CustomSubMenu.AddButton("Transparent", () =>
                        {
                            MelonPreferences.SetEntryValue<string>("PortableMirrorTrans", "MirrorState", "MirrorTransparent");
                            Main main = new Main(); main.OnPreferencesSaved();
                        }, MirrorTrans);
                    }, MirrorOptions);

                    CustomSubMenu.AddToggle("Can Pickup", MelonPreferences.GetEntryValue<bool>("PortableMirrorTrans", "CanPickupMirror"), (action) =>
                    {
                        MelonPreferences.SetEntryValue<bool>("PortableMirrorTrans", "CanPickupMirror", !MelonPreferences.GetEntryValue<bool>("PortableMirrorTrans", "CanPickupMirror"));
                        Main main = new Main(); main.OnPreferencesSaved();
                        AMUtils.RefreshActionMenu();
                    }, Grab);

                    CustomSubMenu.AddSubMenu("Location & Size", () =>
                    {
                        CustomSubMenu.AddButton($"Distance +\n{MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorDistance")}", () =>
                        {
                            MelonPreferences.SetEntryValue<float>("PortableMirrorTrans", "MirrorDistance", MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorDistance") + .25f);
                            Main main = new Main(); main.OnPreferencesSaved();
                            AMUtils.RefreshActionMenu();
                        }, DistPlus);
                        CustomSubMenu.AddButton($"Distance -\n{MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorDistance")}", () =>
                        {
                            MelonPreferences.SetEntryValue<float>("PortableMirrorTrans", "MirrorDistance", MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorDistance") - .25f);
                            Main main = new Main(); main.OnPreferencesSaved();
                            AMUtils.RefreshActionMenu();
                        }, DistMinus);
                        CustomSubMenu.AddButton("Larger", () =>
                        {
                            MelonPreferences.SetEntryValue<float>("PortableMirrorTrans", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorScaleX") + .25f);
                            MelonPreferences.SetEntryValue<float>("PortableMirrorTrans", "MirrorScaleY", MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorScaleY") + .25f);
                            Main main = new Main(); main.OnPreferencesSaved();
                            AMUtils.RefreshActionMenu();
                        }, SizePlus);
                        CustomSubMenu.AddButton("Smaller", () =>
                        {
                            if (MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorScaleX") > .25 && MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorScaleY") > .25)
                            {
                                MelonPreferences.SetEntryValue<float>("PortableMirrorTrans", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorScaleX") - .25f);
                                MelonPreferences.SetEntryValue<float>("PortableMirrorTrans", "MirrorScaleY", MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorScaleY") - .25f);
                                Main main = new Main(); main.OnPreferencesSaved();
                                AMUtils.RefreshActionMenu();
                            }
                        }, SizeMinus);

                    }, MirrorRuler);
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

                }, SettingsGear);



            }, MirrorBase);
        }
    }
}
