using System;

namespace ProcGen
{
	// Token: 0x020004E7 RID: 1255
	public class WorldGenTags
	{
		// Token: 0x0400131B RID: 4891
		public static readonly Tag ConnectToSiblings = TagManager.Create("ConnectToSiblings");

		// Token: 0x0400131C RID: 4892
		public static readonly Tag ConnectTypeMinSpan = TagManager.Create("ConnectTypeMinSpan");

		// Token: 0x0400131D RID: 4893
		public static readonly Tag ConnectTypeSpan = TagManager.Create("ConnectTypeSpan");

		// Token: 0x0400131E RID: 4894
		public static readonly Tag ConnectTypeNone = TagManager.Create("ConnectTypeNone");

		// Token: 0x0400131F RID: 4895
		public static readonly Tag ConnectTypeFull = TagManager.Create("ConnectTypeFull");

		// Token: 0x04001320 RID: 4896
		public static readonly Tag ConnectTypeRandom = TagManager.Create("ConnectTypeRandom");

		// Token: 0x04001321 RID: 4897
		public static readonly Tag Cell = TagManager.Create("Cell");

		// Token: 0x04001322 RID: 4898
		public static readonly Tag Edge = TagManager.Create("Edge");

		// Token: 0x04001323 RID: 4899
		public static readonly Tag Corner = TagManager.Create("Corner");

		// Token: 0x04001324 RID: 4900
		public static readonly Tag EdgeUnpassable = TagManager.Create("EdgeUnpassable");

		// Token: 0x04001325 RID: 4901
		public static readonly Tag EdgeClosed = TagManager.Create("EdgeClosed");

		// Token: 0x04001326 RID: 4902
		public static readonly Tag EdgeOpen = TagManager.Create("EdgeOpen");

		// Token: 0x04001327 RID: 4903
		public static readonly Tag IgnoreCaveOverride = TagManager.Create("IgnoreCaveOverride");

		// Token: 0x04001328 RID: 4904
		public static readonly Tag ErodePointToCentroid = TagManager.Create("ErodePointToCentroid");

		// Token: 0x04001329 RID: 4905
		public static readonly Tag ErodePointToCentroidInv = TagManager.Create("ErodePointToCentroidInv");

		// Token: 0x0400132A RID: 4906
		public static readonly Tag ErodePointToEdge = TagManager.Create("ErodePointToEdge");

		// Token: 0x0400132B RID: 4907
		public static readonly Tag ErodePointToEdgeInv = TagManager.Create("ErodePointToEdgeInv");

		// Token: 0x0400132C RID: 4908
		public static readonly Tag ErodePointToBorder = TagManager.Create("ErodePointToBorder");

		// Token: 0x0400132D RID: 4909
		public static readonly Tag ErodePointToBorderInv = TagManager.Create("ErodePointToBorderInv");

		// Token: 0x0400132E RID: 4910
		public static readonly Tag ErodePointToBorderWeak = TagManager.Create("ErodePointToBorderWeak");

		// Token: 0x0400132F RID: 4911
		public static readonly Tag ErodePointToWorldTop = TagManager.Create("ErodePointToWorldTop");

		// Token: 0x04001330 RID: 4912
		public static readonly Tag ErodePointToWorldTopOrSide = TagManager.Create("ErodePointToWorldTopOrSide");

		// Token: 0x04001331 RID: 4913
		public static readonly Tag DistFunctionPointCentroid = TagManager.Create("DistFunctionPointCentroid");

		// Token: 0x04001332 RID: 4914
		public static readonly Tag DistFunctionPointEdge = TagManager.Create("DistFunctionPointEdge");

		// Token: 0x04001333 RID: 4915
		public static readonly Tag SplitOnParentDensity = TagManager.Create("SplitOnParentDensity");

		// Token: 0x04001334 RID: 4916
		public static readonly Tag SplitTwice = TagManager.Create("SplitTwice");

		// Token: 0x04001335 RID: 4917
		public static readonly Tag UltraHighDensitySplit = TagManager.Create("UltraHighDensitySplit");

		// Token: 0x04001336 RID: 4918
		public static readonly Tag VeryHighDensitySplit = TagManager.Create("VeryHighDensitySplit");

