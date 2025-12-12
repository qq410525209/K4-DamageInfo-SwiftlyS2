# Changelog

All notable changes to K4-DamageInfo will be documented in this file.

## [1.0.1] - 2025-12-12

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
- Configuration structure remains unchanged, no modifications needed to config values
