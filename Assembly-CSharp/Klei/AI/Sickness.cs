using System;
using System.Collections.Generic;
using System.Diagnostics;
using STRINGS;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D76 RID: 3446
	[DebuggerDisplay("{base.Id}")]
	public abstract class Sickness : Resource
	{
		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x0600693E RID: 26942 RVA: 0x0028EEB5 File Offset: 0x0028D0B5
		public new string Name
		{
			get
			{
				return Strings.Get(this.name);
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x0600693F RID: 26943 RVA: 0x0028EEC7 File Offset: 0x0028D0C7
		public float SicknessDuration
		{
			get
			{
				return this.sicknessDuration;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06006940 RID: 26944 RVA: 0x0028EECF File Offset: 0x0028D0CF
		public StringKey DescriptiveSymptoms
		{
			get
			{
				return this.descriptiveSymptoms;
			}
		}

		// Token: 0x06006941 RID: 26945 RVA: 0x0028EED8 File Offset: 0x0028D0D8
		public Sickness(string id, Sickness.SicknessType type, Sickness.Severity severity, float immune_attack_strength, List<Sickness.InfectionVector> infection_vectors, float sickness_duration, string recovery_effect = null)
			: base(id, null, null)
		{
			this.name = new StringKey("STRINGS.DUPLICANTS.DISEASES." + id.ToUpper() + ".NAME");
			this.id = id;
			this.sicknessType = type;
			this.severity = severity;
			this.infectionVectors = infection_vectors;
			this.sicknessDuration = sickness_duration;
			this.recoveryEffect = recovery_effect;
			this.descriptiveSymptoms = new StringKey("STRINGS.DUPLICANTS.DISEASES." + id.ToUpper() + ".DESCRIPTIVE_SYMPTOMS");
			this.cureSpeedBase = new Attribute(id + "CureSpeed", false, Attribute.Display.Normal, false, 0f, null, null, null, null);
			this.cureSpeedBase.BaseValue = 1f;
			this.cureSpeedBase.SetFormatter(new ToPercentAttributeFormatter(1f, GameUtil.TimeSlice.None));
			Db.Get().Attributes.Add(this.cureSpeedBase);
		}

		// Token: 0x06006942 RID: 26946 RVA: 0x0028EFD4 File Offset: 0x0028D1D4
		public object[] Infect(GameObject go, SicknessInstance diseaseInstance, SicknessExposureInfo exposure_info)
		{
			object[] array = new object[this.components.Count];
			for (int i = 0; i < this.components.Count; i++)
			{
				array[i] = this.components[i].OnInfect(go, diseaseInstance);
			}
			return array;
		}

		// Token: 0x06006943 RID: 26947 RVA: 0x0028F020 File Offset: 0x0028D220
		public void Cure(GameObject go, object[] componentData)
		{
			for (int i = 0; i < this.components.Count; i++)
			{
				this.components[i].OnCure(go, componentData[i]);
			}
		}

		// Token: 0x06006944 RID: 26948 RVA: 0x0028F058 File Offset: 0x0028D258
		public List<Descriptor> GetSymptoms()
		{
			List<Descriptor> list = new List<Descriptor>();
			for (int i = 0; i < this.components.Count; i++)
			{
				List<Descriptor> symptoms = this.components[i].GetSymptoms();
				if (symptoms != null)
				{
					list.AddRange(symptoms);
				}
			}
			if (this.fatalityDuration > 0f)
			{
				list.Add(new Descriptor(string.Format(DUPLICANTS.DISEASES.DEATH_SYMPTOM, GameUtil.GetFormattedCycles(this.fatalityDuration, "F1", false)), string.Format(DUPLICANTS.DISEASES.DEATH_SYMPTOM_TOOLTIP, GameUtil.GetFormattedCycles(this.fatalityDuration, "F1", false)), Descriptor.DescriptorType.SymptomAidable, false));
			}
			return list;
		}

		// Token: 0x06006945 RID: 26949 RVA: 0x0028F0F8 File Offset: 0x0028D2F8
		protected void AddSicknessComponent(Sickness.SicknessComponent cmp)
		{
			this.components.Add(cmp);
		}

		// Token: 0x06006946 RID: 26950 RVA: 0x0028F108 File Offset: 0x0028D308
		public T GetSicknessComponent<T>() where T : Sickness.SicknessComponent
		{
			for (int i = 0; i < this.components.Count; i++)
			{
				if (this.components[i] is T)
				{
					return this.components[i] as T;
				}
			}
			return default(T);
		}

		// Token: 0x06006947 RID: 26951 RVA: 0x0028F15E File Offset: 0x0028D35E
		public virtual List<Descriptor> GetSicknessSourceDescriptors()
		{
			return new List<Descriptor>();
		}

		// Token: 0x06006948 RID: 26952 RVA: 0x0028F168 File Offset: 0x0028D368
		public List<Descriptor> GetQualitativeDescriptors()
		{
			List<Descriptor> list = new List<Descriptor>();
			using (List<Sickness.InfectionVector>.Enumerator enumerator = this.infectionVectors.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					switch (enumerator.Current)
					{
					case Sickness.InfectionVector.Contact:
						list.Add(new Descriptor(DUPLICANTS.DISEASES.DESCRIPTORS.INFO.SKINBORNE, DUPLICANTS.DISEASES.DESCRIPTORS.INFO.SKINBORNE_TOOLTIP, Descriptor.DescriptorType.Information, false));
						break;
					case Sickness.InfectionVector.Digestion:
						list.Add(new Descriptor(DUPLICANTS.DISEASES.DESCRIPTORS.INFO.FOODBORNE, DUPLICANTS.DISEASES.DESCRIPTORS.INFO.FOODBORNE_TOOLTIP, Descriptor.DescriptorType.Information, false));
						break;
					case Sickness.InfectionVector.Inhalation:
						list.Add(new Descriptor(DUPLICANTS.DISEASES.DESCRIPTORS.INFO.AIRBORNE, DUPLICANTS.DISEASES.DESCRIPTORS.INFO.AIRBORNE_TOOLTIP, Descriptor.DescriptorType.Information, false));
						break;
					case Sickness.InfectionVector.Exposure:
						list.Add(new Descriptor(DUPLICANTS.DISEASES.DESCRIPTORS.INFO.SUNBORNE, DUPLICANTS.DISEASES.DESCRIPTORS.INFO.SUNBORNE_TOOLTIP, Descriptor.DescriptorType.Information, false));
						break;
					}
				}
			}
			list.Add(new Descriptor(Strings.Get(this.descriptiveSymptoms), "", Descriptor.DescriptorType.Information, false));
			return list;
		}

		// Token: 0x04004F28 RID: 20264
		private StringKey name;

		// Token: 0x04004F29 RID: 20265
		private StringKey descriptiveSymptoms;

		// Token: 0x04004F2A RID: 20266
		private float sicknessDuration = 600f;

		// Token: 0x04004F2B RID: 20267
		public float fatalityDuration;

		// Token: 0x04004F2C RID: 20268
		public HashedString id;

		// Token: 0x04004F2D RID: 20269
		public Sickness.SicknessType sicknessType;

		// Token: 0x04004F2E RID: 20270
		public Sickness.Severity severity;

		// Token: 0x04004F2F RID: 20271
		public string recoveryEffect;

		// Token: 0x04004F30 RID: 20272
		public List<Sickness.InfectionVector> infectionVectors;

		// Token: 0x04004F31 RID: 20273
		private List<Sickness.SicknessComponent> components = new List<Sickness.SicknessComponent>();

		// Token: 0x04004F32 RID: 20274
		public Amount amount;

		// Token: 0x04004F33 RID: 20275
		public Attribute amountDeltaAttribute;

		// Token: 0x04004F34 RID: 20276
		public Attribute cureSpeedBase;

		// Token: 0x02001E46 RID: 7750
		public abstract class SicknessComponent
		{
			// Token: 0x06009B31 RID: 39729
			public abstract object OnInfect(GameObject go, SicknessInstance diseaseInstance);

			// Token: 0x06009B32 RID: 39730
			public abstract void OnCure(GameObject go, object instance_data);

			// Token: 0x06009B33 RID: 39731 RVA: 0x003362CF File Offset: 0x003344CF
			public virtual List<Descriptor> GetSymptoms()
			{
				return null;
			}
		}

		// Token: 0x02001E47 RID: 7751
		public enum InfectionVector
		{
			// Token: 0x0400883C RID: 34876
			Contact,
			// Token: 0x0400883D RID: 34877
			Digestion,
			// Token: 0x0400883E RID: 34878
			Inhalation,
			// Token: 0x0400883F RID: 34879
			Exposure
		}

		// Token: 0x02001E48 RID: 7752
		public enum SicknessType
		{
			// Token: 0x04008841 RID: 34881
			Pathogen,
			// Token: 0x04008842 RID: 34882
			Ailment,
			// Token: 0x04008843 RID: 34883
			Injury
		}

		// Token: 0x02001E49 RID: 7753
		public enum Severity
		{
			// Token: 0x04008845 RID: 34885
			Benign,
			// Token: 0x04008846 RID: 34886
			Minor,
			// Token: 0x04008847 RID: 34887
			Major,
			// Token: 0x04008848 RID: 34888
			Critical
		}
	}
}
