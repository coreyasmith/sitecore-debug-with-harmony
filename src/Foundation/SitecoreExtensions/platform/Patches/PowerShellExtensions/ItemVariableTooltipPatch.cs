using HarmonyLib;
using Sitecore.Data.Items;
using Spe.Core.Debugging;

namespace CoreySmith.Foundation.SitecoreExtensions.Patches.PowerShellExtensions
{
    [HarmonyPatch(typeof(VariableDetails), "GetValueString")]
    public static class ItemVariableTooltipPatch
    {
        static void Postfix(object value, ref string __result)
        {
            if (value is Item item)
            {
                __result = $"[Item: {item.Paths.FullPath}]"; // override the original return value
            }
        }
    }
}
