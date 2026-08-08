using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VicZackPlugins.UGroupsCount
{
    public class UGroupsCountPlugin : RocketPlugin<UGroupsCountConfiguration>
    {
        public static UGroupsCountPlugin Instance { get; private set; }
        public HashSet<CSteamID> ActiveUI { get; private set; }
        public Dictionary<int, int> groupCount { get; private set; } = new Dictionary<int, int>();
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

            InitializeCount();

            U.Events.OnPlayerConnected += OnPlayerConnected;
            U.Events.OnPlayerDisconnected += OnPlayerDisconnected;

        }

        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= OnPlayerConnected;
            U.Events.OnPlayerDisconnected -= OnPlayerDisconnected;

            groupCount.Clear();

            Logger.Log("[UGC] Plugin unloaded.");
        
        }

        private void OnPlayerConnected(UnturnedPlayer player)
        {
            var connection = player.SteamPlayer().transportConnection;
            var config = UGroupsCountPlugin.Instance.Configuration.Instance;

            EffectManager.sendUIEffect(config.UI_ID, config.UI_KEY, connection, true);

            ActiveUI.Add(player.CSteamID);

            // Usar GroupSlotConfig en un bucle for y activar de a poco las opciones.

        }

        private void OnPlayerDisconnected(UnturnedPlayer player)
        {
            var connection = player.SteamPlayer().transportConnection;
            var config = UGroupsCountPlugin.Instance.Configuration.Instance;

            if (ActiveUI.Contains(player.CSteamID))
            {
                // Enviar actualizacion de UI
            }
        }

        private void InitializeCount()
        {
            for (int i = 0; i < 6; i++)
            {
                groupCount.Add(i, 0);
            }
        }

        public GroupSlotConfig GetSlot(int index)
        {
            var slot = Configuration.Instance.Slots?
                .FirstOrDefault(s => s.SlotIndex == index);

            if (slot == null)
            {
                
                return new GroupSlotConfig
                {
                    SlotIndex = index,
                    DisplayName = "N/A",
                    Icon = "",
                    GroupId = null,
                    Enabled = false
                };
            }

            return slot;
        }
    }
}