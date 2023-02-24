using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using STRINGS;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000AE9 RID: 2793
public class LocText : TextMeshProUGUI
{
	// Token: 0x06005590 RID: 21904 RVA: 0x001EEEC0 File Offset: 0x001ED0C0
	protected override void OnEnable()
	{
		base.OnEnable();
	}

	// Token: 0x1700063D RID: 1597
	// (get) Token: 0x06005591 RID: 21905 RVA: 0x001EEEC8 File Offset: 0x001ED0C8
	// (set) Token: 0x06005592 RID: 21906 RVA: 0x001EEED0 File Offset: 0x001ED0D0
	public bool AllowLinks
	{
		get
		{
			return this.allowLinksInternal;
		}
		set
		{
			this.allowLinksInternal = value;
			this.RefreshLinkHandler();
			this.raycastTarget = this.raycastTarget || this.allowLinksInternal;
		}
	}

	// Token: 0x06005593 RID: 21907 RVA: 0x001EEEF8 File Offset: 0x001ED0F8
	[ContextMenu("Apply Settings")]
	public void ApplySettings()
	{
		if (this.key != "" && Application.isPlaying)
		{
			StringKey stringKey = new StringKey(this.key);
			this.text = Strings.Get(stringKey);
		}
		if (this.textStyleSetting != null)
		{
			SetTextStyleSetting.ApplyStyle(this, this.textStyleSetting);
		}
	}

	// Token: 0x06005594 RID: 21908 RVA: 0x001EEF58 File Offset: 0x001ED158
	private new void Awake()
	{
		base.Awake();
		if (!Application.isPlaying)
		{
			return;
		}
		if (this.key != "")
		{
			StringEntry stringEntry = Strings.Get(new StringKey(this.key));
			this.text = stringEntry.String;
		}
		this.text = Localization.Fixup(this.text);
		base.isRightToLeftText = Localization.IsRightToLeft;
		KInputManager.InputChange.AddListener(new UnityAction(this.RefreshText));
		SetTextStyleSetting setTextStyleSetting = base.gameObject.GetComponent<SetTextStyleSetting>();
		if (setTextStyleSetting == null)
		{
			setTextStyleSetting = base.gameObject.AddComponent<SetTextStyleSetting>();
		}
		if (!this.allowOverride)
		{
			setTextStyleSetting.SetStyle(this.textStyleSetting);
		}
		this.textLinkHandler = base.GetComponent<TextLinkHandler>();
	}

	// Token: 0x06005595 RID: 21909 RVA: 0x001EF015 File Offset: 0x001ED215
	private new void Start()
	{
		base.Start();
		this.RefreshLinkHandler();
	}

	// Token: 0x06005596 RID: 21910 RVA: 0x001EF023 File Offset: 0x001ED223
	private new void OnDestroy()
	{
		KInputManager.InputChange.RemoveListener(new UnityAction(this.RefreshText));
		base.OnDestroy();
	}

	// Token: 0x06005597 RID: 21911 RVA: 0x001EF041 File Offset: 0x001ED241
	public override void SetLayoutDirty()
	{
		if (this.staticLayout)
		{
			return;
		}
		base.SetLayoutDirty();
	}

	// Token: 0x1700063E RID: 1598
	// (get) Token: 0x06005598 RID: 21912 RVA: 0x001EF052 File Offset: 0x001ED252
	// (set) Token: 0x06005599 RID: 21913 RVA: 0x001EF05A File Offset: 0x001ED25A
	public override string text
	{
		get
		{
			return base.text;
		}
		set
		{
			base.text = this.FilterInput(value);
		}
	}

	// Token: 0x0600559A RID: 21914 RVA: 0x001EF069 File Offset: 0x001ED269
	public override void SetText(string text)
	{
		text = this.FilterInput(text);
		base.SetText(text);
	}

	// Token: 0x0600559B RID: 21915 RVA: 0x001EF07B File Offset: 0x001ED27B
	private string FilterInput(string input)
	{
		if (input != null)
		{
			string text = LocText.ParseText(input);
			if (text != input)
			{
				this.originalString = input;
			}
			else
			{
				this.originalString = string.Empty;
			}
			input = text;
		}
		if (this.AllowLinks)
		{
			return LocText.ModifyLinkStrings(input);
		}
		return input;
	}

