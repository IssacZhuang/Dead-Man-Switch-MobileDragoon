﻿using RimWorld;
using RimWorld.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace WalkerGear
{
    public class JobDriver_GetInWalkerCore : JobDriver
    {
        private const TargetIndex maintenanceBay = TargetIndex.A;
        protected const int wait = 200;
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            if (this.pawn.BodySize > 1.25)
            {
                Messages.Message("WG_TooBigForPilot".Translate(), MessageTypeDefOf.RejectInput, false);
                return false;
            }
            if (pawn.CurJob.GetTarget(TargetIndex.A).Thing is Building_MaintenanceBay bay && !bay.CanGear(pawn))
            {
                Messages.Message("WG_ApparelLayerTaken".Translate(), MessageTypeDefOf.RejectInput, false);
                return false;
            }
            if (!pawn.DevelopmentalStage.Adult())
            {
                Messages.Message("WG_TooYoungToPilot".Translate(), MessageTypeDefOf.RejectInput, false);
                return false;
            }
            return this.pawn.Reserve(this.job.GetTarget(maintenanceBay), this.job, errorOnFailed: errorOnFailed);
        }
        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedNullOrForbidden(maintenanceBay);

            yield return Toils_Goto.GotoThing(maintenanceBay, PathEndMode.InteractionCell);
            yield return Toils_General.WaitWith(TargetIndex.A, wait, true, true);
            Toil gearUp = new()
            {
                initAction = () =>
                {
                    Pawn actor = this.pawn;
                    if (actor.CurJob.GetTarget(TargetIndex.A).Thing is Building_MaintenanceBay bay)
                    {
                        actor.Position = bay.Position;
                        bay.GearUp(actor);
                        actor.drafter.Drafted = true;
                    }
                }
            };
            yield return gearUp;
        }
    }
}