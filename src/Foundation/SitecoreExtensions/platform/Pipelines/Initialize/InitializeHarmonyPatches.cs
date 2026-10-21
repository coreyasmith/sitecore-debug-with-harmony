using HarmonyLib;
using Sitecore.Diagnostics;
using Sitecore.Pipelines;

namespace CoreySmith.Foundation.SitecoreExtensions.Pipelines.Initialize
{
    public class InitializeHarmonyPatches
    {
        public void Process(PipelineArgs args)
        {
            var harmony = new Harmony("CoreySmith.Foundation.SitecoreExtensions");
            harmony.PatchAll(typeof(InitializeHarmonyPatches).Assembly);

            foreach (var method in harmony.GetPatchedMethods())
            {
                Log.Info($"[Harmony] Patched {method.DeclaringType?.FullName}.{method.Name}", this);
            }
        }
    }
}
