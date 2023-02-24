using System;
using KSerialization;

// Token: 0x02000A7B RID: 2683
[SerializationConfig(MemberSerialization.OptIn)]
public class CreatureBait : StateMachineComponent<CreatureBait.StatesInstance>
{
	// Token: 0x0600521C RID: 21020 RVA: 0x001DA7BB File Offset: 0x001D89BB
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x0600521D RID: 21021 RVA: 0x001DA7C4 File Offset: 0x001D89C4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Tag[] constructionElements = base.GetComponent<Deconstructable>().constructionElements;
		this.baitElement = constructionElements[1];
		base.gameObject.GetSMI<Lure.Instance>().SetActiveLures(new Tag[] { this.baitElement });
		base.smi.StartSM();
	}

	// Token: 0x0400375A RID: 14170
	[Serialize]
	public Tag baitElement;

	// Token: 0x02001905 RID: 6405
	public class StatesInstance : GameStateMachine<CreatureBait.States, CreatureBait.StatesInstance, CreatureBait, object>.GameInstance
	{
		// Token: 0x06008F06 RID: 36614 RVA: 0x0030F73F File Offset: 0x0030D93F
		public StatesInstance(CreatureBait master)
			: base(master)
		{
		}
	}

	// Token: 0x02001906 RID: 6406
	public class States : GameStateMachine<CreatureBait.States, CreatureBait.StatesInstance, CreatureBait>
	{
		// Token: 0x06008F07 RID: 36615 RVA: 0x0030F748 File Offset: 0x0030D948
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.idle.ToggleMainStatusItem(Db.Get().BuildingStatusItems.Baited, null).Enter(delegate(CreatureBait.StatesInstance smi)
			{
				KAnim.Build build = ElementLoader.FindElementByName(smi.master.baitElement.ToString()).substance.anim.GetData().build;
				KAnim.Build.Symbol symbol = build.GetSymbol(new KAnimHashedString(build.name));
				HashedString hashedString = "snapTo_bait";
				smi.GetComponent<SymbolOverrideController>().AddSymbolOverride(hashedString, symbol, 0);
			}).TagTransition(GameTags.LureUsed, this.destroy, false);
			this.destroy.PlayAnim("use").EventHandler(GameHashes.AnimQueueComplete, delegate(CreatureBait.StatesInstance smi)
			{
				Util.KDestroyGameObject(smi.master.gameObject);
			});
		}

		// Token: 0x0400731E RID: 29470
		public GameStateMachine<CreatureBait.States, CreatureBait.StatesInstance, CreatureBait, object>.State idle;

		// Token: 0x0400731F RID: 29471
		public GameStateMachine<CreatureBait.States, CreatureBait.StatesInstance, CreatureBait, object>.State destroy;
	}
}
