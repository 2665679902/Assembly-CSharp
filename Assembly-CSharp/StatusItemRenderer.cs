using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004D4 RID: 1236
public class StatusItemRenderer
{
	// Token: 0x17000135 RID: 309
	// (get) Token: 0x06001CBB RID: 7355 RVA: 0x000993A8 File Offset: 0x000975A8
	// (set) Token: 0x06001CBC RID: 7356 RVA: 0x000993B0 File Offset: 0x000975B0
	public int layer { get; private set; }

	// Token: 0x17000136 RID: 310
	// (get) Token: 0x06001CBD RID: 7357 RVA: 0x000993B9 File Offset: 0x000975B9
	// (set) Token: 0x06001CBE RID: 7358 RVA: 0x000993C1 File Offset: 0x000975C1
	public int selectedHandle { get; private set; }

	// Token: 0x17000137 RID: 311
	// (get) Token: 0x06001CBF RID: 7359 RVA: 0x000993CA File Offset: 0x000975CA
	// (set) Token: 0x06001CC0 RID: 7360 RVA: 0x000993D2 File Offset: 0x000975D2
	public int highlightHandle { get; private set; }

	// Token: 0x17000138 RID: 312
	// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x000993DB File Offset: 0x000975DB
	// (set) Token: 0x06001CC2 RID: 7362 RVA: 0x000993E3 File Offset: 0x000975E3
	public Color32 backgroundColor { get; private set; }

	// Token: 0x17000139 RID: 313
	// (get) Token: 0x06001CC3 RID: 7363 RVA: 0x000993EC File Offset: 0x000975EC
	// (set) Token: 0x06001CC4 RID: 7364 RVA: 0x000993F4 File Offset: 0x000975F4
	public Color32 selectedColor { get; private set; }

	// Token: 0x1700013A RID: 314
	// (get) Token: 0x06001CC5 RID: 7365 RVA: 0x000993FD File Offset: 0x000975FD
	// (set) Token: 0x06001CC6 RID: 7366 RVA: 0x00099405 File Offset: 0x00097605
	public Color32 neutralColor { get; private set; }

	// Token: 0x1700013B RID: 315
	// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x0009940E File Offset: 0x0009760E
	// (set) Token: 0x06001CC8 RID: 7368 RVA: 0x00099416 File Offset: 0x00097616
	public Sprite arrowSprite { get; private set; }

	// Token: 0x1700013C RID: 316
	// (get) Token: 0x06001CC9 RID: 7369 RVA: 0x0009941F File Offset: 0x0009761F
	// (set) Token: 0x06001CCA RID: 7370 RVA: 0x00099427 File Offset: 0x00097627
	public Sprite backgroundSprite { get; private set; }

	// Token: 0x1700013D RID: 317
	// (get) Token: 0x06001CCB RID: 7371 RVA: 0x00099430 File Offset: 0x00097630
	// (set) Token: 0x06001CCC RID: 7372 RVA: 0x00099438 File Offset: 0x00097638
	public float scale { get; private set; }

	// Token: 0x06001CCD RID: 7373 RVA: 0x00099444 File Offset: 0x00097644
	public StatusItemRenderer()
	{
		this.layer = LayerMask.NameToLayer("UI");
		this.entries = new StatusItemRenderer.Entry[100];
		this.shader = Shader.Find("Klei/StatusItem");
		for (int i = 0; i < this.entries.Length; i++)
		{
			StatusItemRenderer.Entry entry = default(StatusItemRenderer.Entry);
			entry.Init(this.shader);
			this.entries[i] = entry;
		}
		this.backgroundColor = new Color32(244, 74, 71, byte.MaxValue);
		this.selectedColor = new Color32(225, 181, 180, byte.MaxValue);
		this.neutralColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		this.arrowSprite = Assets.GetSprite("StatusBubbleTop");
		this.backgroundSprite = Assets.GetSprite("StatusBubble");
		this.scale = 1f;
		Game.Instance.Subscribe(2095258329, new Action<object>(this.OnHighlightObject));
	}

	// Token: 0x06001CCE RID: 7374 RVA: 0x00099578 File Offset: 0x00097778
	public int GetIdx(Transform transform)
	{
		int instanceID = transform.GetInstanceID();
		int num = 0;
		if (!this.handleTable.TryGetValue(instanceID, out num))
		{
			int num2 = this.entryCount;
			this.entryCount = num2 + 1;
			num = num2;
			this.handleTable[instanceID] = num;
			StatusItemRenderer.Entry entry = this.entries[num];
			entry.handle = instanceID;
			entry.transform = transform;
			entry.buildingPos = transform.GetPosition();
			entry.building = transform.GetComponent<Building>();
			entry.isBuilding = entry.building != null;
			entry.selectable = transform.GetComponent<KSelectable>();
			this.entries[num] = entry;
		}
		return num;
	}

