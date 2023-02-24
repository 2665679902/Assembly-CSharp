using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020009CE RID: 2510
[SerializationConfig(MemberSerialization.OptIn)]
public class WaterCooler : StateMachineComponent<WaterCooler.StatesInstance>, IApproachable, IGameObjectEffectDescriptor
{
	// Token: 0x06004A96 RID: 19094 RVA: 0x001A1AD8 File Offset: 0x0019FCD8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		GameScheduler.Instance.Schedule("Scheduling Tutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Schedule, true);
		}, null, null);
		this.workables = new SocialGatheringPointWorkable[this.socializeOffsets.Length];
		for (int i = 0; i < this.workables.Length; i++)
		{
			Vector3 vector = Grid.CellToPosCBC(Grid.OffsetCell(Grid.PosToCell(this), this.socializeOffsets[i]), Grid.SceneLayer.Move);
			SocialGatheringPointWorkable socialGatheringPointWorkable = ChoreHelpers.CreateLocator("WaterCoolerWorkable", vector).AddOrGet<SocialGatheringPointWorkable>();
			socialGatheringPointWorkable.specificEffect = "Socialized";
			socialGatheringPointWorkable.SetWorkTime(this.workTime);
			this.workables[i] = socialGatheringPointWorkable;
		}
		this.chores = new Chore[this.socializeOffsets.Length];
		Extents extents = new Extents(Grid.PosToCell(this), this.socializeOffsets);
		this.validNavCellChangedPartitionerEntry = GameScenePartitioner.Instance.Add("WaterCooler", this, extents, GameScenePartitioner.Instance.validNavCellChangedLayer, new Action<object>(this.OnCellChanged));
		base.Subscribe<WaterCooler>(-1697596308, WaterCooler.OnStorageChangeDelegate);
		base.smi.StartSM();
	}

	// Token: 0x06004A97 RID: 19095 RVA: 0x001A1C08 File Offset: 0x0019FE08
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.validNavCellChangedPartitionerEntry);
		this.CancelDrinkChores();
		for (int i = 0; i < this.workables.Length; i++)
		{
			if (this.workables[i])
			{
				Util.KDestroyGameObject(this.workables[i]);
				this.workables[i] = null;
			}
		}
		base.OnCleanUp();
	}

	// Token: 0x06004A98 RID: 19096 RVA: 0x001A1C6C File Offset: 0x0019FE6C
	public void UpdateDrinkChores(bool force = true)
	{
		if (!force && !this.choresDirty)
		{
			return;
		}
		float num = this.storage.GetMassAvailable(GameTags.Water);
		int num2 = 0;
		for (int i = 0; i < this.socializeOffsets.Length; i++)
		{
			CellOffset cellOffset = this.socializeOffsets[i];
			Chore chore = this.chores[i];
			if (num2 < this.choreCount && this.IsOffsetValid(cellOffset) && num >= 1f)
			{
				num2++;
				num -= 1f;
				if (chore == null || chore.isComplete)
				{
					this.chores[i] = new WaterCoolerChore(this, this.workables[i], null, null, new Action<Chore>(this.OnChoreEnd));
				}
			}
			else if (chore != null)
			{
				chore.Cancel("invalid");
				this.chores[i] = null;
			}
		}
		this.choresDirty = false;
	}

	// Token: 0x06004A99 RID: 19097 RVA: 0x001A1D4C File Offset: 0x0019FF4C
	public void CancelDrinkChores()
	{
		for (int i = 0; i < this.socializeOffsets.Length; i++)
		{
			Chore chore = this.chores[i];
			if (chore != null)
			{
				chore.Cancel("cancelled");
				this.chores[i] = null;
			}
		}
	}

	// Token: 0x06004A9A RID: 19098 RVA: 0x001A1D8C File Offset: 0x0019FF8C
	private bool IsOffsetValid(CellOffset offset)
	{
		int num = Grid.OffsetCell(Grid.PosToCell(this), offset);
		int num2 = Grid.CellBelow(num);
		return GameNavGrids.FloorValidator.IsWalkableCell(num, num2, false);
	}

	// Token: 0x06004A9B RID: 19099 RVA: 0x001A1DB3 File Offset: 0x0019FFB3
	private void OnChoreEnd(Chore chore)
	{
		this.choresDirty = true;
	}

	// Token: 0x06004A9C RID: 19100 RVA: 0x001A1DBC File Offset: 0x0019FFBC
	private void OnCellChanged(object data)
	{
		this.choresDirty = true;
	}

	// Token: 0x06004A9D RID: 19101 RVA: 0x001A1DC5 File Offset: 0x0019FFC5
	private void OnStorageChange(object data)
	{
		this.choresDirty = true;
	}

	// Token: 0x06004A9E RID: 19102 RVA: 0x001A1DCE File Offset: 0x0019FFCE
	public CellOffset[] GetOffsets()
	{
		return this.drinkOffsets;
	}

	// Token: 0x06004A9F RID: 19103 RVA: 0x001A1DD6 File Offset: 0x0019FFD6
	public int GetCell()
	{
		return Grid.PosToCell(this);
	}

	// Token: 0x06004AA0 RID: 19104 RVA: 0x001A1DE0 File Offset: 0x0019FFE0
	private void AddRequirementDesc(List<Descriptor> descs, Tag tag, float mass)
	{
		string text = tag.ProperName();
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTCONSUMEDPERUSE, text, GameUtil.GetFormattedMass(mass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTCONSUMEDPERUSE, text, GameUtil.GetFormattedMass(mass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), Descriptor.DescriptorType.Requirement);
		descs.Add(descriptor);
	}

	// Token: 0x06004AA1 RID: 19105 RVA: 0x001A1E48 File Offset: 0x001A0048
	List<Descriptor> IGameObjectEffectDescriptor.GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(UI.BUILDINGEFFECTS.RECREATION, UI.BUILDINGEFFECTS.TOOLTIPS.RECREATION, Descriptor.DescriptorType.Effect);
		list.Add(descriptor);
		Effect.AddModifierDescriptions(base.gameObject, list, "Socialized", true);
		this.AddRequirementDesc(list, GameTags.Water, 1f);
		return list;
	}

	// Token: 0x04003100 RID: 12544
	public const float DRINK_MASS = 1f;

	// Token: 0x04003101 RID: 12545
	public const string SPECIFIC_EFFECT = "Socialized";

	// Token: 0x04003102 RID: 12546
	public CellOffset[] socializeOffsets = new CellOffset[]
	{
		new CellOffset(-1, 0),
		new CellOffset(2, 0),
		new CellOffset(0, 0),
		new CellOffset(1, 0)
	};

	// Token: 0x04003103 RID: 12547
	public int choreCount = 2;

	// Token: 0x04003104 RID: 12548
	public float workTime = 5f;

	// Token: 0x04003105 RID: 12549
	private CellOffset[] drinkOffsets = new CellOffset[]
	{
		new CellOffset(0, 0),
		new CellOffset(1, 0)
	};

	// Token: 0x04003106 RID: 12550
	private Chore[] chores;

	// Token: 0x04003107 RID: 12551
	private HandleVector<int>.Handle validNavCellChangedPartitionerEntry;

	// Token: 0x04003108 RID: 12552
	private SocialGatheringPointWorkable[] workables;

	// Token: 0x04003109 RID: 12553
	[MyCmpGet]
	private Storage storage;

	// Token: 0x0400310A RID: 12554
	public bool choresDirty;

	// Token: 0x0400310B RID: 12555
	private static readonly EventSystem.IntraObjectHandler<WaterCooler> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<WaterCooler>(delegate(WaterCooler component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x020017CE RID: 6094
	public class States : GameStateMachine<WaterCooler.States, WaterCooler.StatesInstance, WaterCooler>
	{
		// Token: 0x06008BF9 RID: 35833 RVA: 0x00300EC0 File Offset: 0x002FF0C0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.unoperational;
			this.unoperational.TagTransition(GameTags.Operational, this.waitingfordelivery, false).PlayAnim("off");
			this.waitingfordelivery.TagTransition(GameTags.Operational, this.unoperational, true).Transition(this.dispensing, (WaterCooler.StatesInstance smi) => smi.HasMinimumMass(), UpdateRate.SIM_200ms).EventTransition(GameHashes.OnStorageChange, this.dispensing, (WaterCooler.StatesInstance smi) => smi.HasMinimumMass())
				.PlayAnim("off");
			this.dispensing.Enter("StartMeter", delegate(WaterCooler.StatesInstance smi)
			{
				smi.StartMeter();
			}).Enter("UpdateDrinkChores.force", delegate(WaterCooler.StatesInstance smi)
			{
				smi.master.UpdateDrinkChores(true);
			}).Update("UpdateDrinkChores", delegate(WaterCooler.StatesInstance smi, float dt)
			{
				smi.master.UpdateDrinkChores(true);
			}, UpdateRate.SIM_200ms, false)
				.Exit("CancelDrinkChores", delegate(WaterCooler.StatesInstance smi)
				{
					smi.master.CancelDrinkChores();
				})
				.TagTransition(GameTags.Operational, this.unoperational, true)
				.EventTransition(GameHashes.OnStorageChange, this.waitingfordelivery, (WaterCooler.StatesInstance smi) => !smi.HasMinimumMass())
				.PlayAnim("working");
		}

		// Token: 0x04006E16 RID: 28182
		public GameStateMachine<WaterCooler.States, WaterCooler.StatesInstance, WaterCooler, object>.State unoperational;

		// Token: 0x04006E17 RID: 28183
		public GameStateMachine<WaterCooler.States, WaterCooler.StatesInstance, WaterCooler, object>.State waitingfordelivery;

		// Token: 0x04006E18 RID: 28184
		public GameStateMachine<WaterCooler.States, WaterCooler.StatesInstance, WaterCooler, object>.State dispensing;
	}

	// Token: 0x020017CF RID: 6095
	public class StatesInstance : GameStateMachine<WaterCooler.States, WaterCooler.StatesInstance, WaterCooler, object>.GameInstance
	{
		// Token: 0x06008BFB RID: 35835 RVA: 0x00301074 File Offset: 0x002FF274
		public StatesInstance(WaterCooler smi)
			: base(smi)
		{
			this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_bottle", "meter", Meter.Offset.Behind, Grid.SceneLayer.NoLayer, new string[] { "meter_bottle" });
			this.storage = base.master.GetComponent<Storage>();
			base.Subscribe(-1697596308, new Action<object>(this.OnStorageChange));
		}

		// Token: 0x06008BFC RID: 35836 RVA: 0x003010DC File Offset: 0x002FF2DC
		private void OnStorageChange(object data)
		{
			float num = Mathf.Clamp01(this.storage.MassStored() / this.storage.capacityKg);
			this.meter.SetPositionPercent(num);
		}

		// Token: 0x06008BFD RID: 35837 RVA: 0x00301114 File Offset: 0x002FF314
		public void StartMeter()
		{
			PrimaryElement primaryElement = this.storage.FindFirstWithMass(GameTags.Water, 0f);
			if (primaryElement == null)
			{
				return;
			}
			this.meter.SetSymbolTint(new KAnimHashedString("meter_water"), primaryElement.Element.substance.colour);
			this.OnStorageChange(null);
		}

		// Token: 0x06008BFE RID: 35838 RVA: 0x0030116D File Offset: 0x002FF36D
		public bool HasMinimumMass()
		{
			return this.storage.GetMassAvailable(GameTags.Water) >= 1f;
		}

		// Token: 0x04006E19 RID: 28185
		private Storage storage;

		// Token: 0x04006E1A RID: 28186
		private MeterController meter;
	}
}
