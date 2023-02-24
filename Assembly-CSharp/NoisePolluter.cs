using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200086B RID: 2155
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/NoisePolluter")]
public class NoisePolluter : KMonoBehaviour, IPolluter
{
	// Token: 0x06003DD4 RID: 15828 RVA: 0x00159443 File Offset: 0x00157643
	public static bool IsNoiseableCell(int cell)
	{
		return Grid.IsValidCell(cell) && (Grid.IsGas(cell) || !Grid.IsSubstantialLiquid(cell, 0.35f));
	}

	// Token: 0x06003DD5 RID: 15829 RVA: 0x00159467 File Offset: 0x00157667
	public void ResetCells()
	{
		if (this.radius == 0)
		{
			global::Debug.LogFormat("[{0}] has a 0 radius noise, this will disable it", new object[] { this.GetName() });
			return;
		}
	}

	// Token: 0x06003DD6 RID: 15830 RVA: 0x0015948B File Offset: 0x0015768B
	public void SetAttributes(Vector2 pos, int dB, GameObject go, string name)
	{
		this.sourceName = name;
		this.noise = dB;
	}

	// Token: 0x06003DD7 RID: 15831 RVA: 0x0015949C File Offset: 0x0015769C
	public int GetRadius()
	{
		return this.radius;
	}

	// Token: 0x06003DD8 RID: 15832 RVA: 0x001594A4 File Offset: 0x001576A4
	public int GetNoise()
	{
		return this.noise;
	}

	// Token: 0x06003DD9 RID: 15833 RVA: 0x001594AC File Offset: 0x001576AC
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x06003DDA RID: 15834 RVA: 0x001594B4 File Offset: 0x001576B4
	public void SetSplat(NoiseSplat new_splat)
	{
		this.splat = new_splat;
	}

	// Token: 0x06003DDB RID: 15835 RVA: 0x001594BD File Offset: 0x001576BD
	public void Clear()
	{
		if (this.splat != null)
		{
			this.splat.Clear();
			this.splat = null;
		}
	}

	// Token: 0x06003DDC RID: 15836 RVA: 0x001594D9 File Offset: 0x001576D9
	public Vector2 GetPosition()
	{
		return base.transform.GetPosition();
	}

	// Token: 0x17000451 RID: 1105
	// (get) Token: 0x06003DDD RID: 15837 RVA: 0x001594EB File Offset: 0x001576EB
	// (set) Token: 0x06003DDE RID: 15838 RVA: 0x001594F3 File Offset: 0x001576F3
	public string sourceName { get; private set; }

	// Token: 0x17000452 RID: 1106
	// (get) Token: 0x06003DDF RID: 15839 RVA: 0x001594FC File Offset: 0x001576FC
	// (set) Token: 0x06003DE0 RID: 15840 RVA: 0x00159504 File Offset: 0x00157704
	public bool active { get; private set; }

	// Token: 0x06003DE1 RID: 15841 RVA: 0x0015950D File Offset: 0x0015770D
	public void SetActive(bool active = true)
	{
		if (!active && this.splat != null)
		{
			AudioEventManager.Get().ClearNoiseSplat(this.splat);
			this.splat.Clear();
		}
		this.active = active;
	}

	// Token: 0x06003DE2 RID: 15842 RVA: 0x0015953C File Offset: 0x0015773C
	public void Refresh()
	{
		if (this.active)
		{
			if (this.splat != null)
			{
				AudioEventManager.Get().ClearNoiseSplat(this.splat);
				this.splat.Clear();
			}
			KSelectable component = base.GetComponent<KSelectable>();
			string text = ((component != null) ? component.GetName() : base.name);
			GameObject gameObject = base.GetComponent<KMonoBehaviour>().gameObject;
			this.splat = AudioEventManager.Get().CreateNoiseSplat(this.GetPosition(), this.noise, this.radius, text, gameObject);
		}
	}

	// Token: 0x06003DE3 RID: 15843 RVA: 0x001595C4 File Offset: 0x001577C4
	private void OnActiveChanged(object data)
	{
		bool isActive = ((Operational)data).IsActive;
		this.SetActive(isActive);
		this.Refresh();
	}

