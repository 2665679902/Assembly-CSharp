using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006C5 RID: 1733
public class CreatureLightToggleController : GameStateMachine<CreatureLightToggleController, CreatureLightToggleController.Instance, IStateMachineTarget, CreatureLightToggleController.Def>
{
	// Token: 0x06002F2D RID: 12077 RVA: 0x000F9624 File Offset: 0x000F7824
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.light_on;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.light_off.Enter(delegate(CreatureLightToggleController.Instance smi)
		{
			smi.SwitchLight(false);
		}).TagTransition(GameTags.Creatures.Overcrowded, this.turning_on, true);
		this.turning_off.BatchUpdate(delegate(List<UpdateBucketWithUpdater<CreatureLightToggleController.Instance>.Entry> instances, float time_delta)
		{
			CreatureLightToggleController.Instance.ModifyBrightness(instances, CreatureLightToggleController.Instance.dim, time_delta);
		}, UpdateRate.SIM_200ms).Transition(this.light_off, (CreatureLightToggleController.Instance smi) => smi.IsOff(), UpdateRate.SIM_200ms);
		this.light_on.Enter(delegate(CreatureLightToggleController.Instance smi)
		{
			smi.SwitchLight(true);
		}).TagTransition(GameTags.Creatures.Overcrowded, this.turning_off, false);
		this.turning_on.Enter(delegate(CreatureLightToggleController.Instance smi)
		{
			smi.SwitchLight(true);
		}).BatchUpdate(delegate(List<UpdateBucketWithUpdater<CreatureLightToggleController.Instance>.Entry> instances, float time_delta)
		{
			CreatureLightToggleController.Instance.ModifyBrightness(instances, CreatureLightToggleController.Instance.brighten, time_delta);
		}, UpdateRate.SIM_200ms).Transition(this.light_on, (CreatureLightToggleController.Instance smi) => smi.IsOn(), UpdateRate.SIM_200ms);
	}

	// Token: 0x04001C5E RID: 7262
	private GameStateMachine<CreatureLightToggleController, CreatureLightToggleController.Instance, IStateMachineTarget, CreatureLightToggleController.Def>.State light_off;

	// Token: 0x04001C5F RID: 7263
	private GameStateMachine<CreatureLightToggleController, CreatureLightToggleController.Instance, IStateMachineTarget, CreatureLightToggleController.Def>.State turning_off;

	// Token: 0x04001C60 RID: 7264
	private GameStateMachine<CreatureLightToggleController, CreatureLightToggleController.Instance, IStateMachineTarget, CreatureLightToggleController.Def>.State light_on;

	// Token: 0x04001C61 RID: 7265
	private GameStateMachine<CreatureLightToggleController, CreatureLightToggleController.Instance, IStateMachineTarget, CreatureLightToggleController.Def>.State turning_on;

	// Token: 0x020013A8 RID: 5032
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020013A9 RID: 5033
	public new class Instance : GameStateMachine<CreatureLightToggleController, CreatureLightToggleController.Instance, IStateMachineTarget, CreatureLightToggleController.Def>.GameInstance
	{
		// Token: 0x06007E8B RID: 32395 RVA: 0x002D95DB File Offset: 0x002D77DB
		public Instance(IStateMachineTarget master, CreatureLightToggleController.Def def)
			: base(master, def)
		{
			this.light = master.GetComponent<Light2D>();
			this.originalLux = this.light.Lux;
			this.originalRange = this.light.Range;
		}

		// Token: 0x06007E8C RID: 32396 RVA: 0x002D9613 File Offset: 0x002D7813
		public void SwitchLight(bool on)
		{
			this.light.enabled = on;
		}

		// Token: 0x06007E8D RID: 32397 RVA: 0x002D9624 File Offset: 0x002D7824
		public static void ModifyBrightness(List<UpdateBucketWithUpdater<CreatureLightToggleController.Instance>.Entry> instances, CreatureLightToggleController.Instance.ModifyLuxDelegate modify_lux, float time_delta)
		{
			CreatureLightToggleController.Instance.modify_brightness_job.Reset(null);
			for (int num = 0; num != instances.Count; num++)
			{
				UpdateBucketWithUpdater<CreatureLightToggleController.Instance>.Entry entry = instances[num];
				entry.lastUpdateTime = 0f;
				instances[num] = entry;
				CreatureLightToggleController.Instance data = entry.data;
				modify_lux(data, time_delta);
				data.light.Range = data.originalRange * (float)data.light.Lux / (float)data.originalLux;
				data.light.RefreshShapeAndPosition();
				if (data.light.RefreshShapeAndPosition() != Light2D.RefreshResult.None)
				{
					CreatureLightToggleController.Instance.modify_brightness_job.Add(new CreatureLightToggleController.Instance.ModifyBrightnessTask(data.light.emitter));
				}
			}
			GlobalJobManager.Run(CreatureLightToggleController.Instance.modify_brightness_job);
			for (int num2 = 0; num2 != CreatureLightToggleController.Instance.modify_brightness_job.Count; num2++)
			{
				CreatureLightToggleController.Instance.modify_brightness_job.GetWorkItem(num2).Finish();
			}
			CreatureLightToggleController.Instance.modify_brightness_job.Reset(null);
		}

		// Token: 0x06007E8E RID: 32398 RVA: 0x002D9715 File Offset: 0x002D7915
		public bool IsOff()
		{
			return this.light.Lux == 0;
		}

		// Token: 0x06007E8F RID: 32399 RVA: 0x002D9725 File Offset: 0x002D7925
		public bool IsOn()
		{
			return this.light.Lux >= this.originalLux;
		}

		// Token: 0x04006141 RID: 24897
		private const float DIM_TIME = 25f;

		// Token: 0x04006142 RID: 24898
		private const float GLOW_TIME = 15f;

		// Token: 0x04006143 RID: 24899
		private int originalLux;

		// Token: 0x04006144 RID: 24900
		private float originalRange;

		// Token: 0x04006145 RID: 24901
		private Light2D light;

		// Token: 0x04006146 RID: 24902
		private static WorkItemCollection<CreatureLightToggleController.Instance.ModifyBrightnessTask, object> modify_brightness_job = new WorkItemCollection<CreatureLightToggleController.Instance.ModifyBrightnessTask, object>();

		// Token: 0x04006147 RID: 24903
		public static CreatureLightToggleController.Instance.ModifyLuxDelegate dim = delegate(CreatureLightToggleController.Instance instance, float time_delta)
		{
			float num = (float)instance.originalLux / 25f;
			instance.light.Lux = Mathf.FloorToInt(Mathf.Max(0f, (float)instance.light.Lux - num * time_delta));
		};

		// Token: 0x04006148 RID: 24904
		public static CreatureLightToggleController.Instance.ModifyLuxDelegate brighten = delegate(CreatureLightToggleController.Instance instance, float time_delta)
		{
			float num2 = (float)instance.originalLux / 15f;
			instance.light.Lux = Mathf.CeilToInt(Mathf.Min((float)instance.originalLux, (float)instance.light.Lux + num2 * time_delta));
		};

		// Token: 0x02002038 RID: 8248
		private struct ModifyBrightnessTask : IWorkItem<object>
		{
			// Token: 0x0600A2DB RID: 41691 RVA: 0x00345EC0 File Offset: 0x003440C0
			public ModifyBrightnessTask(LightGridManager.LightGridEmitter emitter)
			{
				this.emitter = emitter;
				emitter.RemoveFromGrid();
			}

			// Token: 0x0600A2DC RID: 41692 RVA: 0x00345ECF File Offset: 0x003440CF
			public void Run(object context)
			{
				this.emitter.UpdateLitCells();
			}

			// Token: 0x0600A2DD RID: 41693 RVA: 0x00345EDC File Offset: 0x003440DC
			public void Finish()
			{
				this.emitter.AddToGrid(false);
			}

			// Token: 0x04008F90 RID: 36752
			private LightGridManager.LightGridEmitter emitter;
		}

		// Token: 0x02002039 RID: 8249
		// (Invoke) Token: 0x0600A2DF RID: 41695
		public delegate void ModifyLuxDelegate(CreatureLightToggleController.Instance instance, float time_delta);
	}
}
