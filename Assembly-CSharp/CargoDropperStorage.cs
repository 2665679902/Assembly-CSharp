using System;
using UnityEngine;

// Token: 0x0200067B RID: 1659
public class CargoDropperStorage : GameStateMachine<CargoDropperStorage, CargoDropperStorage.StatesInstance, IStateMachineTarget, CargoDropperStorage.Def>
{
	// Token: 0x06002CBA RID: 11450 RVA: 0x000EA779 File Offset: 0x000E8979
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.EventHandler(GameHashes.JettisonCargo, delegate(CargoDropperStorage.StatesInstance smi, object data)
		{
			smi.JettisonCargo(data);
		});
	}

	// Token: 0x02001341 RID: 4929
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005FFE RID: 24574
		public Vector3 dropOffset;
	}

	// Token: 0x02001342 RID: 4930
	public class StatesInstance : GameStateMachine<CargoDropperStorage, CargoDropperStorage.StatesInstance, IStateMachineTarget, CargoDropperStorage.Def>.GameInstance
	{
		// Token: 0x06007D24 RID: 32036 RVA: 0x002D32A0 File Offset: 0x002D14A0
		public StatesInstance(IStateMachineTarget master, CargoDropperStorage.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06007D25 RID: 32037 RVA: 0x002D32AC File Offset: 0x002D14AC
		public void JettisonCargo(object data)
		{
			Vector3 vector = base.master.transform.GetPosition() + base.def.dropOffset;
			Storage component = base.GetComponent<Storage>();
			if (component != null)
			{
				GameObject gameObject = component.FindFirst("ScoutRover");
				if (gameObject != null)
				{
					component.Drop(gameObject, true);
					Vector3 position = base.master.transform.GetPosition();
					position.z = Grid.GetLayerZ(Grid.SceneLayer.Creatures);
					gameObject.transform.SetPosition(position);
					ChoreProvider component2 = gameObject.GetComponent<ChoreProvider>();
					if (component2 != null)
					{
						KBatchedAnimController component3 = gameObject.GetComponent<KBatchedAnimController>();
						if (component3 != null)
						{
							component3.Play("enter", KAnim.PlayMode.Once, 1f, 0f);
						}
						new EmoteChore(component2, Db.Get().ChoreTypes.EmoteHighPriority, null, new HashedString[] { "enter" }, KAnim.PlayMode.Once, false);
					}
					gameObject.GetMyWorld().SetRoverLanded();
				}
				component.DropAll(vector, false, false, default(Vector3), true, null);
			}
		}
	}
}
