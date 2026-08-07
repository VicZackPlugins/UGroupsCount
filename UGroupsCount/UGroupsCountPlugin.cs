using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Events;

namespace VicZackPlugins.UGroupsCount
{
    public class UGroupsCountPlugin : RocketPlugin<UGroupsCountConfiguration>
    {
        public string PLUGIN_VERSION => "1.0.0";
        protected override void Load()
        {
            Logger.Log(" ");
            Logger.Log("---------------------------------------");
            Logger.Log("------[VZ_UGroups Count Plugin]--------");
            Logger.Log("--------[Successfully Loaded]----------");
            Logger.Log("-----------[By: Vic Zack]--------------");
            Logger.Log("--------[Discord: elbistec01]----------");
            Logger.Log($"----------[Version: {PLUGIN_VERSION}]-------------");
            Logger.Log("---------------------------------------");
            Logger.Log(" ");

            U.Events.OnPlayerConnected += ;
            U.Events.OnPlayerDisconnected += ;

        }

        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= ;
            U.Events.OnPlayerDisconnected -= ;

            Logger.Log("[UGC] Plugin unloaded.");
        }


    }
}