using System;
using UnityEngine;

// Token: 0x02000B08 RID: 2824
public class DividerColumn : TableColumn
{
	// Token: 0x060056E7 RID: 22247 RVA: 0x001F8E6C File Offset: 0x001F706C
	public DividerColumn(Func<bool> revealed = null, string scrollerID = "")
		: base(delegate(IAssignableIdentity minion, GameObject widget_go)
		{
			if (revealed != null)
			{
				if (revealed())
				{
					if (!widget_go.activeSelf)
					{
						widget_go.SetActive(true);
						return;
					}
				}
				else if (widget_go.activeSelf)
				{
					widget_go.SetActive(false);
					return;
				}
			}
			else
			{
				widget_go.SetActive(true);
			}
		}, null, null, null, revealed, false, scrollerID)
	{
	}

	// Token: 0x060056E8 RID: 22248 RVA: 0x001F8EA3 File Offset: 0x001F70A3
	public override GameObject GetDefaultWidget(GameObject parent)
	{
		return Util.KInstantiateUI(Assets.UIPrefabs.TableScreenWidgets.Spacer, parent, true);
	}

	// Token: 0x060056E9 RID: 22249 RVA: 0x001F8EBB File Offset: 0x001F70BB
	public override GameObject GetMinionWidget(GameObject parent)
	{
		return Util.KInstantiateUI(Assets.UIPrefabs.TableScreenWidgets.Spacer, parent, true);
	}

	// Token: 0x060056EA RID: 22250 RVA: 0x001F8ED3 File Offset: 0x001F70D3
	public override GameObject GetHeaderWidget(GameObject parent)
	{
		return Util.KInstantiateUI(Assets.UIPrefabs.TableScreenWidgets.Spacer, parent, true);
	}
}
