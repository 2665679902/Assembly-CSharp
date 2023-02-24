using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using UnityEngine;

// Token: 0x020004B9 RID: 1209
[AddComponentMenu("KMonoBehaviour/scripts/Prioritizable")]
public class Prioritizable : KMonoBehaviour
{
	// Token: 0x06001BDB RID: 7131 RVA: 0x00093C33 File Offset: 0x00091E33
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<Prioritizable>(-905833192, Prioritizable.OnCopySettingsDelegate);
	}

	// Token: 0x06001BDC RID: 7132 RVA: 0x00093C4C File Offset: 0x00091E4C
	private void OnCopySettings(object data)
	{
		Prioritizable component = ((GameObject)data).GetComponent<Prioritizable>();
		if (component != null)
		{
			this.SetMasterPriority(component.GetMasterPriority());
		}
	}

	// Token: 0x06001BDD RID: 7133 RVA: 0x00093C7C File Offset: 0x00091E7C
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (this.masterPriority != -2147483648)
		{
			this.masterPrioritySetting = new PrioritySetting(PriorityScreen.PriorityClass.basic, 5);
			this.masterPriority = int.MinValue;
		}
		PrioritySetting prioritySetting;
		if (SaveLoader.Instance.GameInfo.IsVersionExactly(7, 2) && Prioritizable.conversions.TryGetValue(this.masterPrioritySetting, out prioritySetting))
		{
			this.masterPrioritySetting = prioritySetting;
		}
	}

	// Token: 0x06001BDE RID: 7134 RVA: 0x00093CE0 File Offset: 0x00091EE0
	protected override void OnSpawn()
	{
		if (this.onPriorityChanged != null)
		{
			this.onPriorityChanged(this.masterPrioritySetting);
		}
		this.RefreshHighPriorityNotification();
		this.RefreshTopPriorityOnWorld();
		Vector3 position = base.transform.GetPosition();
		Extents extents = new Extents((int)position.x, (int)position.y, 1, 1);
		this.scenePartitionerEntry = GameScenePartitioner.Instance.Add(base.name, this, extents, GameScenePartitioner.Instance.prioritizableObjects, null);
		Components.Prioritizables.Add(this);
	}

	// Token: 0x06001BDF RID: 7135 RVA: 0x00093D63 File Offset: 0x00091F63
	public PrioritySetting GetMasterPriority()
	{
		return this.masterPrioritySetting;
	}

	// Token: 0x06001BE0 RID: 7136 RVA: 0x00093D6C File Offset: 0x00091F6C
	public void SetMasterPriority(PrioritySetting priority)
	{
		if (!priority.Equals(this.masterPrioritySetting))
		{
			this.masterPrioritySetting = priority;
			if (this.onPriorityChanged != null)
			{
				this.onPriorityChanged(this.masterPrioritySetting);
			}
			this.RefreshTopPriorityOnWorld();
			this.RefreshHighPriorityNotification();
		}
	}

	// Token: 0x06001BE1 RID: 7137 RVA: 0x00093DBF File Offset: 0x00091FBF
	private void RefreshTopPriorityOnWorld()
	{
		this.SetTopPriorityOnWorld(this.IsTopPriority());
	}

	// Token: 0x06001BE2 RID: 7138 RVA: 0x00093DD0 File Offset: 0x00091FD0
	private void SetTopPriorityOnWorld(bool state)
	{
		WorldContainer myWorld = base.gameObject.GetMyWorld();
		if (Game.Instance == null || myWorld == null)
		{
			return;
		}
		if (state)
		{
			myWorld.AddTopPriorityPrioritizable(this);
			return;
		}
		myWorld.RemoveTopPriorityPrioritizable(this);
	}

	// Token: 0x06001BE3 RID: 7139 RVA: 0x00093E12 File Offset: 0x00092012
	public void AddRef()
	{
		this.refCount++;
		this.RefreshTopPriorityOnWorld();
		this.RefreshHighPriorityNotification();
	}

	// Token: 0x06001BE4 RID: 7140 RVA: 0x00093E2E File Offset: 0x0009202E
	public void RemoveRef()
	{
		this.refCount--;
		if (this.IsTopPriority() || this.refCount == 0)
		{
			this.SetTopPriorityOnWorld(false);
		}
		this.RefreshHighPriorityNotification();
	}

	// Token: 0x06001BE5 RID: 7141 RVA: 0x00093E5B File Offset: 0x0009205B
	public bool IsPrioritizable()
	{
		return this.refCount > 0;
	}

	// Token: 0x06001BE6 RID: 7142 RVA: 0x00093E66 File Offset: 0x00092066
	public bool IsTopPriority()
	{
		return this.masterPrioritySetting.priority_class == PriorityScreen.PriorityClass.topPriority && this.IsPrioritizable();
	}

	// Token: 0x06001BE7 RID: 7143 RVA: 0x00093E80 File Offset: 0x00092080
	protected override void OnCleanUp()
	{
		WorldContainer myWorld = base.gameObject.GetMyWorld();
		if (myWorld != null)
		{
			myWorld.RemoveTopPriorityPrioritizable(this);
		}
		else
		{
			global::Debug.LogWarning("World has been destroyed before prioritizable " + base.name);
			foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
			{
				worldContainer.RemoveTopPriorityPrioritizable(this);
			}
		}
		base.OnCleanUp();
		GameScenePartitioner.Instance.Free(ref this.scenePartitionerEntry);
		Components.Prioritizables.Remove(this);
	}

	// Token: 0x06001BE8 RID: 7144 RVA: 0x00093F24 File Offset: 0x00092124
	public static void AddRef(GameObject go)
	{
		Prioritizable component = go.GetComponent<Prioritizable>();
		if (component != null)
		{
			component.AddRef();
		}
	}

	// Token: 0x06001BE9 RID: 7145 RVA: 0x00093F48 File Offset: 0x00092148
	public static void RemoveRef(GameObject go)
	{
		Prioritizable component = go.GetComponent<Prioritizable>();
		if (component != null)
		{
			component.RemoveRef();
		}
	}

	// Token: 0x06001BEA RID: 7146 RVA: 0x00093F6C File Offset: 0x0009216C
	private void RefreshHighPriorityNotification()
	{
		bool flag = this.masterPrioritySetting.priority_class == PriorityScreen.PriorityClass.topPriority && this.IsPrioritizable();
		if (flag && this.highPriorityStatusItem == Guid.Empty)
		{
			this.highPriorityStatusItem = base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.EmergencyPriority, null);
			return;
		}
		if (!flag && this.highPriorityStatusItem != Guid.Empty)
		{
			this.highPriorityStatusItem = base.GetComponent<KSelectable>().RemoveStatusItem(this.highPriorityStatusItem, false);
		}
	}

	// Token: 0x04000F8C RID: 3980
	[SerializeField]
	[Serialize]
	private int masterPriority = int.MinValue;

	// Token: 0x04000F8D RID: 3981
	[SerializeField]
	[Serialize]
	private PrioritySetting masterPrioritySetting = new PrioritySetting(PriorityScreen.PriorityClass.basic, 5);

	// Token: 0x04000F8E RID: 3982
	public Action<PrioritySetting> onPriorityChanged;

	// Token: 0x04000F8F RID: 3983
	public bool showIcon = true;

	// Token: 0x04000F90 RID: 3984
	public Vector2 iconOffset;

	// Token: 0x04000F91 RID: 3985
	public float iconScale = 1f;

	// Token: 0x04000F92 RID: 3986
	[SerializeField]
	private int refCount;

	// Token: 0x04000F93 RID: 3987
	private static readonly EventSystem.IntraObjectHandler<Prioritizable> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<Prioritizable>(delegate(Prioritizable component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x04000F94 RID: 3988
	private static Dictionary<PrioritySetting, PrioritySetting> conversions = new Dictionary<PrioritySetting, PrioritySetting>
	{
		{
			new PrioritySetting(PriorityScreen.PriorityClass.basic, 1),
			new PrioritySetting(PriorityScreen.PriorityClass.basic, 4)
		},
		{
			new PrioritySetting(PriorityScreen.PriorityClass.basic, 2),
			new PrioritySetting(PriorityScreen.PriorityClass.basic, 5)
		},
		{
			new PrioritySetting(PriorityScreen.PriorityClass.basic, 3),
			new PrioritySetting(PriorityScreen.PriorityClass.basic, 6)
		},
		{
			new PrioritySetting(PriorityScreen.PriorityClass.basic, 4),
			new PrioritySetting(PriorityScreen.PriorityClass.basic, 7)
		},
		{
			new PrioritySetting(PriorityScreen.PriorityClass.basic, 5),
			new PrioritySetting(PriorityScreen.PriorityClass.basic, 8)
		},
		{
			new PrioritySetting(PriorityScreen.PriorityClass.high, 1),
			new PrioritySetting(PriorityScreen.PriorityClass.basic, 6)
		},
		{
			new PrioritySetting(PriorityScreen.PriorityClass.high, 2),
			new PrioritySetting(PriorityScreen.PriorityClass.basic, 7)
		},
		{
			new PrioritySetting(PriorityScreen.PriorityClass.high, 3),
			new PrioritySetting(PriorityScreen.PriorityClass.basic, 8)
		},
		{
			new PrioritySetting(PriorityScreen.PriorityClass.high, 4),
			new PrioritySetting(PriorityScreen.PriorityClass.basic, 9)
		},
		{
			new PrioritySetting(PriorityScreen.PriorityClass.high, 5),
			new PrioritySetting(PriorityScreen.PriorityClass.basic, 9)
		}
	};

	// Token: 0x04000F95 RID: 3989
	private HandleVector<int>.Handle scenePartitionerEntry;

	// Token: 0x04000F96 RID: 3990
	private Guid highPriorityStatusItem;
}
