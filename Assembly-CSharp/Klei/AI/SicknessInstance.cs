using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using KSerialization;
using STRINGS;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D77 RID: 3447
	[SerializationConfig(MemberSerialization.OptIn)]
	public class SicknessInstance : ModifierInstance<Sickness>, ISaveLoadable
	{
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06006949 RID: 26953 RVA: 0x0028F288 File Offset: 0x0028D488
		public Sickness Sickness
		{
			get
			{
				return this.modifier;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x0600694A RID: 26954 RVA: 0x0028F290 File Offset: 0x0028D490
		public float TotalCureSpeedMultiplier
		{
			get
			{
				AttributeInstance attributeInstance = Db.Get().Attributes.DiseaseCureSpeed.Lookup(this.smi.master.gameObject);
				AttributeInstance attributeInstance2 = this.modifier.cureSpeedBase.Lookup(this.smi.master.gameObject);
				float num = 1f;
				if (attributeInstance != null)
				{
					num *= attributeInstance.GetTotalValue();
				}
				if (attributeInstance2 != null)
				{
					num *= attributeInstance2.GetTotalValue();
				}
				return num;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x0600694B RID: 26955 RVA: 0x0028F304 File Offset: 0x0028D504
		public bool IsDoctored
		{
			get
			{
				if (base.gameObject == null)
				{
					return false;
				}
				AttributeInstance attributeInstance = Db.Get().Attributes.DoctoredLevel.Lookup(base.gameObject);
				return attributeInstance != null && attributeInstance.GetTotalValue() > 0f;
			}
		}

		// Token: 0x0600694C RID: 26956 RVA: 0x0028F34F File Offset: 0x0028D54F
		public SicknessInstance(GameObject game_object, Sickness disease)
			: base(game_object, disease)
		{
		}

		// Token: 0x0600694D RID: 26957 RVA: 0x0028F359 File Offset: 0x0028D559
		[OnDeserialized]
		private void OnDeserialized()
		{
			this.InitializeAndStart();
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x0600694E RID: 26958 RVA: 0x0028F361 File Offset: 0x0028D561
		// (set) Token: 0x0600694F RID: 26959 RVA: 0x0028F369 File Offset: 0x0028D569
		public SicknessExposureInfo ExposureInfo
		{
			get
			{
				return this.exposureInfo;
			}
			set
			{
				this.exposureInfo = value;
				this.InitializeAndStart();
			}
		}

		// Token: 0x06006950 RID: 26960 RVA: 0x0028F378 File Offset: 0x0028D578
		private void InitializeAndStart()
		{
			Sickness disease = this.modifier;
			Func<List<Notification>, object, string> func = delegate(List<Notification> notificationList, object data)
			{
				string text2 = "";
				for (int i = 0; i < notificationList.Count; i++)
				{
					Notification notification = notificationList[i];
					string text3 = (string)notification.tooltipData;
					text2 += string.Format(DUPLICANTS.DISEASES.NOTIFICATION_TOOLTIP, notification.NotifierName, disease.Name, text3);
					if (i < notificationList.Count - 1)
					{
						text2 += "\n";
					}
				}
				return text2;
			};
			string name = disease.Name;
			string text = name;
			NotificationType notificationType = ((disease.severity <= Sickness.Severity.Minor) ? NotificationType.BadMinor : NotificationType.Bad);
			object sourceInfo = this.exposureInfo.sourceInfo;
			this.notification = new Notification(text, notificationType, func, sourceInfo, true, 0f, null, null, null, true, false, false);
			this.statusItem = new StatusItem(disease.Id, disease.Name, DUPLICANTS.DISEASES.STATUS_ITEM_TOOLTIP.TEMPLATE, "", (disease.severity <= Sickness.Severity.Minor) ? StatusItem.IconType.Info : StatusItem.IconType.Exclamation, (disease.severity <= Sickness.Severity.Minor) ? NotificationType.BadMinor : NotificationType.Bad, false, OverlayModes.None.ID, 129022, true, null);
			this.statusItem.resolveTooltipCallback = new Func<string, object, string>(this.ResolveString);
			if (this.smi != null)
			{
				this.smi.StopSM("refresh");
			}
			this.smi = new SicknessInstance.StatesInstance(this);
			this.smi.StartSM();
		}

		// Token: 0x06006951 RID: 26961 RVA: 0x0028F490 File Offset: 0x0028D690
		private string ResolveString(string str, object data)
		{
			if (this.smi == null)
			{
				global::Debug.LogWarning("Attempting to resolve string when smi is null");
				return str;
			}
			KSelectable component = base.gameObject.GetComponent<KSelectable>();
			str = str.Replace("{Descriptor}", string.Format(DUPLICANTS.DISEASES.STATUS_ITEM_TOOLTIP.DESCRIPTOR, Strings.Get("STRINGS.DUPLICANTS.DISEASES.SEVERITY." + this.modifier.severity.ToString().ToUpper()), Strings.Get("STRINGS.DUPLICANTS.DISEASES.TYPE." + this.modifier.sicknessType.ToString().ToUpper())));
			str = str.Replace("{Infectee}", component.GetProperName());
			str = str.Replace("{InfectionSource}", string.Format(DUPLICANTS.DISEASES.STATUS_ITEM_TOOLTIP.INFECTION_SOURCE, this.exposureInfo.sourceInfo));
			if (this.modifier.severity <= Sickness.Severity.Minor)
			{
				str = str.Replace("{Duration}", string.Format(DUPLICANTS.DISEASES.STATUS_ITEM_TOOLTIP.DURATION, GameUtil.GetFormattedCycles(this.GetInfectedTimeRemaining(), "F1", false)));
			}
			else if (this.modifier.severity == Sickness.Severity.Major)
			{
				str = str.Replace("{Duration}", string.Format(DUPLICANTS.DISEASES.STATUS_ITEM_TOOLTIP.DURATION, GameUtil.GetFormattedCycles(this.GetInfectedTimeRemaining(), "F1", false)));
				if (!this.IsDoctored)
				{
					str = str.Replace("{Doctor}", DUPLICANTS.DISEASES.STATUS_ITEM_TOOLTIP.BEDREST);
				}
				else
				{
					str = str.Replace("{Doctor}", DUPLICANTS.DISEASES.STATUS_ITEM_TOOLTIP.DOCTORED);
				}
			}
			else if (this.modifier.severity >= Sickness.Severity.Critical)
			{
				if (!this.IsDoctored)
				{
					str = str.Replace("{Duration}", string.Format(DUPLICANTS.DISEASES.STATUS_ITEM_TOOLTIP.FATALITY, GameUtil.GetFormattedCycles(this.GetFatalityTimeRemaining(), "F1", false)));
					str = str.Replace("{Doctor}", DUPLICANTS.DISEASES.STATUS_ITEM_TOOLTIP.DOCTOR_REQUIRED);
				}
				else
				{
					str = str.Replace("{Duration}", string.Format(DUPLICANTS.DISEASES.STATUS_ITEM_TOOLTIP.DURATION, GameUtil.GetFormattedCycles(this.GetInfectedTimeRemaining(), "F1", false)));
					str = str.Replace("{Doctor}", DUPLICANTS.DISEASES.STATUS_ITEM_TOOLTIP.DOCTORED);
				}
			}
			List<Descriptor> symptoms = this.modifier.GetSymptoms();
			string text = "";
			foreach (Descriptor descriptor in symptoms)
			{
				if (!string.IsNullOrEmpty(text))
				{
					text += "\n";
				}
				text = text + "    • " + descriptor.text;
			}
			str = str.Replace("{Symptoms}", text);
			str = Regex.Replace(str, "{[^}]*}", "");
			return str;
		}

		// Token: 0x06006952 RID: 26962 RVA: 0x0028F758 File Offset: 0x0028D958
		public float GetInfectedTimeRemaining()
		{
			return this.modifier.SicknessDuration * (1f - this.smi.sm.percentRecovered.Get(this.smi)) / this.TotalCureSpeedMultiplier;
		}

		// Token: 0x06006953 RID: 26963 RVA: 0x0028F78E File Offset: 0x0028D98E
		public float GetFatalityTimeRemaining()
		{
			return this.modifier.fatalityDuration * (1f - this.smi.sm.percentDied.Get(this.smi));
		}

		// Token: 0x06006954 RID: 26964 RVA: 0x0028F7BD File Offset: 0x0028D9BD
		public float GetPercentCured()
		{
			if (this.smi == null)
			{
				return 0f;
			}
			return this.smi.sm.percentRecovered.Get(this.smi);
		}

		// Token: 0x06006955 RID: 26965 RVA: 0x0028F7E8 File Offset: 0x0028D9E8
		public void SetPercentCured(float pct)
		{
			this.smi.sm.percentRecovered.Set(pct, this.smi, false);
		}

		// Token: 0x06006956 RID: 26966 RVA: 0x0028F808 File Offset: 0x0028DA08
		public void Cure()
		{
			this.smi.Cure();
		}

		// Token: 0x06006957 RID: 26967 RVA: 0x0028F815 File Offset: 0x0028DA15
		public override void OnCleanUp()
		{
			if (this.smi != null)
			{
				this.smi.StopSM("DiseaseInstance.OnCleanUp");
				this.smi = null;
			}
		}

		// Token: 0x06006958 RID: 26968 RVA: 0x0028F836 File Offset: 0x0028DA36
		public StatusItem GetStatusItem()
		{
			return this.statusItem;
		}

		// Token: 0x06006959 RID: 26969 RVA: 0x0028F83E File Offset: 0x0028DA3E
		public List<Descriptor> GetDescriptors()
		{
			return this.modifier.GetSicknessSourceDescriptors();
		}

		// Token: 0x04004F35 RID: 20277
		[Serialize]
		private SicknessExposureInfo exposureInfo;

		// Token: 0x04004F36 RID: 20278
		private SicknessInstance.StatesInstance smi;

		// Token: 0x04004F37 RID: 20279
		private StatusItem statusItem;

		// Token: 0x04004F38 RID: 20280
		private Notification notification;

		// Token: 0x02001E4A RID: 7754
		private struct CureInfo
		{
			// Token: 0x04008849 RID: 34889
			public string name;

			// Token: 0x0400884A RID: 34890
			public float multiplier;
		}

		// Token: 0x02001E4B RID: 7755
		public class StatesInstance : GameStateMachine<SicknessInstance.States, SicknessInstance.StatesInstance, SicknessInstance, object>.GameInstance
		{
			// Token: 0x06009B35 RID: 39733 RVA: 0x003362DA File Offset: 0x003344DA
			public StatesInstance(SicknessInstance master)
				: base(master)
			{
			}

			// Token: 0x06009B36 RID: 39734 RVA: 0x003362E4 File Offset: 0x003344E4
			public void UpdateProgress(float dt)
			{
				float num = dt * base.master.TotalCureSpeedMultiplier / base.master.modifier.SicknessDuration;
				base.sm.percentRecovered.Delta(num, base.smi);
				if (base.master.modifier.fatalityDuration > 0f)
				{
					if (!base.master.IsDoctored)
					{
						float num2 = dt / base.master.modifier.fatalityDuration;
						base.sm.percentDied.Delta(num2, base.smi);
						return;
					}
					base.sm.percentDied.Set(0f, base.smi, false);
				}
			}

			// Token: 0x06009B37 RID: 39735 RVA: 0x00336398 File Offset: 0x00334598
			public void Infect()
			{
				Sickness modifier = base.master.modifier;
				this.componentData = modifier.Infect(base.gameObject, base.master, base.master.exposureInfo);
				if (PopFXManager.Instance != null)
				{
					PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, string.Format(DUPLICANTS.DISEASES.INFECTED_POPUP, modifier.Name), base.gameObject.transform, 1.5f, true);
				}
			}

			// Token: 0x06009B38 RID: 39736 RVA: 0x0033641C File Offset: 0x0033461C
			public void Cure()
			{
				Sickness modifier = base.master.modifier;
				base.gameObject.GetComponent<Modifiers>().sicknesses.Cure(modifier);
				modifier.Cure(base.gameObject, this.componentData);
				if (PopFXManager.Instance != null)
				{
					PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, string.Format(DUPLICANTS.DISEASES.CURED_POPUP, modifier.Name), base.gameObject.transform, 1.5f, true);
				}
				if (!string.IsNullOrEmpty(modifier.recoveryEffect))
				{
					Effects component = base.gameObject.GetComponent<Effects>();
					if (component)
					{
						component.Add(modifier.recoveryEffect, true);
					}
				}
			}

			// Token: 0x06009B39 RID: 39737 RVA: 0x003364D5 File Offset: 0x003346D5
			public SicknessExposureInfo GetExposureInfo()
			{
				return base.master.ExposureInfo;
			}

			// Token: 0x0400884B RID: 34891
			private object[] componentData;
		}

		// Token: 0x02001E4C RID: 7756
		public class States : GameStateMachine<SicknessInstance.States, SicknessInstance.StatesInstance, SicknessInstance>
		{
			// Token: 0x06009B3A RID: 39738 RVA: 0x003364E4 File Offset: 0x003346E4
			public override void InitializeStates(out StateMachine.BaseState default_state)
			{
				default_state = this.infected;
				base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
				this.infected.Enter("Infect", delegate(SicknessInstance.StatesInstance smi)
				{
					smi.Infect();
				}).DoNotification((SicknessInstance.StatesInstance smi) => smi.master.notification).Update("UpdateProgress", delegate(SicknessInstance.StatesInstance smi, float dt)
				{
					smi.UpdateProgress(dt);
				}, UpdateRate.SIM_200ms, false)
					.ToggleStatusItem((SicknessInstance.StatesInstance smi) => smi.master.GetStatusItem(), (SicknessInstance.StatesInstance smi) => smi)
					.ParamTransition<float>(this.percentRecovered, this.cured, GameStateMachine<SicknessInstance.States, SicknessInstance.StatesInstance, SicknessInstance, object>.IsGTOne)
					.ParamTransition<float>(this.percentDied, this.fatality_pre, GameStateMachine<SicknessInstance.States, SicknessInstance.StatesInstance, SicknessInstance, object>.IsGTOne);
				this.cured.Enter("Cure", delegate(SicknessInstance.StatesInstance smi)
				{
					smi.master.Cure();
				});
				this.fatality_pre.Update("DeathByDisease", delegate(SicknessInstance.StatesInstance smi, float dt)
				{
					DeathMonitor.Instance smi2 = smi.master.gameObject.GetSMI<DeathMonitor.Instance>();
					if (smi2 != null)
					{
						smi2.Kill(Db.Get().Deaths.FatalDisease);
						smi.GoTo(this.fatality);
					}
				}, UpdateRate.SIM_200ms, false);
				this.fatality.DoNothing();
			}

			// Token: 0x0400884C RID: 34892
			public StateMachine<SicknessInstance.States, SicknessInstance.StatesInstance, SicknessInstance, object>.FloatParameter percentRecovered;

			// Token: 0x0400884D RID: 34893
			public StateMachine<SicknessInstance.States, SicknessInstance.StatesInstance, SicknessInstance, object>.FloatParameter percentDied;

			// Token: 0x0400884E RID: 34894
			public GameStateMachine<SicknessInstance.States, SicknessInstance.StatesInstance, SicknessInstance, object>.State infected;

			// Token: 0x0400884F RID: 34895
			public GameStateMachine<SicknessInstance.States, SicknessInstance.StatesInstance, SicknessInstance, object>.State cured;

			// Token: 0x04008850 RID: 34896
			public GameStateMachine<SicknessInstance.States, SicknessInstance.StatesInstance, SicknessInstance, object>.State fatality_pre;

			// Token: 0x04008851 RID: 34897
			public GameStateMachine<SicknessInstance.States, SicknessInstance.StatesInstance, SicknessInstance, object>.State fatality;
		}
	}
}
