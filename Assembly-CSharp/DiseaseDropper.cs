using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020006CD RID: 1741
public class DiseaseDropper : GameStateMachine<DiseaseDropper, DiseaseDropper.Instance, IStateMachineTarget, DiseaseDropper.Def>
{
	// Token: 0x06002F5A RID: 12122 RVA: 0x000FA218 File Offset: 0x000F8418
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.stopped;
		this.root.EventHandler(GameHashes.BurstEmitDisease, delegate(DiseaseDropper.Instance smi)
		{
			smi.DropSingleEmit();
		});
		this.working.TagTransition(GameTags.PreventEmittingDisease, this.stopped, false).Update(delegate(DiseaseDropper.Instance smi, float dt)
		{
			smi.DropPeriodic(dt);
		}, UpdateRate.SIM_200ms, false);
		this.stopped.TagTransition(GameTags.PreventEmittingDisease, this.working, true);
	}

	// Token: 0x04001C75 RID: 7285
	public GameStateMachine<DiseaseDropper, DiseaseDropper.Instance, IStateMachineTarget, DiseaseDropper.Def>.State working;

	// Token: 0x04001C76 RID: 7286
	public GameStateMachine<DiseaseDropper, DiseaseDropper.Instance, IStateMachineTarget, DiseaseDropper.Def>.State stopped;

	// Token: 0x04001C77 RID: 7287
	public StateMachine<DiseaseDropper, DiseaseDropper.Instance, IStateMachineTarget, DiseaseDropper.Def>.Signal cellChangedSignal;

	// Token: 0x020013BA RID: 5050
	public class Def : StateMachine.BaseDef, IGameObjectEffectDescriptor
	{
		// Token: 0x06007EC5 RID: 32453 RVA: 0x002D9DAC File Offset: 0x002D7FAC
		public List<Descriptor> GetDescriptors(GameObject go)
		{
			List<Descriptor> list = new List<Descriptor>();
			if (this.singleEmitQuantity > 0)
			{
				list.Add(new Descriptor(UI.UISIDESCREENS.PLANTERSIDESCREEN.DISEASE_DROPPER_BURST.Replace("{Disease}", GameUtil.GetFormattedDiseaseName(this.diseaseIdx, false)).Replace("{DiseaseAmount}", GameUtil.GetFormattedDiseaseAmount(this.singleEmitQuantity, GameUtil.TimeSlice.None)), UI.UISIDESCREENS.PLANTERSIDESCREEN.TOOLTIPS.DISEASE_DROPPER_BURST.Replace("{Disease}", GameUtil.GetFormattedDiseaseName(this.diseaseIdx, false)).Replace("{DiseaseAmount}", GameUtil.GetFormattedDiseaseAmount(this.singleEmitQuantity, GameUtil.TimeSlice.None)), Descriptor.DescriptorType.Effect, false));
			}
			if (this.averageEmitPerSecond > 0)
			{
				list.Add(new Descriptor(UI.UISIDESCREENS.PLANTERSIDESCREEN.DISEASE_DROPPER_CONSTANT.Replace("{Disease}", GameUtil.GetFormattedDiseaseName(this.diseaseIdx, false)).Replace("{DiseaseAmount}", GameUtil.GetFormattedDiseaseAmount(this.averageEmitPerSecond, GameUtil.TimeSlice.PerSecond)), UI.UISIDESCREENS.PLANTERSIDESCREEN.TOOLTIPS.DISEASE_DROPPER_CONSTANT.Replace("{Disease}", GameUtil.GetFormattedDiseaseName(this.diseaseIdx, false)).Replace("{DiseaseAmount}", GameUtil.GetFormattedDiseaseAmount(this.averageEmitPerSecond, GameUtil.TimeSlice.PerSecond)), Descriptor.DescriptorType.Effect, false));
			}
			return list;
		}

		// Token: 0x04006167 RID: 24935
		public byte diseaseIdx = byte.MaxValue;

		// Token: 0x04006168 RID: 24936
		public int singleEmitQuantity;

		// Token: 0x04006169 RID: 24937
		public int averageEmitPerSecond;

		// Token: 0x0400616A RID: 24938
		public float emitFrequency = 1f;
	}

	// Token: 0x020013BB RID: 5051
	public new class Instance : GameStateMachine<DiseaseDropper, DiseaseDropper.Instance, IStateMachineTarget, DiseaseDropper.Def>.GameInstance
	{
		// Token: 0x06007EC7 RID: 32455 RVA: 0x002D9ECE File Offset: 0x002D80CE
		public Instance(IStateMachineTarget master, DiseaseDropper.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06007EC8 RID: 32456 RVA: 0x002D9ED8 File Offset: 0x002D80D8
		public bool ShouldDropDisease()
		{
			return true;
		}

		// Token: 0x06007EC9 RID: 32457 RVA: 0x002D9EDB File Offset: 0x002D80DB
		public void DropSingleEmit()
		{
			this.DropDisease(base.def.diseaseIdx, base.def.singleEmitQuantity);
		}

		// Token: 0x06007ECA RID: 32458 RVA: 0x002D9EFC File Offset: 0x002D80FC
		public void DropPeriodic(float dt)
		{
			this.timeSinceLastDrop += dt;
			if (base.def.averageEmitPerSecond > 0 && base.def.emitFrequency > 0f)
			{
				while (this.timeSinceLastDrop > base.def.emitFrequency)
				{
					this.DropDisease(base.def.diseaseIdx, (int)((float)base.def.averageEmitPerSecond * base.def.emitFrequency));
					this.timeSinceLastDrop -= base.def.emitFrequency;
				}
			}
		}

		// Token: 0x06007ECB RID: 32459 RVA: 0x002D9F90 File Offset: 0x002D8190
		public void DropDisease(byte disease_idx, int disease_count)
		{
			if (disease_count <= 0 || disease_idx == 255)
			{
				return;
			}
			int num = Grid.PosToCell(base.transform.GetPosition());
			if (!Grid.IsValidCell(num))
			{
				return;
			}
			SimMessages.ModifyDiseaseOnCell(num, disease_idx, disease_count);
		}

		// Token: 0x06007ECC RID: 32460 RVA: 0x002D9FCC File Offset: 0x002D81CC
		public bool IsValidDropCell()
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			return Grid.IsValidCell(num) && Grid.IsGas(num) && Grid.Mass[num] <= 1f;
		}

		// Token: 0x0400616B RID: 24939
		private float timeSinceLastDrop;
	}
}
