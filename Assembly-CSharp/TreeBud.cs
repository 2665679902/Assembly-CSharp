using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200089C RID: 2204
[AddComponentMenu("KMonoBehaviour/scripts/TreeBud")]
public class TreeBud : KMonoBehaviour, IWiltCause
{
	// Token: 0x06003F2A RID: 16170 RVA: 0x00160C98 File Offset: 0x0015EE98
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.simRenderLoadBalance = true;
		int num = Grid.PosToCell(base.gameObject);
		GameObject gameObject = Grid.Objects[num, 5];
		if (gameObject != null && gameObject != base.gameObject)
		{
			Util.KDestroyGameObject(base.gameObject);
		}
		else
		{
			this.SetOccupyGridSpace(true);
		}
		base.Subscribe<TreeBud>(1272413801, TreeBud.OnHarvestDelegate);
	}

	// Token: 0x06003F2B RID: 16171 RVA: 0x00160D07 File Offset: 0x0015EF07
	private void OnHarvest(object data)
	{
		if (this.buddingTrunk.Get() != null)
		{
			this.buddingTrunk.Get().TryRollNewSeed();
		}
	}

	// Token: 0x06003F2C RID: 16172 RVA: 0x00160D2C File Offset: 0x0015EF2C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.buddingTrunk != null && this.buddingTrunk.Get() != null)
		{
			this.SubscribeToTrunk();
			this.UpdateAnimationSet();
			return;
		}
		global::Debug.LogWarning("TreeBud loaded with missing trunk reference. Destroying...");
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x06003F2D RID: 16173 RVA: 0x00160D7C File Offset: 0x0015EF7C
	protected override void OnCleanUp()
	{
		this.UnsubscribeToTrunk();
		this.SetOccupyGridSpace(false);
		base.OnCleanUp();
	}

	// Token: 0x06003F2E RID: 16174 RVA: 0x00160D94 File Offset: 0x0015EF94
	private void SetOccupyGridSpace(bool active)
	{
		int num = Grid.PosToCell(base.gameObject);
		if (active)
		{
			GameObject gameObject = Grid.Objects[num, 5];
			if (gameObject != null && gameObject != base.gameObject)
			{
				global::Debug.LogWarningFormat(base.gameObject, "TreeBud.SetOccupyGridSpace already occupied by {0}", new object[] { gameObject });
			}
			Grid.Objects[num, 5] = base.gameObject;
			return;
		}
		if (Grid.Objects[num, 5] == base.gameObject)
		{
			Grid.Objects[num, 5] = null;
		}
	}

	// Token: 0x06003F2F RID: 16175 RVA: 0x00160E28 File Offset: 0x0015F028
	private void SubscribeToTrunk()
	{
		if (this.trunkWiltHandle != -1 || this.trunkWiltRecoverHandle != -1)
		{
			return;
		}
		global::Debug.Assert(this.buddingTrunk != null, "buddingTrunk null");
		BuddingTrunk buddingTrunk = this.buddingTrunk.Get();
		global::Debug.Assert(buddingTrunk != null, "tree_trunk null");
		this.trunkWiltHandle = buddingTrunk.Subscribe(-724860998, new Action<object>(this.OnTrunkWilt));
		this.trunkWiltRecoverHandle = buddingTrunk.Subscribe(712767498, new Action<object>(this.OnTrunkRecover));
		base.Trigger(912965142, !buddingTrunk.GetComponent<WiltCondition>().IsWilting());
		ReceptacleMonitor component = base.GetComponent<ReceptacleMonitor>();
		PlantablePlot receptacle = buddingTrunk.GetComponent<ReceptacleMonitor>().GetReceptacle();
		component.SetReceptacle(receptacle);
		Vector3 position = base.gameObject.transform.position;
		position.z = Grid.GetLayerZ(Grid.SceneLayer.BuildingFront) - 0.1f * (float)this.trunkPosition;
		base.gameObject.transform.SetPosition(position);
		base.GetComponent<BudUprootedMonitor>().SetParentObject(buddingTrunk.GetComponent<KPrefabID>());
	}

	// Token: 0x06003F30 RID: 16176 RVA: 0x00160F38 File Offset: 0x0015F138
	private void UnsubscribeToTrunk()
	{
		if (this.buddingTrunk == null)
		{
			global::Debug.LogWarning("buddingTrunk null", base.gameObject);
			return;
		}
		BuddingTrunk buddingTrunk = this.buddingTrunk.Get();
		if (buddingTrunk == null)
		{
			global::Debug.LogWarning("tree_trunk null", base.gameObject);
			return;
		}
		buddingTrunk.Unsubscribe(this.trunkWiltHandle);
		buddingTrunk.Unsubscribe(this.trunkWiltRecoverHandle);
		buddingTrunk.OnBranchRemoved(this.trunkPosition, this);
	}

	// Token: 0x06003F31 RID: 16177 RVA: 0x00160FA9 File Offset: 0x0015F1A9
	public void SetTrunkPosition(BuddingTrunk budding_trunk, int idx)
	{
		this.buddingTrunk = new Ref<BuddingTrunk>(budding_trunk);
		this.trunkPosition = idx;
		this.SubscribeToTrunk();
		this.UpdateAnimationSet();
	}

	// Token: 0x06003F32 RID: 16178 RVA: 0x00160FCA File Offset: 0x0015F1CA
	private void OnTrunkWilt(object data = null)
	{
		base.Trigger(912965142, false);
	}

	// Token: 0x06003F33 RID: 16179 RVA: 0x00160FDD File Offset: 0x0015F1DD
	private void OnTrunkRecover(object data = null)
	{
		base.Trigger(912965142, true);
	}

	// Token: 0x06003F34 RID: 16180 RVA: 0x00160FF0 File Offset: 0x0015F1F0
	private void UpdateAnimationSet()
	{
		this.crop.anims = TreeBud.animSets[this.trunkPosition];
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		component.Offset = TreeBud.animOffset[this.trunkPosition];
		component.Play(this.crop.anims.grow, KAnim.PlayMode.Paused, 1f, 0f);
		this.crop.RefreshPositionPercent();
	}

	// Token: 0x17000462 RID: 1122
	// (get) Token: 0x06003F35 RID: 16181 RVA: 0x00161060 File Offset: 0x0015F260
	public string WiltStateString
	{
		get
		{
			return "    • " + DUPLICANTS.STATS.TRUNKHEALTH.NAME;
		}
	}

	// Token: 0x17000463 RID: 1123
	// (get) Token: 0x06003F36 RID: 16182 RVA: 0x00161076 File Offset: 0x0015F276
	public WiltCondition.Condition[] Conditions
	{
		get
		{
			return new WiltCondition.Condition[] { WiltCondition.Condition.UnhealthyRoot };
		}
	}

	// Token: 0x0400297A RID: 10618
	[MyCmpReq]
	private Growing growing;

	// Token: 0x0400297B RID: 10619
	[MyCmpReq]
	private StandardCropPlant crop;

	// Token: 0x0400297C RID: 10620
	[Serialize]
	public Ref<BuddingTrunk> buddingTrunk;

	// Token: 0x0400297D RID: 10621
	[Serialize]
	private int trunkPosition;

	// Token: 0x0400297E RID: 10622
	[Serialize]
	public int growingPos;

	// Token: 0x0400297F RID: 10623
	private int trunkWiltHandle = -1;

	// Token: 0x04002980 RID: 10624
	private int trunkWiltRecoverHandle = -1;

	// Token: 0x04002981 RID: 10625
	private static StandardCropPlant.AnimSet[] animSets = new StandardCropPlant.AnimSet[]
	{
		new StandardCropPlant.AnimSet
		{
			grow = "branch_a_grow",
			grow_pst = "branch_a_grow_pst",
			idle_full = "branch_a_idle_full",
			wilt_base = "branch_a_wilt",
			harvest = "branch_a_harvest"
		},
		new StandardCropPlant.AnimSet
		{
			grow = "branch_b_grow",
			grow_pst = "branch_b_grow_pst",
			idle_full = "branch_b_idle_full",
			wilt_base = "branch_b_wilt",
			harvest = "branch_b_harvest"
		},
		new StandardCropPlant.AnimSet
		{
			grow = "branch_c_grow",
			grow_pst = "branch_c_grow_pst",
			idle_full = "branch_c_idle_full",
			wilt_base = "branch_c_wilt",
			harvest = "branch_c_harvest"
		},
		new StandardCropPlant.AnimSet
		{
			grow = "branch_d_grow",
			grow_pst = "branch_d_grow_pst",
			idle_full = "branch_d_idle_full",
			wilt_base = "branch_d_wilt",
			harvest = "branch_d_harvest"
		},
		new StandardCropPlant.AnimSet
		{
			grow = "branch_e_grow",
			grow_pst = "branch_e_grow_pst",
			idle_full = "branch_e_idle_full",
			wilt_base = "branch_e_wilt",
			harvest = "branch_e_harvest"
		},
		new StandardCropPlant.AnimSet
		{
			grow = "branch_f_grow",
			grow_pst = "branch_f_grow_pst",
			idle_full = "branch_f_idle_full",
			wilt_base = "branch_f_wilt",
			harvest = "branch_f_harvest"
		},
		new StandardCropPlant.AnimSet
		{
			grow = "branch_g_grow",
			grow_pst = "branch_g_grow_pst",
			idle_full = "branch_g_idle_full",
			wilt_base = "branch_g_wilt",
			harvest = "branch_g_harvest"
		}
	};

	// Token: 0x04002982 RID: 10626
	private static Vector3[] animOffset = new Vector3[]
	{
		new Vector3(1f, 0f, 0f),
		new Vector3(1f, -1f, 0f),
		new Vector3(1f, -2f, 0f),
		new Vector3(0f, -2f, 0f),
		new Vector3(-1f, -2f, 0f),
		new Vector3(-1f, -1f, 0f),
		new Vector3(-1f, 0f, 0f)
	};

	// Token: 0x04002983 RID: 10627
	private static readonly EventSystem.IntraObjectHandler<TreeBud> OnHarvestDelegate = new EventSystem.IntraObjectHandler<TreeBud>(delegate(TreeBud component, object data)
	{
		component.OnHarvest(data);
	});
}
