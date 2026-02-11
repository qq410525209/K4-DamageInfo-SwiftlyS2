using SwiftlyS2.Shared.Players;

namespace K4DamageInfo;

public sealed partial class Plugin
{
	private const string PermissionConsole = "k4.damageinfo.console";
	private const string PermissionCenter = "k4.damageinfo.center";
	private const string PermissionSummary = "k4.damageinfo.summary";

	/// <summary>Check if player has permission to see console damage info</summary>
	private static bool CanSeeConsoleDamage(IPlayer player)
	{
		if (!Config.CurrentValue.Permissions.ConsoleRequirePermission)
			return true;

		return Core.Permission.PlayerHasPermission(player.SteamID, PermissionConsole);
	}

	/// <summary>Check if player has permission to see center damage info</summary>
	private static bool CanSeeCenterDamage(IPlayer player)
	{
		if (!Config.CurrentValue.Permissions.CenterRequirePermission)
			return true;

		return Core.Permission.PlayerHasPermission(player.SteamID, PermissionCenter);
	}

	/// <summary>Check if player has permission to see damage summary</summary>
	private static bool CanSeeDamageSummary(IPlayer player)
	{
		if (!Config.CurrentValue.Permissions.SummaryRequirePermission)
			return true;

		return Core.Permission.PlayerHasPermission(player.SteamID, PermissionSummary);
	}
}
