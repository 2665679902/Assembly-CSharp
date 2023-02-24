using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Database;
using Klei.AI;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020008F1 RID: 2289
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/MinionResume")]
public class MinionResume : KMonoBehaviour, ISaveLoadable, ISim200ms
{
	// Token: 0x170004A9 RID: 1193
	// (get) Token: 0x060041E2 RID: 16866 RVA: 0x00172DA4 File Offset: 0x00170FA4
	public MinionIdentity GetIdentity
	{
		get
		{
			return this.identity;
		}
	}

	// Token: 0x170004AA RID: 1194
	// (get) Token: 0x060041E3 RID: 16867 RVA: 0x00172DAC File Offset: 0x00170FAC
	public float TotalExperienceGained
	{
		get
		{
			return this.totalExperienceGained;
		}
	}

	// Token: 0x170004AB RID: 1195
	// (get) Token: 0x060041E4 RID: 16868 RVA: 0x00172DB4 File Offset: 0x00170FB4
	public int TotalSkillPointsGained
	{
		get
		{
			return MinionResume.CalculateTotalSkillPointsGained(this.TotalExperienceGained);
		}
	}

	// Token: 0x060041E5 RID: 16869 RVA: 0x00172DC1 File Offset: 0x00170FC1
	public static int CalculateTotalSkillPointsGained(float experience)
	{
		return Mathf.FloorToInt(Mathf.Pow(experience / (float)SKILLS.TARGET_SKILLS_CYCLE / 600f, 1f / SKILLS.EXPERIENCE_LEVEL_POWER) * (float)SKILLS.TARGET_SKILLS_EARNED);
	}

	// Token: 0x170004AC RID: 1196
	// (get) Token: 0x060041E6 RID: 16870 RVA: 0x00172DF0 File Offset: 0x00170FF0
	public int SkillsMastered
	{
		get
		{
			int num = 0;
			foreach (KeyValuePair<string, bool> keyValuePair in this.MasteryBySkillID)
			{
				if (keyValuePair.Value)
				{
					num++;
				}
			}
			return num;
		}
	}

	// Token: 0x170004AD RID: 1197
	// (get) Token: 0x060041E7 RID: 16871 RVA: 0x00172E4C File Offset: 0x0017104C
	public int AvailableSkillpoints
	{
		get
		{
			return this.TotalSkillPointsGained - this.SkillsMastered + ((this.GrantedSkillIDs == null) ? 0 : this.GrantedSkillIDs.Count);
		}
	}

