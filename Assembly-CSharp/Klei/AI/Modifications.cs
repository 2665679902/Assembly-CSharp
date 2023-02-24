using System;
using System.Collections.Generic;
using System.IO;
using KSerialization;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D93 RID: 3475
	[SerializationConfig(MemberSerialization.OptIn)]
	public class Modifications<ModifierType, InstanceType> : ISaveLoadableDetails where ModifierType : Resource where InstanceType : ModifierInstance<ModifierType>
	{
		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x060069CD RID: 27085 RVA: 0x002927C3 File Offset: 0x002909C3
		public int Count
		{
			get
			{
				return this.ModifierList.Count;
			}
		}

		// Token: 0x060069CE RID: 27086 RVA: 0x002927D0 File Offset: 0x002909D0
		public IEnumerator<InstanceType> GetEnumerator()
		{
			return this.ModifierList.GetEnumerator();
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x060069CF RID: 27087 RVA: 0x002927E2 File Offset: 0x002909E2
		// (set) Token: 0x060069D0 RID: 27088 RVA: 0x002927EA File Offset: 0x002909EA
		public GameObject gameObject { get; private set; }

		// Token: 0x1700078A RID: 1930
		public InstanceType this[int idx]
		{
			get
			{
				return this.ModifierList[idx];
			}
		}

		// Token: 0x060069D2 RID: 27090 RVA: 0x00292801 File Offset: 0x00290A01
		public ComponentType GetComponent<ComponentType>()
		{
			return this.gameObject.GetComponent<ComponentType>();
		}

		// Token: 0x060069D3 RID: 27091 RVA: 0x0029280E File Offset: 0x00290A0E
		public void Trigger(GameHashes hash, object data = null)
		{
			this.gameObject.GetComponent<KPrefabID>().Trigger((int)hash, data);
		}

		// Token: 0x060069D4 RID: 27092 RVA: 0x00292824 File Offset: 0x00290A24
		public virtual InstanceType CreateInstance(ModifierType modifier)
		{
			return default(InstanceType);
		}

		// Token: 0x060069D5 RID: 27093 RVA: 0x0029283A File Offset: 0x00290A3A
		public Modifications(GameObject go, ResourceSet<ModifierType> resources = null)
		{
			this.resources = resources;
			this.gameObject = go;
		}

		// Token: 0x060069D6 RID: 27094 RVA: 0x0029285B File Offset: 0x00290A5B
		public virtual InstanceType Add(InstanceType instance)
		{
			this.ModifierList.Add(instance);
			return instance;
		}

		// Token: 0x060069D7 RID: 27095 RVA: 0x0029286C File Offset: 0x00290A6C
		public virtual void Remove(InstanceType instance)
		{
			for (int i = 0; i < this.ModifierList.Count; i++)
			{
				if (this.ModifierList[i] == instance)
				{
					this.ModifierList.RemoveAt(i);
					instance.OnCleanUp();
					return;
				}
			}
		}

		// Token: 0x060069D8 RID: 27096 RVA: 0x002928C0 File Offset: 0x00290AC0
		public bool Has(ModifierType modifier)
		{
			return this.Get(modifier) != null;
		}

		// Token: 0x060069D9 RID: 27097 RVA: 0x002928D4 File Offset: 0x00290AD4
		public InstanceType Get(ModifierType modifier)
		{
			foreach (InstanceType instanceType in this.ModifierList)
			{
				if (instanceType.modifier == modifier)
				{
					return instanceType;
				}
			}
			return default(InstanceType);
		}

		// Token: 0x060069DA RID: 27098 RVA: 0x00292948 File Offset: 0x00290B48
		public InstanceType Get(string id)
		{
			foreach (InstanceType instanceType in this.ModifierList)
			{
				if (instanceType.modifier.Id == id)
				{
					return instanceType;
				}
			}
			return default(InstanceType);
		}

		// Token: 0x060069DB RID: 27099 RVA: 0x002929C0 File Offset: 0x00290BC0
		public void Serialize(BinaryWriter writer)
		{
			writer.Write(this.ModifierList.Count);
			foreach (InstanceType instanceType in this.ModifierList)
			{
				writer.WriteKleiString(instanceType.modifier.Id);
				long position = writer.BaseStream.Position;
				writer.Write(0);
				long position2 = writer.BaseStream.Position;
				Serializer.SerializeTypeless(instanceType, writer);
				long position3 = writer.BaseStream.Position;
				long num = position3 - position2;
				writer.BaseStream.Position = position;
				writer.Write((int)num);
				writer.BaseStream.Position = position3;
			}
		}

		// Token: 0x060069DC RID: 27100 RVA: 0x00292AA0 File Offset: 0x00290CA0
		public void Deserialize(IReader reader)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadKleiString();
				int num2 = reader.ReadInt32();
				int position = reader.Position;
				InstanceType instanceType = this.Get(text);
				if (instanceType == null && this.resources != null)
				{
					ModifierType modifierType = this.resources.TryGet(text);
					if (modifierType != null)
					{
						instanceType = this.CreateInstance(modifierType);
					}
				}
				if (instanceType == null)
				{
					if (text != "Condition")
					{
						DebugUtil.LogWarningArgs(new object[]
						{
							this.gameObject.name,
							"Missing modifier: " + text
						});
					}
					reader.SkipBytes(num2);
				}
				else if (!(instanceType is ISaveLoadable))
				{
					reader.SkipBytes(num2);
				}
				else
				{
					Deserializer.DeserializeTypeless(instanceType, reader);
					if (reader.Position != position + num2)
					{
						DebugUtil.LogWarningArgs(new object[]
						{
							"Expected to be at offset",
							position + num2,
							"but was only at offset",
							reader.Position,
							". Skipping to catch up."
						});
						reader.SkipBytes(position + num2 - reader.Position);
					}
				}
			}
		}

		// Token: 0x04004FA0 RID: 20384
		public List<InstanceType> ModifierList = new List<InstanceType>();

		// Token: 0x04004FA2 RID: 20386
		private ResourceSet<ModifierType> resources;
	}
}
