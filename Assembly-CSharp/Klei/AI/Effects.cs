using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D85 RID: 3461
	[SerializationConfig(MemberSerialization.OptIn)]
	[AddComponentMenu("KMonoBehaviour/scripts/Effects")]
	public class Effects : KMonoBehaviour, ISaveLoadable, ISim1000ms
	{
		// Token: 0x0600698B RID: 27019 RVA: 0x002911BB File Offset: 0x0028F3BB
		protected override void OnPrefabInit()
		{
			this.autoRegisterSimRender = false;
		}

		// Token: 0x0600698C RID: 27020 RVA: 0x002911C4 File Offset: 0x0028F3C4
		protected override void OnSpawn()
		{
			if (this.saveLoadImmunities != null)
			{
				foreach (Effects.SaveLoadImmunities saveLoadImmunities in this.saveLoadImmunities)
				{
					if (Db.Get().effects.Exists(saveLoadImmunities.effectID))
					{
						Effect effect = Db.Get().effects.Get(saveLoadImmunities.effectID);
						this.AddImmunity(effect, saveLoadImmunities.giverID, true);
					}
				}
			}
			if (this.saveLoadEffects != null)
			{
				foreach (Effects.SaveLoadEffect saveLoadEffect in this.saveLoadEffects)
				{
					if (Db.Get().effects.Exists(saveLoadEffect.id))
					{
						Effect effect2 = Db.Get().effects.Get(saveLoadEffect.id);
						EffectInstance effectInstance = this.Add(effect2, true);
						if (effectInstance != null)
						{
							effectInstance.timeRemaining = saveLoadEffect.timeRemaining;
						}
					}
				}
			}
			if (this.effectsThatExpire.Count > 0)
			{
				SimAndRenderScheduler.instance.Add(this, this.simRenderLoadBalance);
			}
		}

		// Token: 0x0600698D RID: 27021 RVA: 0x002912C8 File Offset: 0x0028F4C8
		public EffectInstance Get(string effect_id)
		{
			foreach (EffectInstance effectInstance in this.effects)
			{
				if (effectInstance.effect.Id == effect_id)
				{
					return effectInstance;
				}
			}
			return null;
		}

		// Token: 0x0600698E RID: 27022 RVA: 0x00291330 File Offset: 0x0028F530
		public EffectInstance Get(HashedString effect_id)
		{
			foreach (EffectInstance effectInstance in this.effects)
			{
				if (effectInstance.effect.IdHash == effect_id)
				{
					return effectInstance;
				}
			}
			return null;
		}

		// Token: 0x0600698F RID: 27023 RVA: 0x00291398 File Offset: 0x0028F598
		public EffectInstance Get(Effect effect)
		{
			foreach (EffectInstance effectInstance in this.effects)
			{
				if (effectInstance.effect == effect)
				{
					return effectInstance;
				}
			}
			return null;
		}

		// Token: 0x06006990 RID: 27024 RVA: 0x002913F4 File Offset: 0x0028F5F4
		public bool HasImmunityTo(Effect effect)
		{
			using (List<Effects.EffectImmunity>.Enumerator enumerator = this.effectImmunites.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.effect == effect)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06006991 RID: 27025 RVA: 0x00291450 File Offset: 0x0028F650
		public EffectInstance Add(string effect_id, bool should_save)
		{
			Effect effect = Db.Get().effects.Get(effect_id);
			return this.Add(effect, should_save);
		}

		// Token: 0x06006992 RID: 27026 RVA: 0x00291478 File Offset: 0x0028F678
		public EffectInstance Add(HashedString effect_id, bool should_save)
		{
			Effect effect = Db.Get().effects.Get(effect_id);
			return this.Add(effect, should_save);
		}

		// Token: 0x06006993 RID: 27027 RVA: 0x002914A0 File Offset: 0x0028F6A0
		public EffectInstance Add(Effect effect, bool should_save)
		{
			if (this.HasImmunityTo(effect))
			{
				return null;
			}
			bool flag = true;
			Traits component = base.GetComponent<Traits>();
			if (component != null && component.IsEffectIgnored(effect))
			{
				flag = false;
			}
			if (flag)
			{
				Attributes attributes = this.GetAttributes();
				EffectInstance effectInstance = this.Get(effect);
				if (!string.IsNullOrEmpty(effect.stompGroup))
				{
					for (int i = this.effects.Count - 1; i >= 0; i--)
					{
						if (this.effects[i] != effectInstance && this.effects[i].effect.stompGroup == effect.stompGroup)
						{
							this.Remove(this.effects[i].effect);
						}
					}
				}
				if (effectInstance == null)
				{
					effectInstance = new EffectInstance(base.gameObject, effect, should_save);
					effect.AddTo(attributes);
					this.effects.Add(effectInstance);
					if (effect.duration > 0f)
					{
						this.effectsThatExpire.Add(effectInstance);
						if (this.effectsThatExpire.Count == 1)
						{
							SimAndRenderScheduler.instance.Add(this, this.simRenderLoadBalance);
						}
					}
					base.Trigger(-1901442097, effect);
				}
				effectInstance.timeRemaining = effect.duration;
				return effectInstance;
			}
			return null;
		}

		// Token: 0x06006994 RID: 27028 RVA: 0x002915D6 File Offset: 0x0028F7D6
		public void Remove(Effect effect)
		{
			this.Remove(effect.IdHash);
		}

		// Token: 0x06006995 RID: 27029 RVA: 0x002915E4 File Offset: 0x0028F7E4
		public void Remove(HashedString effect_id)
		{
			int i = 0;
			while (i < this.effectsThatExpire.Count)
			{
				if (this.effectsThatExpire[i].effect.IdHash == effect_id)
				{
					int num = this.effectsThatExpire.Count - 1;
					this.effectsThatExpire[i] = this.effectsThatExpire[num];
					this.effectsThatExpire.RemoveAt(num);
					if (this.effectsThatExpire.Count == 0)
					{
						SimAndRenderScheduler.instance.Remove(this);
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			for (int j = 0; j < this.effects.Count; j++)
			{
				if (this.effects[j].effect.IdHash == effect_id)
				{
					Attributes attributes = this.GetAttributes();
					EffectInstance effectInstance = this.effects[j];
					effectInstance.OnCleanUp();
					Effect effect = effectInstance.effect;
					effect.RemoveFrom(attributes);
					int num2 = this.effects.Count - 1;
					this.effects[j] = this.effects[num2];
					this.effects.RemoveAt(num2);
					base.Trigger(-1157678353, effect);
					return;
				}
			}
		}

		// Token: 0x06006996 RID: 27030 RVA: 0x00291718 File Offset: 0x0028F918
		public void Remove(string effect_id)
		{
			int i = 0;
			while (i < this.effectsThatExpire.Count)
			{
				if (this.effectsThatExpire[i].effect.Id == effect_id)
				{
					int num = this.effectsThatExpire.Count - 1;
					this.effectsThatExpire[i] = this.effectsThatExpire[num];
					this.effectsThatExpire.RemoveAt(num);
					if (this.effectsThatExpire.Count == 0)
					{
						SimAndRenderScheduler.instance.Remove(this);
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			for (int j = 0; j < this.effects.Count; j++)
			{
				if (this.effects[j].effect.Id == effect_id)
				{
					Attributes attributes = this.GetAttributes();
					EffectInstance effectInstance = this.effects[j];
					effectInstance.OnCleanUp();
					Effect effect = effectInstance.effect;
					effect.RemoveFrom(attributes);
					int num2 = this.effects.Count - 1;
					this.effects[j] = this.effects[num2];
					this.effects.RemoveAt(num2);
					base.Trigger(-1157678353, effect);
					return;
				}
			}
		}

		// Token: 0x06006997 RID: 27031 RVA: 0x0029184C File Offset: 0x0028FA4C
		public bool HasEffect(HashedString effect_id)
		{
			using (List<EffectInstance>.Enumerator enumerator = this.effects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.effect.IdHash == effect_id)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06006998 RID: 27032 RVA: 0x002918B0 File Offset: 0x0028FAB0
		public bool HasEffect(string effect_id)
		{
			using (List<EffectInstance>.Enumerator enumerator = this.effects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.effect.Id == effect_id)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06006999 RID: 27033 RVA: 0x00291914 File Offset: 0x0028FB14
		public bool HasEffect(Effect effect)
		{
			using (List<EffectInstance>.Enumerator enumerator = this.effects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.effect == effect)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600699A RID: 27034 RVA: 0x00291970 File Offset: 0x0028FB70
		public void Sim1000ms(float dt)
		{
			for (int i = 0; i < this.effectsThatExpire.Count; i++)
			{
				EffectInstance effectInstance = this.effectsThatExpire[i];
				if (effectInstance.IsExpired())
				{
					this.Remove(effectInstance.effect);
				}
				effectInstance.timeRemaining -= dt;
			}
		}

		// Token: 0x0600699B RID: 27035 RVA: 0x002919C4 File Offset: 0x0028FBC4
		public void AddImmunity(Effect effect, string giverID, bool shouldSave = true)
		{
			if (giverID != null)
			{
				foreach (Effects.EffectImmunity effectImmunity in this.effectImmunites)
				{
					if (effectImmunity.giverID == giverID && effectImmunity.effect == effect)
					{
						return;
					}
				}
			}
			Effects.EffectImmunity effectImmunity2 = new Effects.EffectImmunity(effect, giverID, shouldSave);
			this.effectImmunites.Add(effectImmunity2);
		}

		// Token: 0x0600699C RID: 27036 RVA: 0x00291A44 File Offset: 0x0028FC44
		public void RemoveImmunity(Effect effect, string ID)
		{
			Effects.EffectImmunity effectImmunity = default(Effects.EffectImmunity);
			bool flag = false;
			foreach (Effects.EffectImmunity effectImmunity2 in this.effectImmunites)
			{
				if (effectImmunity2.effect == effect && (ID == null || ID == effectImmunity2.giverID))
				{
					effectImmunity = effectImmunity2;
					flag = true;
				}
			}
			if (flag)
			{
				this.effectImmunites.Remove(effectImmunity);
			}
		}

		// Token: 0x0600699D RID: 27037 RVA: 0x00291AC8 File Offset: 0x0028FCC8
		[OnSerializing]
		internal void OnSerializing()
		{
			List<Effects.SaveLoadEffect> list = new List<Effects.SaveLoadEffect>();
			foreach (EffectInstance effectInstance in this.effects)
			{
				if (effectInstance.shouldSave)
				{
					Effects.SaveLoadEffect saveLoadEffect = new Effects.SaveLoadEffect
					{
						id = effectInstance.effect.Id,
						timeRemaining = effectInstance.timeRemaining,
						saved = true
					};
					list.Add(saveLoadEffect);
				}
			}
			this.saveLoadEffects = list.ToArray();
			List<Effects.SaveLoadImmunities> list2 = new List<Effects.SaveLoadImmunities>();
			foreach (Effects.EffectImmunity effectImmunity in this.effectImmunites)
			{
				if (effectImmunity.shouldSave)
				{
					Effect effect = effectImmunity.effect;
					Effects.SaveLoadImmunities saveLoadImmunities = new Effects.SaveLoadImmunities
					{
						effectID = effect.Id,
						giverID = effectImmunity.giverID,
						saved = true
					};
					list2.Add(saveLoadImmunities);
				}
			}
			this.saveLoadImmunities = list2.ToArray();
		}

		// Token: 0x0600699E RID: 27038 RVA: 0x00291C04 File Offset: 0x0028FE04
		public List<Effects.SaveLoadImmunities> GetAllImmunitiesForSerialization()
		{
			List<Effects.SaveLoadImmunities> list = new List<Effects.SaveLoadImmunities>();
			foreach (Effects.EffectImmunity effectImmunity in this.effectImmunites)
			{
				Effect effect = effectImmunity.effect;
				Effects.SaveLoadImmunities saveLoadImmunities = new Effects.SaveLoadImmunities
				{
					effectID = effect.Id,
					giverID = effectImmunity.giverID,
					saved = effectImmunity.shouldSave
				};
				list.Add(saveLoadImmunities);
			}
			return list;
		}

		// Token: 0x0600699F RID: 27039 RVA: 0x00291C9C File Offset: 0x0028FE9C
		public List<Effects.SaveLoadEffect> GetAllEffectsForSerialization()
		{
			List<Effects.SaveLoadEffect> list = new List<Effects.SaveLoadEffect>();
			foreach (EffectInstance effectInstance in this.effects)
			{
				Effects.SaveLoadEffect saveLoadEffect = new Effects.SaveLoadEffect
				{
					id = effectInstance.effect.Id,
					timeRemaining = effectInstance.timeRemaining,
					saved = effectInstance.shouldSave
				};
				list.Add(saveLoadEffect);
			}
			return list;
		}

		// Token: 0x060069A0 RID: 27040 RVA: 0x00291D30 File Offset: 0x0028FF30
		public List<EffectInstance> GetTimeLimitedEffects()
		{
			return this.effectsThatExpire;
		}

		// Token: 0x060069A1 RID: 27041 RVA: 0x00291D38 File Offset: 0x0028FF38
		public void CopyEffects(Effects source)
		{
			foreach (EffectInstance effectInstance in source.effects)
			{
				this.Add(effectInstance.effect, effectInstance.shouldSave).timeRemaining = effectInstance.timeRemaining;
			}
			foreach (EffectInstance effectInstance2 in source.effectsThatExpire)
			{
				this.Add(effectInstance2.effect, effectInstance2.shouldSave).timeRemaining = effectInstance2.timeRemaining;
			}
		}

		// Token: 0x04004F5D RID: 20317
		[Serialize]
		private Effects.SaveLoadEffect[] saveLoadEffects;

		// Token: 0x04004F5E RID: 20318
		[Serialize]
		private Effects.SaveLoadImmunities[] saveLoadImmunities;

		// Token: 0x04004F5F RID: 20319
		private List<EffectInstance> effects = new List<EffectInstance>();

		// Token: 0x04004F60 RID: 20320
		private List<EffectInstance> effectsThatExpire = new List<EffectInstance>();

		// Token: 0x04004F61 RID: 20321
		private List<Effects.EffectImmunity> effectImmunites = new List<Effects.EffectImmunity>();

		// Token: 0x02001E51 RID: 7761
		[Serializable]
		public struct EffectImmunity
		{
			// Token: 0x06009B47 RID: 39751 RVA: 0x0033681F File Offset: 0x00334A1F
			public EffectImmunity(Effect e, string id, bool save = true)
			{
				this.giverID = id;
				this.effect = e;
				this.shouldSave = save;
			}

			// Token: 0x04008854 RID: 34900
			public string giverID;

			// Token: 0x04008855 RID: 34901
			public Effect effect;

			// Token: 0x04008856 RID: 34902
			public bool shouldSave;
		}

		// Token: 0x02001E52 RID: 7762
		[Serializable]
		public struct SaveLoadImmunities
		{
			// Token: 0x04008857 RID: 34903
			public string giverID;

			// Token: 0x04008858 RID: 34904
			public string effectID;

			// Token: 0x04008859 RID: 34905
			public bool saved;
		}

		// Token: 0x02001E53 RID: 7763
		[Serializable]
		public struct SaveLoadEffect
		{
			// Token: 0x0400885A RID: 34906
			public string id;

			// Token: 0x0400885B RID: 34907
			public float timeRemaining;

			// Token: 0x0400885C RID: 34908
			public bool saved;
		}
	}
}
