using System.Collections;
using MelonLoader;
using UIExpansionKit.API;


namespace PortableMirror
{
    class UIX_QM
    {

        public static IEnumerator CreateQuickMenuButton()
        {
            while (QuickMenu.prop_QuickMenu_0 == null) yield return null;

            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Toggle\nPortable\nMirror", () =>
            {
                if (Utils.GetVRCPlayer() != null) Main.ToggleMirror();
            }, (button) => { Main.ButtonList["Base"] = button.transform; button.gameObject.SetActive(Main._enableBase); });
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Toggle\nPortable\nMirror 45", () =>
            {
                if (Utils.GetVRCPlayer() != null) Main.ToggleMirror45();
            }, (button) => { Main.ButtonList["45"] = button.transform; button.gameObject.SetActive(Main._enable45); });
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Toggle\nCeiling\nMirror", () =>
            {
                if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorCeiling(); ;
            }, (button) => { Main.ButtonList["Ceiling"] = button.transform; button.gameObject.SetActive(Main._enableCeiling); });
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Toggle\nMicro\nMirror", () =>
            {
                if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorMicro();
            }, (button) => { Main.ButtonList["Micro"] = button.transform; button.gameObject.SetActive(Main._enableMicro); });
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Toggle\nTransparent\nMirror", () =>
            {
                if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorTrans();
            }, (button) => { Main.ButtonList["Trans"] = button.transform; button.gameObject.SetActive(Main._enableTrans); });
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Portable\nMirror\nSettings", () =>
            {
                if (Main._openLastQMpage && Main._qmOptionsLastPage == 2) QuickMenuOptions2();
                else QuickMenuOptions();
            }, (button) => { Main.ButtonList["Settings"] = button.transform; button.gameObject.SetActive(Main._quickMenuOptions); });
        }

        public static string StateText(string stateRaw)
        {
            switch (stateRaw)
            {
                case "MirrorFull": return "Full";
                case "MirrorOpt": return "Optimized";
                case "MirrorCutout": return "Cutout";
                case "MirrorTransparent": return "Transparent";
                case "MirrorCutoutSolo": return "Cutout Solo";
                case "MirrorTransparentSolo": return "Transparent Solo";
                default: return "Something Broke";
            }
        }
        public static void ToggleMirrorState(string MelonPrefCat, string mirrorState)
        {
            if (mirrorState == "MirrorFull") MelonPreferences.SetEntryValue<string>(MelonPrefCat, "MirrorState", "MirrorOpt");
            else if (mirrorState == "MirrorOpt") MelonPreferences.SetEntryValue<string>(MelonPrefCat, "MirrorState", "MirrorCutout");
            else if (mirrorState == "MirrorCutout") MelonPreferences.SetEntryValue<string>(MelonPrefCat, "MirrorState", "MirrorTransparent");
            else if (mirrorState == "MirrorTransparent") MelonPreferences.SetEntryValue<string>(MelonPrefCat, "MirrorState", "MirrorCutoutSolo");
            else if (mirrorState == "MirrorCutoutSolo") MelonPreferences.SetEntryValue<string>(MelonPrefCat, "MirrorState", "MirrorTransparentSolo");
            else if (mirrorState == "MirrorTransparentSolo") MelonPreferences.SetEntryValue<string>(MelonPrefCat, "MirrorState", "MirrorFull");
        }

        private static void QuickMenuOptions()
        {
            var mirrorMenu = ExpansionKitApi.CreateCustomQuickMenuPage(LayoutDescriptionCustom.QuickMenu3Column);
            Main._qmOptionsLastPage = 1;

            mirrorMenu.AddToggleButton("Portable Mirror", (action) =>
            {
                if (Utils.GetVRCPlayer() != null) Main.ToggleMirror();
            }, () => Main._mirrorBase != null);
            mirrorMenu.AddSimpleButton(StateText(Main._mirrorStateBase), () =>
            {
                ToggleMirrorState("PortableMirror", Main._mirrorStateBase);
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions();
            });
            mirrorMenu.AddSimpleButton(MelonPreferences.GetEntryValue<bool>("PortableMirror", "CanPickupMirror") ? "Pickupable" : "Not Pickupable", () => {
                MelonPreferences.SetEntryValue<bool>("PortableMirror", "CanPickupMirror", !MelonPreferences.GetEntryValue<bool>("PortableMirror", "CanPickupMirror"));
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions();
            });
            mirrorMenu.AddLabel($"Distance: {MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorDistance")}");
            mirrorMenu.AddSimpleButton("+", () => {
                MelonPreferences.SetEntryValue<float>("PortableMirror", "MirrorDistance", MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorDistance") + Main._mirrorDistAdj);
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions();
            });
            mirrorMenu.AddSimpleButton("-", () => {
                MelonPreferences.SetEntryValue<float>("PortableMirror", "MirrorDistance", MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorDistance") - Main._mirrorDistAdj);
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions();
            });
            if (true)//(_enable45)
            {
                mirrorMenu.AddToggleButton("45 Mirror", (action) =>
                {
                    if (Utils.GetVRCPlayer() != null) Main.ToggleMirror45();
                }, () => Main._mirror45 != null);
                mirrorMenu.AddSimpleButton(StateText(Main._mirrorState45), () =>
                {
                    ToggleMirrorState("PortableMirror45", Main._mirrorState45);
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
                mirrorMenu.AddSimpleButton(MelonPreferences.GetEntryValue<bool>("PortableMirror45", "CanPickupMirror") ? "Pickupable" : "Not Pickupable", () =>
                {
                    MelonPreferences.SetEntryValue<bool>("PortableMirror45", "CanPickupMirror", !MelonPreferences.GetEntryValue<bool>("PortableMirror45", "CanPickupMirror"));
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
                mirrorMenu.AddLabel($"Distance: {MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorDistance")}");
                mirrorMenu.AddSimpleButton("+", () => {
                    MelonPreferences.SetEntryValue<float>("PortableMirror45", "MirrorDistance", MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorDistance") + Main._mirrorDistAdj);
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
                mirrorMenu.AddSimpleButton("-", () => {
                    MelonPreferences.SetEntryValue<float>("PortableMirror45", "MirrorDistance", MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorDistance") - Main._mirrorDistAdj);
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
            }
            if (true)//(_enableCeiling)
            {
                mirrorMenu.AddToggleButton("Ceiling Mirror", (action) =>
                {
                    if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorCeiling();
                }, () => Main._mirrorCeiling != null);
                mirrorMenu.AddSimpleButton(StateText(Main._mirrorStateCeiling), () =>
                {
                    ToggleMirrorState("PortableMirrorCeiling", Main._mirrorStateCeiling);
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
                mirrorMenu.AddSimpleButton(MelonPreferences.GetEntryValue<bool>("PortableMirrorCeiling", "CanPickupMirror") ? "Pickupable" : "Not Pickupable", () =>
                {
                    MelonPreferences.SetEntryValue<bool>("PortableMirrorCeiling", "CanPickupMirror", !MelonPreferences.GetEntryValue<bool>("PortableMirrorCeiling", "CanPickupMirror"));
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
                mirrorMenu.AddLabel($"Distance: {MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorDistance")}");
                mirrorMenu.AddSimpleButton("+", () => {
                    MelonPreferences.SetEntryValue<float>("PortableMirrorCeiling", "MirrorDistance", MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorDistance") + Main._mirrorDistAdj);
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
                mirrorMenu.AddSimpleButton("-", () => {
                    MelonPreferences.SetEntryValue<float>("PortableMirrorCeiling", "MirrorDistance", MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorDistance") - Main._mirrorDistAdj);
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
            }
            if (true)//(_enableMicro)
            {
                mirrorMenu.AddToggleButton("Micro Mirror", (action) =>
                {
                    if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorMicro();
                }, () => Main._mirrorMicro != null);
                mirrorMenu.AddSimpleButton(StateText(Main._mirrorStateMicro), () =>
                {
                    ToggleMirrorState("PortableMirrorMicro", Main._mirrorStateMicro);
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
                mirrorMenu.AddSimpleButton(MelonPreferences.GetEntryValue<bool>("PortableMirrorMicro", "CanPickupMirror") ? "Pickupable" : "Not Pickupable", () =>
                {
                    MelonPreferences.SetEntryValue<bool>("PortableMirrorMicro", "CanPickupMirror", !MelonPreferences.GetEntryValue<bool>("PortableMirrorMicro", "CanPickupMirror"));
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
            }
            if (true)//(_enableTrans)
            {
                mirrorMenu.AddToggleButton("Transparent", (action) =>
                {
                    if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorTrans();
                }, () => Main._mirrorTrans != null);

                mirrorMenu.AddSimpleButton(StateText(Main._mirrorStateTrans), () =>
                {
                    ToggleMirrorState("PortableMirrorTrans", Main._mirrorStateTrans);
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });

                mirrorMenu.AddSimpleButton(MelonPreferences.GetEntryValue<bool>("PortableMirrorTrans", "CanPickupMirror") ? "Pickupable" : "Not Pickupable", () =>
                {
                    MelonPreferences.SetEntryValue<bool>("PortableMirrorTrans", "CanPickupMirror", !MelonPreferences.GetEntryValue<bool>("PortableMirrorTrans", "CanPickupMirror"));
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
                //6
                mirrorMenu.AddLabel($"Distance: {MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorDistance")}");
                mirrorMenu.AddSimpleButton("+", () => {
                    MelonPreferences.SetEntryValue<float>("PortableMirrorTrans", "MirrorDistance", MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorDistance") + Main._mirrorDistAdj);
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });

                mirrorMenu.AddSimpleButton("-", () => {
                    MelonPreferences.SetEntryValue<float>("PortableMirrorTrans", "MirrorDistance", MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorDistance") - Main._mirrorDistAdj);
                    Main main = new Main(); main.OnPreferencesSaved();
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
            mirrorMenu.AddToggleButton("High Precision Distance Adj", (action) =>
            {
                Main._mirrorDistHighPrec = !Main._mirrorDistHighPrec;
               Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions();
            }, () => Main._mirrorDistHighPrec);

            mirrorMenu.Show();
        }

        private static void QuickMenuOptions2()
        {
            var mirrorMenu = ExpansionKitApi.CreateCustomQuickMenuPage(LayoutDescriptionCustom.QuickMenu3Column_Longer);
            Main._qmOptionsLastPage = 2;
            //Row 1
            mirrorMenu.AddToggleButton("Portable Mirror", (action) =>
            {
                if (Utils.GetVRCPlayer() != null) Main.ToggleMirror();
            }, () => Main._mirrorBase != null);
            mirrorMenu.AddSimpleButton("Larger", () => {
                MelonPreferences.SetEntryValue<float>("PortableMirror", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorScaleX") + .25f);
                MelonPreferences.SetEntryValue<float>("PortableMirror", "MirrorScaleY", MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorScaleY") + .25f);
               Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });
            mirrorMenu.AddSimpleButton("Smaller", () => {
                if (MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorScaleX") > .25 && MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorScaleY") > .25)
                {
                    MelonPreferences.SetEntryValue<float>("PortableMirror", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorScaleX") - .25f);
                    MelonPreferences.SetEntryValue<float>("PortableMirror", "MirrorScaleY", MelonPreferences.GetEntryValue<float>("PortableMirror", "MirrorScaleY") - .25f);
                   Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions2();
                }
            });

            //2
            mirrorMenu.AddSpacer();
            mirrorMenu.AddSimpleButton($"{(MelonPreferences.GetEntryValue<bool>("PortableMirror", "PositionOnView") ? "Angle from View":"Angle is 90*")}", () => {
                MelonPreferences.SetEntryValue<bool>("PortableMirror", "PositionOnView", !MelonPreferences.GetEntryValue<bool>("PortableMirror", "PositionOnView"));
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });
            mirrorMenu.AddSimpleButton($"{(MelonPreferences.GetEntryValue<bool>("PortableMirror", "AnchorToTracking") ? "Anchored to Tracking Space" : "Anchored to World Space")}", () => {
                MelonPreferences.SetEntryValue<bool>("PortableMirror", "AnchorToTracking", !MelonPreferences.GetEntryValue<bool>("PortableMirror", "AnchorToTracking"));
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });

            //3
            mirrorMenu.AddToggleButton("45 Mirror", (action) =>
            {
                if (Utils.GetVRCPlayer() != null) Main.ToggleMirror45();
            }, () => Main._mirror45 != null);
            mirrorMenu.AddSimpleButton("Larger", () => {
                MelonPreferences.SetEntryValue<float>("PortableMirror45", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorScaleX") + .25f);
                MelonPreferences.SetEntryValue<float>("PortableMirror45", "MirrorScaleY", MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorScaleY") + .25f);
               Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });
            mirrorMenu.AddSimpleButton("Smaller", () => {
                if (MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorScaleX") > .25 && MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorScaleY") > .25)
                {
                    MelonPreferences.SetEntryValue<float>("PortableMirror45", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorScaleX") - .25f);
                    MelonPreferences.SetEntryValue<float>("PortableMirror45", "MirrorScaleY", MelonPreferences.GetEntryValue<float>("PortableMirror45", "MirrorScaleY") - .25f);

                   Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions2();
                }
            });

            //4
            mirrorMenu.AddSpacer();
            mirrorMenu.AddSpacer();
            mirrorMenu.AddSimpleButton($"{(MelonPreferences.GetEntryValue<bool>("PortableMirror45", "AnchorToTracking") ? "Anchored to Tracking Space" : "Anchored to World Space")}", () => {
                MelonPreferences.SetEntryValue<bool>("PortableMirror45", "AnchorToTracking", !MelonPreferences.GetEntryValue<bool>("PortableMirror45", "AnchorToTracking"));
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });

            //5
            mirrorMenu.AddToggleButton("Ceiling Mirror", (action) =>
            {
                if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorCeiling();
            }, () => Main._mirrorCeiling != null);
            mirrorMenu.AddSimpleButton("Larger", () => {
                MelonPreferences.SetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleX") + .25f);
                MelonPreferences.SetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleZ", MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleZ") + .25f);

               Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });
            mirrorMenu.AddSimpleButton("Smaller", () => {
                if (MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleX") > .25 && MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleZ") > .25)
                {
                    MelonPreferences.SetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleX") - .25f);
                    MelonPreferences.SetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleZ", MelonPreferences.GetEntryValue<float>("PortableMirrorCeiling", "MirrorScaleZ") - .25f);

                   Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions2();
                }
            });

            //6
            mirrorMenu.AddSpacer();
            mirrorMenu.AddSpacer();
            mirrorMenu.AddSimpleButton($"{(MelonPreferences.GetEntryValue<bool>("PortableMirrorCeiling", "AnchorToTracking") ? "Anchored to Tracking Space" : "Anchored to World Space")}", () => {
                MelonPreferences.SetEntryValue<bool>("PortableMirrorCeiling", "AnchorToTracking", !MelonPreferences.GetEntryValue<bool>("PortableMirrorCeiling", "AnchorToTracking"));
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });

            //7
            mirrorMenu.AddToggleButton("Micro Mirror", (action) =>
            {
                if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorMicro();
            }, () => Main._mirrorMicro != null);

            mirrorMenu.AddSimpleButton("Larger", () => {
                MelonPreferences.SetEntryValue<float>("PortableMirrorMicro", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirrorMicro", "MirrorScaleX") + .01f);
                MelonPreferences.SetEntryValue<float>("PortableMirrorMicro", "MirrorScaleY", MelonPreferences.GetEntryValue<float>("PortableMirrorMicro", "MirrorScaleY") + .01f);
               Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });

            mirrorMenu.AddSimpleButton("Smaller", () => {
                if (MelonPreferences.GetEntryValue<float>("PortableMirrorMicro", "MirrorScaleX") > .02 && MelonPreferences.GetEntryValue<float>("PortableMirrorMicro", "MirrorScaleY") > .02)
                {
                    MelonPreferences.SetEntryValue<float>("PortableMirrorMicro", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirrorMicro", "MirrorScaleX") - .01f);
                    MelonPreferences.SetEntryValue<float>("PortableMirrorMicro", "MirrorScaleY", MelonPreferences.GetEntryValue<float>("PortableMirrorMicro", "MirrorScaleY") - .01f);

                   Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions2();
                }
            });

            //8
            mirrorMenu.AddSpacer();
            mirrorMenu.AddSimpleButton($"{(MelonPreferences.GetEntryValue<bool>("PortableMirrorMicro", "PositionOnView") ? "Angle from View" : "Angle is 90*")}", () => {
                MelonPreferences.SetEntryValue<bool>("PortableMirrorMicro", "PositionOnView", !MelonPreferences.GetEntryValue<bool>("PortableMirrorMicro", "PositionOnView"));
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });
            mirrorMenu.AddSimpleButton($"{(MelonPreferences.GetEntryValue<bool>("PortableMirrorMicro", "AnchorToTracking") ? "Anchored to Tracking Space" : "Anchored to World Space")}", () => {
                MelonPreferences.SetEntryValue<bool>("PortableMirrorMicro", "AnchorToTracking", !MelonPreferences.GetEntryValue<bool>("PortableMirrorMicro", "AnchorToTracking"));
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });

            //9
            mirrorMenu.AddToggleButton("Transparent", (action) =>
            {
                if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorTrans();
            }, () => Main._mirrorTrans != null);

            mirrorMenu.AddSimpleButton("Larger", () => {
                MelonPreferences.SetEntryValue<float>("PortableMirrorTrans", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorScaleX") + .25f);
                MelonPreferences.SetEntryValue<float>("PortableMirrorTrans", "MirrorScaleY", MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorScaleY") + .25f);
               Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });

            mirrorMenu.AddSimpleButton("Smaller", () => {
                if (MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorScaleX") > .25 && MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorScaleY") > .25)
                {
                    MelonPreferences.SetEntryValue<float>("PortableMirrorTrans", "MirrorScaleX", MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorScaleX") - .25f);
                    MelonPreferences.SetEntryValue<float>("PortableMirrorTrans", "MirrorScaleY", MelonPreferences.GetEntryValue<float>("PortableMirrorTrans", "MirrorScaleY") - .25f);
                   Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions2();
                }
            });

            //10
            mirrorMenu.AddSpacer();
            mirrorMenu.AddSimpleButton($"{(MelonPreferences.GetEntryValue<bool>("PortableMirrorTrans", "PositionOnView") ? "Angle from View" : "Angle is 90*")}", () => {
                MelonPreferences.SetEntryValue<bool>("PortableMirrorTrans", "PositionOnView", !MelonPreferences.GetEntryValue<bool>("PortableMirrorTrans", "PositionOnView"));
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });
            mirrorMenu.AddSimpleButton($"{(MelonPreferences.GetEntryValue<bool>("PortableMirrorTrans", "AnchorToTracking") ? "Anchored to Tracking Space" : "Anchored to World Space")}", () => {
                MelonPreferences.SetEntryValue<bool>("PortableMirrorTrans", "AnchorToTracking", !MelonPreferences.GetEntryValue<bool>("PortableMirrorTrans", "AnchorToTracking"));
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });



            //11
            mirrorMenu.AddSimpleButton($"Close", () => {
                mirrorMenu.Hide();
            });
            mirrorMenu.AddSimpleButton($"Page 1", () => {
                mirrorMenu.Hide();
                QuickMenuOptions();
            });

            mirrorMenu.Show();
        }
    }
}


namespace UIExpansionKit.API
{
    public struct LayoutDescriptionCustom
    {
        public static LayoutDescription QuickMenu3Column = new LayoutDescription { NumColumns = 3, RowHeight = 475 / 10, NumRows = 10 }; //8 was 380
        public static LayoutDescription QuickMenu3Column_Longer = new LayoutDescription { NumColumns = 3, RowHeight = 475 / 11, NumRows = 11 };
    }
}