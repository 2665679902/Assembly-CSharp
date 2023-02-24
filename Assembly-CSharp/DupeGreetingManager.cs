using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;

// Token: 0x0200072B RID: 1835
[AddComponentMenu("KMonoBehaviour/scripts/DupeGreetingManager")]
public class DupeGreetingManager : KMonoBehaviour, ISim200ms
{
	// Token: 0x06003242 RID: 12866 RVA: 0x0010CCAB File Offset: 0x0010AEAB
	protected override void OnPrefabInit()
	{
		this.candidateCells = new Dictionary<int, MinionIdentity>();
		this.activeSetups = new List<DupeGreetingManager.GreetingSetup>();
		this.cooldowns = new Dictionary<MinionIdentity, float>();
	}

	// Token: 0x06003243 RID: 12867 RVA: 0x0010CCD0 File Offset: 0x0010AED0
	public void Sim200ms(float dt)
	{
		if (GameClock.Instance.GetTime() / 600f < TuningData<DupeGreetingManager.Tuning>.Get().cyclesBeforeFirstGreeting)
		{
			return;
		}
		for (int i = this.activeSetups.Count - 1; i >= 0; i--)
		{
			DupeGreetingManager.GreetingSetup greetingSetup = this.activeSetups[i];
			if (!this.ValidNavigatingMinion(greetingSetup.A.minion) || !this.ValidOppositionalMinion(greetingSetup.A.minion, greetingSetup.B.minion))
			{
				greetingSetup.A.reactable.Cleanup();
				greetingSetup.B.reactable.Cleanup();
				this.activeSetups.RemoveAt(i);
			}
		}
		this.candidateCells.Clear();
		foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.Items)
		{
			if ((!this.cooldowns.ContainsKey(minionIdentity) || GameClock.Instance.GetTime() - this.cooldowns[minionIdentity] >= 720f * TuningData<DupeGreetingManager.Tuning>.Get().greetingDelayMultiplier) && this.ValidNavigatingMinion(minionIdentity))
			{
				for (int j = 0; j <= 2; j++)
				{
					int offsetCell = this.GetOffsetCell(minionIdentity, j);
					if (this.candidateCells.ContainsKey(offsetCell) && this.ValidOppositionalMinion(minionIdentity, this.candidateCells[offsetCell]))
					{
						this.BeginNewGreeting(minionIdentity, this.candidateCells[offsetCell], offsetCell);
						break;
					}
					this.candidateCells[offsetCell] = minionIdentity;
				}
			}
		}
	}

	// Token: 0x06003244 RID: 12868 RVA: 0x0010CE78 File Offset: 0x0010B078
	private int GetOffsetCell(MinionIdentity minion, int offset)
	{
		if (!minion.GetComponent<Facing>().GetFacing())
		{
			return Grid.OffsetCell(Grid.PosToCell(minion), offset, 0);
		}
		return Grid.OffsetCell(Grid.PosToCell(minion), -offset, 0);
	}

	// Token: 0x06003245 RID: 12869 RVA: 0x0010CEA4 File Offset: 0x0010B0A4
	private bool ValidNavigatingMinion(MinionIdentity minion)
	{
		if (minion == null)
		{
			return false;
		}
		Navigator component = minion.GetComponent<Navigator>();
		return component != null && component.IsMoving() && component.CurrentNavType == NavType.Floor;
	}

	// Token: 0x06003246 RID: 12870 RVA: 0x0010CEE0 File Offset: 0x0010B0E0
	private bool ValidOppositionalMinion(MinionIdentity reference_minion, MinionIdentity minion)
	{
		if (reference_minion == null)
		{
			return false;
		}
		if (minion == null)
		{
			return false;
		}
		Facing component = minion.GetComponent<Facing>();
		Facing component2 = reference_minion.GetComponent<Facing>();
		return this.ValidNavigatingMinion(minion) && component != null && component2 != null && component.GetFacing() != component2.GetFacing();
	}

	// Token: 0x06003247 RID: 12871 RVA: 0x0010CF40 File Offset: 0x0010B140
	private void BeginNewGreeting(MinionIdentity minion_a, MinionIdentity minion_b, int cell)
	{
		DupeGreetingManager.GreetingSetup greetingSetup = new DupeGreetingManager.GreetingSetup();
		greetingSetup.cell = cell;
		greetingSetup.A = new DupeGreetingManager.GreetingUnit(minion_a, this.GetReactable(minion_a));
		greetingSetup.B = new DupeGreetingManager.GreetingUnit(minion_b, this.GetReactable(minion_b));
		this.activeSetups.Add(greetingSetup);
	}

	// Token: 0x06003248 RID: 12872 RVA: 0x0010CF8C File Offset: 0x0010B18C
	private Reactable GetReactable(MinionIdentity minion)
	{
		if (DupeGreetingManager.emotes == null)
		{
			DupeGreetingManager.emotes = new List<Emote>
			{
				Db.Get().Emotes.Minion.Wave,
				Db.Get().Emotes.Minion.Wave_Shy,
				Db.Get().Emotes.Minion.FingerGuns
			};
		}
		Emote emote = DupeGreetingManager.emotes[UnityEngine.Random.Range(0, DupeGreetingManager.emotes.Count)];
		SelfEmoteReactable selfEmoteReactable = new SelfEmoteReactable(minion.gameObject, "NavigatorPassingGreeting", Db.Get().ChoreTypes.Emote, 1000f, 20f, float.PositiveInfinity, 0f);
		selfEmoteReactable.SetEmote(emote).SetThought(Db.Get().Thoughts.Chatty);
		selfEmoteReactable.RegisterEmoteStepCallbacks("react", new Action<GameObject>(this.BeginReacting), null);
		return selfEmoteReactable;
	}

	// Token: 0x06003249 RID: 12873 RVA: 0x0010D088 File Offset: 0x0010B288
	private void BeginReacting(GameObject minionGO)
	{
		if (minionGO == null)
		{
			return;
		}
		MinionIdentity component = minionGO.GetComponent<MinionIdentity>();
		Vector3 vector = Vector3.zero;
		foreach (DupeGreetingManager.GreetingSetup greetingSetup in this.activeSetups)
		{
			if (greetingSetup.A.minion == component)
			{
				if (greetingSetup.B.minion != null)
				{
					vector = greetingSetup.B.minion.transform.GetPosition();
					greetingSetup.A.minion.Trigger(-594200555, greetingSetup.B.minion);
					greetingSetup.B.minion.Trigger(-594200555, greetingSetup.A.minion);
					break;
				}
				break;
			}
			else if (greetingSetup.B.minion == component)
			{
				if (greetingSetup.A.minion != null)
				{
					vector = greetingSetup.A.minion.transform.GetPosition();
					break;
				}
				break;
			}
		}
		minionGO.GetComponent<Facing>().SetFacing(vector.x < minionGO.transform.GetPosition().x);
		minionGO.GetComponent<Effects>().Add("Greeting", true);
		this.cooldowns[component] = GameClock.Instance.GetTime();
	}

	// Token: 0x04001E89 RID: 7817
	private const float COOLDOWN_TIME = 720f;

	// Token: 0x04001E8A RID: 7818
	private Dictionary<int, MinionIdentity> candidateCells;

	// Token: 0x04001E8B RID: 7819
	private List<DupeGreetingManager.GreetingSetup> activeSetups;

	// Token: 0x04001E8C RID: 7820
	private Dictionary<MinionIdentity, float> cooldowns;

	// Token: 0x04001E8D RID: 7821
	private static List<Emote> emotes;

	// Token: 0x0200142B RID: 5163
	public class Tuning : TuningData<DupeGreetingManager.Tuning>
	{
		// Token: 0x040062AE RID: 25262
		public float cyclesBeforeFirstGreeting;

		// Token: 0x040062AF RID: 25263
		public float greetingDelayMultiplier;
	}

	// Token: 0x0200142C RID: 5164
	private class GreetingUnit
	{
		// Token: 0x06008047 RID: 32839 RVA: 0x002DEE3E File Offset: 0x002DD03E
		public GreetingUnit(MinionIdentity minion, Reactable reactable)
		{
			this.minion = minion;
			this.reactable = reactable;
		}

		// Token: 0x040062B0 RID: 25264
		public MinionIdentity minion;

		// Token: 0x040062B1 RID: 25265
		public Reactable reactable;
	}

	// Token: 0x0200142D RID: 5165
	private class GreetingSetup
	{
		// Token: 0x040062B2 RID: 25266
		public int cell;

		// Token: 0x040062B3 RID: 25267
		public DupeGreetingManager.GreetingUnit A;

		// Token: 0x040062B4 RID: 25268
		public DupeGreetingManager.GreetingUnit B;
	}
}
