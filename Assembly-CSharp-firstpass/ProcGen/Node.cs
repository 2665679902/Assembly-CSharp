using System;
using KSerialization;
using Satsuma;
using UnityEngine;

namespace ProcGen
{
	// Token: 0x020004B9 RID: 1209
	[SerializationConfig(MemberSerialization.OptIn)]
	public class Node
	{
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060033B7 RID: 13239 RVA: 0x000708FA File Offset: 0x0006EAFA
		// (set) Token: 0x060033B8 RID: 13240 RVA: 0x00070902 File Offset: 0x0006EB02
		internal Node node { get; private set; }

		// Token: 0x060033B9 RID: 13241 RVA: 0x0007090B File Offset: 0x0006EB0B
		public void SetNode(Node node)
		{
			global::Debug.Assert(!this.nodeSet, "Tried initializing a Node twice, that ain't gonna work.");
			this.node = node;
			this.nodeSet = true;
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060033BA RID: 13242 RVA: 0x0007092E File Offset: 0x0006EB2E
		// (set) Token: 0x060033BB RID: 13243 RVA: 0x00070936 File Offset: 0x0006EB36
		[Serialize]
		public string type { get; private set; }

		// Token: 0x060033BC RID: 13244 RVA: 0x0007093F File Offset: 0x0006EB3F
		public void SetType(string newtype)
		{
			this.type = newtype;
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x00070948 File Offset: 0x0006EB48
		public string GetSubworld()
		{
			foreach (Tag tag in this.tags)
			{
				if (tag.Name.Contains("subworlds/"))
				{
					return tag.Name;
				}
			}
			return "MISSING";
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x000709B4 File Offset: 0x0006EBB4
		public string GetBiome()
		{
			foreach (Tag tag in this.tags)
			{
				if (tag.Name.Contains("biomes/"))
				{
					return tag.Name;
				}
			}
			return "MISSING";
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x00070A20 File Offset: 0x0006EC20
		public string GetFeature()
		{
			foreach (Tag tag in this.tags)
			{
				if (tag.Name.Contains("features/"))
				{
					return tag.Name;
				}
			}
			return null;
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060033C0 RID: 13248 RVA: 0x00070A88 File Offset: 0x0006EC88
		// (set) Token: 0x060033C1 RID: 13249 RVA: 0x00070A90 File Offset: 0x0006EC90
		[Serialize]
		public Vector2 position { get; private set; }

		// Token: 0x060033C2 RID: 13250 RVA: 0x00070A99 File Offset: 0x0006EC99
		public void SetPosition(Vector2 newPos)
		{
			this.position = newPos;
		}

		// Token: 0x060033C3 RID: 13251 RVA: 0x00070AA2 File Offset: 0x0006ECA2
		public Node()
		{
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x00070AD6 File Offset: 0x0006ECD6
		public Node(string type)
		{
			this.type = type;
		}

		// Token: 0x060033C5 RID: 13253 RVA: 0x00070B14 File Offset: 0x0006ED14
		public Node(Node other)
		{
			this.position = other.position;
			this.node = other.node;
			this.type = other.type;
			this.tags = new TagSet(other.tags);
			this.featureSpecificTags = new TagSet(other.featureSpecificTags);
			this.biomeSpecificTags = new TagSet(other.biomeSpecificTags);
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x00070BAC File Offset: 0x0006EDAC
		public Node(Node node, string type, Vector2 position = default(Vector2))
		{
			this.node = node;
			this.type = type;
			this.position = position;
		}

		// Token: 0x04001221 RID: 4641
		private bool nodeSet;

		// Token: 0x04001225 RID: 4645
		[Serialize]
		public TagSet tags = new TagSet();

		// Token: 0x04001226 RID: 4646
		[Serialize]
		public Tag templateTag = Tag.Invalid;

		// Token: 0x04001227 RID: 4647
		[Serialize]
		public TagSet featureSpecificTags = new TagSet();

		// Token: 0x04001228 RID: 4648
		[Serialize]
		public TagSet biomeSpecificTags = new TagSet();
	}
}
