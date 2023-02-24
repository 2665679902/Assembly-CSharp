using System;
using UnityEngine;

// Token: 0x02000B95 RID: 2965
public class AutoPlumberSideScreen : SideScreenContent
{
	// Token: 0x06005D3D RID: 23869 RVA: 0x00221854 File Offset: 0x0021FA54
	protected override void OnSpawn()
	{
		this.activateButton.onClick += delegate
		{
			DevAutoPlumber.AutoPlumbBuilding(this.building);
		};
		this.powerButton.onClick += delegate
		{
			DevAutoPlumber.DoElectricalPlumbing(this.building);
		};
		this.pipesButton.onClick += delegate
		{
			DevAutoPlumber.DoLiquidAndGasPlumbing(this.building);
		};
		this.solidsButton.onClick += delegate
		{
			DevAutoPlumber.SetupSolidOreDelivery(this.building);
		};
		this.minionButton.onClick += delegate
		{
			this.SpawnMinion();
		};
		this.applyTestFacade.onClick += delegate
		{
			this.CycleAvailableFacades();
		};
	}

	// Token: 0x06005D3E RID: 23870 RVA: 0x002218EC File Offset: 0x0021FAEC
	private void SpawnMinion()
	{
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(MinionConfig.ID), null, null);
		gameObject.name = Assets.GetPrefab(MinionConfig.ID).name;
		Immigration.Instance.ApplyDefaultPersonalPriorities(gameObject);
		Vector3 vector = Grid.CellToPos(Grid.PosToCell(this.building), CellAlignment.Bottom, Grid.SceneLayer.Move);
		gameObject.transform.SetLocalPosition(vector);
		gameObject.SetActive(true);
		new MinionStartingStats(false, null, null, true).Apply(gameObject);
	}

	// Token: 0x06005D3F RID: 23871 RVA: 0x0022196C File Offset: 0x0021FB6C
	public override int GetSideScreenSortOrder()
	{
		return -150;
	}

	// Token: 0x06005D40 RID: 23872 RVA: 0x00221973 File Offset: 0x0021FB73
	public override bool IsValidForTarget(GameObject target)
	{
		return DebugHandler.InstantBuildMode && target.GetComponent<Building>() != null;
	}

	// Token: 0x06005D41 RID: 23873 RVA: 0x0022198A File Offset: 0x0021FB8A
	public override void SetTarget(GameObject target)
	{
		this.building = target.GetComponent<Building>();
		this.Refresh();
	}

	// Token: 0x06005D42 RID: 23874 RVA: 0x0022199E File Offset: 0x0021FB9E
	public override void ClearTarget()
	{
	}

	// Token: 0x06005D43 RID: 23875 RVA: 0x002219A0 File Offset: 0x0021FBA0
	private void Refresh()
	{
		bool flag = this.building != null && this.building.Def.AvailableFacades.Count > 0;
		this.applyTestFacade.gameObject.SetActive(flag);
	}

	// Token: 0x06005D44 RID: 23876 RVA: 0x002219E8 File Offset: 0x0021FBE8
	private void CycleAvailableFacades()
	{
		BuildingFacade component = this.building.GetComponent<BuildingFacade>();
		if (component != null)
		{
			string nextFacade = component.GetNextFacade();
			component.ApplyBuildingFacade(Db.GetBuildingFacades().TryGet(nextFacade));
		}
	}

	// Token: 0x04003FC2 RID: 16322
	public KButton activateButton;

	// Token: 0x04003FC3 RID: 16323
	public KButton powerButton;

	// Token: 0x04003FC4 RID: 16324
	public KButton pipesButton;

	// Token: 0x04003FC5 RID: 16325
	public KButton solidsButton;

	// Token: 0x04003FC6 RID: 16326
	public KButton minionButton;

	// Token: 0x04003FC7 RID: 16327
	public KButton applyTestFacade;

	// Token: 0x04003FC8 RID: 16328
	private Building building;
}
