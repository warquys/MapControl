using MapControl.Handlers;
using Synapse.Api.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapControl
{
    [PluginInformation(
        Author = "TheVoidNebula",
        Description = "Control your Map!.",
        LoadPriority = 0,
        Name = "MapControl",
        SynapseMajor = 2,
        SynapseMinor = 5,
        SynapsePatch = 0,
        Version = "1.1"
        )]
    public class Plugin : AbstractPlugin
    {
        [Synapse.Api.Plugin.Config(section = "MapControl")]
        public static Config Config;
        public override void Load()
        {
            SynapseController.Server.Logger.Info("MapControl loaded!");

            new EventHandlers();
        }

        public override void ReloadConfigs()
        {

        }
    }
}
