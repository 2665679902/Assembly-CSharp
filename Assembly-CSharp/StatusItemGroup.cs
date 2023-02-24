using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004D3 RID: 1235
public class StatusItemGroup
{
	// Token: 0x06001CA9 RID: 7337 RVA: 0x00098DDF File Offset: 0x00096FDF
	public IEnumerator<StatusItemGroup.Entry> GetEnumerator()
	{
		return this.items.GetEnumerator();
	}

	// Token: 0x17000134 RID: 308
	// (get) Token: 0x06001CAA RID: 7338 RVA: 0x00098DF1 File Offset: 0x00096FF1
	// (set) Token: 0x06001CAB RID: 7339 RVA: 0x00098DF9 File Offset: 0x00096FF9
	public GameObject gameObject { get; private set; }

	// Token: 0x06001CAC RID: 7340 RVA: 0x00098E02 File Offset: 0x00097002
	public StatusItemGroup(GameObject go)
	{
		this.gameObject = go;
	}

	// Token: 0x06001CAD RID: 7341 RVA: 0x00098E36 File Offset: 0x00097036
	public void SetOffset(Vector3 offset)
	{
		this.offset = offset;
		Game.Instance.SetStatusItemOffset(this.gameObject.transform, offset);
	}

	// Token: 0x06001CAE RID: 7342 RVA: 0x00098E58 File Offset: 0x00097058
	public StatusItemGroup.Entry GetStatusItem(StatusItemCategory category)
	{
		for (int i = 0; i < this.items.Count; i++)
		{
			if (this.items[i].category == category)
			{
				return this.items[i];
			}
		}
		return StatusItemGroup.Entry.EmptyEntry;
	}

	// Token: 0x06001CAF RID: 7343 RVA: 0x00098EA4 File Offset: 0x000970A4
	public Guid SetStatusItem(StatusItemCategory category, StatusItem item, object data = null)
	{
		if (item != null && item.allowMultiples)
		{
			throw new ArgumentException(item.Name + " allows multiple instances of itself to be active so you must access it via its handle");
		}
		if (category == null)
		{
			throw new ArgumentException("SetStatusItem requires a category.");
		}
		for (int i = 0; i < this.items.Count; i++)
		{
			if (this.items[i].category == category)
			{
				if (this.items[i].item == item)
				{
					this.Log("Set (exists in category)", item, this.items[i].id, category);
					return this.items[i].id;
				}
				this.Log("Set->Remove existing in category", item, this.items[i].id, category);
				this.RemoveStatusItem(this.items[i].id, false);
			}
		}
		if (item != null)
		{
			Guid guid = this.AddStatusItem(item, data, category);
			this.Log("Set (new)", item, guid, category);
			return guid;
		}
		this.Log("Set (failed)", item, Guid.Empty, category);
		return Guid.Empty;
	}

	// Token: 0x06001CB0 RID: 7344 RVA: 0x00098FBF File Offset: 0x000971BF
	public void SetStatusItem(Guid guid, StatusItemCategory category, StatusItem new_item, object data = null)
	{
		this.RemoveStatusItem(guid, false);
		if (new_item != null)
		{
			this.AddStatusItem(new_item, data, category);
		}
	}

