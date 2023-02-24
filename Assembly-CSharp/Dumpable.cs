using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200072A RID: 1834
[AddComponentMenu("KMonoBehaviour/Workable/Dumpable")]
public class Dumpable : Workable
{
	// Token: 0x06003238 RID: 12856 RVA: 0x0010CA02 File Offset: 0x0010AC02
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<Dumpable>(493375141, Dumpable.OnRefreshUserMenuDelegate);
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Emptying;
	}

	// Token: 0x06003239 RID: 12857 RVA: 0x0010CA30 File Offset: 0x0010AC30
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.isMarkedForDumping)
		{
			this.CreateChore();
		}
		base.SetWorkTime(0.1f);
	}

	// Token: 0x0600323A RID: 12858 RVA: 0x0010CA54 File Offset: 0x0010AC54
	public void ToggleDumping()
	{
		if (DebugHandler.InstantBuildMode)
		{
			this.OnCompleteWork(null);
			return;
		}
		if (this.isMarkedForDumping)
		{
			this.isMarkedForDumping = false;
			this.chore.Cancel("Cancel Dumping!");
			Prioritizable.RemoveRef(base.gameObject);
			this.chore = null;
			base.ShowProgressBar(false);
			return;
		}
		this.isMarkedForDumping = true;
		this.CreateChore();
	}

	// Token: 0x0600323B RID: 12859 RVA: 0x0010CAB8 File Offset: 0x0010ACB8
	private void CreateChore()
	{
		if (this.chore == null)
		{
			Prioritizable.AddRef(base.gameObject);
			this.chore = new WorkChore<Dumpable>(Db.Get().ChoreTypes.EmptyStorage, this, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		}
	}

	// Token: 0x0600323C RID: 12860 RVA: 0x0010CB04 File Offset: 0x0010AD04
	protected override void OnCompleteWork(Worker worker)
	{
		this.isMarkedForDumping = false;
		this.chore = null;
		this.Dump();
		Prioritizable.RemoveRef(base.gameObject);
	}

	// Token: 0x0600323D RID: 12861 RVA: 0x0010CB25 File Offset: 0x0010AD25
	public void Dump()
	{
		this.Dump(base.transform.GetPosition());
	}

	// Token: 0x0600323E RID: 12862 RVA: 0x0010CB38 File Offset: 0x0010AD38
	public void Dump(Vector3 pos)
	{
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		if (component.Mass > 0f)
		{
			if (component.Element.IsLiquid)
			{
				FallingWater.instance.AddParticle(Grid.PosToCell(pos), component.Element.idx, component.Mass, component.Temperature, component.DiseaseIdx, component.DiseaseCount, true, false, false, false);
			}
			else
			{
				SimMessages.AddRemoveSubstance(Grid.PosToCell(pos), component.ElementID, CellEventLogger.Instance.Dumpable, component.Mass, component.Temperature, component.DiseaseIdx, component.DiseaseCount, true, -1);
			}
		}
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x0600323F RID: 12863 RVA: 0x0010CBE0 File Offset: 0x0010ADE0
	private void OnRefreshUserMenu(object data)
	{
		if (this.HasTag(GameTags.Stored))
		{
			return;
		}
		KIconButtonMenu.ButtonInfo buttonInfo = (this.isMarkedForDumping ? new KIconButtonMenu.ButtonInfo("action_empty_contents", UI.USERMENUACTIONS.DUMP.NAME_OFF, new System.Action(this.ToggleDumping), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.DUMP.TOOLTIP_OFF, true) : new KIconButtonMenu.ButtonInfo("action_empty_contents", UI.USERMENUACTIONS.DUMP.NAME, new System.Action(this.ToggleDumping), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.DUMP.TOOLTIP, true));
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
	}

	// Token: 0x04001E86 RID: 7814
	private Chore chore;

	// Token: 0x04001E87 RID: 7815
	[Serialize]
	private bool isMarkedForDumping;

	// Token: 0x04001E88 RID: 7816
	private static readonly EventSystem.IntraObjectHandler<Dumpable> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Dumpable>(delegate(Dumpable component, object data)
	{
		component.OnRefreshUserMenu(data);
	});
}
