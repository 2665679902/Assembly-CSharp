using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000B1C RID: 2844
[SerializationConfig(MemberSerialization.OptIn)]
public class MessageTarget : ISaveLoadable
{
	// Token: 0x060057B1 RID: 22449 RVA: 0x001FCFC4 File Offset: 0x001FB1C4
	public MessageTarget(KPrefabID prefab_id)
	{
		this.prefabId.Set(prefab_id);
		this.position = prefab_id.transform.GetPosition();
		this.name = "Unknown";
		KSelectable component = prefab_id.GetComponent<KSelectable>();
		if (component != null)
		{
			this.name = component.GetName();
		}
		prefab_id.Subscribe(-1940207677, new Action<object>(this.OnAbsorbedBy));
	}

	// Token: 0x060057B2 RID: 22450 RVA: 0x001FD03E File Offset: 0x001FB23E
	public Vector3 GetPosition()
	{
		if (this.prefabId.Get() != null)
		{
			return this.prefabId.Get().transform.GetPosition();
		}
		return this.position;
	}

	// Token: 0x060057B3 RID: 22451 RVA: 0x001FD06F File Offset: 0x001FB26F
	public KSelectable GetSelectable()
	{
		if (this.prefabId.Get() != null)
		{
			return this.prefabId.Get().transform.GetComponent<KSelectable>();
		}
		return null;
	}

	// Token: 0x060057B4 RID: 22452 RVA: 0x001FD09B File Offset: 0x001FB29B
	public string GetName()
	{
		return this.name;
	}

	// Token: 0x060057B5 RID: 22453 RVA: 0x001FD0A4 File Offset: 0x001FB2A4
	private void OnAbsorbedBy(object data)
	{
		if (this.prefabId.Get() != null)
		{
			this.prefabId.Get().Unsubscribe(-1940207677, new Action<object>(this.OnAbsorbedBy));
		}
		KPrefabID component = ((GameObject)data).GetComponent<KPrefabID>();
		component.Subscribe(-1940207677, new Action<object>(this.OnAbsorbedBy));
		this.prefabId.Set(component);
	}

	// Token: 0x060057B6 RID: 22454 RVA: 0x001FD118 File Offset: 0x001FB318
	public void OnCleanUp()
	{
		if (this.prefabId.Get() != null)
		{
			this.prefabId.Get().Unsubscribe(-1940207677, new Action<object>(this.OnAbsorbedBy));
			this.prefabId.Set(null);
		}
	}

	// Token: 0x04003B61 RID: 15201
	[Serialize]
	private Ref<KPrefabID> prefabId = new Ref<KPrefabID>();

	// Token: 0x04003B62 RID: 15202
	[Serialize]
	private Vector3 position;

	// Token: 0x04003B63 RID: 15203
	[Serialize]
	private string name;
}
