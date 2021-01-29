﻿using Interactables.Interobjects.DoorUtils;
using MapControl.Handlers;
using MEC;
using Synapse.Api;
using Synapse.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapControl.Commands.gateLockdown
{
    [CommandInformation(
        Name = "gatelockdown",
        Aliases = new[] { "gl" },
        Description = "Start/Stop Gate lockdowns.",
        Permission = "mc.gatelockdown",
        Platforms = new[] { Platform.RemoteAdmin },
        Usage = "gatelockdown <yes/no> <time>"
        )]
    public class gateLockdown : ISynapseCommand
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
                        if (EventHandlers.isGateLocked == true)
                            result.Message = "Gates successfully unlocked with a broadcast!";
                        else
                            result.Message = "Gates successfully unlocked with  a broadcast!";
                        result.State = CommandResultState.Ok;
                        return result;
                    case "no":
                        Gatelockdown(false);
                        if (EventHandlers.isGateLocked == true)
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
                            EventHandlers.coroutines.Add(Timing.RunCoroutine(timedGatelockdown(true, time)));
                            if (EventHandlers.isGateLocked == true)
                                result.Message = "Gates successfully unlocked with a broadcast!";
                            else
                                result.Message = "Gates successfully unlocked with  a broadcast!";
                            result.State = CommandResultState.Ok;
                            return result;
                        case "no":
                            EventHandlers.coroutines.Add(Timing.RunCoroutine(timedGatelockdown(false, time)));
                            if (EventHandlers.isGateLocked == true)
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
            if (wannaBroadcastBro && EventHandlers.isGateLocked == false)
            {
                Map.Get.SendBroadcast(Plugin.Config.BroadcastDuration, Plugin.Config.GatelockdownBroadcast);
                Map.Get.Cassie(Plugin.Config.GatelockdownCassie);
            } 
            if (wannaBroadcastBro && EventHandlers.isGateLocked == true)
            {
                Map.Get.SendBroadcast(Plugin.Config.BroadcastDuration, Plugin.Config.GatelockdownEndingBroadcast);
                Map.Get.Cassie(Plugin.Config.GatelockdownEndingCassie);
            }

            DoorVariant gateA = DoorNametagExtension.NamedDoors["GATE_A"].TargetDoor;
            DoorVariant gateB = DoorNametagExtension.NamedDoors["GATE_B"].TargetDoor;

            if (EventHandlers.isGateLocked == false)
            {
                EventHandlers.isGateLocked = true;
                gateA.NetworkTargetState = false;
                gateA.NetworkActiveLocks = 1;

                gateB.NetworkTargetState = false;
                gateB.NetworkActiveLocks = 1;
            }
            else
            {
                EventHandlers.isGateLocked = false;
                gateA.NetworkTargetState = true;
                gateA.NetworkActiveLocks = 0;

                gateB.NetworkTargetState = true;
                gateB.NetworkActiveLocks = 0;
            }
            
        }

        public static IEnumerator<float> timedGatelockdown(bool wannaBroadcastBro, int time)
        {
            if (wannaBroadcastBro && EventHandlers.isGateLocked == false)
            {
                Map.Get.SendBroadcast(Plugin.Config.BroadcastDuration, Plugin.Config.GatelockdownBroadcast);
                Map.Get.Cassie(Plugin.Config.GatelockdownCassie);
            }
            if (wannaBroadcastBro && EventHandlers.isGateLocked == true)
            {
                Map.Get.SendBroadcast(Plugin.Config.BroadcastDuration, Plugin.Config.GatelockdownEndingBroadcast);
                Map.Get.Cassie(Plugin.Config.GatelockdownEndingCassie);
            }

            DoorVariant gateA = DoorNametagExtension.NamedDoors["GATE_A"].TargetDoor;
            DoorVariant gateB = DoorNametagExtension.NamedDoors["GATE_B"].TargetDoor;

            if (EventHandlers.isGateLocked == false)
            {
                EventHandlers.isGateLocked = true;
                gateA.NetworkTargetState = false;
                gateA.NetworkActiveLocks = 1;

                gateB.NetworkTargetState = false;
                gateB.NetworkActiveLocks = 1;
            }
            else
            {
                EventHandlers.isGateLocked = false;
                gateA.NetworkTargetState = true;
                gateA.NetworkActiveLocks = 0;

                gateB.NetworkTargetState = true;
                gateB.NetworkActiveLocks = 0;
            }

            yield return Timing.WaitForSeconds(time);

            if (wannaBroadcastBro && EventHandlers.isGateLocked == false)
            {
                Map.Get.SendBroadcast(Plugin.Config.BroadcastDuration, Plugin.Config.GatelockdownBroadcast);
                Map.Get.Cassie(Plugin.Config.GatelockdownCassie);
            }
            if (wannaBroadcastBro && EventHandlers.isGateLocked == true)
            {
                Map.Get.SendBroadcast(Plugin.Config.BroadcastDuration, Plugin.Config.GatelockdownEndingBroadcast);
                Map.Get.Cassie(Plugin.Config.GatelockdownEndingCassie);
            }

            if (EventHandlers.isGateLocked == false)
            {
                EventHandlers.isGateLocked = true;
                gateA.NetworkTargetState = false;
                gateA.NetworkActiveLocks = 1;

                gateB.NetworkTargetState = false;
                gateB.NetworkActiveLocks = 1;
            }
            else
            {
                EventHandlers.isGateLocked = false;
                gateA.NetworkTargetState = true;
                gateA.NetworkActiveLocks = 0;

                gateB.NetworkTargetState = true;
                gateB.NetworkActiveLocks = 0;
            }
        }
    }
}
