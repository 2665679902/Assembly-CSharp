using System;
using UnityEngine;

// Token: 0x02000B53 RID: 2899
[AddComponentMenu("KMonoBehaviour/scripts/PageView")]
public class PageView : KMonoBehaviour
{
	// Token: 0x17000663 RID: 1635
	// (get) Token: 0x06005A0D RID: 23053 RVA: 0x00209876 File Offset: 0x00207A76
	public int ChildrenPerPage
	{
		get
		{
			return this.childrenPerPage;
		}
	}

	// Token: 0x06005A0E RID: 23054 RVA: 0x0020987E File Offset: 0x00207A7E
	private void Update()
	{
		if (this.oldChildCount != base.transform.childCount)
		{
			this.oldChildCount = base.transform.childCount;
			this.RefreshPage();
		}
	}

	// Token: 0x06005A0F RID: 23055 RVA: 0x002098AC File Offset: 0x00207AAC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		MultiToggle multiToggle = this.nextButton;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(delegate
		{
			this.currentPage = (this.currentPage + 1) % this.pageCount;
			if (this.OnChangePage != null)
			{
				this.OnChangePage(this.currentPage);
			}
			this.RefreshPage();
		}));
		MultiToggle multiToggle2 = this.prevButton;
		multiToggle2.onClick = (System.Action)Delegate.Combine(multiToggle2.onClick, new System.Action(delegate
		{
			this.currentPage--;
			if (this.currentPage < 0)
			{
				this.currentPage += this.pageCount;
			}
			if (this.OnChangePage != null)
			{
				this.OnChangePage(this.currentPage);
			}
			this.RefreshPage();
		}));
	}

	// Token: 0x17000664 RID: 1636
	// (get) Token: 0x06005A10 RID: 23056 RVA: 0x00209910 File Offset: 0x00207B10
	private int pageCount
	{
		get
		{
			int num = base.transform.childCount / this.childrenPerPage;
			if (base.transform.childCount % this.childrenPerPage != 0)
			{
				num++;
			}
			return num;
		}
	}

	// Token: 0x06005A11 RID: 23057 RVA: 0x0020994C File Offset: 0x00207B4C
	private void RefreshPage()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			if (i < this.currentPage * this.childrenPerPage)
			{
				base.transform.GetChild(i).gameObject.SetActive(false);
			}
			else if (i >= this.currentPage * this.childrenPerPage + this.childrenPerPage)
			{
				base.transform.GetChild(i).gameObject.SetActive(false);
			}
			else
			{
				base.transform.GetChild(i).gameObject.SetActive(true);
			}
		}
		this.pageLabel.SetText((this.currentPage % this.pageCount + 1).ToString() + "/" + this.pageCount.ToString());
	}

	// Token: 0x04003CE1 RID: 15585
	[SerializeField]
	private MultiToggle nextButton;

	// Token: 0x04003CE2 RID: 15586
	[SerializeField]
	private MultiToggle prevButton;

	// Token: 0x04003CE3 RID: 15587
	[SerializeField]
	private LocText pageLabel;

	// Token: 0x04003CE4 RID: 15588
	[SerializeField]
	private int childrenPerPage = 8;

	// Token: 0x04003CE5 RID: 15589
	private int currentPage;

	// Token: 0x04003CE6 RID: 15590
	private int oldChildCount;

	// Token: 0x04003CE7 RID: 15591
	public Action<int> OnChangePage;
}
