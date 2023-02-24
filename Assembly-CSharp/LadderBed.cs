using System;
using System.Collections.Generic;
using FMOD.Studio;

// Token: 0x020005D7 RID: 1495
public class LadderBed : GameStateMachine<LadderBed, LadderBed.Instance, IStateMachineTarget, LadderBed.Def>
{
	// Token: 0x06002553 RID: 9555 RVA: 0x000C9C02 File Offset: 0x000C7E02
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
	}

	// Token: 0x040015B0 RID: 5552
	public static string lightBedShakeSoundPath = GlobalAssets.GetSound("LadderBed_LightShake", false);

	// Token: 0x040015B1 RID: 5553
	public static string noDupeBedShakeSoundPath = GlobalAssets.GetSound("LadderBed_Shake", false);

	// Token: 0x040015B2 RID: 5554
	public static string LADDER_BED_COUNT_BELOW_PARAMETER = "bed_count";

	// Token: 0x0200122F RID: 4655
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005D39 RID: 23865
		public CellOffset[] offsets;
	}

	// Token: 0x02001230 RID: 4656
	public new class Instance : GameStateMachine<LadderBed, LadderBed.Instance, IStateMachineTarget, LadderBed.Def>.GameInstance
	{
		// Token: 0x06007954 RID: 31060 RVA: 0x002C3214 File Offset: 0x002C1414
		public Instance(IStateMachineTarget master, LadderBed.Def def)
			: base(master, def)
		{
			ScenePartitionerLayer scenePartitionerLayer = GameScenePartitioner.Instance.objectLayers[40];
			this.m_cell = Grid.PosToCell(master.gameObject);
			foreach (CellOffset cellOffset in def.offsets)
			{
				int num = Grid.OffsetCell(this.m_cell, cellOffset);
				if (Grid.IsValidCell(this.m_cell) && Grid.IsValidCell(num))
				{
					this.m_partitionEntires.Add(GameScenePartitioner.Instance.Add("LadderBed.Constructor", base.gameObject, num, GameScenePartitioner.Instance.pickupablesChangedLayer, new Action<object>(this.OnMoverChanged)));
					this.OnMoverChanged(null);
				}
			}
			AttachableBuilding attachable = this.m_attachable;
			attachable.onAttachmentNetworkChanged = (Action<object>)Delegate.Combine(attachable.onAttachmentNetworkChanged, new Action<object>(this.OnAttachmentChanged));
			this.OnAttachmentChanged(null);
			base.Subscribe(-717201811, new Action<object>(this.OnSleepDisturbedByMovement));
			master.GetComponent<KAnimControllerBase>().GetLayering().GetLink()
				.syncTint = false;
		}

		// Token: 0x06007955 RID: 31061 RVA: 0x002C332C File Offset: 0x002C152C
		private void OnSleepDisturbedByMovement(object obj)
		{
			base.GetComponent<KAnimControllerBase>().Play("interrupt_light", KAnim.PlayMode.Once, 1f, 0f);
			EventInstance eventInstance = SoundEvent.BeginOneShot(LadderBed.lightBedShakeSoundPath, base.smi.transform.GetPosition(), 1f, false);
			eventInstance.setParameterByName(LadderBed.LADDER_BED_COUNT_BELOW_PARAMETER, (float)this.numBelow, false);
			SoundEvent.EndOneShot(eventInstance);
		}

		// Token: 0x06007956 RID: 31062 RVA: 0x002C3396 File Offset: 0x002C1596
		private void OnAttachmentChanged(object data)
		{
			this.numBelow = AttachableBuilding.CountAttachedBelow(this.m_attachable);
		}

		// Token: 0x06007957 RID: 31063 RVA: 0x002C33AC File Offset: 0x002C15AC
		private void OnMoverChanged(object obj)
		{
			Pickupable pickupable = obj as Pickupable;
			if (pickupable != null && pickupable.gameObject != null && pickupable.HasTag(GameTags.Minion) && pickupable.GetComponent<Navigator>().CurrentNavType == NavType.Ladder)
			{
				if (this.m_sleepable.worker == null)
				{
					base.GetComponent<KAnimControllerBase>().Play("interrupt_light_nodupe", KAnim.PlayMode.Once, 1f, 0f);
					EventInstance eventInstance = SoundEvent.BeginOneShot(LadderBed.noDupeBedShakeSoundPath, base.smi.transform.GetPosition(), 1f, false);
					eventInstance.setParameterByName(LadderBed.LADDER_BED_COUNT_BELOW_PARAMETER, (float)this.numBelow, false);
					SoundEvent.EndOneShot(eventInstance);
					return;
				}
				if (pickupable.gameObject != this.m_sleepable.worker.gameObject)
				{
					this.m_sleepable.worker.Trigger(-717201811, null);
				}
			}
		}

		// Token: 0x06007958 RID: 31064 RVA: 0x002C34A4 File Offset: 0x002C16A4
		protected override void OnCleanUp()
		{
			foreach (HandleVector<int>.Handle handle in this.m_partitionEntires)
			{
				GameScenePartitioner.Instance.Free(ref handle);
			}
			AttachableBuilding attachable = this.m_attachable;
			attachable.onAttachmentNetworkChanged = (Action<object>)Delegate.Remove(attachable.onAttachmentNetworkChanged, new Action<object>(this.OnAttachmentChanged));
			base.OnCleanUp();
		}

		// Token: 0x04005D3A RID: 23866
		private List<HandleVector<int>.Handle> m_partitionEntires = new List<HandleVector<int>.Handle>();

		// Token: 0x04005D3B RID: 23867
		private int m_cell;

		// Token: 0x04005D3C RID: 23868
		[MyCmpGet]
		private Ownable m_ownable;

		// Token: 0x04005D3D RID: 23869
		[MyCmpGet]
		private Sleepable m_sleepable;

		// Token: 0x04005D3E RID: 23870
		[MyCmpGet]
		private AttachableBuilding m_attachable;

		// Token: 0x04005D3F RID: 23871
		private int numBelow;
	}
}
