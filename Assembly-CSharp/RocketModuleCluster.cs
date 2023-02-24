using System;
using UnityEngine;

// Token: 0x0200095F RID: 2399
public class RocketModuleCluster : RocketModule
{
	// Token: 0x17000547 RID: 1351
	// (get) Token: 0x060046EB RID: 18155 RVA: 0x0018F645 File Offset: 0x0018D845
	// (set) Token: 0x060046EC RID: 18156 RVA: 0x0018F64D File Offset: 0x0018D84D
	public CraftModuleInterface CraftInterface
	{
		get
		{
			return this._craftInterface;
		}
		set
		{
			this._craftInterface = value;
			if (this._craftInterface != null)
			{
				base.name = base.name + ": " + this.GetParentRocketName();
			}
		}
	}

	// Token: 0x060046ED RID: 18157 RVA: 0x0018F680 File Offset: 0x0018D880
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<RocketModuleCluster>(2121280625, RocketModuleCluster.OnNewConstructionDelegate);
	}

	// Token: 0x060046EE RID: 18158 RVA: 0x0018F69C File Offset: 0x0018D89C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.CraftInterface == null && DlcManager.FeatureClusterSpaceEnabled())
		{
			this.RegisterWithCraftModuleInterface();
		}
		if (base.GetComponent<RocketEngine>() == null && base.GetComponent<RocketEngineCluster>() == null && base.GetComponent<BuildingUnderConstruction>() == null)
		{
			base.Subscribe<RocketModuleCluster>(1655598572, RocketModuleCluster.OnLaunchConditionChangedDelegate);
			base.Subscribe<RocketModuleCluster>(-887025858, RocketModuleCluster.OnLandDelegate);
		}
	}

	// Token: 0x060046EF RID: 18159 RVA: 0x0018F718 File Offset: 0x0018D918
	protected void OnNewConstruction(object data)
	{
		Constructable constructable = (Constructable)data;
		if (constructable == null)
		{
			return;
		}
		RocketModuleCluster component = constructable.GetComponent<RocketModuleCluster>();
		if (component == null)
		{
			return;
		}
		if (component.CraftInterface != null)
		{
			component.CraftInterface.AddModule(this);
		}
	}

	// Token: 0x060046F0 RID: 18160 RVA: 0x0018F764 File Offset: 0x0018D964
	private void RegisterWithCraftModuleInterface()
	{
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(base.GetComponent<AttachableBuilding>()))
		{
			if (!(gameObject == base.gameObject))
			{
				RocketModuleCluster component = gameObject.GetComponent<RocketModuleCluster>();
				if (component != null)
				{
					component.CraftInterface.AddModule(this);
					break;
				}
			}
		}
	}

	// Token: 0x060046F1 RID: 18161 RVA: 0x0018F7E4 File Offset: 0x0018D9E4
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		this.CraftInterface.RemoveModule(this);
	}

	// Token: 0x060046F2 RID: 18162 RVA: 0x0018F7F8 File Offset: 0x0018D9F8
	public override LaunchConditionManager FindLaunchConditionManager()
	{
		return this.CraftInterface.FindLaunchConditionManager();
	}

	// Token: 0x060046F3 RID: 18163 RVA: 0x0018F805 File Offset: 0x0018DA05
	public override string GetParentRocketName()
	{
		if (this.CraftInterface != null)
		{
			return this.CraftInterface.GetComponent<Clustercraft>().Name;
		}
		return this.parentRocketName;
	}

	// Token: 0x060046F4 RID: 18164 RVA: 0x0018F82C File Offset: 0x0018DA2C
	private void OnLaunchConditionChanged(object data)
	{
		this.UpdateAnimations();
	}

	// Token: 0x060046F5 RID: 18165 RVA: 0x0018F834 File Offset: 0x0018DA34
	private void OnLand(object data)
	{
		this.UpdateAnimations();
	}

	// Token: 0x060046F6 RID: 18166 RVA: 0x0018F83C File Offset: 0x0018DA3C
	protected void UpdateAnimations()
	{
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		Clustercraft clustercraft = ((this.CraftInterface == null) ? null : this.CraftInterface.GetComponent<Clustercraft>());
		if (clustercraft != null && clustercraft.Status == Clustercraft.CraftStatus.Launching && component.HasAnimation("launch"))
		{
			component.ClearQueue();
			if (component.HasAnimation("launch_pre"))
			{
				component.Play("launch_pre", KAnim.PlayMode.Once, 1f, 0f);
			}
			component.Queue("launch", KAnim.PlayMode.Loop, 1f, 0f);
			return;
		}
		if (this.CraftInterface != null && this.CraftInterface.CheckPreppedForLaunch())
		{
			component.initialAnim = "ready_to_launch";
			component.Play("pre_ready_to_launch", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue("ready_to_launch", KAnim.PlayMode.Loop, 1f, 0f);
			return;
		}
		component.initialAnim = "grounded";
		component.Play("pst_ready_to_launch", KAnim.PlayMode.Once, 1f, 0f);
		component.Queue("grounded", KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x04002F00 RID: 12032
	public RocketModulePerformance performanceStats;

	// Token: 0x04002F01 RID: 12033
	private static readonly EventSystem.IntraObjectHandler<RocketModuleCluster> OnNewConstructionDelegate = new EventSystem.IntraObjectHandler<RocketModuleCluster>(delegate(RocketModuleCluster component, object data)
	{
		component.OnNewConstruction(data);
	});

	// Token: 0x04002F02 RID: 12034
	private static readonly EventSystem.IntraObjectHandler<RocketModuleCluster> OnLaunchConditionChangedDelegate = new EventSystem.IntraObjectHandler<RocketModuleCluster>(delegate(RocketModuleCluster component, object data)
	{
		component.OnLaunchConditionChanged(data);
	});

	// Token: 0x04002F03 RID: 12035
	private static readonly EventSystem.IntraObjectHandler<RocketModuleCluster> OnLandDelegate = new EventSystem.IntraObjectHandler<RocketModuleCluster>(delegate(RocketModuleCluster component, object data)
	{
		component.OnLand(data);
	});

	// Token: 0x04002F04 RID: 12036
	private CraftModuleInterface _craftInterface;
}