	// Token: 0x060041E8 RID: 16872 RVA: 0x00172E74 File Offset: 0x00171074
	[OnDeserialized]
	private void OnDeserializedMethod()
	{
		if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 7))
		{
			foreach (KeyValuePair<string, bool> keyValuePair in this.MasteryByRoleID)
			{
				if (keyValuePair.Value && keyValuePair.Key != "NoRole")
				{
					this.ForceAddSkillPoint();
				}
			}
			foreach (KeyValuePair<HashedString, float> keyValuePair2 in this.AptitudeByRoleGroup)
			{
				this.AptitudeBySkillGroup[keyValuePair2.Key] = keyValuePair2.Value;
			}
		}
		if (this.TotalSkillPointsGained > 1000 || this.TotalSkillPointsGained < 0)
		{
			this.ForceSetSkillPoints(100);
		}
	}

	// Token: 0x060041E9 RID: 16873 RVA: 0x00172F70 File Offset: 0x00171170
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Components.MinionResumes.Add(this);
	}

	// Token: 0x060041EA RID: 16874 RVA: 0x00172F84 File Offset: 0x00171184
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.GrantedSkillIDs == null)
		{
			this.GrantedSkillIDs = new List<string>();
		}
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, bool> keyValuePair in this.MasteryBySkillID)
		{
			if (keyValuePair.Value && Db.Get().Skills.Get(keyValuePair.Key).deprecated)
			{
				list.Add(keyValuePair.Key);
			}
		}
		foreach (string text in list)
		{
			this.UnmasterSkill(text);
		}
		foreach (KeyValuePair<string, bool> keyValuePair2 in this.MasteryBySkillID)
		{
			if (keyValuePair2.Value)
			{
				Skill skill = Db.Get().Skills.Get(keyValuePair2.Key);
				foreach (SkillPerk skillPerk in skill.perks)
				{
					if (skillPerk.OnRemove != null)
					{
						skillPerk.OnRemove(this);
					}
					if (skillPerk.OnApply != null)
					{
						skillPerk.OnApply(this);
					}
				}
				if (!this.ownedHats.ContainsKey(skill.hat))
				{
					this.ownedHats.Add(skill.hat, true);
				}
			}
		}
		this.UpdateExpectations();
		this.UpdateMorale();
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		MinionResume.ApplyHat(this.currentHat, component);
		this.ShowNewSkillPointNotification();
	}

	// Token: 0x060041EB RID: 16875 RVA: 0x00173180 File Offset: 0x00171380
	public void RestoreResume(Dictionary<string, bool> MasteryBySkillID, Dictionary<HashedString, float> AptitudeBySkillGroup, List<string> GrantedSkillIDs, float totalExperienceGained)
	{
		this.MasteryBySkillID = MasteryBySkillID;
		this.GrantedSkillIDs = ((GrantedSkillIDs != null) ? GrantedSkillIDs : new List<string>());
		this.AptitudeBySkillGroup = AptitudeBySkillGroup;
		this.totalExperienceGained = totalExperienceGained;
	}

	// Token: 0x060041EC RID: 16876 RVA: 0x001731A9 File Offset: 0x001713A9
	protected override void OnCleanUp()
	{
		Components.MinionResumes.Remove(this);
		if (this.lastSkillNotification != null)
		{
			Game.Instance.GetComponent<Notifier>().Remove(this.lastSkillNotification);
			this.lastSkillNotification = null;
		}
		base.OnCleanUp();
	}

	// Token: 0x060041ED RID: 16877 RVA: 0x001731E0 File Offset: 0x001713E0
	public bool HasMasteredSkill(string skillId)
	{
		return this.MasteryBySkillID.ContainsKey(skillId) && this.MasteryBySkillID[skillId];
	}

	// Token: 0x060041EE RID: 16878 RVA: 0x00173200 File Offset: 0x00171400
	public void UpdateUrge()
	{
		if (this.targetHat != this.currentHat)
		{
			if (!base.gameObject.GetComponent<ChoreConsumer>().HasUrge(Db.Get().Urges.LearnSkill))
			{
				base.gameObject.GetComponent<ChoreConsumer>().AddUrge(Db.Get().Urges.LearnSkill);
				return;
			}
		}
		else
		{
			base.gameObject.GetComponent<ChoreConsumer>().RemoveUrge(Db.Get().Urges.LearnSkill);
		}
	}

	// Token: 0x170004AE RID: 1198
	// (get) Token: 0x060041EF RID: 16879 RVA: 0x00173280 File Offset: 0x00171480
	public string CurrentRole
	{
		get
		{
			return this.currentRole;
		}
	}

	// Token: 0x170004AF RID: 1199
	// (get) Token: 0x060041F0 RID: 16880 RVA: 0x00173288 File Offset: 0x00171488
	public string CurrentHat
	{
		get
		{
			return this.currentHat;
		}
	}

	// Token: 0x170004B0 RID: 1200
	// (get) Token: 0x060041F1 RID: 16881 RVA: 0x00173290 File Offset: 0x00171490
	public string TargetHat
	{
		get
		{
			return this.targetHat;
		}
	}

	// Token: 0x060041F2 RID: 16882 RVA: 0x00173298 File Offset: 0x00171498
	public void SetHats(string current, string target)
	{
		this.currentHat = current;
		this.targetHat = target;
	}

	// Token: 0x060041F3 RID: 16883 RVA: 0x001732A8 File Offset: 0x001714A8
	public void SetCurrentRole(string role_id)
	{
		this.currentRole = role_id;
	}

	// Token: 0x170004B1 RID: 1201
	// (get) Token: 0x060041F4 RID: 16884 RVA: 0x001732B1 File Offset: 0x001714B1
	public string TargetRole
	{
		get
		{
			return this.targetRole;
		}
	}

	// Token: 0x060041F5 RID: 16885 RVA: 0x001732BC File Offset: 0x001714BC
	private void ApplySkillPerks(string skillId)
	{
		foreach (SkillPerk skillPerk in Db.Get().Skills.Get(skillId).perks)
		{
			if (skillPerk.OnApply != null)
			{
				skillPerk.OnApply(this);
			}
		}
	}

	// Token: 0x060041F6 RID: 16886 RVA: 0x0017332C File Offset: 0x0017152C
	private void RemoveSkillPerks(string skillId)
	{
		foreach (SkillPerk skillPerk in Db.Get().Skills.Get(skillId).perks)
		{
			if (skillPerk.OnRemove != null)
			{
				skillPerk.OnRemove(this);
			}
		}
	}

	// Token: 0x060041F7 RID: 16887 RVA: 0x0017339C File Offset: 0x0017159C
	public void Sim200ms(float dt)
	{
		this.DEBUG_SecondsAlive += dt;
		if (!base.GetComponent<KPrefabID>().HasTag(GameTags.Dead))
		{
			this.DEBUG_PassiveExperienceGained += dt * SKILLS.PASSIVE_EXPERIENCE_PORTION;
			this.AddExperience(dt * SKILLS.PASSIVE_EXPERIENCE_PORTION);
		}
	}

	// Token: 0x060041F8 RID: 16888 RVA: 0x001733EC File Offset: 0x001715EC
	public bool IsAbleToLearnSkill(string skillId)
	{
		Skill skill = Db.Get().Skills.Get(skillId);
		string choreGroupID = Db.Get().SkillGroups.Get(skill.skillGroup).choreGroupID;
		if (!string.IsNullOrEmpty(choreGroupID))
		{
			Traits component = base.GetComponent<Traits>();
			if (component != null && component.IsChoreGroupDisabled(choreGroupID))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060041F9 RID: 16889 RVA: 0x00173450 File Offset: 0x00171650
	public bool BelowMoraleExpectation(Skill skill)
	{
		float num = Db.Get().Attributes.QualityOfLife.Lookup(this).GetTotalValue();
		float totalValue = Db.Get().Attributes.QualityOfLifeExpectation.Lookup(this).GetTotalValue();
		int moraleExpectation = skill.GetMoraleExpectation();
		if (this.AptitudeBySkillGroup.ContainsKey(skill.skillGroup) && this.AptitudeBySkillGroup[skill.skillGroup] > 0f)
		{
			num += 1f;
		}
		return totalValue + (float)moraleExpectation <= num;
	}

	// Token: 0x060041FA RID: 16890 RVA: 0x001734E0 File Offset: 0x001716E0
	public bool HasMasteredDirectlyRequiredSkillsForSkill(Skill skill)
	{
		for (int i = 0; i < skill.priorSkills.Count; i++)
		{
			if (!this.HasMasteredSkill(skill.priorSkills[i]))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060041FB RID: 16891 RVA: 0x0017351A File Offset: 0x0017171A
	public bool HasSkillPointsRequiredForSkill(Skill skill)
	{
		return this.AvailableSkillpoints >= 1;
	}

	// Token: 0x060041FC RID: 16892 RVA: 0x00173528 File Offset: 0x00171728
	public bool HasSkillAptitude(Skill skill)
	{
		return this.AptitudeBySkillGroup.ContainsKey(skill.skillGroup) && this.AptitudeBySkillGroup[skill.skillGroup] > 0f;
	}

	// Token: 0x060041FD RID: 16893 RVA: 0x00173562 File Offset: 0x00171762
	public bool HasBeenGrantedSkill(Skill skill)
	{
		return this.GrantedSkillIDs != null && this.GrantedSkillIDs.Contains(skill.Id);
	}

	// Token: 0x060041FE RID: 16894 RVA: 0x00173584 File Offset: 0x00171784
	public bool HasBeenGrantedSkill(string id)
	{
		return this.GrantedSkillIDs != null && this.GrantedSkillIDs.Contains(id);
	}

	// Token: 0x060041FF RID: 16895 RVA: 0x001735A4 File Offset: 0x001717A4
	public MinionResume.SkillMasteryConditions[] GetSkillMasteryConditions(string skillId)
	{
		List<MinionResume.SkillMasteryConditions> list = new List<MinionResume.SkillMasteryConditions>();
		Skill skill = Db.Get().Skills.Get(skillId);
		if (this.HasSkillAptitude(skill))
		{
			list.Add(MinionResume.SkillMasteryConditions.SkillAptitude);
		}
		if (!this.BelowMoraleExpectation(skill))
		{
			list.Add(MinionResume.SkillMasteryConditions.StressWarning);
		}
		if (!this.IsAbleToLearnSkill(skillId))
		{
			list.Add(MinionResume.SkillMasteryConditions.UnableToLearn);
		}
		if (!this.HasSkillPointsRequiredForSkill(skill))
		{
			list.Add(MinionResume.SkillMasteryConditions.NeedsSkillPoints);
		}
		if (!this.HasMasteredDirectlyRequiredSkillsForSkill(skill))
		{
			list.Add(MinionResume.SkillMasteryConditions.MissingPreviousSkill);
		}
		return list.ToArray();
	}

	// Token: 0x06004200 RID: 16896 RVA: 0x0017361E File Offset: 0x0017181E
	public bool CanMasterSkill(MinionResume.SkillMasteryConditions[] masteryConditions)
	{
		return !Array.Exists<MinionResume.SkillMasteryConditions>(masteryConditions, (MinionResume.SkillMasteryConditions element) => element == MinionResume.SkillMasteryConditions.UnableToLearn || element == MinionResume.SkillMasteryConditions.NeedsSkillPoints || element == MinionResume.SkillMasteryConditions.MissingPreviousSkill);
	}

	// Token: 0x06004201 RID: 16897 RVA: 0x0017364A File Offset: 0x0017184A
	public bool OwnsHat(string hatId)
	{
		return this.ownedHats.ContainsKey(hatId) && this.ownedHats[hatId];
	}

	// Token: 0x06004202 RID: 16898 RVA: 0x00173668 File Offset: 0x00171868
	public void SkillLearned()
	{
		if (base.gameObject.GetComponent<ChoreConsumer>().HasUrge(Db.Get().Urges.LearnSkill))
		{
			base.gameObject.GetComponent<ChoreConsumer>().RemoveUrge(Db.Get().Urges.LearnSkill);
		}
		foreach (string text in this.ownedHats.Keys.ToList<string>())
		{
			this.ownedHats[text] = true;
		}
		if (this.targetHat != null && this.currentHat != this.targetHat)
		{
			new PutOnHatChore(this, Db.Get().ChoreTypes.SwitchHat);
		}
	}

	// Token: 0x06004203 RID: 16899 RVA: 0x0017373C File Offset: 0x0017193C
	public void MasterSkill(string skillId)
	{
		if (!base.gameObject.GetComponent<ChoreConsumer>().HasUrge(Db.Get().Urges.LearnSkill))
		{
			base.gameObject.GetComponent<ChoreConsumer>().AddUrge(Db.Get().Urges.LearnSkill);
		}
		this.MasteryBySkillID[skillId] = true;
		this.ApplySkillPerks(skillId);
		this.UpdateExpectations();
		this.UpdateMorale();
		this.TriggerMasterSkillEvents();
		GameScheduler.Instance.Schedule("Morale Tutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Morale, true);
		}, null, null);
		if (!this.ownedHats.ContainsKey(Db.Get().Skills.Get(skillId).hat))
		{
			this.ownedHats.Add(Db.Get().Skills.Get(skillId).hat, false);
		}
		if (this.AvailableSkillpoints == 0 && this.lastSkillNotification != null)
		{
			Game.Instance.GetComponent<Notifier>().Remove(this.lastSkillNotification);
			this.lastSkillNotification = null;
		}
	}

	// Token: 0x06004204 RID: 16900 RVA: 0x00173854 File Offset: 0x00171A54
	public void UnmasterSkill(string skillId)
	{
		if (this.MasteryBySkillID.ContainsKey(skillId))
		{
			this.MasteryBySkillID.Remove(skillId);
			this.RemoveSkillPerks(skillId);
			this.UpdateExpectations();
			this.UpdateMorale();
			this.TriggerMasterSkillEvents();
		}
	}

	// Token: 0x06004205 RID: 16901 RVA: 0x0017388C File Offset: 0x00171A8C
	public void GrantSkill(string skillId)
	{
		if (this.GrantedSkillIDs == null)
		{
			this.GrantedSkillIDs = new List<string>();
		}
		if (!this.HasBeenGrantedSkill(skillId))
		{
			this.MasteryBySkillID[skillId] = true;
			this.ApplySkillPerks(skillId);
			this.GrantedSkillIDs.Add(skillId);
			this.UpdateExpectations();
			this.UpdateMorale();
			this.TriggerMasterSkillEvents();
			if (!this.ownedHats.ContainsKey(Db.Get().Skills.Get(skillId).hat))
			{
				this.ownedHats.Add(Db.Get().Skills.Get(skillId).hat, false);
			}
		}
	}

	// Token: 0x06004206 RID: 16902 RVA: 0x0017392A File Offset: 0x00171B2A
	private void TriggerMasterSkillEvents()
	{
		base.Trigger(540773776, null);
		Game.Instance.Trigger(-1523247426, this);
	}

	// Token: 0x06004207 RID: 16903 RVA: 0x00173948 File Offset: 0x00171B48
	public void ForceSetSkillPoints(int points)
	{
		this.totalExperienceGained = MinionResume.CalculatePreviousExperienceBar(points);
	}

	// Token: 0x06004208 RID: 16904 RVA: 0x00173956 File Offset: 0x00171B56
	public void ForceAddSkillPoint()
	{
		this.AddExperience(MinionResume.CalculateNextExperienceBar(this.TotalSkillPointsGained) - this.totalExperienceGained);
	}

	// Token: 0x06004209 RID: 16905 RVA: 0x00173970 File Offset: 0x00171B70
	public static float CalculateNextExperienceBar(int current_skill_points)
	{
		return Mathf.Pow((float)(current_skill_points + 1) / (float)SKILLS.TARGET_SKILLS_EARNED, SKILLS.EXPERIENCE_LEVEL_POWER) * (float)SKILLS.TARGET_SKILLS_CYCLE * 600f;
	}

	// Token: 0x0600420A RID: 16906 RVA: 0x00173994 File Offset: 0x00171B94
	public static float CalculatePreviousExperienceBar(int current_skill_points)
	{
		return Mathf.Pow((float)current_skill_points / (float)SKILLS.TARGET_SKILLS_EARNED, SKILLS.EXPERIENCE_LEVEL_POWER) * (float)SKILLS.TARGET_SKILLS_CYCLE * 600f;
	}

	// Token: 0x0600420B RID: 16907 RVA: 0x001739B8 File Offset: 0x00171BB8
	private void UpdateExpectations()
	{
		int num = 0;
		foreach (KeyValuePair<string, bool> keyValuePair in this.MasteryBySkillID)
		{
			if (keyValuePair.Value && !this.HasBeenGrantedSkill(keyValuePair.Key))
			{
				Skill skill = Db.Get().Skills.Get(keyValuePair.Key);
				num += skill.tier + 1;
			}
		}
		AttributeInstance attributeInstance = Db.Get().Attributes.QualityOfLifeExpectation.Lookup(this);
		if (this.skillsMoraleExpectationModifier != null)
		{
			attributeInstance.Remove(this.skillsMoraleExpectationModifier);
			this.skillsMoraleExpectationModifier = null;
		}
		if (num > 0)
		{
			this.skillsMoraleExpectationModifier = new AttributeModifier(attributeInstance.Id, (float)num, DUPLICANTS.NEEDS.QUALITYOFLIFE.EXPECTATION_MOD_NAME, false, false, true);
			attributeInstance.Add(this.skillsMoraleExpectationModifier);
		}
	}

	// Token: 0x0600420C RID: 16908 RVA: 0x00173AA4 File Offset: 0x00171CA4
	private void UpdateMorale()
	{
		int num = 0;
		foreach (KeyValuePair<string, bool> keyValuePair in this.MasteryBySkillID)
		{
			if (keyValuePair.Value && !this.HasBeenGrantedSkill(keyValuePair.Key))
			{
				Skill skill = Db.Get().Skills.Get(keyValuePair.Key);
				float num2 = 0f;
				if (this.AptitudeBySkillGroup.TryGetValue(new HashedString(skill.skillGroup), out num2))
				{
					num += (int)num2;
				}
			}
		}
		AttributeInstance attributeInstance = Db.Get().Attributes.QualityOfLife.Lookup(this);
		if (this.skillsMoraleModifier != null)
		{
			attributeInstance.Remove(this.skillsMoraleModifier);
			this.skillsMoraleModifier = null;
		}
		if (num > 0)
		{
			this.skillsMoraleModifier = new AttributeModifier(attributeInstance.Id, (float)num, DUPLICANTS.NEEDS.QUALITYOFLIFE.APTITUDE_SKILLS_MOD_NAME, false, false, true);
			attributeInstance.Add(this.skillsMoraleModifier);
		}
	}

	// Token: 0x0600420D RID: 16909 RVA: 0x00173BAC File Offset: 0x00171DAC
	private void OnSkillPointGained()
	{
		Game.Instance.Trigger(1505456302, this);
		this.ShowNewSkillPointNotification();
		if (PopFXManager.Instance != null)
		{
			string text = MISC.NOTIFICATIONS.SKILL_POINT_EARNED.NAME.Replace("{Duplicant}", this.identity.GetProperName());
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, text, base.transform, new Vector3(0f, 0.5f, 0f), 1.5f, false, false);
		}
		new UpgradeFX.Instance(base.gameObject.GetComponent<KMonoBehaviour>(), new Vector3(0f, 0f, -0.1f)).StartSM();
	}

	// Token: 0x0600420E RID: 16910 RVA: 0x00173C58 File Offset: 0x00171E58
	private void ShowNewSkillPointNotification()
	{
		if (this.AvailableSkillpoints == 1)
		{
			this.lastSkillNotification = new ManagementMenuNotification(global::Action.ManageSkills, NotificationValence.Good, this.identity.GetSoleOwner().gameObject.GetInstanceID().ToString(), MISC.NOTIFICATIONS.SKILL_POINT_EARNED.NAME.Replace("{Duplicant}", this.identity.GetProperName()), NotificationType.Good, new Func<List<Notification>, object, string>(this.GetSkillPointGainedTooltip), this.identity, true, 0f, delegate(object d)
			{
				ManagementMenu.Instance.OpenSkills(this.identity);
			}, null, null, true);
			base.GetComponent<Notifier>().Add(this.lastSkillNotification, "");
		}
	}

	// Token: 0x0600420F RID: 16911 RVA: 0x00173CF4 File Offset: 0x00171EF4
	private string GetSkillPointGainedTooltip(List<Notification> notifications, object data)
	{
		return MISC.NOTIFICATIONS.SKILL_POINT_EARNED.TOOLTIP.Replace("{Duplicant}", ((MinionIdentity)data).GetProperName());
	}

	// Token: 0x06004210 RID: 16912 RVA: 0x00173D10 File Offset: 0x00171F10
	public void SetAptitude(HashedString skillGroupID, float amount)
	{
		this.AptitudeBySkillGroup[skillGroupID] = amount;
	}

	// Token: 0x06004211 RID: 16913 RVA: 0x00173D20 File Offset: 0x00171F20
	public float GetAptitudeExperienceMultiplier(HashedString skillGroupId, float buildingFrequencyMultiplier)
	{
		float num = 0f;
		this.AptitudeBySkillGroup.TryGetValue(skillGroupId, out num);
		return 1f + num * SKILLS.APTITUDE_EXPERIENCE_MULTIPLIER * buildingFrequencyMultiplier;
	}

	// Token: 0x06004212 RID: 16914 RVA: 0x00173D54 File Offset: 0x00171F54
	public void AddExperience(float amount)
	{
		float num = this.totalExperienceGained;
		float num2 = MinionResume.CalculateNextExperienceBar(this.TotalSkillPointsGained);
		this.totalExperienceGained += amount;
		if (base.isSpawned && this.totalExperienceGained >= num2 && num < num2)
		{
			this.OnSkillPointGained();
		}
	}

	// Token: 0x06004213 RID: 16915 RVA: 0x00173DA0 File Offset: 0x00171FA0
	public void AddExperienceWithAptitude(string skillGroupId, float amount, float buildingMultiplier)
	{
		float num = amount * this.GetAptitudeExperienceMultiplier(skillGroupId, buildingMultiplier) * SKILLS.ACTIVE_EXPERIENCE_PORTION;
		this.DEBUG_ActiveExperienceGained += num;
		this.AddExperience(num);
	}

	// Token: 0x06004214 RID: 16916 RVA: 0x00173DD8 File Offset: 0x00171FD8
	public bool HasPerk(HashedString perkId)
	{
		foreach (KeyValuePair<string, bool> keyValuePair in this.MasteryBySkillID)
		{
			if (keyValuePair.Value && Db.Get().Skills.Get(keyValuePair.Key).GivesPerk(perkId))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004215 RID: 16917 RVA: 0x00173E54 File Offset: 0x00172054
	public bool HasPerk(SkillPerk perk)
	{
		foreach (KeyValuePair<string, bool> keyValuePair in this.MasteryBySkillID)
		{
			if (keyValuePair.Value && Db.Get().Skills.Get(keyValuePair.Key).GivesPerk(perk))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004216 RID: 16918 RVA: 0x00173ED0 File Offset: 0x001720D0
	public void RemoveHat()
	{
		MinionResume.RemoveHat(base.GetComponent<KBatchedAnimController>());
	}

	// Token: 0x06004217 RID: 16919 RVA: 0x00173EE0 File Offset: 0x001720E0
	public static void RemoveHat(KBatchedAnimController controller)
	{
		AccessorySlot hat = Db.Get().AccessorySlots.Hat;
		Accessorizer component = controller.GetComponent<Accessorizer>();
		if (component != null)
		{
			Accessory accessory = component.GetAccessory(hat);
			if (accessory != null)
			{
				component.RemoveAccessory(accessory);
			}
		}
		else
		{
			controller.GetComponent<SymbolOverrideController>().TryRemoveSymbolOverride(hat.targetSymbolId, 4);
		}
		controller.SetSymbolVisiblity(hat.targetSymbolId, false);
		controller.SetSymbolVisiblity(Db.Get().AccessorySlots.HatHair.targetSymbolId, false);
		controller.SetSymbolVisiblity(Db.Get().AccessorySlots.Hair.targetSymbolId, true);
	}

	// Token: 0x06004218 RID: 16920 RVA: 0x00173F7C File Offset: 0x0017217C
	public static void AddHat(string hat_id, KBatchedAnimController controller)
	{
		AccessorySlot hat = Db.Get().AccessorySlots.Hat;
		Accessory accessory = hat.Lookup(hat_id);
		if (accessory == null)
		{
			global::Debug.LogWarning("Missing hat: " + hat_id);
		}
		Accessorizer component = controller.GetComponent<Accessorizer>();
		if (component != null)
		{
			Accessory accessory2 = component.GetAccessory(Db.Get().AccessorySlots.Hat);
			if (accessory2 != null)
			{
				component.RemoveAccessory(accessory2);
			}
			if (accessory != null)
			{
				component.AddAccessory(accessory);
			}
		}
		else
		{
			SymbolOverrideController component2 = controller.GetComponent<SymbolOverrideController>();
			component2.TryRemoveSymbolOverride(hat.targetSymbolId, 4);
			component2.AddSymbolOverride(hat.targetSymbolId, accessory.symbol, 4);
		}
		controller.SetSymbolVisiblity(hat.targetSymbolId, true);
		controller.SetSymbolVisiblity(Db.Get().AccessorySlots.HatHair.targetSymbolId, true);
		controller.SetSymbolVisiblity(Db.Get().AccessorySlots.Hair.targetSymbolId, false);
	}

	// Token: 0x06004219 RID: 16921 RVA: 0x00174064 File Offset: 0x00172264
	public void ApplyTargetHat()
	{
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		MinionResume.ApplyHat(this.targetHat, component);
		this.currentHat = this.targetHat;
		this.targetHat = null;
	}

	// Token: 0x0600421A RID: 16922 RVA: 0x00174097 File Offset: 0x00172297
	public static void ApplyHat(string hat_id, KBatchedAnimController controller)
	{
		if (hat_id.IsNullOrWhiteSpace())
		{
			MinionResume.RemoveHat(controller);
			return;
		}
		MinionResume.AddHat(hat_id, controller);
	}

	// Token: 0x0600421B RID: 16923 RVA: 0x001740AF File Offset: 0x001722AF
	public string GetSkillsSubtitle()
	{
		return string.Format(DUPLICANTS.NEEDS.QUALITYOFLIFE.TOTAL_SKILL_POINTS, this.TotalSkillPointsGained);
	}

	// Token: 0x0600421C RID: 16924 RVA: 0x001740CC File Offset: 0x001722CC
	public static bool AnyMinionHasPerk(string perk, int worldId = -1)
	{
		using (List<MinionResume>.Enumerator enumerator = ((worldId >= 0) ? Components.MinionResumes.GetWorldItems(worldId, true) : Components.MinionResumes.Items).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.HasPerk(perk))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600421D RID: 16925 RVA: 0x00174144 File Offset: 0x00172344
	public static bool AnyOtherMinionHasPerk(string perk, MinionResume me)
	{
		foreach (MinionResume minionResume in Components.MinionResumes.Items)
		{
			if (!(minionResume == me) && minionResume.HasPerk(perk))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600421E RID: 16926 RVA: 0x001741B4 File Offset: 0x001723B4
	public void ResetSkillLevels(bool returnSkillPoints = true)
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, bool> keyValuePair in this.MasteryBySkillID)
		{
			if (keyValuePair.Value)
			{
				list.Add(keyValuePair.Key);
			}
		}
		foreach (string text in list)
		{
			this.UnmasterSkill(text);
		}
	}

	// Token: 0x04002BED RID: 11245
	[MyCmpReq]
	private MinionIdentity identity;

	// Token: 0x04002BEE RID: 11246
	[Serialize]
	public Dictionary<string, bool> MasteryByRoleID = new Dictionary<string, bool>();

	// Token: 0x04002BEF RID: 11247
	[Serialize]
	public Dictionary<string, bool> MasteryBySkillID = new Dictionary<string, bool>();

	// Token: 0x04002BF0 RID: 11248
	[Serialize]
	public List<string> GrantedSkillIDs = new List<string>();

	// Token: 0x04002BF1 RID: 11249
	[Serialize]
	public Dictionary<HashedString, float> AptitudeByRoleGroup = new Dictionary<HashedString, float>();

	// Token: 0x04002BF2 RID: 11250
	[Serialize]
	public Dictionary<HashedString, float> AptitudeBySkillGroup = new Dictionary<HashedString, float>();

	// Token: 0x04002BF3 RID: 11251
	[Serialize]
	private string currentRole = "NoRole";

	// Token: 0x04002BF4 RID: 11252
	[Serialize]
	private string targetRole = "NoRole";

	// Token: 0x04002BF5 RID: 11253
	[Serialize]
	private string currentHat;

	// Token: 0x04002BF6 RID: 11254
	[Serialize]
	private string targetHat;

	// Token: 0x04002BF7 RID: 11255
	private Dictionary<string, bool> ownedHats = new Dictionary<string, bool>();

	// Token: 0x04002BF8 RID: 11256
	[Serialize]
	private float totalExperienceGained;

	// Token: 0x04002BF9 RID: 11257
	private Notification lastSkillNotification;

	// Token: 0x04002BFA RID: 11258
	private AttributeModifier skillsMoraleExpectationModifier;

	// Token: 0x04002BFB RID: 11259
	private AttributeModifier skillsMoraleModifier;

	// Token: 0x04002BFC RID: 11260
	public float DEBUG_PassiveExperienceGained;

	// Token: 0x04002BFD RID: 11261
	public float DEBUG_ActiveExperienceGained;

	// Token: 0x04002BFE RID: 11262
	public float DEBUG_SecondsAlive;

	// Token: 0x020016BB RID: 5819
	public enum SkillMasteryConditions
	{
		// Token: 0x04006AB8 RID: 27320
		SkillAptitude,
		// Token: 0x04006AB9 RID: 27321
		StressWarning,
		// Token: 0x04006ABA RID: 27322
		UnableToLearn,
		// Token: 0x04006ABB RID: 27323
		NeedsSkillPoints,
		// Token: 0x04006ABC RID: 27324
		MissingPreviousSkill
	}
}
