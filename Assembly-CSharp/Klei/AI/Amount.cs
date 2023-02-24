using System;
using System.Diagnostics;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D61 RID: 3425
	[DebuggerDisplay("{Id}")]
	public class Amount : Resource
	{
		// Token: 0x060068A6 RID: 26790 RVA: 0x0028BA54 File Offset: 0x00289C54
		public Amount(string id, string name, string description, Attribute min_attribute, Attribute max_attribute, Attribute delta_attribute, bool show_max, Units units, float visual_delta_threshold, bool show_in_ui, string uiSprite = null, string thoughtSprite = null)
		{
			this.Id = id;
			this.Name = name;
			this.description = description;
			this.minAttribute = min_attribute;
			this.maxAttribute = max_attribute;
			this.deltaAttribute = delta_attribute;
			this.showMax = show_max;
			this.units = units;
			this.visualDeltaThreshold = visual_delta_threshold;
			this.showInUI = show_in_ui;
			this.uiSprite = uiSprite;
			this.thoughtSprite = thoughtSprite;
		}

		// Token: 0x060068A7 RID: 26791 RVA: 0x0028BAC4 File Offset: 0x00289CC4
		public void SetDisplayer(IAmountDisplayer displayer)
		{
			this.displayer = displayer;
			this.minAttribute.SetFormatter(displayer.Formatter);
			this.maxAttribute.SetFormatter(displayer.Formatter);
			this.deltaAttribute.SetFormatter(displayer.Formatter);
		}

		// Token: 0x060068A8 RID: 26792 RVA: 0x0028BB00 File Offset: 0x00289D00
		public AmountInstance Lookup(Component cmp)
		{
			return this.Lookup(cmp.gameObject);
		}

		// Token: 0x060068A9 RID: 26793 RVA: 0x0028BB10 File Offset: 0x00289D10
		public AmountInstance Lookup(GameObject go)
		{
			Amounts amounts = go.GetAmounts();
			if (amounts != null)
			{
				return amounts.Get(this);
			}
			return null;
		}

		// Token: 0x060068AA RID: 26794 RVA: 0x0028BB30 File Offset: 0x00289D30
		public void Copy(GameObject to, GameObject from)
		{
			AmountInstance amountInstance = this.Lookup(to);
			AmountInstance amountInstance2 = this.Lookup(from);
			amountInstance.value = amountInstance2.value;
		}

		// Token: 0x060068AB RID: 26795 RVA: 0x0028BB57 File Offset: 0x00289D57
		public string GetValueString(AmountInstance instance)
		{
			return this.displayer.GetValueString(this, instance);
		}

		// Token: 0x060068AC RID: 26796 RVA: 0x0028BB66 File Offset: 0x00289D66
		public string GetDescription(AmountInstance instance)
		{
			return this.displayer.GetDescription(this, instance);
		}

		// Token: 0x060068AD RID: 26797 RVA: 0x0028BB75 File Offset: 0x00289D75
		public string GetTooltip(AmountInstance instance)
		{
			return this.displayer.GetTooltip(this, instance);
		}

		// Token: 0x04004ECA RID: 20170
		public string description;

		// Token: 0x04004ECB RID: 20171
		public bool showMax;

		// Token: 0x04004ECC RID: 20172
		public Units units;

		// Token: 0x04004ECD RID: 20173
		public float visualDeltaThreshold;

		// Token: 0x04004ECE RID: 20174
		public Attribute minAttribute;

		// Token: 0x04004ECF RID: 20175
		public Attribute maxAttribute;

		// Token: 0x04004ED0 RID: 20176
		public Attribute deltaAttribute;

		// Token: 0x04004ED1 RID: 20177
		public bool showInUI;

		// Token: 0x04004ED2 RID: 20178
		public string uiSprite;

		// Token: 0x04004ED3 RID: 20179
		public string thoughtSprite;

		// Token: 0x04004ED4 RID: 20180
		public IAmountDisplayer displayer;
	}
}