		// Token: 0x04001337 RID: 4919
		public static readonly Tag HighDensitySplit = TagManager.Create("HighDensitySplit");

		// Token: 0x04001338 RID: 4920
		public static readonly Tag MediumDensitySplit = TagManager.Create("MediumDensitySplit");

		// Token: 0x04001339 RID: 4921
		public static readonly Tag UnassignedNode = TagManager.Create("UnassignedNode");

		// Token: 0x0400133A RID: 4922
		public static readonly Tag Feature = TagManager.Create("Feature");

		// Token: 0x0400133B RID: 4923
		public static readonly Tag CenteralFeature = TagManager.Create("CenteralFeature");

		// Token: 0x0400133C RID: 4924
		public static readonly Tag Overworld = TagManager.Create("Overworld");

		// Token: 0x0400133D RID: 4925
		public static readonly Tag StartNear = TagManager.Create("StartNear");

		// Token: 0x0400133E RID: 4926
		public static readonly Tag StartMedium = TagManager.Create("StartMedium");

		// Token: 0x0400133F RID: 4927
		public static readonly Tag StartFar = TagManager.Create("StartFar");

		// Token: 0x04001340 RID: 4928
		public static readonly Tag NearEdge = TagManager.Create("NearEdge");

		// Token: 0x04001341 RID: 4929
		public static readonly Tag NearSurface = TagManager.Create("NearSurface");

		// Token: 0x04001342 RID: 4930
		public static readonly Tag NearDepths = TagManager.Create("NearDepths");

		// Token: 0x04001343 RID: 4931
		public static readonly Tag AtStart = TagManager.Create("AtStart");

		// Token: 0x04001344 RID: 4932
		public static readonly Tag AtSurface = TagManager.Create("AtSurface");

		// Token: 0x04001345 RID: 4933
		public static readonly Tag AtDepths = TagManager.Create("AtDepths");

		// Token: 0x04001346 RID: 4934
		public static readonly Tag AtEdge = TagManager.Create("AtEdge");

		// Token: 0x04001347 RID: 4935
		public static readonly Tag EdgeOfVoid = TagManager.Create("EdgeOfVoid");

		// Token: 0x04001348 RID: 4936
		public static readonly Tag Dry = TagManager.Create("Dry");

		// Token: 0x04001349 RID: 4937
		public static readonly Tag Wet = TagManager.Create("Wet");

		// Token: 0x0400134A RID: 4938
		public static readonly Tag River = TagManager.Create("River");

		// Token: 0x0400134B RID: 4939
		public static readonly Tag StartWorld = TagManager.Create("StartWorld");

		// Token: 0x0400134C RID: 4940
		public static readonly Tag StartLocation = TagManager.Create("StartLocation");

		// Token: 0x0400134D RID: 4941
		public static readonly Tag NearStartLocation = TagManager.Create("NearStartLocation");

		// Token: 0x0400134E RID: 4942
		public static readonly Tag POI = TagManager.Create("POI");

		// Token: 0x0400134F RID: 4943
		public static readonly Tag NoGlobalFeatureSpawning = TagManager.Create("NoGlobalFeatureSpawning");

		// Token: 0x04001350 RID: 4944
		public static readonly Tag PreventAmbientMobsInFeature = TagManager.Create("PreventAmbientMobsInFeature");

		// Token: 0x04001351 RID: 4945
		public static readonly Tag AllowExceedNodeBorders = TagManager.Create("AllowExceedNodeBorders");

		// Token: 0x04001352 RID: 4946
		public static readonly Tag HighPriorityFeature = TagManager.Create("HighPriorityFeature");

		// Token: 0x04001353 RID: 4947
		public static readonly Tag RemoveWorldBorderOverVacuum = TagManager.Create("RemoveWorldBorderOverVacuum");

		// Token: 0x04001354 RID: 4948
		public static readonly Tag CaveVoidSliver = TagManager.Create("CaveVoidSliver");

		// Token: 0x04001355 RID: 4949
		public static readonly Tag SwapLakesToBelow = TagManager.Create("SwapLakesToBelow");

