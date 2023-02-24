using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// Token: 0x0200002E RID: 46
public struct BindingEntry : IEquatable<BindingEntry>
{
	// Token: 0x06000227 RID: 551 RVA: 0x0000C568 File Offset: 0x0000A768
	public static KKeyCode GetGamepadKeyCode(int gamepad_number, GamepadButton button)
	{
		switch (gamepad_number)
		{
		case 0:
			return (KKeyCode)(button + 350);
		case 1:
			return (KKeyCode)(button + 370);
		case 2:
			return (KKeyCode)(button + 390);
		case 3:
			return (KKeyCode)(button + 410);
		default:
			DebugUtil.Assert(false);
			return KKeyCode.None;
		}
	}

	// Token: 0x06000228 RID: 552 RVA: 0x0000C5B4 File Offset: 0x0000A7B4
	public BindingEntry(string group, GamepadButton button, KKeyCode key_code, Modifier modifier, global::Action action, bool rebindable = true, bool ignore_root_conflicts = false)
	{
		this = new BindingEntry(group, button, key_code, modifier, action, rebindable, ignore_root_conflicts, null);
	}

	// Token: 0x06000229 RID: 553 RVA: 0x0000C5D4 File Offset: 0x0000A7D4
	public BindingEntry(string group, GamepadButton button, KKeyCode key_code, Modifier modifier, global::Action action, string[] dlcIds)
	{
		this = new BindingEntry(group, button, key_code, modifier, action, true, false, dlcIds);
	}

	// Token: 0x0600022A RID: 554 RVA: 0x0000C5F4 File Offset: 0x0000A7F4
	public BindingEntry(string group, GamepadButton button, KKeyCode key_code, Modifier modifier, global::Action action, bool rebindable, bool ignore_root_conflicts, string[] dlcIds)
	{
		this.mGroup = group;
		this.mButton = button;
		this.mKeyCode = key_code;
		this.mAction = action;
		this.mModifier = modifier;
		this.mRebindable = rebindable;
		this.mIgnoreRootConflics = ignore_root_conflicts;
		this.dlcIds = dlcIds;
		if (this.dlcIds == null)
		{
			this.dlcIds = DlcManager.AVAILABLE_ALL_VERSIONS;
		}
	}

	// Token: 0x0600022B RID: 555 RVA: 0x0000C651 File Offset: 0x0000A851
	public bool Equals(BindingEntry other)
	{
		return this == other;
	}

	// Token: 0x0600022C RID: 556 RVA: 0x0000C660 File Offset: 0x0000A860
	public static bool operator ==(BindingEntry a, BindingEntry b)
	{
		return a.mGroup == b.mGroup && a.mButton == b.mButton && a.mKeyCode == b.mKeyCode && a.mAction == b.mAction && a.mModifier == b.mModifier && a.mRebindable == b.mRebindable;
	}

	// Token: 0x0600022D RID: 557 RVA: 0x0000C6C8 File Offset: 0x0000A8C8
	public bool IsBindingEqual(BindingEntry other)
	{
		return this.mButton == other.mButton && this.mKeyCode == other.mKeyCode && this.mModifier == other.mModifier;
	}

	// Token: 0x0600022E RID: 558 RVA: 0x0000C6F6 File Offset: 0x0000A8F6
	public static bool operator !=(BindingEntry a, BindingEntry b)
	{
		return !(a == b);
	}

	// Token: 0x0600022F RID: 559 RVA: 0x0000C704 File Offset: 0x0000A904
	public override bool Equals(object o)
	{
		if (!(o is BindingEntry))
		{
			return false;
		}
		BindingEntry bindingEntry = (BindingEntry)o;
		return this == bindingEntry;
	}

	// Token: 0x06000230 RID: 560 RVA: 0x0000C72E File Offset: 0x0000A92E
	public override int GetHashCode()
	{
		return (int)(this.mButton ^ (GamepadButton)this.mKeyCode ^ (GamepadButton)this.mAction);
	}

	// Token: 0x04000223 RID: 547
	[JsonIgnore]
	public string mGroup;

	// Token: 0x04000224 RID: 548
	[JsonIgnore]
	public bool mRebindable;

	// Token: 0x04000225 RID: 549
	[JsonIgnore]
	public bool mIgnoreRootConflics;

	// Token: 0x04000226 RID: 550
	[JsonIgnore]
	public string[] dlcIds;

	// Token: 0x04000227 RID: 551
	[JsonConverter(typeof(StringEnumConverter))]
	public GamepadButton mButton;

	// Token: 0x04000228 RID: 552
	[JsonConverter(typeof(StringEnumConverter))]
	public KKeyCode mKeyCode;

	// Token: 0x04000229 RID: 553
	[JsonConverter(typeof(StringEnumConverter))]
	public global::Action mAction;

	// Token: 0x0400022A RID: 554
	[JsonConverter(typeof(StringEnumConverter))]
	public Modifier mModifier;
}
