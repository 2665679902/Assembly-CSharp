using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Token: 0x02000811 RID: 2065
public class MemorySnapshot
{
	// Token: 0x06003BDE RID: 15326 RVA: 0x0014BC54 File Offset: 0x00149E54
	public static MemorySnapshot.TypeData GetTypeData(Type type, Dictionary<int, MemorySnapshot.TypeData> types)
	{
		int hashCode = type.GetHashCode();
		MemorySnapshot.TypeData typeData = null;
		if (!types.TryGetValue(hashCode, out typeData))
		{
			typeData = new MemorySnapshot.TypeData(type);
			types[hashCode] = typeData;
		}
		return typeData;
	}

	// Token: 0x06003BDF RID: 15327 RVA: 0x0014BC88 File Offset: 0x00149E88
	public static void IncrementFieldCount(Dictionary<int, MemorySnapshot.FieldCount> field_counts, string name)
	{
		int hashCode = name.GetHashCode();
		MemorySnapshot.FieldCount fieldCount = null;
		if (!field_counts.TryGetValue(hashCode, out fieldCount))
		{
			fieldCount = new MemorySnapshot.FieldCount();
			fieldCount.name = name;
			field_counts[hashCode] = fieldCount;
		}
		fieldCount.count++;
	}

	// Token: 0x06003BE0 RID: 15328 RVA: 0x0014BCCC File Offset: 0x00149ECC
	private void CountReference(MemorySnapshot.ReferenceArgs refArgs)
	{
		if (MemorySnapshot.ShouldExclude(refArgs.reference_type))
		{
			return;
		}
		if (refArgs.reference_type == MemorySnapshot.detailType)
		{
			string text;
			if (refArgs.lineage.obj as UnityEngine.Object != null)
			{
				text = "\"" + ((UnityEngine.Object)refArgs.lineage.obj).name;
			}
			else
			{
				text = "\"" + MemorySnapshot.detailTypeStr;
			}
			if (refArgs.lineage.parent0 != null)
			{
				text += "\",\"";
				text += refArgs.lineage.parent0.ToString();
			}
			if (refArgs.lineage.parent1 != null)
			{
				text = text + "\",\"" + refArgs.lineage.parent1.ToString();
			}
			if (refArgs.lineage.parent2 != null)
			{
				text = text + "\",\"" + refArgs.lineage.parent2.ToString();
			}
			if (refArgs.lineage.parent3 != null)
			{
				text = text + "\",\"" + refArgs.lineage.parent3.ToString();
			}
			if (refArgs.lineage.parent4 != null)
			{
				text = text + "\",\"" + refArgs.lineage.parent4.ToString();
			}
			text += "\"\n";
			MemorySnapshot.DetailInfo detailInfo;
			this.detailTypeCount.TryGetValue(text, out detailInfo);
			detailInfo.count++;
			if (typeof(Array).IsAssignableFrom(refArgs.reference_type) && refArgs.lineage.obj != null)
			{
				Array array = refArgs.lineage.obj as Array;
				detailInfo.numArrayEntries += ((array != null) ? array.Length : 0);
			}
			this.detailTypeCount[text] = detailInfo;
		}
		if (refArgs.reference_type.IsClass)
		{
			MemorySnapshot.GetTypeData(refArgs.reference_type, this.types).refCount++;
			MemorySnapshot.IncrementFieldCount(this.fieldCounts, refArgs.field_name);
		}
		if (refArgs.lineage.obj == null)
		{
			return;
		}
		try
		{
			if (refArgs.lineage.obj.GetType().IsClass && !this.walked.Add(refArgs.lineage.obj))
			{
				return;
			}
		}
		catch
		{
			return;
		}
		MemorySnapshot.TypeData typeData = MemorySnapshot.GetTypeData(refArgs.lineage.obj.GetType(), this.types);
		if (typeData.type.IsClass)
		{
			typeData.instanceCount++;
			if (typeof(Array).IsAssignableFrom(typeData.type))
			{
				Array array2 = refArgs.lineage.obj as Array;
				typeData.numArrayEntries += ((array2 != null) ? array2.Length : 0);
			}
			MemorySnapshot.HierarchyNode hierarchyNode = new MemorySnapshot.HierarchyNode(refArgs.lineage.parent0, refArgs.lineage.parent1, refArgs.lineage.parent2, refArgs.lineage.parent3, refArgs.lineage.parent4);
			int num = 0;
			typeData.hierarchies.TryGetValue(hierarchyNode, out num);
			typeData.hierarchies[hierarchyNode] = num + 1;
		}
		foreach (FieldInfo fieldInfo in typeData.fields)
		{
			this.fieldsToProcess.Add(new MemorySnapshot.FieldArgs(fieldInfo, new MemorySnapshot.Lineage(refArgs.lineage.obj, refArgs.lineage.parent3, refArgs.lineage.parent2, refArgs.lineage.parent1, refArgs.lineage.parent0, fieldInfo.DeclaringType)));
		}
		ICollection collection = refArgs.lineage.obj as ICollection;
		if (collection != null)
		{
			Type type = typeof(object);
			if (collection.GetType().GetElementType() != null)
			{
				type = collection.GetType().GetElementType();
			}
			else if (collection.GetType().GetGenericArguments().Length != 0)
			{
				type = collection.GetType().GetGenericArguments()[0];
			}
			if (!MemorySnapshot.ShouldExclude(type))
			{
				foreach (object obj in collection)
				{
					this.refsToProcess.Add(new MemorySnapshot.ReferenceArgs(type, refArgs.field_name + ".Item", new MemorySnapshot.Lineage(obj, refArgs.lineage.parent3, refArgs.lineage.parent2, refArgs.lineage.parent1, refArgs.lineage.parent0, collection.GetType())));
				}
			}
		}
	}

