using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A45 RID: 2629
[AddComponentMenu("KMonoBehaviour/scripts/AssignableRegionCharacterSelection")]
public class AssignableRegionCharacterSelection : KMonoBehaviour
{
	// Token: 0x1400001F RID: 31
	// (add) Token: 0x06004FB6 RID: 20406 RVA: 0x001C61B4 File Offset: 0x001C43B4
	// (remove) Token: 0x06004FB7 RID: 20407 RVA: 0x001C61EC File Offset: 0x001C43EC
	public event Action<MinionIdentity> OnDuplicantSelected;

	// Token: 0x06004FB8 RID: 20408 RVA: 0x001C6221 File Offset: 0x001C4421
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.buttonPool = new UIPool<KButton>(this.buttonPrefab);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06004FB9 RID: 20409 RVA: 0x001C6248 File Offset: 0x001C4448
	public void Open()
	{
		base.gameObject.SetActive(true);
		this.buttonPool.ClearAll();
		foreach (MinionIdentity minionIdentity in Components.MinionIdentities.Items)
		{
			KButton btn = this.buttonPool.GetFreeElement(this.buttonParent, true);
			CrewPortrait componentInChildren = btn.GetComponentInChildren<CrewPortrait>();
			componentInChildren.SetIdentityObject(minionIdentity, true);
			this.portraitList.Add(componentInChildren);
			btn.ClearOnClick();
			btn.onClick += delegate
			{
				this.SelectDuplicant(btn);
			};
			this.buttonIdentityMap.Add(btn, minionIdentity);
		}
	}

	// Token: 0x06004FBA RID: 20410 RVA: 0x001C6330 File Offset: 0x001C4530
	public void Close()
	{
		this.buttonPool.DestroyAllActive();
		this.buttonIdentityMap.Clear();
		this.portraitList.Clear();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06004FBB RID: 20411 RVA: 0x001C635F File Offset: 0x001C455F
	private void SelectDuplicant(KButton btn)
	{
		if (this.OnDuplicantSelected != null)
		{
			this.OnDuplicantSelected(this.buttonIdentityMap[btn]);
		}
		this.Close();
	}

	// Token: 0x04003566 RID: 13670
	[SerializeField]
	private KButton buttonPrefab;

	// Token: 0x04003567 RID: 13671
	[SerializeField]
	private GameObject buttonParent;

	// Token: 0x04003568 RID: 13672
	private UIPool<KButton> buttonPool;

	// Token: 0x04003569 RID: 13673
	private Dictionary<KButton, MinionIdentity> buttonIdentityMap = new Dictionary<KButton, MinionIdentity>();

	// Token: 0x0400356A RID: 13674
	private List<CrewPortrait> portraitList = new List<CrewPortrait>();
}
