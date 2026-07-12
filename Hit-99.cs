using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Utils;
using CounterStrikeSharp.API.Modules.Commands;

namespace Hit_99;

public class Hit_99 : BasePlugin
{

    private const string Version = "0.0.1";
    public override string ModuleName => "Hit-99 Plugin";
    public override string ModuleVersion => Version;
    public override string ModuleAuthor => "hyper";
    public override string ModuleDescription => "https://hit99.pro";

    public string PluginPrefix = $"[Hit-99 v{Version}] ";
    
    public override void Load(bool hotReload)
    {
        Console.WriteLine($"{PluginPrefix}Plugin loaded!");
    }

    [ConsoleCommand("ping", "Replies with pong")]
    public void OnPingCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (player != null)
        {
            player.PrintToChat($"{PluginPrefix}pong");
        }
        else
        {
            Console.WriteLine("pong");
        }
    }
}
