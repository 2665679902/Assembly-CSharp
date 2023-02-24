using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200006B RID: 107
public class KTreeControl : MonoBehaviour
{
	// Token: 0x0600046B RID: 1131 RVA: 0x0001631D File Offset: 0x0001451D
	public void SetUserItemRoot(KTreeControl.UserItem rootItem)
	{
		if (this.root != null)
		{
			UnityEngine.Object.Destroy(this.root);
		}
		this.root = this.CreateItem(rootItem);
		this.root.transform.SetParent(base.transform, false);
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x0001635C File Offset: 0x0001455C
	private KTreeItem CreateItem(KTreeControl.UserItem userItem)
	{
		KTreeItem ktreeItem = UnityEngine.Object.Instantiate<KTreeItem>(this.treeItemPrefab);
		ktreeItem.text = userItem.text;
		ktreeItem.userData = userItem.userData;
		ktreeItem.onOpenChanged += this.OnOpenChanged;
		ktreeItem.onCheckChanged += this.OnCheckChanged;
		if (userItem.children != null)
		{
			for (int i = 0; i < userItem.children.Count; i++)
			{
				KTreeItem ktreeItem2 = this.CreateItem(userItem.children[i]);
				ktreeItem.AddChild(ktreeItem2);
			}
		}
		return ktreeItem;
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x000163EA File Offset: 0x000145EA
	private void OnOpenChanged(KTreeItem item, bool value)
	{
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x000163EC File Offset: 0x000145EC
	private void OnCheckChanged(KTreeItem item, bool isChecked)
	{
		if (item.parent != null)
		{
			bool flag = true;
			using (IEnumerator<KTreeItem> enumerator = item.parent.children.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.checkboxChecked)
					{
						flag = false;
						break;
					}
				}
			}
			item.parent.checkboxChecked = flag;
			this.ChangeChecks(item.parent, flag);
		}
		if (item.children != null)
		{
			foreach (KTreeItem ktreeItem in item.children)
			{
				ktreeItem.checkboxChecked = isChecked;
				this.OnCheckChanged(ktreeItem, isChecked);
			}
		}
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x000164B8 File Offset: 0x000146B8
	private void ChangeChecks(KTreeItem item, bool isChecked)
	{
		if (item.parent != null)
		{
			bool flag = true;
			using (IEnumerator<KTreeItem> enumerator = item.parent.children.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.checkboxChecked)
					{
						flag = false;
						break;
					}
				}
			}
			item.parent.checkboxChecked = flag;
			this.ChangeChecks(item.parent, flag);
		}
	}

	// Token: 0x040004B9 RID: 1209
	[SerializeField]
	private KTreeItem treeItemPrefab;

	// Token: 0x040004BA RID: 1210
	[NonSerialized]
	public KTreeItem root;

	// Token: 0x020009BE RID: 2494
	public class UserItem
	{
		// Token: 0x040021C5 RID: 8645
		public string text;

		// Token: 0x040021C6 RID: 8646
		public object userData;

		// Token: 0x040021C7 RID: 8647
		public IList<KTreeControl.UserItem> children;
	}
}
