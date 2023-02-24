using System;
using System.Collections.Generic;
using KSerialization;
using TUNING;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000DA0 RID: 3488
	[SerializationConfig(MemberSerialization.OptIn)]
	[AddComponentMenu("KMonoBehaviour/scripts/Traits")]
	public class Traits : KMonoBehaviour, ISaveLoadable
	{
		// Token: 0x06006A2F RID: 27183 RVA: 0x00293CDA File Offset: 0x00291EDA
		public List<string> GetTraitIds()
		{
			return this.TraitIds;
		}

		// Token: 0x06006A30 RID: 27184 RVA: 0x00293CE2 File Offset: 0x00291EE2
		public void SetTraitIds(List<string> traits)
		{
			this.TraitIds = traits;
		}

		// Token: 0x06006A31 RID: 27185 RVA: 0x00293CEC File Offset: 0x00291EEC
		protected override void OnSpawn()
		{
			foreach (string text in this.TraitIds)
			{
				if (Db.Get().traits.Exists(text))
				{
					Trait trait = Db.Get().traits.Get(text);
					this.AddInternal(trait);
				}
			}
			if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 15))
			{
				List<DUPLICANTSTATS.TraitVal> joytraits = DUPLICANTSTATS.JOYTRAITS;
				if (base.GetComponent<MinionIdentity>())
				{
					bool flag = true;
					foreach (DUPLICANTSTATS.TraitVal traitVal in joytraits)
					{
						if (this.HasTrait(traitVal.id))
						{
							flag = false;
						}
					}
					if (flag)
					{
						DUPLICANTSTATS.TraitVal random = joytraits.GetRandom<DUPLICANTSTATS.TraitVal>();
						Trait trait2 = Db.Get().traits.Get(random.id);
						this.Add(trait2);
					}
				}
			}
		}

		// Token: 0x06006A32 RID: 27186 RVA: 0x00293E0C File Offset: 0x0029200C
		private void AddInternal(Trait trait)
		{
			if (!this.HasTrait(trait))
			{
				this.TraitList.Add(trait);
				trait.AddTo(this.GetAttributes());
				if (trait.OnAddTrait != null)
				{
					trait.OnAddTrait(base.gameObject);
				}
			}
		}

		// Token: 0x06006A33 RID: 27187 RVA: 0x00293E48 File Offset: 0x00292048
		public void Add(Trait trait)
		{
			DebugUtil.Assert(base.IsInitialized() || base.GetComponent<Modifiers>().IsInitialized(), "Tried adding a trait on a prefab, use Modifiers.initialTraits instead!", trait.Name, base.gameObject.name);
			if (trait.ShouldSave)
			{
				this.TraitIds.Add(trait.Id);
			}
			this.AddInternal(trait);
		}

		// Token: 0x06006A34 RID: 27188 RVA: 0x00293EA8 File Offset: 0x002920A8
		public bool HasTrait(string trait_id)
		{
			bool flag = false;
			using (List<Trait>.Enumerator enumerator = this.TraitList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Id == trait_id)
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06006A35 RID: 27189 RVA: 0x00293F08 File Offset: 0x00292108
		public bool HasTrait(Trait trait)
		{
			using (List<Trait>.Enumerator enumerator = this.TraitList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == trait)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06006A36 RID: 27190 RVA: 0x00293F60 File Offset: 0x00292160
		public void Clear()
		{
			while (this.TraitList.Count > 0)
			{
				this.Remove(this.TraitList[0]);
			}
		}

		// Token: 0x06006A37 RID: 27191 RVA: 0x00293F84 File Offset: 0x00292184
		public void Remove(Trait trait)
		{
			for (int i = 0; i < this.TraitList.Count; i++)
			{
				if (this.TraitList[i] == trait)
				{
					this.TraitList.RemoveAt(i);
					this.TraitIds.Remove(trait.Id);
					trait.RemoveFrom(this.GetAttributes());
					return;
				}
			}
		}

		// Token: 0x06006A38 RID: 27192 RVA: 0x00293FE4 File Offset: 0x002921E4
		public bool IsEffectIgnored(Effect effect)
		{
			foreach (Trait trait in this.TraitList)
			{
				if (trait.ignoredEffects != null && Array.IndexOf<string>(trait.ignoredEffects, effect.Id) != -1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006A39 RID: 27193 RVA: 0x00294054 File Offset: 0x00292254
		public bool IsChoreGroupDisabled(ChoreGroup choreGroup)
		{
			Trait trait;
			return this.IsChoreGroupDisabled(choreGroup, out trait);
		}

		// Token: 0x06006A3A RID: 27194 RVA: 0x0029406A File Offset: 0x0029226A
		public bool IsChoreGroupDisabled(ChoreGroup choreGroup, out Trait disablingTrait)
		{
			return this.IsChoreGroupDisabled(choreGroup.IdHash, out disablingTrait);
		}

		// Token: 0x06006A3B RID: 27195 RVA: 0x0029407C File Offset: 0x0029227C
		public bool IsChoreGroupDisabled(HashedString choreGroupId)
		{
			Trait trait;
			return this.IsChoreGroupDisabled(choreGroupId, out trait);
		}

		// Token: 0x06006A3C RID: 27196 RVA: 0x00294094 File Offset: 0x00292294
		public bool IsChoreGroupDisabled(HashedString choreGroupId, out Trait disablingTrait)
		{
			foreach (Trait trait in this.TraitList)
			{
				if (trait.disabledChoreGroups != null)
				{
					ChoreGroup[] disabledChoreGroups = trait.disabledChoreGroups;
					for (int i = 0; i < disabledChoreGroups.Length; i++)
					{
						if (disabledChoreGroups[i].IdHash == choreGroupId)
						{
							disablingTrait = trait;
							return true;
						}
					}
				}
			}
			disablingTrait = null;
			return false;
		}

		// Token: 0x04004FC1 RID: 20417
		public List<Trait> TraitList = new List<Trait>();

		// Token: 0x04004FC2 RID: 20418
		[Serialize]
		private List<string> TraitIds = new List<string>();
	}
}
