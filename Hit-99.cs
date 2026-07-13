using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Utils;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Events;

namespace Hit_99;

public class Hit_99 : BasePlugin
{

    private const string Version = "0.0.4";
    public override string ModuleName => "Hit-99 Plugin";
    public override string ModuleVersion => Version;
    public override string ModuleAuthor => "hyper";
    public override string ModuleDescription => "https://hit99.pro";

    public string PluginPrefix = $"[{ChatColors.Red}Hit-99{ChatColors.White}] ";
    public string ConsolePluginPrefix = $"[Hit-99 v{Version}] ";

    public string discordLink = "https://discord.hit99.pro";
    public string fluxerLink = "https://fluxer.hit99.pro";

    public float autoMessageTimer = 60.0f;

    private List<string> AutoMessages = new()
    {
        $"{ChatColors.Grey}Join our Discord Community: {ChatColors.Green}https://discord.hit99.pro",
        $"{ChatColors.Grey}View a list of commands: {ChatColors.Green}!help",
        $"{ChatColors.Grey}Add Hit-99 to favorites: {ChatColors.Green}cs.hit99.pro:26448",
        $"{ChatColors.Grey}Give feedback or report bugs: {ChatColors.Green}https://discord.hit99.pro",
        $"{ChatColors.Grey}By playing on Hit-99, you agree to the rules: {ChatColors.Green}!rules",
    };

private int CurrentMessageIndex = 0;
    
    public override void Load(bool hotReload)
    {
        Console.WriteLine($"{ConsolePluginPrefix}Plugin loaded!");

        AddTimer(autoMessageTimer, BroadcastAutoMessage);
    }

    // AUTO MESSAGE

    private void BroadcastAutoMessage()
{
    if (AutoMessages.Count == 0)
        return;

    if (Utilities.GetPlayers().Count == 0)
    return;

    string message = AutoMessages[CurrentMessageIndex];

    Server.PrintToChatAll($"{PluginPrefix}{message}");

    CurrentMessageIndex++;

    if (CurrentMessageIndex >= AutoMessages.Count)
        CurrentMessageIndex = 0;

    AddTimer(autoMessageTimer, BroadcastAutoMessage);
}

    // JOIN MESSAGE

    [GameEventHandler]
    public HookResult OnPlayerConnectFull(EventPlayerConnectFull @event, GameEventInfo info)
    {
        CCSPlayerController? player = @event.Userid;

        if (player == null || !player.IsValid)
            return HookResult.Continue;
        player.PrintToChat(" ");
        player.PrintToChat($"Welcome to {ChatColors.Red}Hit-99 {ChatColors.White}Community Competitive {ChatColors.Green}{player.PlayerName}{ChatColors.White}!");
        player.PrintToChat($"Join our Discord Community: {ChatColors.Green}{discordLink}");
        player.PrintToChat($"Type {ChatColors.Green}!help{ChatColors.White} for a list of commands");
        player.PrintToChat(" ");

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

        player.PrintToChat($"{PluginPrefix}Join the Hit-99 Discord Community: {ChatColors.Green}{discordLink}");
    }

    [ConsoleCommand("fluxer", "Replies with fluxer link")]
    public void OnFluxerCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (player == null)
        {
            return;
        }

        player.PrintToChat($"{PluginPrefix}Join the Hit-99 Fluxer Community: {ChatColors.Green}{fluxerLink}");
    }
}
