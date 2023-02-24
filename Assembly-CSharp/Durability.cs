using System;
using Klei.CustomSettings;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x0200072C RID: 1836
[AddComponentMenu("KMonoBehaviour/scripts/Durability")]
public class Durability : KMonoBehaviour
{
	// Token: 0x1700039A RID: 922
	// (get) Token: 0x0600324C RID: 12876 RVA: 0x0010D206 File Offset: 0x0010B406
	// (set) Token: 0x0600324D RID: 12877 RVA: 0x0010D20E File Offset: 0x0010B40E
	public float TimeEquipped
	{
		get
		{
			return this.timeEquipped;
		}
		set
		{
			this.timeEquipped = value;
		}
	}

	// Token: 0x0600324E RID: 12878 RVA: 0x0010D217 File Offset: 0x0010B417
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<Durability>(-1617557748, Durability.OnEquippedDelegate);
		base.Subscribe<Durability>(-170173755, Durability.OnUnequippedDelegate);
	}

	// Token: 0x0600324F RID: 12879 RVA: 0x0010D244 File Offset: 0x0010B444
	protected override void OnSpawn()
	{
		base.GetComponent<KSelectable>().AddStatusItem(Db.Get().MiscStatusItems.Durability, base.gameObject);
		SettingLevel currentQualitySetting = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.Durability);
		if (currentQualitySetting != null)
		{
			string id = currentQualitySetting.id;
			if (id != null)
			{
				if (id == "Indestructible")
				{
					this.difficultySettingMod = EQUIPMENT.SUITS.INDESTRUCTIBLE_DURABILITY_MOD;
					return;
				}
				if (id == "Reinforced")
				{
					this.difficultySettingMod = EQUIPMENT.SUITS.REINFORCED_DURABILITY_MOD;
					return;
				}
				if (id == "Flimsy")
				{
					this.difficultySettingMod = EQUIPMENT.SUITS.FLIMSY_DURABILITY_MOD;
					return;
				}
				if (!(id == "Threadbare"))
				{
					return;
				}
				this.difficultySettingMod = EQUIPMENT.SUITS.THREADBARE_DURABILITY_MOD;
			}
		}
	}

	// Token: 0x06003250 RID: 12880 RVA: 0x0010D2F3 File Offset: 0x0010B4F3
	private void OnEquipped()
	{
		if (!this.isEquipped)
		{
			this.isEquipped = true;
			this.timeEquipped = GameClock.Instance.GetTimeInCycles();
		}
	}

	// Token: 0x06003251 RID: 12881 RVA: 0x0010D314 File Offset: 0x0010B514
	private void OnUnequipped()
	{
		if (this.isEquipped)
		{
			this.isEquipped = false;
			float num = GameClock.Instance.GetTimeInCycles() - this.timeEquipped;
			this.DeltaDurability(num * this.durabilityLossPerCycle);
		}
	}

	// Token: 0x06003252 RID: 12882 RVA: 0x0010D350 File Offset: 0x0010B550
	private void DeltaDurability(float delta)
	{
		delta *= this.difficultySettingMod;
		this.durability = Mathf.Clamp01(this.durability + delta);
	}

	// Token: 0x06003253 RID: 12883 RVA: 0x0010D370 File Offset: 0x0010B570
	public void ConvertToWornObject()
	{
		GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(this.wornEquipmentPrefabID), Grid.SceneLayer.Ore, null, 0);
		gameObject.transform.SetPosition(base.transform.GetPosition());
		gameObject.GetComponent<PrimaryElement>().SetElement(base.GetComponent<PrimaryElement>().ElementID, false);
		gameObject.SetActive(true);
		EquippableFacade component = base.GetComponent<EquippableFacade>();
		if (component != null)
		{
			gameObject.GetComponent<RepairableEquipment>().facadeID = component.FacadeID;
		}
		Storage component2 = base.gameObject.GetComponent<Storage>();
		if (component2)
		{
			JetSuitTank component3 = base.gameObject.GetComponent<JetSuitTank>();
			if (component3)
			{
				component2.AddLiquid(SimHashes.Petroleum, component3.amount, base.GetComponent<PrimaryElement>().Temperature, byte.MaxValue, 0, false, true);
			}
			component2.DropAll(false, false, default(Vector3), true, null);
		}
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x06003254 RID: 12884 RVA: 0x0010D45C File Offset: 0x0010B65C
	public float GetDurability()
	{
		if (this.isEquipped)
		{
			float num = GameClock.Instance.GetTimeInCycles() - this.timeEquipped;
			return this.durability - num * this.durabilityLossPerCycle;
		}
		return this.durability;
	}

	// Token: 0x06003255 RID: 12885 RVA: 0x0010D499 File Offset: 0x0010B699
	public bool IsWornOut()
	{
		return this.GetDurability() <= 0f;
	}

	// Token: 0x04001E8E RID: 7822
	private static readonly EventSystem.IntraObjectHandler<Durability> OnEquippedDelegate = new EventSystem.IntraObjectHandler<Durability>(delegate(Durability component, object data)
	{
		component.OnEquipped();
	});

	// Token: 0x04001E8F RID: 7823
	private static readonly EventSystem.IntraObjectHandler<Durability> OnUnequippedDelegate = new EventSystem.IntraObjectHandler<Durability>(delegate(Durability component, object data)
	{
		component.OnUnequipped();
	});

	// Token: 0x04001E90 RID: 7824
	[Serialize]
	private bool isEquipped;

	// Token: 0x04001E91 RID: 7825
	[Serialize]
	private float timeEquipped;

	// Token: 0x04001E92 RID: 7826
	[Serialize]
	private float durability = 1f;

	// Token: 0x04001E93 RID: 7827
	public float durabilityLossPerCycle = -0.1f;

	// Token: 0x04001E94 RID: 7828
	public string wornEquipmentPrefabID;

	// Token: 0x04001E95 RID: 7829
	private float difficultySettingMod = 1f;
}
