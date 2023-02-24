using System;
using System.Collections.Generic;
using Database;
using UnityEngine;

// Token: 0x02000BC8 RID: 3016
public class MonumentSideScreen : SideScreenContent
{
	// Token: 0x06005ED7 RID: 24279 RVA: 0x0022A9A4 File Offset: 0x00228BA4
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<MonumentPart>() != null;
	}

	// Token: 0x06005ED8 RID: 24280 RVA: 0x0022A9B4 File Offset: 0x00228BB4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.debugVictoryButton.onClick += delegate
		{
			SaveGame.Instance.GetComponent<ColonyAchievementTracker>().DebugTriggerAchievement(Db.Get().ColonyAchievements.Thriving.Id);
			SaveGame.Instance.GetComponent<ColonyAchievementTracker>().DebugTriggerAchievement(Db.Get().ColonyAchievements.Clothe8Dupes.Id);
			SaveGame.Instance.GetComponent<ColonyAchievementTracker>().DebugTriggerAchievement(Db.Get().ColonyAchievements.Build4NatureReserves.Id);
			SaveGame.Instance.GetComponent<ColonyAchievementTracker>().DebugTriggerAchievement(Db.Get().ColonyAchievements.ReachedSpace.Id);
			GameScheduler.Instance.Schedule("ForceCheckAchievements", 0.1f, delegate(object data)
			{
				Game.Instance.Trigger(395452326, null);
			}, null, null);
		};
		this.debugVictoryButton.gameObject.SetActive(DebugHandler.InstantBuildMode && this.target.part == MonumentPartResource.Part.Top);
		this.flipButton.onClick += delegate
		{
			this.target.GetComponent<Rotatable>().Rotate();
		};
	}

	// Token: 0x06005ED9 RID: 24281 RVA: 0x0022AA30 File Offset: 0x00228C30
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.target = target.GetComponent<MonumentPart>();
		this.debugVictoryButton.gameObject.SetActive(DebugHandler.InstantBuildMode && this.target.part == MonumentPartResource.Part.Top);
		this.GenerateStateButtons();
	}

	// Token: 0x06005EDA RID: 24282 RVA: 0x0022AA80 File Offset: 0x00228C80
	public void GenerateStateButtons()
	{
		for (int i = this.buttons.Count - 1; i >= 0; i--)
		{
			Util.KDestroyGameObject(this.buttons[i]);
		}
		this.buttons.Clear();
		using (List<MonumentPartResource>.Enumerator enumerator = Db.GetMonumentParts().GetParts(this.target.part).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				MonumentPartResource state = enumerator.Current;
				GameObject gameObject = Util.KInstantiateUI(this.stateButtonPrefab, this.buttonContainer.gameObject, true);
				string state2 = state.State;
				string symbolName = state.SymbolName;
				gameObject.GetComponent<KButton>().onClick += delegate
				{
					this.target.SetState(state.Id);
				};
				this.buttons.Add(gameObject);
				gameObject.GetComponent<KButton>().fgImage.sprite = Def.GetUISpriteFromMultiObjectAnim(state.AnimFile, state2, false, symbolName);
			}
		}
	}

	// Token: 0x040040E3 RID: 16611
	private MonumentPart target;

	// Token: 0x040040E4 RID: 16612
	public KButton debugVictoryButton;

	// Token: 0x040040E5 RID: 16613
	public KButton flipButton;

	// Token: 0x040040E6 RID: 16614
	public GameObject stateButtonPrefab;

	// Token: 0x040040E7 RID: 16615
	private List<GameObject> buttons = new List<GameObject>();

	// Token: 0x040040E8 RID: 16616
	[SerializeField]
	private RectTransform buttonContainer;
}
