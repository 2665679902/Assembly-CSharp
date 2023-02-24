using System;

// Token: 0x020003C6 RID: 966
public static class PathFinderQueries
{
	// Token: 0x0600140C RID: 5132 RVA: 0x0006A554 File Offset: 0x00068754
	public static void Reset()
	{
		PathFinderQueries.cellQuery = new CellQuery();
		PathFinderQueries.cellCostQuery = new CellCostQuery();
		PathFinderQueries.cellArrayQuery = new CellArrayQuery();
		PathFinderQueries.cellOffsetQuery = new CellOffsetQuery();
		PathFinderQueries.safeCellQuery = new SafeCellQuery();
		PathFinderQueries.idleCellQuery = new IdleCellQuery();
		PathFinderQueries.breathableCellQuery = new BreathableCellQuery();
		PathFinderQueries.drawNavGridQuery = new DrawNavGridQuery();
		PathFinderQueries.plantableCellQuery = new PlantableCellQuery();
		PathFinderQueries.mineableCellQuery = new MineableCellQuery();
		PathFinderQueries.staterpillarCellQuery = new StaterpillarCellQuery();
		PathFinderQueries.floorCellQuery = new FloorCellQuery();
		PathFinderQueries.buildingPlacementQuery = new BuildingPlacementQuery();
	}

	// Token: 0x04000AFE RID: 2814
	public static CellQuery cellQuery = new CellQuery();

	// Token: 0x04000AFF RID: 2815
	public static CellCostQuery cellCostQuery = new CellCostQuery();

	// Token: 0x04000B00 RID: 2816
	public static CellArrayQuery cellArrayQuery = new CellArrayQuery();

	// Token: 0x04000B01 RID: 2817
	public static CellOffsetQuery cellOffsetQuery = new CellOffsetQuery();

	// Token: 0x04000B02 RID: 2818
	public static SafeCellQuery safeCellQuery = new SafeCellQuery();

	// Token: 0x04000B03 RID: 2819
	public static IdleCellQuery idleCellQuery = new IdleCellQuery();

	// Token: 0x04000B04 RID: 2820
	public static BreathableCellQuery breathableCellQuery = new BreathableCellQuery();

	// Token: 0x04000B05 RID: 2821
	public static DrawNavGridQuery drawNavGridQuery = new DrawNavGridQuery();

	// Token: 0x04000B06 RID: 2822
	public static PlantableCellQuery plantableCellQuery = new PlantableCellQuery();

	// Token: 0x04000B07 RID: 2823
	public static MineableCellQuery mineableCellQuery = new MineableCellQuery();

	// Token: 0x04000B08 RID: 2824
	public static StaterpillarCellQuery staterpillarCellQuery = new StaterpillarCellQuery();

	// Token: 0x04000B09 RID: 2825
	public static FloorCellQuery floorCellQuery = new FloorCellQuery();

	// Token: 0x04000B0A RID: 2826
	public static BuildingPlacementQuery buildingPlacementQuery = new BuildingPlacementQuery();
}
