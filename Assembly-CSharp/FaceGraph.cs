using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000814 RID: 2068
[AddComponentMenu("KMonoBehaviour/scripts/FaceGraph")]
public class FaceGraph : KMonoBehaviour
{
	// Token: 0x06003BF8 RID: 15352 RVA: 0x0014D045 File Offset: 0x0014B245
	public IEnumerator<Expression> GetEnumerator()
	{
		return this.expressions.GetEnumerator();
	}

	// Token: 0x17000443 RID: 1091
	// (get) Token: 0x06003BF9 RID: 15353 RVA: 0x0014D057 File Offset: 0x0014B257
	// (set) Token: 0x06003BFA RID: 15354 RVA: 0x0014D05F File Offset: 0x0014B25F
	public Expression overrideExpression { get; private set; }

	// Token: 0x17000444 RID: 1092
	// (get) Token: 0x06003BFB RID: 15355 RVA: 0x0014D068 File Offset: 0x0014B268
	// (set) Token: 0x06003BFC RID: 15356 RVA: 0x0014D070 File Offset: 0x0014B270
	public Expression currentExpression { get; private set; }

	// Token: 0x06003BFD RID: 15357 RVA: 0x0014D079 File Offset: 0x0014B279
	public void AddExpression(Expression expression)
	{
		if (this.expressions.Contains(expression))
		{
			return;
		}
		this.expressions.Add(expression);
		this.UpdateFace();
	}

	// Token: 0x06003BFE RID: 15358 RVA: 0x0014D09C File Offset: 0x0014B29C
	public void RemoveExpression(Expression expression)
	{
		if (this.expressions.Remove(expression))
		{
			this.UpdateFace();
		}
	}

	// Token: 0x06003BFF RID: 15359 RVA: 0x0014D0B2 File Offset: 0x0014B2B2
	public void SetOverrideExpression(Expression expression)
	{
		if (expression != this.overrideExpression)
		{
			this.overrideExpression = expression;
			this.UpdateFace();
		}
	}

	// Token: 0x06003C00 RID: 15360 RVA: 0x0014D0CC File Offset: 0x0014B2CC
	public void ApplyShape()
	{
		KAnimFile anim = Assets.GetAnim(FaceGraph.HASH_HEAD_MASTER_SWAP_KANIM);
		bool flag = this.ShouldUseSidewaysSymbol(this.m_controller);
		if (this.m_blinkMonitor == null)
		{
			this.m_blinkMonitor = this.m_accessorizer.GetSMI<BlinkMonitor.Instance>();
		}
		if (this.m_speechMonitor == null)
		{
			this.m_speechMonitor = this.m_accessorizer.GetSMI<SpeechMonitor.Instance>();
		}
		if (this.m_blinkMonitor.IsNullOrStopped() || !this.m_blinkMonitor.IsBlinking())
		{
			KAnim.Build.Symbol symbol = this.m_accessorizer.GetAccessory(Db.Get().AccessorySlots.Eyes).symbol;
			this.ApplyShape(symbol, this.m_controller, anim, FaceGraph.ANIM_HASH_SNAPTO_EYES, flag);
		}
		if (this.m_speechMonitor.IsNullOrStopped() || !this.m_speechMonitor.IsPlayingSpeech())
		{
			KAnim.Build.Symbol symbol2 = this.m_accessorizer.GetAccessory(Db.Get().AccessorySlots.Mouth).symbol;
			this.ApplyShape(symbol2, this.m_controller, anim, FaceGraph.ANIM_HASH_SNAPTO_MOUTH, flag);
			return;
		}
		this.m_speechMonitor.DrawMouth();
	}

