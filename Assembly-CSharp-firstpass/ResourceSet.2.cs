using System;
using System.Collections.Generic;
using System.Reflection;

// Token: 0x020000EB RID: 235
[Serializable]
public class ResourceSet<T> : ResourceSet where T : Resource
{
	// Token: 0x170000E3 RID: 227
	public T this[int idx]
	{
		get
		{
			return this.resources[idx];
		}
	}

	// Token: 0x170000E4 RID: 228
	// (get) Token: 0x06000868 RID: 2152 RVA: 0x00021ADD File Offset: 0x0001FCDD
	public override int Count
	{
		get
		{
			return this.resources.Count;
		}
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x00021AEA File Offset: 0x0001FCEA
	public override Resource GetResource(int idx)
	{
		return this.resources[idx];
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x00021AFD File Offset: 0x0001FCFD
	public ResourceSet()
	{
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x00021B10 File Offset: 0x0001FD10
	public ResourceSet(string id, ResourceSet parent)
		: base(id, parent)
	{
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x00021B28 File Offset: 0x0001FD28
	public override void Initialize()
	{
		foreach (T t in this.resources)
		{
			t.Initialize();
		}
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x00021B80 File Offset: 0x0001FD80
	public bool Exists(string id)
	{
		using (List<T>.Enumerator enumerator = this.resources.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Id == id)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x00021BE4 File Offset: 0x0001FDE4
	public T TryGet(string id)
	{
		foreach (T t in this.resources)
		{
			if (t.Id == id)
			{
				return t;
			}
		}
		return default(T);
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x00021C54 File Offset: 0x0001FE54
	public T TryGet(HashedString id)
	{
		foreach (T t in this.resources)
		{
			if (t.IdHash == id)
			{
				return t;
			}
		}
		return default(T);
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x00021CC4 File Offset: 0x0001FEC4
	public T Get(HashedString id)
	{
		foreach (T t in this.resources)
		{
			if (new HashedString(t.Id) == id)
			{
				return t;
			}
		}
		string text = "Could not find ";
		string text2 = typeof(T).ToString();
		string text3 = ": ";
		HashedString hashedString = id;
		Debug.LogError(text + text2 + text3 + hashedString.ToString());
		return default(T);
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x00021D6C File Offset: 0x0001FF6C
	public T Get(string id)
	{
		foreach (T t in this.resources)
		{
			if (t.Id == id)
			{
				return t;
			}
		}
		Debug.LogError("Could not find " + typeof(T).ToString() + ": " + id);
		return default(T);
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x00021E00 File Offset: 0x00020000
	public override void Remove(Resource resource)
	{
		T t = resource as T;
		if (t == null)
		{
			Debug.LogError("Resource type mismatch: " + resource.GetType().Name + " does not match " + typeof(T).Name);
		}
		this.resources.Remove(t);
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x00021E5C File Offset: 0x0002005C
	public override Resource Add(Resource resource)
	{
		T t = resource as T;
		if (t == null)
		{
			Debug.LogError("Resource type mismatch: " + resource.GetType().Name + " does not match " + typeof(T).Name);
		}
		this.Add(t);
		return resource;
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x00021EB4 File Offset: 0x000200B4
	public T Add(T resource)
	{
		if (resource == null)
		{
			Debug.LogError("Tried to add a null to the resource set");
			return default(T);
		}
		this.resources.Add(resource);
		return resource;
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x00021EEC File Offset: 0x000200EC
	public void ResolveReferences()
	{
		foreach (FieldInfo fieldInfo in base.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
		{
			if (fieldInfo.FieldType.IsSubclassOf(typeof(Resource)) && fieldInfo.GetValue(this) == null)
			{
				Resource resource = this.Get(fieldInfo.Name);
				if (resource != null)
				{
					fieldInfo.SetValue(this, resource);
				}
			}
		}
	}

	// Token: 0x0400063E RID: 1598
	public List<T> resources = new List<T>();
}
