using System;
using System.Collections.Generic;
using System.Diagnostics;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x02000790 RID: 1936
[SerializationConfig(MemberSerialization.OptIn)]
[DebuggerDisplay("{name}")]
[AddComponentMenu("KMonoBehaviour/scripts/Generator")]
public class Generator : KMonoBehaviour, ISaveLoadable, IEnergyProducer, ICircuitConnected
{
	// Token: 0x170003EC RID: 1004
	// (get) Token: 0x06003612 RID: 13842 RVA: 0x0012C0D3 File Offset: 0x0012A2D3
	public int PowerDistributionOrder
	{
		get
		{
			return this.powerDistributionOrder;
		}
	}

	// Token: 0x170003ED RID: 1005
	// (get) Token: 0x06003613 RID: 13843 RVA: 0x0012C0DB File Offset: 0x0012A2DB
	public virtual float Capacity
	{
		get
		{
			return this.capacity;
		}
	}

	// Token: 0x170003EE RID: 1006
	// (get) Token: 0x06003614 RID: 13844 RVA: 0x0012C0E3 File Offset: 0x0012A2E3
	public virtual bool IsEmpty
	{
		get
		{
			return this.joulesAvailable <= 0f;
		}
	}

	// Token: 0x170003EF RID: 1007
	// (get) Token: 0x06003615 RID: 13845 RVA: 0x0012C0F5 File Offset: 0x0012A2F5
	public virtual float JoulesAvailable
	{
		get
		{
			return this.joulesAvailable;
		}
	}

	// Token: 0x170003F0 RID: 1008
	// (get) Token: 0x06003616 RID: 13846 RVA: 0x0012C0FD File Offset: 0x0012A2FD
	public float WattageRating
	{
		get
		{
			return this.building.Def.GeneratorWattageRating * this.Efficiency;
		}
	}

	// Token: 0x170003F1 RID: 1009
	// (get) Token: 0x06003617 RID: 13847 RVA: 0x0012C116 File Offset: 0x0012A316
	public float BaseWattageRating
	{
		get
		{
			return this.building.Def.GeneratorWattageRating;
		}
	}

	// Token: 0x170003F2 RID: 1010
	// (get) Token: 0x06003618 RID: 13848 RVA: 0x0012C128 File Offset: 0x0012A328
	public float PercentFull
	{
		get
		{
			if (this.Capacity == 0f)
			{
				return 1f;
			}
			return this.joulesAvailable / this.Capacity;
		}
	}

	// Token: 0x170003F3 RID: 1011
	// (get) Token: 0x06003619 RID: 13849 RVA: 0x0012C14A File Offset: 0x0012A34A
	// (set) Token: 0x0600361A RID: 13850 RVA: 0x0012C152 File Offset: 0x0012A352
	public int PowerCell { get; private set; }

	// Token: 0x170003F4 RID: 1012
	// (get) Token: 0x0600361B RID: 13851 RVA: 0x0012C15B File Offset: 0x0012A35B
	public ushort CircuitID
	{
		get
		{
			return Game.Instance.circuitManager.GetCircuitID(this);
		}
	}

	// Token: 0x170003F5 RID: 1013
	// (get) Token: 0x0600361C RID: 13852 RVA: 0x0012C16D File Offset: 0x0012A36D
	private float Efficiency
	{
		get
		{
			return Mathf.Max(1f + this.generatorOutputAttribute.GetTotalValue() / 100f, 0f);
		}
	}

	// Token: 0x170003F6 RID: 1014
	// (get) Token: 0x0600361D RID: 13853 RVA: 0x0012C190 File Offset: 0x0012A390
	// (set) Token: 0x0600361E RID: 13854 RVA: 0x0012C198 File Offset: 0x0012A398
	public bool IsVirtual { get; protected set; }

	// Token: 0x170003F7 RID: 1015
	// (get) Token: 0x0600361F RID: 13855 RVA: 0x0012C1A1 File Offset: 0x0012A3A1
	// (set) Token: 0x06003620 RID: 13856 RVA: 0x0012C1A9 File Offset: 0x0012A3A9
	public object VirtualCircuitKey { get; protected set; }

