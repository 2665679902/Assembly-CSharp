using System;
using System.Collections.Generic;
using ImGuiNET;
using STRINGS;

// Token: 0x02000533 RID: 1331
public class DevTool_StoryTrait_CritterManipulator : DevTool
{
	// Token: 0x06001FF1 RID: 8177 RVA: 0x000AE5A0 File Offset: 0x000AC7A0
	protected override void RenderTo(DevPanel panel)
	{
		if (ImGui.CollapsingHeader("Debug species lore unlock popup", ImGuiTreeNodeFlags.DefaultOpen))
		{
			this.Button_OpenSpecies(Tag.Invalid, "Unknown Species");
			ImGui.Separator();
			foreach (Tag tag in this.GetCritterSpeciesTags())
			{
				this.Button_OpenSpecies(tag, GravitasCreatureManipulatorConfig.GetNameForSpeciesTag(tag).Unwrap());
			}
		}
	}

	// Token: 0x06001FF2 RID: 8178 RVA: 0x000AE620 File Offset: 0x000AC820
	public void Button_OpenSpecies(Tag species, string speciesName = null)
	{
		if (speciesName == null)
		{
			speciesName = species.Name;
		}
		else
		{
			speciesName = string.Format("\"{0}\" (ID: {1})", UI.StripLinkFormatting(speciesName), species);
		}
		if (ImGui.Button("Show popup for: " + speciesName))
		{
			GravitasCreatureManipulator.Instance.ShowLoreUnlockedPopup(species);
		}
	}

	// Token: 0x06001FF3 RID: 8179 RVA: 0x000AE660 File Offset: 0x000AC860
	public IEnumerable<Tag> GetCritterSpeciesTags()
	{
		yield return GameTags.Creatures.Species.HatchSpecies;
		yield return GameTags.Creatures.Species.LightBugSpecies;
		yield return GameTags.Creatures.Species.OilFloaterSpecies;
		yield return GameTags.Creatures.Species.DreckoSpecies;
		yield return GameTags.Creatures.Species.GlomSpecies;
		yield return GameTags.Creatures.Species.PuftSpecies;
		yield return GameTags.Creatures.Species.PacuSpecies;
		yield return GameTags.Creatures.Species.MooSpecies;
		yield return GameTags.Creatures.Species.MoleSpecies;
		yield return GameTags.Creatures.Species.SquirrelSpecies;
		yield return GameTags.Creatures.Species.CrabSpecies;
		yield return GameTags.Creatures.Species.DivergentSpecies;
		yield return GameTags.Creatures.Species.StaterpillarSpecies;
		yield return GameTags.Creatures.Species.BeetaSpecies;
		yield break;
	}
}
