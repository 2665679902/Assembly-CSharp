using System;
using KSerialization;
using STRINGS;

// Token: 0x02000B18 RID: 2840
public class GeneticAnalysisCompleteMessage : Message
{
	// Token: 0x06005790 RID: 22416 RVA: 0x001FCC8F File Offset: 0x001FAE8F
	public GeneticAnalysisCompleteMessage()
	{
	}

	// Token: 0x06005791 RID: 22417 RVA: 0x001FCC97 File Offset: 0x001FAE97
	public GeneticAnalysisCompleteMessage(Tag subSpeciesID)
	{
		this.subSpeciesID = subSpeciesID;
	}

	// Token: 0x06005792 RID: 22418 RVA: 0x001FCCA6 File Offset: 0x001FAEA6
	public override string GetSound()
	{
		return "";
	}

	// Token: 0x06005793 RID: 22419 RVA: 0x001FCCB0 File Offset: 0x001FAEB0
	public override string GetMessageBody()
	{
		PlantSubSpeciesCatalog.SubSpeciesInfo subSpeciesInfo = PlantSubSpeciesCatalog.Instance.FindSubSpecies(this.subSpeciesID);
		return MISC.NOTIFICATIONS.GENETICANALYSISCOMPLETE.MESSAGEBODY.Replace("{Plant}", subSpeciesInfo.speciesID.ProperName()).Replace("{Subspecies}", subSpeciesInfo.GetNameWithMutations(subSpeciesInfo.speciesID.ProperName(), true, false)).Replace("{Info}", subSpeciesInfo.GetMutationsTooltip());
	}

	// Token: 0x06005794 RID: 22420 RVA: 0x001FCD17 File Offset: 0x001FAF17
	public override string GetTitle()
	{
		return MISC.NOTIFICATIONS.GENETICANALYSISCOMPLETE.NAME;
	}

	// Token: 0x06005795 RID: 22421 RVA: 0x001FCD24 File Offset: 0x001FAF24
	public override string GetTooltip()
	{
		PlantSubSpeciesCatalog.SubSpeciesInfo subSpeciesInfo = PlantSubSpeciesCatalog.Instance.FindSubSpecies(this.subSpeciesID);
		return MISC.NOTIFICATIONS.GENETICANALYSISCOMPLETE.TOOLTIP.Replace("{Plant}", subSpeciesInfo.speciesID.ProperName());
	}

	// Token: 0x06005796 RID: 22422 RVA: 0x001FCD5C File Offset: 0x001FAF5C
	public override bool IsValid()
	{
		return this.subSpeciesID.IsValid;
	}

	// Token: 0x04003B59 RID: 15193
	[Serialize]
	public Tag subSpeciesID;
}
