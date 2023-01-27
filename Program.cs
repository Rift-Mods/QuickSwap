using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

[HarmonyPatch(typeof(PlayerInput))]
[HarmonyPatch("Awake")] // if possible use nameof() here
internal class Patch01
{
    public KeyCode QuickSwap = KeyCode.Q;
    
    static void Postfix(PlayerInput __instance)
    {
        __instance.Interact = KeyCode.Z;
        Patch01 p1 = new Patch01();
        __instance.keyCodeLibrary.Add("QuickSwap", p1.QuickSwap);
    }
}

[HarmonyPatch(typeof(Loadout))]
[HarmonyPatch("CheckInput")] // if possible use nameof() here
internal class Patch02
{
    static void Postfix(Loadout __instance)
    {
        Patch01 p1 = new Patch01();
        if (Input.GetKeyDown(p1.QuickSwap))
        {
            if (__instance.curItemIndex == 0)
                __instance.curItemIndex = 1;
            else
                __instance.curItemIndex = 0;
            MethodInfo methodInfo = __instance.GetType().GetMethod("SelectWeapon", BindingFlags.NonPublic | BindingFlags.Instance);
            Debug.Log("CALLED!");

            methodInfo.Invoke(__instance, null);
        }
    }
}
    [HarmonyPatch(typeof(Loadout))]
    [HarmonyPatch("SelectWeapon")] // if possible use nameof() here
    internal class Patch03
    {
        static void Postfix(Loadout __instance)
        {
            Debug.Log("HELLO!");
        }
    }