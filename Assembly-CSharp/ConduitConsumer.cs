using System;
using STRINGS;
using UnityEngine;

// Token: 0x020006A7 RID: 1703
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/ConduitConsumer")]
public class ConduitConsumer : KMonoBehaviour, IConduitConsumer
{
	// Token: 0x17000338 RID: 824
	// (get) Token: 0x06002E27 RID: 11815 RVA: 0x000F380A File Offset: 0x000F1A0A
	public Storage Storage
	{
		get
		{
			return this.storage;
		}
	}

	// Token: 0x17000339 RID: 825
	// (get) Token: 0x06002E28 RID: 11816 RVA: 0x000F3812 File Offset: 0x000F1A12
	public ConduitType ConduitType
	{
		get
		{
			return this.conduitType;
		}
	}

	// Token: 0x1700033A RID: 826
	// (get) Token: 0x06002E29 RID: 11817 RVA: 0x000F381A File Offset: 0x000F1A1A
	public bool IsConnected
	{
		get
		{
			return Grid.Objects[this.utilityCell, (this.conduitType == ConduitType.Gas) ? 12 : 16] != null && this.m_buildingComplete != null;
		}
	}

	// Token: 0x1700033B RID: 827
	// (get) Token: 0x06002E2A RID: 11818 RVA: 0x000F3854 File Offset: 0x000F1A54
	public bool CanConsume
	{
		get
		{
			bool flag = false;
			if (this.IsConnected)
			{
				flag = this.GetConduitManager().GetContents(this.utilityCell).mass > 0f;
			}
			return flag;
		}
	}

	// Token: 0x1700033C RID: 828
	// (get) Token: 0x06002E2B RID: 11819 RVA: 0x000F3890 File Offset: 0x000F1A90
	public float stored_mass
	{
		get
		{
			if (this.storage == null)
			{
				return 0f;
			}
			if (!(this.capacityTag != GameTags.Any))
			{
				return this.storage.MassStored();
			}
			return this.storage.GetMassAvailable(this.capacityTag);
		}
	}

	// Token: 0x1700033D RID: 829
	// (get) Token: 0x06002E2C RID: 11820 RVA: 0x000F38E0 File Offset: 0x000F1AE0
	public float space_remaining_kg
	{
		get
		{
			float num = this.capacityKG - this.stored_mass;
			if (!(this.storage == null))
			{
				return Mathf.Min(this.storage.RemainingCapacity(), num);
			}
			return num;
		}
	}

	// Token: 0x06002E2D RID: 11821 RVA: 0x000F391C File Offset: 0x000F1B1C
	public void SetConduitData(ConduitType type)
	{
		this.conduitType = type;
	}

	// Token: 0x1700033E RID: 830
	// (get) Token: 0x06002E2E RID: 11822 RVA: 0x000F3925 File Offset: 0x000F1B25
	public ConduitType TypeOfConduit
	{
		get
		{
			return this.conduitType;
		}
	}

	// Token: 0x1700033F RID: 831
	// (get) Token: 0x06002E2F RID: 11823 RVA: 0x000F392D File Offset: 0x000F1B2D
	public bool IsAlmostEmpty
	{
		get
		{
			return !this.ignoreMinMassCheck && this.MassAvailable < this.ConsumptionRate * 30f;
		}
	}

	// Token: 0x17000340 RID: 832
	// (get) Token: 0x06002E30 RID: 11824 RVA: 0x000F394D File Offset: 0x000F1B4D
	public bool IsEmpty
	{
		get
		{
			return !this.ignoreMinMassCheck && (this.MassAvailable == 0f || this.MassAvailable < this.ConsumptionRate);
		}
	}

	// Token: 0x17000341 RID: 833
	// (get) Token: 0x06002E31 RID: 11825 RVA: 0x000F3976 File Offset: 0x000F1B76
	public float ConsumptionRate
	{
		get
		{
			return this.consumptionRate;
		}
	}

	// Token: 0x17000342 RID: 834
	// (get) Token: 0x06002E32 RID: 11826 RVA: 0x000F397E File Offset: 0x000F1B7E
	// (set) Token: 0x06002E33 RID: 11827 RVA: 0x000F3993 File Offset: 0x000F1B93
	public bool IsSatisfied
	{
		get
		{
			return this.satisfied || !this.isConsuming;
		}
		set
		{
			this.satisfied = value || this.forceAlwaysSatisfied;
		}
	}

	// Token: 0x06002E34 RID: 11828 RVA: 0x000F39A8 File Offset: 0x000F1BA8
	private ConduitFlow GetConduitManager()
	{
		ConduitType conduitType = this.conduitType;
		if (conduitType == ConduitType.Gas)
		{
			return Game.Instance.gasConduitFlow;
		}
		if (conduitType != ConduitType.Liquid)
		{
			return null;
		}
		return Game.Instance.liquidConduitFlow;
	}

