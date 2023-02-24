using System;
using System.Collections.Generic;
using Database;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000964 RID: 2404
[SerializationConfig(MemberSerialization.OptIn)]
public class Spacecraft
{
	// Token: 0x06004719 RID: 18201 RVA: 0x00190789 File Offset: 0x0018E989
	public Spacecraft(LaunchConditionManager launchConditions)
	{
		this.launchConditions = launchConditions;
	}

	// Token: 0x0600471A RID: 18202 RVA: 0x001907BA File Offset: 0x0018E9BA
	public Spacecraft()
	{
	}

	// Token: 0x1700054E RID: 1358
	// (get) Token: 0x0600471B RID: 18203 RVA: 0x001907E4 File Offset: 0x0018E9E4
	// (set) Token: 0x0600471C RID: 18204 RVA: 0x001907F1 File Offset: 0x0018E9F1
	public LaunchConditionManager launchConditions
	{
		get
		{
			return this.refLaunchConditions.Get();
		}
		set
		{
			this.refLaunchConditions.Set(value);
		}
	}

	// Token: 0x0600471D RID: 18205 RVA: 0x001907FF File Offset: 0x0018E9FF
	public void SetRocketName(string newName)
	{
		this.rocketName = newName;
		this.UpdateNameOnRocketModules();
	}

	// Token: 0x0600471E RID: 18206 RVA: 0x0019080E File Offset: 0x0018EA0E
	public string GetRocketName()
	{
		return this.rocketName;
	}

