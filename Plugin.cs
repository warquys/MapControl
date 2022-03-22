using MapControl.Handlers;
using Synapse.Api;
using Synapse.Api.Plugin;

namespace MapControl
{
    [PluginInformation(
        Author = "TheVoidNebula, update by VT",
        Description = "Control your Map!.",
        LoadPriority = 0,
        Name = "MapControl",
        SynapseMajor = 2,
        SynapseMinor = 6,
        SynapsePatch = 0,
        Version = "v1.2.0"
        )]
    public class Plugin : AbstractPlugin
    {
        [Synapse.Api.Plugin.Config(section = "MapControl")]
        public static Config Config;

        [SynapseTranslation]
        public static Synapse.Translation.SynapseTranslation<PluginTranslation> PluginTranslation;

        public override void Load()
        {
            SynapseController.Server.Logger.Info("MapControl loaded!");

            PluginTranslation.AddTranslation(new PluginTranslation());

            PluginTranslation.AddTranslation(new PluginTranslation
            {
                GatelockdownBroadcast = "Die Gate wurden verschlossen!",
                GatelockdownEndingBroadcast = "Die Gate sind nicht mehr verschlossen!",
                TeslaTimeoutBroadcast = "Die Tesla gates haben ein Timeout!",
                TeslaTimeoutEndingBroadcast = "Die Tesla gates werden Reaktiviert!"

            }, "GERMAN");

            PluginTranslation.AddTranslation(new PluginTranslation
            {
                GatelockdownBroadcast = "Les gates ont été verrouillées !",
                GatelockdownEndingBroadcast = "Les gates ne sont plus verrouillé !",
                TeslaTimeoutBroadcast = "Les tesla gates ont un temps mort !",
                TeslaTimeoutEndingBroadcast = "Les tesla gates sont réactiver !"

            }, "FRENCH");

            //Feel free to ask me or create a PR in order to add more languages
            new EventHandlers();
        }

        public static void LockGate()
        {
            EventHandlers.IsGateLocked = true;
            Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_A).Open = false;
            Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_A).Locked = true;

            Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_B).Open = false;
            Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_B).Locked = true;
        }

        public static void UnlockGate()
        {
            EventHandlers.IsGateLocked = false;
            Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_A).Open = Plugin.Config.GatelockdownOpen;
            Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_A).Locked = false;

            Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_B).Open = Plugin.Config.GatelockdownOpen;
            Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_B).Locked = false;
        }
    }
}
