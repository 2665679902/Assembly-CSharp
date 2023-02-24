using System;
using System.Collections;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x0200088F RID: 2191
[AddComponentMenu("KMonoBehaviour/scripts/BuddingTrunk")]
public class BuddingTrunk : KMonoBehaviour, ISim4000ms
{
	// Token: 0x17000460 RID: 1120
	// (get) Token: 0x06003EC5 RID: 16069 RVA: 0x0015EE48 File Offset: 0x0015D048
	public bool ExtraSeedAvailable
	{
		get
		{
			return this.hasExtraSeedAvailable;
		}
	}

	// Token: 0x06003EC6 RID: 16070 RVA: 0x0015EE50 File Offset: 0x0015D050
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.simRenderLoadBalance = true;
		this.growingBranchesStatusItem = new StatusItem("GROWINGBRANCHES", "MISC", "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, true, 129022, null);
		base.Subscribe<BuddingTrunk>(1119167081, BuddingTrunk.OnNewGameSpawnDelegate);
	}

	// Token: 0x06003EC7 RID: 16071 RVA: 0x0015EEA4 File Offset: 0x0015D0A4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<BuddingTrunk>(-216549700, BuddingTrunk.OnUprootedDelegate);
		base.Subscribe<BuddingTrunk>(-750750377, BuddingTrunk.OnDrownedDelegate);
		base.Subscribe<BuddingTrunk>(-266953818, BuddingTrunk.OnHarvestDesignationChangedDelegate);
	}

	// Token: 0x06003EC8 RID: 16072 RVA: 0x0015EEDF File Offset: 0x0015D0DF
	protected override void OnCleanUp()
	{
		if (this.newGameSpawnRoutine != null)
		{
			base.StopCoroutine(this.newGameSpawnRoutine);
		}
		base.OnCleanUp();
	}

	// Token: 0x06003EC9 RID: 16073 RVA: 0x0015EEFC File Offset: 0x0015D0FC
	private void OnNewGameSpawn(object data)
	{
		float num = 1f;
		if ((double)UnityEngine.Random.value < 0.1)
		{
			num = UnityEngine.Random.Range(0.75f, 0.99f);
		}
		else
		{
			this.newGameSpawnRoutine = base.StartCoroutine(this.NewGameSproutBudRoutine());
		}
		this.growing.OverrideMaturityLevel(num);
	}

	// Token: 0x06003ECA RID: 16074 RVA: 0x0015EF50 File Offset: 0x0015D150
	private IEnumerator NewGameSproutBudRoutine()
	{
		int num2;
		for (int i = 0; i < this.buds.Length; i = num2 + 1)
		{
			yield return SequenceUtil.WaitForEndOfFrame;
			float num = UnityEngine.Random.Range(0f, 1f);
			this.TrySpawnRandomBud(null, num);
			num2 = i;
		}
		this.newGameSpawnRoutine = null;
		yield return SequenceUtil.WaitForNextFrame;
		yield break;
	}

	// Token: 0x06003ECB RID: 16075 RVA: 0x0015EF60 File Offset: 0x0015D160
	public void Sim4000ms(float dt)
	{
		if (this.growing.IsGrown() && !this.wilting.IsWilting())
		{
			this.TrySpawnRandomBud(null, 0f);
			base.GetComponent<KSelectable>().AddStatusItem(this.growingBranchesStatusItem, null);
			return;
		}
		base.GetComponent<KSelectable>().RemoveStatusItem(this.growingBranchesStatusItem, false);
	}

	// Token: 0x06003ECC RID: 16076 RVA: 0x0015EFBA File Offset: 0x0015D1BA
	private void OnUprooted(object data = null)
	{
		this.YieldWood();
	}

	// Token: 0x06003ECD RID: 16077 RVA: 0x0015EFC4 File Offset: 0x0015D1C4
	private void YieldWood()
	{
		foreach (Ref<HarvestDesignatable> @ref in this.buds)
		{
			HarvestDesignatable harvestDesignatable = ((@ref != null) ? @ref.Get() : null);
			if (harvestDesignatable != null)
			{
				harvestDesignatable.Trigger(-216549700, null);
			}
		}
	}

	// Token: 0x06003ECE RID: 16078 RVA: 0x0015F00C File Offset: 0x0015D20C
	public float GetMaxBranchMaturity()
	{
		float num = 0f;
		this.GetMostMatureBranch(out num);
		return num;
	}

	// Token: 0x06003ECF RID: 16079 RVA: 0x0015F02C File Offset: 0x0015D22C
	public void ConsumeMass(float mass_to_consume)
	{
		float num;
		HarvestDesignatable mostMatureBranch = this.GetMostMatureBranch(out num);
		if (mostMatureBranch)
		{
			Growing component = mostMatureBranch.GetComponent<Growing>();
			if (component)
			{
				component.ConsumeMass(mass_to_consume);
			}
		}
	}

	// Token: 0x06003ED0 RID: 16080 RVA: 0x0015F060 File Offset: 0x0015D260
	private HarvestDesignatable GetMostMatureBranch(out float max_maturity)
	{
		max_maturity = 0f;
		HarvestDesignatable harvestDesignatable = null;
		foreach (Ref<HarvestDesignatable> @ref in this.buds)
		{
			HarvestDesignatable harvestDesignatable2 = ((@ref != null) ? @ref.Get() : null);
			if (harvestDesignatable2 != null)
			{
				AmountInstance amountInstance = Db.Get().Amounts.Maturity.Lookup(harvestDesignatable2);
				if (amountInstance != null)
				{
					float num = amountInstance.value / amountInstance.GetMax();
					if (num > max_maturity)
					{
						max_maturity = num;
						harvestDesignatable = harvestDesignatable2;
					}
				}
			}
		}
		return harvestDesignatable;
	}

	// Token: 0x06003ED1 RID: 16081 RVA: 0x0015F0E4 File Offset: 0x0015D2E4
	public void TrySpawnRandomBud(object data = null, float growth_percentage = 0f)
	{
		if (this.uprooted.IsUprooted)
		{
			return;
		}
		BuddingTrunk.spawn_choices.Clear();
		int num = 0;
		for (int i = 0; i < this.buds.Length; i++)
		{
			int num2 = Grid.PosToCell(this.GetBudPosition(i));
			if ((this.buds[i] == null || this.buds[i].Get() == null) && this.CanGrowInto(num2))
			{
				BuddingTrunk.spawn_choices.Add(i);
			}
			else if (this.buds[i] != null && this.buds[i].Get() != null)
			{
				num++;
			}
		}
		if (num >= this.maxBuds)
		{
			return;
		}
		BuddingTrunk.spawn_choices.Shuffle<int>();
		if (BuddingTrunk.spawn_choices.Count > 0)
		{
			int num3 = BuddingTrunk.spawn_choices[0];
			Vector3 budPosition = this.GetBudPosition(num3);
			GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(this.budPrefabID), budPosition);
			gameObject.SetActive(true);
			gameObject.GetComponent<Growing>().OverrideMaturityLevel(growth_percentage);
			gameObject.GetComponent<TreeBud>().SetTrunkPosition(this, num3);
			HarvestDesignatable component = gameObject.GetComponent<HarvestDesignatable>();
			this.buds[num3] = new Ref<HarvestDesignatable>(component);
			this.UpdateBudHarvestState(component);
			this.TryRollNewSeed();
		}
	}

	// Token: 0x06003ED2 RID: 16082 RVA: 0x0015F214 File Offset: 0x0015D414
	public void TryRollNewSeed()
	{
		if (!this.hasExtraSeedAvailable && UnityEngine.Random.Range(0, 100) < 5)
		{
			this.hasExtraSeedAvailable = true;
		}
	}

	// Token: 0x06003ED3 RID: 16083 RVA: 0x0015F230 File Offset: 0x0015D430
	public TreeBud GetBranchAtPosition(int idx)
	{
		if (this.buds[idx] == null)
		{
			return null;
		}
		HarvestDesignatable harvestDesignatable = this.buds[idx].Get();
		if (!(harvestDesignatable != null))
		{
			return null;
		}
		return harvestDesignatable.GetComponent<TreeBud>();
	}

	// Token: 0x06003ED4 RID: 16084 RVA: 0x0015F268 File Offset: 0x0015D468
	public void ExtractExtraSeed()
	{
		if (!this.hasExtraSeedAvailable)
		{
			return;
		}
		this.hasExtraSeedAvailable = false;
		Vector3 position = base.transform.position;
		position.z = Grid.GetLayerZ(Grid.SceneLayer.Ore);
		Util.KInstantiate(Assets.GetPrefab("ForestTreeSeed"), position).SetActive(true);
	}

	// Token: 0x06003ED5 RID: 16085 RVA: 0x0015F2BC File Offset: 0x0015D4BC
	private void UpdateBudHarvestState(HarvestDesignatable bud)
	{
		HarvestDesignatable component = base.GetComponent<HarvestDesignatable>();
		bud.SetHarvestWhenReady(component.HarvestWhenReady);
	}

	// Token: 0x06003ED6 RID: 16086 RVA: 0x0015F2DC File Offset: 0x0015D4DC
	public void OnBranchRemoved(int idx, TreeBud treeBud)
	{
		if (idx < 0 || idx >= this.buds.Length)
		{
			global::Debug.Assert(false, "invalid branch index " + idx.ToString());
		}
		HarvestDesignatable component = treeBud.GetComponent<HarvestDesignatable>();
		HarvestDesignatable harvestDesignatable = ((this.buds[idx] != null) ? this.buds[idx].Get() : null);
		if (component != harvestDesignatable)
		{
			global::Debug.LogWarningFormat(base.gameObject, "OnBranchRemoved branch {0} does not match known branch {1}", new object[] { component, harvestDesignatable });
		}
		this.buds[idx] = null;
	}

	// Token: 0x06003ED7 RID: 16087 RVA: 0x0015F364 File Offset: 0x0015D564
	private void UpdateAllBudsHarvestStatus(object data = null)
	{
		foreach (Ref<HarvestDesignatable> @ref in this.buds)
		{
			if (@ref != null)
			{
				HarvestDesignatable harvestDesignatable = @ref.Get();
				if (harvestDesignatable == null)
				{
					global::Debug.LogWarning("harvest_designatable was null");
				}
				else
				{
					this.UpdateBudHarvestState(harvestDesignatable);
				}
			}
		}
	}

	// Token: 0x06003ED8 RID: 16088 RVA: 0x0015F3B0 File Offset: 0x0015D5B0
	public bool CanGrowInto(int cell)
	{
		if (!Grid.IsValidCell(cell))
		{
			return false;
		}
		if (Grid.Solid[cell])
		{
			return false;
		}
		int num = Grid.CellAbove(cell);
		return Grid.IsValidCell(num) && !Grid.IsSubstantialLiquid(num, 0.35f) && !(Grid.Objects[cell, 1] != null) && !(Grid.Objects[cell, 5] != null) && !Grid.Foundation[cell];
	}

	// Token: 0x06003ED9 RID: 16089 RVA: 0x0015F434 File Offset: 0x0015D634
	private Vector3 GetBudPosition(int idx)
	{
		Vector3 vector = base.transform.position;
		switch (idx)
		{
		case 0:
			vector = base.transform.position + Vector3.left;
			break;
		case 1:
			vector = base.transform.position + Vector3.left + Vector3.up;
			break;
		case 2:
			vector = base.transform.position + Vector3.left + Vector3.up + Vector3.up;
			break;
		case 3:
			vector = base.transform.position + Vector3.up + Vector3.up;
			break;
		case 4:
			vector = base.transform.position + Vector3.right + Vector3.up + Vector3.up;
			break;
		case 5:
			vector = base.transform.position + Vector3.right + Vector3.up;
			break;
		case 6:
			vector = base.transform.position + Vector3.right;
			break;
		}
		return vector;
	}

	// Token: 0x04002918 RID: 10520
	[MyCmpReq]
	private Growing growing;

	// Token: 0x04002919 RID: 10521
	[MyCmpReq]
	private WiltCondition wilting;

	// Token: 0x0400291A RID: 10522
	[MyCmpReq]
	private UprootedMonitor uprooted;

	// Token: 0x0400291B RID: 10523
	public string budPrefabID;

	// Token: 0x0400291C RID: 10524
	public int maxBuds = 5;

	// Token: 0x0400291D RID: 10525
	[Serialize]
	private Ref<HarvestDesignatable>[] buds = new Ref<HarvestDesignatable>[7];

	// Token: 0x0400291E RID: 10526
	private StatusItem growingBranchesStatusItem;

	// Token: 0x0400291F RID: 10527
	[Serialize]
	private bool hasExtraSeedAvailable;

	// Token: 0x04002920 RID: 10528
	private static readonly EventSystem.IntraObjectHandler<BuddingTrunk> OnNewGameSpawnDelegate = new EventSystem.IntraObjectHandler<BuddingTrunk>(delegate(BuddingTrunk component, object data)
	{
		component.OnNewGameSpawn(data);
	});

	// Token: 0x04002921 RID: 10529
	private Coroutine newGameSpawnRoutine;

	// Token: 0x04002922 RID: 10530
	private static readonly EventSystem.IntraObjectHandler<BuddingTrunk> OnUprootedDelegate = new EventSystem.IntraObjectHandler<BuddingTrunk>(delegate(BuddingTrunk component, object data)
	{
		component.OnUprooted(data);
	});

	// Token: 0x04002923 RID: 10531
	private static readonly EventSystem.IntraObjectHandler<BuddingTrunk> OnDrownedDelegate = new EventSystem.IntraObjectHandler<BuddingTrunk>(delegate(BuddingTrunk component, object data)
	{
		component.OnUprooted(data);
	});

	// Token: 0x04002924 RID: 10532
	private static readonly EventSystem.IntraObjectHandler<BuddingTrunk> OnHarvestDesignationChangedDelegate = new EventSystem.IntraObjectHandler<BuddingTrunk>(delegate(BuddingTrunk component, object data)
	{
		component.UpdateAllBudsHarvestStatus(data);
	});

	// Token: 0x04002925 RID: 10533
	private static List<int> spawn_choices = new List<int>();
}