	// Token: 0x06003C01 RID: 15361 RVA: 0x0014D1CC File Offset: 0x0014B3CC
	private bool ShouldUseSidewaysSymbol(KBatchedAnimController controller)
	{
		KAnim.Anim currentAnim = controller.GetCurrentAnim();
		if (currentAnim == null)
		{
			return false;
		}
		int currentFrameIndex = controller.GetCurrentFrameIndex();
		if (currentFrameIndex <= 0)
		{
			return false;
		}
		KBatchGroupData batchGroupData = KAnimBatchManager.Instance().GetBatchGroupData(currentAnim.animFile.animBatchTag);
		KAnim.Anim.Frame frame = batchGroupData.GetFrame(currentFrameIndex);
		for (int i = 0; i < frame.numElements; i++)
		{
			KAnim.Anim.FrameElement frameElement = batchGroupData.GetFrameElement(frame.firstElementIdx + i);
			if (frameElement.symbol == FaceGraph.ANIM_HASH_SNAPTO_EYES && frameElement.frame >= FaceGraph.FIRST_SIDEWAYS_FRAME)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003C02 RID: 15362 RVA: 0x0014D25C File Offset: 0x0014B45C
	private void ApplyShape(KAnim.Build.Symbol variation_symbol, KBatchedAnimController controller, KAnimFile shapes_file, KAnimHashedString symbol_name_in_shape_file, bool should_use_sideways_symbol)
	{
		HashedString hashedString = FaceGraph.ANIM_HASH_NEUTRAL;
		if (this.currentExpression != null)
		{
			hashedString = this.currentExpression.face.hash;
		}
		KAnim.Anim anim = null;
		KAnim.Anim.FrameElement frameElement = default(KAnim.Anim.FrameElement);
		bool flag = false;
		bool flag2 = false;
		int num = 0;
		while (num < shapes_file.GetData().animCount && !flag)
		{
			KAnim.Anim anim2 = shapes_file.GetData().GetAnim(num);
			if (anim2.hash == hashedString)
			{
				anim = anim2;
				KAnim.Anim.Frame frame = anim.GetFrame(shapes_file.GetData().build.batchTag, 0);
				for (int i = 0; i < frame.numElements; i++)
				{
					frameElement = KAnimBatchManager.Instance().GetBatchGroupData(shapes_file.GetData().animBatchTag).GetFrameElement(frame.firstElementIdx + i);
					if (!(frameElement.symbol != symbol_name_in_shape_file))
					{
						if (flag2 || !should_use_sideways_symbol)
						{
							flag = true;
						}
						flag2 = true;
						break;
					}
				}
			}
			num++;
		}
		if (anim == null)
		{
			DebugUtil.Assert(false, "Could not find shape for expression: " + HashCache.Get().Get(hashedString));
		}
		if (!flag2)
		{
			DebugUtil.Assert(false, "Could not find shape element for shape:" + HashCache.Get().Get(variation_symbol.hash));
		}
		KAnim.Build.Symbol symbol = KAnimBatchManager.Instance().GetBatchGroupData(controller.batchGroupID).GetSymbol(symbol_name_in_shape_file);
		KAnim.Build.SymbolFrameInstance symbolFrameInstance = KAnimBatchManager.Instance().GetBatchGroupData(variation_symbol.build.batchTag).symbolFrameInstances[variation_symbol.firstFrameIdx + frameElement.frame];
		symbolFrameInstance.buildImageIdx = this.m_symbolOverrideController.GetAtlasIdx(variation_symbol.build.GetTexture(0));
		controller.SetSymbolOverride(symbol.firstFrameIdx, ref symbolFrameInstance);
	}

	// Token: 0x06003C03 RID: 15363 RVA: 0x0014D408 File Offset: 0x0014B608
	private void UpdateFace()
	{
		Expression expression = null;
		if (this.overrideExpression != null)
		{
			expression = this.overrideExpression;
		}
		else if (this.expressions.Count > 0)
		{
			this.expressions.Sort((Expression a, Expression b) => b.priority.CompareTo(a.priority));
			expression = this.expressions[0];
		}
		if (expression != this.currentExpression || expression == null)
		{
			this.currentExpression = expression;
			this.m_symbolOverrideController.MarkDirty();
		}
		AccessorySlot headEffects = Db.Get().AccessorySlots.HeadEffects;
		if (this.currentExpression != null)
		{
			Accessory accessory = this.m_accessorizer.GetAccessory(Db.Get().AccessorySlots.HeadEffects);
			HashedString hashedString = HashedString.Invalid;
			foreach (Expression expression2 in this.expressions)
			{
				if (expression2.face.headFXHash.IsValid)
				{
					hashedString = expression2.face.headFXHash;
					break;
				}
			}
			Accessory accessory2 = ((hashedString != HashedString.Invalid) ? headEffects.Lookup(hashedString) : null);
			if (accessory != accessory2)
			{
				if (accessory != null)
				{
					this.m_accessorizer.RemoveAccessory(accessory);
				}
				if (accessory2 != null)
				{
					this.m_accessorizer.AddAccessory(accessory2);
				}
			}
			this.m_controller.SetSymbolVisiblity(headEffects.targetSymbolId, accessory2 != null);
			return;
		}
		this.m_controller.SetSymbolVisiblity(headEffects.targetSymbolId, false);
	}

	// Token: 0x04002708 RID: 9992
	private List<Expression> expressions = new List<Expression>();

	// Token: 0x0400270B RID: 9995
	[MyCmpGet]
	private KBatchedAnimController m_controller;

	// Token: 0x0400270C RID: 9996
	[MyCmpGet]
	private Accessorizer m_accessorizer;

	// Token: 0x0400270D RID: 9997
	[MyCmpGet]
	private SymbolOverrideController m_symbolOverrideController;

	// Token: 0x0400270E RID: 9998
	private BlinkMonitor.Instance m_blinkMonitor;

	// Token: 0x0400270F RID: 9999
	private SpeechMonitor.Instance m_speechMonitor;

	// Token: 0x04002710 RID: 10000
	private static HashedString HASH_HEAD_MASTER_SWAP_KANIM = "head_master_swap_kanim";

	// Token: 0x04002711 RID: 10001
	private static KAnimHashedString ANIM_HASH_SNAPTO_EYES = "snapto_eyes";

	// Token: 0x04002712 RID: 10002
	private static KAnimHashedString ANIM_HASH_SNAPTO_MOUTH = "snapto_mouth";

	// Token: 0x04002713 RID: 10003
	private static KAnimHashedString ANIM_HASH_NEUTRAL = "neutral";

	// Token: 0x04002714 RID: 10004
	private static int FIRST_SIDEWAYS_FRAME = 29;
}
