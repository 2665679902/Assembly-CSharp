using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020008F8 RID: 2296
public class RotPile : StateMachineComponent<RotPile.StatesInstance>
{
	// Token: 0x06004258 RID: 16984 RVA: 0x00176239 File Offset: 0x00174439
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06004259 RID: 16985 RVA: 0x00176241 File Offset: 0x00174441
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x0600425A RID: 16986 RVA: 0x00176254 File Offset: 0x00174454
	protected void ConvertToElement()
	{
		PrimaryElement component = base.smi.master.GetComponent<PrimaryElement>();
		float mass = component.Mass;
		float temperature = component.Temperature;
		if (mass <= 0f)
		{
			Util.KDestroyGameObject(base.gameObject);
			return;
		}
		SimHashes simHashes = SimHashes.ToxicSand;
		GameObject gameObject = ElementLoader.FindElementByHash(simHashes).substance.SpawnResource(base.smi.master.transform.GetPosition(), mass, temperature, byte.MaxValue, 0, false, false, false);
		PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, ElementLoader.FindElementByHash(simHashes).name, gameObject.transform, 1.5f, false);
		Util.KDestroyGameObject(base.smi.gameObject);
	}

	// Token: 0x0600425B RID: 16987 RVA: 0x00176308 File Offset: 0x00174508
	private static string OnRottenTooltip(List<Notification> notifications, object data)
	{
		string text = "";
		foreach (Notification notification in notifications)
		{
			if (notification.tooltipData != null)
			{
				text = text + "\n• " + (string)notification.tooltipData + " ";
			}
		}
		return string.Format(MISC.NOTIFICATIONS.FOODROT.TOOLTIP, text);
	}

	// Token: 0x0600425C RID: 16988 RVA: 0x0017638C File Offset: 0x0017458C
	public void TryClearNotification()
	{
		if (this.notification != null)
		{
			base.gameObject.AddOrGet<Notifier>().Remove(this.notification);
		}
	}

	// Token: 0x0600425D RID: 16989 RVA: 0x001763AC File Offset: 0x001745AC
	public void TryCreateNotification()
	{
		WorldContainer myWorld = base.smi.master.GetMyWorld();
		if (myWorld != null && myWorld.worldInventory.IsReachable(base.smi.master.gameObject.GetComponent<Pickupable>()))
		{
			this.notification = new Notification(MISC.NOTIFICATIONS.FOODROT.NAME, NotificationType.BadMinor, new Func<List<Notification>, object, string>(RotPile.OnRottenTooltip), null, true, 0f, null, null, null, true, false, false);
			this.notification.tooltipData = base.smi.master.gameObject.GetProperName();
			base.gameObject.AddOrGet<Notifier>().Add(this.notification, "");
		}
	}

	// Token: 0x04002C55 RID: 11349
	private Notification notification;

	// Token: 0x020016C5 RID: 5829
	public class StatesInstance : GameStateMachine<RotPile.States, RotPile.StatesInstance, RotPile, object>.GameInstance
	{
		// Token: 0x060088AA RID: 34986 RVA: 0x002F639F File Offset: 0x002F459F
		public StatesInstance(RotPile master)
			: base(master)
		{
		}

		// Token: 0x04006AEC RID: 27372
		public AttributeModifier baseDecomposeRate;
	}

	// Token: 0x020016C6 RID: 5830
	public class States : GameStateMachine<RotPile.States, RotPile.StatesInstance, RotPile>
	{
		// Token: 0x060088AB RID: 34987 RVA: 0x002F63A8 File Offset: 0x002F45A8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.decomposing;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.decomposing.Enter(delegate(RotPile.StatesInstance smi)
			{
				smi.master.TryCreateNotification();
			}).Exit(delegate(RotPile.StatesInstance smi)
			{
				smi.master.TryClearNotification();
			}).ParamTransition<float>(this.decompositionAmount, this.convertDestroy, (RotPile.StatesInstance smi, float p) => p >= 600f)
				.Update("Decomposing", delegate(RotPile.StatesInstance smi, float dt)
				{
					this.decompositionAmount.Delta(dt, smi);
				}, UpdateRate.SIM_200ms, false);
			this.convertDestroy.Enter(delegate(RotPile.StatesInstance smi)
			{
				smi.master.ConvertToElement();
			});
		}

		// Token: 0x04006AED RID: 27373
		public GameStateMachine<RotPile.States, RotPile.StatesInstance, RotPile, object>.State decomposing;

		// Token: 0x04006AEE RID: 27374
		public GameStateMachine<RotPile.States, RotPile.StatesInstance, RotPile, object>.State convertDestroy;

		// Token: 0x04006AEF RID: 27375
		public StateMachine<RotPile.States, RotPile.StatesInstance, RotPile, object>.FloatParameter decompositionAmount;
	}
}
