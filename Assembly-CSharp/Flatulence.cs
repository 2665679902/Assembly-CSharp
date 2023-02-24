﻿using System;
using TUNING;
using UnityEngine;

// Token: 0x0200077A RID: 1914
[SkipSaveFileSerialization]
public class Flatulence : StateMachineComponent<Flatulence.StatesInstance>
{
	// Token: 0x060034B0 RID: 13488 RVA: 0x0011C374 File Offset: 0x0011A574
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x060034B1 RID: 13489 RVA: 0x0011C384 File Offset: 0x0011A584
	private void Emit(object data)
	{
		GameObject gameObject = (GameObject)data;
		float value = Db.Get().Amounts.Temperature.Lookup(this).value;
		Equippable equippable = base.GetComponent<SuitEquipper>().IsWearingAirtightSuit();
		if (equippable != null)
		{
			equippable.GetComponent<Storage>().AddGasChunk(SimHashes.Methane, 0.1f, value, byte.MaxValue, 0, false, true);
		}
		else
		{
			Components.Cmps<MinionIdentity> liveMinionIdentities = Components.LiveMinionIdentities;
			Vector2 vector = gameObject.transform.GetPosition();
			for (int i = 0; i < liveMinionIdentities.Count; i++)
			{
				MinionIdentity minionIdentity = liveMinionIdentities[i];
				if (minionIdentity.gameObject != gameObject.gameObject)
				{
					Vector2 vector2 = minionIdentity.transform.GetPosition();
					if (Vector2.SqrMagnitude(vector - vector2) <= 2.25f)
					{
						minionIdentity.Trigger(508119890, Strings.Get("STRINGS.DUPLICANTS.DISEASES.PUTRIDODOUR.CRINGE_EFFECT").String);
						minionIdentity.gameObject.GetSMI<ThoughtGraph.Instance>().AddThought(Db.Get().Thoughts.PutridOdour);
					}
				}
			}
			SimMessages.AddRemoveSubstance(Grid.PosToCell(gameObject.transform.GetPosition()), SimHashes.Methane, CellEventLogger.Instance.ElementConsumerSimUpdate, 0.1f, value, byte.MaxValue, 0, true, -1);
			KBatchedAnimController kbatchedAnimController = FXHelpers.CreateEffect("odor_fx_kanim", gameObject.transform.GetPosition(), gameObject.transform, true, Grid.SceneLayer.Front, false);
			kbatchedAnimController.Play(Flatulence.WorkLoopAnims, KAnim.PlayMode.Once);
			kbatchedAnimController.destroyOnAnimComplete = true;
		}
		GameObject gameObject2 = gameObject;
		bool flag = SoundEvent.ObjectIsSelectedAndVisible(gameObject2);
		Vector3 vector3 = gameObject2.GetComponent<Transform>().GetPosition();
		vector3.z = 0f;
		float num = 1f;
		if (flag)
		{
			vector3 = SoundEvent.AudioHighlightListenerPosition(vector3);
			num = SoundEvent.GetVolume(flag);
		}
		else
		{
			vector3.z = 0f;
		}
		KFMOD.PlayOneShot(GlobalAssets.GetSound("Dupe_Flatulence", false), vector3, num);
	}

	// Token: 0x0400209E RID: 8350
	private const float EmitMass = 0.1f;

	// Token: 0x0400209F RID: 8351
	private const SimHashes EmitElement = SimHashes.Methane;

	// Token: 0x040020A0 RID: 8352
	private const float EmissionRadius = 1.5f;

	// Token: 0x040020A1 RID: 8353
	private const float MaxDistanceSq = 2.25f;

	// Token: 0x040020A2 RID: 8354
	private static readonly HashedString[] WorkLoopAnims = new HashedString[] { "working_pre", "working_loop", "working_pst" };

	// Token: 0x02001472 RID: 5234
	public class StatesInstance : GameStateMachine<Flatulence.States, Flatulence.StatesInstance, Flatulence, object>.GameInstance
	{
		// Token: 0x0600810B RID: 33035 RVA: 0x002E0A8B File Offset: 0x002DEC8B
		public StatesInstance(Flatulence master)
			: base(master)
		{
		}
	}

	// Token: 0x02001473 RID: 5235
	public class States : GameStateMachine<Flatulence.States, Flatulence.StatesInstance, Flatulence>
	{
		// Token: 0x0600810C RID: 33036 RVA: 0x002E0A94 File Offset: 0x002DEC94
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.root.TagTransition(GameTags.Dead, null, false);
			this.idle.Enter("ScheduleNextFart", delegate(Flatulence.StatesInstance smi)
			{
				smi.ScheduleGoTo(this.GetNewInterval(), this.emit);
			});
			this.emit.Enter("Fart", delegate(Flatulence.StatesInstance smi)
			{
				smi.master.Emit(smi.master.gameObject);
			}).ToggleExpression(Db.Get().Expressions.Relief, null).ScheduleGoTo(3f, this.idle);
		}

		// Token: 0x0600810D RID: 33037 RVA: 0x002E0B2E File Offset: 0x002DED2E
		private float GetNewInterval()
		{
			return Mathf.Min(Mathf.Max(Util.GaussianRandom(TRAITS.FLATULENCE_EMIT_INTERVAL_MAX - TRAITS.FLATULENCE_EMIT_INTERVAL_MIN, 1f), TRAITS.FLATULENCE_EMIT_INTERVAL_MIN), TRAITS.FLATULENCE_EMIT_INTERVAL_MAX);
		}

		// Token: 0x04006369 RID: 25449
		public GameStateMachine<Flatulence.States, Flatulence.StatesInstance, Flatulence, object>.State idle;

		// Token: 0x0400636A RID: 25450
		public GameStateMachine<Flatulence.States, Flatulence.StatesInstance, Flatulence, object>.State emit;
	}
}
