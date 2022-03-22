﻿using Synapse.Config;
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
        public bool IsEnabled { get; set; } = true;

        [Description("Are tesla gates enabled?")]
        public bool TeslaGatesEnabled { get; set; } = true;

        [Description("Should random tesla gate timeouts happen?")]
        public bool RandomTeslaTimeouts { get; set; } = true;

        [Description("Should Gate A and Gate B sometimes be randomly locked?")]
        public bool RandomGatelockdowns { get; set; } = true;

        [Description("Should Gate A and Gate B be locked at the beginning of the round?")]
        public bool RoundStartGatelockdown { get; set; } = true;

        [Description("What should the normal Gatelockdown C.A.S.S.I.E announcement show?")]
        public string GatelockdownCassie { get; set; } = "The gates to surface have been locked down";

        [Description("What should the normal Tesla Timout C.A.S.S.I.E announcement show?")]
        public string TeslaTimeoutCassie { get; set; } = "The facility tesla gate. are disabled";

        [Description("What should the normal Gatelockdown C.A.S.S.I.E announcement show?")]
        public string GatelockdownEndingCassie { get; set; } = "The gates to surface are no longer locked down";

        [Description("Should open Gate A and Gate B after a locked ?")]
        public bool GatelockdownOpen { get; set; } = true;

        [Description("Should Tesla gates be disabled if the player has a certain item in his inventory?")]
        public bool TeslaGateBypassItemsEnabled { get; set; } = true;

        [Description("Which items in the inventory of a player should not trigger tesla gates?")]
        public List<int> TeslaBypassItems { get; set; } = new List<int>()
        {
            11,
            19
        };

        [Description("Which classes should not trigger tesla gates?")]
        public List<int> TeslaBypassClasses { get; set; } = new List<int>()
        {
            4,
            6,
            11,
            12,
            13,
            15
        };

        [Description("How long should Gate A and Gate B be locked at the beginning of the round?")]
        public float RoundStartGatelockdownDuration { get; set; } = 120f;

        [Description("How long should the Gatelockdown at the beginning of the round should be delayed? (Important to prevent broadcast spam at the start of the round)")]
        public float RoundStartGatelockdownDelay { get; set; } = 10f;

        [Description("Should a broadcast be shown when a Gatelockdown happens?")]
        public bool GatelockdownBroadcastEnabled { get; set; } = true;

        [Description("Should a C.A.S.S.I.E announcement be shown when a Gatelockdown happens?")]
        public bool GatelockdownCassieEnabled { get; set; } = true;

        [Description("Should a broadcast be shown when a Tesla timeout happens?")]
        public bool TeslaTimeoutBroadcastEnabled { get; set; } = true;

        [Description("Should a C.A.S.S.I.E announcement be shown when a Tesla timeout happens?")]
        public bool TeslaTimeoutCassieEnabled { get; set; } = true;

        [Description("What should the normal Tesla Timout C.A.S.S.I.E announcement show?")]
        public string TeslaTimeoutEndingCassie { get; set; } = "The facility tesla gate. are no longer disabled";

        [Description("What should the normal Broadcast duration be?")]
        public ushort BroadcastDuration { get; set; } = 5;

        [Description("What is the chance for a Gatelockdown?")]
        public int GatelockdownChance { get; set; } = 15;

        [Description("What is intervall in which the chance is being checked, to cause a random gatelockdown?")]
        public int GatelockdownIntervall { get; set; } = 90;

        [Description("What is the minimum duration for a Gatelockdown?")]
        public int GatelockdownMinDuration { get; set; } = 45;

        [Description("What is the maximum duration for a Gatelockdown?")]
        public int GatelockdownMaxDuration { get; set; } = 180;

        [Description("What is the minimum duration for a Tesla gate Timeout?")]
        public int TeslaTimeoutMinDuration { get; set; } = 45;

        [Description("What is the maximum duration for a Tesla gate Timeout?")]
        public int TeslaTimeoutMaxDuration { get; set; } = 180;
    }
}
