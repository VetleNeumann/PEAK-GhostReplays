using HarmonyLib;
using GhostReplays;

[HarmonyPatch]
static class CharacterPatches
{
    [HarmonyPatch(typeof(CharacterAnimations), "PlayEmote")]
    [HarmonyPrefix]
    static bool Prefix(ref CharacterAnimations __instance, ref string emoteName)
    {
        Plugin.Logger.LogInfo($"Tried to play emote: {emoteName}");

        if (emoteName == "A_Scout_Emote_Flex")
        {
            Plugin.Logger.LogInfo("Overriding PlayDead emote: killing character.");

            var character = __instance.GetComponentInParent<Character>();
            AccessTools.Method(typeof(Character), "Die").Invoke(character, null);
            return false;
        }

        return true; // play intended emote
    }

    [HarmonyPatch(typeof(Character), "Awake")]
    [HarmonyPostfix]
    /// <summary>
    /// Instantiate CharacterReplay when a Character is created.
    /// </summary>
    private static void CharacterAwake_Postfix(ref Character __instance)
    {
        Plugin.Logger.LogWarning("CharacterAwake_Postfix called for " + __instance.characterName);
        __instance.gameObject.AddComponent<CharacterReplay>();
    }
}