using Interactables.Interobjects.DoorUtils;
using MapControl.Commands;
using MEC;
using Synapse;
using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapControl.Handlers
{
    public class EventHandlers
    {
        public static List<CoroutineHandle> coroutines = new List<CoroutineHandle>();
        public static bool isGateLocked = false;
        public EventHandlers()
        {
            Server.Get.Events.Map.TriggerTeslaEvent += onTeslaInteract;
            Server.Get.Events.Round.RoundStartEvent += onRoundBegin;
            Server.Get.Events.Round.RoundEndEvent += onRoundEnd;
            Server.Get.Events.Round.RoundRestartEvent += onRoundRestart;
        }

        public void onTeslaInteract(Synapse.Api.Events.SynapseEventArguments.TriggerTeslaEventArgs ev)
        {
            if(Plugin.Config.isEnabled)
            {
                if (Plugin.Config.teslaGatesEnabled == false)
                    ev.Trigger = false;

                if (Plugin.Config.teslaBypassClasses.Contains(ev.Player.RoleID))
                    ev.Trigger = false;

                if (teslaGateDisable.teslaState == false)
                    ev.Trigger = false;
            }
        }

        public void onRoundBegin()
        {
            if (Plugin.Config.isEnabled && Plugin.Config.roundStartGatelockdown)
                Timing.CallDelayed(Plugin.Config.roundStartGatelockdownDelay, () => roundStartGatelockdown());
            else if (Plugin.Config.isEnabled && Plugin.Config.roundStartGatelockdown == false)
                coroutines.Add(Timing.RunCoroutine(randomGatelockdown()));

            if (Plugin.Config.roundStartGatelockdown && Plugin.Config.randomGatelockdowns)
                Timing.CallDelayed(Plugin.Config.roundStartGatelockdownDuration, () => coroutines.Add(Timing.RunCoroutine(randomGatelockdown())));
        }

        public void onRoundEnd() => Timing.KillCoroutines(coroutines.ToArray());


        public void onRoundRestart() => Timing.KillCoroutines(coroutines.ToArray());



        public static void roundStartGatelockdown()
        {
            if (Plugin.Config.GatelockdownBroadcastEnabled)
                Map.Get.SendBroadcast(Plugin.Config.BroadcastDuration, Plugin.Config.GatelockdownBroadcast);

            if (Plugin.Config.GatelockdownCassieEnabled)
                Map.Get.Cassie(Plugin.Config.GatelockdownCassie);

            isGateLocked = true;
            DoorVariant gateA = DoorNametagExtension.NamedDoors["GATE_A"].TargetDoor;
            gateA.NetworkTargetState = false;
            gateA.NetworkActiveLocks = 1;

            DoorVariant gateB = DoorNametagExtension.NamedDoors["GATE_B"].TargetDoor;
            gateB.NetworkTargetState = false;
            gateB.NetworkActiveLocks = 1;


            int time = (int) Plugin.Config.roundStartGatelockdownDuration;
            coroutines.Add(Timing.RunCoroutine(gateUnlock(time)));
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

                    isGateLocked = true;
                    DoorVariant gateA = DoorNametagExtension.NamedDoors["GATE_A"].TargetDoor;
                    gateA.NetworkTargetState = false;
                    gateA.NetworkActiveLocks = 1;

                    DoorVariant gateB = DoorNametagExtension.NamedDoors["GATE_B"].TargetDoor;
                    gateB.NetworkTargetState = false;
                    gateB.NetworkActiveLocks = 1;

                    int time = UnityEngine.Random.Range(Plugin.Config.GatelockdownMinDuration, Plugin.Config.GatelockdownMaxDuration);
                    coroutines.Add(Timing.RunCoroutine(gateUnlock(time)));
                }
                    yield return Timing.WaitForOneFrame;
            }
        }

        public static IEnumerator<float> gateUnlock(int time)
        {
            yield return Timing.WaitForSeconds(time);
            isGateLocked = false;
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
