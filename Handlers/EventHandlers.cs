using MapControl.Commands;
using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Items;
using System;
using System.Collections.Generic;

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

                foreach (SynapseItem items in ev.Player.Inventory.Items)
                    if (Plugin.Config.TeslaBypassItems.Contains(items.ID))
                        ev.Trigger = false;

                if (!TeslaGateDisable.TeslaState)
                    ev.Trigger = false;
            }
        }

        public void OnRoundBegin()
        {
            if (Plugin.Config.IsEnabled)
            {

                if (Plugin.Config.RoundStartGatelockdown)
                    Timing.CallDelayed(Plugin.Config.RoundStartGatelockdownDelay, () => RoundStartGatelockdown());
                else if (Plugin.Config.IsEnabled && !Plugin.Config.RoundStartGatelockdown)
                    Coroutines.Add(Timing.RunCoroutine(RandomGatelockdown()));

                if (Plugin.Config.RoundStartGatelockdown && Plugin.Config.RandomGatelockdowns)
                    Timing.CallDelayed(Plugin.Config.RoundStartGatelockdownDuration, () => Coroutines.Add(Timing.RunCoroutine(RandomGatelockdown())));

            }
        }

        public void OnRoundEnd() => Timing.KillCoroutines(Coroutines.ToArray());


        public void OnRoundRestart() => Timing.KillCoroutines(Coroutines.ToArray());



        public static void RoundStartGatelockdown()
        {
            if (Plugin.Config.IsEnabled)
            {

                if (Plugin.Config.GatelockdownBroadcastEnabled)
                    Map.Get.SendBroadcast(Plugin.Config.BroadcastDuration, Plugin.PluginTranslation.ActiveTranslation.GatelockdownBroadcast);

                if (Plugin.Config.GatelockdownCassieEnabled)
                    Map.Get.Cassie(Plugin.Config.GatelockdownCassie);

                Plugin.LockGate();

                int time = (int)Plugin.Config.RoundStartGatelockdownDuration;
                Coroutines.Add(Timing.RunCoroutine(GateUnlock(time)));

            }
        }

        public static IEnumerator<float> RandomGatelockdown()
        {
            if (Plugin.Config.IsEnabled && Plugin.Config.RandomGatelockdowns)
            {
                Random r = new Random();
                while (true)
                {
                    yield return Timing.WaitForSeconds(Plugin.Config.GatelockdownIntervall);
                    int chance = r.Next(Plugin.Config.GatelockdownChance);
                    if (chance <= Plugin.Config.GatelockdownChance)
                    {
                        if (Plugin.Config.GatelockdownBroadcastEnabled)
                            Map.Get.SendBroadcast(Plugin.Config.BroadcastDuration, Plugin.PluginTranslation.ActiveTranslation.GatelockdownBroadcast);

                        if (Plugin.Config.GatelockdownCassieEnabled)
                            Map.Get.Cassie(Plugin.Config.GatelockdownCassie);

                        Plugin.LockGate();

                        int time = UnityEngine.Random.Range(Plugin.Config.GatelockdownMinDuration, Plugin.Config.GatelockdownMaxDuration);
                        Coroutines.Add(Timing.RunCoroutine(GateUnlock(time)));
                    }
                    yield return Timing.WaitForOneFrame;
                }
            }
        }

        public static IEnumerator<float> GateUnlock(int time)
        {
            yield return Timing.WaitForSeconds(time);

            Plugin.UnlockGate();

            if (Plugin.Config.GatelockdownBroadcastEnabled)
                Map.Get.SendBroadcast(Plugin.Config.BroadcastDuration, Plugin.PluginTranslation.ActiveTranslation.GatelockdownEndingBroadcast);

            if (Plugin.Config.GatelockdownCassieEnabled)
                Map.Get.Cassie(Plugin.Config.GatelockdownEndingCassie);
        }

    }
    
}
