using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020005C9 RID: 1481
public class GourmetCookingStation : ComplexFabricator, IGameObjectEffectDescriptor
{
	// Token: 0x060024D8 RID: 9432 RVA: 0x000C7390 File Offset: 0x000C5590
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.keepAdditionalTag = this.fuelTag;
		this.choreType = Db.Get().ChoreTypes.Cook;
		this.fetchChoreTypeIdHash = Db.Get().ChoreTypes.CookFetch.IdHash;
	}

	// Token: 0x060024D9 RID: 9433 RVA: 0x000C73E0 File Offset: 0x000C55E0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.workable.requiredSkillPerk = Db.Get().SkillPerks.CanElectricGrill.Id;
		this.workable.WorkerStatusItem = Db.Get().DuplicantStatusItems.Cooking;
		this.workable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_cookstation_gourtmet_kanim") };
		this.workable.AttributeConverter = Db.Get().AttributeConverters.CookingSpeed;
		this.workable.AttributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.MOST_DAY_EXPERIENCE;
		this.workable.SkillExperienceSkillGroup = Db.Get().SkillGroups.Cooking.Id;
		this.workable.SkillExperienceMultiplier = SKILLS.MOST_DAY_EXPERIENCE;
		ComplexFabricatorWorkable workable = this.workable;
		workable.OnWorkTickActions = (Action<Worker, float>)Delegate.Combine(workable.OnWorkTickActions, new Action<Worker, float>(delegate(Worker worker, float dt)
		{
			global::Debug.Assert(worker != null, "How did we get a null worker?");
			if (this.diseaseCountKillRate > 0)
			{
				PrimaryElement component = base.GetComponent<PrimaryElement>();
				int num = Math.Max(1, (int)((float)this.diseaseCountKillRate * dt));
				component.ModifyDiseaseCount(-num, "GourmetCookingStation");
			}
		}));
		this.smi = new GourmetCookingStation.StatesInstance(this);
		this.smi.StartSM();
	}

	// Token: 0x060024DA RID: 9434 RVA: 0x000C74E6 File Offset: 0x000C56E6
	public float GetAvailableFuel()
	{
		return this.inStorage.GetAmountAvailable(this.fuelTag);
	}

	// Token: 0x060024DB RID: 9435 RVA: 0x000C74FC File Offset: 0x000C56FC
	protected override List<GameObject> SpawnOrderProduct(ComplexRecipe recipe)
	{
		List<GameObject> list = base.SpawnOrderProduct(recipe);
		foreach (GameObject gameObject in list)
		{
			PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
			component.ModifyDiseaseCount(-component.DiseaseCount, "GourmetCookingStation.CompleteOrder");
		}
		base.GetComponent<Operational>().SetActive(false, false);
		return list;
	}

	// Token: 0x060024DC RID: 9436 RVA: 0x000C7570 File Offset: 0x000C5770
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> descriptors = base.GetDescriptors(go);
		descriptors.Add(new Descriptor(UI.BUILDINGEFFECTS.REMOVES_DISEASE, UI.BUILDINGEFFECTS.TOOLTIPS.REMOVES_DISEASE, Descriptor.DescriptorType.Effect, false));
		return descriptors;
	}

	// Token: 0x0400153A RID: 5434
	private static readonly Operational.Flag gourmetCookingStationFlag = new Operational.Flag("gourmet_cooking_station", Operational.Flag.Type.Requirement);

	// Token: 0x0400153B RID: 5435
	public float GAS_CONSUMPTION_RATE;

	// Token: 0x0400153C RID: 5436
	public float GAS_CONVERSION_RATIO = 0.1f;

	// Token: 0x0400153D RID: 5437
	public const float START_FUEL_MASS = 5f;

	// Token: 0x0400153E RID: 5438
	public Tag fuelTag;

	// Token: 0x0400153F RID: 5439
	[SerializeField]
	private int diseaseCountKillRate = 150;

	// Token: 0x04001540 RID: 5440
	private GourmetCookingStation.StatesInstance smi;

	// Token: 0x02001211 RID: 4625
	public class StatesInstance : GameStateMachine<GourmetCookingStation.States, GourmetCookingStation.StatesInstance, GourmetCookingStation, object>.GameInstance
	{
		// Token: 0x060078E4 RID: 30948 RVA: 0x002C0E96 File Offset: 0x002BF096
		public StatesInstance(GourmetCookingStation smi)
			: base(smi)
		{
		}
	}

	// Token: 0x02001212 RID: 4626
	public class States : GameStateMachine<GourmetCookingStation.States, GourmetCookingStation.StatesInstance, GourmetCookingStation>
	{
		// Token: 0x060078E5 RID: 30949 RVA: 0x002C0EA0 File Offset: 0x002BF0A0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			if (GourmetCookingStation.States.waitingForFuelStatus == null)
			{
				GourmetCookingStation.States.waitingForFuelStatus = new StatusItem("waitingForFuelStatus", BUILDING.STATUSITEMS.ENOUGH_FUEL.NAME, BUILDING.STATUSITEMS.ENOUGH_FUEL.TOOLTIP, "status_item_no_gas_to_pump", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, 129022, true, null);
				GourmetCookingStation.States.waitingForFuelStatus.resolveStringCallback = delegate(string str, object obj)
				{
					GourmetCookingStation gourmetCookingStation = (GourmetCookingStation)obj;
					return string.Format(str, gourmetCookingStation.fuelTag.ProperName(), GameUtil.GetFormattedMass(5f, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				};
			}
			default_state = this.waitingForFuel;
			this.waitingForFuel.Enter(delegate(GourmetCookingStation.StatesInstance smi)
			{
				smi.master.operational.SetFlag(GourmetCookingStation.gourmetCookingStationFlag, false);
			}).ToggleStatusItem(GourmetCookingStation.States.waitingForFuelStatus, (GourmetCookingStation.StatesInstance smi) => smi.master).EventTransition(GameHashes.OnStorageChange, this.ready, (GourmetCookingStation.StatesInstance smi) => smi.master.GetAvailableFuel() >= 5f);
			this.ready.Enter(delegate(GourmetCookingStation.StatesInstance smi)
			{
				smi.master.SetQueueDirty();
				smi.master.operational.SetFlag(GourmetCookingStation.gourmetCookingStationFlag, true);
			}).EventTransition(GameHashes.OnStorageChange, this.waitingForFuel, (GourmetCookingStation.StatesInstance smi) => smi.master.GetAvailableFuel() <= 0f);
		}

		// Token: 0x04005CF0 RID: 23792
		public static StatusItem waitingForFuelStatus;

		// Token: 0x04005CF1 RID: 23793
		public GameStateMachine<GourmetCookingStation.States, GourmetCookingStation.StatesInstance, GourmetCookingStation, object>.State waitingForFuel;

		// Token: 0x04005CF2 RID: 23794
		public GameStateMachine<GourmetCookingStation.States, GourmetCookingStation.StatesInstance, GourmetCookingStation, object>.State ready;
	}
}