	// Token: 0x06003BE1 RID: 15329 RVA: 0x0014C1D4 File Offset: 0x0014A3D4
	private void CountField(MemorySnapshot.FieldArgs fieldArgs)
	{
		if (MemorySnapshot.ShouldExclude(fieldArgs.field.FieldType))
		{
			return;
		}
		object obj = null;
		try
		{
			if (!fieldArgs.field.FieldType.Name.Contains("*"))
			{
				obj = fieldArgs.field.GetValue(fieldArgs.lineage.obj);
			}
		}
		catch
		{
			obj = null;
		}
		string text = fieldArgs.field.DeclaringType.ToString() + "." + fieldArgs.field.Name;
		this.refsToProcess.Add(new MemorySnapshot.ReferenceArgs(fieldArgs.field.FieldType, text, new MemorySnapshot.Lineage(obj, fieldArgs.lineage.parent3, fieldArgs.lineage.parent2, fieldArgs.lineage.parent1, fieldArgs.lineage.parent0, fieldArgs.field.DeclaringType)));
	}

	// Token: 0x06003BE2 RID: 15330 RVA: 0x0014C2C0 File Offset: 0x0014A4C0
	private static bool ShouldExclude(Type type)
	{
		return type.IsPrimitive || type.IsEnum || type == typeof(MemorySnapshot);
	}

	// Token: 0x06003BE3 RID: 15331 RVA: 0x0014C2E4 File Offset: 0x0014A4E4
	private void CountAll()
	{
		while (this.refsToProcess.Count > 0 || this.fieldsToProcess.Count > 0)
		{
			while (this.fieldsToProcess.Count > 0)
			{
				MemorySnapshot.FieldArgs fieldArgs = this.fieldsToProcess[this.fieldsToProcess.Count - 1];
				this.fieldsToProcess.RemoveAt(this.fieldsToProcess.Count - 1);
				this.CountField(fieldArgs);
			}
			while (this.refsToProcess.Count > 0)
			{
				MemorySnapshot.ReferenceArgs referenceArgs = this.refsToProcess[this.refsToProcess.Count - 1];
				this.refsToProcess.RemoveAt(this.refsToProcess.Count - 1);
				this.CountReference(referenceArgs);
			}
		}
	}

