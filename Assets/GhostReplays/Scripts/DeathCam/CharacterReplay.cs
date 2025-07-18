using UnityEngine;
using HarmonyLib;
using Zorro.Core;
using System.Reflection;
using System.Collections.Generic;


namespace GhostReplays
{
    public class CharacterReplay : MonoBehaviour
    {
        private Character character;
        private List<ReplayFrame> replay = new List<ReplayFrame>(1000);
        private Transform animationPosition;
        private Transform animationLook;


        private void Awake()
        {
            Plugin.instantiatedObjects.Add(this);
            character = GetComponent<Character>();
            animationPosition = transform.Find("helperObjects/animationPosition");
            animationLook = transform.Find("helperObjects/animationLook");
            Plugin.Logger.LogInfo("CharacterReplay component initialized on " + character.characterName);
        }

        private void OnDestroy()
        {
            Plugin.Logger.LogInfo("CharacterReplay component destroyed for " + character.characterName);
        }

        private void Update()
        {
            recordFrame();    
        }

        /// <summary>
        /// If plugin loaded mid-game, instantiate CharacterReplay for existing characters.
        /// Otherwise instantiated on Character.Awake (see CharacterPatches.cs)
        /// </summary>
        internal static void InstantiateCharacterReplays()
        {
            var playerCharacters = PlayerHandler.GetAllPlayerCharacters();
            foreach (var character in playerCharacters)
            {
                if (character.GetComponent<CharacterReplay>() != null)
                {
                    Destroy(character.GetComponent<CharacterReplay>());
                }
                character.gameObject.AddComponent<CharacterReplay>();
            }
        }

        private void recordFrame()
        {
            if (Time.frameCount % Mathf.RoundToInt(Time.timeScale * (1f / 30f) / Time.fixedDeltaTime) != 0)
                return;
            
            var frame = new ReplayFrame(animationPosition, animationLook);
            replay.Add(frame);
        }
    }
}