	// Token: 0x0600559C RID: 21916 RVA: 0x001EF0B8 File Offset: 0x001ED2B8
	public static string ParseText(string input)
	{
		string text = "\\{Hotkey/(\\w+)\\}";
		string text2 = Regex.Replace(input, text, delegate(Match m)
		{
			string value = m.Groups[1].Value;
			global::Action action;
			if (LocText.ActionLookup.TryGetValue(value, out action))
			{
				return GameUtil.GetHotkeyString(action);
			}
			return m.Value;
		});
		text = "\\(ClickType/(\\w+)\\)";
		return Regex.Replace(text2, text, delegate(Match m)
		{
			string value2 = m.Groups[1].Value;
			Pair<string, string> pair;
			if (!LocText.ClickLookup.TryGetValue(value2, out pair))
			{
				return m.Value;
			}
			if (KInputManager.currentControllerIsGamepad)
			{
				return pair.first;
			}
			return pair.second;
		});
	}

	// Token: 0x0600559D RID: 21917 RVA: 0x001EF11C File Offset: 0x001ED31C
	private void RefreshText()
	{
		if (this.originalString != string.Empty)
		{
			this.SetText(this.originalString);
		}
	}

	// Token: 0x0600559E RID: 21918 RVA: 0x001EF13C File Offset: 0x001ED33C
	protected override void GenerateTextMesh()
	{
		base.GenerateTextMesh();
	}

	// Token: 0x0600559F RID: 21919 RVA: 0x001EF144 File Offset: 0x001ED344
	internal void SwapFont(TMP_FontAsset font, bool isRightToLeft)
	{
		base.font = font;
		if (this.key != "")
		{
			StringEntry stringEntry = Strings.Get(new StringKey(this.key));
			this.text = stringEntry.String;
		}
		this.text = Localization.Fixup(this.text);
		base.isRightToLeftText = isRightToLeft;
	}

	// Token: 0x060055A0 RID: 21920 RVA: 0x001EF1A0 File Offset: 0x001ED3A0
	private static string ModifyLinkStrings(string input)
	{
		if (input == null || input.IndexOf("<b><style=\"KLink\">") != -1)
		{
			return input;
		}
		StringBuilder stringBuilder = new StringBuilder(input);
		stringBuilder.Replace("<link=\"", LocText.combinedPrefix);
		stringBuilder.Replace("</link>", LocText.combinedSuffix);
		return stringBuilder.ToString();
	}

	// Token: 0x060055A1 RID: 21921 RVA: 0x001EF1F0 File Offset: 0x001ED3F0
	private void RefreshLinkHandler()
	{
		if (this.textLinkHandler == null && this.allowLinksInternal)
		{
			this.textLinkHandler = base.GetComponent<TextLinkHandler>();
			if (this.textLinkHandler == null)
			{
				this.textLinkHandler = base.gameObject.AddComponent<TextLinkHandler>();
			}
		}
		else if (!this.allowLinksInternal && this.textLinkHandler != null)
		{
			UnityEngine.Object.Destroy(this.textLinkHandler);
			this.textLinkHandler = null;
		}
		if (this.textLinkHandler != null)
		{
			this.textLinkHandler.CheckMouseOver();
		}
	}

	// Token: 0x04003A16 RID: 14870
	public string key;

	// Token: 0x04003A17 RID: 14871
	public TextStyleSetting textStyleSetting;

	// Token: 0x04003A18 RID: 14872
	public bool allowOverride;

	// Token: 0x04003A19 RID: 14873
	public bool staticLayout;

	// Token: 0x04003A1A RID: 14874
	private TextLinkHandler textLinkHandler;

	// Token: 0x04003A1B RID: 14875
	private string originalString = string.Empty;

	// Token: 0x04003A1C RID: 14876
	[SerializeField]
	private bool allowLinksInternal;

