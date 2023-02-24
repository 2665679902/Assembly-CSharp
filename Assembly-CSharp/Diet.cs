using System;
using System.Collections.Generic;

// Token: 0x02000464 RID: 1124
public class Diet
{
	// Token: 0x170000E8 RID: 232
	// (get) Token: 0x060018F1 RID: 6385 RVA: 0x00085278 File Offset: 0x00083478
	// (set) Token: 0x060018F2 RID: 6386 RVA: 0x00085280 File Offset: 0x00083480
	public Diet.Info[] infos { get; private set; }

	// Token: 0x060018F3 RID: 6387 RVA: 0x0008528C File Offset: 0x0008348C
	public Diet(params Diet.Info[] infos)
	{
		this.infos = infos;
		this.consumedTags = new List<KeyValuePair<Tag, float>>();
		this.producedTags = new List<KeyValuePair<Tag, float>>();
		for (int i = 0; i < infos.Length; i++)
		{
			Diet.Info info = infos[i];
			if (info.eatsPlantsDirectly)
			{
				this.eatsPlantsDirectly = true;
			}
			using (HashSet<Tag>.Enumerator enumerator = info.consumedTags.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Tag tag = enumerator.Current;
					if (-1 == this.consumedTags.FindIndex((KeyValuePair<Tag, float> e) => e.Key == tag))
					{
						this.consumedTags.Add(new KeyValuePair<Tag, float>(tag, info.caloriesPerKg));
					}
					if (this.consumedTagToInfo.ContainsKey(tag))
					{
						string text = "Duplicate diet entry: ";
						Tag tag2 = tag;
						Debug.LogError(text + tag2.ToString());
					}
					this.consumedTagToInfo[tag] = info;
				}
			}
			if (info.producedElement != Tag.Invalid && -1 == this.producedTags.FindIndex((KeyValuePair<Tag, float> e) => e.Key == info.producedElement))
			{
				this.producedTags.Add(new KeyValuePair<Tag, float>(info.producedElement, info.producedConversionRate));
			}
		}
	}

	// Token: 0x060018F4 RID: 6388 RVA: 0x0008543C File Offset: 0x0008363C
	public Diet.Info GetDietInfo(Tag tag)
	{
		Diet.Info info = null;
		this.consumedTagToInfo.TryGetValue(tag, out info);
		return info;
	}

	// Token: 0x04000DF3 RID: 3571
	public List<KeyValuePair<Tag, float>> consumedTags;

	// Token: 0x04000DF4 RID: 3572
	public List<KeyValuePair<Tag, float>> producedTags;

	// Token: 0x04000DF5 RID: 3573
	public bool eatsPlantsDirectly;

	// Token: 0x04000DF6 RID: 3574
	private Dictionary<Tag, Diet.Info> consumedTagToInfo = new Dictionary<Tag, Diet.Info>();

	// Token: 0x0200108F RID: 4239
	public class Info
	{
		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06007378 RID: 29560 RVA: 0x002B0699 File Offset: 0x002AE899
		// (set) Token: 0x06007379 RID: 29561 RVA: 0x002B06A1 File Offset: 0x002AE8A1
		public HashSet<Tag> consumedTags { get; private set; }

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x0600737A RID: 29562 RVA: 0x002B06AA File Offset: 0x002AE8AA
		// (set) Token: 0x0600737B RID: 29563 RVA: 0x002B06B2 File Offset: 0x002AE8B2
		public Tag producedElement { get; private set; }

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x0600737C RID: 29564 RVA: 0x002B06BB File Offset: 0x002AE8BB
		// (set) Token: 0x0600737D RID: 29565 RVA: 0x002B06C3 File Offset: 0x002AE8C3
		public float caloriesPerKg { get; private set; }

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x0600737E RID: 29566 RVA: 0x002B06CC File Offset: 0x002AE8CC
		// (set) Token: 0x0600737F RID: 29567 RVA: 0x002B06D4 File Offset: 0x002AE8D4
		public float producedConversionRate { get; private set; }

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06007380 RID: 29568 RVA: 0x002B06DD File Offset: 0x002AE8DD
		// (set) Token: 0x06007381 RID: 29569 RVA: 0x002B06E5 File Offset: 0x002AE8E5
		public byte diseaseIdx { get; private set; }

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06007382 RID: 29570 RVA: 0x002B06EE File Offset: 0x002AE8EE
		// (set) Token: 0x06007383 RID: 29571 RVA: 0x002B06F6 File Offset: 0x002AE8F6
		public float diseasePerKgProduced { get; private set; }

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06007384 RID: 29572 RVA: 0x002B06FF File Offset: 0x002AE8FF
		// (set) Token: 0x06007385 RID: 29573 RVA: 0x002B0707 File Offset: 0x002AE907
		public bool produceSolidTile { get; private set; }

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06007386 RID: 29574 RVA: 0x002B0710 File Offset: 0x002AE910
		// (set) Token: 0x06007387 RID: 29575 RVA: 0x002B0718 File Offset: 0x002AE918
		public bool eatsPlantsDirectly { get; private set; }

		// Token: 0x06007388 RID: 29576 RVA: 0x002B0724 File Offset: 0x002AE924
		public Info(HashSet<Tag> consumed_tags, Tag produced_element, float calories_per_kg, float produced_conversion_rate = 1f, string disease_id = null, float disease_per_kg_produced = 0f, bool produce_solid_tile = false, bool eats_plants_directly = false)
		{
			this.consumedTags = consumed_tags;
			this.producedElement = produced_element;
			this.caloriesPerKg = calories_per_kg;
			this.producedConversionRate = produced_conversion_rate;
			if (!string.IsNullOrEmpty(disease_id))
			{
				this.diseaseIdx = Db.Get().Diseases.GetIndex(disease_id);
			}
			else
			{
				this.diseaseIdx = byte.MaxValue;
			}
			this.produceSolidTile = produce_solid_tile;
			this.eatsPlantsDirectly = eats_plants_directly;
		}

		// Token: 0x06007389 RID: 29577 RVA: 0x002B0796 File Offset: 0x002AE996
		public bool IsMatch(Tag tag)
		{
			return this.consumedTags.Contains(tag);
		}

		// Token: 0x0600738A RID: 29578 RVA: 0x002B07A4 File Offset: 0x002AE9A4
		public bool IsMatch(HashSet<Tag> tags)
		{
			if (tags.Count < this.consumedTags.Count)
			{
				foreach (Tag tag in tags)
				{
					if (this.consumedTags.Contains(tag))
					{
						return true;
					}
				}
				return false;
			}
			foreach (Tag tag2 in this.consumedTags)
			{
				if (tags.Contains(tag2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600738B RID: 29579 RVA: 0x002B0860 File Offset: 0x002AEA60
		public float ConvertCaloriesToConsumptionMass(float calories)
		{
			return calories / this.caloriesPerKg;
		}

		// Token: 0x0600738C RID: 29580 RVA: 0x002B086A File Offset: 0x002AEA6A
		public float ConvertConsumptionMassToCalories(float mass)
		{
			return this.caloriesPerKg * mass;
		}

		// Token: 0x0600738D RID: 29581 RVA: 0x002B0874 File Offset: 0x002AEA74
		public float ConvertConsumptionMassToProducedMass(float consumed_mass)
		{
			return consumed_mass * this.producedConversionRate;
		}
	}
}
