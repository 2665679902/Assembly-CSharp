using System;

// Token: 0x02000700 RID: 1792
public enum BuildLocationRule
{
	// Token: 0x04001D9E RID: 7582
	Anywhere,
	// Token: 0x04001D9F RID: 7583
	OnFloor,
	// Token: 0x04001DA0 RID: 7584
	OnFloorOverSpace,
	// Token: 0x04001DA1 RID: 7585
	OnCeiling,
	// Token: 0x04001DA2 RID: 7586
	OnWall,
	// Token: 0x04001DA3 RID: 7587
	InCorner,
	// Token: 0x04001DA4 RID: 7588
	Tile,
	// Token: 0x04001DA5 RID: 7589
	NotInTiles,
	// Token: 0x04001DA6 RID: 7590
	Conduit,
	// Token: 0x04001DA7 RID: 7591
	LogicBridge,
	// Token: 0x04001DA8 RID: 7592
	WireBridge,
	// Token: 0x04001DA9 RID: 7593
	HighWattBridgeTile,
	// Token: 0x04001DAA RID: 7594
	BuildingAttachPoint,
	// Token: 0x04001DAB RID: 7595
	OnFloorOrBuildingAttachPoint,
	// Token: 0x04001DAC RID: 7596
	OnFoundationRotatable,
	// Token: 0x04001DAD RID: 7597
	BelowRocketCeiling,
	// Token: 0x04001DAE RID: 7598
	OnRocketEnvelope,
	// Token: 0x04001DAF RID: 7599
	WallFloor,
	// Token: 0x04001DB0 RID: 7600
	NoLiquidConduitAtOrigin
}
