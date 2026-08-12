using Rocket.API.Serialisation;
using Rocket.Core;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static UnityEngine.Scripting.GarbageCollector;

namespace VicZackPlugins.UGroupsCount.Managers
{
    public class CountSystemManager
    {
        public static HashSet<CSteamID> PlayersActiveUI { get; private set; } = new HashSet<CSteamID>();
        public static Dictionary<int, int> GroupsCount { get; private set; } = new Dictionary<int, int>();

        public static void Initialize()
        {
            for (int i = 0; i <= 6; i++)
            {
                GroupsCount.Add(i, 0);
            }

            foreach (SteamPlayer client in Provider.clients)
            {
                UnturnedPlayer player = UnturnedPlayer.FromSteamPlayer(client);

                PlayersActiveUI.Add(player.CSteamID);
                IsInGroup(player, true);
            }

            if (PlayersActiveUI.Count > 0)
            {
                UpdateAllClients();
            }

        }

        public static void Shutdown()
        {
            GroupsCount.Clear();
            PlayersActiveUI.Clear();
        }

        public static void ActivePlayerUI(UnturnedPlayer player)
        {
            PlayersActiveUI.Add(player.CSteamID);
        }

        public static void DisablePlayerUI(UnturnedPlayer player)
        {
            PlayersActiveUI.Remove(player.CSteamID);
        }

        public static void UpdateAllClients()
        {
            var plugin = UGroupsCountPlugin.Instance;
            var config = plugin.Configuration.Instance;
            string mode;

            if (config.directionMode.Equals("horizontal", StringComparison.OrdinalIgnoreCase)) mode = "h";
            else mode = "v";

            foreach (SteamPlayer client in Provider.clients)
            {
                UnturnedPlayer player = UnturnedPlayer.FromSteamPlayer(client);

                // Send UI to player
                if (PlayersActiveUI.Contains(player.CSteamID))
                {
                    var connection = client.transportConnection;

                    EffectManager.askEffectClearByID(config.UI_ID, connection);
                    EffectManager.sendUIEffect(config.UI_ID, config.UI_KEY, connection, true);
                    EffectManager.sendUIEffectVisibility(config.UI_KEY, connection, true, $"{mode}MaskUI", true);

                    for (int i = 0; i <= 6; i++)
                    {
                        GroupSlotConfig slot = GetSlot(i);
                        if (!slot.Enabled) continue;

                        EffectManager.sendUIEffectVisibility(config.UI_KEY, connection, true, $"{mode}Img_Slot{i}", true);

                        if (config.showName) EffectManager.sendUIEffectText(config.UI_KEY, connection, true, $"{mode}Txt_Name_Slot{i}", slot.DisplayName);
                        else EffectManager.sendUIEffectText(config.UI_KEY, connection, true, $"{mode}Txt_Name_Slot{i}", "");

                        if (slot.Icon != null) EffectManager.sendUIEffectImageURL(config.UI_KEY, connection, true, $"{mode}Img_Slot{i}", slot.Icon);

                        int count = GroupsCount[i];
                        if (count < 0) count = 0;
                        EffectManager.sendUIEffectText(config.UI_KEY, connection, true, $"{mode}Txt_Online_Slot{i}", count.ToString());
                    }

                }
            }
        }

        public static void IsInGroup(UnturnedPlayer player, bool isJoined)
        {
            var config = UGroupsCountPlugin.Instance.Configuration.Instance;

            List<RocketPermissionsGroup> Groups = R.Permissions.GetGroups(player, true);

            for (int i = 0; i <= 6; i++)
            {
                GroupSlotConfig slot = GetSlot(i);
                if (!slot.Enabled) continue;

                string groupId = slot.GroupId;

                foreach (RocketPermissionsGroup group in Groups.OrderBy(n => n.Priority))
                {
                    if (group.Id.Equals(groupId, StringComparison.OrdinalIgnoreCase))
                    {
                        if (isJoined) GroupsCount[i] += 1;
                        else GroupsCount[i] -= 1;

                        if (config.oneGroupPerPlayer) return;
                    }
                }
            }
        }

        public static GroupSlotConfig GetSlot(int index)
        {
            var config = UGroupsCountPlugin.Instance.Configuration.Instance;
            var slot = config.Slots?.FirstOrDefault(s => s.SlotIndex == index);

            if (slot == null)
            {

                return new GroupSlotConfig
                {
                    SlotIndex = index,
                    DisplayName = "N/A",
                    Icon = null,
                    GroupId = null,
                    Enabled = false
                };
            }

            return slot;
        }
    }
}
