using System;
using UnityEngine;

// Token: 0x02000597 RID: 1431
[AddComponentMenu("KMonoBehaviour/scripts/ConduitOverflow")]
public class ConduitOverflow : KMonoBehaviour, ISecondaryOutput
{
	// Token: 0x0600231C RID: 8988 RVA: 0x000BE438 File Offset: 0x000BC638
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Building component = base.GetComponent<Building>();
		this.inputCell = component.GetUtilityInputCell();
		this.outputCell = component.GetUtilityOutputCell();
		int num = Grid.PosToCell(base.transform.GetPosition());
		CellOffset rotatedOffset = component.GetRotatedOffset(this.portInfo.offset);
		int num2 = Grid.OffsetCell(num, rotatedOffset);
		Conduit.GetFlowManager(this.portInfo.conduitType).AddConduitUpdater(new Action<float>(this.ConduitUpdate), ConduitFlowPriority.Default);
		IUtilityNetworkMgr networkManager = Conduit.GetNetworkManager(this.portInfo.conduitType);
		this.secondaryOutput = new FlowUtilityNetwork.NetworkItem(this.portInfo.conduitType, Endpoint.Sink, num2, base.gameObject);
		networkManager.AddToNetworks(this.secondaryOutput.Cell, this.secondaryOutput, true);
	}

	// Token: 0x0600231D RID: 8989 RVA: 0x000BE4FC File Offset: 0x000BC6FC
	protected override void OnCleanUp()
	{
		Conduit.GetNetworkManager(this.portInfo.conduitType).RemoveFromNetworks(this.secondaryOutput.Cell, this.secondaryOutput, true);
		Conduit.GetFlowManager(this.portInfo.conduitType).RemoveConduitUpdater(new Action<float>(this.ConduitUpdate));
		base.OnCleanUp();
	}

	// Token: 0x0600231E RID: 8990 RVA: 0x000BE558 File Offset: 0x000BC758
	private void ConduitUpdate(float dt)
	{
		ConduitFlow flowManager = Conduit.GetFlowManager(this.portInfo.conduitType);
		if (!flowManager.HasConduit(this.inputCell))
		{
			return;
		}
		ConduitFlow.ConduitContents contents = flowManager.GetContents(this.inputCell);
		if (contents.mass <= 0f)
		{
			return;
		}
		int cell = this.outputCell;
		ConduitFlow.ConduitContents conduitContents = flowManager.GetContents(cell);
		if (conduitContents.mass > 0f)
		{
			cell = this.secondaryOutput.Cell;
			conduitContents = flowManager.GetContents(cell);
		}
		if (conduitContents.mass <= 0f)
		{
			float num = flowManager.AddElement(cell, contents.element, contents.mass, contents.temperature, contents.diseaseIdx, contents.diseaseCount);
			if (num > 0f)
			{
				flowManager.RemoveElement(this.inputCell, num);
			}
		}
	}

	// Token: 0x0600231F RID: 8991 RVA: 0x000BE620 File Offset: 0x000BC820
	public bool HasSecondaryConduitType(ConduitType type)
	{
		return this.portInfo.conduitType == type;
	}

	// Token: 0x06002320 RID: 8992 RVA: 0x000BE630 File Offset: 0x000BC830
	public CellOffset GetSecondaryConduitOffset(ConduitType type)
	{
		return this.portInfo.offset;
	}

	// Token: 0x04001435 RID: 5173
	[SerializeField]
	public ConduitPortInfo portInfo;

	// Token: 0x04001436 RID: 5174
	private int inputCell;

	// Token: 0x04001437 RID: 5175
	private int outputCell;

	// Token: 0x04001438 RID: 5176
	private FlowUtilityNetwork.NetworkItem secondaryOutput;
}
