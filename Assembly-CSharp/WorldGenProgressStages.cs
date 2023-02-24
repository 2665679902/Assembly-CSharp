using System;
using System.Collections.Generic;

// Token: 0x02000C3A RID: 3130
public static class WorldGenProgressStages
{
	// Token: 0x040044C9 RID: 17609
	public static KeyValuePair<WorldGenProgressStages.Stages, float>[] StageWeights = new KeyValuePair<WorldGenProgressStages.Stages, float>[]
	{
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.Failure, 0f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.SetupNoise, 0.01f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.GenerateNoise, 1f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.GenerateSolarSystem, 0.01f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.WorldLayout, 1f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.CompleteLayout, 0.01f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.NoiseMapBuilder, 9f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.ClearingLevel, 0.5f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.Processing, 1f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.Borders, 0.1f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.ProcessRivers, 0.1f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.ConvertCellsToEdges, 0f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.DrawWorldBorder, 0.2f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.PlaceTemplates, 5f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.SettleSim, 6f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.DetectNaturalCavities, 6f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.PlacingCreatures, 0.01f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.Complete, 0f),
		new KeyValuePair<WorldGenProgressStages.Stages, float>(WorldGenProgressStages.Stages.NumberOfStages, 0f)
	};

	// Token: 0x02001ACA RID: 6858
	public enum Stages
	{
		// Token: 0x040078BB RID: 30907
		Failure,
		// Token: 0x040078BC RID: 30908
		SetupNoise,
		// Token: 0x040078BD RID: 30909
		GenerateNoise,
		// Token: 0x040078BE RID: 30910
		GenerateSolarSystem,
		// Token: 0x040078BF RID: 30911
		WorldLayout,
		// Token: 0x040078C0 RID: 30912
		CompleteLayout,
		// Token: 0x040078C1 RID: 30913
		NoiseMapBuilder,
		// Token: 0x040078C2 RID: 30914
		ClearingLevel,
		// Token: 0x040078C3 RID: 30915
		Processing,
		// Token: 0x040078C4 RID: 30916
		Borders,
		// Token: 0x040078C5 RID: 30917
		ProcessRivers,
		// Token: 0x040078C6 RID: 30918
		ConvertCellsToEdges,
		// Token: 0x040078C7 RID: 30919
		DrawWorldBorder,
		// Token: 0x040078C8 RID: 30920
		PlaceTemplates,
		// Token: 0x040078C9 RID: 30921
		SettleSim,
		// Token: 0x040078CA RID: 30922
		DetectNaturalCavities,
		// Token: 0x040078CB RID: 30923
		PlacingCreatures,
		// Token: 0x040078CC RID: 30924
		Complete,
		// Token: 0x040078CD RID: 30925
		NumberOfStages
	}
}