	// Token: 0x17000343 RID: 835
	// (get) Token: 0x06002E35 RID: 11829 RVA: 0x000F39E0 File Offset: 0x000F1BE0
	public float MassAvailable
	{
		get
		{
			ConduitFlow conduitManager = this.GetConduitManager();
			int inputCell = this.GetInputCell(conduitManager.conduitType);
			return conduitManager.GetContents(inputCell).mass;
		}
	}

	// Token: 0x06002E36 RID: 11830 RVA: 0x000F3A10 File Offset: 0x000F1C10
	private int GetInputCell(ConduitType inputConduitType)
	{
		if (this.useSecondaryInput)
		{
			ISecondaryInput[] components = base.GetComponents<ISecondaryInput>();
			foreach (ISecondaryInput secondaryInput in components)
			{
				if (secondaryInput.HasSecondaryConduitType(inputConduitType))
				{
					return Grid.OffsetCell(this.building.NaturalBuildingCell(), secondaryInput.GetSecondaryConduitOffset(inputConduitType));
				}
			}
			global::Debug.LogWarning("No secondaryInput of type was found");
			return Grid.OffsetCell(this.building.NaturalBuildingCell(), components[0].GetSecondaryConduitOffset(inputConduitType));
		}
		return this.building.GetUtilityInputCell();
	}

