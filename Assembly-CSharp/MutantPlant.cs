using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x0200083A RID: 2106
[SerializationConfig(MemberSerialization.OptIn)]
public class MutantPlant : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x17000445 RID: 1093
	// (get) Token: 0x06003CBA RID: 15546 RVA: 0x00152DDA File Offset: 0x00150FDA
	public List<string> MutationIDs
	{
		get
		{
			return this.mutationIDs;
		}
	}

	// Token: 0x17000446 RID: 1094
	// (get) Token: 0x06003CBB RID: 15547 RVA: 0x00152DE2 File Offset: 0x00150FE2
	public bool IsOriginal
	{
		get
		{
			return this.mutationIDs == null || this.mutationIDs.Count == 0;
		}
	}

	// Token: 0x17000447 RID: 1095
	// (get) Token: 0x06003CBC RID: 15548 RVA: 0x00152DFC File Offset: 0x00150FFC
	public bool IsIdentified
	{
		get
		{
			return this.analyzed && PlantSubSpeciesCatalog.Instance.IsSubSpeciesIdentified(this.SubSpeciesID);
		}
	}

	// Token: 0x17000448 RID: 1096
	// (get) Token: 0x06003CBD RID: 15549 RVA: 0x00152E18 File Offset: 0x00151018
	// (set) Token: 0x06003CBE RID: 15550 RVA: 0x00152E3B File Offset: 0x0015103B
	public Tag SpeciesID
	{
		get
		{
			global::Debug.Assert(this.speciesID != null, "Ack, forgot to configure the species ID for this mutantPlant!");
			return this.speciesID;
		}
		set
		{
			this.speciesID = value;
		}
	}

	// Token: 0x17000449 RID: 1097
	// (get) Token: 0x06003CBF RID: 15551 RVA: 0x00152E44 File Offset: 0x00151044
	public Tag SubSpeciesID
	{
		get
		{
			if (this.cachedSubspeciesID == null)
			{
				this.cachedSubspeciesID = this.GetSubSpeciesInfo().ID;
			}
			return this.cachedSubspeciesID;
		}
	}

	// Token: 0x06003CC0 RID: 15552 RVA: 0x00152E70 File Offset: 0x00151070
	protected override void OnPrefabInit()
	{
		base.Subscribe<MutantPlant>(-2064133523, MutantPlant.OnAbsorbDelegate);
		base.Subscribe<MutantPlant>(1335436905, MutantPlant.OnSplitFromChunkDelegate);
	}

	// Token: 0x06003CC1 RID: 15553 RVA: 0x00152E94 File Offset: 0x00151094
	protected override void OnSpawn()
	{
		if (this.IsOriginal || this.HasTag(GameTags.Plant))
		{
			this.analyzed = true;
		}
		if (!this.IsOriginal)
		{
			this.AddTag(GameTags.MutatedSeed);
		}
		this.AddTag(this.SubSpeciesID);
		Components.MutantPlants.Add(this);
		base.OnSpawn();
		this.ApplyMutations();
		this.UpdateNameAndTags();
		PlantSubSpeciesCatalog.Instance.DiscoverSubSpecies(this.GetSubSpeciesInfo(), this);
	}

	// Token: 0x06003CC2 RID: 15554 RVA: 0x00152F0A File Offset: 0x0015110A
	protected override void OnCleanUp()
	{
		Components.MutantPlants.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06003CC3 RID: 15555 RVA: 0x00152F20 File Offset: 0x00151120
	private void OnAbsorb(object data)
	{
		MutantPlant component = (data as Pickupable).GetComponent<MutantPlant>();
		global::Debug.Assert(component != null && this.SubSpeciesID == component.SubSpeciesID, "Two seeds of different subspecies just absorbed!");
	}

	// Token: 0x06003CC4 RID: 15556 RVA: 0x00152F60 File Offset: 0x00151160
	private void OnSplitFromChunk(object data)
	{
		MutantPlant component = (data as Pickupable).GetComponent<MutantPlant>();
		if (component != null)
		{
			component.CopyMutationsTo(this);
		}
	}

	// Token: 0x06003CC5 RID: 15557 RVA: 0x00152F8C File Offset: 0x0015118C
	public void Mutate()
	{
		List<string> list = ((this.mutationIDs != null) ? new List<string>(this.mutationIDs) : new List<string>());
		while (list.Count >= 1 && list.Count > 0)
		{
			list.RemoveAt(UnityEngine.Random.Range(0, list.Count));
		}
		list.Add(Db.Get().PlantMutations.GetRandomMutation(this.PrefabID().Name).Id);
		this.SetSubSpecies(list);
	}

	// Token: 0x06003CC6 RID: 15558 RVA: 0x00153009 File Offset: 0x00151209
	public void Analyze()
	{
		this.analyzed = true;
		this.UpdateNameAndTags();
	}

	// Token: 0x06003CC7 RID: 15559 RVA: 0x00153018 File Offset: 0x00151218
	public void ApplyMutations()
	{
		if (this.IsOriginal)
		{
			return;
		}
		foreach (string text in this.mutationIDs)
		{
			Db.Get().PlantMutations.Get(text).ApplyTo(this);
		}
	}

	// Token: 0x06003CC8 RID: 15560 RVA: 0x00153084 File Offset: 0x00151284
	public void DummySetSubspecies(List<string> mutations)
	{
		this.mutationIDs = mutations;
	}

	// Token: 0x06003CC9 RID: 15561 RVA: 0x0015308D File Offset: 0x0015128D
	public void SetSubSpecies(List<string> mutations)
	{
		if (base.gameObject.HasTag(this.SubSpeciesID))
		{
			base.gameObject.RemoveTag(this.SubSpeciesID);
		}
		this.cachedSubspeciesID = Tag.Invalid;
		this.mutationIDs = mutations;
		this.UpdateNameAndTags();
	}

	// Token: 0x06003CCA RID: 15562 RVA: 0x001530CB File Offset: 0x001512CB
	public PlantSubSpeciesCatalog.SubSpeciesInfo GetSubSpeciesInfo()
	{
		return new PlantSubSpeciesCatalog.SubSpeciesInfo(this.SpeciesID, this.mutationIDs);
	}

	// Token: 0x06003CCB RID: 15563 RVA: 0x001530DE File Offset: 0x001512DE
	public void CopyMutationsTo(MutantPlant target)
	{
		target.SetSubSpecies(this.mutationIDs);
		target.analyzed = this.analyzed;
	}

	// Token: 0x06003CCC RID: 15564 RVA: 0x001530F8 File Offset: 0x001512F8
	public void UpdateNameAndTags()
	{
		bool flag = !base.IsInitialized() || this.IsIdentified;
		bool flag2 = PlantSubSpeciesCatalog.Instance == null || PlantSubSpeciesCatalog.Instance.GetAllSubSpeciesForSpecies(this.SpeciesID).Count == 1;
		KPrefabID component = base.GetComponent<KPrefabID>();
		component.AddTag(this.SubSpeciesID, false);
		component.SetTag(GameTags.UnidentifiedSeed, !flag);
		base.gameObject.name = component.PrefabTag.ToString() + " (" + this.SubSpeciesID.ToString() + ")";
		base.GetComponent<KSelectable>().SetName(this.GetSubSpeciesInfo().GetNameWithMutations(component.PrefabTag.ProperName(), flag, flag2));
		KSelectable component2 = base.GetComponent<KSelectable>();
		foreach (Guid guid in this.statusItemHandles)
		{
			component2.RemoveStatusItem(guid, false);
		}
		this.statusItemHandles.Clear();
		if (!flag2)
		{
			if (this.IsOriginal)
			{
				this.statusItemHandles.Add(component2.AddStatusItem(Db.Get().CreatureStatusItems.OriginalPlantMutation, null));
				return;
			}
			if (!flag)
			{
				this.statusItemHandles.Add(component2.AddStatusItem(Db.Get().CreatureStatusItems.UnknownMutation, null));
				return;
			}
			foreach (string text in this.mutationIDs)
			{
				this.statusItemHandles.Add(component2.AddStatusItem(Db.Get().CreatureStatusItems.SpecificPlantMutation, Db.Get().PlantMutations.Get(text)));
			}
		}
	}

	// Token: 0x06003CCD RID: 15565 RVA: 0x001532EC File Offset: 0x001514EC
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		if (this.IsOriginal)
		{
			return null;
		}
		List<Descriptor> list = new List<Descriptor>();
		foreach (string text in this.mutationIDs)
		{
			Db.Get().PlantMutations.Get(text).GetDescriptors(ref list, go);
		}
		return list;
	}

	// Token: 0x06003CCE RID: 15566 RVA: 0x00153364 File Offset: 0x00151564
	public List<string> GetSoundEvents()
	{
		List<string> list = new List<string>();
		if (this.mutationIDs != null)
		{
			foreach (string text in this.mutationIDs)
			{
				PlantMutation plantMutation = Db.Get().PlantMutations.Get(text);
				list.AddRange(plantMutation.AdditionalSoundEvents);
			}
		}
		return list;
	}

	// Token: 0x040027A9 RID: 10153
	[Serialize]
	private bool analyzed;

	// Token: 0x040027AA RID: 10154
	[Serialize]
	private List<string> mutationIDs;

	// Token: 0x040027AB RID: 10155
	private List<Guid> statusItemHandles = new List<Guid>();

	// Token: 0x040027AC RID: 10156
	private const int MAX_MUTATIONS = 1;

	// Token: 0x040027AD RID: 10157
	[SerializeField]
	private Tag speciesID;

	// Token: 0x040027AE RID: 10158
	private Tag cachedSubspeciesID;

	// Token: 0x040027AF RID: 10159
	private static readonly EventSystem.IntraObjectHandler<MutantPlant> OnAbsorbDelegate = new EventSystem.IntraObjectHandler<MutantPlant>(delegate(MutantPlant component, object data)
	{
		component.OnAbsorb(data);
	});

	// Token: 0x040027B0 RID: 10160
	private static readonly EventSystem.IntraObjectHandler<MutantPlant> OnSplitFromChunkDelegate = new EventSystem.IntraObjectHandler<MutantPlant>(delegate(MutantPlant component, object data)
	{
		component.OnSplitFromChunk(data);
	});
}
