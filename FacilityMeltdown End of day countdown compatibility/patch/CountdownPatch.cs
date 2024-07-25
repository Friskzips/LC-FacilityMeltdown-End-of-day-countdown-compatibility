
namespace Friskzips.patch;


using HarmonyLib;
using LC_CountDownMod.Patches;
using FacilityMeltdown.MeltdownSequence.Behaviours;
using UnityEngine;
using Friskzips;
using LC_CountDownMod;


[HarmonyPatch(typeof(MeltdownHandler))]

public class MeltdownCheck
{
    public static bool triggerOnce = false;

    [HarmonyPatch("StartMeltdownClientRpc")]
    [HarmonyPostfix]
    public static void Postfix(ref MeltdownHandler __instance)
    {

        Plugin.Log.LogInfo("Meltdown detected!");
        Plugin.Log.LogInfo("Time until meltdown: " + __instance.TimeLeftUntilMeltdown);
        triggerOnce = true;

    }

    

}


[HarmonyPatch(typeof(TimeOfDay))]
public class TenSecondCheck : MonoBehaviour
{
    
    [HarmonyPatch("Update")]
    [HarmonyPostfix]
    [HarmonyPatch(typeof(MeltdownHandler))]

    private static void Update(ref MeltdownHandler __instance)
    {
        //Plugin.Log.LogInfo("Test: "+ __instance.TimeLeftUntilMeltdown);

        if (__instance.TimeLeftUntilMeltdown <= 30 && MeltdownCheck.triggerOnce == true) //
        {
            TimeOfDayUpdatePatch.DisableDisabler = false;
            TimeOfDayUpdatePatch.DownTimer = 2.5f;
            TimeOfDayUpdatePatch.Number = 9;
            TimeOfDayUpdatePatch.CountingDown = true;
            CountDownModBase.countdowner.SpawnCountdown();
            CountDownModBase.countdowner.SetCanvasLayer(CountDownModBase.CountdownUISortOrder.Value);
            CountDownModBase.countdowner.Countdown(10);

            MeltdownCheck.triggerOnce = false;
            
        }
        
    }

}




