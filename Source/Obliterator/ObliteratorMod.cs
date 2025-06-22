using Mlie;
using UnityEngine;
using Verse;

namespace VVO_Obliterator;

public class ObliteratorMod : Mod
{
    public static ObliteratorMod Instance;
    private static string currentVersion;

    /// <summary>
    ///     A reference to our settings.
    /// </summary>
    public readonly ObliteratorSettings Settings;

    /// <summary>
    ///     A mandatory constructor which resolves the reference to our settings.
    /// </summary>
    /// <param name="content"></param>
    public ObliteratorMod(ModContentPack content) : base(content)
    {
        Instance = this;
        Settings = GetSettings<ObliteratorSettings>();
        currentVersion = VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }

    /// <summary>
    ///     The (optional) GUI part to set your settings.
    /// </summary>
    /// <param name="inRect">A Unity Rect with the size of the settings window.</param>
    public override void DoSettingsWindowContents(Rect inRect)
    {
        var listingStandard = new Listing_Standard();
        listingStandard.Begin(inRect);
        listingStandard.CheckboxLabeled("VVO_Obliterator_ShowAlert".Translate(), ref Settings.EnableAlert,
            "VVO_Obliterator_ShowAlertTT".Translate());
        listingStandard.Label("VVO_Obliterator_Chance".Translate(Settings.DestroyBodyPartChance.ToStringPercent()));
        Settings.DestroyBodyPartChance = listingStandard.Slider(Settings.DestroyBodyPartChance, 0f, 1f);
        if (currentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("VVO_Obliterator_CurrentModVersion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();
        base.DoSettingsWindowContents(inRect);
    }

    /// <summary>
    ///     Override SettingsCategory to show up in the list of settings.
    ///     Using .Translate() is optional, but does allow for localisation.
    /// </summary>
    /// <returns>The (translated) mod name.</returns>
    public override string SettingsCategory()
    {
        return "Obliterator";
    }
}