using System.Linq;
using RimWorld;
using Verse;

namespace VVO_Obliterator;

public class Projectile_ObliteratorBullet : Bullet
{
    public ModExtension_ObliteratorBullet Props => def.GetModExtension<ModExtension_ObliteratorBullet>();

    protected override void Impact(Thing hitThing, bool blockedByShield = false)
    {
        base.Impact(hitThing, blockedByShield);
        if (Props == null || hitThing is not Pawn hitPawn)
        {
            return;
        }

        var enableAlert = ObliteratorMod.instance.settings.enableAlert;
        var destroyBodyPartChance = ObliteratorMod.instance.settings.destroyBodyPartChance;

        // See if we apply the hediff 
        if (Rand.Value > destroyBodyPartChance)
        {
            return;
        }

        // Get list of available body parts
        var parts = hitPawn.health?.hediffSet?.GetNotMissingParts();
        if (parts == null)
        {
            return;
        }

        var part = parts.Where(record => record.depth == BodyPartDepth.Outside).RandomElement();

        // Destroy a body part of a given pawn
        DestroyPart(hitPawn, part);
        if (enableAlert)
        {
            Messages.Message("VVO_Obliterator_SuccessMessage".Translate(
                launcher.Label, part.Label, hitPawn.Label), MessageTypeDefOf.NeutralEvent);
        }
    }

    protected virtual void DestroyPart(Pawn pawn, BodyPartRecord part)
    {
        pawn.TakeDamage(new DamageInfo(DamageDefOf.Bullet, 99999f, 999f, -1f, null, part));
    }
}