	// Token: 0x0600471F RID: 18207 RVA: 0x00190818 File Offset: 0x0018EA18
	public void UpdateNameOnRocketModules()
	{
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(this.launchConditions.GetComponent<AttachableBuilding>()))
		{
			RocketModule component = gameObject.GetComponent<RocketModule>();
			if (component != null)
			{
				component.SetParentRocketName(this.rocketName);
			}
		}
	}

	// Token: 0x06004720 RID: 18208 RVA: 0x00190888 File Offset: 0x0018EA88
	public bool HasInvalidID()
	{
		return this.id == -1;
	}

	// Token: 0x06004721 RID: 18209 RVA: 0x00190893 File Offset: 0x0018EA93
	public void SetID(int id)
	{
		this.id = id;
	}

	// Token: 0x06004722 RID: 18210 RVA: 0x0019089C File Offset: 0x0018EA9C
	public void SetState(Spacecraft.MissionState state)
	{
		this.state = state;
	}

	// Token: 0x06004723 RID: 18211 RVA: 0x001908A5 File Offset: 0x0018EAA5
	public void BeginMission(SpaceDestination destination)
	{
		this.missionElapsed = 0f;
		this.missionDuration = (float)destination.OneBasedDistance * ROCKETRY.MISSION_DURATION_SCALE / this.GetPilotNavigationEfficiency();
		this.SetState(Spacecraft.MissionState.Launching);
	}

	// Token: 0x06004724 RID: 18212 RVA: 0x001908D4 File Offset: 0x0018EAD4
	private float GetPilotNavigationEfficiency()
	{
		List<MinionStorage.Info> storedMinionInfo = this.launchConditions.GetComponent<MinionStorage>().GetStoredMinionInfo();
		if (storedMinionInfo.Count < 1)
		{
			return 1f;
		}
		StoredMinionIdentity component = storedMinionInfo[0].serializedMinion.Get().GetComponent<StoredMinionIdentity>();
		string text = Db.Get().Attributes.SpaceNavigation.Id;
		float num = 1f;
		foreach (KeyValuePair<string, bool> keyValuePair in component.MasteryBySkillID)
		{
			foreach (SkillPerk skillPerk in Db.Get().Skills.Get(keyValuePair.Key).perks)
			{
				SkillAttributePerk skillAttributePerk = skillPerk as SkillAttributePerk;
				if (skillAttributePerk != null && skillAttributePerk.modifier.AttributeId == text)
				{
					num += skillAttributePerk.modifier.Value;
				}
			}
		}
		return num;
	}

	// Token: 0x06004725 RID: 18213 RVA: 0x001909F4 File Offset: 0x0018EBF4
	public void ForceComplete()
	{
		this.missionElapsed = this.missionDuration;
	}

	// Token: 0x06004726 RID: 18214 RVA: 0x00190A04 File Offset: 0x0018EC04
	public void ProgressMission(float deltaTime)
	{
		if (this.state == Spacecraft.MissionState.Underway)
		{
			this.missionElapsed += deltaTime;
			if (this.controlStationBuffTimeRemaining > 0f)
			{
				this.missionElapsed += deltaTime * 0.20000005f;
				this.controlStationBuffTimeRemaining -= deltaTime;
			}
			else
			{
				this.controlStationBuffTimeRemaining = 0f;
			}
			if (this.missionElapsed > this.missionDuration)
			{
				this.CompleteMission();
			}
		}
	}

	// Token: 0x06004727 RID: 18215 RVA: 0x00190A78 File Offset: 0x0018EC78
	public float GetTimeLeft()
	{
		return this.missionDuration - this.missionElapsed;
	}

	// Token: 0x06004728 RID: 18216 RVA: 0x00190A87 File Offset: 0x0018EC87
	public float GetDuration()
	{
		return this.missionDuration;
	}

	// Token: 0x06004729 RID: 18217 RVA: 0x00190A8F File Offset: 0x0018EC8F
	public void CompleteMission()
	{
		SpacecraftManager.instance.PushReadyToLandNotification(this);
		this.SetState(Spacecraft.MissionState.WaitingToLand);
		this.Land();
	}

	// Token: 0x0600472A RID: 18218 RVA: 0x00190AAC File Offset: 0x0018ECAC
	private void Land()
	{
		this.launchConditions.Trigger(-1165815793, SpacecraftManager.instance.GetSpacecraftDestination(this.id));
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(this.launchConditions.GetComponent<AttachableBuilding>()))
		{
			if (gameObject != this.launchConditions.gameObject)
			{
				gameObject.Trigger(-1165815793, SpacecraftManager.instance.GetSpacecraftDestination(this.id));
			}
		}
	}

	// Token: 0x0600472B RID: 18219 RVA: 0x00190B50 File Offset: 0x0018ED50
	public void TemporallyTear()
	{
		SpacecraftManager.instance.hasVisitedWormHole = true;
		LaunchConditionManager launchConditions = this.launchConditions;
		for (int i = launchConditions.rocketModules.Count - 1; i >= 0; i--)
		{
			Storage component = launchConditions.rocketModules[i].GetComponent<Storage>();
			if (component != null)
			{
				component.ConsumeAllIgnoringDisease();
			}
			MinionStorage component2 = launchConditions.rocketModules[i].GetComponent<MinionStorage>();
			if (component2 != null)
			{
				List<MinionStorage.Info> storedMinionInfo = component2.GetStoredMinionInfo();
				for (int j = storedMinionInfo.Count - 1; j >= 0; j--)
				{
					component2.DeleteStoredMinion(storedMinionInfo[j].id);
				}
			}
			Util.KDestroyGameObject(launchConditions.rocketModules[i].gameObject);
		}
	}

	// Token: 0x0600472C RID: 18220 RVA: 0x00190C13 File Offset: 0x0018EE13
	public void GenerateName()
	{
		this.SetRocketName(GameUtil.GenerateRandomRocketName());
	}

	// Token: 0x04002F1C RID: 12060
	[Serialize]
	public int id = -1;

	// Token: 0x04002F1D RID: 12061
	[Serialize]
	public string rocketName = UI.STARMAP.DEFAULT_NAME;

	// Token: 0x04002F1E RID: 12062
	[Serialize]
	public float controlStationBuffTimeRemaining;

	// Token: 0x04002F1F RID: 12063
	[Serialize]
	public Ref<LaunchConditionManager> refLaunchConditions = new Ref<LaunchConditionManager>();

	// Token: 0x04002F20 RID: 12064
	[Serialize]
	public Spacecraft.MissionState state;

	// Token: 0x04002F21 RID: 12065
	[Serialize]
	private float missionElapsed;

	// Token: 0x04002F22 RID: 12066
	[Serialize]
	private float missionDuration;

	// Token: 0x02001766 RID: 5990
	public enum MissionState
	{
		// Token: 0x04006CF2 RID: 27890
		Grounded,
		// Token: 0x04006CF3 RID: 27891
		Launching,
		// Token: 0x04006CF4 RID: 27892
		Underway,
		// Token: 0x04006CF5 RID: 27893
		WaitingToLand,
		// Token: 0x04006CF6 RID: 27894
		Landing,
		// Token: 0x04006CF7 RID: 27895
		Destroyed
	}
}
