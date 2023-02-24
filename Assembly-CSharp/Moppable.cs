using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020004A4 RID: 1188
[AddComponentMenu("KMonoBehaviour/Workable/Moppable")]
public class Moppable : Workable, ISim1000ms, ISim200ms
{
	// Token: 0x06001ACB RID: 6859 RVA: 0x0008F5B0 File Offset: 0x0008D7B0
	private Moppable()
	{
		this.showProgressBar = false;
	}

	// Token: 0x06001ACC RID: 6860 RVA: 0x0008F60C File Offset: 0x0008D80C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Mopping;
		this.attributeConverter = Db.Get().AttributeConverters.TidyingSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Basekeeping.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		this.childRenderer = base.GetComponentInChildren<MeshRenderer>();
		Prioritizable.AddRef(base.gameObject);
	}

	// Token: 0x06001ACD RID: 6861 RVA: 0x0008F690 File Offset: 0x0008D890
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (!this.IsThereLiquid())
		{
			base.gameObject.DeleteObject();
			return;
		}
		Grid.Objects[Grid.PosToCell(base.gameObject), 8] = base.gameObject;
		new WorkChore<Moppable>(Db.Get().ChoreTypes.Mop, this, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		base.SetWorkTime(float.PositiveInfinity);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().MiscStatusItems.WaitingForMop, null);
		base.Subscribe<Moppable>(493375141, Moppable.OnRefreshUserMenuDelegate);
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_mop_dirtywater_kanim") };
		this.partitionerEntry = GameScenePartitioner.Instance.Add("Moppable.OnSpawn", base.gameObject, new Extents(Grid.PosToCell(this), new CellOffset[]
		{
			new CellOffset(0, 0)
		}), GameScenePartitioner.Instance.liquidChangedLayer, new Action<object>(this.OnLiquidChanged));
		this.Refresh();
		base.Subscribe<Moppable>(-1432940121, Moppable.OnReachableChangedDelegate);
		new ReachabilityMonitor.Instance(this).StartSM();
		SimAndRenderScheduler.instance.Remove(this);
	}

	// Token: 0x06001ACE RID: 6862 RVA: 0x0008F7DC File Offset: 0x0008D9DC
	private void OnRefreshUserMenu(object data)
	{
		Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("icon_cancel", UI.USERMENUACTIONS.CANCELMOP.NAME, new System.Action(this.OnCancel), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.CANCELMOP.TOOLTIP, true), 1f);
	}

	// Token: 0x06001ACF RID: 6863 RVA: 0x0008F836 File Offset: 0x0008DA36
	private void OnCancel()
	{
		DetailsScreen.Instance.Show(false);
		base.gameObject.Trigger(2127324410, null);
	}

	// Token: 0x06001AD0 RID: 6864 RVA: 0x0008F854 File Offset: 0x0008DA54
	protected override void OnStartWork(Worker worker)
	{
		SimAndRenderScheduler.instance.Add(this, false);
		this.Refresh();
		this.MopTick(this.amountMoppedPerTick);
	}

	// Token: 0x06001AD1 RID: 6865 RVA: 0x0008F874 File Offset: 0x0008DA74
	protected override void OnStopWork(Worker worker)
	{
		SimAndRenderScheduler.instance.Remove(this);
	}

	// Token: 0x06001AD2 RID: 6866 RVA: 0x0008F881 File Offset: 0x0008DA81
	protected override void OnCompleteWork(Worker worker)
	{
		SimAndRenderScheduler.instance.Remove(this);
	}

	// Token: 0x06001AD3 RID: 6867 RVA: 0x0008F88E File Offset: 0x0008DA8E
	public override bool InstantlyFinish(Worker worker)
	{
		this.MopTick(1000f);
		return true;
	}

	// Token: 0x06001AD4 RID: 6868 RVA: 0x0008F89C File Offset: 0x0008DA9C
	public void Sim1000ms(float dt)
	{
		if (this.amountMopped > 0f)
		{
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, GameUtil.GetFormattedMass(-this.amountMopped, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), base.transform, 1.5f, false);
			this.amountMopped = 0f;
		}
	}

	// Token: 0x06001AD5 RID: 6869 RVA: 0x0008F8F6 File Offset: 0x0008DAF6
	public void Sim200ms(float dt)
	{
		if (base.worker != null)
		{
			this.Refresh();
			this.MopTick(this.amountMoppedPerTick);
		}
	}

	// Token: 0x06001AD6 RID: 6870 RVA: 0x0008F918 File Offset: 0x0008DB18
	private void OnCellMopped(Sim.MassConsumedCallback mass_cb_info, object data)
	{
		if (this == null)
		{
			return;
		}
		if (mass_cb_info.mass > 0f)
		{
			this.amountMopped += mass_cb_info.mass;
			int num = Grid.PosToCell(this);
			SubstanceChunk substanceChunk = LiquidSourceManager.Instance.CreateChunk(ElementLoader.elements[(int)mass_cb_info.elemIdx], mass_cb_info.mass, mass_cb_info.temperature, mass_cb_info.diseaseIdx, mass_cb_info.diseaseCount, Grid.CellToPosCCC(num, Grid.SceneLayer.Ore));
			substanceChunk.transform.SetPosition(substanceChunk.transform.GetPosition() + new Vector3((UnityEngine.Random.value - 0.5f) * 0.5f, 0f, 0f));
		}
	}

	// Token: 0x06001AD7 RID: 6871 RVA: 0x0008F9D0 File Offset: 0x0008DBD0
	public static void MopCell(int cell, float amount, Action<Sim.MassConsumedCallback, object> cb)
	{
		if (Grid.Element[cell].IsLiquid)
		{
			int num = -1;
			if (cb != null)
			{
				num = Game.Instance.massConsumedCallbackManager.Add(cb, null, "Moppable").index;
			}
			SimMessages.ConsumeMass(cell, Grid.Element[cell].id, amount, 1, num);
		}
	}

	// Token: 0x06001AD8 RID: 6872 RVA: 0x0008FA24 File Offset: 0x0008DC24
	private void MopTick(float mopAmount)
	{
		int num = Grid.PosToCell(this);
		for (int i = 0; i < this.offsets.Length; i++)
		{
			int num2 = Grid.OffsetCell(num, this.offsets[i]);
			if (Grid.Element[num2].IsLiquid)
			{
				Moppable.MopCell(num2, mopAmount, new Action<Sim.MassConsumedCallback, object>(this.OnCellMopped));
			}
		}
	}

	// Token: 0x06001AD9 RID: 6873 RVA: 0x0008FA80 File Offset: 0x0008DC80
	private bool IsThereLiquid()
	{
		int num = Grid.PosToCell(this);
		bool flag = false;
		for (int i = 0; i < this.offsets.Length; i++)
		{
			int num2 = Grid.OffsetCell(num, this.offsets[i]);
			if (Grid.Element[num2].IsLiquid && Grid.Mass[num2] <= MopTool.maxMopAmt)
			{
				flag = true;
			}
		}
		return flag;
	}

	// Token: 0x06001ADA RID: 6874 RVA: 0x0008FAE0 File Offset: 0x0008DCE0
	private void Refresh()
	{
		if (!this.IsThereLiquid())
		{
			if (!this.destroyHandle.IsValid)
			{
				this.destroyHandle = GameScheduler.Instance.Schedule("DestroyMoppable", 1f, delegate(object moppable)
				{
					this.TryDestroy();
				}, this, null);
				return;
			}
		}
		else if (this.destroyHandle.IsValid)
		{
			this.destroyHandle.ClearScheduler();
		}
	}

	// Token: 0x06001ADB RID: 6875 RVA: 0x0008FB43 File Offset: 0x0008DD43
	private void OnLiquidChanged(object data)
	{
		this.Refresh();
	}

	// Token: 0x06001ADC RID: 6876 RVA: 0x0008FB4B File Offset: 0x0008DD4B
	private void TryDestroy()
	{
		if (this != null)
		{
			base.gameObject.DeleteObject();
		}
	}

	// Token: 0x06001ADD RID: 6877 RVA: 0x0008FB61 File Offset: 0x0008DD61
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
	}

	// Token: 0x06001ADE RID: 6878 RVA: 0x0008FB7C File Offset: 0x0008DD7C
	private void OnReachableChanged(object data)
	{
		if (this.childRenderer != null)
		{
			Material material = this.childRenderer.material;
			bool flag = (bool)data;
			if (material.color == Game.Instance.uiColours.Dig.invalidLocation)
			{
				return;
			}
			KSelectable component = base.GetComponent<KSelectable>();
			if (flag)
			{
				material.color = Game.Instance.uiColours.Dig.validLocation;
				component.RemoveStatusItem(Db.Get().BuildingStatusItems.MopUnreachable, false);
				return;
			}
			component.AddStatusItem(Db.Get().BuildingStatusItems.MopUnreachable, this);
			GameScheduler.Instance.Schedule("Locomotion Tutorial", 2f, delegate(object obj)
			{
				Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Locomotion, true);
			}, null, null);
			material.color = Game.Instance.uiColours.Dig.unreachable;
		}
	}

	// Token: 0x04000EE5 RID: 3813
	[MyCmpReq]
	private KSelectable Selectable;

	// Token: 0x04000EE6 RID: 3814
	[MyCmpAdd]
	private Prioritizable prioritizable;

	// Token: 0x04000EE7 RID: 3815
	public float amountMoppedPerTick = 1000f;

	// Token: 0x04000EE8 RID: 3816
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04000EE9 RID: 3817
	private SchedulerHandle destroyHandle;

	// Token: 0x04000EEA RID: 3818
	private float amountMopped;

	// Token: 0x04000EEB RID: 3819
	private MeshRenderer childRenderer;

	// Token: 0x04000EEC RID: 3820
	private CellOffset[] offsets = new CellOffset[]
	{
		new CellOffset(0, 0),
		new CellOffset(1, 0),
		new CellOffset(-1, 0)
	};

	// Token: 0x04000EED RID: 3821
	private static readonly EventSystem.IntraObjectHandler<Moppable> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Moppable>(delegate(Moppable component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x04000EEE RID: 3822
	private static readonly EventSystem.IntraObjectHandler<Moppable> OnReachableChangedDelegate = new EventSystem.IntraObjectHandler<Moppable>(delegate(Moppable component, object data)
	{
		component.OnReachableChanged(data);
	});
}