	// Token: 0x06003621 RID: 13857 RVA: 0x0012C1B4 File Offset: 0x0012A3B4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Attributes attributes = base.gameObject.GetAttributes();
		this.generatorOutputAttribute = attributes.Add(Db.Get().Attributes.GeneratorOutput);
	}

	// Token: 0x06003622 RID: 13858 RVA: 0x0012C1F0 File Offset: 0x0012A3F0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.Generators.Add(this);
		base.Subscribe<Generator>(-1582839653, Generator.OnTagsChangedDelegate);
		this.OnTagsChanged(null);
		this.capacity = Generator.CalculateCapacity(this.building.Def, null);
		this.PowerCell = this.building.GetPowerOutputCell();
		this.CheckConnectionStatus();
		Game.Instance.energySim.AddGenerator(this);
	}

	// Token: 0x06003623 RID: 13859 RVA: 0x0012C264 File Offset: 0x0012A464
	private void OnTagsChanged(object data)
	{
		if (this.HasAllTags(this.connectedTags))
		{
			Game.Instance.circuitManager.Connect(this);
			return;
		}
		Game.Instance.circuitManager.Disconnect(this);
	}

	// Token: 0x06003624 RID: 13860 RVA: 0x0012C295 File Offset: 0x0012A495
	public virtual bool IsProducingPower()
	{
		return this.operational.IsActive;
	}

	// Token: 0x06003625 RID: 13861 RVA: 0x0012C2A2 File Offset: 0x0012A4A2
	public virtual void EnergySim200ms(float dt)
	{
		this.CheckConnectionStatus();
	}

	// Token: 0x06003626 RID: 13862 RVA: 0x0012C2AC File Offset: 0x0012A4AC
	private void SetStatusItem(StatusItem status_item)
	{
		if (status_item != this.currentStatusItem && this.currentStatusItem != null)
		{
			this.statusItemID = this.selectable.RemoveStatusItem(this.statusItemID, false);
		}
		if (status_item != null && this.statusItemID == Guid.Empty)
		{
			this.statusItemID = this.selectable.AddStatusItem(status_item, this);
		}
		this.currentStatusItem = status_item;
	}

	// Token: 0x06003627 RID: 13863 RVA: 0x0012C314 File Offset: 0x0012A514
	private void CheckConnectionStatus()
	{
		if (this.CircuitID == 65535)
		{
			if (this.showConnectedConsumerStatusItems)
			{
				this.SetStatusItem(Db.Get().BuildingStatusItems.NoWireConnected);
			}
			this.operational.SetFlag(Generator.generatorConnectedFlag, false);
			return;
		}
		if (!Game.Instance.circuitManager.HasConsumers(this.CircuitID) && !Game.Instance.circuitManager.HasBatteries(this.CircuitID))
		{
			if (this.showConnectedConsumerStatusItems)
			{
				this.SetStatusItem(Db.Get().BuildingStatusItems.NoPowerConsumers);
			}
			this.operational.SetFlag(Generator.generatorConnectedFlag, true);
			return;
		}
		this.SetStatusItem(null);
		this.operational.SetFlag(Generator.generatorConnectedFlag, true);
	}

	// Token: 0x06003628 RID: 13864 RVA: 0x0012C3D2 File Offset: 0x0012A5D2
	protected override void OnCleanUp()
	{
		Game.Instance.energySim.RemoveGenerator(this);
		Game.Instance.circuitManager.Disconnect(this);
		Components.Generators.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06003629 RID: 13865 RVA: 0x0012C405 File Offset: 0x0012A605
	public static float CalculateCapacity(BuildingDef def, Element element)
	{
		if (element == null)
		{
			return def.GeneratorBaseCapacity;
		}
		return def.GeneratorBaseCapacity * (1f + (element.HasTag(GameTags.RefinedMetal) ? 1f : 0f));
	}

	// Token: 0x0600362A RID: 13866 RVA: 0x0012C437 File Offset: 0x0012A637
	public void ResetJoules()
	{
		this.joulesAvailable = 0f;
	}

	// Token: 0x0600362B RID: 13867 RVA: 0x0012C444 File Offset: 0x0012A644
	public virtual void ApplyDeltaJoules(float joulesDelta, bool canOverPower = false)
	{
		this.joulesAvailable = Mathf.Clamp(this.joulesAvailable + joulesDelta, 0f, canOverPower ? float.MaxValue : this.Capacity);
	}

	// Token: 0x0600362C RID: 13868 RVA: 0x0012C470 File Offset: 0x0012A670
	public void GenerateJoules(float joulesAvailable, bool canOverPower = false)
	{
		global::Debug.Assert(base.GetComponent<Battery>() == null);
		this.joulesAvailable = Mathf.Clamp(joulesAvailable, 0f, canOverPower ? float.MaxValue : this.Capacity);
		ReportManager.Instance.ReportValue(ReportManager.ReportType.EnergyCreated, this.joulesAvailable, this.GetProperName(), null);
		if (!Game.Instance.savedInfo.powerCreatedbyGeneratorType.ContainsKey(this.PrefabID()))
		{
			Game.Instance.savedInfo.powerCreatedbyGeneratorType.Add(this.PrefabID(), 0f);
		}
		Dictionary<Tag, float> powerCreatedbyGeneratorType = Game.Instance.savedInfo.powerCreatedbyGeneratorType;
		Tag tag = this.PrefabID();
		powerCreatedbyGeneratorType[tag] += this.joulesAvailable;
	}

	// Token: 0x0600362D RID: 13869 RVA: 0x0012C52F File Offset: 0x0012A72F
	public void AssignJoulesAvailable(float joulesAvailable)
	{
		global::Debug.Assert(base.GetComponent<PowerTransformer>() != null);
		this.joulesAvailable = joulesAvailable;
	}

	// Token: 0x0600362E RID: 13870 RVA: 0x0012C549 File Offset: 0x0012A749
	public virtual void ConsumeEnergy(float joules)
	{
		this.joulesAvailable = Mathf.Max(0f, this.JoulesAvailable - joules);
	}

	// Token: 0x04002416 RID: 9238
	protected const int SimUpdateSortKey = 1001;

	// Token: 0x04002417 RID: 9239
	[MyCmpReq]
	protected Building building;

	// Token: 0x04002418 RID: 9240
	[MyCmpReq]
	protected Operational operational;

	// Token: 0x04002419 RID: 9241
	[MyCmpReq]
	protected KSelectable selectable;

	// Token: 0x0400241A RID: 9242
	[Serialize]
	private float joulesAvailable;

	// Token: 0x0400241B RID: 9243
	[SerializeField]
	public int powerDistributionOrder;

	// Token: 0x0400241C RID: 9244
	public static readonly Operational.Flag generatorConnectedFlag = new Operational.Flag("GeneratorConnected", Operational.Flag.Type.Requirement);

	// Token: 0x0400241D RID: 9245
	protected static readonly Operational.Flag wireConnectedFlag = new Operational.Flag("generatorWireConnected", Operational.Flag.Type.Requirement);

	// Token: 0x0400241E RID: 9246
	private float capacity;

	// Token: 0x04002422 RID: 9250
	public static readonly Tag[] DEFAULT_CONNECTED_TAGS = new Tag[] { GameTags.Operational };

	// Token: 0x04002423 RID: 9251
	[SerializeField]
	public Tag[] connectedTags = Generator.DEFAULT_CONNECTED_TAGS;

	// Token: 0x04002424 RID: 9252
	public bool showConnectedConsumerStatusItems = true;

	// Token: 0x04002425 RID: 9253
	private StatusItem currentStatusItem;

	// Token: 0x04002426 RID: 9254
	private Guid statusItemID;

	// Token: 0x04002427 RID: 9255
	private AttributeInstance generatorOutputAttribute;

	// Token: 0x04002428 RID: 9256
	private static readonly EventSystem.IntraObjectHandler<Generator> OnTagsChangedDelegate = new EventSystem.IntraObjectHandler<Generator>(delegate(Generator component, object data)
	{
		component.OnTagsChanged(data);
	});
}
