using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A92 RID: 2706
[AddComponentMenu("KMonoBehaviour/scripts/DropDown")]
public class DropDown : KMonoBehaviour
{
	// Token: 0x1700062C RID: 1580
	// (get) Token: 0x060052F5 RID: 21237 RVA: 0x001E0CF3 File Offset: 0x001DEEF3
	// (set) Token: 0x060052F6 RID: 21238 RVA: 0x001E0CFB File Offset: 0x001DEEFB
	public bool open { get; private set; }

	// Token: 0x1700062D RID: 1581
	// (get) Token: 0x060052F7 RID: 21239 RVA: 0x001E0D04 File Offset: 0x001DEF04
	public List<IListableOption> Entries
	{
		get
		{
			return this.entries;
		}
	}

	// Token: 0x060052F8 RID: 21240 RVA: 0x001E0D0C File Offset: 0x001DEF0C
	public void Initialize(IEnumerable<IListableOption> contentKeys, Action<IListableOption, object> onEntrySelectedAction, Func<IListableOption, IListableOption, object, int> sortFunction = null, Action<DropDownEntry, object> refreshAction = null, bool displaySelectedValueWhenClosed = true, object targetData = null)
	{
		this.targetData = targetData;
		this.sortFunction = sortFunction;
		this.onEntrySelectedAction = onEntrySelectedAction;
		this.displaySelectedValueWhenClosed = displaySelectedValueWhenClosed;
		this.rowRefreshAction = refreshAction;
		this.ChangeContent(contentKeys);
		this.openButton.ClearOnClick();
		this.openButton.onClick += delegate
		{
			this.OnClick();
		};
		this.canvasScaler = GameScreenManager.Instance.ssOverlayCanvas.GetComponent<KCanvasScaler>();
	}

	// Token: 0x060052F9 RID: 21241 RVA: 0x001E0D7D File Offset: 0x001DEF7D
	public void CustomizeEmptyRow(string txt, Sprite icon)
	{
		this.emptyRowLabel = txt;
		this.emptyRowSprite = icon;
	}

	// Token: 0x060052FA RID: 21242 RVA: 0x001E0D8D File Offset: 0x001DEF8D
	public void OnClick()
	{
		if (!this.open)
		{
			this.Open();
			return;
		}
		this.Close();
	}

	// Token: 0x060052FB RID: 21243 RVA: 0x001E0DA4 File Offset: 0x001DEFA4
	public void ChangeContent(IEnumerable<IListableOption> contentKeys)
	{
		this.entries.Clear();
		foreach (IListableOption listableOption in contentKeys)
		{
			this.entries.Add(listableOption);
		}
		this.built = false;
	}

	// Token: 0x060052FC RID: 21244 RVA: 0x001E0E04 File Offset: 0x001DF004
	private void Update()
	{
		if (!this.open)
		{
			return;
		}
		if (!Input.GetMouseButtonDown(0) && Input.GetAxis("Mouse ScrollWheel") == 0f && !KInputManager.steamInputInterpreter.GetSteamInputActionIsDown(global::Action.MouseLeft))
		{
			return;
		}
		float canvasScale = this.canvasScaler.GetCanvasScale();
		if (this.scrollRect.rectTransform().GetPosition().x + this.scrollRect.rectTransform().sizeDelta.x * canvasScale < KInputManager.GetMousePos().x || this.scrollRect.rectTransform().GetPosition().x > KInputManager.GetMousePos().x || this.scrollRect.rectTransform().GetPosition().y - this.scrollRect.rectTransform().sizeDelta.y * canvasScale > KInputManager.GetMousePos().y || this.scrollRect.rectTransform().GetPosition().y < KInputManager.GetMousePos().y)
		{
			this.Close();
		}
	}

