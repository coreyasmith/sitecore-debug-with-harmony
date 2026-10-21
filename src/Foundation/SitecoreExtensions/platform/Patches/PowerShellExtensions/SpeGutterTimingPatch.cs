using System.Diagnostics;
using HarmonyLib;
using Sitecore.Data.Items;
using Spe.Core.Diagnostics;
using Spe.Integrations.Gutters;

namespace CoreySmith.Foundation.SitecoreExtensions.Patches.PowerShellExtensions
{
    [HarmonyPatch(typeof(GutterStatusRenderer), "RunGutterScript")]
    public static class SpeGutterTimingPatch
    {
        static void Prefix(out Stopwatch __state)
        {
            __state = Stopwatch.StartNew();
        }

        static void Postfix(Item contextItem, Item scriptItem, Stopwatch __state)
        {
            __state.Stop();
            if (__state.ElapsedMilliseconds >= 50)
            {
                PowerShellLog.Warn($"[Gutter] {scriptItem.Paths.FullPath} took {__state.ElapsedMilliseconds} ms for {contextItem.Paths.FullPath}");
            }
        }
    }
}
