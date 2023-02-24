using System;
using Klei.AI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000AF3 RID: 2803
[AddComponentMenu("KMonoBehaviour/scripts/CrewListEntry")]
public class CrewListEntry : KMonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	// Token: 0x17000642 RID: 1602
	// (get) Token: 0x0600560E RID: 22030 RVA: 0x001F24CE File Offset: 0x001F06CE
	public MinionIdentity Identity
	{
		get
		{
			return this.identity;
		}
	}

	// Token: 0x0600560F RID: 22031 RVA: 0x001F24D6 File Offset: 0x001F06D6
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.mouseOver = true;
		this.BGImage.enabled = true;
		this.BorderHighlight.color = new Color(0.65882355f, 0.2901961f, 0.4745098f);
	}

	// Token: 0x06005610 RID: 22032 RVA: 0x001F250A File Offset: 0x001F070A
	public void OnPointerExit(PointerEventData eventData)
	{
		this.mouseOver = false;
		this.BGImage.enabled = false;
		this.BorderHighlight.color = new Color(0.8f, 0.8f, 0.8f);
	}

	// Token: 0x06005611 RID: 22033 RVA: 0x001F2540 File Offset: 0x001F0740
	public void OnPointerClick(PointerEventData eventData)
	{
		bool flag = Time.unscaledTime - this.lastClickTime < 0.3f;
		this.SelectCrewMember(flag);
		this.lastClickTime = Time.unscaledTime;
	}

	// Token: 0x06005612 RID: 22034 RVA: 0x001F2574 File Offset: 0x001F0774
	public virtual void Populate(MinionIdentity _identity)
	{
		this.identity = _identity;
		if (this.portrait == null)
		{
			GameObject gameObject = ((this.crewPortraitParent != null) ? this.crewPortraitParent : base.gameObject);
			this.portrait = Util.KInstantiateUI<CrewPortrait>(this.PortraitPrefab.gameObject, gameObject, false);
			if (this.crewPortraitParent == null)
			{
				this.portrait.transform.SetSiblingIndex(2);
			}
		}
		this.portrait.SetIdentityObject(_identity, true);
	}

	// Token: 0x06005613 RID: 22035 RVA: 0x001F25F7 File Offset: 0x001F07F7
	public virtual void Refresh()
	{
	}

	// Token: 0x06005614 RID: 22036 RVA: 0x001F25F9 File Offset: 0x001F07F9
	public void RefreshCrewPortraitContent()
	{
		if (this.portrait != null)
		{
			this.portrait.ForceRefresh();
		}
	}

	// Token: 0x06005615 RID: 22037 RVA: 0x001F2614 File Offset: 0x001F0814
	private string seniorityString()
	{
		return this.identity.GetAttributes().GetProfessionString(true);
	}

	// Token: 0x06005616 RID: 22038 RVA: 0x001F2628 File Offset: 0x001F0828
	public void SelectCrewMember(bool focus)
	{
		if (focus)
		{
			SelectTool.Instance.SelectAndFocus(this.identity.transform.GetPosition(), this.identity.GetComponent<KSelectable>(), new Vector3(8f, 0f, 0f));
			return;
		}
		SelectTool.Instance.Select(this.identity.GetComponent<KSelectable>(), false);
	}

	// Token: 0x04003A8B RID: 14987
	protected MinionIdentity identity;

	// Token: 0x04003A8C RID: 14988
	protected CrewPortrait portrait;

	// Token: 0x04003A8D RID: 14989
	public CrewPortrait PortraitPrefab;

	// Token: 0x04003A8E RID: 14990
	public GameObject crewPortraitParent;

	// Token: 0x04003A8F RID: 14991
	protected bool mouseOver;

	// Token: 0x04003A90 RID: 14992
	public Image BorderHighlight;

	// Token: 0x04003A91 RID: 14993
	public Image BGImage;

	// Token: 0x04003A92 RID: 14994
	public float lastClickTime;
}
