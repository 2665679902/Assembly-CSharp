using System;
using System.Collections.Generic;
using System.Diagnostics;
using STRINGS;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D83 RID: 3459
	[DebuggerDisplay("{Id}")]
	public class Effect : Modifier
	{
		// Token: 0x06006978 RID: 27000 RVA: 0x00290AA0 File Offset: 0x0028ECA0
		public Effect(string id, string name, string description, float duration, bool show_in_ui, bool trigger_floating_text, bool is_bad, Emote emote = null, float emote_cooldown = -1f, float max_initial_delay = 0f, string stompGroup = null, string custom_icon = "")
			: base(id, name, description)
		{
			this.duration = duration;
			this.showInUI = show_in_ui;
			this.triggerFloatingText = trigger_floating_text;
			this.isBad = is_bad;
			this.emote = emote;
			this.emoteCooldown = emote_cooldown;
			this.maxInitialDelay = max_initial_delay;
			this.stompGroup = stompGroup;
			this.customIcon = custom_icon;
		}

		// Token: 0x06006979 RID: 27001 RVA: 0x00290B00 File Offset: 0x0028ED00
		public Effect(string id, string name, string description, float duration, bool show_in_ui, bool trigger_floating_text, bool is_bad, string emoteAnim, float emote_cooldown = -1f, string stompGroup = null, string custom_icon = "")
			: base(id, name, description)
		{
			this.duration = duration;
			this.showInUI = show_in_ui;
			this.triggerFloatingText = trigger_floating_text;
			this.isBad = is_bad;
			this.emoteAnim = emoteAnim;
			this.emoteCooldown = emote_cooldown;
			this.stompGroup = stompGroup;
			this.customIcon = custom_icon;
		}

		// Token: 0x0600697A RID: 27002 RVA: 0x00290B56 File Offset: 0x0028ED56
		public override void AddTo(Attributes attributes)
		{
			base.AddTo(attributes);
		}

		// Token: 0x0600697B RID: 27003 RVA: 0x00290B5F File Offset: 0x0028ED5F
		public override void RemoveFrom(Attributes attributes)
		{
			base.RemoveFrom(attributes);
		}

		// Token: 0x0600697C RID: 27004 RVA: 0x00290B68 File Offset: 0x0028ED68
		public void SetEmote(Emote emote, float emoteCooldown = -1f)
		{
			this.emote = emote;
			this.emoteCooldown = emoteCooldown;
		}

		// Token: 0x0600697D RID: 27005 RVA: 0x00290B78 File Offset: 0x0028ED78
		public void AddEmotePrecondition(Reactable.ReactablePrecondition precon)
		{
			if (this.emotePreconditions == null)
			{
				this.emotePreconditions = new List<Reactable.ReactablePrecondition>();
			}
			this.emotePreconditions.Add(precon);
		}

		// Token: 0x0600697E RID: 27006 RVA: 0x00290B9C File Offset: 0x0028ED9C
		public static string CreateTooltip(Effect effect, bool showDuration, string linePrefix = "\n    • ", bool showHeader = true)
		{
			string text = (showHeader ? DUPLICANTS.MODIFIERS.EFFECT_HEADER.text : "");
			foreach (AttributeModifier attributeModifier in effect.SelfModifiers)
			{
				Attribute attribute = Db.Get().Attributes.TryGet(attributeModifier.AttributeId);
				if (attribute == null)
				{
					attribute = Db.Get().CritterAttributes.TryGet(attributeModifier.AttributeId);
				}
				if (attribute != null && attribute.ShowInUI != Attribute.Display.Never)
				{
					text = text + linePrefix + string.Format(DUPLICANTS.MODIFIERS.MODIFIER_FORMAT, attribute.Name, attributeModifier.GetFormattedString());
				}
			}
			StringEntry stringEntry;
			if (Strings.TryGet("STRINGS.DUPLICANTS.MODIFIERS." + effect.Id.ToUpper() + ".ADDITIONAL_EFFECTS", out stringEntry))
			{
				text = text + linePrefix + stringEntry;
			}
			if (showDuration && effect.duration > 0f)
			{
				text = text + "\n" + string.Format(DUPLICANTS.MODIFIERS.TIME_TOTAL, GameUtil.GetFormattedCycles(effect.duration, "F1", false));
			}
			return text;
		}

		// Token: 0x0600697F RID: 27007 RVA: 0x00290CD0 File Offset: 0x0028EED0
		public static string CreateFullTooltip(Effect effect, bool showDuration)
		{
			return string.Concat(new string[]
			{
				effect.Name,
				"\n\n",
				effect.description,
				"\n\n",
				Effect.CreateTooltip(effect, showDuration, "\n    • ", true)
			});
		}

		// Token: 0x06006980 RID: 27008 RVA: 0x00290D10 File Offset: 0x0028EF10
		public static void AddModifierDescriptions(GameObject parent, List<Descriptor> descs, string effect_id, bool increase_indent = false)
		{
			foreach (AttributeModifier attributeModifier in Db.Get().effects.Get(effect_id).SelfModifiers)
			{
				Descriptor descriptor = new Descriptor(Strings.Get("STRINGS.DUPLICANTS.ATTRIBUTES." + attributeModifier.AttributeId.ToUpper() + ".NAME") + ": " + attributeModifier.GetFormattedString(), "", Descriptor.DescriptorType.Effect, false);
				if (increase_indent)
				{
					descriptor.IncreaseIndent();
				}
				descs.Add(descriptor);
			}
		}

		// Token: 0x04004F4D RID: 20301
		public float duration;

		// Token: 0x04004F4E RID: 20302
		public bool showInUI;

		// Token: 0x04004F4F RID: 20303
		public bool triggerFloatingText;

		// Token: 0x04004F50 RID: 20304
		public bool isBad;

		// Token: 0x04004F51 RID: 20305
		public string customIcon;

		// Token: 0x04004F52 RID: 20306
		public string emoteAnim;

		// Token: 0x04004F53 RID: 20307
		public Emote emote;

		// Token: 0x04004F54 RID: 20308
		public float emoteCooldown;

		// Token: 0x04004F55 RID: 20309
		public float maxInitialDelay;

		// Token: 0x04004F56 RID: 20310
		public List<Reactable.ReactablePrecondition> emotePreconditions;

		// Token: 0x04004F57 RID: 20311
		public string stompGroup;
	}
}
