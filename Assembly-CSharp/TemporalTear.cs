using System;
using System.Collections.Generic;
using KSerialization;

// Token: 0x02000967 RID: 2407
[SerializationConfig(MemberSerialization.OptIn)]
public class TemporalTear : ClusterGridEntity
{
	// Token: 0x17000554 RID: 1364
	// (get) Token: 0x06004754 RID: 18260 RVA: 0x00191C7F File Offset: 0x0018FE7F
	public override string Name
	{
		get
		{
			return Db.Get().SpaceDestinationTypes.Wormhole.typeName;
		}
	}

	// Token: 0x17000555 RID: 1365
	// (get) Token: 0x06004755 RID: 18261 RVA: 0x00191C95 File Offset: 0x0018FE95
	public override EntityLayer Layer
	{
		get
		{
			return EntityLayer.POI;
		}
	}

	// Token: 0x17000556 RID: 1366
	// (get) Token: 0x06004756 RID: 18262 RVA: 0x00191C98 File Offset: 0x0018FE98
	public override List<ClusterGridEntity.AnimConfig> AnimConfigs
	{
		get
		{
			return new List<ClusterGridEntity.AnimConfig>
			{
				new ClusterGridEntity.AnimConfig
				{
					animFile = Assets.GetAnim("temporal_tear_kanim"),
					initialAnim = "closed_loop"
				}
			};
		}
	}

	// Token: 0x17000557 RID: 1367
	// (get) Token: 0x06004757 RID: 18263 RVA: 0x00191CDB File Offset: 0x0018FEDB
	public override bool IsVisible
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000558 RID: 1368
	// (get) Token: 0x06004758 RID: 18264 RVA: 0x00191CDE File Offset: 0x0018FEDE
	public override ClusterRevealLevel IsVisibleInFOW
	{
		get
		{
			return ClusterRevealLevel.Peeked;
		}
	}

	// Token: 0x06004759 RID: 18265 RVA: 0x00191CE1 File Offset: 0x0018FEE1
	protected override void OnSpawn()
	{
		base.OnSpawn();
		ClusterManager.Instance.GetComponent<ClusterPOIManager>().RegisterTemporalTear(this);
		this.UpdateStatus();
	}

	// Token: 0x0600475A RID: 18266 RVA: 0x00191D00 File Offset: 0x0018FF00
	public void UpdateStatus()
	{
		KSelectable component = base.GetComponent<KSelectable>();
		ClusterMapVisualizer clusterMapVisualizer = null;
		if (ClusterMapScreen.Instance != null)
		{
			clusterMapVisualizer = ClusterMapScreen.Instance.GetEntityVisAnim(this);
		}
		if (this.IsOpen())
		{
			if (clusterMapVisualizer != null)
			{
				clusterMapVisualizer.PlayAnim("open_loop", KAnim.PlayMode.Loop);
			}
			component.RemoveStatusItem(Db.Get().MiscStatusItems.TearClosed, false);
			component.AddStatusItem(Db.Get().MiscStatusItems.TearOpen, null);
			return;
		}
		if (clusterMapVisualizer != null)
		{
			clusterMapVisualizer.PlayAnim("closed_loop", KAnim.PlayMode.Loop);
		}
		component.RemoveStatusItem(Db.Get().MiscStatusItems.TearOpen, false);
		base.GetComponent<KSelectable>().AddStatusItem(Db.Get().MiscStatusItems.TearClosed, null);
	}

	// Token: 0x0600475B RID: 18267 RVA: 0x00191DC3 File Offset: 0x0018FFC3
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x0600475C RID: 18268 RVA: 0x00191DCC File Offset: 0x0018FFCC
	public void ConsumeCraft(Clustercraft craft)
	{
		if (this.m_open && craft.Location == base.Location && !craft.IsFlightInProgress())
		{
			for (int i = 0; i < Components.MinionIdentities.Count; i++)
			{
				MinionIdentity minionIdentity = Components.MinionIdentities[i];
				if (minionIdentity.GetMyWorldId() == craft.ModuleInterface.GetInteriorWorld().id)
				{
					Util.KDestroyGameObject(minionIdentity.gameObject);
				}
			}
			craft.DestroyCraftAndModules();
			this.m_hasConsumedCraft = true;
		}
	}

	// Token: 0x0600475D RID: 18269 RVA: 0x00191E4D File Offset: 0x0019004D
	public void Open()
	{
		this.m_open = true;
		this.UpdateStatus();
	}

	// Token: 0x0600475E RID: 18270 RVA: 0x00191E5C File Offset: 0x0019005C
	public bool IsOpen()
	{
		return this.m_open;
	}

	// Token: 0x0600475F RID: 18271 RVA: 0x00191E64 File Offset: 0x00190064
	public bool HasConsumedCraft()
	{
		return this.m_hasConsumedCraft;
	}

	// Token: 0x04002F2D RID: 12077
	[Serialize]
	private bool m_open;

	// Token: 0x04002F2E RID: 12078
	[Serialize]
	private bool m_hasConsumedCraft;
}