	// Token: 0x06001CB1 RID: 7345 RVA: 0x00098FD8 File Offset: 0x000971D8
	public bool HasStatusItem(StatusItem status_item)
	{
		for (int i = 0; i < this.items.Count; i++)
		{
			if (this.items[i].item.Id == status_item.Id)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001CB2 RID: 7346 RVA: 0x00099024 File Offset: 0x00097224
	public bool HasStatusItemID(string status_item_id)
	{
		for (int i = 0; i < this.items.Count; i++)
		{
			if (this.items[i].item.Id == status_item_id)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001CB3 RID: 7347 RVA: 0x00099068 File Offset: 0x00097268
	public Guid AddStatusItem(StatusItem item, object data = null, StatusItemCategory category = null)
	{
		if (this.gameObject == null || (!item.allowMultiples && this.HasStatusItem(item)))
		{
			return Guid.Empty;
		}
		if (!item.allowMultiples)
		{
			using (List<StatusItemGroup.Entry>.Enumerator enumerator = this.items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.item.Id == item.Id)
					{
						throw new ArgumentException("Tried to add " + item.Id + " multiples times which is not permitted.");
					}
				}
			}
		}
		StatusItemGroup.Entry entry = new StatusItemGroup.Entry(item, category, data);
		if (item.shouldNotify)
		{
			entry.notification = new Notification(item.notificationText, item.notificationType, new Func<List<Notification>, object, string>(StatusItemGroup.OnToolTip), item, false, 0f, item.notificationClickCallback, data, null, true, false, false);
			this.gameObject.AddOrGet<Notifier>().Add(entry.notification, "");
		}
		if (item.ShouldShowIcon())
		{
			Game.Instance.AddStatusItem(this.gameObject.transform, item);
			Game.Instance.SetStatusItemOffset(this.gameObject.transform, this.offset);
		}
		this.items.Add(entry);
		if (this.OnAddStatusItem != null)
		{
			this.OnAddStatusItem(entry, category);
		}
		return entry.id;
	}

	// Token: 0x06001CB4 RID: 7348 RVA: 0x000991D8 File Offset: 0x000973D8
	public Guid RemoveStatusItem(StatusItem status_item, bool immediate = false)
	{
		if (status_item.allowMultiples)
		{
			throw new ArgumentException(status_item.Name + " allows multiple instances of itself to be active so it must be released via an instance handle");
		}
		int i = 0;
		while (i < this.items.Count)
		{
			if (this.items[i].item.Id == status_item.Id)
			{
				Guid id = this.items[i].id;
				if (id == Guid.Empty)
				{
					return id;
				}
				this.RemoveStatusItemInternal(id, i, immediate);
				return id;
			}
			else
			{
				i++;
			}
		}
		return Guid.Empty;
	}

	// Token: 0x06001CB5 RID: 7349 RVA: 0x00099270 File Offset: 0x00097470
	public Guid RemoveStatusItem(Guid guid, bool immediate = false)
	{
		if (guid == Guid.Empty)
		{
			return guid;
		}
		for (int i = 0; i < this.items.Count; i++)
		{
			if (this.items[i].id == guid)
			{
				this.RemoveStatusItemInternal(guid, i, immediate);
				return guid;
			}
		}
		return Guid.Empty;
	}

	// Token: 0x06001CB6 RID: 7350 RVA: 0x000992CC File Offset: 0x000974CC
	private void RemoveStatusItemInternal(Guid guid, int itemIdx, bool immediate)
	{
		StatusItemGroup.Entry entry = this.items[itemIdx];
		this.items.RemoveAt(itemIdx);
		if (entry.notification != null)
		{
			this.gameObject.GetComponent<Notifier>().Remove(entry.notification);
		}
		if (entry.item.ShouldShowIcon() && Game.Instance != null)
		{
			Game.Instance.RemoveStatusItem(this.gameObject.transform, entry.item);
		}
		if (this.OnRemoveStatusItem != null)
		{
			this.OnRemoveStatusItem(entry, immediate);
		}
	}

	// Token: 0x06001CB7 RID: 7351 RVA: 0x0009935A File Offset: 0x0009755A
	private static string OnToolTip(List<Notification> notifications, object data)
	{
		return ((StatusItem)data).notificationTooltipText + notifications.ReduceMessages(true);
	}

	// Token: 0x06001CB8 RID: 7352 RVA: 0x00099373 File Offset: 0x00097573
	public void Destroy()
	{
		if (Game.IsQuitting())
		{
			return;
		}
		while (this.items.Count > 0)
		{
			this.RemoveStatusItem(this.items[0].id, false);
		}
	}

	// Token: 0x06001CB9 RID: 7353 RVA: 0x000993A4 File Offset: 0x000975A4
	[Conditional("ENABLE_LOGGER")]
	private void Log(string action, StatusItem item, Guid guid)
	{
	}

	// Token: 0x06001CBA RID: 7354 RVA: 0x000993A6 File Offset: 0x000975A6
	private void Log(string action, StatusItem item, Guid guid, StatusItemCategory category)
	{
	}

	// Token: 0x04001030 RID: 4144
	private List<StatusItemGroup.Entry> items = new List<StatusItemGroup.Entry>();

	// Token: 0x04001031 RID: 4145
	public Action<StatusItemGroup.Entry, StatusItemCategory> OnAddStatusItem;

	// Token: 0x04001032 RID: 4146
	public Action<StatusItemGroup.Entry, bool> OnRemoveStatusItem;

	// Token: 0x04001034 RID: 4148
	private Vector3 offset = new Vector3(0f, 0f, 0f);

	// Token: 0x02001116 RID: 4374
	public struct Entry : IComparable<StatusItemGroup.Entry>, IEquatable<StatusItemGroup.Entry>
	{
		// Token: 0x06007574 RID: 30068 RVA: 0x002B5EA1 File Offset: 0x002B40A1
		public Entry(StatusItem item, StatusItemCategory category, object data)
		{
			this.id = Guid.NewGuid();
			this.item = item;
			this.data = data;
			this.category = category;
			this.notification = null;
		}

		// Token: 0x06007575 RID: 30069 RVA: 0x002B5ECA File Offset: 0x002B40CA
		public string GetName()
		{
			return this.item.GetName(this.data);
		}

		// Token: 0x06007576 RID: 30070 RVA: 0x002B5EDD File Offset: 0x002B40DD
		public void ShowToolTip(ToolTip tooltip_widget, TextStyleSetting property_style)
		{
			this.item.ShowToolTip(tooltip_widget, this.data, property_style);
		}

		// Token: 0x06007577 RID: 30071 RVA: 0x002B5EF2 File Offset: 0x002B40F2
		public void SetIcon(Image image)
		{
			this.item.SetIcon(image, this.data);
		}

		// Token: 0x06007578 RID: 30072 RVA: 0x002B5F06 File Offset: 0x002B4106
		public int CompareTo(StatusItemGroup.Entry other)
		{
			return this.id.CompareTo(other.id);
		}

		// Token: 0x06007579 RID: 30073 RVA: 0x002B5F19 File Offset: 0x002B4119
		public bool Equals(StatusItemGroup.Entry other)
		{
			return this.id == other.id;
		}

		// Token: 0x0600757A RID: 30074 RVA: 0x002B5F2C File Offset: 0x002B412C
		public void OnClick()
		{
			this.item.OnClick(this.data);
		}

		// Token: 0x040059C0 RID: 22976
		public static StatusItemGroup.Entry EmptyEntry = new StatusItemGroup.Entry
		{
			id = Guid.Empty
		};

		// Token: 0x040059C1 RID: 22977
		public Guid id;

		// Token: 0x040059C2 RID: 22978
		public StatusItem item;

		// Token: 0x040059C3 RID: 22979
		public object data;

		// Token: 0x040059C4 RID: 22980
		public Notification notification;

		// Token: 0x040059C5 RID: 22981
		public StatusItemCategory category;
	}
}
