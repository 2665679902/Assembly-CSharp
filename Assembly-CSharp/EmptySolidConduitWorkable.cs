using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200073F RID: 1855
[AddComponentMenu("KMonoBehaviour/Workable/EmptySolidConduitWorkable")]
public class EmptySolidConduitWorkable : Workable, IEmptyConduitWorkable
{
	// Token: 0x060032F9 RID: 13049 RVA: 0x001135CC File Offset: 0x001117CC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
		base.SetWorkTime(float.PositiveInfinity);
		this.faceTargetWhenWorking = true;
		this.multitoolContext = "build";
		this.multitoolHitEffectTag = EffectConfigs.BuildSplashId;
		base.Subscribe<EmptySolidConduitWorkable>(2127324410, EmptySolidConduitWorkable.OnEmptyConduitCancelledDelegate);
		if (EmptySolidConduitWorkable.emptySolidConduitStatusItem == null)
		{
			EmptySolidConduitWorkable.emptySolidConduitStatusItem = new StatusItem("EmptySolidConduit", BUILDINGS.PREFABS.CONDUIT.STATUS_ITEM.NAME, BUILDINGS.PREFABS.CONDUIT.STATUS_ITEM.TOOLTIP, "status_item_empty_pipe", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.SolidConveyor.ID, 32770, true, null);
		}
		this.requiredSkillPerk = Db.Get().SkillPerks.CanDoPlumbing.Id;
		this.shouldShowSkillPerkStatusItem = false;
	}

	// Token: 0x060032FA RID: 13050 RVA: 0x0011368C File Offset: 0x0011188C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.elapsedTime != -1f)
		{
			this.MarkForEmptying();
		}
	}

	// Token: 0x060032FB RID: 13051 RVA: 0x001136A8 File Offset: 0x001118A8
	public void MarkForEmptying()
	{
		if (this.chore == null && this.HasContents())
		{
			StatusItem statusItem = this.GetStatusItem();
			base.GetComponent<KSelectable>().ToggleStatusItem(statusItem, true, null);
			this.CreateWorkChore();
		}
	}

	// Token: 0x060032FC RID: 13052 RVA: 0x001136E4 File Offset: 0x001118E4
	private bool HasContents()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		return this.GetFlowManager().GetContents(num).pickupableHandle.IsValid();
	}

	// Token: 0x060032FD RID: 13053 RVA: 0x0011371B File Offset: 0x0011191B
	private void CancelEmptying()
	{
		this.CleanUpVisualization();
		if (this.chore != null)
		{
			this.chore.Cancel("Cancel");
			this.chore = null;
			this.shouldShowSkillPerkStatusItem = false;
			this.UpdateStatusItem(null);
		}
	}

	// Token: 0x060032FE RID: 13054 RVA: 0x00113750 File Offset: 0x00111950
	private void CleanUpVisualization()
	{
		StatusItem statusItem = this.GetStatusItem();
		KSelectable component = base.GetComponent<KSelectable>();
		if (component != null)
		{
			component.ToggleStatusItem(statusItem, false, null);
		}
		this.elapsedTime = -1f;
		if (this.chore != null)
		{
			base.GetComponent<Prioritizable>().RemoveRef();
		}
	}

	// Token: 0x060032FF RID: 13055 RVA: 0x0011379C File Offset: 0x0011199C
	protected override void OnCleanUp()
	{
		this.CancelEmptying();
		base.OnCleanUp();
	}

	// Token: 0x06003300 RID: 13056 RVA: 0x001137AA File Offset: 0x001119AA
	private SolidConduitFlow GetFlowManager()
	{
		return Game.Instance.solidConduitFlow;
	}

	// Token: 0x06003301 RID: 13057 RVA: 0x001137B6 File Offset: 0x001119B6
	private void OnEmptyConduitCancelled(object data)
	{
		this.CancelEmptying();
	}

	// Token: 0x06003302 RID: 13058 RVA: 0x001137BE File Offset: 0x001119BE
	private StatusItem GetStatusItem()
	{
		return EmptySolidConduitWorkable.emptySolidConduitStatusItem;
	}

	// Token: 0x06003303 RID: 13059 RVA: 0x001137C8 File Offset: 0x001119C8
	private void CreateWorkChore()
	{
		base.GetComponent<Prioritizable>().AddRef();
		this.chore = new WorkChore<EmptySolidConduitWorkable>(Db.Get().ChoreTypes.EmptyStorage, this, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		this.chore.AddPrecondition(ChorePreconditions.instance.HasSkillPerk, Db.Get().SkillPerks.CanDoPlumbing.Id);
		this.elapsedTime = 0f;
		this.emptiedPipe = false;
		this.shouldShowSkillPerkStatusItem = true;
		this.UpdateStatusItem(null);
	}

	// Token: 0x06003304 RID: 13060 RVA: 0x00113858 File Offset: 0x00111A58
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		if (this.elapsedTime == -1f)
		{
			return true;
		}
		bool flag = false;
		this.elapsedTime += dt;
		if (!this.emptiedPipe)
		{
			if (this.elapsedTime > 4f)
			{
				this.EmptyContents();
				this.emptiedPipe = true;
				this.elapsedTime = 0f;
			}
		}
		else if (this.elapsedTime > 2f)
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			if (this.GetFlowManager().GetContents(num).pickupableHandle.IsValid())
			{
				this.elapsedTime = 0f;
				this.emptiedPipe = false;
			}
			else
			{
				this.CleanUpVisualization();
				this.chore = null;
				flag = true;
				this.shouldShowSkillPerkStatusItem = false;
				this.UpdateStatusItem(null);
			}
		}
		return flag;
	}

	// Token: 0x06003305 RID: 13061 RVA: 0x00113921 File Offset: 0x00111B21
	public override bool InstantlyFinish(Worker worker)
	{
		worker.Work(4f);
		return true;
	}

	// Token: 0x06003306 RID: 13062 RVA: 0x00113930 File Offset: 0x00111B30
	public void EmptyContents()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		this.GetFlowManager().RemovePickupable(num);
		this.elapsedTime = 0f;
	}

	// Token: 0x06003307 RID: 13063 RVA: 0x00113966 File Offset: 0x00111B66
	public override float GetPercentComplete()
	{
		return Mathf.Clamp01(this.elapsedTime / 4f);
	}

	// Token: 0x04001F5D RID: 8029
	[MyCmpReq]
	private SolidConduit conduit;

	// Token: 0x04001F5E RID: 8030
	private static StatusItem emptySolidConduitStatusItem;

	// Token: 0x04001F5F RID: 8031
	private Chore chore;

	// Token: 0x04001F60 RID: 8032
	private const float RECHECK_PIPE_INTERVAL = 2f;

	// Token: 0x04001F61 RID: 8033
	private const float TIME_TO_EMPTY_PIPE = 4f;

	// Token: 0x04001F62 RID: 8034
	private const float NO_EMPTY_SCHEDULED = -1f;

	// Token: 0x04001F63 RID: 8035
	[Serialize]
	private float elapsedTime = -1f;

	// Token: 0x04001F64 RID: 8036
	private bool emptiedPipe = true;

	// Token: 0x04001F65 RID: 8037
	private static readonly EventSystem.IntraObjectHandler<EmptySolidConduitWorkable> OnEmptyConduitCancelledDelegate = new EventSystem.IntraObjectHandler<EmptySolidConduitWorkable>(delegate(EmptySolidConduitWorkable component, object data)
	{
		component.OnEmptyConduitCancelled(data);
	});
}
