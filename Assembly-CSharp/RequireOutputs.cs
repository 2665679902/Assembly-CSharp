using System;
using UnityEngine;

// Token: 0x020008D6 RID: 2262
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/RequireOutputs")]
public class RequireOutputs : KMonoBehaviour
{
	// Token: 0x06004112 RID: 16658 RVA: 0x0016C824 File Offset: 0x0016AA24
	protected override void OnSpawn()
	{
		base.OnSpawn();
		ScenePartitionerLayer scenePartitionerLayer = null;
		Building component = base.GetComponent<Building>();
		this.utilityCell = component.GetUtilityOutputCell();
		this.conduitType = component.Def.OutputConduitType;
		switch (component.Def.OutputConduitType)
		{
		case ConduitType.Gas:
			scenePartitionerLayer = GameScenePartitioner.Instance.gasConduitsLayer;
			break;
		case ConduitType.Liquid:
			scenePartitionerLayer = GameScenePartitioner.Instance.liquidConduitsLayer;
			break;
		case ConduitType.Solid:
			scenePartitionerLayer = GameScenePartitioner.Instance.solidConduitsLayer;
			break;
		}
		this.UpdateConnectionState(true);
		this.UpdatePipeRoomState(true);
		if (scenePartitionerLayer != null)
		{
			this.partitionerEntry = GameScenePartitioner.Instance.Add("RequireOutputs", base.gameObject, this.utilityCell, scenePartitionerLayer, delegate(object data)
			{
				this.UpdateConnectionState(false);
			});
		}
		this.GetConduitFlow().AddConduitUpdater(new Action<float>(this.UpdatePipeState), ConduitFlowPriority.First);
	}

	// Token: 0x06004113 RID: 16659 RVA: 0x0016C8FC File Offset: 0x0016AAFC
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		IConduitFlow conduitFlow = this.GetConduitFlow();
		if (conduitFlow != null)
		{
			conduitFlow.RemoveConduitUpdater(new Action<float>(this.UpdatePipeState));
		}
		base.OnCleanUp();
	}

	// Token: 0x06004114 RID: 16660 RVA: 0x0016C93C File Offset: 0x0016AB3C
	private void UpdateConnectionState(bool force_update = false)
	{
		this.connected = this.IsConnected(this.utilityCell);
		if (this.connected != this.previouslyConnected || force_update)
		{
			this.operational.SetFlag(RequireOutputs.outputConnectedFlag, this.connected);
			this.previouslyConnected = this.connected;
			StatusItem statusItem = null;
			switch (this.conduitType)
			{
			case ConduitType.Gas:
				statusItem = Db.Get().BuildingStatusItems.NeedGasOut;
				break;
			case ConduitType.Liquid:
				statusItem = Db.Get().BuildingStatusItems.NeedLiquidOut;
				break;
			case ConduitType.Solid:
				statusItem = Db.Get().BuildingStatusItems.NeedSolidOut;
				break;
			}
			this.hasPipeGuid = this.selectable.ToggleStatusItem(statusItem, this.hasPipeGuid, !this.connected, this);
		}
	}

	// Token: 0x06004115 RID: 16661 RVA: 0x0016CA0C File Offset: 0x0016AC0C
	private bool OutputPipeIsEmpty()
	{
		if (this.ignoreFullPipe)
		{
			return true;
		}
		bool flag = true;
		if (this.connected)
		{
			flag = this.GetConduitFlow().IsConduitEmpty(this.utilityCell);
		}
		return flag;
	}

	// Token: 0x06004116 RID: 16662 RVA: 0x0016CA40 File Offset: 0x0016AC40
	private void UpdatePipeState(float dt)
	{
		this.UpdatePipeRoomState(false);
	}

	// Token: 0x06004117 RID: 16663 RVA: 0x0016CA4C File Offset: 0x0016AC4C
	private void UpdatePipeRoomState(bool force_update = false)
	{
		bool flag = this.OutputPipeIsEmpty();
		if (flag != this.previouslyHadRoom || force_update)
		{
			this.operational.SetFlag(RequireOutputs.pipesHaveRoomFlag, flag);
			this.previouslyHadRoom = flag;
			StatusItem statusItem = Db.Get().BuildingStatusItems.ConduitBlockedMultiples;
			if (this.conduitType == ConduitType.Solid)
			{
				statusItem = Db.Get().BuildingStatusItems.SolidConduitBlockedMultiples;
			}
			this.pipeBlockedGuid = this.selectable.ToggleStatusItem(statusItem, this.pipeBlockedGuid, !flag, null);
		}
	}

	// Token: 0x06004118 RID: 16664 RVA: 0x0016CAD0 File Offset: 0x0016ACD0
	private IConduitFlow GetConduitFlow()
	{
		switch (this.conduitType)
		{
		case ConduitType.Gas:
			return Game.Instance.gasConduitFlow;
		case ConduitType.Liquid:
			return Game.Instance.liquidConduitFlow;
		case ConduitType.Solid:
			return Game.Instance.solidConduitFlow;
		default:
			global::Debug.LogWarning("GetConduitFlow() called with unexpected conduitType: " + this.conduitType.ToString());
			return null;
		}
	}

	// Token: 0x06004119 RID: 16665 RVA: 0x0016CB3C File Offset: 0x0016AD3C
	private bool IsConnected(int cell)
	{
		return RequireOutputs.IsConnected(cell, this.conduitType);
	}

	// Token: 0x0600411A RID: 16666 RVA: 0x0016CB4C File Offset: 0x0016AD4C
	public static bool IsConnected(int cell, ConduitType conduitType)
	{
		ObjectLayer objectLayer = ObjectLayer.NumLayers;
		switch (conduitType)
		{
		case ConduitType.Gas:
			objectLayer = ObjectLayer.GasConduit;
			break;
		case ConduitType.Liquid:
			objectLayer = ObjectLayer.LiquidConduit;
			break;
		case ConduitType.Solid:
			objectLayer = ObjectLayer.SolidConduit;
			break;
		}
		GameObject gameObject = Grid.Objects[cell, (int)objectLayer];
		return gameObject != null && gameObject.GetComponent<BuildingComplete>() != null;
	}

	// Token: 0x04002B72 RID: 11122
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x04002B73 RID: 11123
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04002B74 RID: 11124
	public bool ignoreFullPipe;

	// Token: 0x04002B75 RID: 11125
	private int utilityCell;

	// Token: 0x04002B76 RID: 11126
	private ConduitType conduitType;

	// Token: 0x04002B77 RID: 11127
	private static readonly Operational.Flag outputConnectedFlag = new Operational.Flag("output_connected", Operational.Flag.Type.Requirement);

	// Token: 0x04002B78 RID: 11128
	private static readonly Operational.Flag pipesHaveRoomFlag = new Operational.Flag("pipesHaveRoom", Operational.Flag.Type.Requirement);

	// Token: 0x04002B79 RID: 11129
	private bool previouslyConnected = true;

	// Token: 0x04002B7A RID: 11130
	private bool previouslyHadRoom = true;

	// Token: 0x04002B7B RID: 11131
	private bool connected;

	// Token: 0x04002B7C RID: 11132
	private Guid hasPipeGuid;

	// Token: 0x04002B7D RID: 11133
	private Guid pipeBlockedGuid;

	// Token: 0x04002B7E RID: 11134
	private HandleVector<int>.Handle partitionerEntry;
}