	// Token: 0x06002E37 RID: 11831 RVA: 0x000F3A90 File Offset: 0x000F1C90
	protected override void OnSpawn()
	{
		base.OnSpawn();
		GameScheduler.Instance.Schedule("PlumbingTutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Plumbing, true);
		}, null, null);
		ConduitFlow conduitManager = this.GetConduitManager();
		this.utilityCell = this.GetInputCell(conduitManager.conduitType);
		ScenePartitionerLayer scenePartitionerLayer = GameScenePartitioner.Instance.objectLayers[(this.conduitType == ConduitType.Gas) ? 12 : 16];
		this.partitionerEntry = GameScenePartitioner.Instance.Add("ConduitConsumer.OnSpawn", base.gameObject, this.utilityCell, scenePartitionerLayer, new Action<object>(this.OnConduitConnectionChanged));
		this.GetConduitManager().AddConduitUpdater(new Action<float>(this.ConduitUpdate), ConduitFlowPriority.Default);
		this.OnConduitConnectionChanged(null);
	}

	// Token: 0x06002E38 RID: 11832 RVA: 0x000F3B5A File Offset: 0x000F1D5A
	protected override void OnCleanUp()
	{
		this.GetConduitManager().RemoveConduitUpdater(new Action<float>(this.ConduitUpdate));
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		base.OnCleanUp();
	}

	// Token: 0x06002E39 RID: 11833 RVA: 0x000F3B89 File Offset: 0x000F1D89
	private void OnConduitConnectionChanged(object data)
	{
		base.Trigger(-2094018600, this.IsConnected);
	}

	// Token: 0x06002E3A RID: 11834 RVA: 0x000F3BA1 File Offset: 0x000F1DA1
	public void SetOnState(bool onState)
	{
		this.isOn = onState;
	}

	// Token: 0x06002E3B RID: 11835 RVA: 0x000F3BAC File Offset: 0x000F1DAC
	private void ConduitUpdate(float dt)
	{
		if (this.isConsuming && this.isOn)
		{
			ConduitFlow conduitManager = this.GetConduitManager();
			this.Consume(dt, conduitManager);
		}
	}

	// Token: 0x06002E3C RID: 11836 RVA: 0x000F3BD8 File Offset: 0x000F1DD8
	private void Consume(float dt, ConduitFlow conduit_mgr)
	{
		this.IsSatisfied = false;
		this.consumedLastTick = true;
		if (this.building.Def.CanMove)
		{
			this.utilityCell = this.GetInputCell(conduit_mgr.conduitType);
		}
		if (!this.IsConnected)
		{
			return;
		}
		ConduitFlow.ConduitContents contents = conduit_mgr.GetContents(this.utilityCell);
		if (contents.mass <= 0f)
		{
			return;
		}
		this.IsSatisfied = true;
		if (!this.alwaysConsume && !this.operational.MeetsRequirements(this.OperatingRequirement))
		{
			return;
		}
		float num = this.ConsumptionRate * dt;
		num = Mathf.Min(num, this.space_remaining_kg);
		Element element = ElementLoader.FindElementByHash(contents.element);
		if (contents.element != this.lastConsumedElement)
		{
			DiscoveredResources.Instance.Discover(element.tag, element.materialCategory);
		}
		float num2 = 0f;
		if (num > 0f)
		{
			ConduitFlow.ConduitContents conduitContents = conduit_mgr.RemoveElement(this.utilityCell, num);
			num2 = conduitContents.mass;
			this.lastConsumedElement = conduitContents.element;
		}
		bool flag = element.HasTag(this.capacityTag);
		if (num2 > 0f && this.capacityTag != GameTags.Any && !flag)
		{
			base.Trigger(-794517298, new BuildingHP.DamageSourceInfo
			{
				damage = 1,
				source = BUILDINGS.DAMAGESOURCES.BAD_INPUT_ELEMENT,
				popString = UI.GAMEOBJECTEFFECTS.DAMAGE_POPS.WRONG_ELEMENT
			});
		}
		if (flag || this.wrongElementResult == ConduitConsumer.WrongElementResult.Store || contents.element == SimHashes.Vacuum || this.capacityTag == GameTags.Any)
		{
			if (num2 > 0f)
			{
				this.consumedLastTick = false;
				int num3 = (int)((float)contents.diseaseCount * (num2 / contents.mass));
				Element element2 = ElementLoader.FindElementByHash(contents.element);
				ConduitType conduitType = this.conduitType;
				if (conduitType != ConduitType.Gas)
				{
					if (conduitType == ConduitType.Liquid)
					{
						if (element2.IsLiquid)
						{
							this.storage.AddLiquid(contents.element, num2, contents.temperature, contents.diseaseIdx, num3, this.keepZeroMassObject, false);
							return;
						}
						global::Debug.LogWarning("Liquid conduit consumer consuming non liquid: " + element2.id.ToString());
						return;
					}
				}
				else
				{
					if (element2.IsGas)
					{
						this.storage.AddGasChunk(contents.element, num2, contents.temperature, contents.diseaseIdx, num3, this.keepZeroMassObject, false);
						return;
					}
					global::Debug.LogWarning("Gas conduit consumer consuming non gas: " + element2.id.ToString());
					return;
				}
			}
		}
		else if (num2 > 0f)
		{
			this.consumedLastTick = false;
			if (this.wrongElementResult == ConduitConsumer.WrongElementResult.Dump)
			{
				int num4 = (int)((float)contents.diseaseCount * (num2 / contents.mass));
				SimMessages.AddRemoveSubstance(Grid.PosToCell(base.transform.GetPosition()), contents.element, CellEventLogger.Instance.ConduitConsumerWrongElement, num2, contents.temperature, contents.diseaseIdx, num4, true, -1);
			}
		}
	}

	// Token: 0x04001BD6 RID: 7126
	[SerializeField]
	public ConduitType conduitType;

	// Token: 0x04001BD7 RID: 7127
	[SerializeField]
	public bool ignoreMinMassCheck;

	// Token: 0x04001BD8 RID: 7128
	[SerializeField]
	public Tag capacityTag = GameTags.Any;

	// Token: 0x04001BD9 RID: 7129
	[SerializeField]
	public float capacityKG = float.PositiveInfinity;

	// Token: 0x04001BDA RID: 7130
	[SerializeField]
	public bool forceAlwaysSatisfied;

	// Token: 0x04001BDB RID: 7131
	[SerializeField]
	public bool alwaysConsume;

	// Token: 0x04001BDC RID: 7132
	[SerializeField]
	public bool keepZeroMassObject = true;

	// Token: 0x04001BDD RID: 7133
	[SerializeField]
	public bool useSecondaryInput;

	// Token: 0x04001BDE RID: 7134
	[SerializeField]
	public bool isOn = true;

	// Token: 0x04001BDF RID: 7135
	[NonSerialized]
	public bool isConsuming = true;

	// Token: 0x04001BE0 RID: 7136
	[NonSerialized]
	public bool consumedLastTick = true;

	// Token: 0x04001BE1 RID: 7137
	[MyCmpReq]
	public Operational operational;

	// Token: 0x04001BE2 RID: 7138
	[MyCmpReq]
	private Building building;

	// Token: 0x04001BE3 RID: 7139
	public Operational.State OperatingRequirement;

	// Token: 0x04001BE4 RID: 7140
	public ISecondaryInput targetSecondaryInput;

	// Token: 0x04001BE5 RID: 7141
	[MyCmpGet]
	public Storage storage;

	// Token: 0x04001BE6 RID: 7142
	[MyCmpGet]
	private BuildingComplete m_buildingComplete;

	// Token: 0x04001BE7 RID: 7143
	private int utilityCell = -1;

	// Token: 0x04001BE8 RID: 7144
	public float consumptionRate = float.PositiveInfinity;

	// Token: 0x04001BE9 RID: 7145
	public SimHashes lastConsumedElement = SimHashes.Vacuum;

	// Token: 0x04001BEA RID: 7146
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04001BEB RID: 7147
	private bool satisfied;

	// Token: 0x04001BEC RID: 7148
	public ConduitConsumer.WrongElementResult wrongElementResult;

	// Token: 0x02001372 RID: 4978
	public enum WrongElementResult
	{
		// Token: 0x04006090 RID: 24720
		Destroy,
		// Token: 0x04006091 RID: 24721
		Dump,
		// Token: 0x04006092 RID: 24722
		Store
	}
}
