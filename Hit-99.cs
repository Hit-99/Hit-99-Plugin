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

    private const string Version = "0.0.2";
    public override string ModuleName => "Hit-99 Plugin";
    public override string ModuleVersion => Version;
    public override string ModuleAuthor => "hyper";
    public override string ModuleDescription => "https://hit99.pro";


    public string PluginPrefix = $"{ChatColors.Grey}[{ChatColors.Red}Hit-99{ChatColors.Grey}] {ChatColors.White}";
    public string ConsolePluginPrefix = $"[Hit-99 v{Version}] ";
    
    public override void Load(bool hotReload)
    {
        Console.WriteLine($"{ConsolePluginPrefix}Plugin loaded!");
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

    [ConsoleCommand("discord", "Replies with discord link")]
    public void OnDiscordCommand(CCSPlayerController? player, CommandInfo command)
    {
        string discordLink = "https://discord.hit99.pro";

        if (player == null)
        {
            return;
        }

        player.PrintToChat($"{PluginPrefix}Join the Hit-99 Discord Community: {ChatColors.Blue}{discordLink}");
    }

    [ConsoleCommand("fluxer", "Replies with fluxer link")]
    public void OnFluxerCommand(CCSPlayerController? player, CommandInfo command)
    {
        string fluxerLink = "https://fluxer.hit99.pro";

        if (player == null)
        {
            return;
        }

        player.PrintToChat($"{PluginPrefix}Join the Hit-99 Fluxer Community: {ChatColors.Blue}{fluxerLink}");
    }
}
