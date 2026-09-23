using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Utils;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Events;
using CounterStrikeSharp.API.Modules.Timers;

namespace Hit_99;

public class Hit99Config : BasePluginConfig
{
    public float AutoMessageTimer { get; set; } = 90.0f;

    public List<string> AutoMessages { get; set; } = new()
    {
        "{GREY}Join our Discord Community: {GREEN}https://discord.hit99.pro",
        "{GREY}View a list of commands: {GREEN}!help",
        "{GREY}Add Hit-99 to favorites: {GREEN}cs.hit99.pro:26448"
    };

    public List<string> JoinMessages { get; set; } = new()
    {
        " ",
        "Welcome to {RED}Hit-99 {WHITE}Community Competitive {GREEN}{PLAYER}",
        "Join our Discord: {GREEN}https://discord.hit99.pro",
        "Type {GREEN}!help {WHITE}for commands",
        " "
    };

    public string DiscordLink { get; set; } = "https://discord.hit99.pro";
    public string FluxerLink { get; set; } = "https://fluxer.hit99.pro";
    public string SteamLink { get; set; } = "https://steam.hit99.pro";
    public string FaceitLink { get; set; } = "https://faceit.hit99.pro";

    public string JoinCountMessage { get; set; } = "{GREEN}{PLAYER}{WHITE} joined {RED}Hit-99{WHITE} for the {GREEN}{JOINS}{WHITE} time!";
}
public class Hit_99 : BasePlugin, IPluginConfig<Hit99Config>
{
    public Hit99Config Config { get; set; } = new();

    private const string Version = "0.5.2";
    public override string ModuleName => "Hit-99 Plugin";
    public override string ModuleVersion => Version;
    public override string ModuleAuthor => "hyper";
    public override string ModuleDescription => "https://hit99.pro";

    public string PluginPrefix = $"[{ChatColors.Red}Hit-99{ChatColors.White}] ";
    public string ConsolePluginPrefix = $"[Hit-99 v{Version}] ";

    public void OnConfigParsed(Hit99Config config){Config = config;}

    private int CurrentMessageIndex = 0;
    
    public override void Load(bool hotReload)
    {
        Console.WriteLine($@"
   ___     ___  ___  _________             _______  _______
  |   |   |   ||   ||         |           |       ||       |
  |   |___|   ||   ||__     __|  _______  |    _  ||    _  |
  |           ||   |   |   |    |       | |   |_| ||   |_| |
  |    ___    ||   |   |   |    |_______| |____   ||____   |
  |   |   |   ||   |   |   |               ____|  | ____|  |
  |___|   |___||___|   |___|              |_______||_______|

           https://github.com/Hyp3r7/Hit-99-Plugin.git
                            v{Version}
");

        AddTimer(Config.AutoMessageTimer, BroadcastAutoMessage, TimerFlags.REPEAT);
    }

    private string FormatMessage(string message, CCSPlayerController? player, int? joinCount = null)
    {
    return message
        .Replace("{RED}", ChatColors.Red.ToString())
        .Replace("{GREEN}", ChatColors.Green.ToString())
        .Replace("{WHITE}", ChatColors.White.ToString())
        .Replace("{GREY}", ChatColors.Grey.ToString())
        .Replace("{PLAYER}", player?.PlayerName ?? "")
        .Replace("{JOINS}", joinCount?.ToString() ?? "");
    }

    // AUTO MESSAGE

    private void BroadcastAutoMessage()
    {
        if (Config.AutoMessages.Count == 0)
            return;

        if (Utilities.GetPlayers().Count == 0)
            return;

        string raw = Config.AutoMessages[CurrentMessageIndex];
        string message = FormatMessage(raw, null);

        Server.PrintToChatAll($"{PluginPrefix}{message}");

        CurrentMessageIndex++;

        if (CurrentMessageIndex >= Config.AutoMessages.Count)
            CurrentMessageIndex = 0;
    }

    // JOIN MESSAGE

    [GameEventHandler]
    public HookResult OnPlayerConnectFull(EventPlayerConnectFull @event, GameEventInfo info)
    {
        CCSPlayerController? player = @event.Userid;

        if (player == null || !player.IsValid)
            return HookResult.Continue;

        // NORMAL JOIN MESSAGE

        foreach (var raw in Config.JoinMessages)
        {
            player.PrintToChat(FormatMessage(raw, player));
        }

        return HookResult.Continue;
    }

    // COMMANDS

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

    [ConsoleCommand("help", "Replies list of commands")]
    public void OnHelpCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (player == null)
        {
            return;
        }

        player.PrintToChat($"{PluginPrefix}Commands: !discord !fluxer !rtv");
    }

    [ConsoleCommand("rules", "Replies list of rules")]
    public void OnRulesCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (player == null)
        {
            return;
        }

        player.PrintToChat($"{PluginPrefix}Rules: Have fun!");
    }

    [ConsoleCommand("discord", "Replies with discord link")]
    public void OnDiscordCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (player == null)
        {
            return;
        }

        player.PrintToChat($"{PluginPrefix}Join the Hit-99 Discord Community: {ChatColors.Green}{Config.DiscordLink}");
    }

    [ConsoleCommand("fluxer", "Replies with fluxer link")]
    public void OnFluxerCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (player == null)
        {
            return;
        }

        player.PrintToChat($"{PluginPrefix}Join the Hit-99 Fluxer Community: {ChatColors.Green}{Config.FluxerLink}");
    }

    [ConsoleCommand("steam", "Replies with steam link")]
    public void OnSteamCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (player == null)
        {
            return;
        }

        player.PrintToChat($"{PluginPrefix}Join the Hit-99 Stean Community: {ChatColors.Green}{Config.SteamLink}");
    }

    [ConsoleCommand("faceit", "Replies with faceit link")]
    public void OnFaceitCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (player == null)
        {
            return;
        }

        player.PrintToChat($"{PluginPrefix}Join the Hit-99 Faceit Club: {ChatColors.Green}{Config.FaceitLink}");
    }
}

/*
STATS TO TRACK

kills
deaths
assists
HS%
total damage

MVPs
utility damage
enemies flashed
KDR
ADR

playtime
map win/loss

post game data

number of joins
first join

*/

/*

OTHER STUFF

scoreboard

match history

*/