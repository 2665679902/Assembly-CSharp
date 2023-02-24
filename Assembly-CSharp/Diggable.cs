using System;
using System.Collections;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000474 RID: 1140
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/Workable/Diggable")]
public class Diggable : Workable
{
	// Token: 0x170000ED RID: 237
	// (get) Token: 0x0600194C RID: 6476 RVA: 0x0008777B File Offset: 0x0008597B
	public bool Reachable
	{
		get
		{
			return this.isReachable;
		}
	}

	// Token: 0x0600194D RID: 6477 RVA: 0x00087784 File Offset: 0x00085984
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Digging;
		this.readyForSkillWorkStatusItem = Db.Get().BuildingStatusItems.DigRequiresSkillPerk;
		this.faceTargetWhenWorking = true;
		base.Subscribe<Diggable>(-1432940121, Diggable.OnReachableChangedDelegate);
		this.attributeConverter = Db.Get().AttributeConverters.DiggingSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.MOST_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Mining.Id;
		this.skillExperienceMultiplier = SKILLS.MOST_DAY_EXPERIENCE;
		this.multitoolContext = "dig";
		this.multitoolHitEffectTag = "fx_dig_splash";
		this.workingPstComplete = null;
		this.workingPstFailed = null;
		Prioritizable.AddRef(base.gameObject);
	}

	// Token: 0x0600194E RID: 6478 RVA: 0x00087857 File Offset: 0x00085A57
	private Diggable()
	{
		base.SetOffsetTable(OffsetGroups.InvertedStandardTableWithCorners);
	}

	// Token: 0x0600194F RID: 6479 RVA: 0x0008786C File Offset: 0x00085A6C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		int num = Grid.PosToCell(this);
		this.originalDigElement = Grid.Element[num];
		if (this.originalDigElement.hardness == 255)
		{
			this.OnCancel();
		}
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().MiscStatusItems.WaitingForDig, null);
		this.UpdateColor(this.isReachable);
		Grid.Objects[num, 7] = base.gameObject;
		ChoreType choreType = Db.Get().ChoreTypes.Dig;
		if (this.choreTypeIdHash.IsValid)
		{
			choreType = Db.Get().ChoreTypes.GetByHash(this.choreTypeIdHash);
		}
		this.chore = new WorkChore<Diggable>(choreType, this, null, true, null, null, null, true, null, false, true, null, true, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		base.SetWorkTime(float.PositiveInfinity);
		this.partitionerEntry = GameScenePartitioner.Instance.Add("Diggable.OnSpawn", base.gameObject, Grid.PosToCell(this), GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnSolidChanged));
		this.OnSolidChanged(null);
		new ReachabilityMonitor.Instance(this).StartSM();
		base.Subscribe<Diggable>(493375141, Diggable.OnRefreshUserMenuDelegate);
		this.handle = Game.Instance.Subscribe(-1523247426, new Action<object>(this.UpdateStatusItem));
		Components.Diggables.Add(this);
	}

	// Token: 0x06001950 RID: 6480 RVA: 0x000879D8 File Offset: 0x00085BD8
	public override Workable.AnimInfo GetAnim(Worker worker)
	{
		Workable.AnimInfo animInfo = default(Workable.AnimInfo);
		if (this.overrideAnims != null && this.overrideAnims.Length != 0)
		{
			animInfo.overrideAnims = this.overrideAnims;
		}
		if (this.multitoolContext.IsValid && this.multitoolHitEffectTag.IsValid)
		{
			animInfo.smi = new MultitoolController.Instance(this, worker, this.multitoolContext, Assets.GetPrefab(this.multitoolHitEffectTag));
		}
		return animInfo;
	}

	// Token: 0x06001951 RID: 6481 RVA: 0x00087A48 File Offset: 0x00085C48
	private static bool IsCellBuildable(int cell)
	{
		bool flag = false;
		GameObject gameObject = Grid.Objects[cell, 1];
		if (gameObject != null && gameObject.GetComponent<Constructable>() != null)
		{
			flag = true;
		}
		return flag;
	}

	// Token: 0x06001952 RID: 6482 RVA: 0x00087A7E File Offset: 0x00085C7E
	private IEnumerator PeriodicUnstableFallingRecheck()
	{
		yield return SequenceUtil.WaitForSeconds(2f);
		this.OnSolidChanged(null);
		yield break;
	}

	// Token: 0x06001953 RID: 6483 RVA: 0x00087A90 File Offset: 0x00085C90
	private void OnSolidChanged(object data)
	{
		if (this == null || base.gameObject == null)
		{
			return;
		}
		GameScenePartitioner.Instance.Free(ref this.unstableEntry);
		int num = Grid.PosToCell(this);
		int num2 = -1;
		this.UpdateColor(this.isReachable);
		if (Grid.Element[num].hardness == 255)
		{
			this.UpdateColor(false);
			this.requiredSkillPerk = null;
			this.chore.AddPrecondition(ChorePreconditions.instance.HasSkillPerk, Db.Get().SkillPerks.CanDigUnobtanium);
		}
		else if (Grid.Element[num].hardness >= 251)
		{
			bool flag = false;
			using (List<Chore.PreconditionInstance>.Enumerator enumerator = this.chore.GetPreconditions().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.id == ChorePreconditions.instance.HasSkillPerk.id)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				this.chore.AddPrecondition(ChorePreconditions.instance.HasSkillPerk, Db.Get().SkillPerks.CanDigRadioactiveMaterials);
			}
			this.requiredSkillPerk = Db.Get().SkillPerks.CanDigRadioactiveMaterials.Id;
			this.materialDisplay.sharedMaterial = this.materials[3];
		}
		else if (Grid.Element[num].hardness >= 200)
		{
			bool flag2 = false;
			using (List<Chore.PreconditionInstance>.Enumerator enumerator = this.chore.GetPreconditions().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.id == ChorePreconditions.instance.HasSkillPerk.id)
					{
						flag2 = true;
						break;
					}
				}
			}
			if (!flag2)
			{
				this.chore.AddPrecondition(ChorePreconditions.instance.HasSkillPerk, Db.Get().SkillPerks.CanDigSuperDuperHard);
			}
			this.requiredSkillPerk = Db.Get().SkillPerks.CanDigSuperDuperHard.Id;
			this.materialDisplay.sharedMaterial = this.materials[3];
		}
		else if (Grid.Element[num].hardness >= 150)
		{
			bool flag3 = false;
			using (List<Chore.PreconditionInstance>.Enumerator enumerator = this.chore.GetPreconditions().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.id == ChorePreconditions.instance.HasSkillPerk.id)
					{
						flag3 = true;
						break;
					}
				}
			}
			if (!flag3)
			{
				this.chore.AddPrecondition(ChorePreconditions.instance.HasSkillPerk, Db.Get().SkillPerks.CanDigNearlyImpenetrable);
			}
			this.requiredSkillPerk = Db.Get().SkillPerks.CanDigNearlyImpenetrable.Id;
			this.materialDisplay.sharedMaterial = this.materials[2];
		}
		else if (Grid.Element[num].hardness >= 50)
		{
			bool flag4 = false;
			using (List<Chore.PreconditionInstance>.Enumerator enumerator = this.chore.GetPreconditions().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.id == ChorePreconditions.instance.HasSkillPerk.id)
					{
						flag4 = true;
						break;
					}
				}
			}
			if (!flag4)
			{
				this.chore.AddPrecondition(ChorePreconditions.instance.HasSkillPerk, Db.Get().SkillPerks.CanDigVeryFirm);
			}
			this.requiredSkillPerk = Db.Get().SkillPerks.CanDigVeryFirm.Id;
			this.materialDisplay.sharedMaterial = this.materials[1];
		}
		else
		{
			this.requiredSkillPerk = null;
			this.chore.GetPreconditions().Remove(this.chore.GetPreconditions().Find((Chore.PreconditionInstance o) => o.id == ChorePreconditions.instance.HasSkillPerk.id));
		}
		this.UpdateStatusItem(null);
		bool flag5 = false;
		if (!Grid.Solid[num])
		{
			num2 = Diggable.GetUnstableCellAbove(num);
			if (num2 == -1)
			{
				flag5 = true;
			}
			else
			{
				base.StartCoroutine("PeriodicUnstableFallingRecheck");
			}
		}
		else if (Grid.Foundation[num])
		{
			flag5 = true;
		}
		if (!flag5)
		{
			if (num2 != -1)
			{
				Extents extents = default(Extents);
				Grid.CellToXY(num, out extents.x, out extents.y);
				extents.width = 1;
				extents.height = (num2 - num + Grid.WidthInCells - 1) / Grid.WidthInCells + 1;
				this.unstableEntry = GameScenePartitioner.Instance.Add("Diggable.OnSolidChanged", base.gameObject, extents, GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnSolidChanged));
			}
			return;
		}
		this.isDigComplete = true;
		if (this.chore == null || !this.chore.InProgress())
		{
			Util.KDestroyGameObject(base.gameObject);
			return;
		}
		base.GetComponentInChildren<MeshRenderer>().enabled = false;
	}

	// Token: 0x06001954 RID: 6484 RVA: 0x00087FA4 File Offset: 0x000861A4
	public Element GetTargetElement()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		return Grid.Element[num];
	}

	// Token: 0x06001955 RID: 6485 RVA: 0x00087FC9 File Offset: 0x000861C9
	public override string GetConversationTopic()
	{
		return this.originalDigElement.tag.Name;
	}

	// Token: 0x06001956 RID: 6486 RVA: 0x00087FDB File Offset: 0x000861DB
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		Diggable.DoDigTick(Grid.PosToCell(this), dt);
		return this.isDigComplete;
	}

	// Token: 0x06001957 RID: 6487 RVA: 0x00087FEF File Offset: 0x000861EF
	protected override void OnStopWork(Worker worker)
	{
		if (this.isDigComplete)
		{
			Util.KDestroyGameObject(base.gameObject);
		}
	}

	// Token: 0x06001958 RID: 6488 RVA: 0x00088004 File Offset: 0x00086204
	public override bool InstantlyFinish(Worker worker)
	{
		int num = Grid.PosToCell(this);
		if (Grid.Element[num].hardness == 255)
		{
			return false;
		}
		float approximateDigTime = Diggable.GetApproximateDigTime(num);
		worker.Work(approximateDigTime);
		return true;
	}

	// Token: 0x06001959 RID: 6489 RVA: 0x00088040 File Offset: 0x00086240
	public static void DoDigTick(int cell, float dt)
	{
		float approximateDigTime = Diggable.GetApproximateDigTime(cell);
		float num = dt / approximateDigTime;
		WorldDamage.Instance.ApplyDamage(cell, num, -1, null, null);
	}

	// Token: 0x0600195A RID: 6490 RVA: 0x00088068 File Offset: 0x00086268
	public static float GetApproximateDigTime(int cell)
	{
		float num = (float)Grid.Element[cell].hardness;
		if (num == 255f)
		{
			return float.MaxValue;
		}
		Element element = ElementLoader.FindElementByHash(SimHashes.Ice);
		float num2 = num / (float)element.hardness;
		float num3 = Mathf.Min(Grid.Mass[cell], 400f) / 400f;
		float num4 = 4f * num3;
		return num4 + num2 * num4;
	}

	// Token: 0x0600195B RID: 6491 RVA: 0x000880D4 File Offset: 0x000862D4
	public static Diggable GetDiggable(int cell)
	{
		GameObject gameObject = Grid.Objects[cell, 7];
		if (gameObject != null)
		{
			return gameObject.GetComponent<Diggable>();
		}
		return null;
	}

	// Token: 0x0600195C RID: 6492 RVA: 0x000880FF File Offset: 0x000862FF
	public static bool IsDiggable(int cell)
	{
		if (Grid.Solid[cell])
		{
			return !Grid.Foundation[cell];
		}
		return Diggable.GetUnstableCellAbove(cell) != Grid.InvalidCell;
	}

	// Token: 0x0600195D RID: 6493 RVA: 0x00088130 File Offset: 0x00086330
	private static int GetUnstableCellAbove(int cell)
	{
		Vector2I vector2I = Grid.CellToXY(cell);
		List<int> cellsContainingFallingAbove = World.Instance.GetComponent<UnstableGroundManager>().GetCellsContainingFallingAbove(vector2I);
		if (cellsContainingFallingAbove.Contains(cell))
		{
			return cell;
		}
		byte b = Grid.WorldIdx[cell];
		int num = Grid.CellAbove(cell);
		while (Grid.IsValidCell(num) && Grid.WorldIdx[num] == b)
		{
			if (Grid.Foundation[num])
			{
				return Grid.InvalidCell;
			}
			if (Grid.Solid[num])
			{
				if (Grid.Element[num].IsUnstable)
				{
					return num;
				}
				return Grid.InvalidCell;
			}
			else
			{
				if (cellsContainingFallingAbove.Contains(num))
				{
					return num;
				}
				num = Grid.CellAbove(num);
			}
		}
		return Grid.InvalidCell;
	}

	// Token: 0x0600195E RID: 6494 RVA: 0x000881D0 File Offset: 0x000863D0
	public static bool RequiresTool(Element e)
	{
		return false;
	}

	// Token: 0x0600195F RID: 6495 RVA: 0x000881D3 File Offset: 0x000863D3
	public static bool Undiggable(Element e)
	{
		return e.id == SimHashes.Unobtanium;
	}

	// Token: 0x06001960 RID: 6496 RVA: 0x000881E4 File Offset: 0x000863E4
	private void OnReachableChanged(object data)
	{
		if (this.childRenderer == null)
		{
			this.childRenderer = base.GetComponentInChildren<MeshRenderer>();
		}
		Material material = this.childRenderer.material;
		this.isReachable = (bool)data;
		if (material.color == Game.Instance.uiColours.Dig.invalidLocation)
		{
			return;
		}
		this.UpdateColor(this.isReachable);
		KSelectable component = base.GetComponent<KSelectable>();
		if (this.isReachable)
		{
			component.RemoveStatusItem(Db.Get().BuildingStatusItems.DigUnreachable, false);
			return;
		}
		component.AddStatusItem(Db.Get().BuildingStatusItems.DigUnreachable, this);
		GameScheduler.Instance.Schedule("Locomotion Tutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Locomotion, true);
		}, null, null);
	}

	// Token: 0x06001961 RID: 6497 RVA: 0x000882C4 File Offset: 0x000864C4
	private void UpdateColor(bool reachable)
	{
		if (this.childRenderer != null)
		{
			Material material = this.childRenderer.material;
			if (Diggable.RequiresTool(Grid.Element[Grid.PosToCell(base.gameObject)]) || Diggable.Undiggable(Grid.Element[Grid.PosToCell(base.gameObject)]))
			{
				material.color = Game.Instance.uiColours.Dig.invalidLocation;
				return;
			}
			if (Grid.Element[Grid.PosToCell(base.gameObject)].hardness >= 50)
			{
				if (reachable)
				{
					material.color = Game.Instance.uiColours.Dig.validLocation;
				}
				else
				{
					material.color = Game.Instance.uiColours.Dig.unreachable;
				}
				this.multitoolContext = Diggable.lasersForHardness[1].first;
				this.multitoolHitEffectTag = Diggable.lasersForHardness[1].second;
				return;
			}
			if (reachable)
			{
				material.color = Game.Instance.uiColours.Dig.validLocation;
			}
			else
			{
				material.color = Game.Instance.uiColours.Dig.unreachable;
			}
			this.multitoolContext = Diggable.lasersForHardness[0].first;
			this.multitoolHitEffectTag = Diggable.lasersForHardness[0].second;
		}
	}

	// Token: 0x06001962 RID: 6498 RVA: 0x00088428 File Offset: 0x00086628
	public override float GetPercentComplete()
	{
		return Grid.Damage[Grid.PosToCell(this)];
	}

	// Token: 0x06001963 RID: 6499 RVA: 0x00088438 File Offset: 0x00086638
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		GameScenePartitioner.Instance.Free(ref this.unstableEntry);
		Game.Instance.Unsubscribe(this.handle);
		int num = Grid.PosToCell(this);
		GameScenePartitioner.Instance.TriggerEvent(num, GameScenePartitioner.Instance.digDestroyedLayer, null);
		Components.Diggables.Remove(this);
	}

	// Token: 0x06001964 RID: 6500 RVA: 0x000884A3 File Offset: 0x000866A3
	private void OnCancel()
	{
		if (DetailsScreen.Instance != null)
		{
			DetailsScreen.Instance.Show(false);
		}
		base.gameObject.Trigger(2127324410, null);
	}

	// Token: 0x06001965 RID: 6501 RVA: 0x000884D0 File Offset: 0x000866D0
	private void OnRefreshUserMenu(object data)
	{
		Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("icon_cancel", UI.USERMENUACTIONS.CANCELDIG.NAME, new System.Action(this.OnCancel), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.CANCELDIG.TOOLTIP, true), 1f);
	}

	// Token: 0x04000E24 RID: 3620
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04000E25 RID: 3621
	private HandleVector<int>.Handle unstableEntry;

	// Token: 0x04000E26 RID: 3622
	private MeshRenderer childRenderer;

	// Token: 0x04000E27 RID: 3623
	private bool isReachable;

	// Token: 0x04000E28 RID: 3624
	private Element originalDigElement;

	// Token: 0x04000E29 RID: 3625
	[MyCmpAdd]
	private Prioritizable prioritizable;

	// Token: 0x04000E2A RID: 3626
	[SerializeField]
	public HashedString choreTypeIdHash;

	// Token: 0x04000E2B RID: 3627
	[SerializeField]
	public Material[] materials;

	// Token: 0x04000E2C RID: 3628
	[SerializeField]
	public MeshRenderer materialDisplay;

	// Token: 0x04000E2D RID: 3629
	private bool isDigComplete;

	// Token: 0x04000E2E RID: 3630
	private static List<global::Tuple<string, Tag>> lasersForHardness = new List<global::Tuple<string, Tag>>
	{
		new global::Tuple<string, Tag>("dig", "fx_dig_splash"),
		new global::Tuple<string, Tag>("specialistdig", "fx_dig_splash")
	};

	// Token: 0x04000E2F RID: 3631
	private int handle;

	// Token: 0x04000E30 RID: 3632
	private static readonly EventSystem.IntraObjectHandler<Diggable> OnReachableChangedDelegate = new EventSystem.IntraObjectHandler<Diggable>(delegate(Diggable component, object data)
	{
		component.OnReachableChanged(data);
	});

	// Token: 0x04000E31 RID: 3633
	private static readonly EventSystem.IntraObjectHandler<Diggable> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Diggable>(delegate(Diggable component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x04000E32 RID: 3634
	public Chore chore;
}
