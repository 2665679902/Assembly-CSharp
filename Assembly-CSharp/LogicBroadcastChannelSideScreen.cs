using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BC3 RID: 3011
public class LogicBroadcastChannelSideScreen : SideScreenContent
{
	// Token: 0x06005EA6 RID: 24230 RVA: 0x002292DA File Offset: 0x002274DA
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<LogicBroadcastReceiver>() != null;
	}

	// Token: 0x06005EA7 RID: 24231 RVA: 0x002292E8 File Offset: 0x002274E8
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.sensor = target.GetComponent<LogicBroadcastReceiver>();
		this.Build();
	}

	// Token: 0x06005EA8 RID: 24232 RVA: 0x00229304 File Offset: 0x00227504
	private void ClearRows()
	{
		if (this.emptySpaceRow != null)
		{
			Util.KDestroyGameObject(this.emptySpaceRow);
		}
		foreach (KeyValuePair<LogicBroadcaster, GameObject> keyValuePair in this.broadcasterRows)
		{
			Util.KDestroyGameObject(keyValuePair.Value);
		}
		this.broadcasterRows.Clear();
	}

	// Token: 0x06005EA9 RID: 24233 RVA: 0x00229380 File Offset: 0x00227580
	private void Build()
	{
		this.headerLabel.SetText(UI.UISIDESCREENS.LOGICBROADCASTCHANNELSIDESCREEN.HEADER);
		this.ClearRows();
		foreach (object obj in Components.LogicBroadcasters)
		{
			LogicBroadcaster logicBroadcaster = (LogicBroadcaster)obj;
			if (!logicBroadcaster.IsNullOrDestroyed())
			{
				GameObject gameObject = Util.KInstantiateUI(this.rowPrefab, this.listContainer, false);
				gameObject.gameObject.name = logicBroadcaster.gameObject.GetProperName();
				global::Debug.Assert(!this.broadcasterRows.ContainsKey(logicBroadcaster), "Adding two of the same broadcaster to LogicBroadcastChannelSideScreen UI: " + logicBroadcaster.gameObject.GetProperName());
				this.broadcasterRows.Add(logicBroadcaster, gameObject);
				gameObject.SetActive(true);
			}
		}
		this.noChannelRow.SetActive(Components.LogicBroadcasters.Count == 0);
		this.Refresh();
	}

	// Token: 0x06005EAA RID: 24234 RVA: 0x0022947C File Offset: 0x0022767C
	private void Refresh()
	{
		using (Dictionary<LogicBroadcaster, GameObject>.Enumerator enumerator = this.broadcasterRows.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<LogicBroadcaster, GameObject> kvp = enumerator.Current;
				kvp.Value.GetComponent<HierarchyReferences>().GetReference<LocText>("Label").SetText(kvp.Key.gameObject.GetProperName());
				kvp.Value.GetComponent<HierarchyReferences>().GetReference<LocText>("DistanceLabel").SetText(LogicBroadcastReceiver.CheckRange(this.sensor.gameObject, kvp.Key.gameObject) ? UI.UISIDESCREENS.LOGICBROADCASTCHANNELSIDESCREEN.IN_RANGE : UI.UISIDESCREENS.LOGICBROADCASTCHANNELSIDESCREEN.OUT_OF_RANGE);
				kvp.Value.GetComponent<HierarchyReferences>().GetReference<Image>("Icon").sprite = Def.GetUISprite(kvp.Key.gameObject, "ui", false).first;
				kvp.Value.GetComponent<HierarchyReferences>().GetReference<Image>("Icon").color = Def.GetUISprite(kvp.Key.gameObject, "ui", false).second;
				WorldContainer myWorld = kvp.Key.GetMyWorld();
				kvp.Value.GetComponent<HierarchyReferences>().GetReference<Image>("WorldIcon").sprite = (myWorld.IsModuleInterior ? Assets.GetSprite("icon_category_rocketry") : Def.GetUISprite(myWorld.GetComponent<ClusterGridEntity>(), "ui", false).first);
				kvp.Value.GetComponent<HierarchyReferences>().GetReference<Image>("WorldIcon").color = (myWorld.IsModuleInterior ? Color.white : Def.GetUISprite(myWorld.GetComponent<ClusterGridEntity>(), "ui", false).second);
				kvp.Value.GetComponent<HierarchyReferences>().GetReference<MultiToggle>("Toggle").onClick = delegate
				{
					this.sensor.SetChannel(kvp.Key);
					this.Refresh();
				};
				kvp.Value.GetComponent<HierarchyReferences>().GetReference<MultiToggle>("Toggle").ChangeState((this.sensor.GetChannel() == kvp.Key) ? 1 : 0);
			}
		}
	}

	// Token: 0x040040C1 RID: 16577
	private LogicBroadcastReceiver sensor;

	// Token: 0x040040C2 RID: 16578
	[SerializeField]
	private GameObject rowPrefab;

	// Token: 0x040040C3 RID: 16579
	[SerializeField]
	private GameObject listContainer;

	// Token: 0x040040C4 RID: 16580
	[SerializeField]
	private LocText headerLabel;

	// Token: 0x040040C5 RID: 16581
	[SerializeField]
	private GameObject noChannelRow;

	// Token: 0x040040C6 RID: 16582
	private Dictionary<LogicBroadcaster, GameObject> broadcasterRows = new Dictionary<LogicBroadcaster, GameObject>();

	// Token: 0x040040C7 RID: 16583
	private GameObject emptySpaceRow;
}
