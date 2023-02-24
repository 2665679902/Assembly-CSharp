using System;
using UnityEngine;

// Token: 0x02000565 RID: 1381
[AddComponentMenu("KMonoBehaviour/scripts/BuildingConduitEndpoints")]
public class BuildingConduitEndpoints : KMonoBehaviour
{
	// Token: 0x06002154 RID: 8532 RVA: 0x000B5CA7 File Offset: 0x000B3EA7
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.AddEndpoint();
	}

	// Token: 0x06002155 RID: 8533 RVA: 0x000B5CB5 File Offset: 0x000B3EB5
	protected override void OnCleanUp()
	{
		this.RemoveEndPoint();
		base.OnCleanUp();
	}

	// Token: 0x06002156 RID: 8534 RVA: 0x000B5CC4 File Offset: 0x000B3EC4
	public void RemoveEndPoint()
	{
		if (this.itemInput != null)
		{
			if (this.itemInput.ConduitType == ConduitType.Solid)
			{
				Game.Instance.solidConduitSystem.RemoveFromNetworks(this.itemInput.Cell, this.itemInput, true);
			}
			else
			{
				Conduit.GetNetworkManager(this.itemInput.ConduitType).RemoveFromNetworks(this.itemInput.Cell, this.itemInput, true);
			}
			this.itemInput = null;
		}
		if (this.itemOutput != null)
		{
			if (this.itemOutput.ConduitType == ConduitType.Solid)
			{
				Game.Instance.solidConduitSystem.RemoveFromNetworks(this.itemOutput.Cell, this.itemOutput, true);
			}
			else
			{
				Conduit.GetNetworkManager(this.itemOutput.ConduitType).RemoveFromNetworks(this.itemOutput.Cell, this.itemOutput, true);
			}
			this.itemOutput = null;
		}
	}

	// Token: 0x06002157 RID: 8535 RVA: 0x000B5DA0 File Offset: 0x000B3FA0
	public void AddEndpoint()
	{
		Building component = base.GetComponent<Building>();
		BuildingDef def = component.Def;
		this.RemoveEndPoint();
		if (def.InputConduitType != ConduitType.None)
		{
			int utilityInputCell = component.GetUtilityInputCell();
			this.itemInput = new FlowUtilityNetwork.NetworkItem(def.InputConduitType, Endpoint.Sink, utilityInputCell, base.gameObject);
			if (def.InputConduitType == ConduitType.Solid)
			{
				Game.Instance.solidConduitSystem.AddToNetworks(utilityInputCell, this.itemInput, true);
			}
			else
			{
				Conduit.GetNetworkManager(def.InputConduitType).AddToNetworks(utilityInputCell, this.itemInput, true);
			}
		}
		if (def.OutputConduitType != ConduitType.None)
		{
			int utilityOutputCell = component.GetUtilityOutputCell();
			this.itemOutput = new FlowUtilityNetwork.NetworkItem(def.OutputConduitType, Endpoint.Source, utilityOutputCell, base.gameObject);
			if (def.OutputConduitType == ConduitType.Solid)
			{
				Game.Instance.solidConduitSystem.AddToNetworks(utilityOutputCell, this.itemOutput, true);
				return;
			}
			Conduit.GetNetworkManager(def.OutputConduitType).AddToNetworks(utilityOutputCell, this.itemOutput, true);
		}
	}

	// Token: 0x04001325 RID: 4901
	private FlowUtilityNetwork.NetworkItem itemInput;

	// Token: 0x04001326 RID: 4902
	private FlowUtilityNetwork.NetworkItem itemOutput;
}
