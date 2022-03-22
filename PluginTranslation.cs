using Synapse.Translation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapControl
{
    public class PluginTranslation : IPluginTranslation
    {
        [Description("Broadcast of the normal Gatelockdown ")]
        public string GatelockdownBroadcast { get; set; } = "<b>The gates have been locked down!</b>";

        [Description("Broadcast of normal Gatelockdown when the gates are being unlocked")]
        public string GatelockdownEndingBroadcast { get; set; } = "<b>The gates are no longer locked down!</b>";

        [Description("Broadcast of normal Tesla Timeout")]
        public string TeslaTimeoutBroadcast { get; set; } = "<b>The tesla gates have a timeout!</b>";

        [Description("Broadcast of Tesla Timout when the gates are being unlocked")]
        public string TeslaTimeoutEndingBroadcast { get; set; } = "<b>The tesla gates have no longer a timeout!</b>";
    }
}