	// Token: 0x06001CCF RID: 7375 RVA: 0x00099628 File Offset: 0x00097828
	public void Add(Transform transform, StatusItem status_item)
	{
		if (this.entryCount == this.entries.Length)
		{
			StatusItemRenderer.Entry[] array = new StatusItemRenderer.Entry[this.entries.Length * 2];
			for (int i = 0; i < this.entries.Length; i++)
			{
				array[i] = this.entries[i];
			}
			for (int j = this.entries.Length; j < array.Length; j++)
			{
				array[j].Init(this.shader);
			}
			this.entries = array;
		}
		int idx = this.GetIdx(transform);
		StatusItemRenderer.Entry entry = this.entries[idx];
		entry.Add(status_item);
		this.entries[idx] = entry;
	}

	// Token: 0x06001CD0 RID: 7376 RVA: 0x000996D8 File Offset: 0x000978D8
	public void Remove(Transform transform, StatusItem status_item)
	{
		int instanceID = transform.GetInstanceID();
		int num = 0;
		if (!this.handleTable.TryGetValue(instanceID, out num))
		{
			return;
		}
		StatusItemRenderer.Entry entry = this.entries[num];
		if (entry.statusItems.Count == 0)
		{
			return;
		}
		entry.Remove(status_item);
		this.entries[num] = entry;
		if (entry.statusItems.Count == 0)
		{
			this.ClearIdx(num);
		}
	}

	// Token: 0x06001CD1 RID: 7377 RVA: 0x00099744 File Offset: 0x00097944
	private void ClearIdx(int idx)
	{
		StatusItemRenderer.Entry entry = this.entries[idx];
		this.handleTable.Remove(entry.handle);
		if (idx != this.entryCount - 1)
		{
			entry.Replace(this.entries[this.entryCount - 1]);
			this.entries[idx] = entry;
			this.handleTable[entry.handle] = idx;
		}
		entry = this.entries[this.entryCount - 1];
		entry.Clear();
		this.entries[this.entryCount - 1] = entry;
		this.entryCount--;
	}

	// Token: 0x06001CD2 RID: 7378 RVA: 0x000997F1 File Offset: 0x000979F1
	private HashedString GetMode()
	{
		if (OverlayScreen.Instance != null)
		{
			return OverlayScreen.Instance.mode;
		}
		return OverlayModes.None.ID;
	}

	// Token: 0x06001CD3 RID: 7379 RVA: 0x00099810 File Offset: 0x00097A10
	public void MarkAllDirty()
	{
		for (int i = 0; i < this.entryCount; i++)
		{
			this.entries[i].MarkDirty();
		}
	}

