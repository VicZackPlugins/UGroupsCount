using Rocket.API;
using System.Collections.Generic;
using System.Net;
using UnityEngine.Experimental.GlobalIllumination;

namespace VicZackPlugins.UGroupsCount
{
    public class UGroupsCountConfiguration : IRocketPluginConfiguration
    {
        // UI ID
        public ushort UI_ID { get; set; }

        // UI Key
        public short UI_KEY { get; set; }

        // Direction Mode
        public string directionMode { get; set; }

        // Show the name of group
        public bool showName { get; set; }

        // Count only one group per player
        public bool oneGroupPerPlayer { get; set; }

        // Show group list
        public List<GroupSlotConfig> Slots { get; set; }

        public void LoadDefaults()
        {

            UI_ID = 46135;
            UI_KEY = 6135;

            directionMode = "horizontal";

            showName = true;

            oneGroupPerPlayer = true;

            Slots = new List<GroupSlotConfig>
            {
                new GroupSlotConfig { SlotIndex = 0, DisplayName = "Civilian", GroupId = "default" },
                new GroupSlotConfig { SlotIndex = 1, DisplayName = "Mechanic", GroupId = "mechanic" },
                new GroupSlotConfig { SlotIndex = 2, DisplayName = "EMS", GroupId = "ems" },
                new GroupSlotConfig { SlotIndex = 3, DisplayName = "Police", GroupId = "police" },
                new GroupSlotConfig { SlotIndex = 4, DisplayName = "Military", GroupId = "military" },
                new GroupSlotConfig { SlotIndex = 5, DisplayName = "Staff", GroupId = "staff" },
                new GroupSlotConfig { SlotIndex = 6, DisplayName = "Admin", GroupId = "admin" },
            };

        }
    }

    public class GroupSlotConfig
    {
        public int SlotIndex { get; set; }
        public string DisplayName { get; set; }
        public string Icon { get; set; }
        public string GroupId { get; set; }
        public bool Enabled { get; set; } = true;
    }
}
