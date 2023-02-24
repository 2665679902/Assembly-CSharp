using System;
using System.Collections.Generic;
using Klei.AI;
using Klei.CustomSettings;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020004A3 RID: 1187
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/MinionIdentity")]
public class MinionIdentity : KMonoBehaviour, ISaveLoadable, IAssignableIdentity, IListableOption, ISim1000ms
{
	// Token: 0x17000105 RID: 261
	// (get) Token: 0x06001AAD RID: 6829 RVA: 0x0008E898 File Offset: 0x0008CA98
	// (set) Token: 0x06001AAE RID: 6830 RVA: 0x0008E8A0 File Offset: 0x0008CAA0
	[Serialize]
	public string genderStringKey { get; set; }

	// Token: 0x17000106 RID: 262
	// (get) Token: 0x06001AAF RID: 6831 RVA: 0x0008E8A9 File Offset: 0x0008CAA9
	// (set) Token: 0x06001AB0 RID: 6832 RVA: 0x0008E8B1 File Offset: 0x0008CAB1
	[Serialize]
	public string nameStringKey { get; set; }

	// Token: 0x17000107 RID: 263
	// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x0008E8BA File Offset: 0x0008CABA
	// (set) Token: 0x06001AB2 RID: 6834 RVA: 0x0008E8C2 File Offset: 0x0008CAC2
	[Serialize]
	public HashedString personalityResourceId { get; set; }

	// Token: 0x06001AB3 RID: 6835 RVA: 0x0008E8CB File Offset: 0x0008CACB
	public static void DestroyStatics()
	{
		MinionIdentity.maleNameList = null;
		MinionIdentity.femaleNameList = null;
	}

	// Token: 0x06001AB4 RID: 6836 RVA: 0x0008E8DC File Offset: 0x0008CADC
	protected override void OnPrefabInit()
	{
		if (this.name == null)
		{
			this.name = MinionIdentity.ChooseRandomName();
		}
		if (GameClock.Instance != null)
		{
			this.arrivalTime = (float)GameClock.Instance.GetCycle();
		}
		KAnimControllerBase component = base.GetComponent<KAnimControllerBase>();
		if (component != null)
		{
			KAnimControllerBase kanimControllerBase = component;
			kanimControllerBase.OnUpdateBounds = (Action<Bounds>)Delegate.Combine(kanimControllerBase.OnUpdateBounds, new Action<Bounds>(this.OnUpdateBounds));
		}
		GameUtil.SubscribeToTags<MinionIdentity>(this, MinionIdentity.OnDeadTagAddedDelegate, true);
	}

	// Token: 0x06001AB5 RID: 6837 RVA: 0x0008E958 File Offset: 0x0008CB58
	protected override void OnSpawn()
	{
		if (this.addToIdentityList)
		{
			this.ValidateProxy();
			this.CleanupLimboMinions();
		}
		PathProber component = base.GetComponent<PathProber>();
		if (component != null)
		{
			component.SetGroupProber(MinionGroupProber.Get());
		}
		this.SetName(this.name);
		if (this.nameStringKey == null)
		{
			this.nameStringKey = this.name;
		}
		this.SetGender(this.gender);
		if (this.genderStringKey == null)
		{
			this.genderStringKey = "NB";
		}
		if (this.personalityResourceId == HashedString.Invalid)
		{
			Personality personalityFromNameStringKey = Db.Get().Personalities.GetPersonalityFromNameStringKey(this.nameStringKey);
			if (personalityFromNameStringKey != null)
			{
				this.personalityResourceId = personalityFromNameStringKey.Id;
			}
		}
		if (this.addToIdentityList)
		{
			Components.MinionIdentities.Add(this);
			if (!base.gameObject.HasTag(GameTags.Dead))
			{
				Components.LiveMinionIdentities.Add(this);
				Game.Instance.Trigger(2144209314, this);
			}
		}
		SymbolOverrideController component2 = base.GetComponent<SymbolOverrideController>();
		if (component2 != null)
		{
			Accessorizer component3 = base.gameObject.GetComponent<Accessorizer>();
			if (component3 != null)
			{
				string text = HashCache.Get().Get(component3.GetAccessory(Db.Get().AccessorySlots.HeadShape).symbol.hash).Replace("headshape", "cheek");
				component2.AddSymbolOverride("snapto_cheek", Assets.GetAnim("head_swap_kanim").GetData().build.GetSymbol(text), 1);
				component2.AddSymbolOverride("snapto_hair_always", component3.GetAccessory(Db.Get().AccessorySlots.Hair).symbol, 1);
				component2.AddSymbolOverride(Db.Get().AccessorySlots.HatHair.targetSymbolId, Db.Get().AccessorySlots.HatHair.Lookup("hat_" + HashCache.Get().Get(component3.GetAccessory(Db.Get().AccessorySlots.Hair).symbol.hash)).symbol, 1);
			}
		}
		this.voiceId = (this.voiceIdx + 1).ToString("D2");
		Prioritizable component4 = base.GetComponent<Prioritizable>();
		if (component4 != null)
		{
			component4.showIcon = false;
		}
		Pickupable component5 = base.GetComponent<Pickupable>();
		if (component5 != null)
		{
			component5.carryAnimOverride = Assets.GetAnim("anim_incapacitated_carrier_kanim");
		}
		this.ApplyCustomGameSettings();
	}