	// Token: 0x060052FD RID: 21245 RVA: 0x001E0F08 File Offset: 0x001DF108
	private void Build(List<IListableOption> contentKeys)
	{
		this.built = true;
		for (int i = this.contentContainer.childCount - 1; i >= 0; i--)
		{
			Util.KDestroyGameObject(this.contentContainer.GetChild(i));
		}
		this.rowLookup.Clear();
		if (this.addEmptyRow)
		{
			this.emptyRow = Util.KInstantiateUI(this.rowEntryPrefab, this.contentContainer.gameObject, true);
			this.emptyRow.GetComponent<KButton>().onClick += delegate
			{
				this.onEntrySelectedAction(null, this.targetData);
				if (this.displaySelectedValueWhenClosed)
				{
					this.selectedLabel.text = this.emptyRowLabel ?? UI.DROPDOWN.NONE;
				}
				this.Close();
			};
			string text = this.emptyRowLabel ?? UI.DROPDOWN.NONE;
			this.emptyRow.GetComponent<DropDownEntry>().label.text = text;
			if (this.emptyRowSprite != null)
			{
				this.emptyRow.GetComponent<DropDownEntry>().image.sprite = this.emptyRowSprite;
			}
		}
		for (int j = 0; j < contentKeys.Count; j++)
		{
			GameObject gameObject = Util.KInstantiateUI(this.rowEntryPrefab, this.contentContainer.gameObject, true);
			IListableOption id = contentKeys[j];
			gameObject.GetComponent<DropDownEntry>().entryData = id;
			gameObject.GetComponent<KButton>().onClick += delegate
			{
				this.onEntrySelectedAction(id, this.targetData);
				if (this.displaySelectedValueWhenClosed)
				{
					this.selectedLabel.text = id.GetProperName();
				}
				this.Close();
			};
			this.rowLookup.Add(id, gameObject);
		}
		this.RefreshEntries();
		this.Close();
		this.scrollRect.gameObject.transform.SetParent(this.targetDropDownContainer.transform);
		this.scrollRect.gameObject.SetActive(false);
	}

	// Token: 0x060052FE RID: 21246 RVA: 0x001E10A8 File Offset: 0x001DF2A8
	private void RefreshEntries()
	{
		foreach (KeyValuePair<IListableOption, GameObject> keyValuePair in this.rowLookup)
		{
			DropDownEntry component = keyValuePair.Value.GetComponent<DropDownEntry>();
			component.label.text = keyValuePair.Key.GetProperName();
			if (component.portrait != null && keyValuePair.Key is IAssignableIdentity)
			{
				component.portrait.SetIdentityObject(keyValuePair.Key as IAssignableIdentity, true);
			}
		}
		if (this.sortFunction != null)
		{
			this.entries.Sort((IListableOption a, IListableOption b) => this.sortFunction(a, b, this.targetData));
			for (int i = 0; i < this.entries.Count; i++)
			{
				this.rowLookup[this.entries[i]].transform.SetAsFirstSibling();
			}
			if (this.emptyRow != null)
			{
				this.emptyRow.transform.SetAsFirstSibling();
			}
		}
		foreach (KeyValuePair<IListableOption, GameObject> keyValuePair2 in this.rowLookup)
		{
			DropDownEntry component2 = keyValuePair2.Value.GetComponent<DropDownEntry>();
			this.rowRefreshAction(component2, this.targetData);
		}
		if (this.emptyRow != null)
		{
			this.rowRefreshAction(this.emptyRow.GetComponent<DropDownEntry>(), this.targetData);
		}
	}

	// Token: 0x060052FF RID: 21247 RVA: 0x001E124C File Offset: 0x001DF44C
	protected override void OnCleanUp()
	{
		Util.KDestroyGameObject(this.scrollRect);
		base.OnCleanUp();
	}

