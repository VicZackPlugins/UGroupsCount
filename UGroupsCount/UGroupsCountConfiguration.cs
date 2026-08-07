using Rocket.API;
using System.Collections.Generic;
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
        public int directionMode { get; set; }

        // Show the name of group
        public bool showName { get; set; }

        // Show de count of group
        public bool showCount { get; set; }

        // Show group list
        public List<GroupSlotConfig> Slots { get; set; }

        public void LoadDefaults()
        {

            UI_ID = 46135;
            UI_KEY = 6135;

            directionMode = 0;

            showName = true;
            showCount = true;

            Slots = new List<GroupSlotConfig>
            {
                new GroupSlotConfig { SlotIndex = 0, DisplayName = "Grupo 1", Icon = 100, GroupId = "grupo1" },
                new GroupSlotConfig { SlotIndex = 1, DisplayName = "Grupo 2", Icon = 101, GroupId = "grupo2" },
                new GroupSlotConfig { SlotIndex = 2, DisplayName = "Grupo 3", Icon = 102, GroupId = "grupo3" },
                new GroupSlotConfig { SlotIndex = 3, DisplayName = "Grupo 4", Icon = 103, GroupId = "grupo4" },
                new GroupSlotConfig { SlotIndex = 4, DisplayName = "Grupo 5", Icon = 104, GroupId = "grupo5" },
                new GroupSlotConfig { SlotIndex = 5, DisplayName = "Grupo 6", Icon = 105, GroupId = "grupo6" },
            };

        }
    }

    public class GroupSlotConfig
    {
        public int SlotIndex { get; set; }
        public string DisplayName { get; set; }
        public ushort Icon { get; set; }
        public string GroupId { get; set; }
        public bool Enabled { get; set; } = true;
    }
}
