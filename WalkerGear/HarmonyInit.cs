﻿using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Verse;
using static HarmonyLib.AccessTools;

namespace WalkerGear
{
    public class WalkerGear : Mod
    {
        public WalkerGear(ModContentPack content) : base(content)
        {
            //Log.Warning("Patching");
            var ins = new Harmony("WalkerGear");
            //Log.Message(ins.GetPatchedMethods().Count());
            ins.PatchAllUncategorized();
            if (ModLister.GetActiveModWithIdentifier("petetimessix.simplesidearms", true)!=null)
            {
                ins.Patch(
                Method(TypeByName("WeaponAssingment"), "equipSpecificWeapon"),prefix:Method(typeof(SimpleSidearms),nameof(SimpleSidearms.EquipSpecificWeapon)));
            }
            //Log.Message(ins.GetPatchedMethods().Count());
        }
        [HarmonyPatchCategory("ModPatches")]
        internal static class SimpleSidearms
        {
            [HarmonyPrefix]
            internal static bool EquipSpecificWeapon(Pawn pawn,ref bool __result)
            {
                return pawn.equipment.Primary == null || !pawn.equipment.Primary.HasComp<CompApparelForcedWeapon>() || (__result = false);
            }
        }
    }
}
