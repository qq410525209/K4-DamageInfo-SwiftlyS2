# Changelog

All notable changes to K4-DamageInfo will be documented in this file.

## [1.0.1] - 2025-12-12

### Added
- **CenterDamageMode**: New configuration option to choose center damage display type
  - Mode 1 (default): `CenterHTML` - Formatted HTML display
  - Mode 2: `CenterAlert` - Plain text alert (max 3 lines, `\n` for newlines, no HTML formatting)
  - New translation key: `phrases.center.alert` for alert mode

### Changed
- **Configuration System Refactored**: Migrated from `_config` field to `IOptionsMonitor<PluginConfig>` pattern
  - Added `Config.CurrentValue` for all configuration access
  - Enables hot-reload of configuration without plugin restart
  - Config file renamed from `config.jsonc` to `k4-damageinfo.jsonc`
- **PluginConfig Class**: Moved from nested partial class to standalone class file
- **Build Configuration**: Updated configuration builder to match SwiftlyS2 best practices
  - Added `reloadOnChange: true` for live configuration updates
  - Added dependency injection with `ServiceCollection`

### Technical Details
- Added imports: `Microsoft.Extensions.DependencyInjection`, `Microsoft.Extensions.Options`
- Configuration now uses `IOptionsMonitor<PluginConfig>` instead of direct `IConfiguration` binding
- GameRules access already followed best practices (`Core.EntitySystem.GetGameRules()`)

### Migration Notes
- Rename existing `config.jsonc` to `k4-damageinfo.jsonc` after update
- Add `"CenterDamageMode": 1` to config if you want to keep HTML mode (default)
- Add translation key `phrases.center.alert` for CenterAlert mode (e.g., `"{0}\n-{1} Armor\n{2} Total"` )
