using System;
using System.Runtime.CompilerServices;

// Token: 0x02000689 RID: 1673
public readonly struct ClothingOutfitNameProposal
{
	// Token: 0x06002D36 RID: 11574 RVA: 0x000EDC38 File Offset: 0x000EBE38
	private ClothingOutfitNameProposal(string candidateName, ClothingOutfitNameProposal.Result result)
	{
		this.candidateName = candidateName;
		this.result = result;
	}

	// Token: 0x06002D37 RID: 11575 RVA: 0x000EDC48 File Offset: 0x000EBE48
	public static ClothingOutfitNameProposal ForNewOutfit(string candidateName)
	{
		ClothingOutfitNameProposal.<>c__DisplayClass3_0 CS$<>8__locals1;
		CS$<>8__locals1.candidateName = candidateName;
		if (string.IsNullOrEmpty(CS$<>8__locals1.candidateName))
		{
			return ClothingOutfitNameProposal.<ForNewOutfit>g__Make|3_0(ClothingOutfitNameProposal.Result.Error_NoInputName, ref CS$<>8__locals1);
		}
		if (ClothingOutfitTarget.DoesExist(CS$<>8__locals1.candidateName))
		{
			return ClothingOutfitNameProposal.<ForNewOutfit>g__Make|3_0(ClothingOutfitNameProposal.Result.Error_NameAlreadyExists, ref CS$<>8__locals1);
		}
		return ClothingOutfitNameProposal.<ForNewOutfit>g__Make|3_0(ClothingOutfitNameProposal.Result.NewOutfit, ref CS$<>8__locals1);
	}

	// Token: 0x06002D38 RID: 11576 RVA: 0x000EDC94 File Offset: 0x000EBE94
	public static ClothingOutfitNameProposal FromExistingOutfit(string candidateName, ClothingOutfitTarget existingOutfit, bool isSameNameAllowed)
	{
		ClothingOutfitNameProposal.<>c__DisplayClass4_0 CS$<>8__locals1;
		CS$<>8__locals1.candidateName = candidateName;
		if (string.IsNullOrEmpty(CS$<>8__locals1.candidateName))
		{
			return ClothingOutfitNameProposal.<FromExistingOutfit>g__Make|4_0(ClothingOutfitNameProposal.Result.Error_NoInputName, ref CS$<>8__locals1);
		}
		if (!ClothingOutfitTarget.DoesExist(CS$<>8__locals1.candidateName))
		{
			return ClothingOutfitNameProposal.<FromExistingOutfit>g__Make|4_0(ClothingOutfitNameProposal.Result.NewOutfit, ref CS$<>8__locals1);
		}
		if (!isSameNameAllowed || !(CS$<>8__locals1.candidateName == existingOutfit.ReadName()))
		{
			return ClothingOutfitNameProposal.<FromExistingOutfit>g__Make|4_0(ClothingOutfitNameProposal.Result.Error_NameAlreadyExists, ref CS$<>8__locals1);
		}
		if (existingOutfit.CanWriteName)
		{
			return ClothingOutfitNameProposal.<FromExistingOutfit>g__Make|4_0(ClothingOutfitNameProposal.Result.SameOutfit, ref CS$<>8__locals1);
		}
		return ClothingOutfitNameProposal.<FromExistingOutfit>g__Make|4_0(ClothingOutfitNameProposal.Result.Error_SameOutfitReadonly, ref CS$<>8__locals1);
	}

	// Token: 0x06002D39 RID: 11577 RVA: 0x000EDD0F File Offset: 0x000EBF0F
	[CompilerGenerated]
	internal static ClothingOutfitNameProposal <ForNewOutfit>g__Make|3_0(ClothingOutfitNameProposal.Result result, ref ClothingOutfitNameProposal.<>c__DisplayClass3_0 A_1)
	{
		return new ClothingOutfitNameProposal(A_1.candidateName, result);
	}

	// Token: 0x06002D3A RID: 11578 RVA: 0x000EDD1D File Offset: 0x000EBF1D
	[CompilerGenerated]
	internal static ClothingOutfitNameProposal <FromExistingOutfit>g__Make|4_0(ClothingOutfitNameProposal.Result result, ref ClothingOutfitNameProposal.<>c__DisplayClass4_0 A_1)
	{
		return new ClothingOutfitNameProposal(A_1.candidateName, result);
	}

	// Token: 0x04001AEF RID: 6895
	public readonly string candidateName;

	// Token: 0x04001AF0 RID: 6896
	public readonly ClothingOutfitNameProposal.Result result;

	// Token: 0x0200134D RID: 4941
	public enum Result
	{
		// Token: 0x04006026 RID: 24614
		None,
		// Token: 0x04006027 RID: 24615
		NewOutfit,
		// Token: 0x04006028 RID: 24616
		SameOutfit,
		// Token: 0x04006029 RID: 24617
		Error_NoInputName,
		// Token: 0x0400602A RID: 24618
		Error_NameAlreadyExists,
		// Token: 0x0400602B RID: 24619
		Error_SameOutfitReadonly
	}
}
