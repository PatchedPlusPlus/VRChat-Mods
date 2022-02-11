using System.Collections;
using MelonLoader;
using UIExpansionKit.API;


namespace PortableMirror
{
    class UIX_QM
    {

        public static void CreateQuickMenuButton()
        {
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Toggle\nPortable\nMirror", () =>
            {
                if (Utils.GetVRCPlayer() != null) Main.ToggleMirror();
            }, (button) => { Main.ButtonList["Base"] = button.transform; button.gameObject.SetActive(Main._base_enableBase.Value); });
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Toggle\nPortable\nMirror 45", () =>
            {
                if (Utils.GetVRCPlayer() != null) Main.ToggleMirror45();
            }, (button) => { Main.ButtonList["45"] = button.transform; button.gameObject.SetActive(Main._45_enable45.Value); });
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Toggle\nCeiling\nMirror", () =>
            {
                if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorCeiling(); ;
            }, (button) => { Main.ButtonList["Ceiling"] = button.transform; button.gameObject.SetActive(Main._ceil_enableCeiling.Value); });
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Toggle\nMicro\nMirror", () =>
            {
                if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorMicro();
            }, (button) => { Main.ButtonList["Micro"] = button.transform; button.gameObject.SetActive(Main._micro_enableMicro.Value); });
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Toggle\nTransparent\nMirror", () =>
            {
                if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorTrans();
            }, (button) => { Main.ButtonList["Trans"] = button.transform; button.gameObject.SetActive(Main._trans_enableTrans.Value); });
            ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Portable\nMirror\nSettings", () =>
            {
                if (Main.OpenLastQMpage.Value && Main._qmOptionsLastPage == 2) QuickMenuOptions2();
                else QuickMenuOptions();
            }, (button) => { Main.ButtonList["Settings"] = button.transform; button.gameObject.SetActive(Main.QuickMenuOptions.Value); });
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
        public static void ToggleMirrorState(ref MelonPreferences_Entry<string> pref)
        {
            if (pref.Value == "MirrorFull") pref.Value = "MirrorOpt";
            else if (pref.Value == "MirrorOpt") pref.Value = "MirrorCutout";
            else if (pref.Value == "MirrorCutout") pref.Value = "MirrorTransparent";
            else if (pref.Value == "MirrorTransparent") pref.Value = "MirrorCutoutSolo";
            else if (pref.Value == "MirrorCutoutSolo") pref.Value = "MirrorTransparentSolo";
            else if (pref.Value == "MirrorTransparentSolo") pref.Value = "MirrorFull";
        }

        private static void QuickMenuOptions()
        {
            var mirrorMenu = ExpansionKitApi.CreateCustomQuickMenuPage(LayoutDescriptionCustom.QuickMenu3Column);
            Main._qmOptionsLastPage = 1;

            mirrorMenu.AddToggleButton("Portable Mirror", (action) =>
            {
                if (Utils.GetVRCPlayer() != null) Main.ToggleMirror();
            }, () => Main._mirrorBase != null);
            mirrorMenu.AddSimpleButton(StateText(Main._base_MirrorState.Value), () =>
            {
                ToggleMirrorState(ref Main._base_MirrorState);
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions();
            });
            mirrorMenu.AddToggleButton("Pickupable", (action) =>
            {
                Main._base_CanPickupMirror.Value = !Main._base_CanPickupMirror.Value;
            }, () => Main._base_CanPickupMirror.Value);
            mirrorMenu.AddLabel($"Distance: {Main._base_MirrorDistance.Value}");
            mirrorMenu.AddSimpleButton("+", () => {
                Main._base_MirrorDistance.Value = Main._base_MirrorDistance.Value + Main._mirrorDistAdj;
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions();
            });
            mirrorMenu.AddSimpleButton("-", () => {
                Main._base_MirrorDistance.Value = Main._base_MirrorDistance.Value - Main._mirrorDistAdj;
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions();
            });
            if (true)//(Main._45_enable45.Value)
            {
                mirrorMenu.AddToggleButton("45 Mirror", (action) =>
                {
                    if (Utils.GetVRCPlayer() != null) Main.ToggleMirror45();
                }, () => Main._mirror45 != null);
                mirrorMenu.AddSimpleButton(StateText(Main._45_MirrorState.Value), () =>
                {
                    ToggleMirrorState(ref Main._45_MirrorState);
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
                mirrorMenu.AddToggleButton("Pickupable", (action) =>
                {
                    Main._45_CanPickupMirror.Value = !Main._45_CanPickupMirror.Value;
                }, () => Main._45_CanPickupMirror.Value);
                mirrorMenu.AddLabel($"Distance: {Main._45_MirrorDistance.Value}");
                mirrorMenu.AddSimpleButton("+", () => {
                    Main._45_MirrorDistance.Value = Main._45_MirrorDistance.Value + Main._mirrorDistAdj;
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
                mirrorMenu.AddSimpleButton("-", () => {
                    Main._45_MirrorDistance.Value = Main._45_MirrorDistance.Value - Main._mirrorDistAdj;
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
            }
            if (true)//(Main._ceil_enableCeiling.Value)
            {
                mirrorMenu.AddToggleButton("Ceiling Mirror", (action) =>
                {
                    if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorCeiling();
                }, () => Main._mirrorCeiling != null);
                mirrorMenu.AddSimpleButton(StateText(Main._ceil_MirrorState.Value), () =>
                {
                    ToggleMirrorState(ref Main._ceil_MirrorState);
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
                mirrorMenu.AddToggleButton("Pickupable", (action) =>
                {
                    Main._ceil_CanPickupMirror.Value = !Main._ceil_CanPickupMirror.Value;
                }, () => Main._ceil_CanPickupMirror.Value);
                mirrorMenu.AddLabel($"Distance: {Main._ceil_MirrorDistance.Value}");
                mirrorMenu.AddSimpleButton("+", () => {
                    Main._ceil_MirrorDistance.Value = Main._ceil_MirrorDistance.Value + Main._mirrorDistAdj;
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
                mirrorMenu.AddSimpleButton("-", () => {
                    Main._ceil_MirrorDistance.Value = Main._ceil_MirrorDistance.Value - Main._mirrorDistAdj;
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
            }
            if (true)//(Main._micro_enableMicro.Value)
            {
                mirrorMenu.AddToggleButton("Micro Mirror", (action) =>
                {
                    if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorMicro();
                }, () => Main._mirrorMicro != null);
                mirrorMenu.AddSimpleButton(StateText(Main._micro_MirrorState.Value), () =>
                {
                    ToggleMirrorState(ref Main._micro_MirrorState);
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });
                mirrorMenu.AddToggleButton("Pickupable", (action) =>
                {
                    Main._micro_CanPickupMirror.Value = !Main._micro_CanPickupMirror.Value;
                }, () => Main._micro_CanPickupMirror.Value);
            }
            if (true)//(Main._trans_enableTrans.Value)
            {
                mirrorMenu.AddToggleButton("Transparent", (action) =>
                {
                    if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorTrans();
                }, () => Main._mirrorTrans != null);

                mirrorMenu.AddSimpleButton(StateText(Main._trans_MirrorState.Value), () =>
                {
                    ToggleMirrorState(ref Main._trans_MirrorState);
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });

                mirrorMenu.AddToggleButton("Pickupable", (action) =>
                {
                    Main._trans_CanPickupMirror.Value = !Main._trans_CanPickupMirror.Value;
                }, () => Main._trans_CanPickupMirror.Value);
                //6
                mirrorMenu.AddLabel($"Distance: {Main._trans_MirrorDistance.Value}");
                mirrorMenu.AddSimpleButton("+", () => {
                    Main._trans_MirrorDistance.Value = Main._trans_MirrorDistance.Value + Main._mirrorDistAdj;
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions();
                });

                mirrorMenu.AddSimpleButton("-", () => {
                    Main._trans_MirrorDistance.Value = Main._trans_MirrorDistance.Value - Main._mirrorDistAdj;
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
                Main._base_MirrorScaleX.Value += .25f;
                Main._base_MirrorScaleY.Value += .25f;
               Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });
            mirrorMenu.AddSimpleButton("Smaller", () => {
                if (Main._base_MirrorScaleX.Value > .25 && Main._base_MirrorScaleY.Value > .25)
                {
                    Main._base_MirrorScaleX.Value -= .25f;
                    Main._base_MirrorScaleY.Value -= .25f;
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions2();
                }
            });

            //2
            mirrorMenu.AddSpacer();
            mirrorMenu.AddToggleButton($"Angle from View", (action) => {
                Main._base_PositionOnView.Value = !Main._base_PositionOnView.Value;
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            }, () => Main._base_PositionOnView.Value);
            mirrorMenu.AddToggleButton($"Anchor to Tracking Space", (action) => {
                Main._base_AnchorToTracking.Value = !Main._base_AnchorToTracking.Value;
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            }, () => Main._base_AnchorToTracking.Value);

            //3
            mirrorMenu.AddToggleButton("45 Mirror", (action) =>
            {
                if (Utils.GetVRCPlayer() != null) Main.ToggleMirror45();
            }, () => Main._mirror45 != null);
            mirrorMenu.AddSimpleButton("Larger", () => {
                Main._45_MirrorScaleX.Value += .25f;
                Main._45_MirrorScaleY.Value += .25f;
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });
            mirrorMenu.AddSimpleButton("Smaller", () => {
                if (Main._45_MirrorScaleX.Value > .25 && Main._45_MirrorScaleY.Value > .25)
                {
                    Main._45_MirrorScaleX.Value -= .25f;
                    Main._45_MirrorScaleY.Value -= .25f;
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions2();
                }
            });

            //4
            mirrorMenu.AddSpacer();
            mirrorMenu.AddSpacer();
            mirrorMenu.AddToggleButton($"Anchor to Tracking Space", (action) => {
                Main._45_AnchorToTracking.Value = !Main._45_AnchorToTracking.Value;
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            }, () => Main._45_AnchorToTracking.Value);

            //5
            mirrorMenu.AddToggleButton("Ceiling Mirror", (action) =>
            {
                if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorCeiling();
            }, () => Main._mirrorCeiling != null);
            mirrorMenu.AddSimpleButton("Larger", () => {
                Main._ceil_MirrorScaleX.Value += .25f;
                Main._ceil_MirrorScaleZ.Value += .25f;
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });
            mirrorMenu.AddSimpleButton("Smaller", () => {
                if (Main._ceil_MirrorScaleX.Value > .25 && Main._ceil_MirrorScaleZ.Value > .25)
                {
                    Main._ceil_MirrorScaleX.Value -= .25f;
                    Main._ceil_MirrorScaleZ.Value -= .25f;
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions2();
                }
            });

            //6
            mirrorMenu.AddSpacer();
            mirrorMenu.AddSpacer();
            mirrorMenu.AddToggleButton($"Anchor to Tracking Space", (action) => {
                Main._ceil_AnchorToTracking.Value = !Main._ceil_AnchorToTracking.Value;
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            }, () => Main._ceil_AnchorToTracking.Value);

            //7
            mirrorMenu.AddToggleButton("Micro Mirror", (action) =>
            {
                if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorMicro();
            }, () => Main._mirrorMicro != null);

            mirrorMenu.AddSimpleButton("Larger", () => {
                Main._micro_MirrorScaleX.Value += .01f;
                Main._micro_MirrorScaleY.Value += .01f;
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });

            mirrorMenu.AddSimpleButton("Smaller", () => {
                if (Main._micro_MirrorScaleX.Value > .02 && Main._micro_MirrorScaleY.Value > .02)
                {
                    Main._micro_MirrorScaleX.Value -= .01f;
                    Main._micro_MirrorScaleY.Value -= .01f;
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions2();
                }
            });

            //8
            mirrorMenu.AddSpacer();
            mirrorMenu.AddToggleButton($"Angle from View", (action) => {
                Main._micro_PositionOnView.Value = !Main._micro_PositionOnView.Value;
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            }, () => Main._micro_PositionOnView.Value);
            mirrorMenu.AddToggleButton($"Anchor to Tracking Space", (action) => {
                Main._micro_AnchorToTracking.Value = !Main._micro_AnchorToTracking.Value;
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            }, () => Main._micro_AnchorToTracking.Value);

            //9
            mirrorMenu.AddToggleButton("Transparent", (action) =>
            {
                if (Utils.GetVRCPlayer() != null) Main.ToggleMirrorTrans();
            }, () => Main._mirrorTrans != null);

            mirrorMenu.AddSimpleButton("Larger", () => {
                Main._trans_MirrorScaleX.Value += .25f;
                Main._trans_MirrorScaleY.Value += .25f;
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            });

            mirrorMenu.AddSimpleButton("Smaller", () => {
                if (Main._trans_MirrorScaleX.Value > .25 && Main._trans_MirrorScaleY.Value > .25)
                {
                    Main._trans_MirrorScaleX.Value -= .25f;
                    Main._trans_MirrorScaleY.Value -= .25f;
                    Main main = new Main(); main.OnPreferencesSaved();
                    mirrorMenu.Hide(); QuickMenuOptions2();
                }
            });

            //10
            //mirrorMenu.AddSpacer();
            mirrorMenu.AddToggleButton($"-All Pickupable-", (action) => {
                Main._AllPickupable = !Main._AllPickupable;
                Main._base_CanPickupMirror.Value = Main._AllPickupable;
                Main._45_CanPickupMirror.Value = Main._AllPickupable;
                Main._ceil_CanPickupMirror.Value = Main._AllPickupable;
                Main._micro_CanPickupMirror.Value = Main._AllPickupable;
                Main._trans_CanPickupMirror.Value = Main._AllPickupable;
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            }, () => Main._AllPickupable);
            mirrorMenu.AddToggleButton($"Angle from View", (action) => {
                Main._trans_PositionOnView.Value = !Main._trans_PositionOnView.Value;
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            }, () => Main._trans_PositionOnView.Value);
            mirrorMenu.AddToggleButton($"Anchor to Tracking Space", (action) => {
                Main._trans_AnchorToTracking.Value = !Main._trans_AnchorToTracking.Value;
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            }, () => Main._trans_AnchorToTracking.Value);


            //11
            mirrorMenu.AddSimpleButton($"Close", () => {
                mirrorMenu.Hide();
            });
            mirrorMenu.AddSimpleButton($"Page 1", () => {
                mirrorMenu.Hide();
                QuickMenuOptions();
            });

            mirrorMenu.AddToggleButton("Pickups snap to hand", (action) =>
            {
                Main.PickupToHand.Value = !Main.PickupToHand.Value;
                Main main = new Main(); main.OnPreferencesSaved();
                mirrorMenu.Hide(); QuickMenuOptions2();
            }, () => Main.PickupToHand.Value);

            mirrorMenu.Show();
        }
    }
}


namespace UIExpansionKit.API
{
    public struct LayoutDescriptionCustom
    {
        public static LayoutDescription QuickMenu3Column = new LayoutDescription { NumColumns = 3, RowHeight = 380 / 10, NumRows = 10 }; //8 was 380   //475 / 10, NumRows = 10
        public static LayoutDescription QuickMenu3Column_Longer = new LayoutDescription { NumColumns = 3, RowHeight = 380 / 11, NumRows = 11 };  //475 / 11, NumRows = 11
    }
}