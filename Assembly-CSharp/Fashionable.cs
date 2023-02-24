using System;

// Token: 0x02000860 RID: 2144
[SkipSaveFileSerialization]
public class Fashionable : StateMachineComponent<Fashionable.StatesInstance>
{
	// Token: 0x06003DA4 RID: 15780 RVA: 0x00158A7C File Offset: 0x00156C7C
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x06003DA5 RID: 15781 RVA: 0x00158A8C File Offset: 0x00156C8C
	protected bool IsUncomfortable()
	{
		ClothingWearer component = base.GetComponent<ClothingWearer>();
		return component != null && component.currentClothing.decorMod <= 0;
	}

	// Token: 0x02001623 RID: 5667
	public class StatesInstance : GameStateMachine<Fashionable.States, Fashionable.StatesInstance, Fashionable, object>.GameInstance
	{
		// Token: 0x060086D8 RID: 34520 RVA: 0x002F017E File Offset: 0x002EE37E
		public StatesInstance(Fashionable master)
			: base(master)
		{
		}
	}

	// Token: 0x02001624 RID: 5668
	public class States : GameStateMachine<Fashionable.States, Fashionable.StatesInstance, Fashionable>
	{
		// Token: 0x060086D9 RID: 34521 RVA: 0x002F0188 File Offset: 0x002EE388
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.satisfied;
			this.root.EventHandler(GameHashes.EquippedItemEquipper, delegate(Fashionable.StatesInstance smi)
			{
				if (smi.master.IsUncomfortable())
				{
					smi.GoTo(this.suffering);
					return;
				}
				smi.GoTo(this.satisfied);
			}).EventHandler(GameHashes.UnequippedItemEquipper, delegate(Fashionable.StatesInstance smi)
			{
				if (smi.master.IsUncomfortable())
				{
					smi.GoTo(this.suffering);
					return;
				}
				smi.GoTo(this.satisfied);
			});
			this.suffering.AddEffect("UnfashionableClothing").ToggleExpression(Db.Get().Expressions.Uncomfortable, null);
			this.satisfied.DoNothing();
		}

		// Token: 0x04006921 RID: 26913
		public GameStateMachine<Fashionable.States, Fashionable.StatesInstance, Fashionable, object>.State satisfied;

		// Token: 0x04006922 RID: 26914
		public GameStateMachine<Fashionable.States, Fashionable.StatesInstance, Fashionable, object>.State suffering;
	}
}
