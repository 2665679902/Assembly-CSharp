using System;
using System.Collections.Generic;
using System.Reflection;

// Token: 0x020000DA RID: 218
public abstract class MyAttributeManager<T> : IAttributeManager where T : class
{
	// Token: 0x06000821 RID: 2081 RVA: 0x00020F4F File Offset: 0x0001F14F
	public MyAttributeManager(Dictionary<Type, MethodInfo> attributeMap, Action<T> spawnFunc = null)
	{
		this.m_methodInfosByAttribute = attributeMap;
		this.m_spawnFunc = spawnFunc;
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x00020F7C File Offset: 0x0001F17C
	private void GetFieldDatas(List<MyAttributeManager<T>.FieldData> field_data_list, Type type)
	{
		foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
		{
			object[] customAttributes = fieldInfo.GetCustomAttributes(false);
			for (int j = 0; j < customAttributes.Length; j++)
			{
				Type type2 = customAttributes[j].GetType();
				if (this.IsFunctionAttribute(type2))
				{
					bool flag = true;
					using (List<MyAttributeManager<T>.FieldData>.Enumerator enumerator = field_data_list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.fieldInfo.Name == fieldInfo.Name)
							{
								flag = false;
								break;
							}
						}
					}
					if (flag)
					{
						field_data_list.Add(new MyAttributeManager<T>.FieldData
						{
							myAttributeType = type2,
							attrFns = this.GetAttrFns(fieldInfo.FieldType),
							fieldInfo = fieldInfo
						});
					}
				}
			}
		}
		Type baseType = type.BaseType;
		if (baseType != typeof(KMonoBehaviour) && baseType != typeof(object) && baseType != null)
		{
			this.GetFieldDatas(field_data_list, baseType);
		}
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x000210B4 File Offset: 0x0001F2B4
	private MyAttributeManager<T>.FieldData[] GetFields(Type type)
	{
		if (this.m_typeFieldInfos == null)
		{
			this.m_typeFieldInfos = new Dictionary<Type, MyAttributeManager<T>.FieldData[]>();
		}
		MyAttributeManager<T>.FieldData[] array = null;
		if (!this.m_typeFieldInfos.TryGetValue(type, out array))
		{
			List<MyAttributeManager<T>.FieldData> list = new List<MyAttributeManager<T>.FieldData>();
			this.GetFieldDatas(list, type);
			array = list.ToArray();
			this.m_typeFieldInfos[type] = array;
		}
		return array;
	}

	// Token: 0x06000824 RID: 2084 RVA: 0x0002110C File Offset: 0x0001F30C
	public void OnAwake(object obj, KMonoBehaviour cmp)
	{
		Type type = obj.GetType();
		foreach (MyAttributeManager<T>.FieldData fieldData in this.GetFields(type))
		{
			MyAttributeManager<T>.AttrFns attrFns = fieldData.attrFns;
			FieldInfo fieldInfo = fieldData.fieldInfo;
			if ((T)((object)fieldInfo.GetValue(obj)) == null)
			{
				T t = attrFns.GetFunction(fieldData.myAttributeType)(cmp, false);
				fieldInfo.SetValue(obj, t);
			}
		}
	}

	// Token: 0x06000825 RID: 2085 RVA: 0x00021184 File Offset: 0x0001F384
	public void OnStart(object obj, KMonoBehaviour cmp)
	{
		Type type = obj.GetType();
		foreach (MyAttributeManager<T>.FieldData fieldData in this.GetFields(type))
		{
			MyAttributeManager<T>.AttrFns attrFns = fieldData.attrFns;
			FieldInfo fieldInfo = fieldData.fieldInfo;
			T t = fieldInfo.GetValue(obj) as T;
			if (t != null)
			{
				if (this.m_spawnFunc != null)
				{
					this.m_spawnFunc(t);
				}
			}
			else
			{
				t = attrFns.GetFunction(fieldData.myAttributeType)(cmp, true);
				if (t != null && this.m_spawnFunc != null)
				{
					this.m_spawnFunc(t);
				}
				fieldInfo.SetValue(obj, t);
			}
		}
	}

	// Token: 0x06000826 RID: 2086 RVA: 0x00021240 File Offset: 0x0001F440
	private bool IsFunctionAttribute(Type attribute)
	{
		foreach (KeyValuePair<Type, MethodInfo> keyValuePair in this.m_methodInfosByAttribute)
		{
			if (attribute == keyValuePair.Key)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000827 RID: 2087 RVA: 0x000212A4 File Offset: 0x0001F4A4
	private MyAttributeManager<T>.AttrFns GetAttrFns(Type type)
	{
		MyAttributeManager<T>.AttrFns attrFns = null;
		if (!this.m_attrFns.TryGetValue(type, out attrFns))
		{
			attrFns = new MyAttributeManager<T>.AttrFns(type, this.m_methodInfosByAttribute);
			this.m_attrFns[type] = attrFns;
		}
		return attrFns;
	}

	// Token: 0x04000626 RID: 1574
	private Dictionary<Type, MyAttributeManager<T>.FieldData[]> m_typeFieldInfos;

	// Token: 0x04000627 RID: 1575
	private Action<T> m_spawnFunc;

	// Token: 0x04000628 RID: 1576
	private Dictionary<Type, MethodInfo> m_methodInfosByAttribute = new Dictionary<Type, MethodInfo>();

	// Token: 0x04000629 RID: 1577
	private Dictionary<Type, MyAttributeManager<T>.AttrFns> m_attrFns = new Dictionary<Type, MyAttributeManager<T>.AttrFns>();

	// Token: 0x020009F0 RID: 2544
	private class FieldData
	{
		// Token: 0x0400223D RID: 8765
		public Type myAttributeType;

		// Token: 0x0400223E RID: 8766
		public MyAttributeManager<T>.AttrFns attrFns;

		// Token: 0x0400223F RID: 8767
		public FieldInfo fieldInfo;
	}

	// Token: 0x020009F1 RID: 2545
	private class AttrFns
	{
		// Token: 0x060053E5 RID: 21477 RVA: 0x0009C72C File Offset: 0x0009A92C
		public AttrFns(Type type, Dictionary<Type, MethodInfo> methodInfosByAttribute)
		{
			foreach (KeyValuePair<Type, MethodInfo> keyValuePair in methodInfosByAttribute)
			{
				MethodInfo methodInfo = null;
				try
				{
					methodInfo = keyValuePair.Value.MakeGenericMethod(new Type[] { type });
				}
				catch (Exception ex)
				{
					Debug.LogError(string.Format("Exception for type {0}: {1}", type, ex));
				}
				Func<KMonoBehaviour, bool, T> func = (Func<KMonoBehaviour, bool, T>)Delegate.CreateDelegate(typeof(Func<KMonoBehaviour, bool, T>), methodInfo);
				this.m_fnsByAttribute[keyValuePair.Key] = func;
			}
		}

		// Token: 0x060053E6 RID: 21478 RVA: 0x0009C7EC File Offset: 0x0009A9EC
		public Func<KMonoBehaviour, bool, T> GetFunction(Type attribute)
		{
			return this.m_fnsByAttribute[attribute];
		}

		// Token: 0x04002240 RID: 8768
		private Dictionary<Type, Func<KMonoBehaviour, bool, T>> m_fnsByAttribute = new Dictionary<Type, Func<KMonoBehaviour, bool, T>>();
	}
}
