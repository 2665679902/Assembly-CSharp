using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x0200060C RID: 1548
public class MicrobeMusher : ComplexFabricator
{
	// Token: 0x06002861 RID: 10337 RVA: 0x000D62B2 File Offset: 0x000D44B2
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.choreType = Db.Get().ChoreTypes.Cook;
		this.fetchChoreTypeIdHash = Db.Get().ChoreTypes.CookFetch.IdHash;
	}

	// Token: 0x06002862 RID: 10338 RVA: 0x000D62EC File Offset: 0x000D44EC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		GameScheduler.Instance.Schedule("WaterFetchingTutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_FetchingWater, true);
		}, null, null);
		this.workable.WorkerStatusItem = Db.Get().DuplicantStatusItems.Mushing;
		this.workable.AttributeConverter = Db.Get().AttributeConverters.CookingSpeed;
		this.workable.AttributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.MOST_DAY_EXPERIENCE;
		this.workable.SkillExperienceSkillGroup = Db.Get().SkillGroups.Cooking.Id;
		this.workable.SkillExperienceMultiplier = SKILLS.MOST_DAY_EXPERIENCE;
		this.workable.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Behind, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_ration" });
		this.workable.meter.meterController.SetSymbolVisiblity(MicrobeMusher.canHash, false);
		this.workable.meter.meterController.SetSymbolVisiblity(MicrobeMusher.meterRationHash, false);
	}

	// Token: 0x06002863 RID: 10339 RVA: 0x000D6418 File Offset: 0x000D4618
	protected override List<GameObject> SpawnOrderProduct(ComplexRecipe recipe)
	{
		List<GameObject> list = base.SpawnOrderProduct(recipe);
		foreach (GameObject gameObject in list)
		{
			PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
			if (component != null)
			{
				if (gameObject.PrefabID() == "MushBar")
				{
					byte index = Db.Get().Diseases.GetIndex("FoodPoisoning");
					component.AddDisease(index, 1000, "Made of mud");
				}
				if (gameObject.GetComponent<PrimaryElement>().DiseaseCount > 0)
				{
					Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_DiseaseCooking, true);
				}
			}
		}
		return list;
	}

	// Token: 0x040017B3 RID: 6067
	[SerializeField]
	public Vector3 mushbarSpawnOffset = Vector3.right;

	// Token: 0x040017B4 RID: 6068
	private static readonly KAnimHashedString meterRationHash = new KAnimHashedString("meter_ration");

	// Token: 0x040017B5 RID: 6069
	private static readonly KAnimHashedString canHash = new KAnimHashedString("can");
}
