using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Friskzips.patch;
using Friskzips.service;
using LC_CountDownMod.Patches;
using static Friskzips.patch.MeltdownCheck;

namespace Friskzips;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    public static Plugin Instance { get; set; }

    internal static readonly BepInEx.Logging.ManualLogSource Log = BepInEx.Logging.Logger.CreateLogSource(PluginInfo.PLUGIN_NAME);

    private readonly Harmony _harmony = new(PluginInfo.PLUGIN_GUID);

    public TemplateService Service;

    public Plugin()
    {
        Instance = this;
    }

    private void Awake()
    {
        Service = new TemplateService();


        Log.LogInfo($"Applying patches...");
        ApplyPluginPatch();
        Log.LogInfo($"Patches applied");
    }

    /// <summary>
    /// Applies the patch to the game.
    /// </summary>
    private void ApplyPluginPatch()
    {
        
        
        _harmony.PatchAll(typeof(MeltdownCheck));
        _harmony.PatchAll(typeof(TenSecondCheck));
       



    }
}
