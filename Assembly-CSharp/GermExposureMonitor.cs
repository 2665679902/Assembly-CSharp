using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000830 RID: 2096
public class GermExposureMonitor : GameStateMachine<GermExposureMonitor, GermExposureMonitor.Instance>
{
	// Token: 0x06003C99 RID: 15513 RVA: 0x0015211C File Offset: 0x0015031C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		base.serializable = StateMachine.SerializeType.Never;
		this.root.Update(delegate(GermExposureMonitor.Instance smi, float dt)
		{
			smi.OnInhaleExposureTick(dt);
		}, UpdateRate.SIM_1000ms, true).EventHandler(GameHashes.EatCompleteEater, delegate(GermExposureMonitor.Instance smi, object obj)
		{
			smi.OnEatComplete(obj);
		}).EventHandler(GameHashes.SicknessAdded, delegate(GermExposureMonitor.Instance smi, object data)
		{
			smi.OnSicknessAdded(data);
		})
			.EventHandler(GameHashes.SicknessCured, delegate(GermExposureMonitor.Instance smi, object data)
			{
				smi.OnSicknessCured(data);
			})
			.EventHandler(GameHashes.SleepFinished, delegate(GermExposureMonitor.Instance smi)
			{
				smi.OnSleepFinished();
			});
	}

	// Token: 0x06003C9A RID: 15514 RVA: 0x00152209 File Offset: 0x00150409
	public static float GetContractionChance(float rating)
	{
		return 0.5f - 0.5f * (float)Math.Tanh(0.25 * (double)rating);
	}

	// Token: 0x020015B2 RID: 5554
	public enum ExposureState
	{
		// Token: 0x0400679C RID: 26524
		None,
		// Token: 0x0400679D RID: 26525
		Contact,
		// Token: 0x0400679E RID: 26526
		Exposed,
		// Token: 0x0400679F RID: 26527
		Contracted,
		// Token: 0x040067A0 RID: 26528
		Sick
	}

	// Token: 0x020015B3 RID: 5555
	public class ExposureStatusData
	{
		// Token: 0x040067A1 RID: 26529
		public ExposureType exposure_type;

		// Token: 0x040067A2 RID: 26530
		public GermExposureMonitor.Instance owner;
	}

	// Token: 0x020015B4 RID: 5556
	[SerializationConfig(MemberSerialization.OptIn)]
	public new class Instance : GameStateMachine<GermExposureMonitor, GermExposureMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x060084E9 RID: 34025 RVA: 0x002EAF1C File Offset: 0x002E911C
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.sicknesses = master.GetComponent<MinionModifiers>().sicknesses;
			this.primaryElement = master.GetComponent<PrimaryElement>();
			this.traits = master.GetComponent<Traits>();
			this.lastDiseaseSources = new Dictionary<HashedString, GermExposureMonitor.Instance.DiseaseSourceInfo>();
			this.lastExposureTime = new Dictionary<HashedString, float>();
			this.inhaleExposureTick = new Dictionary<HashedString, GermExposureMonitor.Instance.InhaleTickInfo>();
			GameClock.Instance.Subscribe(-722330267, new Action<object>(this.OnNightTime));
			OxygenBreather component = base.GetComponent<OxygenBreather>();
			component.onSimConsume = (Action<Sim.MassConsumedCallback>)Delegate.Combine(component.onSimConsume, new Action<Sim.MassConsumedCallback>(this.OnAirConsumed));
		}

		// Token: 0x060084EA RID: 34026 RVA: 0x002EAFE9 File Offset: 0x002E91E9
		public override void StartSM()
		{
			base.StartSM();
			this.RefreshStatusItems();
		}

		// Token: 0x060084EB RID: 34027 RVA: 0x002EAFF8 File Offset: 0x002E91F8
		public override void StopSM(string reason)
		{
			GameClock.Instance.Unsubscribe(-722330267, new Action<object>(this.OnNightTime));
			foreach (ExposureType exposureType in GERM_EXPOSURE.TYPES)
			{
				Guid guid;
				this.statusItemHandles.TryGetValue(exposureType.germ_id, out guid);
				guid = base.GetComponent<KSelectable>().RemoveStatusItem(guid, false);
			}
			base.StopSM(reason);
		}

		// Token: 0x060084EC RID: 34028 RVA: 0x002EB064 File Offset: 0x002E9264
		public void OnEatComplete(object obj)
		{
			Edible edible = (Edible)obj;
			HandleVector<int>.Handle handle = GameComps.DiseaseContainers.GetHandle(edible.gameObject);
			if (handle != HandleVector<int>.InvalidHandle)
			{
				DiseaseHeader header = GameComps.DiseaseContainers.GetHeader(handle);
				if (header.diseaseIdx != 255)
				{
					Disease disease = Db.Get().Diseases[(int)header.diseaseIdx];
					float num = edible.unitsConsumed / (edible.unitsConsumed + edible.Units);
					int num2 = Mathf.CeilToInt((float)header.diseaseCount * num);
					GameComps.DiseaseContainers.ModifyDiseaseCount(handle, -num2);
					KPrefabID component = edible.GetComponent<KPrefabID>();
					this.InjectDisease(disease, num2, component.PrefabID(), Sickness.InfectionVector.Digestion);
				}
			}
		}

		// Token: 0x060084ED RID: 34029 RVA: 0x002EB114 File Offset: 0x002E9314
		public void OnAirConsumed(Sim.MassConsumedCallback mass_cb_info)
		{
			if (mass_cb_info.diseaseIdx != 255)
			{
				Disease disease = Db.Get().Diseases[(int)mass_cb_info.diseaseIdx];
				this.InjectDisease(disease, mass_cb_info.diseaseCount, ElementLoader.elements[(int)mass_cb_info.elemIdx].tag, Sickness.InfectionVector.Inhalation);
			}
		}

		// Token: 0x060084EE RID: 34030 RVA: 0x002EB168 File Offset: 0x002E9368
		public void OnInhaleExposureTick(float dt)
		{
			foreach (KeyValuePair<HashedString, GermExposureMonitor.Instance.InhaleTickInfo> keyValuePair in this.inhaleExposureTick)
			{
				if (keyValuePair.Value.inhaled)
				{
					keyValuePair.Value.inhaled = false;
					keyValuePair.Value.ticks++;
				}
				else
				{
					keyValuePair.Value.ticks = Mathf.Max(0, keyValuePair.Value.ticks - 1);
				}
			}
		}

		// Token: 0x060084EF RID: 34031 RVA: 0x002EB208 File Offset: 0x002E9408
		public void TryInjectDisease(byte disease_idx, int count, Tag source, Sickness.InfectionVector vector)
		{
			if (disease_idx != 255)
			{
				Disease disease = Db.Get().Diseases[(int)disease_idx];
				this.InjectDisease(disease, count, source, vector);
			}
		}

		// Token: 0x060084F0 RID: 34032 RVA: 0x002EB239 File Offset: 0x002E9439
		public float GetGermResistance()
		{
			return Db.Get().Attributes.GermResistance.Lookup(base.gameObject).GetTotalValue();
		}

		// Token: 0x060084F1 RID: 34033 RVA: 0x002EB25C File Offset: 0x002E945C
		public float GetResistanceToExposureType(ExposureType exposureType, float overrideExposureTier = -1f)
		{
			float num = overrideExposureTier;
			if (num == -1f)
			{
				num = this.GetExposureTier(exposureType.germ_id);
			}
			num = Mathf.Clamp(num, 1f, 3f);
			float num2 = GERM_EXPOSURE.EXPOSURE_TIER_RESISTANCE_BONUSES[(int)num - 1];
			float totalValue = Db.Get().Attributes.GermResistance.Lookup(base.gameObject).GetTotalValue();
			return (float)exposureType.base_resistance + totalValue + num2;
		}

		// Token: 0x060084F2 RID: 34034 RVA: 0x002EB2C8 File Offset: 0x002E94C8
		public int AssessDigestedGerms(ExposureType exposure_type, int count)
		{
			int exposure_threshold = exposure_type.exposure_threshold;
			int num = count / exposure_threshold;
			return MathUtil.Clamp(1, 3, num);
		}

		// Token: 0x060084F3 RID: 34035 RVA: 0x002EB2E8 File Offset: 0x002E94E8
		public bool AssessInhaledGerms(ExposureType exposure_type)
		{
			GermExposureMonitor.Instance.InhaleTickInfo inhaleTickInfo;
			this.inhaleExposureTick.TryGetValue(exposure_type.germ_id, out inhaleTickInfo);
			if (inhaleTickInfo == null)
			{
				inhaleTickInfo = new GermExposureMonitor.Instance.InhaleTickInfo();
				this.inhaleExposureTick[exposure_type.germ_id] = inhaleTickInfo;
			}
			if (!inhaleTickInfo.inhaled)
			{
				float exposureTier = this.GetExposureTier(exposure_type.germ_id);
				inhaleTickInfo.inhaled = true;
				return inhaleTickInfo.ticks >= GERM_EXPOSURE.INHALE_TICK_THRESHOLD[(int)exposureTier];
			}
			return false;
		}

		// Token: 0x060084F4 RID: 34036 RVA: 0x002EB360 File Offset: 0x002E9560
		public void InjectDisease(Disease disease, int count, Tag source, Sickness.InfectionVector vector)
		{
			foreach (ExposureType exposureType in GERM_EXPOSURE.TYPES)
			{
				if (disease.id == exposureType.germ_id && count > exposureType.exposure_threshold && this.HasMinExposurePeriodElapsed(exposureType.germ_id) && this.IsExposureValidForTraits(exposureType))
				{
					Sickness sickness = ((exposureType.sickness_id != null) ? Db.Get().Sicknesses.Get(exposureType.sickness_id) : null);
					if (sickness == null || sickness.infectionVectors.Contains(vector))
					{
						GermExposureMonitor.ExposureState exposureState = this.GetExposureState(exposureType.germ_id);
						float exposureTier = this.GetExposureTier(exposureType.germ_id);
						if (exposureState == GermExposureMonitor.ExposureState.None || exposureState == GermExposureMonitor.ExposureState.Contact)
						{
							float contractionChance = GermExposureMonitor.GetContractionChance(this.GetResistanceToExposureType(exposureType, -1f));
							this.SetExposureState(exposureType.germ_id, GermExposureMonitor.ExposureState.Contact);
							if (contractionChance > 0f)
							{
								this.lastDiseaseSources[disease.id] = new GermExposureMonitor.Instance.DiseaseSourceInfo(source, vector, contractionChance, base.transform.GetPosition());
								if (exposureType.infect_immediately)
								{
									this.InfectImmediately(exposureType);
								}
								else
								{
									bool flag = true;
									bool flag2 = vector == Sickness.InfectionVector.Inhalation;
									bool flag3 = vector == Sickness.InfectionVector.Digestion;
									int num = 1;
									if (flag2)
									{
										flag = this.AssessInhaledGerms(exposureType);
									}
									if (flag3)
									{
										num = this.AssessDigestedGerms(exposureType, count);
									}
									if (flag)
									{
										if (flag2)
										{
											this.inhaleExposureTick[exposureType.germ_id].ticks = 0;
										}
										this.SetExposureState(exposureType.germ_id, GermExposureMonitor.ExposureState.Exposed);
										this.SetExposureTier(exposureType.germ_id, (float)num);
										float num2 = Mathf.Clamp01(contractionChance);
										GermExposureTracker.Instance.AddExposure(exposureType, num2);
									}
								}
							}
						}
						else if (exposureState == GermExposureMonitor.ExposureState.Exposed && exposureTier < 3f)
						{
							float contractionChance2 = GermExposureMonitor.GetContractionChance(this.GetResistanceToExposureType(exposureType, -1f));
							if (contractionChance2 > 0f)
							{
								this.lastDiseaseSources[disease.id] = new GermExposureMonitor.Instance.DiseaseSourceInfo(source, vector, contractionChance2, base.transform.GetPosition());
								if (!exposureType.infect_immediately)
								{
									bool flag4 = true;
									bool flag5 = vector == Sickness.InfectionVector.Inhalation;
									bool flag6 = vector == Sickness.InfectionVector.Digestion;
									int num3 = 1;
									if (flag5)
									{
										flag4 = this.AssessInhaledGerms(exposureType);
									}
									if (flag6)
									{
										num3 = this.AssessDigestedGerms(exposureType, count);
									}
									if (flag4)
									{
										if (flag5)
										{
											this.inhaleExposureTick[exposureType.germ_id].ticks = 0;
										}
										this.SetExposureTier(exposureType.germ_id, this.GetExposureTier(exposureType.germ_id) + (float)num3);
										float num4 = Mathf.Clamp01(GermExposureMonitor.GetContractionChance(this.GetResistanceToExposureType(exposureType, -1f)) - contractionChance2);
										GermExposureTracker.Instance.AddExposure(exposureType, num4);
									}
								}
							}
						}
					}
				}
			}
			this.RefreshStatusItems();
		}

		// Token: 0x060084F5 RID: 34037 RVA: 0x002EB624 File Offset: 0x002E9824
		public GermExposureMonitor.ExposureState GetExposureState(string germ_id)
		{
			GermExposureMonitor.ExposureState exposureState;
			this.exposureStates.TryGetValue(germ_id, out exposureState);
			return exposureState;
		}

		// Token: 0x060084F6 RID: 34038 RVA: 0x002EB644 File Offset: 0x002E9844
		public float GetExposureTier(string germ_id)
		{
			float num = 1f;
			this.exposureTiers.TryGetValue(germ_id, out num);
			return Mathf.Clamp(num, 1f, 3f);
		}

		// Token: 0x060084F7 RID: 34039 RVA: 0x002EB676 File Offset: 0x002E9876
		public void SetExposureState(string germ_id, GermExposureMonitor.ExposureState exposure_state)
		{
			this.exposureStates[germ_id] = exposure_state;
			this.RefreshStatusItems();
		}

		// Token: 0x060084F8 RID: 34040 RVA: 0x002EB68B File Offset: 0x002E988B
		public void SetExposureTier(string germ_id, float tier)
		{
			tier = Mathf.Clamp(tier, 0f, 3f);
			this.exposureTiers[germ_id] = tier;
			this.RefreshStatusItems();
		}

		// Token: 0x060084F9 RID: 34041 RVA: 0x002EB6B2 File Offset: 0x002E98B2
		public void ContractGerms(string germ_id)
		{
			DebugUtil.DevAssert(this.GetExposureState(germ_id) == GermExposureMonitor.ExposureState.Exposed, "Duplicant is contracting a sickness but was never exposed to it!", null);
			this.SetExposureState(germ_id, GermExposureMonitor.ExposureState.Contracted);
		}

		// Token: 0x060084FA RID: 34042 RVA: 0x002EB6D4 File Offset: 0x002E98D4
		public void OnSicknessAdded(object sickness_instance_data)
		{
			SicknessInstance sicknessInstance = (SicknessInstance)sickness_instance_data;
			foreach (ExposureType exposureType in GERM_EXPOSURE.TYPES)
			{
				if (exposureType.sickness_id == sicknessInstance.Sickness.Id)
				{
					this.SetExposureState(exposureType.germ_id, GermExposureMonitor.ExposureState.Sick);
				}
			}
		}

		// Token: 0x060084FB RID: 34043 RVA: 0x002EB728 File Offset: 0x002E9928
		public void OnSicknessCured(object sickness_instance_data)
		{
			SicknessInstance sicknessInstance = (SicknessInstance)sickness_instance_data;
			foreach (ExposureType exposureType in GERM_EXPOSURE.TYPES)
			{
				if (exposureType.sickness_id == sicknessInstance.Sickness.Id)
				{
					this.SetExposureState(exposureType.germ_id, GermExposureMonitor.ExposureState.None);
				}
			}
		}

		// Token: 0x060084FC RID: 34044 RVA: 0x002EB77C File Offset: 0x002E997C
		private bool IsExposureValidForTraits(ExposureType exposure_type)
		{
			if (exposure_type.required_traits != null && exposure_type.required_traits.Count > 0)
			{
				foreach (string text in exposure_type.required_traits)
				{
					if (!this.traits.HasTrait(text))
					{
						return false;
					}
				}
			}
			if (exposure_type.excluded_traits != null && exposure_type.excluded_traits.Count > 0)
			{
				foreach (string text2 in exposure_type.excluded_traits)
				{
					if (this.traits.HasTrait(text2))
					{
						return false;
					}
				}
			}
			if (exposure_type.excluded_effects != null && exposure_type.excluded_effects.Count > 0)
			{
				Effects component = base.master.GetComponent<Effects>();
				foreach (string text3 in exposure_type.excluded_effects)
				{
					if (component.HasEffect(text3))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060084FD RID: 34045 RVA: 0x002EB8C8 File Offset: 0x002E9AC8
		private bool HasMinExposurePeriodElapsed(string germ_id)
		{
			float num;
			this.lastExposureTime.TryGetValue(germ_id, out num);
			return num == 0f || GameClock.Instance.GetTime() - num > 540f;
		}

		// Token: 0x060084FE RID: 34046 RVA: 0x002EB908 File Offset: 0x002E9B08
		private void RefreshStatusItems()
		{
			foreach (ExposureType exposureType in GERM_EXPOSURE.TYPES)
			{
				Guid guid;
				this.contactStatusItemHandles.TryGetValue(exposureType.germ_id, out guid);
				Guid guid2;
				this.statusItemHandles.TryGetValue(exposureType.germ_id, out guid2);
				GermExposureMonitor.ExposureState exposureState = this.GetExposureState(exposureType.germ_id);
				if (guid2 == Guid.Empty && (exposureState == GermExposureMonitor.ExposureState.Exposed || exposureState == GermExposureMonitor.ExposureState.Contracted) && !string.IsNullOrEmpty(exposureType.sickness_id))
				{
					guid2 = base.GetComponent<KSelectable>().AddStatusItem(Db.Get().DuplicantStatusItems.ExposedToGerms, new GermExposureMonitor.ExposureStatusData
					{
						exposure_type = exposureType,
						owner = this
					});
				}
				else if (guid2 != Guid.Empty && exposureState != GermExposureMonitor.ExposureState.Exposed && exposureState != GermExposureMonitor.ExposureState.Contracted)
				{
					guid2 = base.GetComponent<KSelectable>().RemoveStatusItem(guid2, false);
				}
				this.statusItemHandles[exposureType.germ_id] = guid2;
				if (guid == Guid.Empty && exposureState == GermExposureMonitor.ExposureState.Contact)
				{
					if (!string.IsNullOrEmpty(exposureType.sickness_id))
					{
						guid = base.GetComponent<KSelectable>().AddStatusItem(Db.Get().DuplicantStatusItems.ContactWithGerms, new GermExposureMonitor.ExposureStatusData
						{
							exposure_type = exposureType,
							owner = this
						});
					}
				}
				else if (guid != Guid.Empty && exposureState != GermExposureMonitor.ExposureState.Contact)
				{
					guid = base.GetComponent<KSelectable>().RemoveStatusItem(guid, false);
				}
				this.contactStatusItemHandles[exposureType.germ_id] = guid;
			}
		}

		// Token: 0x060084FF RID: 34047 RVA: 0x002EBA7B File Offset: 0x002E9C7B
		private void OnNightTime(object data)
		{
			this.UpdateReports();
		}

		// Token: 0x06008500 RID: 34048 RVA: 0x002EBA84 File Offset: 0x002E9C84
		private void UpdateReports()
		{
			ReportManager.Instance.ReportValue(ReportManager.ReportType.DiseaseStatus, (float)this.primaryElement.DiseaseCount, StringFormatter.Replace(UI.ENDOFDAYREPORT.NOTES.GERMS, "{0}", base.master.name), base.master.gameObject.GetProperName());
		}

		// Token: 0x06008501 RID: 34049 RVA: 0x002EBAD8 File Offset: 0x002E9CD8
		public void InfectImmediately(ExposureType exposure_type)
		{
			if (exposure_type.infection_effect != null)
			{
				base.master.GetComponent<Effects>().Add(exposure_type.infection_effect, true);
			}
			if (exposure_type.sickness_id != null)
			{
				string lastDiseaseSource = this.GetLastDiseaseSource(exposure_type.germ_id);
				SicknessExposureInfo sicknessExposureInfo = new SicknessExposureInfo(exposure_type.sickness_id, lastDiseaseSource);
				this.sicknesses.Infect(sicknessExposureInfo);
			}
		}

		// Token: 0x06008502 RID: 34050 RVA: 0x002EBB34 File Offset: 0x002E9D34
		public void OnSleepFinished()
		{
			foreach (ExposureType exposureType in GERM_EXPOSURE.TYPES)
			{
				if (!exposureType.infect_immediately && exposureType.sickness_id != null)
				{
					GermExposureMonitor.ExposureState exposureState = this.GetExposureState(exposureType.germ_id);
					if (exposureState == GermExposureMonitor.ExposureState.Exposed)
					{
						this.SetExposureState(exposureType.germ_id, GermExposureMonitor.ExposureState.None);
					}
					if (exposureState == GermExposureMonitor.ExposureState.Contracted)
					{
						this.SetExposureState(exposureType.germ_id, GermExposureMonitor.ExposureState.Sick);
						string lastDiseaseSource = this.GetLastDiseaseSource(exposureType.germ_id);
						SicknessExposureInfo sicknessExposureInfo = new SicknessExposureInfo(exposureType.sickness_id, lastDiseaseSource);
						this.sicknesses.Infect(sicknessExposureInfo);
					}
					this.SetExposureTier(exposureType.germ_id, 0f);
				}
			}
		}

		// Token: 0x06008503 RID: 34051 RVA: 0x002EBBD4 File Offset: 0x002E9DD4
		public string GetLastDiseaseSource(string id)
		{
			GermExposureMonitor.Instance.DiseaseSourceInfo diseaseSourceInfo;
			string text;
			if (this.lastDiseaseSources.TryGetValue(id, out diseaseSourceInfo))
			{
				switch (diseaseSourceInfo.vector)
				{
				case Sickness.InfectionVector.Contact:
					text = DUPLICANTS.DISEASES.INFECTIONSOURCES.SKIN;
					break;
				case Sickness.InfectionVector.Digestion:
					text = string.Format(DUPLICANTS.DISEASES.INFECTIONSOURCES.FOOD, diseaseSourceInfo.sourceObject.ProperName());
					break;
				case Sickness.InfectionVector.Inhalation:
					text = string.Format(DUPLICANTS.DISEASES.INFECTIONSOURCES.AIR, diseaseSourceInfo.sourceObject.ProperName());
					break;
				default:
					text = DUPLICANTS.DISEASES.INFECTIONSOURCES.UNKNOWN;
					break;
				}
			}
			else
			{
				text = DUPLICANTS.DISEASES.INFECTIONSOURCES.UNKNOWN;
			}
			return text;
		}

		// Token: 0x06008504 RID: 34052 RVA: 0x002EBC70 File Offset: 0x002E9E70
		public Vector3 GetLastExposurePosition(string germ_id)
		{
			GermExposureMonitor.Instance.DiseaseSourceInfo diseaseSourceInfo;
			if (this.lastDiseaseSources.TryGetValue(germ_id, out diseaseSourceInfo))
			{
				return diseaseSourceInfo.position;
			}
			return base.transform.GetPosition();
		}

		// Token: 0x06008505 RID: 34053 RVA: 0x002EBCA4 File Offset: 0x002E9EA4
		public float GetExposureWeight(string id)
		{
			float exposureTier = this.GetExposureTier(id);
			GermExposureMonitor.Instance.DiseaseSourceInfo diseaseSourceInfo;
			if (this.lastDiseaseSources.TryGetValue(id, out diseaseSourceInfo))
			{
				return diseaseSourceInfo.factor * exposureTier;
			}
			return 0f;
		}

		// Token: 0x040067A3 RID: 26531
		[Serialize]
		public Dictionary<HashedString, GermExposureMonitor.Instance.DiseaseSourceInfo> lastDiseaseSources;

		// Token: 0x040067A4 RID: 26532
		[Serialize]
		public Dictionary<HashedString, float> lastExposureTime;

		// Token: 0x040067A5 RID: 26533
		private Dictionary<HashedString, GermExposureMonitor.Instance.InhaleTickInfo> inhaleExposureTick;

		// Token: 0x040067A6 RID: 26534
		private Sicknesses sicknesses;

		// Token: 0x040067A7 RID: 26535
		private PrimaryElement primaryElement;

		// Token: 0x040067A8 RID: 26536
		private Traits traits;

		// Token: 0x040067A9 RID: 26537
		[Serialize]
		private Dictionary<string, GermExposureMonitor.ExposureState> exposureStates = new Dictionary<string, GermExposureMonitor.ExposureState>();

		// Token: 0x040067AA RID: 26538
		[Serialize]
		private Dictionary<string, float> exposureTiers = new Dictionary<string, float>();

		// Token: 0x040067AB RID: 26539
		private Dictionary<string, Guid> statusItemHandles = new Dictionary<string, Guid>();

		// Token: 0x040067AC RID: 26540
		private Dictionary<string, Guid> contactStatusItemHandles = new Dictionary<string, Guid>();

		// Token: 0x02002079 RID: 8313
		[Serializable]
		public class DiseaseSourceInfo
		{
			// Token: 0x0600A3EA RID: 41962 RVA: 0x0034706C File Offset: 0x0034526C
			public DiseaseSourceInfo(Tag sourceObject, Sickness.InfectionVector vector, float factor, Vector3 position)
			{
				this.sourceObject = sourceObject;
				this.vector = vector;
				this.factor = factor;
				this.position = position;
			}

			// Token: 0x040090BE RID: 37054
			public Tag sourceObject;

			// Token: 0x040090BF RID: 37055
			public Sickness.InfectionVector vector;

			// Token: 0x040090C0 RID: 37056
			public float factor;

			// Token: 0x040090C1 RID: 37057
			public Vector3 position;
		}

		// Token: 0x0200207A RID: 8314
		public class InhaleTickInfo
		{
			// Token: 0x040090C2 RID: 37058
			public bool inhaled;

			// Token: 0x040090C3 RID: 37059
			public int ticks;
		}
	}
}