	// Token: 0x06001AB6 RID: 6838 RVA: 0x0008EBEA File Offset: 0x0008CDEA
	public void ValidateProxy()
	{
		this.assignableProxy = MinionAssignablesProxy.InitAssignableProxy(this.assignableProxy, this);
	}

	// Token: 0x06001AB7 RID: 6839 RVA: 0x0008EC00 File Offset: 0x0008CE00
	private void CleanupLimboMinions()
	{
		KPrefabID component = base.GetComponent<KPrefabID>();
		if (component.InstanceID == -1)
		{
			DebugUtil.LogWarningArgs(new object[] { "Minion with an invalid kpid! Attempting to recover...", this.name });
			if (KPrefabIDTracker.Get().GetInstance(component.InstanceID) != null)
			{
				KPrefabIDTracker.Get().Unregister(component);
			}
			component.InstanceID = KPrefabID.GetUniqueID();
			KPrefabIDTracker.Get().Register(component);
			DebugUtil.LogWarningArgs(new object[] { "Restored as:", component.InstanceID });
		}
		if (component.conflicted)
		{
			DebugUtil.LogWarningArgs(new object[] { "Minion with a conflicted kpid! Attempting to recover... ", component.InstanceID, this.name });
			if (KPrefabIDTracker.Get().GetInstance(component.InstanceID) != null)
			{
				KPrefabIDTracker.Get().Unregister(component);
			}
			component.InstanceID = KPrefabID.GetUniqueID();
			KPrefabIDTracker.Get().Register(component);
			DebugUtil.LogWarningArgs(new object[] { "Restored as:", component.InstanceID });
		}
		this.assignableProxy.Get().SetTarget(this, base.gameObject);
	}

	// Token: 0x06001AB8 RID: 6840 RVA: 0x0008ED39 File Offset: 0x0008CF39
	public string GetProperName()
	{
		return base.gameObject.GetProperName();
	}

	// Token: 0x06001AB9 RID: 6841 RVA: 0x0008ED46 File Offset: 0x0008CF46
	public string GetVoiceId()
	{
		return this.voiceId;
	}

	// Token: 0x06001ABA RID: 6842 RVA: 0x0008ED4E File Offset: 0x0008CF4E
	public void SetName(string name)
	{
		this.name = name;
		if (this.selectable != null)
		{
			this.selectable.SetName(name);
		}
		base.gameObject.name = name;
		NameDisplayScreen.Instance.UpdateName(base.gameObject);
	}

	// Token: 0x06001ABB RID: 6843 RVA: 0x0008ED8D File Offset: 0x0008CF8D
	public void SetStickerType(string stickerType)
	{
		this.stickerType = stickerType;
	}

	// Token: 0x06001ABC RID: 6844 RVA: 0x0008ED96 File Offset: 0x0008CF96
	public bool IsNull()
	{
		return this == null;
	}

	// Token: 0x06001ABD RID: 6845 RVA: 0x0008ED9F File Offset: 0x0008CF9F
	public void SetGender(string gender)
	{
		this.gender = gender;
		this.selectable.SetGender(gender);
	}

