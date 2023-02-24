using System;

// Token: 0x020004D5 RID: 1237
public class Storable : KMonoBehaviour
{
	// Token: 0x06001CDB RID: 7387 RVA: 0x00099BCA File Offset: 0x00097DCA
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<Storable>(856640610, Storable.OnStoreDelegate);
		base.Subscribe<Storable>(-778359855, Storable.RefreshStorageTagsDelegate);
	}

	// Token: 0x06001CDC RID: 7388 RVA: 0x00099BF4 File Offset: 0x00097DF4
	public void OnStore(object data)
	{
		this.RefreshStorageTags(data);
	}

	// Token: 0x06001CDD RID: 7389 RVA: 0x00099C00 File Offset: 0x00097E00
	private void RefreshStorageTags(object data = null)
	{
		bool flag = data is Storage || (data != null && (bool)data);
		Storage storage = (Storage)data;
		if (storage != null && storage.gameObject == base.gameObject)
		{
			return;
		}
		KPrefabID component = base.GetComponent<KPrefabID>();
		SaveLoadRoot component2 = base.GetComponent<SaveLoadRoot>();
		KSelectable component3 = base.GetComponent<KSelectable>();
		if (component3)
		{
			component3.IsSelectable = !flag;
		}
		if (flag)
		{
			component.AddTag(GameTags.Stored, false);
			if (storage == null || !storage.allowItemRemoval)
			{
				component.AddTag(GameTags.StoredPrivate, false);
			}
			else
			{
				component.RemoveTag(GameTags.StoredPrivate);
			}
			if (component2 != null)
			{
				component2.SetRegistered(false);
				return;
			}
		}
		else
		{
			component.RemoveTag(GameTags.Stored);
			component.RemoveTag(GameTags.StoredPrivate);
			if (component2 != null)
			{
				component2.SetRegistered(true);
			}
		}
	}

	// Token: 0x04001043 RID: 4163
	private static readonly EventSystem.IntraObjectHandler<Storable> OnStoreDelegate = new EventSystem.IntraObjectHandler<Storable>(delegate(Storable component, object data)
	{
		component.OnStore(data);
	});

	// Token: 0x04001044 RID: 4164
	private static readonly EventSystem.IntraObjectHandler<Storable> RefreshStorageTagsDelegate = new EventSystem.IntraObjectHandler<Storable>(delegate(Storable component, object data)
	{
		component.RefreshStorageTags(data);
	});
}
