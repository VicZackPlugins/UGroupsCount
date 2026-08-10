using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using VicZackPlugins.UGroupsCount.Managers;

namespace VicZackPlugins.UGroupsCount
{
    public class UGroupsCountPlugin : RocketPlugin<UGroupsCountConfiguration>
    {
        public static UGroupsCountPlugin Instance { get; private set; }
        public string PLUGIN_VERSION => "1.0.0";
        protected override void Load()
        {
            Instance = this;
            
            Logger.Log(" ");
            Logger.Log("---------------------------------------");
            Logger.Log("------[VZ_UGroups Count Plugin]--------");
            Logger.Log("--------[Successfully Loaded]----------");
            Logger.Log("-----------[By: Vic Zack]--------------");
            Logger.Log("--------[Discord: elbistec01]----------");
            Logger.Log($"----------[Version: {PLUGIN_VERSION}]-------------");
            Logger.Log("---------------------------------------");
            Logger.Log(" ");

            CountSystemManager.Initialize();

            U.Events.OnPlayerConnected += OnPlayerConnected;
            U.Events.OnPlayerDisconnected += OnPlayerDisconnected;

        }

        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= OnPlayerConnected;
            U.Events.OnPlayerDisconnected -= OnPlayerDisconnected;

            CountSystemManager.Shutdown();

            Logger.Log("[UGC] Plugin unloaded.");
        
        }

        private void OnPlayerConnected(UnturnedPlayer player)
        {
            var connection = player.SteamPlayer().transportConnection;
            var config = UGroupsCountPlugin.Instance.Configuration.Instance;

            CountSystemManager.ActivePlayerUI(player);

            CountSystemManager.IsInGroup(player, true);
            CountSystemManager.UpdateAllClients();

        }

        private void OnPlayerDisconnected(UnturnedPlayer player)
        {
            var connection = player.SteamPlayer().transportConnection;
            var config = UGroupsCountPlugin.Instance.Configuration.Instance;

            CountSystemManager.IsInGroup(player, false);
            CountSystemManager.UpdateAllClients();
        }
    }
}