	// Token: 0x06001ABE RID: 6846 RVA: 0x0008EDB4 File Offset: 0x0008CFB4
	public static string ChooseRandomName()
	{
		if (MinionIdentity.femaleNameList == null)
		{
			MinionIdentity.maleNameList = new MinionIdentity.NameList(Game.Instance.maleNamesFile);
			MinionIdentity.femaleNameList = new MinionIdentity.NameList(Game.Instance.femaleNamesFile);
		}
		if (UnityEngine.Random.value > 0.5f)
		{
			return MinionIdentity.maleNameList.Next();
		}
		return MinionIdentity.femaleNameList.Next();
	}

	// Token: 0x06001ABF RID: 6847 RVA: 0x0008EE14 File Offset: 0x0008D014
	protected override void OnCleanUp()
	{
		if (this.assignableProxy != null)
		{
			MinionAssignablesProxy minionAssignablesProxy = this.assignableProxy.Get();
			if (minionAssignablesProxy && minionAssignablesProxy.target == this)
			{
				Util.KDestroyGameObject(minionAssignablesProxy.gameObject);
			}
		}
		Components.MinionIdentities.Remove(this);
		Components.LiveMinionIdentities.Remove(this);
		Game.Instance.Trigger(2144209314, this);
	}

	// Token: 0x06001AC0 RID: 6848 RVA: 0x0008EE77 File Offset: 0x0008D077
	private void OnUpdateBounds(Bounds bounds)
	{
		KBoxCollider2D component = base.GetComponent<KBoxCollider2D>();
		component.offset = bounds.center;
		component.size = bounds.extents;
	}

	// Token: 0x06001AC1 RID: 6849 RVA: 0x0008EEA2 File Offset: 0x0008D0A2
	private void OnDied(object data)
	{
		this.GetSoleOwner().UnassignAll();
		this.GetEquipment().UnequipAll();
		Components.LiveMinionIdentities.Remove(this);
		Game.Instance.Trigger(2144209314, this);
	}

	// Token: 0x06001AC2 RID: 6850 RVA: 0x0008EED5 File Offset: 0x0008D0D5
	public List<Ownables> GetOwners()
	{
		return this.assignableProxy.Get().ownables;
	}

	// Token: 0x06001AC3 RID: 6851 RVA: 0x0008EEE7 File Offset: 0x0008D0E7
	public Ownables GetSoleOwner()
	{
		return this.assignableProxy.Get().GetComponent<Ownables>();
	}

	// Token: 0x06001AC4 RID: 6852 RVA: 0x0008EEF9 File Offset: 0x0008D0F9
	public bool HasOwner(Assignables owner)
	{
		return this.GetOwners().Contains(owner as Ownables);
	}

	// Token: 0x06001AC5 RID: 6853 RVA: 0x0008EF0C File Offset: 0x0008D10C
	public int NumOwners()
	{
		return this.GetOwners().Count;
	}

	// Token: 0x06001AC6 RID: 6854 RVA: 0x0008EF19 File Offset: 0x0008D119
	public Equipment GetEquipment()
	{
		return this.assignableProxy.Get().GetComponent<Equipment>();
	}

	// Token: 0x06001AC7 RID: 6855 RVA: 0x0008EF2C File Offset: 0x0008D12C
	public void Sim1000ms(float dt)
	{
		if (this == null)
		{
			return;
		}
		if (this.navigator == null)
		{
			this.navigator = base.GetComponent<Navigator>();
		}
		if (this.navigator != null && !this.navigator.IsMoving())
		{
			return;
		}
		if (this.choreDriver == null)
		{
			this.choreDriver = base.GetComponent<ChoreDriver>();
		}
		if (this.choreDriver != null)
		{
			Chore currentChore = this.choreDriver.GetCurrentChore();
			if (currentChore != null && currentChore is FetchAreaChore)
			{
				MinionResume component = base.GetComponent<MinionResume>();
				if (component != null)
				{
					component.AddExperienceWithAptitude(Db.Get().SkillGroups.Hauling.Id, dt, SKILLS.ALL_DAY_EXPERIENCE);
				}
			}
		}
	}

