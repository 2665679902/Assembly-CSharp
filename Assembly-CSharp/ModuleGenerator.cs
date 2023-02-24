using System;

// Token: 0x02000859 RID: 2137
public class ModuleGenerator : Generator
{
	// Token: 0x06003D5F RID: 15711 RVA: 0x00157294 File Offset: 0x00155494
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.connectedTags = new Tag[0];
		base.IsVirtual = true;
	}

	// Token: 0x06003D60 RID: 15712 RVA: 0x001572B0 File Offset: 0x001554B0
	protected override void OnSpawn()
	{
		CraftModuleInterface craftInterface = base.GetComponent<RocketModuleCluster>().CraftInterface;
		base.VirtualCircuitKey = craftInterface;
		this.clustercraft = craftInterface.GetComponent<Clustercraft>();
		Game.Instance.electricalConduitSystem.AddToVirtualNetworks(base.VirtualCircuitKey, this, true);
		base.OnSpawn();
	}

	// Token: 0x06003D61 RID: 15713 RVA: 0x001572F9 File Offset: 0x001554F9
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Game.Instance.electricalConduitSystem.RemoveFromVirtualNetworks(base.VirtualCircuitKey, this, true);
	}

	// Token: 0x06003D62 RID: 15714 RVA: 0x00157318 File Offset: 0x00155518
	public override bool IsProducingPower()
	{
		return this.clustercraft.IsFlightInProgress();
	}

	// Token: 0x06003D63 RID: 15715 RVA: 0x00157328 File Offset: 0x00155528
	public override void EnergySim200ms(float dt)
	{
		base.EnergySim200ms(dt);
		if (this.IsProducingPower())
		{
			base.GenerateJoules(base.WattageRating * dt, false);
			if (this.poweringStatusItemHandle == Guid.Empty)
			{
				this.poweringStatusItemHandle = this.selectable.ReplaceStatusItem(this.notPoweringStatusItemHandle, Db.Get().BuildingStatusItems.ModuleGeneratorPowered, this);
				this.notPoweringStatusItemHandle = Guid.Empty;
				return;
			}
		}
		else if (this.notPoweringStatusItemHandle == Guid.Empty)
		{
			this.notPoweringStatusItemHandle = this.selectable.ReplaceStatusItem(this.poweringStatusItemHandle, Db.Get().BuildingStatusItems.ModuleGeneratorNotPowered, this);
			this.poweringStatusItemHandle = Guid.Empty;
		}
	}

	// Token: 0x0400284B RID: 10315
	private Clustercraft clustercraft;

	// Token: 0x0400284C RID: 10316
	private Guid poweringStatusItemHandle;

	// Token: 0x0400284D RID: 10317
	private Guid notPoweringStatusItemHandle;
}
