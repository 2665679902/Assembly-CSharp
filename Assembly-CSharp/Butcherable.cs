using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000676 RID: 1654
[AddComponentMenu("KMonoBehaviour/Workable/Butcherable")]
public class Butcherable : Workable, ISaveLoadable
{
	// Token: 0x06002C98 RID: 11416 RVA: 0x000E9E36 File Offset: 0x000E8036
	public void SetDrops(string[] drops)
	{
		this.drops = drops;
	}

	// Token: 0x06002C99 RID: 11417 RVA: 0x000E9E40 File Offset: 0x000E8040
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<Butcherable>(1272413801, Butcherable.SetReadyToButcherDelegate);
		base.Subscribe<Butcherable>(493375141, Butcherable.OnRefreshUserMenuDelegate);
		this.workTime = 3f;
		this.multitoolContext = "harvest";
		this.multitoolHitEffectTag = "fx_harvest_splash";
	}

	// Token: 0x06002C9A RID: 11418 RVA: 0x000E9EA0 File Offset: 0x000E80A0
	public void SetReadyToButcher(object param)
	{
		this.readyToButcher = true;
	}

	// Token: 0x06002C9B RID: 11419 RVA: 0x000E9EA9 File Offset: 0x000E80A9
	public void SetReadyToButcher(bool ready)
	{
		this.readyToButcher = ready;
	}

	// Token: 0x06002C9C RID: 11420 RVA: 0x000E9EB4 File Offset: 0x000E80B4
	public void ActivateChore(object param)
	{
		if (this.chore != null)
		{
			return;
		}
		this.chore = new WorkChore<Butcherable>(Db.Get().ChoreTypes.Harvest, this, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		this.OnRefreshUserMenu(null);
	}

	// Token: 0x06002C9D RID: 11421 RVA: 0x000E9EFD File Offset: 0x000E80FD
	public void CancelChore(object param)
	{
		if (this.chore == null)
		{
			return;
		}
		this.chore.Cancel("User cancelled");
		this.chore = null;
	}

	// Token: 0x06002C9E RID: 11422 RVA: 0x000E9F1F File Offset: 0x000E811F
	private void OnClickCancel()
	{
		this.CancelChore(null);
	}

	// Token: 0x06002C9F RID: 11423 RVA: 0x000E9F28 File Offset: 0x000E8128
	private void OnClickButcher()
	{
		if (DebugHandler.InstantBuildMode)
		{
			this.OnButcherComplete();
			return;
		}
		this.ActivateChore(null);
	}

	// Token: 0x06002CA0 RID: 11424 RVA: 0x000E9F40 File Offset: 0x000E8140
	private void OnRefreshUserMenu(object data)
	{
		if (!this.readyToButcher)
		{
			return;
		}
		KIconButtonMenu.ButtonInfo buttonInfo = ((this.chore != null) ? new KIconButtonMenu.ButtonInfo("action_harvest", "Cancel Meatify", new System.Action(this.OnClickCancel), global::Action.NumActions, null, null, null, "", true) : new KIconButtonMenu.ButtonInfo("action_harvest", "Meatify", new System.Action(this.OnClickButcher), global::Action.NumActions, null, null, null, "", true));
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
	}

	// Token: 0x06002CA1 RID: 11425 RVA: 0x000E9FCE File Offset: 0x000E81CE
	protected override void OnCompleteWork(Worker worker)
	{
		this.OnButcherComplete();
	}

	// Token: 0x06002CA2 RID: 11426 RVA: 0x000E9FD8 File Offset: 0x000E81D8
	public void OnButcherComplete()
	{
		if (this.butchered)
		{
			return;
		}
		KSelectable component = base.GetComponent<KSelectable>();
		if (component && component.IsSelected)
		{
			SelectTool.Instance.Select(null, false);
		}
		for (int i = 0; i < this.drops.Length; i++)
		{
			GameObject gameObject = Scenario.SpawnPrefab(this.GetDropSpawnLocation(), 0, 0, this.drops[i], Grid.SceneLayer.Ore);
			gameObject.SetActive(true);
			Edible component2 = gameObject.GetComponent<Edible>();
			if (component2)
			{
				ReportManager.Instance.ReportValue(ReportManager.ReportType.CaloriesCreated, component2.Calories, StringFormatter.Replace(UI.ENDOFDAYREPORT.NOTES.BUTCHERED, "{0}", gameObject.GetProperName()), UI.ENDOFDAYREPORT.NOTES.BUTCHERED_CONTEXT);
			}
		}
		this.chore = null;
		this.butchered = true;
		this.readyToButcher = false;
		Game.Instance.userMenu.Refresh(base.gameObject);
		base.Trigger(395373363, null);
	}

	// Token: 0x06002CA3 RID: 11427 RVA: 0x000EA0C0 File Offset: 0x000E82C0
	private int GetDropSpawnLocation()
	{
		int num = Grid.PosToCell(base.gameObject);
		int num2 = Grid.CellAbove(num);
		if (Grid.IsValidCell(num2) && !Grid.Solid[num2])
		{
			return num2;
		}
		return num;
	}

	// Token: 0x04001A87 RID: 6791
	[MyCmpGet]
	private KAnimControllerBase controller;

	// Token: 0x04001A88 RID: 6792
	[MyCmpGet]
	private Harvestable harvestable;

	// Token: 0x04001A89 RID: 6793
	private bool readyToButcher;

	// Token: 0x04001A8A RID: 6794
	private bool butchered;

	// Token: 0x04001A8B RID: 6795
	public string[] drops;

	// Token: 0x04001A8C RID: 6796
	private Chore chore;

	// Token: 0x04001A8D RID: 6797
	private static readonly EventSystem.IntraObjectHandler<Butcherable> SetReadyToButcherDelegate = new EventSystem.IntraObjectHandler<Butcherable>(delegate(Butcherable component, object data)
	{
		component.SetReadyToButcher(data);
	});

	// Token: 0x04001A8E RID: 6798
	private static readonly EventSystem.IntraObjectHandler<Butcherable> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Butcherable>(delegate(Butcherable component, object data)
	{
		component.OnRefreshUserMenu(data);
	});
}
