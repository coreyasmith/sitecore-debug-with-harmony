using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Sitecore.Data.Items;
using Spe.Core.Diagnostics;
using Spe.Core.Host;
using Spe.Integrations.Processors;

namespace CoreySmith.Foundation.SitecoreExtensions.Patches.PowerShellExtensions
{
    [HarmonyPatch(typeof(BaseScriptedDataSource), "RunEnumeration")]
    public static class SpeDataSourceOutputPatch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return instructions.MethodReplacer(
                AccessTools.Method(typeof(ScriptSession), nameof(ScriptSession.ExecuteScriptPart), new[] { typeof(Item), typeof(bool) }),
                AccessTools.Method(typeof(SpeDataSourceOutputPatch), nameof(ExecuteScriptPart)));
        }

        static List<object> ExecuteScriptPart(ScriptSession session, Item scriptItem, bool stringOutput)
        {
            var output = session.ExecuteScriptPart(scriptItem, stringOutput);
            var items = output.OfType<Item>().Count();
            if (items != output.Count)
            {
                var dropped = output.Where(result => !(result is Item)).Select(result => result?.GetType().Name ?? "null");
                PowerShellLog.Warn($"[DataSource] {scriptItem.Paths.FullPath} returned {output.Count} object(s) but only {items} Item(s); dropped: {string.Join(", ", dropped)}");
            }
            return output;
        }
    }
}
