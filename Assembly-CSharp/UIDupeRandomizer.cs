using System;
using System.Collections.Generic;
using Database;
using UnityEngine;

// Token: 0x02000C15 RID: 3093
public class UIDupeRandomizer : MonoBehaviour
{
	// Token: 0x060061FC RID: 25084 RVA: 0x0024335C File Offset: 0x0024155C
	protected virtual void Start()
	{
		this.slots = Db.Get().AccessorySlots;
		for (int i = 0; i < this.anims.Length; i++)
		{
			this.anims[i].curBody = null;
			this.GetNewBody(i);
		}
	}

	// Token: 0x060061FD RID: 25085 RVA: 0x002433A8 File Offset: 0x002415A8
	protected void GetNewBody(int minion_idx)
	{
		Personality random = Db.Get().Personalities.GetRandom(true, false);
		foreach (KBatchedAnimController kbatchedAnimController in this.anims[minion_idx].minions)
		{
			this.Apply(kbatchedAnimController, random);
		}
	}

	// Token: 0x060061FE RID: 25086 RVA: 0x0024341C File Offset: 0x0024161C
	private void Apply(KBatchedAnimController dupe, Personality personality)
	{
		KCompBuilder.BodyData bodyData = MinionStartingStats.CreateBodyData(personality);
		SymbolOverrideController component = dupe.GetComponent<SymbolOverrideController>();
		component.RemoveAllSymbolOverrides(0);
		UIDupeRandomizer.AddAccessory(dupe, this.slots.Hair.Lookup(bodyData.hair));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.HatHair.Lookup("hat_" + HashCache.Get().Get(bodyData.hair)));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.Eyes.Lookup(bodyData.eyes));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.HeadShape.Lookup(bodyData.headShape));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.Mouth.Lookup(bodyData.mouth));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.Body.Lookup(bodyData.body));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.Arm.Lookup(bodyData.arms));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.ArmLower.Lookup(bodyData.armslower));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.Belt.Lookup(bodyData.belt));
		if (this.applySuit && UnityEngine.Random.value < 0.15f)
		{
			component.AddBuildOverride(Assets.GetAnim("body_oxygen_kanim").GetData(), 6);
			dupe.SetSymbolVisiblity("snapto_neck", true);
			dupe.SetSymbolVisiblity("belt", false);
		}
		else
		{
			dupe.SetSymbolVisiblity("snapto_neck", false);
		}
		if (this.applyHat && UnityEngine.Random.value < 0.5f)
		{
			List<string> list = new List<string>();
			foreach (Skill skill in Db.Get().Skills.resources)
			{
				list.Add(skill.hat);
			}
			string text = list[UnityEngine.Random.Range(0, list.Count)];
			UIDupeRandomizer.AddAccessory(dupe, this.slots.Hat.Lookup(text));
			dupe.SetSymbolVisiblity(Db.Get().AccessorySlots.Hair.targetSymbolId, false);
			dupe.SetSymbolVisiblity(Db.Get().AccessorySlots.HatHair.targetSymbolId, true);
		}
		else
		{
			dupe.SetSymbolVisiblity(Db.Get().AccessorySlots.Hair.targetSymbolId, true);
			dupe.SetSymbolVisiblity(Db.Get().AccessorySlots.HatHair.targetSymbolId, false);
			dupe.SetSymbolVisiblity(Db.Get().AccessorySlots.Hat.targetSymbolId, false);
		}
		dupe.SetSymbolVisiblity(Db.Get().AccessorySlots.Skirt.targetSymbolId, false);
		dupe.SetSymbolVisiblity(Db.Get().AccessorySlots.Necklace.targetSymbolId, false);
	}

	// Token: 0x060061FF RID: 25087 RVA: 0x0024371C File Offset: 0x0024191C
	public static KAnimHashedString AddAccessory(KBatchedAnimController minion, Accessory accessory)
	{
		if (accessory != null)
		{
			SymbolOverrideController component = minion.GetComponent<SymbolOverrideController>();
			DebugUtil.Assert(component != null, minion.name + " is missing symbol override controller");
			component.TryRemoveSymbolOverride(accessory.slot.targetSymbolId, 0);
			component.AddSymbolOverride(accessory.slot.targetSymbolId, accessory.symbol, 0);
			minion.SetSymbolVisiblity(accessory.slot.targetSymbolId, true);
			return accessory.slot.targetSymbolId;
		}
		return HashedString.Invalid;
	}

	// Token: 0x06006200 RID: 25088 RVA: 0x002437AC File Offset: 0x002419AC
	public KAnimHashedString AddRandomAccessory(KBatchedAnimController minion, List<Accessory> choices)
	{
		Accessory accessory = choices[UnityEngine.Random.Range(1, choices.Count)];
		return UIDupeRandomizer.AddAccessory(minion, accessory);
	}

	// Token: 0x06006201 RID: 25089 RVA: 0x002437D4 File Offset: 0x002419D4
	public void Randomize()
	{
		if (this.slots == null)
		{
			return;
		}
		for (int i = 0; i < this.anims.Length; i++)
		{
			this.GetNewBody(i);
		}
	}

	// Token: 0x06006202 RID: 25090 RVA: 0x00243804 File Offset: 0x00241A04
	protected virtual void Update()
	{
	}

	// Token: 0x040043CA RID: 17354
	[Tooltip("Enable this to allow for a chance for skill hats to appear")]
	public bool applyHat = true;

	// Token: 0x040043CB RID: 17355
	[Tooltip("Enable this to allow for a chance for suit helmets to appear (ie. atmosuit and leadsuit)")]
	public bool applySuit = true;

	// Token: 0x040043CC RID: 17356
	public UIDupeRandomizer.AnimChoice[] anims;

	// Token: 0x040043CD RID: 17357
	private AccessorySlots slots;

	// Token: 0x02001AAC RID: 6828
	[Serializable]
	public struct AnimChoice
	{
		// Token: 0x04007852 RID: 30802
		public string anim_name;

		// Token: 0x04007853 RID: 30803
		public List<KBatchedAnimController> minions;

		// Token: 0x04007854 RID: 30804
		public float minSecondsBetweenAction;

		// Token: 0x04007855 RID: 30805
		public float maxSecondsBetweenAction;

		// Token: 0x04007856 RID: 30806
		public float lastWaitTime;

		// Token: 0x04007857 RID: 30807
		public KAnimFile curBody;
	}
}
