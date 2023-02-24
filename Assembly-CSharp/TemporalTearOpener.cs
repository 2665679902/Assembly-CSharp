using System;
using System.Collections.Generic;
using Database;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200065B RID: 1627
public class TemporalTearOpener : GameStateMachine<TemporalTearOpener, TemporalTearOpener.Instance, IStateMachineTarget, TemporalTearOpener.Def>
{
	// Token: 0x06002BB3 RID: 11187 RVA: 0x000E561C File Offset: 0x000E381C
	private static StatusItem CreateColoniesStatusItem()
	{
		StatusItem statusItem = new StatusItem("Temporal_Tear_Opener_Insufficient_Colonies", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);
		statusItem.resolveStringCallback = delegate(string str, object data)
		{
			TemporalTearOpener.Instance instance = (TemporalTearOpener.Instance)data;
			str = str.Replace("{progress}", string.Format("({0}/{1})", instance.CountColonies(), EstablishColonies.BASE_COUNT));
			return str;
		};
		return statusItem;
	}

	// Token: 0x06002BB4 RID: 11188 RVA: 0x000E5674 File Offset: 0x000E3874
	private static StatusItem CreateProgressStatusItem()
	{
		StatusItem statusItem = new StatusItem("Temporal_Tear_Opener_Progress", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);
		statusItem.resolveStringCallback = delegate(string str, object data)
		{
			TemporalTearOpener.Instance instance = (TemporalTearOpener.Instance)data;
			str = str.Replace("{progress}", GameUtil.GetFormattedPercent(instance.GetPercentComplete(), GameUtil.TimeSlice.None));
			return str;
		};
		return statusItem;
	}

