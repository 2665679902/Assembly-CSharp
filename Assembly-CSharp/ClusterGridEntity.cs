using System;
using System.Collections.Generic;
using KSerialization;
using ProcGen;
using UnityEngine;

// Token: 0x0200096C RID: 2412
public abstract class ClusterGridEntity : KMonoBehaviour
{
	// Token: 0x1700055F RID: 1375
	// (get) Token: 0x0600479C RID: 18332
	public abstract string Name { get; }

	// Token: 0x17000560 RID: 1376
	// (get) Token: 0x0600479D RID: 18333
	public abstract EntityLayer Layer { get; }

	// Token: 0x17000561 RID: 1377
	// (get) Token: 0x0600479E RID: 18334
	public abstract List<ClusterGridEntity.AnimConfig> AnimConfigs { get; }

	// Token: 0x17000562 RID: 1378
	// (get) Token: 0x0600479F RID: 18335
	public abstract bool IsVisible { get; }

	// Token: 0x060047A0 RID: 18336 RVA: 0x00193168 File Offset: 0x00191368
	public virtual bool ShowName()
	{
		return false;
	}

	// Token: 0x060047A1 RID: 18337 RVA: 0x0019316B File Offset: 0x0019136B
	public virtual bool ShowProgressBar()
	{
		return false;
	}

	// Token: 0x060047A2 RID: 18338 RVA: 0x0019316E File Offset: 0x0019136E
	public virtual float GetProgress()
	{
		return 0f;
	}

	// Token: 0x060047A3 RID: 18339 RVA: 0x00193175 File Offset: 0x00191375
	public virtual bool SpaceOutInSameHex()
	{
		return false;
	}

	// Token: 0x060047A4 RID: 18340 RVA: 0x00193178 File Offset: 0x00191378
	public virtual bool ShowPath()
	{
		return true;
	}

	// Token: 0x17000563 RID: 1379
	// (get) Token: 0x060047A5 RID: 18341
	public abstract ClusterRevealLevel IsVisibleInFOW { get; }

	// Token: 0x17000564 RID: 1380
	// (get) Token: 0x060047A6 RID: 18342 RVA: 0x0019317B File Offset: 0x0019137B
	// (set) Token: 0x060047A7 RID: 18343 RVA: 0x00193184 File Offset: 0x00191384
	public AxialI Location
	{
		get
		{
			return this.m_location;
		}
		set
		{
			if (value != this.m_location)
			{
				AxialI location = this.m_location;
				this.m_location = value;
				if (base.gameObject.GetSMI<StateMachine.Instance>() == null)
				{
					this.positionDirty = true;
				}
				this.SendClusterLocationChangedEvent(location, this.m_location);
			}
		}
	}

	// Token: 0x060047A8 RID: 18344 RVA: 0x001931D0 File Offset: 0x001913D0
	protected override void OnSpawn()
	{
		ClusterGrid.Instance.RegisterEntity(this);
		if (this.m_selectable != null)
		{
			this.m_selectable.SetName(this.Name);
		}
		if (!this.isWorldEntity)
		{
			this.m_transform.SetLocalPosition(new Vector3(-1f, 0f, 0f));
		}
		if (ClusterMapScreen.Instance != null)
		{
			ClusterMapScreen.Instance.Trigger(1980521255, null);
		}
	}

	// Token: 0x060047A9 RID: 18345 RVA: 0x0019324C File Offset: 0x0019144C
	protected override void OnCleanUp()
	{
		ClusterGrid.Instance.UnregisterEntity(this);
	}

	// Token: 0x060047AA RID: 18346 RVA: 0x0019325C File Offset: 0x0019145C
	public virtual Sprite GetUISprite()
	{
		if (DlcManager.FeatureClusterSpaceEnabled())
		{
			List<ClusterGridEntity.AnimConfig> animConfigs = this.AnimConfigs;
			if (animConfigs.Count > 0)
			{
				return Def.GetUISpriteFromMultiObjectAnim(animConfigs[0].animFile, "ui", false, "");
			}
		}
		else
		{
			WorldContainer component = base.GetComponent<WorldContainer>();
			if (component != null)
			{
				ProcGen.World worldData = SettingsCache.worlds.GetWorldData(component.worldName);
				if (worldData == null)
				{
					return null;
				}
				return Assets.GetSprite(worldData.asteroidIcon);
			}
		}
		return null;
	}

	// Token: 0x060047AB RID: 18347 RVA: 0x001932D8 File Offset: 0x001914D8
	public void SendClusterLocationChangedEvent(AxialI oldLocation, AxialI newLocation)
	{
		ClusterLocationChangedEvent clusterLocationChangedEvent = new ClusterLocationChangedEvent
		{
			entity = this,
			oldLocation = oldLocation,
			newLocation = newLocation
		};
		base.Trigger(-1298331547, clusterLocationChangedEvent);
		Game.Instance.Trigger(-1298331547, clusterLocationChangedEvent);
		if (this.m_selectable != null && this.m_selectable.IsSelected)
		{
			DetailsScreen.Instance.Refresh(base.gameObject);
		}
	}

	// Token: 0x04002F49 RID: 12105
	[Serialize]
	protected AxialI m_location;

	// Token: 0x04002F4A RID: 12106
	public bool positionDirty;

	// Token: 0x04002F4B RID: 12107
	[MyCmpGet]
	protected KSelectable m_selectable;

	// Token: 0x04002F4C RID: 12108
	[MyCmpReq]
	private Transform m_transform;

	// Token: 0x04002F4D RID: 12109
	public bool isWorldEntity;

	// Token: 0x02001772 RID: 6002
	public struct AnimConfig
	{
		// Token: 0x04006D14 RID: 27924
		public KAnimFile animFile;

		// Token: 0x04006D15 RID: 27925
		public string initialAnim;

		// Token: 0x04006D16 RID: 27926
		public KAnim.PlayMode playMode;

		// Token: 0x04006D17 RID: 27927
		public string symbolSwapTarget;

		// Token: 0x04006D18 RID: 27928
		public string symbolSwapSymbol;

		// Token: 0x04006D19 RID: 27929
		public Vector3 animOffset;
	}
}
