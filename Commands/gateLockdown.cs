using Interactables.Interobjects.DoorUtils;
using MapControl.Handlers;
using MEC;
using Synapse.Api;
using Synapse.Command;
using System.Collections.Generic;

namespace MapControl.Commands
{
    [CommandInformation(
        Name = "gatelockdown",
        Aliases = new[] { "gl" },
        Description = "Start/Stop Gate lockdowns.",
        Permission = "mc.gatelockdown",
        Platforms = new[] { Platform.RemoteAdmin },
        Usage = "gatelockdown <yes/no> <time>"
        )]
    public class GateLockdown : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();


            if (!context.Player.HasPermission("mc.gatelockdown"))
            {
                result.State = CommandResultState.NoPermission;
                result.Message = "You do not have enough permissions!";
                return result;
            }

            if (context.Arguments.Count < 1)
            {
                result.State = CommandResultState.Error;
                result.Message = "Usage: gatelockdown <yes/no> <time>";
                return result;
            }
            else if (context.Arguments.Count == 1)
            {
                switch (context.Arguments.Array[1].ToLower())
                {
                    case "yes":
                        Gatelockdown(true);
                        if (EventHandlers.IsGateLocked)
                            result.Message = "Gates successfully unlocked with a broadcast!";
                        else
                            result.Message = "Gates successfully unlocked with  a broadcast!";
                        result.State = CommandResultState.Ok;
                        return result;
                    case "no":
                        Gatelockdown(false);
                        if (EventHandlers.IsGateLocked)
                            result.Message = "Gates successfully unlocked without a broadcast!";
                        else
                            result.Message = "Gates successfully unlocked without a broadcast!";
                        result.State = CommandResultState.Ok;
                        return result;
                    default:
                        result.State = CommandResultState.Error;
                        result.Message = "Usage: gatelockdown <yes/no> <time>";
                        return result;
                }

            }
            else if (context.Arguments.Count == 2)
            {
                var isNumeric = int.TryParse(context.Arguments.Array[2], out int time);
                if (!isNumeric)
                {
                    result.State = CommandResultState.Error;
                    result.Message = "You need to enter a number as the time argument!";
                    return result;
                }
                else
                {
                    switch (context.Arguments.Array[1].ToLower())
                    {
                        case "yes":
                            EventHandlers.Coroutines.Add(Timing.RunCoroutine(timedGatelockdown(true, time)));
                            if (EventHandlers.IsGateLocked)
                                result.Message = "Gates successfully unlocked with a broadcast!";
                            else
                                result.Message = "Gates successfully unlocked with  a broadcast!";
                            result.State = CommandResultState.Ok;
                            return result;
                        case "no":
                            EventHandlers.Coroutines.Add(Timing.RunCoroutine(timedGatelockdown(false, time)));
                            if (EventHandlers.IsGateLocked)
                                result.Message = "Gates successfully unlocked without a broadcast!";
                            else
                                result.Message = "Gates successfully unlocked without a broadcast!";
                            result.State = CommandResultState.Ok;
                            return result;
                            default:
                        result.State = CommandResultState.Error;
                        result.Message = "Usage: gatelockdown <yes/no> <time>";
                        return result;
                    }
                }

            }
            else
            {
                result.State = CommandResultState.Error;
                result.Message = "Usage: gatelockdown <yes/no> <time>";
                return result;
            }
        }


        public static void Gatelockdown(bool wannaBroadcastBro)
        {
            if (wannaBroadcastBro && !EventHandlers.IsGateLocked)
            {
                Map.Get.SendBroadcast(Plugin.Config.BroadcastDuration, Plugin.Config.GatelockdownBroadcast);
                Map.Get.Cassie(Plugin.Config.GatelockdownCassie);
            } 
            if (wannaBroadcastBro && EventHandlers.IsGateLocked)
            {
                Map.Get.SendBroadcast(Plugin.Config.BroadcastDuration, Plugin.Config.GatelockdownEndingBroadcast);
                Map.Get.Cassie(Plugin.Config.GatelockdownEndingCassie);
            }


            if (!EventHandlers.IsGateLocked)
            {
                EventHandlers.IsGateLocked = true;
                Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_A).Open = false;
                Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_A).Locked = true;

                Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_B).Open = false;
                Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_B).Locked = true;
            }
            else
            {
                EventHandlers.IsGateLocked = false;
                Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_A).Open = true;
                Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_A).Locked = false;

                Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_B).Open = true;
                Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_B).Locked = false;
            }
            
        }

        public static IEnumerator<float> timedGatelockdown(bool wannaBroadcastBro, int time)
        {
            if (wannaBroadcastBro && !EventHandlers.IsGateLocked)
            {
                Map.Get.SendBroadcast(Plugin.Config.BroadcastDuration, Plugin.Config.GatelockdownBroadcast);
                Map.Get.Cassie(Plugin.Config.GatelockdownCassie);
            }
            if (wannaBroadcastBro && EventHandlers.IsGateLocked)
            {
                Map.Get.SendBroadcast(Plugin.Config.BroadcastDuration, Plugin.Config.GatelockdownEndingBroadcast);
                Map.Get.Cassie(Plugin.Config.GatelockdownEndingCassie);
            }


            if (!EventHandlers.IsGateLocked)
            {
                EventHandlers.IsGateLocked = true;
                Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_A).Open = false;
                Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_A).Locked = true;

                Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_B).Open = false;
                Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_B).Locked = true;
            }
            else
            {
                EventHandlers.IsGateLocked = false;
                Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_A).Open = true;
                Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_A).Locked = false;

                Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_B).Open = true;
                Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_B).Locked = false;
            }

            yield return Timing.WaitForSeconds(time);

            if (wannaBroadcastBro && !EventHandlers.IsGateLocked)
            {
                Map.Get.SendBroadcast(Plugin.Config.BroadcastDuration, Plugin.Config.GatelockdownBroadcast);
                Map.Get.Cassie(Plugin.Config.GatelockdownCassie);
            }
            if (wannaBroadcastBro && EventHandlers.IsGateLocked)
            {
                Map.Get.SendBroadcast(Plugin.Config.BroadcastDuration, Plugin.Config.GatelockdownEndingBroadcast);
                Map.Get.Cassie(Plugin.Config.GatelockdownEndingCassie);
            }

            if (!EventHandlers.IsGateLocked)
            {
                EventHandlers.IsGateLocked = true;
                Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_A).Open = false;
                Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_A).Locked = true;

                Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_B).Open = false;
                Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_B).Locked = true;
            }
            else
            {
                EventHandlers.IsGateLocked = false;
                Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_A).Open = true;
                Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_A).Locked = false;

                Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_B).Open = true;
                Map.Get.GetDoor(Synapse.Api.Enum.DoorType.Gate_B).Locked = false;
            }
        }
    }
}
