using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x02000026 RID: 38
public class GameInputMapping
{
	// Token: 0x060001F8 RID: 504 RVA: 0x0000B7DC File Offset: 0x000099DC
	public static HashSet<KeyCode> GetKeyCodes()
	{
		HashSet<KeyCode> hashSet = new HashSet<KeyCode>();
		foreach (BindingEntry bindingEntry in GameInputMapping.GetBindingEntries())
		{
			if (bindingEntry.mKeyCode < KKeyCode.KleiKeys)
			{
				hashSet.Add((KeyCode)bindingEntry.mKeyCode);
			}
		}
		hashSet.Add(KeyCode.LeftAlt);
		hashSet.Add(KeyCode.LeftControl);
		hashSet.Add(KeyCode.LeftShift);
		hashSet.Add(KeyCode.CapsLock);
		return hashSet;
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x0000B856 File Offset: 0x00009A56
	public static HashSet<string> GetAxis()
	{
		return new HashSet<string> { "Mouse X", "Mouse Y", "Mouse ScrollWheel" };
	}

	// Token: 0x1700005E RID: 94
	// (get) Token: 0x060001FA RID: 506 RVA: 0x0000B881 File Offset: 0x00009A81
	// (set) Token: 0x060001FB RID: 507 RVA: 0x0000B888 File Offset: 0x00009A88
	public static BindingEntry[] DefaultBindings { get; private set; }

	// Token: 0x060001FC RID: 508 RVA: 0x0000B890 File Offset: 0x00009A90
	public static void SetDefaultKeyBindings(BindingEntry[] default_keybindings)
	{
		GameInputMapping.DefaultBindings = default_keybindings;
		GameInputMapping.KeyBindings = (BindingEntry[])default_keybindings.Clone();
	}

	// Token: 0x060001FD RID: 509 RVA: 0x0000B8A8 File Offset: 0x00009AA8
	public static BindingEntry[] GetBindingEntries()
	{
		return GameInputMapping.KeyBindings;
	}

	// Token: 0x060001FE RID: 510 RVA: 0x0000B8B0 File Offset: 0x00009AB0
	public static BindingEntry FindEntry(global::Action mAction)
	{
		foreach (BindingEntry bindingEntry in GameInputMapping.KeyBindings)
		{
			if (bindingEntry.mAction == mAction)
			{
				return bindingEntry;
			}
		}
		global::Debug.Assert(false, "Unbound action " + mAction.ToString());
		return GameInputMapping.KeyBindings[0];
	}

	// Token: 0x060001FF RID: 511 RVA: 0x0000B90C File Offset: 0x00009B0C
	public static bool CompareActionKeyCodes(global::Action a, global::Action b)
	{
		BindingEntry bindingEntry = GameInputMapping.FindEntry(a);
		BindingEntry bindingEntry2 = GameInputMapping.FindEntry(b);
		return bindingEntry.mKeyCode == bindingEntry2.mKeyCode && bindingEntry.mModifier == bindingEntry2.mModifier;
	}

	// Token: 0x06000200 RID: 512 RVA: 0x0000B948 File Offset: 0x00009B48
	public static BindingEntry[] FindEntriesByKeyCode(KKeyCode keycode)
	{
		List<BindingEntry> list = new List<BindingEntry>();
		foreach (BindingEntry bindingEntry in GameInputMapping.KeyBindings)
		{
			if (bindingEntry.mKeyCode == keycode)
			{
				list.Add(bindingEntry);
			}
		}
		return list.ToArray();
	}

	// Token: 0x1700005F RID: 95
	// (get) Token: 0x06000201 RID: 513 RVA: 0x0000B98D File Offset: 0x00009B8D
	private static string BindingsFilename
	{
		get
		{
			return Path.Combine(Util.RootFolder(), "keybindings.json");
		}
	}

	// Token: 0x06000202 RID: 514 RVA: 0x0000B9A0 File Offset: 0x00009BA0
	public static void SaveBindings()
	{
		if (!Directory.Exists(Util.RootFolder()))
		{
			Directory.CreateDirectory(Util.RootFolder());
		}
		List<BindingEntry> list = new List<BindingEntry>();
		foreach (BindingEntry bindingEntry in GameInputMapping.KeyBindings)
		{
			bool flag = false;
			foreach (BindingEntry bindingEntry2 in GameInputMapping.DefaultBindings)
			{
				if (bindingEntry == bindingEntry2)
				{
					flag = true;
					break;
				}
			}
			if (!flag && bindingEntry.mRebindable)
			{
				list.Add(bindingEntry);
			}
		}
		if (list.Count > 0)
		{
			string text = JsonConvert.SerializeObject(list);
			File.WriteAllText(GameInputMapping.BindingsFilename, text);
			return;
		}
		if (File.Exists(GameInputMapping.BindingsFilename))
		{
			File.Delete(GameInputMapping.BindingsFilename);
		}
	}

	// Token: 0x06000203 RID: 515 RVA: 0x0000BA64 File Offset: 0x00009C64
	public static void LoadBindings()
	{
		GameInputMapping.KeyBindings = (BindingEntry[])GameInputMapping.DefaultBindings.Clone();
		if (!File.Exists(GameInputMapping.BindingsFilename))
		{
			return;
		}
		string text = File.ReadAllText(GameInputMapping.BindingsFilename);
		if (text == null || text == "")
		{
			return;
		}
		BindingEntry[] array = null;
		try
		{
			array = JsonConvert.DeserializeObject<BindingEntry[]>(text);
		}
		catch
		{
			DebugUtil.LogErrorArgs(new object[]
			{
				"Error parsing",
				GameInputMapping.BindingsFilename
			});
		}
		if (array == null || array.Length == 0)
		{
			return;
		}
		for (int i = 0; i < GameInputMapping.KeyBindings.Length; i++)
		{
			BindingEntry bindingEntry = GameInputMapping.KeyBindings[i];
			foreach (BindingEntry bindingEntry2 in array)
			{
				if (bindingEntry2.mAction == bindingEntry.mAction && bindingEntry.mRebindable)
				{
					BindingEntry bindingEntry3 = bindingEntry;
					bindingEntry3.mButton = bindingEntry2.mButton;
					bindingEntry3.mKeyCode = bindingEntry2.mKeyCode;
					bindingEntry3.mModifier = bindingEntry2.mModifier;
					GameInputMapping.KeyBindings[i] = bindingEntry3;
					break;
				}
			}
		}
	}

	// Token: 0x040001EA RID: 490
	public static BindingEntry[] KeyBindings;
}
