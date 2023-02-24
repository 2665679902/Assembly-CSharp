using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Klei;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x020008A2 RID: 2210
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/PrimaryElement")]
public class PrimaryElement : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x06003F67 RID: 16231 RVA: 0x00161F8C File Offset: 0x0016018C
	public void SetUseSimDiseaseInfo(bool use)
	{
		this.useSimDiseaseInfo = use;
	}

	// Token: 0x17000468 RID: 1128
	// (get) Token: 0x06003F68 RID: 16232 RVA: 0x00161F95 File Offset: 0x00160195
	// (set) Token: 0x06003F69 RID: 16233 RVA: 0x00161FA0 File Offset: 0x001601A0
	[Serialize]
	public float Units
	{
		get
		{
			return this._units;
		}
		set
		{
			if (float.IsInfinity(value) || float.IsNaN(value))
			{
				DebugUtil.DevLogError("Invalid units value for element, setting Units to 0");
				this._units = 0f;
			}
			else
			{
				this._units = value;
			}
			if (this.onDataChanged != null)
			{
				this.onDataChanged(this);
			}
		}
	}

	// Token: 0x17000469 RID: 1129
	// (get) Token: 0x06003F6A RID: 16234 RVA: 0x00161FEF File Offset: 0x001601EF
	// (set) Token: 0x06003F6B RID: 16235 RVA: 0x00161FFD File Offset: 0x001601FD
	public float Temperature
	{
		get
		{
			return this.getTemperatureCallback(this);
		}
		set
		{
			this.SetTemperature(value);
		}
	}

	// Token: 0x1700046A RID: 1130
	// (get) Token: 0x06003F6C RID: 16236 RVA: 0x00162006 File Offset: 0x00160206
	// (set) Token: 0x06003F6D RID: 16237 RVA: 0x0016200E File Offset: 0x0016020E
	public float InternalTemperature
	{
		get
		{
			return this._Temperature;
		}
		set
		{
			this._Temperature = value;
		}
	}

	// Token: 0x06003F6E RID: 16238 RVA: 0x00162018 File Offset: 0x00160218
	[OnSerializing]
	private void OnSerializing()
	{
		this._Temperature = this.Temperature;
		this.SanitizeMassAndTemperature();
		this.diseaseID.HashValue = 0;
		this.diseaseCount = 0;
		if (this.useSimDiseaseInfo)
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			if (Grid.DiseaseIdx[num] != 255)
			{
				this.diseaseID = Db.Get().Diseases[(int)Grid.DiseaseIdx[num]].id;
				this.diseaseCount = Grid.DiseaseCount[num];
				return;
			}
		}
		else if (this.diseaseHandle.IsValid())
		{
			DiseaseHeader header = GameComps.DiseaseContainers.GetHeader(this.diseaseHandle);
			if (header.diseaseIdx != 255)
			{
				this.diseaseID = Db.Get().Diseases[(int)header.diseaseIdx].id;
				this.diseaseCount = header.diseaseCount;
			}
		}
	}

	// Token: 0x06003F6F RID: 16239 RVA: 0x00162108 File Offset: 0x00160308
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (this.ElementID == (SimHashes)351109216)
		{
			this.ElementID = SimHashes.Creature;
		}
		this.SanitizeMassAndTemperature();
		float num = this._Temperature;
		if (float.IsNaN(num) || float.IsInfinity(num) || num < 0f || 10000f < num)
		{
			DeserializeWarnings.Instance.PrimaryElementTemperatureIsNan.Warn(string.Format("{0} has invalid temperature of {1}. Resetting temperature.", base.name, this.Temperature), null);
			num = this.Element.defaultValues.temperature;
		}
		this._Temperature = num;
		this.Temperature = num;
		if (this.Element == null)
		{
			DeserializeWarnings.Instance.PrimaryElementHasNoElement.Warn(base.name + "Primary element has no element.", null);
		}
		if (this.Mass < 0f)
		{
			DebugUtil.DevLogError(base.gameObject, "deserialized ore with less than 0 mass. Error! Destroying");
			Util.KDestroyGameObject(base.gameObject);
			return;
		}
		if (this.Mass == 0f && !this.KeepZeroMassObject)
		{
			DebugUtil.DevLogError(base.gameObject, "deserialized element with 0 mass. Destroying");
			Util.KDestroyGameObject(base.gameObject);
			return;
		}
		if (this.onDataChanged != null)
		{
			this.onDataChanged(this);
		}
		byte index = Db.Get().Diseases.GetIndex(this.diseaseID);
		if (index == 255 || this.diseaseCount <= 0)
		{
			if (this.diseaseHandle.IsValid())
			{
				GameComps.DiseaseContainers.Remove(base.gameObject);
				this.diseaseHandle.Clear();
				return;
			}
		}
		else
		{
			if (this.diseaseHandle.IsValid())
			{
				DiseaseHeader header = GameComps.DiseaseContainers.GetHeader(this.diseaseHandle);
				header.diseaseIdx = index;
				header.diseaseCount = this.diseaseCount;
				GameComps.DiseaseContainers.SetHeader(this.diseaseHandle, header);
				return;
			}
			this.diseaseHandle = GameComps.DiseaseContainers.Add(base.gameObject, index, this.diseaseCount);
		}
	}

	// Token: 0x06003F70 RID: 16240 RVA: 0x001622EC File Offset: 0x001604EC
	protected override void OnLoadLevel()
	{
		base.OnLoadLevel();
	}

	// Token: 0x06003F71 RID: 16241 RVA: 0x001622F4 File Offset: 0x001604F4
	private void SanitizeMassAndTemperature()
	{
		if (this._Temperature <= 0f)
		{
			DebugUtil.DevLogError(base.gameObject.name + " is attempting to serialize a temperature of <= 0K. Resetting to default. world=" + base.gameObject.DebugGetMyWorldName());
			this._Temperature = this.Element.defaultValues.temperature;
		}
		if (this.Mass > PrimaryElement.MAX_MASS)
		{
			DebugUtil.DevLogError(string.Format("{0} is attempting to serialize very large mass {1}. Resetting to default. world={2}", base.gameObject.name, this.Mass, base.gameObject.DebugGetMyWorldName()));
			this.Mass = this.Element.defaultValues.mass;
		}
	}

	// Token: 0x1700046B RID: 1131
	// (get) Token: 0x06003F72 RID: 16242 RVA: 0x0016239C File Offset: 0x0016059C
	// (set) Token: 0x06003F73 RID: 16243 RVA: 0x001623AB File Offset: 0x001605AB
	public float Mass
	{
		get
		{
			return this.Units * this.MassPerUnit;
		}
		set
		{
			this.SetMass(value);
			if (this.onDataChanged != null)
			{
				this.onDataChanged(this);
			}
		}
	}

	// Token: 0x06003F74 RID: 16244 RVA: 0x001623C8 File Offset: 0x001605C8
	private void SetMass(float mass)
	{
		if ((mass > PrimaryElement.MAX_MASS || mass < 0f) && this.ElementID != SimHashes.Regolith)
		{
			DebugUtil.DevLogErrorFormat(base.gameObject, "{0} is getting an abnormal mass set {1}.", new object[]
			{
				base.gameObject.name,
				mass
			});
		}
		mass = Mathf.Clamp(mass, 0f, PrimaryElement.MAX_MASS);
		this.Units = mass / this.MassPerUnit;
		if (this.Units <= 0f && !this.KeepZeroMassObject)
		{
			Util.KDestroyGameObject(base.gameObject);
		}
	}

	// Token: 0x06003F75 RID: 16245 RVA: 0x00162460 File Offset: 0x00160660
	private void SetTemperature(float temperature)
	{
		if (float.IsNaN(temperature) || float.IsInfinity(temperature))
		{
			DebugUtil.LogErrorArgs(base.gameObject, new object[] { "Invalid temperature [" + temperature.ToString() + "]" });
			return;
		}
		if (temperature <= 0f)
		{
			KCrashReporter.Assert(false, "Tried to set PrimaryElement.Temperature to a value <= 0");
		}
		this.setTemperatureCallback(this, temperature);
	}

	// Token: 0x06003F76 RID: 16246 RVA: 0x001624C8 File Offset: 0x001606C8
	public void SetMassTemperature(float mass, float temperature)
	{
		this.SetMass(mass);
		this.SetTemperature(temperature);
	}

	// Token: 0x1700046C RID: 1132
	// (get) Token: 0x06003F77 RID: 16247 RVA: 0x001624D8 File Offset: 0x001606D8
	public Element Element
	{
		get
		{
			if (this._Element == null)
			{
				this._Element = ElementLoader.FindElementByHash(this.ElementID);
			}
			return this._Element;
		}
	}

	// Token: 0x1700046D RID: 1133
	// (get) Token: 0x06003F78 RID: 16248 RVA: 0x001624FC File Offset: 0x001606FC
	public byte DiseaseIdx
	{
		get
		{
			if (this.diseaseRedirectTarget)
			{
				return this.diseaseRedirectTarget.DiseaseIdx;
			}
			byte b = byte.MaxValue;
			if (this.useSimDiseaseInfo)
			{
				int num = Grid.PosToCell(base.transform.GetPosition());
				b = Grid.DiseaseIdx[num];
			}
			else if (this.diseaseHandle.IsValid())
			{
				b = GameComps.DiseaseContainers.GetHeader(this.diseaseHandle).diseaseIdx;
			}
			return b;
		}
	}

	// Token: 0x1700046E RID: 1134
	// (get) Token: 0x06003F79 RID: 16249 RVA: 0x00162574 File Offset: 0x00160774
	public int DiseaseCount
	{
		get
		{
			if (this.diseaseRedirectTarget)
			{
				return this.diseaseRedirectTarget.DiseaseCount;
			}
			int num = 0;
			if (this.useSimDiseaseInfo)
			{
				int num2 = Grid.PosToCell(base.transform.GetPosition());
				num = Grid.DiseaseCount[num2];
			}
			else if (this.diseaseHandle.IsValid())
			{
				num = GameComps.DiseaseContainers.GetHeader(this.diseaseHandle).diseaseCount;
			}
			return num;
		}
	}

	// Token: 0x06003F7A RID: 16250 RVA: 0x001625E7 File Offset: 0x001607E7
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		GameComps.InfraredVisualizers.Add(base.gameObject);
		base.Subscribe<PrimaryElement>(1335436905, PrimaryElement.OnSplitFromChunkDelegate);
		base.Subscribe<PrimaryElement>(-2064133523, PrimaryElement.OnAbsorbDelegate);
	}

	// Token: 0x06003F7B RID: 16251 RVA: 0x00162624 File Offset: 0x00160824
	protected override void OnSpawn()
	{
		Attributes attributes = this.GetAttributes();
		if (attributes != null)
		{
			foreach (AttributeModifier attributeModifier in this.Element.attributeModifiers)
			{
				attributes.Add(attributeModifier);
			}
		}
	}

	// Token: 0x06003F7C RID: 16252 RVA: 0x00162688 File Offset: 0x00160888
	public void ForcePermanentDiseaseContainer(bool force_on)
	{
		if (force_on)
		{
			if (!this.diseaseHandle.IsValid())
			{
				this.diseaseHandle = GameComps.DiseaseContainers.Add(base.gameObject, byte.MaxValue, 0);
			}
		}
		else if (this.diseaseHandle.IsValid() && this.DiseaseIdx == 255)
		{
			GameComps.DiseaseContainers.Remove(base.gameObject);
			this.diseaseHandle.Clear();
		}
		this.forcePermanentDiseaseContainer = force_on;
	}

	// Token: 0x06003F7D RID: 16253 RVA: 0x001626FF File Offset: 0x001608FF
	protected override void OnCleanUp()
	{
		GameComps.InfraredVisualizers.Remove(base.gameObject);
		if (this.diseaseHandle.IsValid())
		{
			GameComps.DiseaseContainers.Remove(base.gameObject);
			this.diseaseHandle.Clear();
		}
		base.OnCleanUp();
	}

	// Token: 0x06003F7E RID: 16254 RVA: 0x0016273F File Offset: 0x0016093F
	public void SetElement(SimHashes element_id, bool addTags = true)
	{
		this.ElementID = element_id;
		if (addTags)
		{
			this.UpdateTags();
		}
	}

	// Token: 0x06003F7F RID: 16255 RVA: 0x00162754 File Offset: 0x00160954
	public void UpdateTags()
	{
		if (this.ElementID == (SimHashes)0)
		{
			global::Debug.Log("UpdateTags() Primary element 0", base.gameObject);
			return;
		}
		KPrefabID component = base.GetComponent<KPrefabID>();
		if (component != null)
		{
			List<Tag> list = new List<Tag>();
			foreach (Tag tag in this.Element.oreTags)
			{
				list.Add(tag);
			}
			if (component.HasAnyTags(PrimaryElement.metalTags))
			{
				list.Add(GameTags.StoredMetal);
			}
			foreach (Tag tag2 in list)
			{
				component.AddTag(tag2, false);
			}
		}
	}

	// Token: 0x06003F80 RID: 16256 RVA: 0x00162818 File Offset: 0x00160A18
	public void ModifyDiseaseCount(int delta, string reason)
	{
		if (this.diseaseRedirectTarget)
		{
			this.diseaseRedirectTarget.ModifyDiseaseCount(delta, reason);
			return;
		}
		if (this.useSimDiseaseInfo)
		{
			SimMessages.ModifyDiseaseOnCell(Grid.PosToCell(this), byte.MaxValue, delta);
			return;
		}
		if (delta != 0 && this.diseaseHandle.IsValid() && GameComps.DiseaseContainers.ModifyDiseaseCount(this.diseaseHandle, delta) <= 0 && !this.forcePermanentDiseaseContainer)
		{
			base.Trigger(-1689370368, false);
			GameComps.DiseaseContainers.Remove(base.gameObject);
			this.diseaseHandle.Clear();
		}
	}

	// Token: 0x06003F81 RID: 16257 RVA: 0x001628B4 File Offset: 0x00160AB4
	public void AddDisease(byte disease_idx, int delta, string reason)
	{
		if (delta == 0)
		{
			return;
		}
		if (this.diseaseRedirectTarget)
		{
			this.diseaseRedirectTarget.AddDisease(disease_idx, delta, reason);
			return;
		}
		if (this.useSimDiseaseInfo)
		{
			SimMessages.ModifyDiseaseOnCell(Grid.PosToCell(this), disease_idx, delta);
			return;
		}
		if (this.diseaseHandle.IsValid())
		{
			if (GameComps.DiseaseContainers.AddDisease(this.diseaseHandle, disease_idx, delta) <= 0)
			{
				GameComps.DiseaseContainers.Remove(base.gameObject);
				this.diseaseHandle.Clear();
				return;
			}
		}
		else if (delta > 0)
		{
			this.diseaseHandle = GameComps.DiseaseContainers.Add(base.gameObject, disease_idx, delta);
			base.Trigger(-1689370368, true);
			base.Trigger(-283306403, null);
		}
	}

	// Token: 0x06003F82 RID: 16258 RVA: 0x0016296E File Offset: 0x00160B6E
	private static float OnGetTemperature(PrimaryElement primary_element)
	{
		return primary_element._Temperature;
	}

	// Token: 0x06003F83 RID: 16259 RVA: 0x00162978 File Offset: 0x00160B78
	private static void OnSetTemperature(PrimaryElement primary_element, float temperature)
	{
		global::Debug.Assert(!float.IsNaN(temperature));
		if (temperature <= 0f)
		{
			DebugUtil.LogErrorArgs(primary_element.gameObject, new object[] { primary_element.gameObject.name + " has a temperature of zero which has always been an error in my experience." });
		}
		primary_element._Temperature = temperature;
	}

	// Token: 0x06003F84 RID: 16260 RVA: 0x001629CC File Offset: 0x00160BCC
	private void OnSplitFromChunk(object data)
	{
		Pickupable pickupable = (Pickupable)data;
		if (pickupable == null)
		{
			return;
		}
		float num = this.Units / (this.Units + pickupable.PrimaryElement.Units);
		SimUtil.DiseaseInfo percentOfDisease = SimUtil.GetPercentOfDisease(pickupable.PrimaryElement, num);
		this.AddDisease(percentOfDisease.idx, percentOfDisease.count, "PrimaryElement.SplitFromChunk");
		pickupable.PrimaryElement.ModifyDiseaseCount(-percentOfDisease.count, "PrimaryElement.SplitFromChunk");
	}

	// Token: 0x06003F85 RID: 16261 RVA: 0x00162A40 File Offset: 0x00160C40
	private void OnAbsorb(object data)
	{
		Pickupable pickupable = (Pickupable)data;
		if (pickupable == null)
		{
			return;
		}
		this.AddDisease(pickupable.PrimaryElement.DiseaseIdx, pickupable.PrimaryElement.DiseaseCount, "PrimaryElement.OnAbsorb");
	}

	// Token: 0x06003F86 RID: 16262 RVA: 0x00162A80 File Offset: 0x00160C80
	private void SetDiseaseVisualProvider(GameObject visualizer)
	{
		HandleVector<int>.Handle handle = GameComps.DiseaseContainers.GetHandle(base.gameObject);
		if (handle != HandleVector<int>.InvalidHandle)
		{
			DiseaseContainer payload = GameComps.DiseaseContainers.GetPayload(handle);
			payload.visualDiseaseProvider = visualizer;
			GameComps.DiseaseContainers.SetPayload(handle, ref payload);
		}
	}

	// Token: 0x06003F87 RID: 16263 RVA: 0x00162ACC File Offset: 0x00160CCC
	public void RedirectDisease(GameObject target)
	{
		this.SetDiseaseVisualProvider(target);
		this.diseaseRedirectTarget = (target ? target.GetComponent<PrimaryElement>() : null);
		global::Debug.Assert(this.diseaseRedirectTarget != this, "Disease redirect target set to myself");
	}

	// Token: 0x040029A0 RID: 10656
	public static float MAX_MASS = 100000f;

	// Token: 0x040029A1 RID: 10657
	public PrimaryElement.GetTemperatureCallback getTemperatureCallback = new PrimaryElement.GetTemperatureCallback(PrimaryElement.OnGetTemperature);

	// Token: 0x040029A2 RID: 10658
	public PrimaryElement.SetTemperatureCallback setTemperatureCallback = new PrimaryElement.SetTemperatureCallback(PrimaryElement.OnSetTemperature);

	// Token: 0x040029A3 RID: 10659
	private PrimaryElement diseaseRedirectTarget;

	// Token: 0x040029A4 RID: 10660
	private bool useSimDiseaseInfo;

	// Token: 0x040029A5 RID: 10661
	public const float DefaultChunkMass = 400f;

	// Token: 0x040029A6 RID: 10662
	private static readonly Tag[] metalTags = new Tag[]
	{
		GameTags.Metal,
		GameTags.RefinedMetal
	};

	// Token: 0x040029A7 RID: 10663
	[Serialize]
	[HashedEnum]
	public SimHashes ElementID;

	// Token: 0x040029A8 RID: 10664
	private float _units = 1f;

	// Token: 0x040029A9 RID: 10665
	[Serialize]
	[SerializeField]
	private float _Temperature;

	// Token: 0x040029AA RID: 10666
	[Serialize]
	[NonSerialized]
	public bool KeepZeroMassObject;

	// Token: 0x040029AB RID: 10667
	[Serialize]
	private HashedString diseaseID;

	// Token: 0x040029AC RID: 10668
	[Serialize]
	private int diseaseCount;

	// Token: 0x040029AD RID: 10669
	private HandleVector<int>.Handle diseaseHandle = HandleVector<int>.InvalidHandle;

	// Token: 0x040029AE RID: 10670
	public float MassPerUnit = 1f;

	// Token: 0x040029AF RID: 10671
	[NonSerialized]
	private Element _Element;

	// Token: 0x040029B0 RID: 10672
	[NonSerialized]
	public Action<PrimaryElement> onDataChanged;

	// Token: 0x040029B1 RID: 10673
	[NonSerialized]
	private bool forcePermanentDiseaseContainer;

	// Token: 0x040029B2 RID: 10674
	private static readonly EventSystem.IntraObjectHandler<PrimaryElement> OnSplitFromChunkDelegate = new EventSystem.IntraObjectHandler<PrimaryElement>(delegate(PrimaryElement component, object data)
	{
		component.OnSplitFromChunk(data);
	});

	// Token: 0x040029B3 RID: 10675
	private static readonly EventSystem.IntraObjectHandler<PrimaryElement> OnAbsorbDelegate = new EventSystem.IntraObjectHandler<PrimaryElement>(delegate(PrimaryElement component, object data)
	{
		component.OnAbsorb(data);
	});

	// Token: 0x02001673 RID: 5747
	// (Invoke) Token: 0x060087CA RID: 34762
	public delegate float GetTemperatureCallback(PrimaryElement primary_element);

	// Token: 0x02001674 RID: 5748
	// (Invoke) Token: 0x060087CE RID: 34766
	public delegate void SetTemperatureCallback(PrimaryElement primary_element, float temperature);
}