	// Token: 0x06003BE4 RID: 15332 RVA: 0x0014C3A0 File Offset: 0x0014A5A0
	public MemorySnapshot()
	{
		MemorySnapshot.Lineage lineage = new MemorySnapshot.Lineage(null, null, null, null, null, null);
		foreach (Type type in App.GetCurrentDomainTypes())
		{
			foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
			{
				if (fieldInfo.IsStatic)
				{
					this.statics.Add(fieldInfo);
					lineage.parent0 = fieldInfo.DeclaringType;
					this.fieldsToProcess.Add(new MemorySnapshot.FieldArgs(fieldInfo, lineage));
				}
			}
		}
		this.CountAll();
		foreach (UnityEngine.Object @object in Resources.FindObjectsOfTypeAll(typeof(UnityEngine.Object)))
		{
			lineage.obj = @object;
			lineage.parent0 = @object.GetType();
			this.refsToProcess.Add(new MemorySnapshot.ReferenceArgs(@object.GetType(), "Object." + @object.name, lineage));
		}
		this.CountAll();
	}

	// Token: 0x06003BE5 RID: 15333 RVA: 0x0014C510 File Offset: 0x0014A710
	public void WriteTypeDetails(MemorySnapshot compare)
	{
		List<KeyValuePair<string, MemorySnapshot.DetailInfo>> list = null;
		if (compare != null)
		{
			list = compare.detailTypeCount.ToList<KeyValuePair<string, MemorySnapshot.DetailInfo>>();
		}
		List<KeyValuePair<string, MemorySnapshot.DetailInfo>> list2 = this.detailTypeCount.ToList<KeyValuePair<string, MemorySnapshot.DetailInfo>>();
		list2.Sort((KeyValuePair<string, MemorySnapshot.DetailInfo> x, KeyValuePair<string, MemorySnapshot.DetailInfo> y) => y.Value.count - x.Value.count);
		using (StreamWriter streamWriter = new StreamWriter(GarbageProfiler.GetFileName("type_details_" + MemorySnapshot.detailTypeStr)))
		{
			streamWriter.WriteLine("Delta,Count,NumArrayEntries,Type");
			foreach (KeyValuePair<string, MemorySnapshot.DetailInfo> keyValuePair in list2)
			{
				int num = keyValuePair.Value.count;
				if (list != null)
				{
					foreach (KeyValuePair<string, MemorySnapshot.DetailInfo> keyValuePair2 in list)
					{
						if (keyValuePair2.Key == keyValuePair.Key)
						{
							num -= keyValuePair2.Value.count;
							break;
						}
					}
				}
				TextWriter textWriter = streamWriter;
				string[] array = new string[7];
				array[0] = num.ToString();
				array[1] = ",";
				int num2 = 2;
				MemorySnapshot.DetailInfo detailInfo = keyValuePair.Value;
				array[num2] = detailInfo.count.ToString();
				array[3] = ",";
				int num3 = 4;
				detailInfo = keyValuePair.Value;
				array[num3] = detailInfo.numArrayEntries.ToString();
				array[5] = ",";
				array[6] = keyValuePair.Key;
				textWriter.Write(string.Concat(array));
			}
		}
	}

	// Token: 0x040026FE RID: 9982
	public Dictionary<int, MemorySnapshot.TypeData> types = new Dictionary<int, MemorySnapshot.TypeData>();

	// Token: 0x040026FF RID: 9983
	public Dictionary<int, MemorySnapshot.FieldCount> fieldCounts = new Dictionary<int, MemorySnapshot.FieldCount>();

