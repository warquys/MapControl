# MapControl
Control your map!

[Original Plugin](https://github.com/TheVoidNebula/MapControl)

[![forthebadge](https://forthebadge.com/images/badges/built-with-love.svg)](https://forthebadge.com)[![forthebadge](https://forthebadge.com/images/badges/made-with-c-sharp.svg)](https://forthebadge.com)[![forthebadge](https://forthebadge.com/images/badges/you-didnt-ask-for-this.svg)](https://forthebadge.com)

## Features
* Decide which classes do not trigger tesla gates
* Decide if tesla gates are generally enabled
* Have random tesla gate timeouts
* Have random gate lockdowns (which lock both Gate-A and Gate-B)
* Lock the Gates via commands

## Showcase

![Tesla gate Disabled](/assets/tesladisable.png)
![Gatelockdown](/assets/gatelockdown1.png)
![Tesla gate Disabled](/assets/gatelockdown2.png)

## Installation
1. [Install Synapse](https://github.com/SynapseSL/Synapse/wiki#hosting-guides)
2. Place the MapControl.dll file that you can download [here](https://github.com/TheVoidNebula/MapControl/releases) in your plugin directory
3. Restart/Start your server.

## Commands
Command  | Usage | Aliases | Permission | Description
------------ | ------------ | ------------- | ------------ | ------------ 
`gatelockdown` | `gatelockdown` `<yes/no>` `<time>` | `gl` | `mc.gatelockdown` | With this command you can lock/unlock gates yourself. `<yes/no>` determines if you want that a broadcast appears to everyone and C.A.S.S.I.E announces it. `time` allows you to lock/unlock the gates for a certain amount of time.
`teslagatedisable` | `teslagatedisable` | `tgd` | `mc.teslagatedisable` | This command allows you to enable/disable all tesla gates on the map.

## Config
Name  | Type | Default | Description
------------ | ------------ | ------------- | ------------ 
`IsEnabled` | Boolean | true | Is this plugin enabled?
`TeslaGatesEnabled` | Boolean | true | Are tesla gates enabled?
`RandomTeslaTimeouts` | Boolean | true | Should random tesla gate timeouts happen?
`RandomGatelockdowns` | Boolean | true | Should Gate A and Gate B sometimes be randomly locked?
`RoundStartGatelockdown` | Boolean | true | Should Gate A and Gate B be locked at the beginning of the round?
`TeslaBypassItems` | List | 11, 19 | Which classes should not trigger tesla gates? (RoleIDs)
`TeslaBypassClasses` | List | 4, 6, 11, 12, 13, 15 | Which items in the inventory of a player should not trigger tesla gates? (Item IDs)
`RoundStartGatelockdownDuration` | Float | 120f | How long should Gate A and Gate B be locked at the beginning of the round?
`RoundStartGatelockdownDelay` | Float | 10f | How long should the Gatelockdown at the beginning of the round should be delayed? (Important to prevent broadcast spam at the start of the round)
`GatelockdownBroadcastEnabled` | Boolean | true | Should a broadcast be shown when a Gatelockdown happens?
`GatelockdownCassieEnabled` | Boolean | true | Should a C.A.S.S.I.E announcement be shown when a Gatelockdown happens?
`TeslaTimeoutBroadcastEnabled` | Boolean | true | Should a broadcast be shown when a Tesla timeout happens?
`TeslaTimeoutCassieEnabled` | Boolean | true | Should a C.A.S.S.I.E announcement be shown when a Tesla timeout happens?
`GatelockdownBroadcast` | String | The gates to surface have been locked down! | What should the normal Gatelockdown Broadcast show?
`GatelockdownCassie` | String | The gates to surface have been locked down | What should the normal Gatelockdown C.A.S.S.I.E announcement show?
`GatelockdownEndingBroadcast` | String | The gates to surface are no longer locked down! | What should the normal Gatelockdown Broadcast show when the gates are being unlocked?
`GatelockdownEndingCassie` | String | The gates to surface are no longer locked down | What should the normal Gatelockdown C.A.S.S.I.E announcement show?
`TeslaTimeoutBroadcast` | String | The tesla gates have a timeout! | What should the normal Tesla Timeout Broadcast show?
`TeslaTimeoutCassie` | String | The facility tesla gate. are disabled | What should the normal Tesla Timout C.A.S.S.I.E announcement show?
`TeslaTimeoutEndingBroadcast` | String | The tesla gates have no longer a timeout! | What should the normal Tesla Timout Broadcast show when the gates are being unlocked?
`TeslaTimeoutEndingCassie` | String | The facility tesla gate. are no longer disabled | What should the normal Tesla Timout C.A.S.S.I.E announcement show?
`BroadcastDuration` | uShort | 5 | What should the normal Broadcast duration be?
`GatelockdownChance` | Int | 15 | What is the chance for a Gatelockdown?
`GatelockdownIntervall` | Int | 90 | What is intervall in which the chance is being checked, to cause a random gatelockdown?
`GatelockdownMinDuration` | Int | 45 | What is the minimum duration for a Gatelockdown?
`GatelockdownMaxDuration` | Int | 180 | What is the maximum duration for a Gatelockdown?
`TeslaTimeoutMinDuration` | Int | 45 | What is the minimum duration for a Tesla gate Timeout?
`TeslaTimeoutMaxDuration` | Int | 180 | What is the maximum duration for a Tesla gate Timeout?

## Config.yml
```yml
[MapControl]
{
# Is this plugin enabled?
isEnabled: true
# Are tesla gates enabled?
teslaGatesEnabled: true
# Should random tesla gate timeouts happen?
randomTeslaTimeouts: true
# Should Gate A and Gate B sometimes be randomly locked?
randomGatelockdowns: true
# Should Gate A and Gate B be locked at the beginning of the round?
roundStartGatelockdown: true
# Should Tesla gates be disabled if the player has a certain item in his inventory?
teslaGateBypassItemsEnabled: true
# Which items in the inventory of a player should not trigger tesla gates?
teslaBypassItems:
- 11
- 19
# Which classes should not trigger tesla gates?
teslaBypassClasses:
- 4
- 6
- 11
- 12
- 13
- 15
# How long should Gate A and Gate B be locked at the beginning of the round?
roundStartGatelockdownDuration: 120
# How long should the Gatelockdown at the beginning of the round should be delayed? (Important to prevent broadcast spam at the start of the round)
roundStartGatelockdownDelay: 10
# Should a broadcast be shown when a Gatelockdown happens?
gatelockdownBroadcastEnabled: true
# Should a C.A.S.S.I.E announcement be shown when a Gatelockdown happens?
gatelockdownCassieEnabled: true
# Should a broadcast be shown when a Tesla timeout happens?
teslaTimeoutBroadcastEnabled: true
# Should a C.A.S.S.I.E announcement be shown when a Tesla timeout happens?
teslaTimeoutCassieEnabled: true
# What should the normal Gatelockdown Broadcast show?
gatelockdownBroadcast: <b>The gates to surface have been locked down!</b>
# What should the normal Gatelockdown C.A.S.S.I.E announcement show?
gatelockdownCassie: The gates to surface have been locked down
# What should the normal Gatelockdown Broadcast show when the gates are being unlocked?
gatelockdownEndingBroadcast: <b>The gates to surface are no longer locked down!</b>
# What should the normal Gatelockdown C.A.S.S.I.E announcement show?
gatelockdownEndingCassie: The gates to surface are no longer locked down
# What should the normal Tesla Timeout Broadcast show?
teslaTimeoutBroadcast: <b>The tesla gates have a timeout!</b>
# What should the normal Tesla Timout C.A.S.S.I.E announcement show?
teslaTimeoutCassie: The facility tesla gate. are disabled
# What should the normal Tesla Timout Broadcast show when the gates are being unlocked?
teslaTimeoutEndingBroadcast: <b>The tesla gates have no longer a timeout!</b>
# What should the normal Tesla Timout C.A.S.S.I.E announcement show?
teslaTimeoutEndingCassie: The facility tesla gate. are no longer disabled
# What should the normal Broadcast duration be?
broadcastDuration: 5
# What is the chance for a Gatelockdown?
gatelockdownChance: 15
# What is intervall in which the chance is being checked, to cause a random gatelockdown?
gatelockdownIntervall: 90
# What is the minimum duration for a Gatelockdown?
gatelockdownMinDuration: 45
# What is the maximum duration for a Gatelockdown?
gatelockdownMaxDuration: 180
# What is the minimum duration for a Tesla gate Timeout?
teslaTimeoutMinDuration: 45
# What is the maximum duration for a Tesla gate Timeout?
teslaTimeoutMaxDuration: 180
}
```
