using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using KSerialization;
using UnityEngine;

// Token: 0x020000E5 RID: 229
[SerializationConfig(MemberSerialization.OptIn)]
[DebuggerDisplay("{id}")]
public class Ref<ReferenceType> : ISaveLoadable where ReferenceType : KMonoBehaviour
{
	// Token: 0x06000845 RID: 2117 RVA: 0x0002164D File Offset: 0x0001F84D
	public Ref(ReferenceType obj)
	{
		this.Set(obj);
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x00021663 File Offset: 0x0001F863
	public Ref()
	{
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x00021672 File Offset: 0x0001F872
	private void UpdateID()
	{
		if (this.Get())
		{
			this.id = this.obj.GetComponent<KPrefabID>().InstanceID;
			return;
		}
		this.id = -1;
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x000216A9 File Offset: 0x0001F8A9
	[OnSerializing]
	public void OnSerializing()
	{
		this.UpdateID();
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x000216B1 File Offset: 0x0001F8B1
	public int GetId()
	{
		this.UpdateID();
		return this.id;
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x000216C0 File Offset: 0x0001F8C0
	public ComponentType Get<ComponentType>() where ComponentType : MonoBehaviour
	{
		ReferenceType referenceType = this.Get();
		if (referenceType == null)
		{
			return default(ComponentType);
		}
		return referenceType.GetComponent<ComponentType>();
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x000216F8 File Offset: 0x0001F8F8
	public ReferenceType Get()
	{
		if (this.obj == null && this.id != -1)
		{
			KPrefabID instance = KPrefabIDTracker.Get().GetInstance(this.id);
			if (instance != null)
			{
				this.obj = instance.GetComponent<ReferenceType>();
				if (this.obj == null)
				{
					this.id = -1;
					global::Debug.LogWarning("Missing " + typeof(ReferenceType).Name + " reference: " + this.id.ToString());
				}
			}
			else
			{
				global::Debug.LogWarning("Missing KPrefabID reference: " + this.id.ToString());
				this.id = -1;
			}
		}
		return this.obj;
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x000217BE File Offset: 0x0001F9BE
	public void Set(ReferenceType obj)
	{
		if (obj == null)
		{
			this.id = -1;
		}
		else
		{
			this.id = obj.GetComponent<KPrefabID>().InstanceID;
		}
		this.obj = obj;
	}

	// Token: 0x04000635 RID: 1589
	[Serialize]
	private int id = -1;

	// Token: 0x04000636 RID: 1590
	private ReferenceType obj;
}
