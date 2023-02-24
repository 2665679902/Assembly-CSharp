using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000918 RID: 2328
public class SetLocker : StateMachineComponent<SetLocker.StatesInstance>, ISidescreenButtonControl
{
	// Token: 0x060043B4 RID: 17332 RVA: 0x0017E3F3 File Offset: 0x0017C5F3
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x060043B5 RID: 17333 RVA: 0x0017E3FB File Offset: 0x0017C5FB
	public void ChooseContents()
	{
		this.contents = this.possible_contents_ids[UnityEngine.Random.Range(0, this.possible_contents_ids.GetLength(0))];
	}

	// Token: 0x060043B6 RID: 17334 RVA: 0x0017E41C File Offset: 0x0017C61C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x060043B7 RID: 17335 RVA: 0x0017E430 File Offset: 0x0017C630
	public void DropContents()
	{
		if (this.contents == null)
		{
			return;
		}
		for (int i = 0; i < this.contents.Length; i++)
		{
			Scenario.SpawnPrefab(Grid.PosToCell(base.gameObject), this.dropOffset.x, this.dropOffset.y, this.contents[i], Grid.SceneLayer.Front).SetActive(true);
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, Assets.GetPrefab(this.contents[i].ToTag()).GetProperName(), base.smi.master.transform, 1.5f, false);
		}
		if (DlcManager.IsExpansion1Active() && this.numDataBanks.Length >= 2)
		{
			int num = UnityEngine.Random.Range(this.numDataBanks[0], this.numDataBanks[1]);
			for (int j = 0; j <= num; j++)
			{
				Scenario.SpawnPrefab(Grid.PosToCell(base.gameObject), this.dropOffset.x, this.dropOffset.y, "OrbitalResearchDatabank", Grid.SceneLayer.Front).SetActive(true);
				PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, Assets.GetPrefab("OrbitalResearchDatabank".ToTag()).GetProperName(), base.smi.master.transform, 1.5f, false);
			}
		}
		base.gameObject.Trigger(-372600542, this);
	}

	// Token: 0x060043B8 RID: 17336 RVA: 0x0017E591 File Offset: 0x0017C791
	private void OnClickOpen()
	{
		this.ActivateChore(null);
	}

	// Token: 0x060043B9 RID: 17337 RVA: 0x0017E59A File Offset: 0x0017C79A
	private void OnClickCancel()
	{
		this.CancelChore(null);
	}

	// Token: 0x060043BA RID: 17338 RVA: 0x0017E5A4 File Offset: 0x0017C7A4
	public void ActivateChore(object param = null)
	{
		if (this.chore != null)
		{
			return;
		}
		base.GetComponent<Workable>().SetWorkTime(1.5f);
		this.chore = new WorkChore<Workable>(Db.Get().ChoreTypes.EmptyStorage, this, null, true, delegate(Chore o)
		{
			this.CompleteChore();
		}, null, null, true, null, false, true, Assets.GetAnim(this.overrideAnim), false, true, true, PriorityScreen.PriorityClass.high, 5, false, true);
	}

	// Token: 0x060043BB RID: 17339 RVA: 0x0017E610 File Offset: 0x0017C810
	public void CancelChore(object param = null)
	{
		if (this.chore == null)
		{
			return;
		}
		this.chore.Cancel("User cancelled");
		this.chore = null;
	}

	// Token: 0x060043BC RID: 17340 RVA: 0x0017E632 File Offset: 0x0017C832
	private void CompleteChore()
	{
		this.used = true;
		base.smi.GoTo(base.smi.sm.open);
		this.chore = null;
		Game.Instance.userMenu.Refresh(base.gameObject);
	}

	// Token: 0x170004CB RID: 1227
	// (get) Token: 0x060043BD RID: 17341 RVA: 0x0017E672 File Offset: 0x0017C872
	public string SidescreenButtonText
	{
		get
		{
			return (this.chore == null) ? UI.USERMENUACTIONS.OPENPOI.NAME : UI.USERMENUACTIONS.OPENPOI.NAME_OFF;
		}
	}

	// Token: 0x170004CC RID: 1228
	// (get) Token: 0x060043BE RID: 17342 RVA: 0x0017E68D File Offset: 0x0017C88D
	public string SidescreenButtonTooltip
	{
		get
		{
			return (this.chore == null) ? UI.USERMENUACTIONS.OPENPOI.TOOLTIP : UI.USERMENUACTIONS.OPENPOI.TOOLTIP_OFF;
		}
	}

	// Token: 0x060043BF RID: 17343 RVA: 0x0017E6A8 File Offset: 0x0017C8A8
	public bool SidescreenEnabled()
	{
		return true;
	}

	// Token: 0x060043C0 RID: 17344 RVA: 0x0017E6AB File Offset: 0x0017C8AB
	public void OnSidescreenButtonPressed()
	{
		if (this.chore == null)
		{
			this.OnClickOpen();
			return;
		}
		this.OnClickCancel();
	}

	// Token: 0x060043C1 RID: 17345 RVA: 0x0017E6C2 File Offset: 0x0017C8C2
	public bool SidescreenButtonInteractable()
	{
		return !this.used;
	}

	// Token: 0x060043C2 RID: 17346 RVA: 0x0017E6CD File Offset: 0x0017C8CD
	public int ButtonSideScreenSortOrder()
	{
		return 20;
	}

	// Token: 0x060043C3 RID: 17347 RVA: 0x0017E6D1 File Offset: 0x0017C8D1
	public void SetButtonTextOverride(ButtonMenuTextOverride text)
	{
		throw new NotImplementedException();
	}

	// Token: 0x04002D35 RID: 11573
	public string[][] possible_contents_ids;

	// Token: 0x04002D36 RID: 11574
	public string machineSound;

	// Token: 0x04002D37 RID: 11575
	public string overrideAnim;

	// Token: 0x04002D38 RID: 11576
	public Vector2I dropOffset = Vector2I.zero;

	// Token: 0x04002D39 RID: 11577
	public int[] numDataBanks;

	// Token: 0x04002D3A RID: 11578
	[Serialize]
	private string[] contents;

	// Token: 0x04002D3B RID: 11579
	[Serialize]
	private bool used;

	// Token: 0x04002D3C RID: 11580
	private Chore chore;

	// Token: 0x020016F0 RID: 5872
	public class StatesInstance : GameStateMachine<SetLocker.States, SetLocker.StatesInstance, SetLocker, object>.GameInstance
	{
		// Token: 0x0600891A RID: 35098 RVA: 0x002F79F2 File Offset: 0x002F5BF2
		public StatesInstance(SetLocker master)
			: base(master)
		{
		}
	}

	// Token: 0x020016F1 RID: 5873
	public class States : GameStateMachine<SetLocker.States, SetLocker.StatesInstance, SetLocker>
	{
		// Token: 0x0600891B RID: 35099 RVA: 0x002F79FC File Offset: 0x002F5BFC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.closed;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.closed.PlayAnim("on").Enter(delegate(SetLocker.StatesInstance smi)
			{
				if (smi.master.machineSound != null)
				{
					LoopingSounds component = smi.master.GetComponent<LoopingSounds>();
					if (component != null)
					{
						component.StartSound(GlobalAssets.GetSound(smi.master.machineSound, false));
					}
				}
			});
			this.open.PlayAnim("working").OnAnimQueueComplete(this.off).Exit(delegate(SetLocker.StatesInstance smi)
			{
				smi.master.DropContents();
			});
			this.off.PlayAnim("off").Enter(delegate(SetLocker.StatesInstance smi)
			{
				if (smi.master.machineSound != null)
				{
					LoopingSounds component2 = smi.master.GetComponent<LoopingSounds>();
					if (component2 != null)
					{
						component2.StopSound(GlobalAssets.GetSound(smi.master.machineSound, false));
					}
				}
			});
		}

		// Token: 0x04006B6D RID: 27501
		public GameStateMachine<SetLocker.States, SetLocker.StatesInstance, SetLocker, object>.State closed;

		// Token: 0x04006B6E RID: 27502
		public GameStateMachine<SetLocker.States, SetLocker.StatesInstance, SetLocker, object>.State open;

		// Token: 0x04006B6F RID: 27503
		public GameStateMachine<SetLocker.States, SetLocker.StatesInstance, SetLocker, object>.State off;
	}
}
