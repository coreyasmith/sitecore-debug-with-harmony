# ☯️ Debug Sitecore with Harmony

This solution demonstrates how to debug non-public members with the .NET library [Harmony][1] when working with Sitecore.

> [!WARNING]
> These patches are for debugging on a local instance. Do not deploy them to production.

## 🏗️ Setup

This solution was tested on [Sitecore 10.5][2] with [Sitecore PowerShell Extensions (SPE)][3] 8 and Harmony 2.4.3. It should work with [Sitecore 9.0 Initial Release][4] or later and SPE 6.4 or later.

1. Install a new instance of Sitecore with [Sitecore PowerShell Extensions][3].
2. Update the `publishUrl` in [`PublishSettings.Platform.targets`](PublishSettings.Platform.targets) to your Sitecore installation's web root (e.g., `C:\inetpub\wwwroot\sc1050.sc`).
3. If you installed a version of Sitecore other than 10.5, update the `Sitecore` packages in [`Directory.Packages.props`](Directory.Packages.props) with the version number required by your instance.
4. If you installed a version of SPE other than 8, update the `Spe` package in [`Directory.Packages.props`](Directory.Packages.props) with the version number required by your instance.
5. Build the solution.
6. Publish the solution with the [`Local`](src/Foundation/SitecoreExtensions/platform/Properties/PublishProfiles/Local.pubxml) publish profile.
7. Set breakpoints in the patch classes and attach the Visual Studio debugger to `w3wp.exe`.
8. Enjoy experimenting.

## 🩹 Patches

| Patch | Harmony technique | What it does |
| ----- | ----------------- | ------------ |
| [`ItemVariableTooltipPatch`](src/Foundation/SitecoreExtensions/platform/Patches/PowerShellExtensions/ItemVariableTooltipPatch.cs) | `Postfix` changing `__result` | Adds the item path to ISE variable tooltips |
| [`ForceSpeRulesPatch`](src/Foundation/SitecoreExtensions/platform/Patches/PowerShellExtensions/ForceSpeRulesPatch.cs) | `Prefix` skipping the original | Makes every SPE Show and Enable Rule pass |
| [`SpeStrictModePatch`](src/Foundation/SitecoreExtensions/platform/Patches/PowerShellExtensions/SpeStrictModePatch.cs) | `Prefix` modifying a `ref` parameter | Turns on PowerShell strict mode for your scripts |
| [`ItemFieldsTooltipPatch`](src/Foundation/SitecoreExtensions/platform/Patches/PowerShellExtensions/ItemFieldsTooltipPatch.cs) | `Postfix` reading a private field (`___valueObject`) | Adds item field values to ISE variable tooltips |
| [`SpeGutterTimingPatch`](src/Foundation/SitecoreExtensions/platform/Patches/PowerShellExtensions/SpeGutterTimingPatch.cs) | `Prefix` and `Postfix` sharing `__state` | Logs gutter scripts that take 50 ms or more |
| [`SpeDataSourceOutputPatch`](src/Foundation/SitecoreExtensions/platform/Patches/PowerShellExtensions/SpeDataSourceOutputPatch.cs) | `Transpiler` replacing a method call | Logs Scripted Data Source output that isn't an `Item` |

[1]: https://github.com/pardeike/Harmony
[2]: https://developers.sitecore.com/downloads/Sitecore_Experience_Platform/105/Sitecore_Experience_Platform_105
[3]: https://github.com/SitecorePowerShell/Console
[4]: https://developers.sitecore.com/downloads/Sitecore_Experience_Platform/90/Sitecore_Experience_Platform_90_Initial_Release