	// Token: 0x06001AC8 RID: 6856 RVA: 0x0008EFE8 File Offset: 0x0008D1E8
	private void ApplyCustomGameSettings()
	{
		SettingLevel currentQualitySetting = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.ImmuneSystem);
		if (currentQualitySetting.id == "Compromised")
		{
			Db.Get().Attributes.DiseaseCureSpeed.Lookup(this).Add(new AttributeModifier(Db.Get().Attributes.DiseaseCureSpeed.Id, -0.3333f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.IMMUNESYSTEM.LEVELS.COMPROMISED.ATTRIBUTE_MODIFIER_NAME, false, false, true));
			Db.Get().Attributes.GermResistance.Lookup(this).Add(new AttributeModifier(Db.Get().Attributes.GermResistance.Id, -2f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.IMMUNESYSTEM.LEVELS.COMPROMISED.ATTRIBUTE_MODIFIER_NAME, false, false, true));
		}
		else if (currentQualitySetting.id == "Weak")
		{
			Db.Get().Attributes.GermResistance.Lookup(this).Add(new AttributeModifier(Db.Get().Attributes.GermResistance.Id, -1f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.IMMUNESYSTEM.LEVELS.WEAK.ATTRIBUTE_MODIFIER_NAME, false, false, true));
		}
		else if (currentQualitySetting.id == "Strong")
		{
			Db.Get().Attributes.DiseaseCureSpeed.Lookup(this).Add(new AttributeModifier(Db.Get().Attributes.DiseaseCureSpeed.Id, 2f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.IMMUNESYSTEM.LEVELS.STRONG.ATTRIBUTE_MODIFIER_NAME, false, false, true));
			Db.Get().Attributes.GermResistance.Lookup(this).Add(new AttributeModifier(Db.Get().Attributes.GermResistance.Id, 2f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.IMMUNESYSTEM.LEVELS.STRONG.ATTRIBUTE_MODIFIER_NAME, false, false, true));
		}
		else if (currentQualitySetting.id == "Invincible")
		{
			Db.Get().Attributes.DiseaseCureSpeed.Lookup(this).Add(new AttributeModifier(Db.Get().Attributes.DiseaseCureSpeed.Id, 100000000f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.IMMUNESYSTEM.LEVELS.INVINCIBLE.ATTRIBUTE_MODIFIER_NAME, false, false, true));
			Db.Get().Attributes.GermResistance.Lookup(this).Add(new AttributeModifier(Db.Get().Attributes.GermResistance.Id, 200f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.IMMUNESYSTEM.LEVELS.INVINCIBLE.ATTRIBUTE_MODIFIER_NAME, false, false, true));
		}
		SettingLevel currentQualitySetting2 = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.Stress);
		if (currentQualitySetting2.id == "Doomed")
		{
			Db.Get().Amounts.Stress.deltaAttribute.Lookup(this).Add(new AttributeModifier(Db.Get().Amounts.Stress.deltaAttribute.Id, 0.033333335f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.STRESS.LEVELS.DOOMED.ATTRIBUTE_MODIFIER_NAME, false, false, true));
		}
		else if (currentQualitySetting2.id == "Pessimistic")
		{
			Db.Get().Amounts.Stress.deltaAttribute.Lookup(this).Add(new AttributeModifier(Db.Get().Amounts.Stress.deltaAttribute.Id, 0.016666668f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.STRESS.LEVELS.PESSIMISTIC.ATTRIBUTE_MODIFIER_NAME, false, false, true));
		}
		else if (currentQualitySetting2.id == "Optimistic")
		{
			Db.Get().Amounts.Stress.deltaAttribute.Lookup(this).Add(new AttributeModifier(Db.Get().Amounts.Stress.deltaAttribute.Id, -0.016666668f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.STRESS.LEVELS.OPTIMISTIC.ATTRIBUTE_MODIFIER_NAME, false, false, true));
		}
		else if (currentQualitySetting2.id == "Indomitable")
		{
			Db.Get().Amounts.Stress.deltaAttribute.Lookup(this).Add(new AttributeModifier(Db.Get().Amounts.Stress.deltaAttribute.Id, float.NegativeInfinity, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.STRESS.LEVELS.INDOMITABLE.ATTRIBUTE_MODIFIER_NAME, false, false, true));
		}
		SettingLevel currentQualitySetting3 = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.CalorieBurn);
		if (currentQualitySetting3.id == "VeryHard")
		{
			Db.Get().Amounts.Calories.deltaAttribute.Lookup(this).Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -1666.6666f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.CALORIE_BURN.LEVELS.VERYHARD.ATTRIBUTE_MODIFIER_NAME, false, false, true));
			return;
		}
		if (currentQualitySetting3.id == "Hard")
		{
			Db.Get().Amounts.Calories.deltaAttribute.Lookup(this).Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -833.3333f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.CALORIE_BURN.LEVELS.HARD.ATTRIBUTE_MODIFIER_NAME, false, false, true));
			return;
		}
		if (currentQualitySetting3.id == "Easy")
		{
			Db.Get().Amounts.Calories.deltaAttribute.Lookup(this).Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, 833.3333f, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.CALORIE_BURN.LEVELS.EASY.ATTRIBUTE_MODIFIER_NAME, false, false, true));
			return;
		}
		if (currentQualitySetting3.id == "Disabled")
		{
			Db.Get().Amounts.Calories.deltaAttribute.Lookup(this).Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, float.PositiveInfinity, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.CALORIE_BURN.LEVELS.DISABLED.ATTRIBUTE_MODIFIER_NAME, false, false, true));
		}
	}

	// Token: 0x04000ECE RID: 3790
	public const string HairAlwaysSymbol = "snapto_hair_always";

	// Token: 0x04000ECF RID: 3791
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x04000ED0 RID: 3792
	public int femaleVoiceCount;

	// Token: 0x04000ED1 RID: 3793
	public int maleVoiceCount;

	// Token: 0x04000ED2 RID: 3794
	[Serialize]
	private new string name;

	// Token: 0x04000ED3 RID: 3795
	[Serialize]
	public string gender;

	// Token: 0x04000ED7 RID: 3799
	[Serialize]
	public string stickerType;

	// Token: 0x04000ED8 RID: 3800
	[Serialize]
	[ReadOnly]
	public float arrivalTime;

	// Token: 0x04000ED9 RID: 3801
	[Serialize]
	public int voiceIdx;

	// Token: 0x04000EDA RID: 3802
	[Serialize]
	public Ref<MinionAssignablesProxy> assignableProxy;

	// Token: 0x04000EDB RID: 3803
	private Navigator navigator;

	// Token: 0x04000EDC RID: 3804
	private ChoreDriver choreDriver;

	// Token: 0x04000EDD RID: 3805
	public float timeLastSpoke;

	// Token: 0x04000EDE RID: 3806
	private string voiceId;

	// Token: 0x04000EDF RID: 3807
	private KAnimHashedString overrideExpression;

	// Token: 0x04000EE0 RID: 3808
	private KAnimHashedString expression;

	// Token: 0x04000EE1 RID: 3809
	public bool addToIdentityList = true;

	// Token: 0x04000EE2 RID: 3810
	private static MinionIdentity.NameList maleNameList;

	// Token: 0x04000EE3 RID: 3811
	private static MinionIdentity.NameList femaleNameList;

	// Token: 0x04000EE4 RID: 3812
	private static readonly EventSystem.IntraObjectHandler<MinionIdentity> OnDeadTagAddedDelegate = GameUtil.CreateHasTagHandler<MinionIdentity>(GameTags.Dead, delegate(MinionIdentity component, object data)
	{
		component.OnDied(data);
	});

	// Token: 0x020010E0 RID: 4320
	private class NameList
	{
		// Token: 0x060074C0 RID: 29888 RVA: 0x002B4070 File Offset: 0x002B2270
		public NameList(TextAsset file)
		{
			string[] array = file.text.Replace("  ", " ").Replace("\r\n", "\n").Split(new char[] { '\n' });
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[] { ' ' });
				if (array2[array2.Length - 1] != "" && array2[array2.Length - 1] != null)
				{
					this.names.Add(array2[array2.Length - 1]);
				}
			}
			this.names.Shuffle<string>();
		}

		// Token: 0x060074C1 RID: 29889 RVA: 0x002B4120 File Offset: 0x002B2320
		public string Next()
		{
			List<string> list = this.names;
			int num = this.idx;
			this.idx = num + 1;
			return list[num % this.names.Count];
		}

		// Token: 0x040058FC RID: 22780
		private List<string> names = new List<string>();

		// Token: 0x040058FD RID: 22781
		private int idx;
	}
}