	// Token: 0x06003DE4 RID: 15844 RVA: 0x001595EA File Offset: 0x001577EA
	public void SetValues(EffectorValues values)
	{
		this.noise = values.amount;
		this.radius = values.radius;
	}

	// Token: 0x06003DE5 RID: 15845 RVA: 0x00159604 File Offset: 0x00157804
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.radius == 0 || this.noise == 0)
		{
			global::Debug.LogWarning(string.Concat(new string[]
			{
				"Noisepollutor::OnSpawn [",
				this.GetName(),
				"] noise: [",
				this.noise.ToString(),
				"] radius: [",
				this.radius.ToString(),
				"]"
			}));
			UnityEngine.Object.Destroy(this);
			return;
		}
		this.ResetCells();
		Operational component = base.GetComponent<Operational>();
		if (component != null)
		{
			base.Subscribe<NoisePolluter>(824508782, NoisePolluter.OnActiveChangedDelegate);
		}
		this.refreshCallback = new System.Action(this.Refresh);
		this.refreshPartionerCallback = delegate(object data)
		{
			this.Refresh();
		};
		this.onCollectNoisePollutersCallback = new Action<object>(this.OnCollectNoisePolluters);
		Attributes attributes = this.GetAttributes();
		Db db = Db.Get();
		this.dB = attributes.Add(db.BuildingAttributes.NoisePollution);
		this.dBRadius = attributes.Add(db.BuildingAttributes.NoisePollutionRadius);
		if (this.noise != 0 && this.radius != 0)
		{
			AttributeModifier attributeModifier = new AttributeModifier(db.BuildingAttributes.NoisePollution.Id, (float)this.noise, UI.TOOLTIPS.BASE_VALUE, false, false, true);
			AttributeModifier attributeModifier2 = new AttributeModifier(db.BuildingAttributes.NoisePollutionRadius.Id, (float)this.radius, UI.TOOLTIPS.BASE_VALUE, false, false, true);
			attributes.Add(attributeModifier);
			attributes.Add(attributeModifier2);
		}
		else
		{
			global::Debug.LogWarning(string.Concat(new string[]
			{
				"Noisepollutor::OnSpawn [",
				this.GetName(),
				"] radius: [",
				this.radius.ToString(),
				"] noise: [",
				this.noise.ToString(),
				"]"
			}));
		}
		KBatchedAnimController component2 = base.GetComponent<KBatchedAnimController>();
		this.isMovable = component2 != null && component2.isMovable;
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new System.Action(this.OnCellChange), "NoisePolluter.OnSpawn");
		AttributeInstance attributeInstance = this.dB;
		attributeInstance.OnDirty = (System.Action)Delegate.Combine(attributeInstance.OnDirty, this.refreshCallback);
		AttributeInstance attributeInstance2 = this.dBRadius;
		attributeInstance2.OnDirty = (System.Action)Delegate.Combine(attributeInstance2.OnDirty, this.refreshCallback);
		if (component != null)
		{
			this.OnActiveChanged(component.IsActive);
		}
	}

	// Token: 0x06003DE6 RID: 15846 RVA: 0x00159885 File Offset: 0x00157A85
	private void OnCellChange()
	{
		this.Refresh();
	}

	// Token: 0x06003DE7 RID: 15847 RVA: 0x0015988D File Offset: 0x00157A8D
	private void OnCollectNoisePolluters(object data)
	{
		((List<NoisePolluter>)data).Add(this);
	}

	// Token: 0x06003DE8 RID: 15848 RVA: 0x0015989B File Offset: 0x00157A9B
	public string GetName()
	{
		if (string.IsNullOrEmpty(this.sourceName))
		{
			this.sourceName = base.GetComponent<KSelectable>().GetName();
		}
		return this.sourceName;
	}

	// Token: 0x06003DE9 RID: 15849 RVA: 0x001598C4 File Offset: 0x00157AC4
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (base.isSpawned)
		{
			if (this.dB != null)
			{
				AttributeInstance attributeInstance = this.dB;
				attributeInstance.OnDirty = (System.Action)Delegate.Remove(attributeInstance.OnDirty, this.refreshCallback);
				AttributeInstance attributeInstance2 = this.dBRadius;
				attributeInstance2.OnDirty = (System.Action)Delegate.Remove(attributeInstance2.OnDirty, this.refreshCallback);
			}
			if (this.isMovable)
			{
				Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new System.Action(this.OnCellChange));
			}
		}
		if (this.splat != null)
		{
			AudioEventManager.Get().ClearNoiseSplat(this.splat);
			this.splat.Clear();
		}
	}

	// Token: 0x06003DEA RID: 15850 RVA: 0x00159970 File Offset: 0x00157B70
	public float GetNoiseForCell(int cell)
	{
		return this.splat.GetDBForCell(cell);
	}

	// Token: 0x06003DEB RID: 15851 RVA: 0x00159980 File Offset: 0x00157B80
	public List<Descriptor> GetEffectDescriptions()
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.dB != null && this.dBRadius != null)
		{
			float totalValue = this.dB.GetTotalValue();
			float totalValue2 = this.dBRadius.GetTotalValue();
			string text = ((this.noise > 0) ? UI.BUILDINGEFFECTS.TOOLTIPS.NOISE_POLLUTION_INCREASE : UI.BUILDINGEFFECTS.TOOLTIPS.NOISE_POLLUTION_DECREASE);
			text = text + "\n\n" + this.dB.GetAttributeValueTooltip();
			string text2 = GameUtil.AddPositiveSign(totalValue.ToString(), totalValue > 0f);
			Descriptor descriptor = new Descriptor(string.Format(UI.BUILDINGEFFECTS.NOISE_CREATED, text2, totalValue2), string.Format(text, text2, totalValue2), Descriptor.DescriptorType.Effect, false);
			list.Add(descriptor);
		}
		else if (this.noise != 0)
		{
			string text3 = ((this.noise >= 0) ? UI.BUILDINGEFFECTS.TOOLTIPS.NOISE_POLLUTION_INCREASE : UI.BUILDINGEFFECTS.TOOLTIPS.NOISE_POLLUTION_DECREASE);
			string text4 = GameUtil.AddPositiveSign(this.noise.ToString(), this.noise > 0);
			Descriptor descriptor2 = new Descriptor(string.Format(UI.BUILDINGEFFECTS.NOISE_CREATED, text4, this.radius), string.Format(text3, text4, this.radius), Descriptor.DescriptorType.Effect, false);
			list.Add(descriptor2);
		}
		return list;
	}

	// Token: 0x06003DEC RID: 15852 RVA: 0x00159AC5 File Offset: 0x00157CC5
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return this.GetEffectDescriptions();
	}

	// Token: 0x0400287F RID: 10367
	public const string ID = "NoisePolluter";

	// Token: 0x04002880 RID: 10368
	public int radius;

	// Token: 0x04002881 RID: 10369
	public int noise;

	// Token: 0x04002882 RID: 10370
	public AttributeInstance dB;

	// Token: 0x04002883 RID: 10371
	public AttributeInstance dBRadius;

	// Token: 0x04002884 RID: 10372
	private NoiseSplat splat;

	// Token: 0x04002886 RID: 10374
	public System.Action refreshCallback;

	// Token: 0x04002887 RID: 10375
	public Action<object> refreshPartionerCallback;

	// Token: 0x04002888 RID: 10376
	public Action<object> onCollectNoisePollutersCallback;

	// Token: 0x04002889 RID: 10377
	public bool isMovable;

	// Token: 0x0400288A RID: 10378
	[MyCmpReq]
	public OccupyArea occupyArea;

	// Token: 0x0400288C RID: 10380
	private static readonly EventSystem.IntraObjectHandler<NoisePolluter> OnActiveChangedDelegate = new EventSystem.IntraObjectHandler<NoisePolluter>(delegate(NoisePolluter component, object data)
	{
		component.OnActiveChanged(data);
	});
}