	// Token: 0x06005300 RID: 21248 RVA: 0x001E1260 File Offset: 0x001DF460
	public void Open()
	{
		if (this.open)
		{
			return;
		}
		if (!this.built)
		{
			this.Build(this.entries);
		}
		else
		{
			this.RefreshEntries();
		}
		this.open = true;
		this.scrollRect.gameObject.SetActive(true);
		this.scrollRect.rectTransform().localScale = Vector3.one;
		foreach (KeyValuePair<IListableOption, GameObject> keyValuePair in this.rowLookup)
		{
			keyValuePair.Value.SetActive(true);
		}
		this.scrollRect.rectTransform().sizeDelta = new Vector2(this.scrollRect.rectTransform().sizeDelta.x, 32f * (float)Mathf.Min(this.contentContainer.childCount, 8));
		Vector3 vector = this.dropdownAlignmentTarget.TransformPoint(this.dropdownAlignmentTarget.rect.x, this.dropdownAlignmentTarget.rect.y, 0f);
		Vector2 vector2 = new Vector2(Mathf.Min(0f, (float)Screen.width - (vector.x + (this.rowEntryPrefab.GetComponent<LayoutElement>().minWidth * this.canvasScaler.GetCanvasScale() + DropDown.edgePadding.x))), -Mathf.Min(0f, vector.y - (this.scrollRect.rectTransform().sizeDelta.y * this.canvasScaler.GetCanvasScale() + DropDown.edgePadding.y)));
		vector += vector2;
		this.scrollRect.rectTransform().SetPosition(vector);
	}

	// Token: 0x06005301 RID: 21249 RVA: 0x001E142C File Offset: 0x001DF62C
	public void Close()
	{
		if (!this.open)
		{
			return;
		}
		this.open = false;
		foreach (KeyValuePair<IListableOption, GameObject> keyValuePair in this.rowLookup)
		{
			keyValuePair.Value.SetActive(false);
		}
		this.scrollRect.SetActive(false);
	}

	// Token: 0x0400381D RID: 14365
	public GameObject targetDropDownContainer;

	// Token: 0x0400381E RID: 14366
	public LocText selectedLabel;

	// Token: 0x04003820 RID: 14368
	public KButton openButton;

	// Token: 0x04003821 RID: 14369
	public Transform contentContainer;

	// Token: 0x04003822 RID: 14370
	public GameObject scrollRect;

	// Token: 0x04003823 RID: 14371
	public RectTransform dropdownAlignmentTarget;

	// Token: 0x04003824 RID: 14372
	public GameObject rowEntryPrefab;

	// Token: 0x04003825 RID: 14373
	public bool addEmptyRow = true;

	// Token: 0x04003826 RID: 14374
	private static Vector2 edgePadding = new Vector2(8f, 8f);

	// Token: 0x04003827 RID: 14375
	public object targetData;

	// Token: 0x04003828 RID: 14376
	private List<IListableOption> entries = new List<IListableOption>();

	// Token: 0x04003829 RID: 14377
	private Action<IListableOption, object> onEntrySelectedAction;

	// Token: 0x0400382A RID: 14378
	private Action<DropDownEntry, object> rowRefreshAction;

	// Token: 0x0400382B RID: 14379
	public Dictionary<IListableOption, GameObject> rowLookup = new Dictionary<IListableOption, GameObject>();

	// Token: 0x0400382C RID: 14380
	private Func<IListableOption, IListableOption, object, int> sortFunction;

	// Token: 0x0400382D RID: 14381
	private GameObject emptyRow;

	// Token: 0x0400382E RID: 14382
	private string emptyRowLabel;

	// Token: 0x0400382F RID: 14383
	private Sprite emptyRowSprite;

	// Token: 0x04003830 RID: 14384
	private bool built;

	// Token: 0x04003831 RID: 14385
	private bool displaySelectedValueWhenClosed = true;

	// Token: 0x04003832 RID: 14386
	private const int ROWS_BEFORE_SCROLL = 8;

	// Token: 0x04003833 RID: 14387
	private KCanvasScaler canvasScaler;
}
