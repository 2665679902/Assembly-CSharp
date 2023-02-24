using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B68 RID: 2920
[AddComponentMenu("KMonoBehaviour/scripts/ReportScreenEntryRow")]
public class ReportScreenEntryRow : KMonoBehaviour
{
	// Token: 0x06005B2B RID: 23339 RVA: 0x00211848 File Offset: 0x0020FA48
	private List<ReportManager.ReportEntry.Note> Sort(List<ReportManager.ReportEntry.Note> notes, ReportManager.ReportEntry.Order order)
	{
		if (order == ReportManager.ReportEntry.Order.Ascending)
		{
			notes.Sort((ReportManager.ReportEntry.Note x, ReportManager.ReportEntry.Note y) => x.value.CompareTo(y.value));
		}
		else if (order == ReportManager.ReportEntry.Order.Descending)
		{
			notes.Sort((ReportManager.ReportEntry.Note x, ReportManager.ReportEntry.Note y) => y.value.CompareTo(x.value));
		}
		return notes;
	}

	// Token: 0x06005B2C RID: 23340 RVA: 0x002118AA File Offset: 0x0020FAAA
	public static void DestroyStatics()
	{
		ReportScreenEntryRow.notes = null;
	}

	// Token: 0x06005B2D RID: 23341 RVA: 0x002118B4 File Offset: 0x0020FAB4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.added.GetComponent<ToolTip>().OnToolTip = new Func<string>(this.OnPositiveNoteTooltip);
		this.removed.GetComponent<ToolTip>().OnToolTip = new Func<string>(this.OnNegativeNoteTooltip);
		this.net.GetComponent<ToolTip>().OnToolTip = new Func<string>(this.OnNetNoteTooltip);
		this.name.GetComponent<ToolTip>().OnToolTip = new Func<string>(this.OnNetNoteTooltip);
	}

	// Token: 0x06005B2E RID: 23342 RVA: 0x00211938 File Offset: 0x0020FB38
	private string OnNoteTooltip(float total_accumulation, string tooltip_text, ReportManager.ReportEntry.Order order, ReportManager.FormattingFn format_fn, Func<ReportManager.ReportEntry.Note, bool> is_note_applicable_cb, ReportManager.GroupFormattingFn group_format_fn = null)
	{
		ReportScreenEntryRow.notes.Clear();
		this.entry.IterateNotes(delegate(ReportManager.ReportEntry.Note note)
		{
			if (is_note_applicable_cb(note))
			{
				ReportScreenEntryRow.notes.Add(note);
			}
		});
		string text = "";
		float num = 0f;
		if (this.entry.contextEntries.Count > 0)
		{
			num = (float)this.entry.contextEntries.Count;
		}
		else
		{
			num = (float)ReportScreenEntryRow.notes.Count;
		}
		num = Mathf.Max(num, 1f);
		foreach (ReportManager.ReportEntry.Note note2 in this.Sort(ReportScreenEntryRow.notes, this.reportGroup.posNoteOrder))
		{
			string text2 = format_fn(note2.value);
			if (this.toggle.gameObject.activeInHierarchy && group_format_fn != null)
			{
				text2 = group_format_fn(note2.value, num);
			}
			text = string.Format(UI.ENDOFDAYREPORT.NOTES.NOTE_ENTRY_LINE_ITEM, text, note2.note, text2);
		}
		string text3 = format_fn(total_accumulation);
		if (this.entry.context != null)
		{
			return string.Format(tooltip_text + "\n" + text, text3, this.entry.context);
		}
		if (group_format_fn != null)
		{
			text3 = group_format_fn(total_accumulation, num);
			return string.Format(tooltip_text + "\n" + text, text3, UI.ENDOFDAYREPORT.MY_COLONY);
		}
		return string.Format(tooltip_text + "\n" + text, text3, UI.ENDOFDAYREPORT.MY_COLONY);
	}

	// Token: 0x06005B2F RID: 23343 RVA: 0x00211AD4 File Offset: 0x0020FCD4
	private string OnNegativeNoteTooltip()
	{
		return this.OnNoteTooltip(-this.entry.Negative, this.reportGroup.negativeTooltip, this.reportGroup.negNoteOrder, this.reportGroup.formatfn, (ReportManager.ReportEntry.Note note) => this.IsNegativeNote(note), this.reportGroup.groupFormatfn);
	}

	// Token: 0x06005B30 RID: 23344 RVA: 0x00211B2C File Offset: 0x0020FD2C
	private string OnPositiveNoteTooltip()
	{
		return this.OnNoteTooltip(this.entry.Positive, this.reportGroup.positiveTooltip, this.reportGroup.posNoteOrder, this.reportGroup.formatfn, (ReportManager.ReportEntry.Note note) => this.IsPositiveNote(note), this.reportGroup.groupFormatfn);
	}

	// Token: 0x06005B31 RID: 23345 RVA: 0x00211B82 File Offset: 0x0020FD82
	private string OnNetNoteTooltip()
	{
		if (this.entry.Net > 0f)
		{
			return this.OnPositiveNoteTooltip();
		}
		return this.OnNegativeNoteTooltip();
	}

	// Token: 0x06005B32 RID: 23346 RVA: 0x00211BA3 File Offset: 0x0020FDA3
	private bool IsPositiveNote(ReportManager.ReportEntry.Note note)
	{
		return note.value > 0f;
	}

	// Token: 0x06005B33 RID: 23347 RVA: 0x00211BB5 File Offset: 0x0020FDB5
	private bool IsNegativeNote(ReportManager.ReportEntry.Note note)
	{
		return note.value < 0f;
	}

	// Token: 0x06005B34 RID: 23348 RVA: 0x00211BC8 File Offset: 0x0020FDC8
	public void SetLine(ReportManager.ReportEntry entry, ReportManager.ReportGroup reportGroup)
	{
		this.entry = entry;
		this.reportGroup = reportGroup;
		ListPool<ReportManager.ReportEntry.Note, ReportScreenEntryRow>.PooledList pos_notes = ListPool<ReportManager.ReportEntry.Note, ReportScreenEntryRow>.Allocate();
		entry.IterateNotes(delegate(ReportManager.ReportEntry.Note note)
		{
			if (this.IsPositiveNote(note))
			{
				pos_notes.Add(note);
			}
		});
		ListPool<ReportManager.ReportEntry.Note, ReportScreenEntryRow>.PooledList neg_notes = ListPool<ReportManager.ReportEntry.Note, ReportScreenEntryRow>.Allocate();
		entry.IterateNotes(delegate(ReportManager.ReportEntry.Note note)
		{
			if (this.IsNegativeNote(note))
			{
				neg_notes.Add(note);
			}
		});
		LayoutElement component = this.name.GetComponent<LayoutElement>();
		if (entry.context == null)
		{
			component.minWidth = (component.preferredWidth = this.nameWidth);
			if (entry.HasContextEntries())
			{
				this.toggle.gameObject.SetActive(true);
				this.spacer.minWidth = this.groupSpacerWidth;
			}
			else
			{
				this.toggle.gameObject.SetActive(false);
				this.spacer.minWidth = this.groupSpacerWidth + this.toggle.GetComponent<LayoutElement>().minWidth;
			}
			this.name.text = reportGroup.stringKey;
		}
		else
		{
			this.toggle.gameObject.SetActive(false);
			this.spacer.minWidth = this.contextSpacerWidth;
			this.name.text = entry.context;
			component.minWidth = (component.preferredWidth = this.nameWidth - this.indentWidth);
			if (base.transform.GetSiblingIndex() % 2 != 0)
			{
				this.bgImage.color = this.oddRowColor;
			}
		}
		if (this.addedValue != entry.Positive)
		{
			string text = reportGroup.formatfn(entry.Positive);
			if (reportGroup.groupFormatfn != null && entry.context == null)
			{
				float num;
				if (entry.contextEntries.Count > 0)
				{
					num = (float)entry.contextEntries.Count;
				}
				else
				{
					num = (float)pos_notes.Count;
				}
				num = Mathf.Max(num, 1f);
				text = reportGroup.groupFormatfn(entry.Positive, num);
			}
			this.added.text = text;
			this.addedValue = entry.Positive;
		}
		if (this.removedValue != entry.Negative)
		{
			string text2 = reportGroup.formatfn(-entry.Negative);
			if (reportGroup.groupFormatfn != null && entry.context == null)
			{
				float num2;
				if (entry.contextEntries.Count > 0)
				{
					num2 = (float)entry.contextEntries.Count;
				}
				else
				{
					num2 = (float)neg_notes.Count;
				}
				num2 = Mathf.Max(num2, 1f);
				text2 = reportGroup.groupFormatfn(-entry.Negative, num2);
			}
			this.removed.text = text2;
			this.removedValue = entry.Negative;
		}
		if (this.netValue != entry.Net)
		{
			string text3 = ((reportGroup.formatfn == null) ? entry.Net.ToString() : reportGroup.formatfn(entry.Net));
			if (reportGroup.groupFormatfn != null && entry.context == null)
			{
				float num3;
				if (entry.contextEntries.Count > 0)
				{
					num3 = (float)entry.contextEntries.Count;
				}
				else
				{
					num3 = (float)(pos_notes.Count + neg_notes.Count);
				}
				num3 = Mathf.Max(num3, 1f);
				text3 = reportGroup.groupFormatfn(entry.Net, num3);
			}
			this.net.text = text3;
			this.netValue = entry.Net;
		}
		pos_notes.Recycle();
		neg_notes.Recycle();
	}

	// Token: 0x04003DD1 RID: 15825
	[SerializeField]
	public new LocText name;

	// Token: 0x04003DD2 RID: 15826
	[SerializeField]
	public LocText added;

	// Token: 0x04003DD3 RID: 15827
	[SerializeField]
	public LocText removed;

	// Token: 0x04003DD4 RID: 15828
	[SerializeField]
	public LocText net;

	// Token: 0x04003DD5 RID: 15829
	private float addedValue = float.NegativeInfinity;

	// Token: 0x04003DD6 RID: 15830
	private float removedValue = float.NegativeInfinity;

	// Token: 0x04003DD7 RID: 15831
	private float netValue = float.NegativeInfinity;

	// Token: 0x04003DD8 RID: 15832
	[SerializeField]
	public MultiToggle toggle;

	// Token: 0x04003DD9 RID: 15833
	[SerializeField]
	private LayoutElement spacer;

	// Token: 0x04003DDA RID: 15834
	[SerializeField]
	private Image bgImage;

	// Token: 0x04003DDB RID: 15835
	public float groupSpacerWidth;

	// Token: 0x04003DDC RID: 15836
	public float contextSpacerWidth;

	// Token: 0x04003DDD RID: 15837
	private float nameWidth = 164f;

	// Token: 0x04003DDE RID: 15838
	private float indentWidth = 6f;

	// Token: 0x04003DDF RID: 15839
	[SerializeField]
	private Color oddRowColor;

	// Token: 0x04003DE0 RID: 15840
	private static List<ReportManager.ReportEntry.Note> notes = new List<ReportManager.ReportEntry.Note>();

	// Token: 0x04003DE1 RID: 15841
	private ReportManager.ReportEntry entry;

	// Token: 0x04003DE2 RID: 15842
	private ReportManager.ReportGroup reportGroup;
}