	// Token: 0x04002700 RID: 9984
	public HashSet<object> walked = new HashSet<object>();

	// Token: 0x04002701 RID: 9985
	public List<FieldInfo> statics = new List<FieldInfo>();

	// Token: 0x04002702 RID: 9986
	public Dictionary<string, MemorySnapshot.DetailInfo> detailTypeCount = new Dictionary<string, MemorySnapshot.DetailInfo>();

	// Token: 0x04002703 RID: 9987
	private static readonly Type detailType = typeof(byte[]);

	// Token: 0x04002704 RID: 9988
	private static readonly string detailTypeStr = MemorySnapshot.detailType.ToString();

	// Token: 0x04002705 RID: 9989
	private List<MemorySnapshot.FieldArgs> fieldsToProcess = new List<MemorySnapshot.FieldArgs>();

	// Token: 0x04002706 RID: 9990
	private List<MemorySnapshot.ReferenceArgs> refsToProcess = new List<MemorySnapshot.ReferenceArgs>();

	// Token: 0x02001565 RID: 5477
	public struct HierarchyNode
	{
		// Token: 0x060083CC RID: 33740 RVA: 0x002E85C0 File Offset: 0x002E67C0
		public HierarchyNode(Type parent_0, Type parent_1, Type parent_2, Type parent_3, Type parent_4)
		{
			this.parent0 = parent_0;
			this.parent1 = parent_1;
			this.parent2 = parent_2;
			this.parent3 = parent_3;
			this.parent4 = parent_4;
		}

		// Token: 0x060083CD RID: 33741 RVA: 0x002E85E8 File Offset: 0x002E67E8
		public bool Equals(MemorySnapshot.HierarchyNode a, MemorySnapshot.HierarchyNode b)
		{
			return a.parent0 == b.parent0 && a.parent1 == b.parent1 && a.parent2 == b.parent2 && a.parent3 == b.parent3 && a.parent4 == b.parent4;
		}

		// Token: 0x060083CE RID: 33742 RVA: 0x002E8654 File Offset: 0x002E6854
		public override int GetHashCode()
		{
			int num = 0;
			if (this.parent0 != null)
			{
				num += this.parent0.GetHashCode();
			}
			if (this.parent1 != null)
			{
				num += this.parent1.GetHashCode();
			}
			if (this.parent2 != null)
			{
				num += this.parent2.GetHashCode();
			}
			if (this.parent3 != null)
			{
				num += this.parent3.GetHashCode();
			}
			if (this.parent4 != null)
			{
				num += this.parent4.GetHashCode();
			}
			return num;
		}

		// Token: 0x060083CF RID: 33743 RVA: 0x002E86F0 File Offset: 0x002E68F0
		public override string ToString()
		{
			if (this.parent4 != null)
			{
				return string.Concat(new string[]
				{
					this.parent4.ToString(),
					"--",
					this.parent3.ToString(),
					"--",
					this.parent2.ToString(),
					"--",
					this.parent1.ToString(),
					"--",
					this.parent0.ToString()
				});
			}
			if (this.parent3 != null)
			{
				return string.Concat(new string[]
				{
					this.parent3.ToString(),
					"--",
					this.parent2.ToString(),
					"--",
					this.parent1.ToString(),
					"--",
					this.parent0.ToString()
				});
			}
			if (this.parent2 != null)
			{
				return string.Concat(new string[]
				{
					this.parent2.ToString(),
					"--",
					this.parent1.ToString(),
					"--",
					this.parent0.ToString()
				});
			}
			if (this.parent1 != null)
			{
				return this.parent1.ToString() + "--" + this.parent0.ToString();
			}
			return this.parent0.ToString();
		}

		// Token: 0x04006696 RID: 26262
		public Type parent0;

		// Token: 0x04006697 RID: 26263
		public Type parent1;

		// Token: 0x04006698 RID: 26264
		public Type parent2;

		// Token: 0x04006699 RID: 26265
		public Type parent3;

