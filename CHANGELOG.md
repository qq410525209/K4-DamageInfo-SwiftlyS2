# Changelog

All notable changes to K4-DamageInfo will be documented in this file.

## [1.0.2] - 2026-02-11

### Added

- **Permission-based damage info display** - Control who can see damage information
  - `Permissions.ConsoleRequirePermission` - Require `k4.damageinfo.console` permission for console damage
  - `Permissions.CenterRequirePermission` - Require `k4.damageinfo.center` permission for center damage (HTML/Alert)
  - `Permissions.SummaryRequirePermission` - Require `k4.damageinfo.summary` permission for chat summary
  - All permissions default to `false` (everyone can see damage info)
  - Permissions can be set individually for each damage type
  - Supports wildcards: `k4.damageinfo.*` grants all damage info permissions

### Fixed

- **CRITICAL**: Fixed configuration binding bug in `LoadConfiguration()` method
  - Changed `.BindConfiguration(ConfigFileName)` to `.BindConfiguration(ConfigSection)`
  - This bug caused the plugin to always use hardcoded default values instead of reading from `k4-damageinfo.jsonc`
  - **Impact**: Plugin configuration was completely non-functional - all settings were ignored

### Changed

- Upgraded configuration initialization to use `.AddOptionsWithValidateOnStart<T>()` (per SwiftlyS2 documentation)
  - Validates configuration on startup to catch errors early

### Usage Example

**Config (`k4-damageinfo.jsonc`):**
```jsonc
{
  "K4DamageInfo": {
    // ... other settings ...
    "Permissions": {
      "ConsoleRequirePermission": true,   // Only players with k4.damageinfo.console
      "CenterRequirePermission": false,    // Everyone can see center damage
      "SummaryRequirePermission": false    // Everyone can see death/round summary
    }
  }
}
```

**Permissions (`admins.json`):**
```json
{
  "76561198012345678": {
    "permissions": ["k4.damageinfo.*"]     // All damage info permissions
  },
  "76561198087654321": {
    "permissions": ["k4.damageinfo.console", "k4.damageinfo.center"]  // Specific permissions
  }
}
```

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
