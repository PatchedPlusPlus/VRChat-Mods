using System.Reflection;
using MelonLoader;
using UnityEngine;
using UnhollowerRuntimeLib;
using System.IO;
using ActionMenuApi.Api;
using System;


namespace PortableMirror
{
    //I could do this better and have less duplicate code, but copy pasting is so much faster... // I gotcha mate! - Davi
    public class CustomActionMenu
    {
        public static AssetBundle assetBundleIcons;
        public static Texture2D DistMinus, DistPlus, SizeMinus, SizePlus, Grab, MirrorBase, Mirror45, MirrorCeil, MirrorCut, MirrorFull, MirrorMicro, MirrorOpt, MirrorTrans, MirrorRuler, MirrorOptions, SettingsGear, TransPlus, TransMinus, CameraMirror, DistAdjIcon, GrabDistPlus, GrabDistMinus, MirrorCutSolo, MirrorTransSolo, SnapToHand, PosfromView, FollowsYou;

        private static void loadAssets()
        {
            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PortableMirrorMod.mirroricons"))
            {
                try
                {
                    using (var tempStream = new MemoryStream((int)assetStream.Length))
                    {
                        assetStream.CopyTo(tempStream);
                        assetBundleIcons = AssetBundle.LoadFromMemory_Internal(tempStream.ToArray(), 0);
                        assetBundleIcons.hideFlags |= HideFlags.DontUnloadUnusedAsset;
                    }
                }
                catch (Exception ex)
                {
                    MelonLogger.Error(ex);
                }
            }

            if (assetBundleIcons != null)
            {
                try { DistMinus = LoadTexture("icons8-distance-128-Distance-Minus.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { DistPlus = LoadTexture("icons8-distance-128-Distance-Plus.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { SizeMinus = LoadTexture("icons8-distance-128-Size-Minus.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { SizePlus = LoadTexture("icons8-distance-128-Size-Plus.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { Grab = LoadTexture("icons8-grab-100-edit.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { MirrorBase = LoadTexture("icons8-rectangular-mirror-128-BW-Edit.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { Mirror45 = LoadTexture("icons8-rectangular-mirror-128-BW-Edit-45.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { MirrorCeil = LoadTexture("icons8-rectangular-mirror-128-BW-Edit-Ceiling.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { MirrorCut = LoadTexture("icons8-rectangular-mirror-128-BW-Edit-Cut.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { MirrorFull = LoadTexture("icons8-rectangular-mirror-128-BW-Edit-Full.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { MirrorMicro = LoadTexture("icons8-rectangular-mirror-128-BW-Edit-Micro.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { MirrorOpt = LoadTexture("icons8-rectangular-mirror-128-BW-Edit-Opt.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { MirrorTrans = LoadTexture("icons8-rectangular-mirror-128-BW-Edit-Trans.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { MirrorRuler = LoadTexture("icons8-ruler-128-edit.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { MirrorOptions = LoadTexture("icons8-rectangular-mirror-128-BW-Edit-Options.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { SettingsGear = LoadTexture("icons8-automation-100-edit.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { TransPlus = LoadTexture("icons8-rectangular-mirror-128-BW-Edit-TransPlus.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { TransMinus = LoadTexture("icons8-rectangular-mirror-128-BW-Edit-TransMinus.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { CameraMirror = LoadTexture("icons8-rectangular-mirror-128-BW-Camera.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { DistAdjIcon = LoadTexture("icons8-distance-128-Distance-Adj.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { GrabDistPlus = LoadTexture("icons8-grab-100-edit-Distance_Plus.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { GrabDistMinus = LoadTexture("icons8-grab-100-edit-Distance_Minus.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { MirrorCutSolo = LoadTexture("icons8-rectangular-mirror-128-BW-Edit-CutSolo.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { MirrorTransSolo = LoadTexture("icons8-rectangular-mirror-128-BW-Edit-TransSolo.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { SnapToHand = LoadTexture("icons8-grab-100-edit-SnapToHand.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { PosfromView = LoadTexture("icons8-rectangular-mirror-128-BW-Edit-PosfromView.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
                try { FollowsYou = LoadTexture("icons8-rectangular-mirror-128-BW-Edit-FollowsYou.png"); } catch { Main.Logger.Error("Failed to load image from asset bundle"); }
            }
            else Main.Logger.Error("Bundle was null");
        }

        private static Texture2D LoadTexture(string Texture)
        { // https://github.com/KortyBoi/VRChat-TeleporterVR/blob/59bdfb200497db665621b519a9ff9c3d1c3f2bc8/Utils/ResourceManager.cs#L32
            Texture2D Texture2 = assetBundleIcons.LoadAsset_Internal(Texture, Il2CppType.Of<Texture2D>()).Cast<Texture2D>();
            Texture2.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            //Texture2.hideFlags = HideFlags.HideAndDontSave;
            return Texture2;
        }

        private static void MirrorMenu(string MirrorType, MelonPreferences_Entry<string> prefState, MelonPreferences_Entry<bool> prefPickup, MelonPreferences_Entry<float> prefDist,
            MelonPreferences_Entry<float> prefX, MelonPreferences_Entry<float> prefY, MelonPreferences_Entry<bool> prefPos, MelonPreferences_Entry<bool> prefTracking)
        {
            string MirrorScaleType = "MirrorScale" + (MirrorType == "PortableMirrorCeiling" ? "Z" : "Y");
            float Scale = (MirrorType == "PortableMirrorMicro" ? .01f : .25f);

            CustomSubMenu.AddSubMenu("Mirror Type", () =>
            {
                CustomSubMenu.AddButton("Full", () =>
                { 
                    prefState.Value = "MirrorFull";
                    Main main = new Main(); main.OnPreferencesSaved();
                }, MirrorFull);
                CustomSubMenu.AddButton("Optimized", () =>
                {
                    prefState.Value = "MirrorOpt";
                    Main main = new Main(); main.OnPreferencesSaved();
                }, MirrorOpt);
                CustomSubMenu.AddButton("Cutout", () =>
                {
                    prefState.Value = "MirrorCutout";
                    Main main = new Main(); main.OnPreferencesSaved();
                }, MirrorCut);
                CustomSubMenu.AddButton("Transparent", () =>
                {
                    prefState.Value = "MirrorTransparent";
                    Main main = new Main(); main.OnPreferencesSaved();
                }, MirrorTrans);
                CustomSubMenu.AddButton("Cutout Solo", () =>
                {
                    prefState.Value = "MirrorCutoutSolo";
                    Main main = new Main(); main.OnPreferencesSaved();
                }, MirrorCutSolo);
                CustomSubMenu.AddButton("Transparent Solo", () =>
                {
                    prefState.Value = "MirrorTransparentSolo";
                    Main main = new Main(); main.OnPreferencesSaved();
                }, MirrorTransSolo);
            }, MirrorOptions);

            
            CustomSubMenu.AddToggle("Can Pickup", prefPickup.Value, (action) =>
            {
                prefPickup.Value = !prefPickup.Value;
                Main main = new Main(); main.OnPreferencesSaved();
                AMUtils.RefreshActionMenu();
            }, Grab);

            CustomSubMenu.AddSubMenu("Location & Size", () =>
            {
                if (MirrorType != "PortableMirrorMicro")
                {
                    CustomSubMenu.AddButton($"Distance +\n{prefDist.Value.ToString("F2").TrimEnd('0')}", () =>
                    {
                        prefDist.Value += Main._mirrorDistAdj;
                        Main main = new Main(); main.OnPreferencesSaved();
                        AMUtils.RefreshActionMenu();
                    }, DistPlus);
                    CustomSubMenu.AddButton($"Distance -\n{prefDist.Value.ToString("F2").TrimEnd('0')}", () =>
                    {
                        prefDist.Value -= Main._mirrorDistAdj;
                        Main main = new Main(); main.OnPreferencesSaved();
                        AMUtils.RefreshActionMenu();
                    }, DistMinus);
                }
                CustomSubMenu.AddButton("Larger", () =>
                {
                    prefX.Value += Scale;
                    prefY.Value += Scale;
                    Main main = new Main(); main.OnPreferencesSaved();
                    AMUtils.RefreshActionMenu();
                }, SizePlus);
                CustomSubMenu.AddButton("Smaller", () =>
                {
                    if ((prefX.Value > Scale * (MirrorType == "PortableMirrorMicro" ? 2f : 1f)) && prefY.Value > Scale * (MirrorType == "PortableMirrorMicro" ? 2f : 1f))
                    {
                        prefX.Value -= Scale;
                        prefY.Value -= Scale;
                        Main main = new Main(); main.OnPreferencesSaved();
                        AMUtils.RefreshActionMenu();
                    }
                }, SizeMinus);
            }, MirrorRuler);

            if (MirrorType == "PortableMirror" || MirrorType == "PortableMirrorTrans" || MirrorType == "PortableMirrorMicro")
            {
                CustomSubMenu.AddToggle("Pos&Rotation from View", prefPos.Value, (action) =>
                {
                    prefPos.Value = !prefPos.Value;
                    Main main = new Main(); main.OnPreferencesSaved();
                    AMUtils.RefreshActionMenu();
                }, PosfromView);
            }

            CustomSubMenu.AddToggle("Mirror follows you", prefTracking.Value, (action) =>
            {
            prefTracking.Value = !prefTracking.Value;
                Main main = new Main(); main.OnPreferencesSaved();
                AMUtils.RefreshActionMenu();
            }, FollowsYou);

        }

        public static void InitUi()
        {
            loadAssets();

            if (Main.amapi_ModsFolder.Value)
                AMUtils.AddToModsFolder("<color=#ff00ff>Portable Mirror</color>", () => AMsubMenu(), MirrorOpt);
            else
                VRCActionMenuPage.AddSubMenu(ActionMenuPage.Main, "<color=#ff00ff>Portable Mirror</color>", () => AMsubMenu(), MirrorOpt);
        }

        private static void AMsubMenu()
        {
            CustomSubMenu.AddSubMenu("Portable Mirror", () =>
            {
                CustomSubMenu.AddToggle("Enable", (Main._mirrorBase != null), (action) =>
                {
                    if (Utils.GetVRCPlayer() != null) Main.ToggleMirror();
                    AMUtils.RefreshActionMenu();
                }, MirrorBase);

                MirrorMenu("PortableMirror", Main._base_MirrorState, Main._base_CanPickupMirror, Main._base_MirrorDistance, Main._base_MirrorScaleX, Main._base_MirrorScaleY,
                    Main._base_PositionOnView, Main._base_AnchorToTracking);

            }, MirrorBase);

            CustomSubMenu.AddSubMenu("45 Mirror", () =>
            {
                CustomSubMenu.AddToggle("Enable", (Main._mirror45 != null), (action) =>
                {
                    if (Utils.GetVRCPlayer() != null) Main.ToggleMirror45();
                    AMUtils.RefreshActionMenu();
                }, Mirror45);

                MirrorMenu("PortableMirror45", Main._45_MirrorState, Main._45_CanPickupMirror, Main._45_MirrorDistance, Main._45_MirrorScaleX, Main._45_MirrorScaleY,
                    Main.Spacer1, Main._45_AnchorToTracking);

            }, Mirror45);

            CustomSubMenu.AddSubMenu("Ceiling Mirror", () =>
            {
                CustomSubMenu.AddToggle("Enable", (Main._mirrorCeiling != null), (action) =>
                {
                    if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorCeiling();
                    AMUtils.RefreshActionMenu();
                }, MirrorCeil);

                MirrorMenu("PortableMirrorCeiling", Main._ceil_MirrorState, Main._ceil_CanPickupMirror, Main._ceil_MirrorDistance, Main._ceil_MirrorScaleX, Main._ceil_MirrorScaleZ,
                    Main.Spacer1, Main._ceil_AnchorToTracking);

            }, MirrorCeil);

            CustomSubMenu.AddSubMenu("Micro Mirror", () =>
            {
                CustomSubMenu.AddToggle("Enable", (Main._mirrorMicro != null), (action) =>
                {
                    if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorMicro();
                    AMUtils.RefreshActionMenu();
                }, MirrorMicro);

                MirrorMenu("PortableMirrorMicro", Main._micro_MirrorState, Main._micro_CanPickupMirror, Main._base_MirrorDistance, Main._micro_MirrorScaleX, Main._micro_MirrorScaleY,
                    Main._micro_PositionOnView, Main._micro_AnchorToTracking);

            }, MirrorMicro);

            CustomSubMenu.AddSubMenu("Transparent Mirror", () =>
            {
                CustomSubMenu.AddToggle("Enable", (Main._mirrorTrans != null), (action) =>
                {
                    if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorTrans();
                    AMUtils.RefreshActionMenu();
                }, MirrorTrans);

                MirrorMenu("PortableMirrorTrans", Main._trans_MirrorState, Main._trans_CanPickupMirror, Main._trans_MirrorDistance, Main._trans_MirrorScaleX, Main._trans_MirrorScaleY,
                    Main._trans_PositionOnView, Main._trans_AnchorToTracking);

            }, MirrorTrans);



            CustomSubMenu.AddSubMenu("Extras", () =>
            {
                CustomSubMenu.AddButton($"Transparency:\n{Main.TransMirrorTrans.Value}", () =>
                {
                    Main.TransMirrorTrans.Value += .1f;
                    Main main = new Main(); main.OnPreferencesSaved();
                    AMUtils.RefreshActionMenu();
                }, TransPlus);
                CustomSubMenu.AddButton($"Transparency:\n{Main.TransMirrorTrans.Value}", () =>
                {
                    Main.TransMirrorTrans.Value -= .1f;
                    Main main = new Main(); main.OnPreferencesSaved();
                    AMUtils.RefreshActionMenu();
                }, TransMinus);
                CustomSubMenu.AddToggle("Mirrors Show In Camera", Main.MirrorsShowInCamera.Value, (action) =>
                {
                    Main.MirrorsShowInCamera.Value = !Main.MirrorsShowInCamera.Value;
                    Main main = new Main(); main.OnPreferencesSaved();
                    AMUtils.RefreshActionMenu();
                }, CameraMirror);
                CustomSubMenu.AddToggle("High Precision Adjust", Main._mirrorDistHighPrec, (action) =>
                {
                    Main._mirrorDistHighPrec = !Main._mirrorDistHighPrec;
                    Main main = new Main(); main.OnPreferencesSaved();
                    AMUtils.RefreshActionMenu();
                }, DistAdjIcon);

                CustomSubMenu.AddButton($"ColliderDepth:\n{Main.ColliderDepth.Value.ToString("F2").TrimEnd('0')}", () =>
                {
                    Main.ColliderDepth.Value += .1f;
                    Main main = new Main(); main.OnPreferencesSaved();
                    AMUtils.RefreshActionMenu();
                }, GrabDistPlus);
                CustomSubMenu.AddButton($"ColliderDepth:\n{Main.ColliderDepth.Value.ToString("F2").TrimEnd('0')}", () =>
                {
                    if (Main.ColliderDepth.Value > .1f)
                    {
                        Main.ColliderDepth.Value -= .1f;
                        Main main = new Main(); main.OnPreferencesSaved();
                        AMUtils.RefreshActionMenu();
                    }
                }, GrabDistMinus);

                CustomSubMenu.AddToggle("Pickups snap to hand", Main.PickupToHand.Value, (action) =>
                {
                    Main.PickupToHand.Value = !Main.PickupToHand.Value;
                    Main main = new Main(); main.OnPreferencesSaved();
                    AMUtils.RefreshActionMenu();
                }, SnapToHand);

                CustomSubMenu.AddToggle("All Pickupable", Main._AllPickupable, (action) =>
                {
                    Main._AllPickupable = !Main._AllPickupable;
                    Main._base_CanPickupMirror.Value = Main._AllPickupable;
                    Main._45_CanPickupMirror.Value = Main._AllPickupable;
                    Main._ceil_CanPickupMirror.Value = Main._AllPickupable;
                    Main._micro_CanPickupMirror.Value = Main._AllPickupable;
                    Main._trans_CanPickupMirror.Value = Main._AllPickupable;
                    Main main = new Main(); main.OnPreferencesSaved();
                    AMUtils.RefreshActionMenu();
                }, Grab);

            }, SettingsGear);
        }
    }
}