	// Token: 0x04003A1D RID: 14877
	private static readonly Dictionary<string, global::Action> ActionLookup = Enum.GetNames(typeof(global::Action)).ToDictionary((string x) => x, (string x) => (global::Action)Enum.Parse(typeof(global::Action), x), StringComparer.OrdinalIgnoreCase);

	// Token: 0x04003A1E RID: 14878
	private static readonly Dictionary<string, Pair<string, string>> ClickLookup = new Dictionary<string, Pair<string, string>>
	{
		{
			UI.ClickType.Click.ToString(),
			new Pair<string, string>(UI.CONTROLS.PRESS, UI.CONTROLS.CLICK)
		},
		{
			UI.ClickType.Clickable.ToString(),
			new Pair<string, string>(UI.CONTROLS.PRESSABLE, UI.CONTROLS.CLICKABLE)
		},
		{
			UI.ClickType.Clicked.ToString(),
			new Pair<string, string>(UI.CONTROLS.PRESSED, UI.CONTROLS.CLICKED)
		},
		{
			UI.ClickType.Clicking.ToString(),
			new Pair<string, string>(UI.CONTROLS.PRESSING, UI.CONTROLS.CLICKING)
		},
		{
			UI.ClickType.Clicks.ToString(),
			new Pair<string, string>(UI.CONTROLS.PRESSES, UI.CONTROLS.CLICKS)
		},
		{
			UI.ClickType.click.ToString(),
			new Pair<string, string>(UI.CONTROLS.PRESSLOWER, UI.CONTROLS.CLICKLOWER)
		},
		{
			UI.ClickType.clickable.ToString(),
			new Pair<string, string>(UI.CONTROLS.PRESSABLELOWER, UI.CONTROLS.CLICKABLELOWER)
		},
		{
			UI.ClickType.clicked.ToString(),
			new Pair<string, string>(UI.CONTROLS.PRESSEDLOWER, UI.CONTROLS.CLICKEDLOWER)
		},
		{
			UI.ClickType.clicking.ToString(),
			new Pair<string, string>(UI.CONTROLS.PRESSINGLOWER, UI.CONTROLS.CLICKINGLOWER)
		},
		{
			UI.ClickType.clicks.ToString(),
			new Pair<string, string>(UI.CONTROLS.PRESSESLOWER, UI.CONTROLS.CLICKSLOWER)
		},
		{
			UI.ClickType.CLICK.ToString(),
			new Pair<string, string>(UI.CONTROLS.PRESSUPPER, UI.CONTROLS.CLICKUPPER)
		},
		{
			UI.ClickType.CLICKABLE.ToString(),
			new Pair<string, string>(UI.CONTROLS.PRESSABLEUPPER, UI.CONTROLS.CLICKABLEUPPER)
		},
		{
			UI.ClickType.CLICKED.ToString(),
			new Pair<string, string>(UI.CONTROLS.PRESSEDUPPER, UI.CONTROLS.CLICKEDUPPER)
		},
		{
			UI.ClickType.CLICKING.ToString(),
			new Pair<string, string>(UI.CONTROLS.PRESSINGUPPER, UI.CONTROLS.CLICKINGUPPER)
		},
		{
			UI.ClickType.CLICKS.ToString(),
			new Pair<string, string>(UI.CONTROLS.PRESSESUPPER, UI.CONTROLS.CLICKSUPPER)
		}
	};

	// Token: 0x04003A1F RID: 14879
	private const string linkPrefix_open = "<link=\"";

	// Token: 0x04003A20 RID: 14880
	private const string linkSuffix = "</link>";

	// Token: 0x04003A21 RID: 14881
	private const string linkColorPrefix = "<b><style=\"KLink\">";

	// Token: 0x04003A22 RID: 14882
	private const string linkColorSuffix = "</style></b>";

	// Token: 0x04003A23 RID: 14883
	private static readonly string combinedPrefix = "<b><style=\"KLink\"><link=\"";

	// Token: 0x04003A24 RID: 14884
	private static readonly string combinedSuffix = "</style></b></link>";
}
