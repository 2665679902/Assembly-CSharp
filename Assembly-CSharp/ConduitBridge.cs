using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000595 RID: 1429
[AddComponentMenu("KMonoBehaviour/scripts/ConduitBridge")]
public class ConduitBridge : ConduitBridgeBase, IBridgedNetworkItem
{
	// Token: 0x06002312 RID: 8978 RVA: 0x000BE179 File Offset: 0x000BC379
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.accumulator = Game.Instance.accumulators.Add("Flow", this);
	}

	// Token: 0x06002313 RID: 8979 RVA: 0x000BE19C File Offset: 0x000BC39C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Building component = base.GetComponent<Building>();
		this.inputCell = component.GetUtilityInputCell();
		this.outputCell = component.GetUtilityOutputCell();
		Conduit.GetFlowManager(this.type).AddConduitUpdater(new Action<float>(this.ConduitUpdate), ConduitFlowPriority.Default);
	}

	// Token: 0x06002314 RID: 8980 RVA: 0x000BE1EB File Offset: 0x000BC3EB
	protected override void OnCleanUp()
	{
		Conduit.GetFlowManager(this.type).RemoveConduitUpdater(new Action<float>(this.ConduitUpdate));
		Game.Instance.accumulators.Remove(this.accumulator);
		base.OnCleanUp();
	}

	// Token: 0x06002315 RID: 8981 RVA: 0x000BE228 File Offset: 0x000BC428
	private void ConduitUpdate(float dt)
	{
		ConduitFlow flowManager = Conduit.GetFlowManager(this.type);
		if (!flowManager.HasConduit(this.inputCell) || !flowManager.HasConduit(this.outputCell))
		{
			base.SendEmptyOnMassTransfer();
			return;
		}
		ConduitFlow.ConduitContents contents = flowManager.GetContents(this.inputCell);
		float num = contents.mass;
		if (this.desiredMassTransfer != null)
		{
			num = this.desiredMassTransfer(dt, contents.element, contents.mass, contents.temperature, contents.diseaseIdx, contents.diseaseCount, null);
		}
		if (num > 0f)
		{
			int num2 = (int)(num / contents.mass * (float)contents.diseaseCount);
			float num3 = flowManager.AddElement(this.outputCell, contents.element, num, contents.temperature, contents.diseaseIdx, num2);
			if (num3 <= 0f)
			{
				base.SendEmptyOnMassTransfer();
				return;
			}
			flowManager.RemoveElement(this.inputCell, num3);
			Game.Instance.accumulators.Accumulate(this.accumulator, contents.mass);
			if (this.OnMassTransfer != null)
			{
				this.OnMassTransfer(contents.element, num3, contents.temperature, contents.diseaseIdx, num2, null);
				return;
			}
		}
		else
		{
			base.SendEmptyOnMassTransfer();
		}
	}

	// Token: 0x06002316 RID: 8982 RVA: 0x000BE35C File Offset: 0x000BC55C
	public void AddNetworks(ICollection<UtilityNetwork> networks)
	{
		IUtilityNetworkMgr networkManager = Conduit.GetNetworkManager(this.type);
		UtilityNetwork utilityNetwork = networkManager.GetNetworkForCell(this.inputCell);
		if (utilityNetwork != null)
		{
			networks.Add(utilityNetwork);
		}
		utilityNetwork = networkManager.GetNetworkForCell(this.outputCell);
		if (utilityNetwork != null)
		{
			networks.Add(utilityNetwork);
		}
	}

	// Token: 0x06002317 RID: 8983 RVA: 0x000BE3A4 File Offset: 0x000BC5A4
	public bool IsConnectedToNetworks(ICollection<UtilityNetwork> networks)
	{
		bool flag = false;
		IUtilityNetworkMgr networkManager = Conduit.GetNetworkManager(this.type);
		return flag || networks.Contains(networkManager.GetNetworkForCell(this.inputCell)) || networks.Contains(networkManager.GetNetworkForCell(this.outputCell));
	}

	// Token: 0x06002318 RID: 8984 RVA: 0x000BE3EB File Offset: 0x000BC5EB
	public int GetNetworkCell()
	{
		return this.inputCell;
	}

	// Token: 0x0400142F RID: 5167
	[SerializeField]
	public ConduitType type;

	// Token: 0x04001430 RID: 5168
	private int inputCell;

	// Token: 0x04001431 RID: 5169
	private int outputCell;

	// Token: 0x04001432 RID: 5170
	private HandleVector<int>.Handle accumulator = HandleVector<int>.InvalidHandle;
}
