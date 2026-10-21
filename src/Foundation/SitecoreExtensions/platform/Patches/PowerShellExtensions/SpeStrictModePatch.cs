using System;
using HarmonyLib;
using Spe.Core.Host;

namespace CoreySmith.Foundation.SitecoreExtensions.Patches.PowerShellExtensions
{
    [HarmonyPatch(typeof(ScriptSession), "ExecuteScriptPart", typeof(string), typeof(bool), typeof(bool), typeof(bool))]
    public static class SpeStrictModePatch
    {
        static void Prefix(ref string script, bool internalScript)
        {
            if (internalScript || string.IsNullOrWhiteSpace(script) || script.TrimStart().StartsWith("param", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            script = "Set-StrictMode -Version Latest; " + script;
        }
    }
}
