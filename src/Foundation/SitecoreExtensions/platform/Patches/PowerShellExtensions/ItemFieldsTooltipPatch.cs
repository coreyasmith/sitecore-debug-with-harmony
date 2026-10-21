using System;
using System.Linq;
using HarmonyLib;
using Sitecore.Data.Items;
using Spe.Core.Debugging;

namespace CoreySmith.Foundation.SitecoreExtensions.Patches.PowerShellExtensions
{
    [HarmonyPatch(typeof(VariableDetails), nameof(VariableDetails.GetChildren), new Type[0])]
    public static class ItemFieldsTooltipPatch
    {
        static void Postfix(object ___valueObject, ref VariableDetailsBase[] __result)
        {
            if (!(___valueObject is Item item))
            {
                return;
            }

            item.Fields.ReadAll();
            var fields = item.Fields
                .Where(field => !field.Name.StartsWith("__", StringComparison.Ordinal))
                .Select(field => new VariableDetails(field.Name, field.Value));

            __result = __result.Concat(fields).ToArray();
        }
    }
}
