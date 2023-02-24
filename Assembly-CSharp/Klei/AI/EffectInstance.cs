using System;
using System.Diagnostics;
using STRINGS;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D84 RID: 3460
	[DebuggerDisplay("{effect.Id}")]
	public class EffectInstance : ModifierInstance<Effect>
	{
		// Token: 0x06006981 RID: 27009 RVA: 0x00290DC0 File Offset: 0x0028EFC0
		public EffectInstance(GameObject game_object, Effect effect, bool should_save)
			: base(game_object, effect)
		{
			this.effect = effect;
			this.shouldSave = should_save;
			this.ConfigureStatusItem();
			if (effect.showInUI)
			{
				KSelectable component = base.gameObject.GetComponent<KSelectable>();
				if (!component.GetStatusItemGroup().HasStatusItem(this.statusItem))
				{
					component.AddStatusItem(this.statusItem, this);
				}
			}
			if (effect.triggerFloatingText && PopFXManager.Instance != null)
			{
				PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, effect.Name, game_object.transform, 1.5f, false);
			}
			if (effect.emote != null)
			{
				this.RegisterEmote(effect.emote, effect.emoteCooldown);
			}
			if (!string.IsNullOrEmpty(effect.emoteAnim))
			{
				this.RegisterEmote(effect.emoteAnim, effect.emoteCooldown);
			}
		}

		// Token: 0x06006982 RID: 27010 RVA: 0x00290E94 File Offset: 0x0028F094
		public void RegisterEmote(string emoteAnim, float cooldown = -1f)
		{
			ReactionMonitor.Instance smi = base.gameObject.GetSMI<ReactionMonitor.Instance>();
			if (smi == null)
			{
				return;
			}
			bool flag = cooldown < 0f;
			float num = (flag ? 100000f : cooldown);
			EmoteReactable emoteReactable = smi.AddSelfEmoteReactable(base.gameObject, this.effect.Name + "_Emote", emoteAnim, flag, Db.Get().ChoreTypes.Emote, num, 20f, float.NegativeInfinity, this.effect.maxInitialDelay, this.effect.emotePreconditions);
			if (emoteReactable == null)
			{
				return;
			}
			emoteReactable.InsertPrecondition(0, new Reactable.ReactablePrecondition(this.NotInATube));
			if (!flag)
			{
				this.reactable = emoteReactable;
			}
		}

		// Token: 0x06006983 RID: 27011 RVA: 0x00290F3C File Offset: 0x0028F13C
		public void RegisterEmote(Emote emote, float cooldown = -1f)
		{
			ReactionMonitor.Instance smi = base.gameObject.GetSMI<ReactionMonitor.Instance>();
			if (smi == null)
			{
				return;
			}
			bool flag = cooldown < 0f;
			float num = (flag ? 100000f : cooldown);
			EmoteReactable emoteReactable = smi.AddSelfEmoteReactable(base.gameObject, this.effect.Name + "_Emote", emote, flag, Db.Get().ChoreTypes.Emote, num, 20f, float.NegativeInfinity, this.effect.maxInitialDelay, this.effect.emotePreconditions);
			if (emoteReactable == null)
			{
				return;
			}
			emoteReactable.InsertPrecondition(0, new Reactable.ReactablePrecondition(this.NotInATube));
			if (!flag)
			{
				this.reactable = emoteReactable;
			}
		}

		// Token: 0x06006984 RID: 27012 RVA: 0x00290FE8 File Offset: 0x0028F1E8
		private bool NotInATube(GameObject go, Navigator.ActiveTransition transition)
		{
			return transition.navGridTransition.start != NavType.Tube && transition.navGridTransition.end != NavType.Tube;
		}

		// Token: 0x06006985 RID: 27013 RVA: 0x0029100C File Offset: 0x0028F20C
		public override void OnCleanUp()
		{
			if (this.statusItem != null)
			{
				base.gameObject.GetComponent<KSelectable>().RemoveStatusItem(this.statusItem, false);
				this.statusItem = null;
			}
			if (this.reactable != null)
			{
				this.reactable.Cleanup();
				this.reactable = null;
			}
		}

		// Token: 0x06006986 RID: 27014 RVA: 0x0029105A File Offset: 0x0028F25A
		public float GetTimeRemaining()
		{
			return this.timeRemaining;
		}

		// Token: 0x06006987 RID: 27015 RVA: 0x00291062 File Offset: 0x0028F262
		public bool IsExpired()
		{
			return this.effect.duration > 0f && this.timeRemaining <= 0f;
		}

		// Token: 0x06006988 RID: 27016 RVA: 0x00291088 File Offset: 0x0028F288
		private void ConfigureStatusItem()
		{
			StatusItem.IconType iconType = (this.effect.isBad ? StatusItem.IconType.Exclamation : StatusItem.IconType.Info);
			if (!this.effect.customIcon.IsNullOrWhiteSpace())
			{
				iconType = StatusItem.IconType.Custom;
			}
			this.statusItem = new StatusItem(this.effect.Id, this.effect.Name, this.effect.description, this.effect.customIcon, iconType, this.effect.isBad ? NotificationType.Bad : NotificationType.Neutral, false, OverlayModes.None.ID, 2, false, null);
			this.statusItem.resolveStringCallback = new Func<string, object, string>(this.ResolveString);
			this.statusItem.resolveTooltipCallback = new Func<string, object, string>(this.ResolveTooltip);
		}

		// Token: 0x06006989 RID: 27017 RVA: 0x0029113B File Offset: 0x0028F33B
		private string ResolveString(string str, object data)
		{
			return str;
		}

		// Token: 0x0600698A RID: 27018 RVA: 0x00291140 File Offset: 0x0028F340
		private string ResolveTooltip(string str, object data)
		{
			string text = str;
			EffectInstance effectInstance = (EffectInstance)data;
			string text2 = Effect.CreateTooltip(effectInstance.effect, false, "\n    • ", true);
			if (!string.IsNullOrEmpty(text2))
			{
				text = text + "\n\n" + text2;
			}
			if (effectInstance.effect.duration > 0f)
			{
				text = text + "\n\n" + string.Format(DUPLICANTS.MODIFIERS.TIME_REMAINING, GameUtil.GetFormattedCycles(this.GetTimeRemaining(), "F1", false));
			}
			return text;
		}

		// Token: 0x04004F58 RID: 20312
		public Effect effect;

		// Token: 0x04004F59 RID: 20313
		public bool shouldSave;

		// Token: 0x04004F5A RID: 20314
		public StatusItem statusItem;

		// Token: 0x04004F5B RID: 20315
		public float timeRemaining;

		// Token: 0x04004F5C RID: 20316
		public EmoteReactable reactable;
	}
}
