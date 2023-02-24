using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000582 RID: 1410
public class CarePackage : StateMachineComponent<CarePackage.SMInstance>
{
	// Token: 0x06002252 RID: 8786 RVA: 0x000BA2A8 File Offset: 0x000B84A8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		if (this.info != null)
		{
			this.SetAnimToInfo();
		}
		this.reactable = this.CreateReactable();
	}

	// Token: 0x06002253 RID: 8787 RVA: 0x000BA2D8 File Offset: 0x000B84D8
	public Reactable CreateReactable()
	{
		return new EmoteReactable(base.gameObject, "UpgradeFX", Db.Get().ChoreTypes.Emote, 15, 8, 0f, 20f, float.PositiveInfinity, 0f).SetEmote(Db.Get().Emotes.Minion.Cheer);
	}

	// Token: 0x06002254 RID: 8788 RVA: 0x000BA339 File Offset: 0x000B8539
	protected override void OnCleanUp()
	{
		this.reactable.Cleanup();
		base.OnCleanUp();
	}

	// Token: 0x06002255 RID: 8789 RVA: 0x000BA34C File Offset: 0x000B854C
	public void SetInfo(CarePackageInfo info)
	{
		this.info = info;
		this.SetAnimToInfo();
	}

	// Token: 0x06002256 RID: 8790 RVA: 0x000BA35B File Offset: 0x000B855B
	public void SetFacade(string facadeID)
	{
		this.facadeID = facadeID;
		this.SetAnimToInfo();
	}

	// Token: 0x06002257 RID: 8791 RVA: 0x000BA36C File Offset: 0x000B856C
	private void SetAnimToInfo()
	{
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab("Meter".ToTag()), base.gameObject, null);
		GameObject prefab = Assets.GetPrefab(this.info.id);
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		KBatchedAnimController component2 = prefab.GetComponent<KBatchedAnimController>();
		SymbolOverrideController component3 = prefab.GetComponent<SymbolOverrideController>();
		KBatchedAnimController component4 = gameObject.GetComponent<KBatchedAnimController>();
		component4.transform.SetLocalPosition(Vector3.forward);
		component4.AnimFiles = component2.AnimFiles;
		component4.isMovable = true;
		component4.animWidth = component2.animWidth;
		component4.animHeight = component2.animHeight;
		if (component3 != null)
		{
			SymbolOverrideController symbolOverrideController = SymbolOverrideControllerUtil.AddToPrefab(gameObject);
			foreach (SymbolOverrideController.SymbolEntry symbolEntry in component3.GetSymbolOverrides)
			{
				symbolOverrideController.AddSymbolOverride(symbolEntry.targetSymbol, symbolEntry.sourceSymbol, 0);
			}
		}
		component4.initialAnim = component2.initialAnim;
		component4.initialMode = KAnim.PlayMode.Loop;
		if (!string.IsNullOrEmpty(this.facadeID))
		{
			component4.SwapAnims(new KAnimFile[] { Db.GetEquippableFacades().Get(this.facadeID).AnimFile });
			base.GetComponentsInChildren<KBatchedAnimController>()[1].SetSymbolVisiblity("object", false);
		}
		KBatchedAnimTracker component5 = gameObject.GetComponent<KBatchedAnimTracker>();
		component5.controller = component;
		component5.symbol = new HashedString("snapTO_object");
		component5.offset = new Vector3(0f, 0.5f, 0f);
		gameObject.SetActive(true);
		component.SetSymbolVisiblity("snapTO_object", false);
		new KAnimLink(component, component4);
	}

	// Token: 0x06002258 RID: 8792 RVA: 0x000BA50C File Offset: 0x000B870C
	private void SpawnContents()
	{
		if (this.info == null)
		{
			global::Debug.LogWarning("CarePackage has no data to spawn from. Probably a save from before the CarePackage info data was serialized.");
			return;
		}
		GameObject gameObject = null;
		GameObject prefab = Assets.GetPrefab(this.info.id);
		Element element = ElementLoader.GetElement(this.info.id.ToTag());
		Vector3 vector = base.transform.position + Vector3.up / 2f;
		if (element == null && prefab != null)
		{
			int num = 0;
			while ((float)num < this.info.quantity)
			{
				gameObject = Util.KInstantiate(prefab, vector);
				if (gameObject != null)
				{
					if (!this.facadeID.IsNullOrWhiteSpace())
					{
						EquippableFacade.AddFacadeToEquippable(gameObject.GetComponent<Equippable>(), this.facadeID);
					}
					gameObject.SetActive(true);
				}
				num++;
			}
		}
		else if (element != null)
		{
			float quantity = this.info.quantity;
			gameObject = element.substance.SpawnResource(vector, quantity, element.defaultValues.temperature, byte.MaxValue, 0, false, true, false);
		}
		else
		{
			global::Debug.LogWarning("Can't find spawnable thing from tag " + this.info.id);
		}
		if (gameObject != null)
		{
			gameObject.SetActive(true);
		}
	}

	// Token: 0x040013D0 RID: 5072
	[Serialize]
	public CarePackageInfo info;

	// Token: 0x040013D1 RID: 5073
	private string facadeID;

	// Token: 0x040013D2 RID: 5074
	private Reactable reactable;

	// Token: 0x020011AB RID: 4523
	public class SMInstance : GameStateMachine<CarePackage.States, CarePackage.SMInstance, CarePackage, object>.GameInstance
	{
		// Token: 0x06007778 RID: 30584 RVA: 0x002BB635 File Offset: 0x002B9835
		public SMInstance(CarePackage master)
			: base(master)
		{
		}

		// Token: 0x04005B8A RID: 23434
		public List<Chore> activeUseChores;
	}

	// Token: 0x020011AC RID: 4524
	public class States : GameStateMachine<CarePackage.States, CarePackage.SMInstance, CarePackage>
	{
		// Token: 0x06007779 RID: 30585 RVA: 0x002BB640 File Offset: 0x002B9840
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.spawn;
			base.serializable = StateMachine.SerializeType.ParamsOnly;
			this.spawn.PlayAnim("portalbirth").OnAnimQueueComplete(this.open).ParamTransition<bool>(this.spawnedContents, this.pst, GameStateMachine<CarePackage.States, CarePackage.SMInstance, CarePackage, object>.IsTrue);
			this.open.PlayAnim("portalbirth_pst").QueueAnim("object_idle_loop", false, null).Exit(delegate(CarePackage.SMInstance smi)
			{
				smi.master.SpawnContents();
				this.spawnedContents.Set(true, smi, false);
			})
				.ScheduleGoTo(1f, this.pst);
			this.pst.PlayAnim("object_idle_pst").ScheduleGoTo(5f, this.destroy);
			this.destroy.Enter(delegate(CarePackage.SMInstance smi)
			{
				Util.KDestroyGameObject(smi.master.gameObject);
			});
		}

		// Token: 0x04005B8B RID: 23435
		public StateMachine<CarePackage.States, CarePackage.SMInstance, CarePackage, object>.BoolParameter spawnedContents;

		// Token: 0x04005B8C RID: 23436
		public GameStateMachine<CarePackage.States, CarePackage.SMInstance, CarePackage, object>.State spawn;

		// Token: 0x04005B8D RID: 23437
		public GameStateMachine<CarePackage.States, CarePackage.SMInstance, CarePackage, object>.State open;

		// Token: 0x04005B8E RID: 23438
		public GameStateMachine<CarePackage.States, CarePackage.SMInstance, CarePackage, object>.State pst;

		// Token: 0x04005B8F RID: 23439
		public GameStateMachine<CarePackage.States, CarePackage.SMInstance, CarePackage, object>.State destroy;
	}
}