	// Token: 0x06002BB5 RID: 11189 RVA: 0x000E56CC File Offset: 0x000E38CC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.Enter(delegate(TemporalTearOpener.Instance smi)
		{
			smi.UpdateMeter();
			if (ClusterManager.Instance.GetClusterPOIManager().IsTemporalTearOpen())
			{
				smi.GoTo(this.opening_tear_finish);
				return;
			}
			smi.GoTo(this.check_requirements);
		}).PlayAnim("off");
		this.check_requirements.DefaultState(this.check_requirements.has_target).Enter(delegate(TemporalTearOpener.Instance smi)
		{
			smi.GetComponent<HighEnergyParticleStorage>().receiverOpen = false;
			smi.GetComponent<KBatchedAnimController>().Play("port_close", KAnim.PlayMode.Once, 1f, 0f);
			smi.GetComponent<KBatchedAnimController>().Queue("off", KAnim.PlayMode.Loop, 1f, 0f);
		});
		this.check_requirements.has_target.ToggleStatusItem(TemporalTearOpener.s_noTargetStatus, null).UpdateTransition(this.check_requirements.has_los, (TemporalTearOpener.Instance smi, float dt) => ClusterManager.Instance.GetClusterPOIManager().IsTemporalTearRevealed(), UpdateRate.SIM_200ms, false);
		this.check_requirements.has_los.ToggleStatusItem(TemporalTearOpener.s_noLosStatus, null).UpdateTransition(this.check_requirements.enough_colonies, (TemporalTearOpener.Instance smi, float dt) => smi.HasLineOfSight(), UpdateRate.SIM_200ms, false);
		this.check_requirements.enough_colonies.ToggleStatusItem(TemporalTearOpener.s_insufficient_colonies, null).UpdateTransition(this.charging, (TemporalTearOpener.Instance smi, float dt) => smi.HasSufficientColonies(), UpdateRate.SIM_200ms, false);
		this.charging.DefaultState(this.charging.idle).ToggleStatusItem(TemporalTearOpener.s_progressStatus, (TemporalTearOpener.Instance smi) => smi).UpdateTransition(this.check_requirements.has_los, (TemporalTearOpener.Instance smi, float dt) => !smi.HasLineOfSight(), UpdateRate.SIM_200ms, false)
			.UpdateTransition(this.check_requirements.enough_colonies, (TemporalTearOpener.Instance smi, float dt) => !smi.HasSufficientColonies(), UpdateRate.SIM_200ms, false)
			.Enter(delegate(TemporalTearOpener.Instance smi)
			{
				smi.GetComponent<HighEnergyParticleStorage>().receiverOpen = true;
				smi.GetComponent<KBatchedAnimController>().Play("port_open", KAnim.PlayMode.Once, 1f, 0f);
				smi.GetComponent<KBatchedAnimController>().Queue("inert", KAnim.PlayMode.Loop, 1f, 0f);
			});
		this.charging.idle.EventTransition(GameHashes.OnParticleStorageChanged, this.charging.consuming, (TemporalTearOpener.Instance smi) => !smi.GetComponent<HighEnergyParticleStorage>().IsEmpty());
		this.charging.consuming.EventTransition(GameHashes.OnParticleStorageChanged, this.charging.idle, (TemporalTearOpener.Instance smi) => smi.GetComponent<HighEnergyParticleStorage>().IsEmpty()).UpdateTransition(this.ready, (TemporalTearOpener.Instance smi, float dt) => smi.ConsumeParticlesAndCheckComplete(dt), UpdateRate.SIM_200ms, false);
		this.ready.ToggleNotification((TemporalTearOpener.Instance smi) => new Notification(BUILDING.STATUSITEMS.TEMPORAL_TEAR_OPENER_READY.NOTIFICATION, NotificationType.Good, (List<Notification> a, object b) => BUILDING.STATUSITEMS.TEMPORAL_TEAR_OPENER_READY.NOTIFICATION_TOOLTIP, null, false, 0f, null, null, null, true, false, false));
		this.opening_tear_beam_pre.PlayAnim("working_pre", KAnim.PlayMode.Once).OnAnimQueueComplete(this.opening_tear_beam);
		this.opening_tear_beam.Enter(delegate(TemporalTearOpener.Instance smi)
		{
			smi.CreateBeamFX();
		}).PlayAnim("working_loop", KAnim.PlayMode.Loop).ScheduleGoTo(5f, this.opening_tear_finish);
		this.opening_tear_finish.PlayAnim("working_pst").Enter(delegate(TemporalTearOpener.Instance smi)
		{
			smi.OpenTemporalTear();
		});
	}

	// Token: 0x040019D3 RID: 6611
	private const float MIN_SUNLIGHT_EXPOSURE = 15f;

	// Token: 0x040019D4 RID: 6612
	private static StatusItem s_noLosStatus = new StatusItem("Temporal_Tear_Opener_No_Los", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);

	// Token: 0x040019D5 RID: 6613
	private static StatusItem s_insufficient_colonies = TemporalTearOpener.CreateColoniesStatusItem();

	// Token: 0x040019D6 RID: 6614
	private static StatusItem s_noTargetStatus = new StatusItem("Temporal_Tear_Opener_No_Target", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);

	// Token: 0x040019D7 RID: 6615
	private static StatusItem s_progressStatus = TemporalTearOpener.CreateProgressStatusItem();

	// Token: 0x040019D8 RID: 6616
	private TemporalTearOpener.CheckRequirementsState check_requirements;

	// Token: 0x040019D9 RID: 6617
	private TemporalTearOpener.ChargingState charging;

	// Token: 0x040019DA RID: 6618
	private GameStateMachine<TemporalTearOpener, TemporalTearOpener.Instance, IStateMachineTarget, TemporalTearOpener.Def>.State opening_tear_beam_pre;

	// Token: 0x040019DB RID: 6619
	private GameStateMachine<TemporalTearOpener, TemporalTearOpener.Instance, IStateMachineTarget, TemporalTearOpener.Def>.State opening_tear_beam;

	// Token: 0x040019DC RID: 6620
	private GameStateMachine<TemporalTearOpener, TemporalTearOpener.Instance, IStateMachineTarget, TemporalTearOpener.Def>.State opening_tear_finish;

	// Token: 0x040019DD RID: 6621
	private GameStateMachine<TemporalTearOpener, TemporalTearOpener.Instance, IStateMachineTarget, TemporalTearOpener.Def>.State ready;

	// Token: 0x0200130E RID: 4878
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005F5C RID: 24412
		public float consumeRate;

		// Token: 0x04005F5D RID: 24413
		public float numParticlesToOpen;
	}

	// Token: 0x0200130F RID: 4879
	private class ChargingState : GameStateMachine<TemporalTearOpener, TemporalTearOpener.Instance, IStateMachineTarget, TemporalTearOpener.Def>.State
	{
		// Token: 0x04005F5E RID: 24414
		public GameStateMachine<TemporalTearOpener, TemporalTearOpener.Instance, IStateMachineTarget, TemporalTearOpener.Def>.State idle;

		// Token: 0x04005F5F RID: 24415
		public GameStateMachine<TemporalTearOpener, TemporalTearOpener.Instance, IStateMachineTarget, TemporalTearOpener.Def>.State consuming;
	}

	// Token: 0x02001310 RID: 4880
	private class CheckRequirementsState : GameStateMachine<TemporalTearOpener, TemporalTearOpener.Instance, IStateMachineTarget, TemporalTearOpener.Def>.State
	{
		// Token: 0x04005F60 RID: 24416
		public GameStateMachine<TemporalTearOpener, TemporalTearOpener.Instance, IStateMachineTarget, TemporalTearOpener.Def>.State has_target;

		// Token: 0x04005F61 RID: 24417
		public GameStateMachine<TemporalTearOpener, TemporalTearOpener.Instance, IStateMachineTarget, TemporalTearOpener.Def>.State has_los;

		// Token: 0x04005F62 RID: 24418
		public GameStateMachine<TemporalTearOpener, TemporalTearOpener.Instance, IStateMachineTarget, TemporalTearOpener.Def>.State enough_colonies;
	}

	// Token: 0x02001311 RID: 4881
	public new class Instance : GameStateMachine<TemporalTearOpener, TemporalTearOpener.Instance, IStateMachineTarget, TemporalTearOpener.Def>.GameInstance, ISidescreenButtonControl
	{
		// Token: 0x06007C5C RID: 31836 RVA: 0x002CFFA3 File Offset: 0x002CE1A3
		public Instance(IStateMachineTarget master, TemporalTearOpener.Def def)
			: base(master, def)
		{
			this.m_meter = new MeterController(base.gameObject.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Array.Empty<string>());
			EnterTemporalTearSequence.tearOpenerGameObject = base.gameObject;
		}

		// Token: 0x06007C5D RID: 31837 RVA: 0x002CFFE0 File Offset: 0x002CE1E0
		protected override void OnCleanUp()
		{
			if (EnterTemporalTearSequence.tearOpenerGameObject == base.gameObject)
			{
				EnterTemporalTearSequence.tearOpenerGameObject = null;
			}
			base.OnCleanUp();
		}

		// Token: 0x06007C5E RID: 31838 RVA: 0x002D0000 File Offset: 0x002CE200
		public bool HasLineOfSight()
		{
			Extents extents = base.GetComponent<Building>().GetExtents();
			int x = extents.x;
			int num = extents.x + extents.width - 1;
			for (int i = x; i <= num; i++)
			{
				int num2 = Grid.XYToCell(i, extents.y);
				if ((float)Grid.ExposedToSunlight[num2] < 15f)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06007C5F RID: 31839 RVA: 0x002D005D File Offset: 0x002CE25D
		public bool HasSufficientColonies()
		{
			return this.CountColonies() >= EstablishColonies.BASE_COUNT;
		}

		// Token: 0x06007C60 RID: 31840 RVA: 0x002D0070 File Offset: 0x002CE270
		public int CountColonies()
		{
			int num = 0;
			for (int i = 0; i < Components.Telepads.Count; i++)
			{
				Activatable component = Components.Telepads[i].GetComponent<Activatable>();
				if (component == null || component.IsActivated)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06007C61 RID: 31841 RVA: 0x002D00BC File Offset: 0x002CE2BC
		public bool ConsumeParticlesAndCheckComplete(float dt)
		{
			float num = Mathf.Min(dt * base.def.consumeRate, base.def.numParticlesToOpen - this.m_particlesConsumed);
			float num2 = base.GetComponent<HighEnergyParticleStorage>().ConsumeAndGet(num);
			this.m_particlesConsumed += num2;
			this.UpdateMeter();
			return this.m_particlesConsumed >= base.def.numParticlesToOpen;
		}

		// Token: 0x06007C62 RID: 31842 RVA: 0x002D0125 File Offset: 0x002CE325
		public void UpdateMeter()
		{
			this.m_meter.SetPositionPercent(this.GetAmountComplete());
		}

		// Token: 0x06007C63 RID: 31843 RVA: 0x002D0138 File Offset: 0x002CE338
		private float GetAmountComplete()
		{
			return Mathf.Min(this.m_particlesConsumed / base.def.numParticlesToOpen, 1f);
		}

		// Token: 0x06007C64 RID: 31844 RVA: 0x002D0156 File Offset: 0x002CE356
		public float GetPercentComplete()
		{
			return this.GetAmountComplete() * 100f;
		}

		// Token: 0x06007C65 RID: 31845 RVA: 0x002D0164 File Offset: 0x002CE364
		public void CreateBeamFX()
		{
			Vector3 position = base.gameObject.transform.position;
			position.y += 3.25f;
			Quaternion quaternion = Quaternion.Euler(-90f, 90f, 0f);
			Util.KInstantiate(EffectPrefabs.Instance.OpenTemporalTearBeam, position, quaternion, base.gameObject, null, true, 0);
		}

		// Token: 0x06007C66 RID: 31846 RVA: 0x002D01C2 File Offset: 0x002CE3C2
		public void OpenTemporalTear()
		{
			ClusterManager.Instance.GetClusterPOIManager().RevealTemporalTear();
			ClusterManager.Instance.GetClusterPOIManager().OpenTemporalTear(this.GetMyWorldId());
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06007C67 RID: 31847 RVA: 0x002D01E8 File Offset: 0x002CE3E8
		public string SidescreenButtonText
		{
			get
			{
				return BUILDINGS.PREFABS.TEMPORALTEAROPENER.SIDESCREEN.TEXT;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06007C68 RID: 31848 RVA: 0x002D01F4 File Offset: 0x002CE3F4
		public string SidescreenButtonTooltip
		{
			get
			{
				return BUILDINGS.PREFABS.TEMPORALTEAROPENER.SIDESCREEN.TOOLTIP;
			}
		}

		// Token: 0x06007C69 RID: 31849 RVA: 0x002D0200 File Offset: 0x002CE400
		public bool SidescreenEnabled()
		{
			return this.GetCurrentState() == base.sm.ready || DebugHandler.InstantBuildMode;
		}

		// Token: 0x06007C6A RID: 31850 RVA: 0x002D021C File Offset: 0x002CE41C
		public bool SidescreenButtonInteractable()
		{
			return this.GetCurrentState() == base.sm.ready || DebugHandler.InstantBuildMode;
		}

		// Token: 0x06007C6B RID: 31851 RVA: 0x002D0238 File Offset: 0x002CE438
		public void OnSidescreenButtonPressed()
		{
			ConfirmDialogScreen component = GameScreenManager.Instance.StartScreen(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, null, GameScreenManager.UIRenderTarget.ScreenSpaceOverlay).GetComponent<ConfirmDialogScreen>();
			string text = UI.UISIDESCREENS.TEMPORALTEARSIDESCREEN.CONFIRM_POPUP_MESSAGE;
			System.Action action = delegate
			{
				this.FireTemporalTearOpener(base.smi);
			};
			System.Action action2 = delegate
			{
			};
			string text2 = null;
			System.Action action3 = null;
			string text3 = UI.UISIDESCREENS.TEMPORALTEARSIDESCREEN.CONFIRM_POPUP_CONFIRM;
			string text4 = UI.UISIDESCREENS.TEMPORALTEARSIDESCREEN.CONFIRM_POPUP_CANCEL;
			component.PopupConfirmDialog(text, action, action2, text2, action3, UI.UISIDESCREENS.TEMPORALTEARSIDESCREEN.CONFIRM_POPUP_TITLE, text3, text4, null);
		}

		// Token: 0x06007C6C RID: 31852 RVA: 0x002D02C4 File Offset: 0x002CE4C4
		private void FireTemporalTearOpener(TemporalTearOpener.Instance smi)
		{
			smi.GoTo(base.sm.opening_tear_beam_pre);
		}

		// Token: 0x06007C6D RID: 31853 RVA: 0x002D02D7 File Offset: 0x002CE4D7
		public int ButtonSideScreenSortOrder()
		{
			return 20;
		}

		// Token: 0x06007C6E RID: 31854 RVA: 0x002D02DB File Offset: 0x002CE4DB
		public void SetButtonTextOverride(ButtonMenuTextOverride text)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04005F63 RID: 24419
		[Serialize]
		private float m_particlesConsumed;

		// Token: 0x04005F64 RID: 24420
		private MeterController m_meter;
	}
}
