﻿using System;
using System.IO;
using System.Reflection;
using Harmony;
using MelonLoader;
//https://github.com/knah/VRCMods/tree/master/Common

namespace PortableMirror
{
    [HarmonyShield]
    internal static class LoaderIntegrityCheck
    {
        public static void CheckIntegrity()
        {
            try
            {
                using var stream = Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("PortableMirrorMod._dummy_.dll");
                using var memStream = new MemoryStream((int)stream.Length);
                stream.CopyTo(memStream);

                Assembly.Load(memStream.ToArray());

                PrintWarningMessage();

                while (Console.In.Peek() != '\n') Console.In.Read();
            }
            catch (BadImageFormatException)
            {
            }

            try
            {
                using var stream = Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("PortableMirrorMod._dummy2_.dll");
                using var memStream = new MemoryStream((int)stream.Length);
                stream.CopyTo(memStream);

                Assembly.Load(memStream.ToArray());
            }
            catch (BadImageFormatException ex)
            {
                Main.Logger.Error(ex.ToString());

                PrintWarningMessage();

                while (Console.In.Peek() != '\n') Console.In.Read();
            }

            try
            {
                var harmony = HarmonyInstance.Create(Guid.NewGuid().ToString());
                harmony.Patch(AccessTools.Method(typeof(LoaderIntegrityCheck), nameof(PatchTest)),
                    new HarmonyMethod(typeof(LoaderIntegrityCheck), nameof(ReturnFalse)));

                PatchTest();

                PrintWarningMessage();

                while (Console.In.Peek() != '\n') Console.In.Read();
            }
            catch (BadImageFormatException)
            {
            }
        }

        private static bool ReturnFalse() => false;

        public static void PatchTest()
        {
            throw new BadImageFormatException();
        }

        private static void PrintWarningMessage()
        {
            Main.Logger.Error("===================================================================");
            Main.Logger.Error("You're using MelonLoader with important security features missing.");
            Main.Logger.Error("This exposes you to additional risks from certain malicious actors,");
            Main.Logger.Error("including account theft, account bans, and other unwanted consequences");
            Main.Logger.Error("If this is not what you want, download the official installer from");
            Main.Logger.Error("https://github.com/LavaGang/MelonLoader/releases");
            Main.Logger.Error("then close this console, and reinstall MelonLoader using it.");
            Main.Logger.Error("If you want to accept those risks, press Enter to continue");
            Main.Logger.Error("===================================================================");
        }
    }
}