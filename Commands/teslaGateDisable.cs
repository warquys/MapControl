using Synapse.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapControl.Commands
{
    [CommandInformation(
        Name = "teslagatedisable",
        Aliases = new[] { "disabletesla", "teslaoff", "tgd" },
        Description = "Enable/Disable all tesla gates.",
        Permission = "mc.teslagatedisable",
        Platforms = new[] { Platform.RemoteAdmin },
        Usage = ".teslagatedisable"
        )]
    public class teslaGateDisable : ISynapseCommand
    {
        public static bool TeslaState = true;
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();

            if (!context.Player.HasPermission("mc.teslagatedisable"))
            {
                result.State = CommandResultState.NoPermission;
                result.Message = "You do not have enough permission!";
                return result;
            }

            if (TeslaState && Plugin.Config.TeslaGatesEnabled)
            {
                TeslaState = false;
                result.State = CommandResultState.Ok;
                result.Message = "Tesla gates disabled!";
                return result;
            }
            else if (TeslaState == false && Plugin.Config.TeslaGatesEnabled)
            {
                TeslaState = true;
                result.State = CommandResultState.Ok;
                result.Message = "Tesla gates enabled!";
                return result;
            }
            else
            {
                result.State = CommandResultState.Error;
                result.Message = "You need to have tesla gates enabled in the config for this command to work!";
                return result;
            }




        }
    }
}