	// Token: 0x06001CD4 RID: 7380 RVA: 0x00099840 File Offset: 0x00097A40
	public void RenderEveryTick()
	{
		if (DebugHandler.HideUI)
		{
			return;
		}
		this.scale = 1f + Mathf.Sin(Time.unscaledTime * 8f) * 0.1f;
		Shader.SetGlobalVector("_StatusItemParameters", new Vector4(this.scale, 0f, 0f, 0f));
		Vector3 vector = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.transform.GetPosition().z));
		Vector3 vector2 = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Camera.main.transform.GetPosition().z));
		this.visibleEntries.Clear();
		Camera worldCamera = GameScreenManager.Instance.worldSpaceCanvas.GetComponent<Canvas>().worldCamera;
		for (int i = 0; i < this.entryCount; i++)
		{
			this.entries[i].Render(this, vector2, vector, this.GetMode(), worldCamera);
		}
	}

	// Token: 0x06001CD5 RID: 7381 RVA: 0x00099944 File Offset: 0x00097B44
	public void GetIntersections(Vector2 pos, List<InterfaceTool.Intersection> intersections)
	{
		foreach (StatusItemRenderer.Entry entry in this.visibleEntries)
		{
			entry.GetIntersection(pos, intersections, this.scale);
		}
	}

	// Token: 0x06001CD6 RID: 7382 RVA: 0x000999A0 File Offset: 0x00097BA0
	public void GetIntersections(Vector2 pos, List<KSelectable> selectables)
	{
		foreach (StatusItemRenderer.Entry entry in this.visibleEntries)
		{
			entry.GetIntersection(pos, selectables, this.scale);
		}
	}

	// Token: 0x06001CD7 RID: 7383 RVA: 0x000999FC File Offset: 0x00097BFC
	public void SetOffset(Transform transform, Vector3 offset)
	{
		int num = 0;
		if (this.handleTable.TryGetValue(transform.GetInstanceID(), out num))
		{
			this.entries[num].offset = offset;
		}
	}

	// Token: 0x06001CD8 RID: 7384 RVA: 0x00099A34 File Offset: 0x00097C34
	private void OnSelectObject(object data)
	{
		int num = 0;
		if (this.handleTable.TryGetValue(this.selectedHandle, out num))
		{
			this.entries[num].MarkDirty();
		}
		GameObject gameObject = (GameObject)data;
		if (gameObject != null)
		{
			this.selectedHandle = gameObject.transform.GetInstanceID();
			if (this.handleTable.TryGetValue(this.selectedHandle, out num))
			{
				this.entries[num].MarkDirty();
				return;
			}
		}
		else
		{
			this.highlightHandle = -1;
		}
	}

	// Token: 0x06001CD9 RID: 7385 RVA: 0x00099AB8 File Offset: 0x00097CB8
	private void OnHighlightObject(object data)
	{
		int num = 0;
		if (this.handleTable.TryGetValue(this.highlightHandle, out num))
		{
			StatusItemRenderer.Entry entry = this.entries[num];
			entry.MarkDirty();
			this.entries[num] = entry;
		}
		GameObject gameObject = (GameObject)data;
		if (gameObject != null)
		{
			this.highlightHandle = gameObject.transform.GetInstanceID();
			if (this.handleTable.TryGetValue(this.highlightHandle, out num))
			{
				StatusItemRenderer.Entry entry2 = this.entries[num];
				entry2.MarkDirty();
				this.entries[num] = entry2;
				return;
			}
		}
		else
		{
			this.highlightHandle = -1;
		}
	}

	// Token: 0x06001CDA RID: 7386 RVA: 0x00099B5C File Offset: 0x00097D5C
	public void Destroy()
	{
		Game.Instance.Unsubscribe(-1503271301, new Action<object>(this.OnSelectObject));
		Game.Instance.Unsubscribe(-1201923725, new Action<object>(this.OnHighlightObject));
		foreach (StatusItemRenderer.Entry entry in this.entries)
		{
			entry.Clear();
			entry.FreeResources();
		}
	}

	// Token: 0x04001035 RID: 4149
	private StatusItemRenderer.Entry[] entries;

	// Token: 0x04001036 RID: 4150
	private int entryCount;

	// Token: 0x04001037 RID: 4151
	private Dictionary<int, int> handleTable = new Dictionary<int, int>();

	// Token: 0x04001041 RID: 4161
	private Shader shader;

	// Token: 0x04001042 RID: 4162
	public List<StatusItemRenderer.Entry> visibleEntries = new List<StatusItemRenderer.Entry>();

	// Token: 0x02001117 RID: 4375
	public struct Entry
	{
		// Token: 0x0600757C RID: 30076 RVA: 0x002B5F67 File Offset: 0x002B4167
		public void Init(Shader shader)
		{
			this.statusItems = new List<StatusItem>();
			this.mesh = new Mesh();
			this.mesh.name = "StatusItemRenderer";
			this.dirty = true;
			this.material = new Material(shader);
		}

		// Token: 0x0600757D RID: 30077 RVA: 0x002B5FA4 File Offset: 0x002B41A4
		public void Render(StatusItemRenderer renderer, Vector3 camera_bl, Vector3 camera_tr, HashedString overlay, Camera camera)
		{
			if (this.transform == null)
			{
				string text = "Error cleaning up status items:";
				foreach (StatusItem statusItem in this.statusItems)
				{
					text += statusItem.Id;
				}
				global::Debug.LogWarning(text);
				return;
			}
			Vector3 vector = (this.isBuilding ? this.buildingPos : this.transform.GetPosition());
			if (this.isBuilding)
			{
				vector.x += (float)((this.building.Def.WidthInCells - 1) % 2) / 2f;
			}
			if (vector.x < camera_bl.x || vector.x > camera_tr.x || vector.y < camera_bl.y || vector.y > camera_tr.y)
			{
				return;
			}
			int num = Grid.PosToCell(vector);
			if (Grid.IsValidCell(num) && (!Grid.IsVisible(num) || (int)Grid.WorldIdx[num] != ClusterManager.Instance.activeWorldId))
			{
				return;
			}
			if (!this.selectable.IsSelectable)
			{
				return;
			}
			renderer.visibleEntries.Add(this);
			if (this.dirty)
			{
				int num2 = 0;
				foreach (StatusItem statusItem2 in this.statusItems)
				{
					if (statusItem2.UseConditionalCallback(overlay, this.transform) || !(overlay != OverlayModes.None.ID) || !(statusItem2.render_overlay != overlay))
					{
						num2++;
					}
				}
				this.hasVisibleStatusItems = num2 != 0;
				StatusItemRenderer.Entry.MeshBuilder meshBuilder = new StatusItemRenderer.Entry.MeshBuilder(num2 + 6, this.material);
				float num3 = 0.25f;
				float num4 = -5f;
				Vector2 vector2 = new Vector2(0.05f, -0.05f);
				float num5 = 0.02f;
				Color32 color = new Color32(0, 0, 0, byte.MaxValue);
				Color32 color2 = new Color32(0, 0, 0, 75);
				Color32 color3 = renderer.neutralColor;
				if (renderer.selectedHandle == this.handle || renderer.highlightHandle == this.handle)
				{
					color3 = renderer.selectedColor;
				}
				else
				{
					for (int i = 0; i < this.statusItems.Count; i++)
					{
						if (this.statusItems[i].notificationType != NotificationType.Neutral)
						{
							color3 = renderer.backgroundColor;
							break;
						}
					}
				}
				meshBuilder.AddQuad(new Vector2(0f, 0.29f) + vector2, new Vector2(0.05f, 0.05f), num4, renderer.arrowSprite, color2);
				meshBuilder.AddQuad(new Vector2(0f, 0f) + vector2, new Vector2(num3 * (float)num2, num3), num4, renderer.backgroundSprite, color2);
				meshBuilder.AddQuad(new Vector2(0f, 0f), new Vector2(num3 * (float)num2 + num5, num3 + num5), num4, renderer.backgroundSprite, color);
				meshBuilder.AddQuad(new Vector2(0f, 0f), new Vector2(num3 * (float)num2, num3), num4, renderer.backgroundSprite, color3);
				int num6 = 0;
				for (int j = 0; j < this.statusItems.Count; j++)
				{
					StatusItem statusItem3 = this.statusItems[j];
					if (statusItem3.UseConditionalCallback(overlay, this.transform) || !(overlay != OverlayModes.None.ID) || !(statusItem3.render_overlay != overlay))
					{
						float num7 = (float)num6 * num3 * 2f - num3 * (float)(num2 - 1);
						if (this.statusItems[j].sprite == null)
						{
							DebugUtil.DevLogError(string.Concat(new string[]
							{
								"Status Item ",
								this.statusItems[j].Id,
								" has null sprite for icon '",
								this.statusItems[j].iconName,
								"', you need to add the sprite to the TintedSprites list in the GameAssets prefab manually."
							}));
							this.statusItems[j].iconName = "status_item_exclamation";
							this.statusItems[j].sprite = Assets.GetTintedSprite("status_item_exclamation");
						}
						Sprite sprite = this.statusItems[j].sprite.sprite;
						meshBuilder.AddQuad(new Vector2(num7, 0f), new Vector2(num3, num3), num4, sprite, color);
						num6++;
					}
				}
				meshBuilder.AddQuad(new Vector2(0f, 0.29f + num5), new Vector2(0.05f + num5, 0.05f + num5), num4, renderer.arrowSprite, color);
				meshBuilder.AddQuad(new Vector2(0f, 0.29f), new Vector2(0.05f, 0.05f), num4, renderer.arrowSprite, color3);
				meshBuilder.End(this.mesh);
				this.dirty = false;
			}
			if (this.hasVisibleStatusItems && GameScreenManager.Instance != null)
			{
				Graphics.DrawMesh(this.mesh, vector + this.offset, Quaternion.identity, this.material, renderer.layer, camera, 0, null, false, false);
			}
		}

		// Token: 0x0600757E RID: 30078 RVA: 0x002B6530 File Offset: 0x002B4730
		public void Add(StatusItem status_item)
		{
			this.statusItems.Add(status_item);
			this.dirty = true;
		}

		// Token: 0x0600757F RID: 30079 RVA: 0x002B6545 File Offset: 0x002B4745
		public void Remove(StatusItem status_item)
		{
			this.statusItems.Remove(status_item);
			this.dirty = true;
		}

		// Token: 0x06007580 RID: 30080 RVA: 0x002B655C File Offset: 0x002B475C
		public void Replace(StatusItemRenderer.Entry entry)
		{
			this.handle = entry.handle;
			this.transform = entry.transform;
			this.building = this.transform.GetComponent<Building>();
			this.buildingPos = this.transform.GetPosition();
			this.isBuilding = this.building != null;
			this.selectable = this.transform.GetComponent<KSelectable>();
			this.offset = entry.offset;
			this.dirty = true;
			this.statusItems.Clear();
			this.statusItems.AddRange(entry.statusItems);
		}

		// Token: 0x06007581 RID: 30081 RVA: 0x002B65F8 File Offset: 0x002B47F8
		private bool Intersects(Vector2 pos, float scale)
		{
			if (this.transform == null)
			{
				return false;
			}
			Bounds bounds = this.mesh.bounds;
			Vector3 vector = this.buildingPos + this.offset + bounds.center;
			Vector2 vector2 = new Vector2(vector.x, vector.y);
			Vector3 size = bounds.size;
			Vector2 vector3 = new Vector2(size.x * scale * 0.5f, size.y * scale * 0.5f);
			Vector2 vector4 = vector2 - vector3;
			Vector2 vector5 = vector2 + vector3;
			return pos.x >= vector4.x && pos.x <= vector5.x && pos.y >= vector4.y && pos.y <= vector5.y;
		}

		// Token: 0x06007582 RID: 30082 RVA: 0x002B66D0 File Offset: 0x002B48D0
		public void GetIntersection(Vector2 pos, List<InterfaceTool.Intersection> intersections, float scale)
		{
			if (this.Intersects(pos, scale) && this.selectable.IsSelectable)
			{
				intersections.Add(new InterfaceTool.Intersection
				{
					component = this.selectable,
					distance = -100f
				});
			}
		}

		// Token: 0x06007583 RID: 30083 RVA: 0x002B671C File Offset: 0x002B491C
		public void GetIntersection(Vector2 pos, List<KSelectable> selectables, float scale)
		{
			if (this.Intersects(pos, scale) && this.selectable.IsSelectable && !selectables.Contains(this.selectable))
			{
				selectables.Add(this.selectable);
			}
		}

		// Token: 0x06007584 RID: 30084 RVA: 0x002B674F File Offset: 0x002B494F
		public void Clear()
		{
			this.statusItems.Clear();
			this.offset = Vector3.zero;
			this.dirty = false;
		}

		// Token: 0x06007585 RID: 30085 RVA: 0x002B676E File Offset: 0x002B496E
		public void FreeResources()
		{
			if (this.mesh != null)
			{
				UnityEngine.Object.DestroyImmediate(this.mesh);
				this.mesh = null;
			}
			if (this.material != null)
			{
				UnityEngine.Object.DestroyImmediate(this.material);
			}
		}

		// Token: 0x06007586 RID: 30086 RVA: 0x002B67A9 File Offset: 0x002B49A9
		public void MarkDirty()
		{
			this.dirty = true;
		}

		// Token: 0x040059C6 RID: 22982
		public int handle;

		// Token: 0x040059C7 RID: 22983
		public Transform transform;

		// Token: 0x040059C8 RID: 22984
		public Building building;

		// Token: 0x040059C9 RID: 22985
		public Vector3 buildingPos;

		// Token: 0x040059CA RID: 22986
		public KSelectable selectable;

		// Token: 0x040059CB RID: 22987
		public List<StatusItem> statusItems;

		// Token: 0x040059CC RID: 22988
		public Mesh mesh;

		// Token: 0x040059CD RID: 22989
		public bool dirty;

		// Token: 0x040059CE RID: 22990
		public int layer;

		// Token: 0x040059CF RID: 22991
		public Material material;

		// Token: 0x040059D0 RID: 22992
		public Vector3 offset;

		// Token: 0x040059D1 RID: 22993
		public bool hasVisibleStatusItems;

		// Token: 0x040059D2 RID: 22994
		public bool isBuilding;

		// Token: 0x02001F7D RID: 8061
		private struct MeshBuilder
		{
			// Token: 0x06009F15 RID: 40725 RVA: 0x0033FE6C File Offset: 0x0033E06C
			public MeshBuilder(int quad_count, Material material)
			{
				this.vertices = new Vector3[4 * quad_count];
				this.uvs = new Vector2[4 * quad_count];
				this.uv2s = new Vector2[4 * quad_count];
				this.colors = new Color32[4 * quad_count];
				this.triangles = new int[6 * quad_count];
				this.material = material;
				this.quadIdx = 0;
			}

			// Token: 0x06009F16 RID: 40726 RVA: 0x0033FED0 File Offset: 0x0033E0D0
			public void AddQuad(Vector2 center, Vector2 half_size, float z, Sprite sprite, Color color)
			{
				if (this.quadIdx == StatusItemRenderer.Entry.MeshBuilder.textureIds.Length)
				{
					return;
				}
				Rect rect = sprite.rect;
				Rect textureRect = sprite.textureRect;
				float num = textureRect.width / rect.width;
				float num2 = textureRect.height / rect.height;
				int num3 = 4 * this.quadIdx;
				this.vertices[num3] = new Vector3((center.x - half_size.x) * num, (center.y - half_size.y) * num2, z);
				this.vertices[1 + num3] = new Vector3((center.x - half_size.x) * num, (center.y + half_size.y) * num2, z);
				this.vertices[2 + num3] = new Vector3((center.x + half_size.x) * num, (center.y - half_size.y) * num2, z);
				this.vertices[3 + num3] = new Vector3((center.x + half_size.x) * num, (center.y + half_size.y) * num2, z);
				float num4 = textureRect.x / (float)sprite.texture.width;
				float num5 = textureRect.y / (float)sprite.texture.height;
				float num6 = textureRect.width / (float)sprite.texture.width;
				float num7 = textureRect.height / (float)sprite.texture.height;
				this.uvs[num3] = new Vector2(num4, num5);
				this.uvs[1 + num3] = new Vector2(num4, num5 + num7);
				this.uvs[2 + num3] = new Vector2(num4 + num6, num5);
				this.uvs[3 + num3] = new Vector2(num4 + num6, num5 + num7);
				this.colors[num3] = color;
				this.colors[1 + num3] = color;
				this.colors[2 + num3] = color;
				this.colors[3 + num3] = color;
				float num8 = (float)this.quadIdx + 0.5f;
				this.uv2s[num3] = new Vector2(num8, 0f);
				this.uv2s[1 + num3] = new Vector2(num8, 0f);
				this.uv2s[2 + num3] = new Vector2(num8, 0f);
				this.uv2s[3 + num3] = new Vector2(num8, 0f);
				int num9 = 6 * this.quadIdx;
				this.triangles[num9] = num3;
				this.triangles[1 + num9] = num3 + 1;
				this.triangles[2 + num9] = num3 + 2;
				this.triangles[3 + num9] = num3 + 2;
				this.triangles[4 + num9] = num3 + 1;
				this.triangles[5 + num9] = num3 + 3;
				this.material.SetTexture(StatusItemRenderer.Entry.MeshBuilder.textureIds[this.quadIdx], sprite.texture);
				this.quadIdx++;
			}

			// Token: 0x06009F17 RID: 40727 RVA: 0x0034021C File Offset: 0x0033E41C
			public void End(Mesh mesh)
			{
				mesh.Clear();
				mesh.vertices = this.vertices;
				mesh.uv = this.uvs;
				mesh.uv2 = this.uv2s;
				mesh.colors32 = this.colors;
				mesh.SetTriangles(this.triangles, 0);
				mesh.RecalculateBounds();
			}

			// Token: 0x04008BF0 RID: 35824
			private Vector3[] vertices;

			// Token: 0x04008BF1 RID: 35825
			private Vector2[] uvs;

			// Token: 0x04008BF2 RID: 35826
			private Vector2[] uv2s;

			// Token: 0x04008BF3 RID: 35827
			private int[] triangles;

			// Token: 0x04008BF4 RID: 35828
			private Color32[] colors;

			// Token: 0x04008BF5 RID: 35829
			private int quadIdx;

			// Token: 0x04008BF6 RID: 35830
			private Material material;

			// Token: 0x04008BF7 RID: 35831
			private static int[] textureIds = new int[]
			{
				Shader.PropertyToID("_Tex0"),
				Shader.PropertyToID("_Tex1"),
				Shader.PropertyToID("_Tex2"),
				Shader.PropertyToID("_Tex3"),
				Shader.PropertyToID("_Tex4"),
				Shader.PropertyToID("_Tex5"),
				Shader.PropertyToID("_Tex6"),
				Shader.PropertyToID("_Tex7"),
				Shader.PropertyToID("_Tex8"),
				Shader.PropertyToID("_Tex9"),
				Shader.PropertyToID("_Tex10")
			};
		}
	}
}