		// Token: 0x0400669A RID: 26266
		public Type parent4;
	}

	// Token: 0x02001566 RID: 5478
	public class FieldCount
	{
		// Token: 0x0400669B RID: 26267
		public string name;

		// Token: 0x0400669C RID: 26268
		public int count;
	}

	// Token: 0x02001567 RID: 5479
	public class TypeData
	{
		// Token: 0x060083D1 RID: 33745 RVA: 0x002E8880 File Offset: 0x002E6A80
		public TypeData(Type type)
		{
			this.type = type;
			this.fields = new List<FieldInfo>();
			this.instanceCount = 0;
			this.refCount = 0;
			this.numArrayEntries = 0;
			foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
			{
				if (!fieldInfo.IsStatic && !MemorySnapshot.ShouldExclude(fieldInfo.FieldType))
				{
					this.fields.Add(fieldInfo);
				}
			}
		}

		// Token: 0x0400669D RID: 26269
		public Dictionary<MemorySnapshot.HierarchyNode, int> hierarchies = new Dictionary<MemorySnapshot.HierarchyNode, int>();

		// Token: 0x0400669E RID: 26270
		public Type type;

		// Token: 0x0400669F RID: 26271
		public List<FieldInfo> fields;

		// Token: 0x040066A0 RID: 26272
		public int instanceCount;

		// Token: 0x040066A1 RID: 26273
		public int refCount;

		// Token: 0x040066A2 RID: 26274
		public int numArrayEntries;
	}

	// Token: 0x02001568 RID: 5480
	public struct DetailInfo
	{
		// Token: 0x040066A3 RID: 26275
		public int count;

		// Token: 0x040066A4 RID: 26276
		public int numArrayEntries;
	}

	// Token: 0x02001569 RID: 5481
	private struct Lineage
	{
		// Token: 0x060083D2 RID: 33746 RVA: 0x002E8901 File Offset: 0x002E6B01
		public Lineage(object obj, Type parent4, Type parent3, Type parent2, Type parent1, Type parent0)
		{
			this.obj = obj;
			this.parent0 = parent0;
			this.parent1 = parent1;
			this.parent2 = parent2;
			this.parent3 = parent3;
			this.parent4 = parent4;
		}

		// Token: 0x040066A5 RID: 26277
		public object obj;

		// Token: 0x040066A6 RID: 26278
		public Type parent0;

		// Token: 0x040066A7 RID: 26279
		public Type parent1;

		// Token: 0x040066A8 RID: 26280
		public Type parent2;

		// Token: 0x040066A9 RID: 26281
		public Type parent3;

		// Token: 0x040066AA RID: 26282
		public Type parent4;
	}

	// Token: 0x0200156A RID: 5482
	private struct ReferenceArgs
	{
		// Token: 0x060083D3 RID: 33747 RVA: 0x002E8930 File Offset: 0x002E6B30
		public ReferenceArgs(Type reference_type, string field_name, MemorySnapshot.Lineage lineage)
		{
			this.reference_type = reference_type;
			this.lineage = lineage;
			this.field_name = field_name;
		}

		// Token: 0x040066AB RID: 26283
		public Type reference_type;

		// Token: 0x040066AC RID: 26284
		public string field_name;

		// Token: 0x040066AD RID: 26285
		public MemorySnapshot.Lineage lineage;
	}

	// Token: 0x0200156B RID: 5483
	private struct FieldArgs
	{
		// Token: 0x060083D4 RID: 33748 RVA: 0x002E8947 File Offset: 0x002E6B47
		public FieldArgs(FieldInfo field, MemorySnapshot.Lineage lineage)
		{
			this.field = field;
			this.lineage = lineage;
		}

		// Token: 0x040066AE RID: 26286
		public FieldInfo field;

		// Token: 0x040066AF RID: 26287
		public MemorySnapshot.Lineage lineage;
	}
}
