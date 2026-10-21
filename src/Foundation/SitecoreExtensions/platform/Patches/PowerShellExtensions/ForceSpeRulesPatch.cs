using HarmonyLib;
using Sitecore.Rules;
using Spe.Core.Utility;

namespace CoreySmith.Foundation.SitecoreExtensions.Patches.PowerShellExtensions
{
    [HarmonyPatch(typeof(RulesUtils), nameof(RulesUtils.EvaluateRules), typeof(string), typeof(RuleContext), typeof(bool))]
    public static class ForceSpeRulesPatch
    {
        static bool Prefix(ref bool __result)
        {
            __result = true; // every Show and Enable Rule passes
            return false; // skip the original method
        }
    }
}
