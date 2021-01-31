using Interactables.Interobjects.DoorUtils;
using MapControl.Commands;
using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapControl.Handlers
{
    public class EventHandlers
    {
        public static List<CoroutineHandle> Coroutines = new List<CoroutineHandle>();
        public static bool IsGateLocked = false;
        public EventHandlers()
        {
            Server.Get.Events.Map.TriggerTeslaEvent += OnTeslaInteract;
            Server.Get.Events.Round.RoundStartEvent += OnRoundBegin;
            Server.Get.Events.Round.RoundEndEvent += OnRoundEnd;
            Server.Get.Events.Round.RoundRestartEvent += OnRoundRestart;
        }

        public void OnTeslaInteract(Synapse.Api.Events.SynapseEventArguments.TriggerTeslaEventArgs ev)
        {
            if(Plugin.Config.IsEnabled)
            {
                if (!Plugin.Config.TeslaGatesEnabled)
                    ev.Trigger = false;

                if (Plugin.Config.TeslaBypassClasses.Contains(ev.Player.RoleID))
                    ev.Trigger = false;

                foreach (SynapseItem items in ev.Player.Inventory.Items.ToArray())
                    if (Plugin.Config.TeslaBypassItems.Contains(items.ID))
                        ev.Trigger = false;
                    
                if (!teslaGateDisable.TeslaState)
                    ev.Trigger = false;
            }
        }

        public void OnRoundBegin()
        {
            if (Plugin.Config.IsEnabled && Plugin.Config.RoundStartGatelockdown)
                Timing.CallDelayed(Plugin.Config.RoundStartGatelockdownDelay, () => RoundStartGatelockdown());
            else if (Plugin.Config.IsEnabled && !Plugin.Config.RoundStartGatelockdown)
                Coroutines.Add(Timing.RunCoroutine(randomGatelockdown()));

            if (Plugin.Config.RoundStartGatelockdown && Plugin.Config.RandomGatelockdowns)
                Timing.CallDelayed(Plugin.Config.RoundStartGatelockdownDuration, () => Coroutines.Add(Timing.RunCoroutine(randomGatelockdown())));
        }

        public void OnRoundEnd() => Timing.KillCoroutines(Coroutines.ToArray());


        public void OnRoundRestart() => Timing.KillCoroutines(Coroutines.ToArray());



        public static void RoundStartGatelockdown()
        {
            if (Plugin.Config.GatelockdownBroadcastEnabled)
                Map.Get.SendBroadcast(Plugin.Config.BroadcastDuration, Plugin.Config.GatelockdownBroadcast);

            if (Plugin.Config.GatelockdownCassieEnabled)
                Map.Get.Cassie(Plugin.Config.GatelockdownCassie);

            IsGateLocked = true;
            DoorVariant gateA = DoorNametagExtension.NamedDoors["GATE_A"].TargetDoor;
            gateA.NetworkTargetState = false;
            gateA.NetworkActiveLocks = 1;

            DoorVariant gateB = DoorNametagExtension.NamedDoors["GATE_B"].TargetDoor;
            gateB.NetworkTargetState = false;
            gateB.NetworkActiveLocks = 1;


            int time = (int) Plugin.Config.RoundStartGatelockdownDuration;
            Coroutines.Add(Timing.RunCoroutine(gateUnlock(time)));
        }

        public static IEnumerator<float> randomGatelockdown()
        {
            Random r = new Random();
            while (true)
            {
                yield return Timing.WaitForSeconds(Plugin.Config.GatelockdownIntervall);
                int chance = r.Next(Plugin.Config.GatelockdownChance);
                if(chance <= Plugin.Config.GatelockdownChance)
                {
                    if (Plugin.Config.GatelockdownBroadcastEnabled)
                        Map.Get.SendBroadcast(Plugin.Config.BroadcastDuration, Plugin.Config.GatelockdownBroadcast);

                    if (Plugin.Config.GatelockdownCassieEnabled)
                        Map.Get.Cassie(Plugin.Config.GatelockdownCassie);

                    IsGateLocked = true;
                    DoorVariant gateA = DoorNametagExtension.NamedDoors["GATE_A"].TargetDoor;
                    gateA.NetworkTargetState = false;
                    gateA.NetworkActiveLocks = 1;

                    DoorVariant gateB = DoorNametagExtension.NamedDoors["GATE_B"].TargetDoor;
                    gateB.NetworkTargetState = false;
                    gateB.NetworkActiveLocks = 1;

                    int time = UnityEngine.Random.Range(Plugin.Config.GatelockdownMinDuration, Plugin.Config.GatelockdownMaxDuration);
                    Coroutines.Add(Timing.RunCoroutine(gateUnlock(time)));
                }
                    yield return Timing.WaitForOneFrame;
            }
        }

        public static IEnumerator<float> gateUnlock(int time)
        {
            yield return Timing.WaitForSeconds(time);
            IsGateLocked = false;
            DoorVariant gateA = DoorNametagExtension.NamedDoors["GATE_A"].TargetDoor;
            DoorVariant gateB = DoorNametagExtension.NamedDoors["GATE_B"].TargetDoor;
            gateA.NetworkTargetState = true;
            gateA.NetworkActiveLocks = 0;

            gateB.NetworkTargetState = true;
            gateB.NetworkActiveLocks = 0;

            if (Plugin.Config.GatelockdownBroadcastEnabled)
                Map.Get.SendBroadcast(Plugin.Config.BroadcastDuration, Plugin.Config.GatelockdownEndingBroadcast);

            if (Plugin.Config.GatelockdownCassieEnabled)
                Map.Get.Cassie(Plugin.Config.GatelockdownEndingCassie);
        }

    }
    
}
