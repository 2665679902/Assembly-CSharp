using System;
using System.Collections;
using System.Collections.Generic;
using KSerialization;

// Token: 0x02000969 RID: 2409
public class AsteroidGridEntity : ClusterGridEntity
{
	// Token: 0x0600476D RID: 18285 RVA: 0x0019225A File Offset: 0x0019045A
	public override bool ShowName()
	{
		return true;
	}

	// Token: 0x1700055A RID: 1370
	// (get) Token: 0x0600476E RID: 18286 RVA: 0x0019225D File Offset: 0x0019045D
	public override string Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x1700055B RID: 1371
	// (get) Token: 0x0600476F RID: 18287 RVA: 0x00192265 File Offset: 0x00190465
	public override EntityLayer Layer
	{
		get
		{
			return EntityLayer.Asteroid;
		}
	}

	// Token: 0x1700055C RID: 1372
	// (get) Token: 0x06004770 RID: 18288 RVA: 0x00192268 File Offset: 0x00190468
	public override List<ClusterGridEntity.AnimConfig> AnimConfigs
	{
		get
		{
			List<ClusterGridEntity.AnimConfig> list = new List<ClusterGridEntity.AnimConfig>();
			ClusterGridEntity.AnimConfig animConfig = new ClusterGridEntity.AnimConfig
			{
				animFile = Assets.GetAnim(this.m_asteroidAnim.IsNullOrWhiteSpace() ? AsteroidGridEntity.DEFAULT_ASTEROID_ICON_ANIM : this.m_asteroidAnim),
				initialAnim = "idle_loop"
			};
			list.Add(animConfig);
			animConfig = new ClusterGridEntity.AnimConfig
			{
				animFile = Assets.GetAnim("orbit_kanim"),
				initialAnim = "orbit"
			};
			list.Add(animConfig);
			return list;
		}
	}

	// Token: 0x1700055D RID: 1373
	// (get) Token: 0x06004771 RID: 18289 RVA: 0x001922F1 File Offset: 0x001904F1
	public override bool IsVisible
	{
		get
		{
			return true;
		}
	}

	// Token: 0x1700055E RID: 1374
	// (get) Token: 0x06004772 RID: 18290 RVA: 0x001922F4 File Offset: 0x001904F4
	public override ClusterRevealLevel IsVisibleInFOW
	{
		get
		{
			return ClusterRevealLevel.Peeked;
		}
	}

	// Token: 0x06004773 RID: 18291 RVA: 0x001922F7 File Offset: 0x001904F7
	public void Init(string name, AxialI location, string asteroidTypeId)
	{
		this.m_name = name;
		this.m_location = location;
		this.m_asteroidAnim = asteroidTypeId;
	}

	// Token: 0x06004774 RID: 18292 RVA: 0x00192310 File Offset: 0x00190510
	protected override void OnSpawn()
	{
		Game.Instance.Subscribe(-1298331547, new Action<object>(this.OnClusterLocationChanged));
		Game.Instance.Subscribe(-1991583975, new Action<object>(this.OnFogOfWarRevealed));
		if (ClusterGrid.Instance.IsCellVisible(this.m_location))
		{
			SaveGame.Instance.GetSMI<ClusterFogOfWarManager.Instance>().RevealLocation(this.m_location, 1);
		}
		base.OnSpawn();
	}

	// Token: 0x06004775 RID: 18293 RVA: 0x00192383 File Offset: 0x00190583
	protected override void OnCleanUp()
	{
		Game.Instance.Unsubscribe(-1298331547, new Action<object>(this.OnClusterLocationChanged));
		Game.Instance.Unsubscribe(-1991583975, new Action<object>(this.OnFogOfWarRevealed));
		base.OnCleanUp();
	}

	// Token: 0x06004776 RID: 18294 RVA: 0x001923C4 File Offset: 0x001905C4
	public void OnClusterLocationChanged(object data)
	{
		if (this.m_worldContainer.IsDiscovered)
		{
			return;
		}
		if (!ClusterGrid.Instance.IsCellVisible(base.Location))
		{
			return;
		}
		Clustercraft component = ((ClusterLocationChangedEvent)data).entity.GetComponent<Clustercraft>();
		if (component == null)
		{
			return;
		}
		if (component.GetOrbitAsteroid() == this)
		{
			this.m_worldContainer.SetDiscovered(true);
		}
	}

	// Token: 0x06004777 RID: 18295 RVA: 0x00192428 File Offset: 0x00190628
	public void OnFogOfWarRevealed(object data = null)
	{
		if (data == null)
		{
			return;
		}
		if ((AxialI)data != this.m_location)
		{
			return;
		}
		if (!ClusterGrid.Instance.IsCellVisible(base.Location))
		{
			return;
		}
		WorldDetectedMessage worldDetectedMessage = new WorldDetectedMessage(this.m_worldContainer);
		MusicManager.instance.PlaySong("Stinger_WorldDetected", false);
		Messenger.Instance.QueueMessage(worldDetectedMessage);
		if (!this.m_worldContainer.IsDiscovered)
		{
			using (IEnumerator enumerator = Components.Clustercrafts.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((Clustercraft)enumerator.Current).GetOrbitAsteroid() == this)
					{
						this.m_worldContainer.SetDiscovered(true);
						break;
					}
				}
			}
		}
	}

	// Token: 0x04002F37 RID: 12087
	public static string DEFAULT_ASTEROID_ICON_ANIM = "asteroid_sandstone_start_kanim";

	// Token: 0x04002F38 RID: 12088
	[MyCmpReq]
	private WorldContainer m_worldContainer;

	// Token: 0x04002F39 RID: 12089
	[Serialize]
	private string m_name;

	// Token: 0x04002F3A RID: 12090
	[Serialize]
	private string m_asteroidAnim;
}
