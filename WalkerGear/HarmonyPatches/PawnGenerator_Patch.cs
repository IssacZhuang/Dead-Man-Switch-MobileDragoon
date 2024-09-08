using HarmonyLib;
using RimWorld;
using Verse;

namespace WalkerGear
{

    [HarmonyPatch(typeof(PawnGenerator), "GenerateGearFor")]
    internal static class PawnGenerator_Patch
    {
        public static void Postfix(Pawn pawn, PawnGenerationRequest request)
        {
            Log.Message("Force Apparel Gen");
            PawnKindDef def = request.KindDef;
            if (def == null) return;
            ModExtForceApparelGen modExt = def.GetModExtension<ModExtForceApparelGen>();
            if (modExt == null)
            {
                return;
            }

            WalkerGear_Core core = null;
            foreach (ThingDef apparelDef in modExt.apparels)
            {
                Apparel apparel = (Apparel)ThingMaker.MakeThing(apparelDef);
                pawn.apparel.Wear(apparel);
                WalkerGear_Core core2 = apparel as WalkerGear_Core;
                if (core2 != null)
                {
                    core = core2;
                }
            }

            core?.RefreshHP(true);
            core.Health = core.HealthMax * modExt.StructurePointRange.RandomInRange;
        }
    }
}