		// Token: 0x04001356 RID: 4950
		public static readonly Tag Geode = TagManager.Create("Geode");

		// Token: 0x04001357 RID: 4951
		public static readonly Tag TheVoid = TagManager.Create("TheVoid");

		// Token: 0x04001358 RID: 4952
		public static readonly Tag SprinkleOfMetal = TagManager.Create("SprinkleOfMetal");

		// Token: 0x04001359 RID: 4953
		public static readonly Tag SprinkleOfOxyRock = TagManager.Create("SprinkleOfOxyRock");

		// Token: 0x0400135A RID: 4954
		public static readonly Tag RocketInterior = TagManager.Create("RocketInterior");

		// Token: 0x0400135B RID: 4955
		public static readonly Tag DEBUG_Split = TagManager.Create("DEBUG_Split");

		// Token: 0x0400135C RID: 4956
		public static readonly Tag DEBUG_SplitForChildCount = TagManager.Create("DEBUG_SplitForChildCount");

		// Token: 0x0400135D RID: 4957
		public static readonly Tag DEBUG_SplitTopSite = TagManager.Create("DEBUG_SplitTopSite");

		// Token: 0x0400135E RID: 4958
		public static readonly Tag DEBUG_SplitBottomSite = TagManager.Create("DEBUG_SplitBottomSite");

		// Token: 0x0400135F RID: 4959
		public static readonly Tag DEBUG_SplitLargeStartingSites = TagManager.Create("DEBUG_SplitLargeStartingSites");

		// Token: 0x04001360 RID: 4960
		public static readonly Tag DEBUG_NoSplitForChildCount = TagManager.Create("DEBUG_NoSplitForChildCount");

		// Token: 0x04001361 RID: 4961
		public static readonly TagSet DebugTags = new TagSet(new Tag[]
		{
			WorldGenTags.DEBUG_Split,
			WorldGenTags.DEBUG_SplitForChildCount,
			WorldGenTags.DEBUG_SplitTopSite,
			WorldGenTags.DEBUG_SplitBottomSite,
			WorldGenTags.DEBUG_SplitLargeStartingSites,
			WorldGenTags.DEBUG_NoSplitForChildCount
		});

		// Token: 0x04001362 RID: 4962
		public static readonly TagSet MapTags = new TagSet(new Tag[]
		{
			WorldGenTags.Cell,
			WorldGenTags.Edge,
			WorldGenTags.Corner,
			WorldGenTags.EdgeUnpassable,
			WorldGenTags.EdgeClosed,
			WorldGenTags.EdgeOpen
		});

		// Token: 0x04001363 RID: 4963
		public static readonly TagSet CommandTags = new TagSet(new Tag[]
		{
			WorldGenTags.IgnoreCaveOverride,
			WorldGenTags.ErodePointToCentroid,
			WorldGenTags.ErodePointToCentroidInv,
			WorldGenTags.DistFunctionPointCentroid,
			WorldGenTags.DistFunctionPointEdge,
			WorldGenTags.SplitOnParentDensity,
			WorldGenTags.SplitTwice,
			WorldGenTags.UltraHighDensitySplit,
			WorldGenTags.VeryHighDensitySplit,
			WorldGenTags.HighDensitySplit,
			WorldGenTags.MediumDensitySplit
		});

		// Token: 0x04001364 RID: 4964
		public static readonly TagSet WorldTags = new TagSet(new Tag[]
		{
			WorldGenTags.UnassignedNode,
			WorldGenTags.Feature,
			WorldGenTags.CenteralFeature,
			WorldGenTags.Overworld,
			WorldGenTags.NearSurface,
			WorldGenTags.NearDepths,
			WorldGenTags.AtSurface,
			WorldGenTags.AtDepths,
			WorldGenTags.AtEdge,
			WorldGenTags.AtStart,
			WorldGenTags.StartNear,
			WorldGenTags.StartMedium
		});

		// Token: 0x04001365 RID: 4965
		public static readonly TagSet DistanceTags = new TagSet(new Tag[]
		{
			WorldGenTags.AtSurface,
			WorldGenTags.AtDepths,
			WorldGenTags.AtEdge,
			WorldGenTags.AtStart
		});
	}
}
