using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace VVO_Obliterator
{
    public class Projectile_ObliteratorBullet : Bullet
    {
        public ModExtension_ObliteratorBullet Props => base.def.GetModExtension<ModExtension_ObliteratorBullet>();
        protected override void Impact(Thing hitThing)
        {
            base.Impact(hitThing);
            if (Props != null && hitThing != null && hitThing is Pawn hitPawn) 
            {
                float rand = Rand.Value;
                // See if we apply the hediff 
                if (rand <= Props.addHediffChance)
                {
                    Messages.Message("VVO_Obliterator_SuccessMessage".Translate(
                        this.launcher.Label, hitPawn.Label), MessageTypeDefOf.NeutralEvent);

                    // Verify if the target already has the hediff
                    Hediff plagueOnPawn = hitPawn.health?.hediffSet?.GetFirstHediffOfDef(Props.hediffToAdd);
                    float randomSeverity = Rand.Range(0.15f, 0.30f);

                    if (plagueOnPawn != null)
                    {
                        // Target already has plague, add the severity
                        plagueOnPawn.Severity += randomSeverity;
                    } else
                    {
                        // Target has no plague
                        Hediff hediff = HediffMaker.MakeHediff(Props.hediffToAdd, hitPawn);
                        hediff.Severity = randomSeverity;
                        hitPawn.health.AddHediff(hediff);
                    }


                }
                else
                {
                    MoteMaker.ThrowText(hitThing.PositionHeld.ToVector3(), hitThing.MapHeld, "VVO_Obliterator_FailureMote".Translate(Props.addHediffChance), 12f);
                }
            }
        }
    }
}
