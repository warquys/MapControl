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
        Usage = "gatelockdown in the AR, yes or not for a broadcast, time optinal and in second",
        Arguments = new[] { "yes/no", "(time)" }
        )]
    public class GateLockdown : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();

            if (context.Arguments.Count < 1)
            {
                result.State = CommandResultState.Error;
                result.Message = "Usage: gatelockdown <yes/no> (time)";
                return result;
            }
            else if (context.Arguments.Count == 1)
            {
                ParseArguments(context.Arguments.Array[1], -1, result);
                return result;
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
                    ParseArguments(context.Arguments.Array[1], time, result);
                    return result;
                }
            }
            else
            {
                result.State = CommandResultState.Error;
                result.Message = "Usage: gatelockdown <yes/no> (time)";
                return result;
            }
        }

        public void ParseArguments(string arg, int time, CommandResult result)
        {
            switch (arg.ToLower())
            {
                case "yes":
                    result.State = CommandResultState.Ok;

                    if (time > 0)
                        EventHandlers.Coroutines.Add(Timing.RunCoroutine(TimedGatelockdown(true, time)));
                    else
                        Gatelockdown(true);

                    if (EventHandlers.IsGateLocked)
                        result.Message = "Gates successfully unlocked with a broadcast!";
                    else
                        result.Message = "Gates successfully unlocked with  a broadcast!";

                    break;
                case "no":
                    result.State = CommandResultState.Ok;

                    if (time > 0)
                        EventHandlers.Coroutines.Add(Timing.RunCoroutine(TimedGatelockdown(false, time)));
                    else
                        Gatelockdown(true);

                    if (EventHandlers.IsGateLocked)
                        result.Message = "Gates successfully unlocked without a broadcast!";
                    else
                        result.Message = "Gates successfully unlocked without a broadcast!";

                    break;
                default:
                    result.State = CommandResultState.Error;
                    result.Message = "Usage: gatelockdown <yes/no> (time)";
                    break;
            }
        }

        public void Gatelockdown(bool wannaBroadcastBro)
        {
            if (wannaBroadcastBro)
                SendWarnBroadcast();

            if (EventHandlers.IsGateLocked)
                Plugin.UnlockGate();
            else
                Plugin.LockGate();
        }

        public IEnumerator<float> TimedGatelockdown(bool wannaBroadcastBro, int time)
        {
            if (wannaBroadcastBro)
                SendWarnBroadcast();


            if (EventHandlers.IsGateLocked)
                Plugin.UnlockGate();
            else
                Plugin.LockGate();

            yield return Timing.WaitForSeconds(time);

            if (wannaBroadcastBro)
                SendWarnBroadcast();

            if (EventHandlers.IsGateLocked)
                Plugin.UnlockGate();
            else
                Plugin.LockGate();
        }

        public void SendWarnBroadcast()
        {
            if (EventHandlers.IsGateLocked)
            {
                Map.Get.SendBroadcast(Plugin.Config.BroadcastDuration, Plugin.PluginTranslation.ActiveTranslation.GatelockdownEndingBroadcast);
                Map.Get.Cassie(Plugin.Config.GatelockdownEndingCassie);
            }
            else
            {
                Map.Get.SendBroadcast(Plugin.Config.BroadcastDuration, Plugin.PluginTranslation.ActiveTranslation.GatelockdownBroadcast);
                Map.Get.Cassie(Plugin.Config.GatelockdownCassie);
            }
        }
    }
}
