using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.Plugins;

namespace K4DamageInfo;

[PluginMetadata(
	Id = "k4.damageinfo",
	Version = "1.0.2",
	Name = "K4 - Damage Info",
	Author = "K4ryuu",
	Description = "Shows damage information to players."
)]
public sealed partial class Plugin(ISwiftlyCore core) : BasePlugin(core)
{
	private const string ConfigFileName = "k4-damageinfo.jsonc";
	private const string ConfigSection = "K4DamageInfo";

	public static new ISwiftlyCore Core { get; private set; } = null!;
	public static IOptionsMonitor<PluginConfig> Config { get; private set; } = null!;

	private readonly Dictionary<int, PlayerData> _playerData = [];

	public override void Load(bool hotReload)
	{
		Core = base.Core;
		LoadConfiguration();
		RegisterEvents();
	}

	public override void Unload()
	{
		_playerData.Clear();
	}

	private static void LoadConfiguration()
	{
		Core.Configuration
			.InitializeJsonWithModel<PluginConfig>(ConfigFileName, ConfigSection)
			.Configure(builder =>
			{
				builder.AddJsonFile(ConfigFileName, optional: false, reloadOnChange: true);
			});

		ServiceCollection services = new();
		services.AddSwiftly(Core)
			.AddOptionsWithValidateOnStart<PluginConfig>()
			.BindConfiguration(ConfigSection);

		var provider = services.BuildServiceProvider();
		Config = provider.GetRequiredService<IOptionsMonitor<PluginConfig>>();
	}

	private void RegisterEvents()
	{
		Core.GameEvent.HookPost<EventPlayerHurt>(OnPlayerHurt);
		Core.GameEvent.HookPost<EventPlayerDeath>(OnPlayerDeath);
		Core.GameEvent.HookPost<EventPlayerSpawn>(OnPlayerSpawn);
		Core.GameEvent.HookPost<EventRoundEnd>(OnRoundEnd);
		Core.GameEvent.HookPost<EventRoundStart>(OnRoundStart);
	}

	private PlayerData GetPlayerData(int slot)
	{
		if (!_playerData.TryGetValue(slot, out var data))
		{
			data = new PlayerData();
			_playerData[slot] = data;
		}
		return data;
	}

	private static bool IsValidPlayer(IPlayer? player) => player != null && player.IsValid;
}
