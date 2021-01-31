using Synapse.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapControl
{
    public class Config : AbstractConfigSection
    {

        [Description("Is this plugin enabled?")]
        public bool IsEnabled = true;

        [Description("Are tesla gates enabled?")]
        public bool TeslaGatesEnabled = true;

        [Description("Should random tesla gate timeouts happen?")]
        public bool RandomTeslaTimeouts = true;

        [Description("Should Gate A and Gate B sometimes be randomly locked?")]
        public bool RandomGatelockdowns = true;

        [Description("Should Gate A and Gate B be locked at the beginning of the round?")]
        public bool RoundStartGatelockdown = true;

        [Description("Should Tesla gates be disabled if the player has a certain item in his inventory?")]
        public bool TeslaGateBypassItemsEnabled = true;

        [Description("Which items in the inventory of a player should not trigger tesla gates?")]
        public List<int> TeslaBypassItems = new List<int>()
        {
            11,
            19
        };

        [Description("Which classes should not trigger tesla gates?")]
        public List<int> TeslaBypassClasses = new List<int>()
        {
            4,
            6,
            11,
            12,
            13,
            15
        };

        [Description("How long should Gate A and Gate B be locked at the beginning of the round?")]
        public float RoundStartGatelockdownDuration = 120f;

        [Description("How long should the Gatelockdown at the beginning of the round should be delayed? (Important to prevent broadcast spam at the start of the round)")]
        public float RoundStartGatelockdownDelay = 10f;

        [Description("Should a broadcast be shown when a Gatelockdown happens?")]
        public bool GatelockdownBroadcastEnabled = true;

        [Description("Should a C.A.S.S.I.E announcement be shown when a Gatelockdown happens?")]
        public bool GatelockdownCassieEnabled = true;

        [Description("Should a broadcast be shown when a Tesla timeout happens?")]
        public bool TeslaTimeoutBroadcastEnabled = true;

        [Description("Should a C.A.S.S.I.E announcement be shown when a Tesla timeout happens?")]
        public bool TeslaTimeoutCassieEnabled = true;

        [Description("What should the normal Gatelockdown Broadcast show?")]
        public string GatelockdownBroadcast = "<b>The gates to surface have been locked down!</b>";

        [Description("What should the normal Gatelockdown C.A.S.S.I.E announcement show?")]
        public string GatelockdownCassie = "The gates to surface have been locked down";

        [Description("What should the normal Gatelockdown Broadcast show when the gates are being unlocked?")]
        public string GatelockdownEndingBroadcast = "<b>The gates to surface are no longer locked down!</b>";

        [Description("What should the normal Gatelockdown C.A.S.S.I.E announcement show?")]
        public string GatelockdownEndingCassie = "The gates to surface are no longer locked down";

        [Description("What should the normal Tesla Timeout Broadcast show?")]
        public string TeslaTimeoutBroadcast = "<b>The tesla gates have a timeout!</b>";

        [Description("What should the normal Tesla Timout C.A.S.S.I.E announcement show?")]
        public string TeslaTimeoutCassie = "The facility tesla gate. are disabled";

        [Description("What should the normal Tesla Timout Broadcast show when the gates are being unlocked?")]
        public string TeslaTimeoutEndingBroadcast = "<b>The tesla gates have no longer a timeout!</b>";

        [Description("What should the normal Tesla Timout C.A.S.S.I.E announcement show?")]
        public string TeslaTimeoutEndingCassie = "The facility tesla gate. are no longer disabled";

        [Description("What should the normal Broadcast duration be?")]
        public ushort BroadcastDuration = 5;

        [Description("What is the chance for a Gatelockdown?")]
        public int GatelockdownChance = 15;

        [Description("What is intervall in which the chance is being checked, to cause a random gatelockdown?")]
        public int GatelockdownIntervall = 90;

        [Description("What is the minimum duration for a Gatelockdown?")]
        public int GatelockdownMinDuration = 45;

        [Description("What is the maximum duration for a Gatelockdown?")]
        public int GatelockdownMaxDuration = 180;

        [Description("What is the minimum duration for a Tesla gate Timeout?")]
        public int TeslaTimeoutMinDuration = 45;

        [Description("What is the maximum duration for a Tesla gate Timeout?")]
        public int TeslaTimeoutMaxDuration = 180;
    }
}
