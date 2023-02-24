using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200093E RID: 2366
public class ClusterTraveler : KMonoBehaviour, ISim200ms
{
	// Token: 0x170004FC RID: 1276
	// (get) Token: 0x06004550 RID: 17744 RVA: 0x00186A7C File Offset: 0x00184C7C
	public List<AxialI> CurrentPath
	{
		get
		{
			if (this.m_cachedPath == null || this.m_destinationSelector.GetDestination() != this.m_cachedPathDestination)
			{
				this.m_cachedPathDestination = this.m_destinationSelector.GetDestination();
				this.m_cachedPath = ClusterGrid.Instance.GetPath(this.m_clusterGridEntity.Location, this.m_cachedPathDestination, this.m_destinationSelector);
			}
			return this.m_cachedPath;
		}
	}

	// Token: 0x06004551 RID: 17745 RVA: 0x00186AE7 File Offset: 0x00184CE7
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Components.ClusterTravelers.Add(this);
	}

	// Token: 0x06004552 RID: 17746 RVA: 0x00186AFA File Offset: 0x00184CFA
	protected override void OnCleanUp()
	{
		Components.ClusterTravelers.Remove(this);
		Game.Instance.Unsubscribe(-1991583975, new Action<object>(this.OnClusterFogOfWarRevealed));
		base.OnCleanUp();
	}

	// Token: 0x06004553 RID: 17747 RVA: 0x00186B28 File Offset: 0x00184D28
	private void ForceRevealLocation(AxialI location)
	{
		if (!ClusterGrid.Instance.IsCellVisible(location))
		{
			SaveGame.Instance.GetSMI<ClusterFogOfWarManager.Instance>().RevealLocation(location, 0);
		}
	}

	// Token: 0x06004554 RID: 17748 RVA: 0x00186B48 File Offset: 0x00184D48
	protected override void OnSpawn()
	{
		base.Subscribe<ClusterTraveler>(543433792, ClusterTraveler.ClusterDestinationChangedHandler);
		Game.Instance.Subscribe(-1991583975, new Action<object>(this.OnClusterFogOfWarRevealed));
		this.UpdateAnimationTags();
		this.MarkPathDirty();
		this.RevalidatePath(false);
		this.ForceRevealLocation(this.m_clusterGridEntity.Location);
	}

	// Token: 0x06004555 RID: 17749 RVA: 0x00186BA6 File Offset: 0x00184DA6
	private void MarkPathDirty()
	{
		this.m_isPathDirty = true;
	}

	// Token: 0x06004556 RID: 17750 RVA: 0x00186BAF File Offset: 0x00184DAF
	private void OnClusterFogOfWarRevealed(object data)
	{
		this.MarkPathDirty();
	}

	// Token: 0x06004557 RID: 17751 RVA: 0x00186BB7 File Offset: 0x00184DB7
	private void OnClusterDestinationChanged(object data)
	{
		if (this.m_destinationSelector.IsAtDestination())
		{
			this.m_movePotential = 0f;
			if (this.CurrentPath != null)
			{
				this.CurrentPath.Clear();
			}
		}
		this.MarkPathDirty();
	}

	// Token: 0x06004558 RID: 17752 RVA: 0x00186BEA File Offset: 0x00184DEA
	public int GetDestinationWorldID()
	{
		return this.m_destinationSelector.GetDestinationWorld();
	}

	// Token: 0x06004559 RID: 17753 RVA: 0x00186BF7 File Offset: 0x00184DF7
	public float TravelETA()
	{
		if (!this.IsTraveling() || this.getSpeedCB == null)
		{
			return 0f;
		}
		return this.RemainingTravelDistance() / this.getSpeedCB();
	}

	// Token: 0x0600455A RID: 17754 RVA: 0x00186C24 File Offset: 0x00184E24
	public float RemainingTravelDistance()
	{
		int num = this.RemainingTravelNodes();
		if (this.GetDestinationWorldID() >= 0)
		{
			num--;
			num = Mathf.Max(num, 0);
		}
		return (float)num * 600f - this.m_movePotential;
	}

	// Token: 0x0600455B RID: 17755 RVA: 0x00186C5C File Offset: 0x00184E5C
	public int RemainingTravelNodes()
	{
		int count = this.CurrentPath.Count;
		return Mathf.Max(0, count);
	}

	// Token: 0x0600455C RID: 17756 RVA: 0x00186C7C File Offset: 0x00184E7C
	public float GetMoveProgress()
	{
		return this.m_movePotential / 600f;
	}

	// Token: 0x0600455D RID: 17757 RVA: 0x00186C8A File Offset: 0x00184E8A
	public bool IsTraveling()
	{
		return !this.m_destinationSelector.IsAtDestination();
	}

	// Token: 0x0600455E RID: 17758 RVA: 0x00186C9C File Offset: 0x00184E9C
	public void Sim200ms(float dt)
	{
		if (!this.IsTraveling())
		{
			return;
		}
		bool flag = this.CurrentPath != null && this.CurrentPath.Count > 0;
		bool flag2 = this.m_destinationSelector.HasAsteroidDestination();
		bool flag3 = flag2 && flag && this.CurrentPath.Count == 1;
		if (this.getCanTravelCB != null && !this.getCanTravelCB(flag3))
		{
			return;
		}
		AxialI location = this.m_clusterGridEntity.Location;
		if (flag)
		{
			if (flag2)
			{
				bool requireLaunchPadOnAsteroidDestination = this.m_destinationSelector.requireLaunchPadOnAsteroidDestination;
			}
			if (!flag2 || this.CurrentPath.Count > 1)
			{
				float num = dt * this.getSpeedCB();
				this.m_movePotential += num;
				if (this.m_movePotential >= 600f)
				{
					this.m_movePotential = 600f;
					if (this.AdvancePathOneStep())
					{
						global::Debug.Assert(ClusterGrid.Instance.GetVisibleEntityOfLayerAtCell(this.m_clusterGridEntity.Location, EntityLayer.Asteroid) == null, string.Format("Somehow this clustercraft pathed through an asteroid at {0}", this.m_clusterGridEntity.Location));
						this.m_movePotential -= 600f;
						if (this.onTravelCB != null)
						{
							this.onTravelCB();
						}
					}
				}
			}
			else
			{
				this.AdvancePathOneStep();
			}
		}
		this.RevalidatePath(true);
	}

	// Token: 0x0600455F RID: 17759 RVA: 0x00186DEC File Offset: 0x00184FEC
	public bool AdvancePathOneStep()
	{
		if (this.validateTravelCB != null && !this.validateTravelCB(this.CurrentPath[0]))
		{
			return false;
		}
		AxialI axialI = this.CurrentPath[0];
		this.CurrentPath.RemoveAt(0);
		this.ForceRevealLocation(axialI);
		this.m_clusterGridEntity.Location = axialI;
		this.UpdateAnimationTags();
		return true;
	}

	// Token: 0x06004560 RID: 17760 RVA: 0x00186E50 File Offset: 0x00185050
	private void UpdateAnimationTags()
	{
		if (this.CurrentPath == null)
		{
			this.m_clusterGridEntity.RemoveTag(GameTags.BallisticEntityLaunching);
			this.m_clusterGridEntity.RemoveTag(GameTags.BallisticEntityLanding);
			this.m_clusterGridEntity.RemoveTag(GameTags.BallisticEntityMoving);
			return;
		}
		if (!(ClusterGrid.Instance.GetAsteroidAtCell(this.m_clusterGridEntity.Location) != null))
		{
			this.m_clusterGridEntity.AddTag(GameTags.BallisticEntityMoving);
			this.m_clusterGridEntity.RemoveTag(GameTags.BallisticEntityLanding);
			this.m_clusterGridEntity.RemoveTag(GameTags.BallisticEntityLaunching);
			return;
		}
		if (this.CurrentPath.Count == 0 || this.m_clusterGridEntity.Location == this.CurrentPath[this.CurrentPath.Count - 1])
		{
			this.m_clusterGridEntity.AddTag(GameTags.BallisticEntityLanding);
			this.m_clusterGridEntity.RemoveTag(GameTags.BallisticEntityLaunching);
			this.m_clusterGridEntity.RemoveTag(GameTags.BallisticEntityMoving);
			return;
		}
		this.m_clusterGridEntity.AddTag(GameTags.BallisticEntityLaunching);
		this.m_clusterGridEntity.RemoveTag(GameTags.BallisticEntityLanding);
		this.m_clusterGridEntity.RemoveTag(GameTags.BallisticEntityMoving);
	}

	// Token: 0x06004561 RID: 17761 RVA: 0x00186F80 File Offset: 0x00185180
	public void RevalidatePath(bool react_to_change = true)
	{
		string reason;
		List<AxialI> list;
		if (this.HasCurrentPathChanged(out reason, out list))
		{
			if (this.stopAndNotifyWhenPathChanges && react_to_change)
			{
				this.m_destinationSelector.SetDestination(this.m_destinationSelector.GetMyWorldLocation());
				string message = MISC.NOTIFICATIONS.BADROCKETPATH.TOOLTIP;
				Notification notification = new Notification(MISC.NOTIFICATIONS.BADROCKETPATH.NAME, NotificationType.BadMinor, (List<Notification> notificationList, object data) => message + notificationList.ReduceMessages(false) + "\n\n" + reason, null, true, 0f, null, null, null, true, false, false);
				base.GetComponent<Notifier>().Add(notification, "");
				return;
			}
			this.m_cachedPath = list;
		}
	}

	// Token: 0x06004562 RID: 17762 RVA: 0x00187018 File Offset: 0x00185218
	private bool HasCurrentPathChanged(out string reason, out List<AxialI> updatedPath)
	{
		if (!this.m_isPathDirty)
		{
			reason = null;
			updatedPath = null;
			return false;
		}
		this.m_isPathDirty = false;
		updatedPath = ClusterGrid.Instance.GetPath(this.m_clusterGridEntity.Location, this.m_cachedPathDestination, this.m_destinationSelector, out reason);
		if (updatedPath == null)
		{
			return true;
		}
		if (updatedPath.Count != this.m_cachedPath.Count)
		{
			return true;
		}
		for (int i = 0; i < this.m_cachedPath.Count; i++)
		{
			if (this.m_cachedPath[i] != updatedPath[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004563 RID: 17763 RVA: 0x001870B0 File Offset: 0x001852B0
	[ContextMenu("Fill Move Potential")]
	public void FillMovePotential()
	{
		this.m_movePotential = 600f;
	}

	// Token: 0x04002E38 RID: 11832
	[MyCmpReq]
	private ClusterDestinationSelector m_destinationSelector;

	// Token: 0x04002E39 RID: 11833
	[MyCmpReq]
	private ClusterGridEntity m_clusterGridEntity;

	// Token: 0x04002E3A RID: 11834
	[Serialize]
	private float m_movePotential;

	// Token: 0x04002E3B RID: 11835
	public Func<float> getSpeedCB;

	// Token: 0x04002E3C RID: 11836
	public Func<bool, bool> getCanTravelCB;

	// Token: 0x04002E3D RID: 11837
	public Func<AxialI, bool> validateTravelCB;

	// Token: 0x04002E3E RID: 11838
	public System.Action onTravelCB;

	// Token: 0x04002E3F RID: 11839
	private AxialI m_cachedPathDestination;

	// Token: 0x04002E40 RID: 11840
	private List<AxialI> m_cachedPath;

	// Token: 0x04002E41 RID: 11841
	private bool m_isPathDirty;

	// Token: 0x04002E42 RID: 11842
	public bool stopAndNotifyWhenPathChanges;

	// Token: 0x04002E43 RID: 11843
	private static EventSystem.IntraObjectHandler<ClusterTraveler> ClusterDestinationChangedHandler = new EventSystem.IntraObjectHandler<ClusterTraveler>(delegate(ClusterTraveler cmp, object data)
	{
		cmp.OnClusterDestinationChanged(data);
	});
}
