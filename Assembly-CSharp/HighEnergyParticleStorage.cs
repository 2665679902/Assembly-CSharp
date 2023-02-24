using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020007AC RID: 1964
public class HighEnergyParticleStorage : KMonoBehaviour, IStorage
{
	// Token: 0x17000411 RID: 1041
	// (get) Token: 0x06003790 RID: 14224 RVA: 0x00134E3D File Offset: 0x0013303D
	public float Particles
	{
		get
		{
			return this.particles;
		}
	}

	// Token: 0x17000412 RID: 1042
	// (get) Token: 0x06003791 RID: 14225 RVA: 0x00134E45 File Offset: 0x00133045
	// (set) Token: 0x06003792 RID: 14226 RVA: 0x00134E4D File Offset: 0x0013304D
	public bool allowUIItemRemoval { get; set; }

	// Token: 0x06003793 RID: 14227 RVA: 0x00134E58 File Offset: 0x00133058
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (this.autoStore)
		{
			HighEnergyParticlePort component = base.gameObject.GetComponent<HighEnergyParticlePort>();
			component.onParticleCapture = (HighEnergyParticlePort.OnParticleCapture)Delegate.Combine(component.onParticleCapture, new HighEnergyParticlePort.OnParticleCapture(this.OnParticleCapture));
			component.onParticleCaptureAllowed = (HighEnergyParticlePort.OnParticleCaptureAllowed)Delegate.Combine(component.onParticleCaptureAllowed, new HighEnergyParticlePort.OnParticleCaptureAllowed(this.OnParticleCaptureAllowed));
		}
		this.SetupStorageStatusItems();
	}

	// Token: 0x06003794 RID: 14228 RVA: 0x00134EC7 File Offset: 0x001330C7
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.UpdateLogicPorts();
	}

	// Token: 0x06003795 RID: 14229 RVA: 0x00134ED8 File Offset: 0x001330D8
	private void UpdateLogicPorts()
	{
		if (this._logicPorts != null)
		{
			bool flag = this.IsFull();
			this._logicPorts.SendSignal(this.PORT_ID, Convert.ToInt32(flag));
		}
	}

	// Token: 0x06003796 RID: 14230 RVA: 0x00134F16 File Offset: 0x00133116
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.autoStore)
		{
			HighEnergyParticlePort component = base.gameObject.GetComponent<HighEnergyParticlePort>();
			component.onParticleCapture = (HighEnergyParticlePort.OnParticleCapture)Delegate.Remove(component.onParticleCapture, new HighEnergyParticlePort.OnParticleCapture(this.OnParticleCapture));
		}
	}

	// Token: 0x06003797 RID: 14231 RVA: 0x00134F54 File Offset: 0x00133154
	private void OnParticleCapture(HighEnergyParticle particle)
	{
		float num = Mathf.Min(particle.payload, this.capacity - this.particles);
		this.Store(num);
		particle.payload -= num;
		if (particle.payload > 0f)
		{
			base.gameObject.GetComponent<HighEnergyParticlePort>().Uncapture(particle);
		}
	}

	// Token: 0x06003798 RID: 14232 RVA: 0x00134FAE File Offset: 0x001331AE
	private bool OnParticleCaptureAllowed(HighEnergyParticle particle)
	{
		return this.particles < this.capacity && this.receiverOpen;
	}

	// Token: 0x06003799 RID: 14233 RVA: 0x00134FC8 File Offset: 0x001331C8
	private void DeltaParticles(float delta)
	{
		this.particles += delta;
		if (this.particles <= 0f)
		{
			base.Trigger(155636535, base.transform.gameObject);
		}
		base.Trigger(-1837862626, base.transform.gameObject);
		this.UpdateLogicPorts();
	}

	// Token: 0x0600379A RID: 14234 RVA: 0x00135024 File Offset: 0x00133224
	public float Store(float amount)
	{
		float num = Mathf.Min(amount, this.RemainingCapacity());
		this.DeltaParticles(num);
		return num;
	}

	// Token: 0x0600379B RID: 14235 RVA: 0x00135046 File Offset: 0x00133246
	public float ConsumeAndGet(float amount)
	{
		amount = Mathf.Min(this.Particles, amount);
		this.DeltaParticles(-amount);
		return amount;
	}

	// Token: 0x0600379C RID: 14236 RVA: 0x0013505F File Offset: 0x0013325F
	[ContextMenu("Trigger Stored Event")]
	public void DEBUG_TriggerStorageEvent()
	{
		base.Trigger(-1837862626, base.transform.gameObject);
	}

	// Token: 0x0600379D RID: 14237 RVA: 0x00135077 File Offset: 0x00133277
	[ContextMenu("Trigger Zero Event")]
	public void DEBUG_TriggerZeroEvent()
	{
		this.ConsumeAndGet(this.particles + 1f);
	}

	// Token: 0x0600379E RID: 14238 RVA: 0x0013508C File Offset: 0x0013328C
	public float ConsumeAll()
	{
		return this.ConsumeAndGet(this.particles);
	}

	// Token: 0x0600379F RID: 14239 RVA: 0x0013509A File Offset: 0x0013329A
	public bool HasRadiation()
	{
		return this.Particles > 0f;
	}

	// Token: 0x060037A0 RID: 14240 RVA: 0x001350A9 File Offset: 0x001332A9
	public GameObject Drop(GameObject go, bool do_disease_transfer = true)
	{
		return null;
	}

	// Token: 0x060037A1 RID: 14241 RVA: 0x001350AC File Offset: 0x001332AC
	public List<GameObject> GetItems()
	{
		return new List<GameObject> { base.gameObject };
	}

	// Token: 0x060037A2 RID: 14242 RVA: 0x001350BF File Offset: 0x001332BF
	public bool IsFull()
	{
		return this.RemainingCapacity() <= 0f;
	}

	// Token: 0x060037A3 RID: 14243 RVA: 0x001350D1 File Offset: 0x001332D1
	public bool IsEmpty()
	{
		return this.Particles == 0f;
	}

	// Token: 0x060037A4 RID: 14244 RVA: 0x001350E0 File Offset: 0x001332E0
	public float Capacity()
	{
		return this.capacity;
	}

	// Token: 0x060037A5 RID: 14245 RVA: 0x001350E8 File Offset: 0x001332E8
	public float RemainingCapacity()
	{
		return Mathf.Max(this.capacity - this.Particles, 0f);
	}

	// Token: 0x060037A6 RID: 14246 RVA: 0x00135101 File Offset: 0x00133301
	public bool ShouldShowInUI()
	{
		return this.showInUI;
	}

	// Token: 0x060037A7 RID: 14247 RVA: 0x00135109 File Offset: 0x00133309
	public float GetAmountAvailable(Tag tag)
	{
		if (tag != GameTags.HighEnergyParticle)
		{
			return 0f;
		}
		return this.Particles;
	}

	// Token: 0x060037A8 RID: 14248 RVA: 0x00135124 File Offset: 0x00133324
	public void ConsumeIgnoringDisease(Tag tag, float amount)
	{
		DebugUtil.DevAssert(tag == GameTags.HighEnergyParticle, "Consuming non-particle tag as amount", null);
		this.ConsumeAndGet(amount);
	}

	// Token: 0x060037A9 RID: 14249 RVA: 0x00135144 File Offset: 0x00133344
	private void SetupStorageStatusItems()
	{
		if (HighEnergyParticleStorage.capacityStatusItem == null)
		{
			HighEnergyParticleStorage.capacityStatusItem = new StatusItem("StorageLocker", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			HighEnergyParticleStorage.capacityStatusItem.resolveStringCallback = delegate(string str, object data)
			{
				HighEnergyParticleStorage highEnergyParticleStorage = (HighEnergyParticleStorage)data;
				string text = Util.FormatWholeNumber(highEnergyParticleStorage.particles);
				string text2 = Util.FormatWholeNumber(highEnergyParticleStorage.capacity);
				str = str.Replace("{Stored}", text);
				str = str.Replace("{Capacity}", text2);
				str = str.Replace("{Units}", UI.UNITSUFFIXES.HIGHENERGYPARTICLES.PARTRICLES);
				return str;
			};
		}
		if (this.showCapacityStatusItem)
		{
			if (this.showCapacityAsMainStatus)
			{
				base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, HighEnergyParticleStorage.capacityStatusItem, this);
				return;
			}
			base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Stored, HighEnergyParticleStorage.capacityStatusItem, this);
		}
	}

	// Token: 0x0400253D RID: 9533
	[Serialize]
	[SerializeField]
	private float particles;

	// Token: 0x0400253E RID: 9534
	public float capacity = float.MaxValue;

	// Token: 0x0400253F RID: 9535
	public bool showInUI = true;

	// Token: 0x04002540 RID: 9536
	public bool showCapacityStatusItem;

	// Token: 0x04002541 RID: 9537
	public bool showCapacityAsMainStatus;

	// Token: 0x04002543 RID: 9539
	public bool autoStore;

	// Token: 0x04002544 RID: 9540
	[Serialize]
	public bool receiverOpen = true;

	// Token: 0x04002545 RID: 9541
	[MyCmpGet]
	private LogicPorts _logicPorts;

	// Token: 0x04002546 RID: 9542
	public string PORT_ID = "";

	// Token: 0x04002547 RID: 9543
	private static StatusItem capacityStatusItem;
}
