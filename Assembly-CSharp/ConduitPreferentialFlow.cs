using System;
using UnityEngine;

// Token: 0x02000598 RID: 1432
[AddComponentMenu("KMonoBehaviour/scripts/ConduitPreferentialFlow")]
public class ConduitPreferentialFlow : KMonoBehaviour, ISecondaryInput
{
	// Token: 0x06002322 RID: 8994 RVA: 0x000BE648 File Offset: 0x000BC848
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
		this.secondaryInput = new FlowUtilityNetwork.NetworkItem(this.portInfo.conduitType, Endpoint.Sink, num2, base.gameObject);
		networkManager.AddToNetworks(this.secondaryInput.Cell, this.secondaryInput, true);
	}

	// Token: 0x06002323 RID: 8995 RVA: 0x000BE70C File Offset: 0x000BC90C
	protected override void OnCleanUp()
	{
		Conduit.GetNetworkManager(this.portInfo.conduitType).RemoveFromNetworks(this.secondaryInput.Cell, this.secondaryInput, true);
		Conduit.GetFlowManager(this.portInfo.conduitType).RemoveConduitUpdater(new Action<float>(this.ConduitUpdate));
		base.OnCleanUp();
	}

	// Token: 0x06002324 RID: 8996 RVA: 0x000BE768 File Offset: 0x000BC968
	private void ConduitUpdate(float dt)
	{
		ConduitFlow flowManager = Conduit.GetFlowManager(this.portInfo.conduitType);
		if (!flowManager.HasConduit(this.outputCell))
		{
			return;
		}
		int cell = this.inputCell;
		ConduitFlow.ConduitContents conduitContents = flowManager.GetContents(cell);
		if (conduitContents.mass <= 0f)
		{
			cell = this.secondaryInput.Cell;
			conduitContents = flowManager.GetContents(cell);
		}
		if (conduitContents.mass > 0f)
		{
			float num = flowManager.AddElement(this.outputCell, conduitContents.element, conduitContents.mass, conduitContents.temperature, conduitContents.diseaseIdx, conduitContents.diseaseCount);
			if (num > 0f)
			{
				flowManager.RemoveElement(cell, num);
			}
		}
	}

	// Token: 0x06002325 RID: 8997 RVA: 0x000BE811 File Offset: 0x000BCA11
	public bool HasSecondaryConduitType(ConduitType type)
	{
		return this.portInfo.conduitType == type;
	}

	// Token: 0x06002326 RID: 8998 RVA: 0x000BE821 File Offset: 0x000BCA21
	public CellOffset GetSecondaryConduitOffset(ConduitType type)
	{
		if (this.portInfo.conduitType == type)
		{
			return this.portInfo.offset;
		}
		return CellOffset.none;
	}

	// Token: 0x04001439 RID: 5177
	[SerializeField]
	public ConduitPortInfo portInfo;

	// Token: 0x0400143A RID: 5178
	private int inputCell;

	// Token: 0x0400143B RID: 5179
	private int outputCell;

	// Token: 0x0400143C RID: 5180
	private FlowUtilityNetwork.NetworkItem secondaryInput;
}
