using Verse;

namespace VVO_Obliterator;

public class ObliteratorSettings : ModSettings
{
    /// <summary>
    ///     The three settings our mod has.
    /// </summary>
    public float DestroyBodyPartChance = 1f;

    public bool EnableAlert;

    /// <summary>
    ///     The part that writes our settings to file. Note that saving is by ref.
    /// </summary>
    public override void ExposeData()
    {
        Scribe_Values.Look(ref DestroyBodyPartChance, "destroyBodyPartChance", 1f);
        Scribe_Values.Look(ref EnableAlert, "enableAlert");
        base.ExposeData();
    }
}