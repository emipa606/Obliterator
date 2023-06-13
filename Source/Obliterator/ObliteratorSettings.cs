using Verse;

namespace VVO_Obliterator;

public class ObliteratorSettings : ModSettings
{
    /// <summary>
    ///     The three settings our mod has.
    /// </summary>
    public float destroyBodyPartChance = 1f;

    public bool enableAlert;

    /// <summary>
    ///     The part that writes our settings to file. Note that saving is by ref.
    /// </summary>
    public override void ExposeData()
    {
        Scribe_Values.Look(ref destroyBodyPartChance, "destroyBodyPartChance", 1f);
        Scribe_Values.Look(ref enableAlert, "enableAlert");
        base.ExposeData();
    }
}