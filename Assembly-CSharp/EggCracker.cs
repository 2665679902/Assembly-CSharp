using System;
using TUNING;
using UnityEngine;

// Token: 0x020005AD RID: 1453
[AddComponentMenu("KMonoBehaviour/scripts/EggCracker")]
public class EggCracker : KMonoBehaviour
{
	// Token: 0x060023F0 RID: 9200 RVA: 0x000C269C File Offset: 0x000C089C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.refinery.choreType = Db.Get().ChoreTypes.Cook;
		this.refinery.fetchChoreTypeIdHash = Db.Get().ChoreTypes.CookFetch.IdHash;
		this.workable.WorkerStatusItem = Db.Get().DuplicantStatusItems.Processing;
		this.workable.AttributeConverter = Db.Get().AttributeConverters.CookingSpeed;
		this.workable.AttributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.MOST_DAY_EXPERIENCE;
		this.workable.SkillExperienceSkillGroup = Db.Get().SkillGroups.Cooking.Id;
		this.workable.SkillExperienceMultiplier = SKILLS.MOST_DAY_EXPERIENCE;
		ComplexFabricatorWorkable complexFabricatorWorkable = this.workable;
		complexFabricatorWorkable.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(complexFabricatorWorkable.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnWorkableEvent));
	}

	// Token: 0x060023F1 RID: 9201 RVA: 0x000C2782 File Offset: 0x000C0982
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		UnityEngine.Object.Destroy(this.tracker);
		this.tracker = null;
	}

	// Token: 0x060023F2 RID: 9202 RVA: 0x000C279C File Offset: 0x000C099C
	private void OnWorkableEvent(Workable workable, Workable.WorkableEvent e)
	{
		if (e == Workable.WorkableEvent.WorkStarted)
		{
			ComplexRecipe currentWorkingOrder = this.refinery.CurrentWorkingOrder;
			if (currentWorkingOrder != null)
			{
				ComplexRecipe.RecipeElement[] ingredients = currentWorkingOrder.ingredients;
				if (ingredients.Length != 0)
				{
					ComplexRecipe.RecipeElement recipeElement = ingredients[0];
					this.display_egg = this.refinery.buildStorage.FindFirst(recipeElement.material);
					this.PositionActiveEgg();
					return;
				}
			}
		}
		else if (e == Workable.WorkableEvent.WorkCompleted)
		{
			if (this.display_egg)
			{
				this.display_egg.GetComponent<KBatchedAnimController>().Play("hatching_pst", KAnim.PlayMode.Once, 1f, 0f);
				return;
			}
		}
		else if (e == Workable.WorkableEvent.WorkStopped)
		{
			UnityEngine.Object.Destroy(this.tracker);
			this.tracker = null;
			this.display_egg = null;
		}
	}

	// Token: 0x060023F3 RID: 9203 RVA: 0x000C2844 File Offset: 0x000C0A44
	private void PositionActiveEgg()
	{
		if (!this.display_egg)
		{
			return;
		}
		KBatchedAnimController component = this.display_egg.GetComponent<KBatchedAnimController>();
		component.enabled = true;
		component.SetSceneLayer(Grid.SceneLayer.BuildingUse);
		KSelectable component2 = this.display_egg.GetComponent<KSelectable>();
		if (component2 != null)
		{
			component2.enabled = true;
		}
		this.tracker = this.display_egg.AddComponent<KBatchedAnimTracker>();
		this.tracker.symbol = "snapto_egg";
	}

	// Token: 0x040014A6 RID: 5286
	[MyCmpReq]
	private ComplexFabricator refinery;

	// Token: 0x040014A7 RID: 5287
	[MyCmpReq]
	private ComplexFabricatorWorkable workable;

	// Token: 0x040014A8 RID: 5288
	private KBatchedAnimTracker tracker;

	// Token: 0x040014A9 RID: 5289
	private GameObject display_egg;
}
