using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;

namespace GhostReplays
{
    [BepInPlugin("com.github.peak_ghostreplays", "GhostReplays", "1.0.0.0")]
    [BepInProcess("PEAK.exe")]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;
        private Harmony harmonyInstance;
        internal static List<UnityEngine.Object> instantiatedObjects = new List<UnityEngine.Object>();

        private void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;
            harmonyInstance = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());

            CharacterReplay.InstantiateCharacterReplays();

            Logger.LogInfo($"Plugin {Info.Metadata.GUID} is loaded!");
            Logger.LogWarning("This is a warning message, TEST.");
        }

        private void OnDestroy()
        {
            harmonyInstance.UnpatchSelf();
            Logger.LogInfo($"Plugin {Info.Metadata.GUID} is unloaded!");

            foreach (var obj in instantiatedObjects)
            {
                if (obj != null)
                {
                    Destroy(obj);
                }
            }
        }
    }

}

