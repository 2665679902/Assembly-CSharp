using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D8E RID: 3470
	public class PlantBlightEvent : GameplayEvent<PlantBlightEvent.StatesInstance>
	{
		// Token: 0x060069C3 RID: 27075 RVA: 0x00292670 File Offset: 0x00290870
		public PlantBlightEvent(string id, string targetPlantPrefab, float infectionDuration, float incubationDuration)
			: base(id, 0, 0)
		{
			this.targetPlantPrefab = targetPlantPrefab;
			this.infectionDuration = infectionDuration;
			this.incubationDuration = incubationDuration;
			this.title = GAMEPLAY_EVENTS.EVENT_TYPES.PLANT_BLIGHT.NAME;
			this.description = GAMEPLAY_EVENTS.EVENT_TYPES.PLANT_BLIGHT.DESCRIPTION;
		}

		// Token: 0x060069C4 RID: 27076 RVA: 0x002926BC File Offset: 0x002908BC
		public override StateMachine.Instance GetSMI(GameplayEventManager manager, GameplayEventInstance eventInstance)
		{
			return new PlantBlightEvent.StatesInstance(manager, eventInstance, this);
		}

		// Token: 0x04004F94 RID: 20372
		private const float BLIGHT_DISTANCE = 6f;

		// Token: 0x04004F95 RID: 20373
		public string targetPlantPrefab;

		// Token: 0x04004F96 RID: 20374
		public float infectionDuration;

		// Token: 0x04004F97 RID: 20375
		public float incubationDuration;

		// Token: 0x02001E66 RID: 7782
		public class States : GameplayEventStateMachine<PlantBlightEvent.States, PlantBlightEvent.StatesInstance, GameplayEventManager, PlantBlightEvent>
		{
			// Token: 0x06009B9E RID: 39838 RVA: 0x003388B0 File Offset: 0x00336AB0
			public override void InitializeStates(out StateMachine.BaseState default_state)
			{
				base.InitializeStates(out default_state);
				default_state = this.planning;
				base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
				this.planning.Enter(delegate(PlantBlightEvent.StatesInstance smi)
				{
					smi.InfectAPlant(true);
				}).GoTo(this.running);
				this.running.ToggleNotification((PlantBlightEvent.StatesInstance smi) => EventInfoScreen.CreateNotification(this.GenerateEventPopupData(smi), null)).EventHandlerTransition(GameHashes.Uprooted, this.finished, new Func<PlantBlightEvent.StatesInstance, object, bool>(this.NoBlightedPlants)).DefaultState(this.running.waiting)
					.OnSignal(this.doFinish, this.finished);
				this.running.waiting.ParamTransition<float>(this.nextInfection, this.running.infect, (PlantBlightEvent.StatesInstance smi, float p) => p <= 0f).Update(delegate(PlantBlightEvent.StatesInstance smi, float dt)
				{
					this.nextInfection.Delta(-dt, smi);
				}, UpdateRate.SIM_4000ms, false);
				this.running.infect.Enter(delegate(PlantBlightEvent.StatesInstance smi)
				{
					smi.InfectAPlant(false);
				}).GoTo(this.running.waiting);
				this.finished.DoNotification((PlantBlightEvent.StatesInstance smi) => this.CreateSuccessNotification(smi, this.GenerateEventPopupData(smi))).ReturnSuccess();
			}

			// Token: 0x06009B9F RID: 39839 RVA: 0x00338A10 File Offset: 0x00336C10
			public override EventInfoData GenerateEventPopupData(PlantBlightEvent.StatesInstance smi)
			{
				EventInfoData eventInfoData = new EventInfoData(smi.gameplayEvent.title, smi.gameplayEvent.description, smi.gameplayEvent.animFileName);
				string text = smi.gameplayEvent.targetPlantPrefab.ToTag().ProperName();
				eventInfoData.location = GAMEPLAY_EVENTS.LOCATIONS.COLONY_WIDE;
				eventInfoData.whenDescription = GAMEPLAY_EVENTS.TIMES.NOW;
				eventInfoData.SetTextParameter("plant", text);
				return eventInfoData;
			}

			// Token: 0x06009BA0 RID: 39840 RVA: 0x00338A88 File Offset: 0x00336C88
			private Notification CreateSuccessNotification(PlantBlightEvent.StatesInstance smi, EventInfoData eventInfoData)
			{
				string plantName = smi.gameplayEvent.targetPlantPrefab.ToTag().ProperName();
				return new Notification(GAMEPLAY_EVENTS.EVENT_TYPES.PLANT_BLIGHT.SUCCESS.Replace("{plant}", plantName), NotificationType.Neutral, (List<Notification> list, object data) => GAMEPLAY_EVENTS.EVENT_TYPES.PLANT_BLIGHT.SUCCESS_TOOLTIP.Replace("{plant}", plantName), null, true, 0f, null, null, null, true, false, false);
			}

			// Token: 0x06009BA1 RID: 39841 RVA: 0x00338AEC File Offset: 0x00336CEC
			private bool NoBlightedPlants(PlantBlightEvent.StatesInstance smi, object obj)
			{
				GameObject gameObject = (GameObject)obj;
				if (!gameObject.HasTag(GameTags.Blighted))
				{
					return true;
				}
				foreach (Crop crop in Components.Crops.Items.FindAll((Crop p) => p.name == smi.gameplayEvent.targetPlantPrefab))
				{
					if (!(gameObject.gameObject == crop.gameObject) && crop.HasTag(GameTags.Blighted))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0400889E RID: 34974
			public GameStateMachine<PlantBlightEvent.States, PlantBlightEvent.StatesInstance, GameplayEventManager, object>.State planning;

			// Token: 0x0400889F RID: 34975
			public PlantBlightEvent.States.RunningStates running;

			// Token: 0x040088A0 RID: 34976
			public GameStateMachine<PlantBlightEvent.States, PlantBlightEvent.StatesInstance, GameplayEventManager, object>.State finished;

			// Token: 0x040088A1 RID: 34977
			public StateMachine<PlantBlightEvent.States, PlantBlightEvent.StatesInstance, GameplayEventManager, object>.Signal doFinish;

			// Token: 0x040088A2 RID: 34978
			public StateMachine<PlantBlightEvent.States, PlantBlightEvent.StatesInstance, GameplayEventManager, object>.FloatParameter nextInfection;

			// Token: 0x02002D9F RID: 11679
			public class RunningStates : GameStateMachine<PlantBlightEvent.States, PlantBlightEvent.StatesInstance, GameplayEventManager, object>.State
			{
				// Token: 0x0400BA32 RID: 47666
				public GameStateMachine<PlantBlightEvent.States, PlantBlightEvent.StatesInstance, GameplayEventManager, object>.State waiting;

				// Token: 0x0400BA33 RID: 47667
				public GameStateMachine<PlantBlightEvent.States, PlantBlightEvent.StatesInstance, GameplayEventManager, object>.State infect;
			}
		}

		// Token: 0x02001E67 RID: 7783
		public class StatesInstance : GameplayEventStateMachine<PlantBlightEvent.States, PlantBlightEvent.StatesInstance, GameplayEventManager, PlantBlightEvent>.GameplayEventStateMachineInstance
		{
			// Token: 0x06009BA6 RID: 39846 RVA: 0x00338BD4 File Offset: 0x00336DD4
			public StatesInstance(GameplayEventManager master, GameplayEventInstance eventInstance, PlantBlightEvent blightEvent)
				: base(master, eventInstance, blightEvent)
			{
				this.startTime = Time.time;
			}

			// Token: 0x06009BA7 RID: 39847 RVA: 0x00338BEC File Offset: 0x00336DEC
			public void InfectAPlant(bool initialInfection)
			{
				if (Time.time - this.startTime > base.smi.gameplayEvent.infectionDuration)
				{
					base.sm.doFinish.Trigger(base.smi);
					return;
				}
				List<Crop> list = Components.Crops.Items.FindAll((Crop p) => p.name == base.smi.gameplayEvent.targetPlantPrefab);
				if (list.Count == 0)
				{
					base.sm.doFinish.Trigger(base.smi);
					return;
				}
				if (list.Count > 0)
				{
					List<Crop> list2 = new List<Crop>();
					List<Crop> list3 = new List<Crop>();
					foreach (Crop crop in list)
					{
						if (crop.HasTag(GameTags.Blighted))
						{
							list2.Add(crop);
						}
						else
						{
							list3.Add(crop);
						}
					}
					if (list2.Count == 0)
					{
						if (initialInfection)
						{
							Crop crop2 = list3[UnityEngine.Random.Range(0, list3.Count)];
							global::Debug.Log("Blighting a random plant", crop2);
							crop2.GetComponent<BlightVulnerable>().MakeBlighted();
						}
						else
						{
							base.sm.doFinish.Trigger(base.smi);
						}
					}
					else if (list3.Count > 0)
					{
						Crop crop3 = list2[UnityEngine.Random.Range(0, list2.Count)];
						global::Debug.Log("Spreading blight from a plant", crop3);
						list3.Shuffle<Crop>();
						foreach (Crop crop4 in list3)
						{
							if ((crop4.transform.GetPosition() - crop3.transform.GetPosition()).magnitude < 6f)
							{
								crop4.GetComponent<BlightVulnerable>().MakeBlighted();
								break;
							}
						}
					}
				}
				base.sm.nextInfection.Set(base.smi.gameplayEvent.incubationDuration, this, false);
			}

			// Token: 0x040088A3 RID: 34979
			[Serialize]
			private float startTime;
		}
	}
}
