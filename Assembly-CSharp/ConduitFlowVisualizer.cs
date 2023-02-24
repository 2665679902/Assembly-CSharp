using System;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x020009F9 RID: 2553
public class ConduitFlowVisualizer
{
	// Token: 0x06004CAD RID: 19629 RVA: 0x001AF85C File Offset: 0x001ADA5C
	public ConduitFlowVisualizer(ConduitFlow flow_manager, Game.ConduitVisInfo vis_info, EventReference overlay_sound, ConduitFlowVisualizer.Tuning tuning)
	{
		this.flowManager = flow_manager;
		this.visInfo = vis_info;
		this.overlaySound = overlay_sound;
		this.tuning = tuning;
		this.movingBallMesh = new ConduitFlowVisualizer.ConduitFlowMesh();
		this.staticBallMesh = new ConduitFlowVisualizer.ConduitFlowMesh();
		ConduitFlowVisualizer.RenderMeshTask.Ball.InitializeResources();
	}

	// Token: 0x06004CAE RID: 19630 RVA: 0x001AF8E8 File Offset: 0x001ADAE8
	public void FreeResources()
	{
		this.movingBallMesh.Cleanup();
		this.staticBallMesh.Cleanup();
	}

	// Token: 0x06004CAF RID: 19631 RVA: 0x001AF900 File Offset: 0x001ADB00
	private float CalculateMassScale(float mass)
	{
		float num = (mass - this.visInfo.overlayMassScaleRange.x) / (this.visInfo.overlayMassScaleRange.y - this.visInfo.overlayMassScaleRange.x);
		return Mathf.Lerp(this.visInfo.overlayMassScaleValues.x, this.visInfo.overlayMassScaleValues.y, num);
	}

	// Token: 0x06004CB0 RID: 19632 RVA: 0x001AF968 File Offset: 0x001ADB68
	private Color32 GetContentsColor(Element element, Color32 default_color)
	{
		if (element != null)
		{
			Color color = element.substance.conduitColour;
			color.a = 128f;
			return color;
		}
		return default_color;
	}

	// Token: 0x06004CB1 RID: 19633 RVA: 0x001AF99D File Offset: 0x001ADB9D
	private Color32 GetTintColour()
	{
		if (!this.showContents)
		{
			return this.visInfo.tint;
		}
		return GlobalAssets.Instance.colorSet.GetColorByName(this.visInfo.overlayTintName);
	}

	// Token: 0x06004CB2 RID: 19634 RVA: 0x001AF9CD File Offset: 0x001ADBCD
	private Color32 GetInsulatedTintColour()
	{
		if (!this.showContents)
		{
			return this.visInfo.insulatedTint;
		}
		return GlobalAssets.Instance.colorSet.GetColorByName(this.visInfo.overlayInsulatedTintName);
	}

	// Token: 0x06004CB3 RID: 19635 RVA: 0x001AF9FD File Offset: 0x001ADBFD
	private Color32 GetRadiantTintColour()
	{
		if (!this.showContents)
		{
			return this.visInfo.radiantTint;
		}
		return GlobalAssets.Instance.colorSet.GetColorByName(this.visInfo.overlayRadiantTintName);
	}

	// Token: 0x06004CB4 RID: 19636 RVA: 0x001AFA30 File Offset: 0x001ADC30
	private Color32 GetCellTintColour(int cell)
	{
		Color32 color;
		if (this.insulatedCells.Contains(cell))
		{
			color = this.GetInsulatedTintColour();
		}
		else if (this.radiantCells.Contains(cell))
		{
			color = this.GetRadiantTintColour();
		}
		else
		{
			color = this.GetTintColour();
		}
		return color;
	}

	// Token: 0x06004CB5 RID: 19637 RVA: 0x001AFA74 File Offset: 0x001ADC74
	public void Render(float z, int render_layer, float lerp_percent, bool trigger_audio = false)
	{
		this.animTime += (double)Time.deltaTime;
		if (trigger_audio)
		{
			if (this.audioInfo == null)
			{
				this.audioInfo = new List<ConduitFlowVisualizer.AudioInfo>();
			}
			for (int i = 0; i < this.audioInfo.Count; i++)
			{
				ConduitFlowVisualizer.AudioInfo audioInfo = this.audioInfo[i];
				audioInfo.distance = float.PositiveInfinity;
				audioInfo.position = Vector3.zero;
				audioInfo.blobCount = (audioInfo.blobCount + 1) % 10;
				this.audioInfo[i] = audioInfo;
			}
		}
		if (this.tuning.renderMesh)
		{
			this.RenderMesh(z, render_layer, lerp_percent, trigger_audio);
		}
		if (trigger_audio)
		{
			this.TriggerAudio();
		}
	}

	// Token: 0x06004CB6 RID: 19638 RVA: 0x001AFB28 File Offset: 0x001ADD28
	private void RenderMesh(float z, int render_layer, float lerp_percent, bool trigger_audio)
	{
		GridArea visibleArea = GridVisibleArea.GetVisibleArea();
		Vector2I vector2I = new Vector2I(Mathf.Max(0, visibleArea.Min.x - 1), Mathf.Max(0, visibleArea.Min.y - 1));
		Vector2I vector2I2 = new Vector2I(Mathf.Min(Grid.WidthInCells - 1, visibleArea.Max.x + 1), Mathf.Min(Grid.HeightInCells - 1, visibleArea.Max.y + 1));
		ConduitFlowVisualizer.RenderMeshContext renderMeshContext = new ConduitFlowVisualizer.RenderMeshContext(this, lerp_percent, vector2I, vector2I2);
		if (renderMeshContext.visible_conduits.Count == 0)
		{
			renderMeshContext.Finish();
			return;
		}
		ConduitFlowVisualizer.render_mesh_job.Reset(renderMeshContext);
		int num = Mathf.Max(1, (int)((float)(renderMeshContext.visible_conduits.Count / CPUBudget.coreCount) / 1.5f));
		int num2 = Mathf.Max(1, renderMeshContext.visible_conduits.Count / num);
		for (int num3 = 0; num3 != num2; num3++)
		{
			int num4 = num3 * num;
			int num5 = ((num3 == num2 - 1) ? renderMeshContext.visible_conduits.Count : (num4 + num));
			ConduitFlowVisualizer.render_mesh_job.Add(new ConduitFlowVisualizer.RenderMeshTask(num4, num5));
		}
		GlobalJobManager.Run(ConduitFlowVisualizer.render_mesh_job);
		float num6 = 0f;
		if (this.showContents)
		{
			num6 = 1f;
		}
		float num7 = (float)((int)(this.animTime / (1.0 / (double)this.tuning.framesPerSecond)) % (int)this.tuning.spriteCount) * (1f / this.tuning.spriteCount);
		this.movingBallMesh.Begin();
		this.movingBallMesh.SetTexture("_BackgroundTex", this.tuning.backgroundTexture);
		this.movingBallMesh.SetTexture("_ForegroundTex", this.tuning.foregroundTexture);
		this.movingBallMesh.SetVector("_SpriteSettings", new Vector4(1f / this.tuning.spriteCount, 1f, num6, num7));
		this.movingBallMesh.SetVector("_Highlight", new Vector4((float)this.highlightColour.r / 255f, (float)this.highlightColour.g / 255f, (float)this.highlightColour.b / 255f, 0f));
		this.staticBallMesh.Begin();
		this.staticBallMesh.SetTexture("_BackgroundTex", this.tuning.backgroundTexture);
		this.staticBallMesh.SetTexture("_ForegroundTex", this.tuning.foregroundTexture);
		this.staticBallMesh.SetVector("_SpriteSettings", new Vector4(1f / this.tuning.spriteCount, 1f, num6, 0f));
		this.staticBallMesh.SetVector("_Highlight", new Vector4((float)this.highlightColour.r / 255f, (float)this.highlightColour.g / 255f, (float)this.highlightColour.b / 255f, 0f));
		Vector3 position = CameraController.Instance.transform.GetPosition();
		ConduitFlowVisualizer conduitFlowVisualizer = (trigger_audio ? this : null);
		for (int num8 = 0; num8 != ConduitFlowVisualizer.render_mesh_job.Count; num8++)
		{
			ConduitFlowVisualizer.render_mesh_job.GetWorkItem(num8).Finish(this.movingBallMesh, this.staticBallMesh, position, conduitFlowVisualizer);
		}
		this.movingBallMesh.End(z, this.layer);
		this.staticBallMesh.End(z, this.layer);
		renderMeshContext.Finish();
		ConduitFlowVisualizer.render_mesh_job.Reset(null);
	}

	// Token: 0x06004CB7 RID: 19639 RVA: 0x001AFEBD File Offset: 0x001AE0BD
	public void ColourizePipeContents(bool show_contents, bool move_to_overlay_layer)
	{
		this.showContents = show_contents;
		this.layer = ((show_contents && move_to_overlay_layer) ? LayerMask.NameToLayer("MaskedOverlay") : 0);
	}

	// Token: 0x06004CB8 RID: 19640 RVA: 0x001AFEE0 File Offset: 0x001AE0E0
	private void AddAudioSource(ConduitFlow.Conduit conduit, Vector3 camera_pos)
	{
		using (new KProfiler.Region("AddAudioSource", null))
		{
			UtilityNetwork network = this.flowManager.GetNetwork(conduit);
			if (network != null)
			{
				Vector3 vector = Grid.CellToPosCCC(conduit.GetCell(this.flowManager), Grid.SceneLayer.Building);
				float num = Vector3.SqrMagnitude(vector - camera_pos);
				bool flag = false;
				for (int i = 0; i < this.audioInfo.Count; i++)
				{
					ConduitFlowVisualizer.AudioInfo audioInfo = this.audioInfo[i];
					if (audioInfo.networkID == network.id)
					{
						if (num < audioInfo.distance)
						{
							audioInfo.distance = num;
							audioInfo.position = vector;
							this.audioInfo[i] = audioInfo;
						}
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					ConduitFlowVisualizer.AudioInfo audioInfo2 = default(ConduitFlowVisualizer.AudioInfo);
					audioInfo2.networkID = network.id;
					audioInfo2.position = vector;
					audioInfo2.distance = num;
					audioInfo2.blobCount = 0;
					this.audioInfo.Add(audioInfo2);
				}
			}
		}
	}

	// Token: 0x06004CB9 RID: 19641 RVA: 0x001AFFF8 File Offset: 0x001AE1F8
	private void TriggerAudio()
	{
		if (SpeedControlScreen.Instance.IsPaused)
		{
			return;
		}
		CameraController instance = CameraController.Instance;
		int num = 0;
		List<ConduitFlowVisualizer.AudioInfo> list = new List<ConduitFlowVisualizer.AudioInfo>();
		for (int i = 0; i < this.audioInfo.Count; i++)
		{
			if (instance.IsVisiblePos(this.audioInfo[i].position))
			{
				list.Add(this.audioInfo[i]);
				num++;
			}
		}
		for (int j = 0; j < list.Count; j++)
		{
			ConduitFlowVisualizer.AudioInfo audioInfo = list[j];
			if (audioInfo.distance != float.PositiveInfinity)
			{
				Vector3 position = audioInfo.position;
				position.z = 0f;
				EventInstance eventInstance = SoundEvent.BeginOneShot(this.overlaySound, position, 1f, false);
				eventInstance.setParameterByName("blobCount", (float)audioInfo.blobCount, false);
				eventInstance.setParameterByName("networkCount", (float)num, false);
				SoundEvent.EndOneShot(eventInstance);
			}
		}
	}

	// Token: 0x06004CBA RID: 19642 RVA: 0x001B00EA File Offset: 0x001AE2EA
	public void AddThermalConductivity(int cell, float conductivity)
	{
		if (conductivity < 1f)
		{
			this.insulatedCells.Add(cell);
			return;
		}
		if (conductivity > 1f)
		{
			this.radiantCells.Add(cell);
		}
	}

	// Token: 0x06004CBB RID: 19643 RVA: 0x001B0117 File Offset: 0x001AE317
	public void RemoveThermalConductivity(int cell, float conductivity)
	{
		if (conductivity < 1f)
		{
			this.insulatedCells.Remove(cell);
			return;
		}
		if (conductivity > 1f)
		{
			this.radiantCells.Remove(cell);
		}
	}

	// Token: 0x06004CBC RID: 19644 RVA: 0x001B0144 File Offset: 0x001AE344
	public void SetHighlightedCell(int cell)
	{
		this.highlightedCell = cell;
	}

	// Token: 0x04003282 RID: 12930
	private ConduitFlow flowManager;

	// Token: 0x04003283 RID: 12931
	private EventReference overlaySound;

	// Token: 0x04003284 RID: 12932
	private bool showContents;

	// Token: 0x04003285 RID: 12933
	private double animTime;

	// Token: 0x04003286 RID: 12934
	private int layer;

	// Token: 0x04003287 RID: 12935
	private static Vector2 GRID_OFFSET = new Vector2(0.5f, 0.5f);

	// Token: 0x04003288 RID: 12936
	private List<ConduitFlowVisualizer.AudioInfo> audioInfo;

	// Token: 0x04003289 RID: 12937
	private HashSet<int> insulatedCells = new HashSet<int>();

	// Token: 0x0400328A RID: 12938
	private HashSet<int> radiantCells = new HashSet<int>();

	// Token: 0x0400328B RID: 12939
	private Game.ConduitVisInfo visInfo;

	// Token: 0x0400328C RID: 12940
	private ConduitFlowVisualizer.ConduitFlowMesh movingBallMesh;

	// Token: 0x0400328D RID: 12941
	private ConduitFlowVisualizer.ConduitFlowMesh staticBallMesh;

	// Token: 0x0400328E RID: 12942
	private int highlightedCell = -1;

	// Token: 0x0400328F RID: 12943
	private Color32 highlightColour = new Color(0.2f, 0.2f, 0.2f, 0.2f);

	// Token: 0x04003290 RID: 12944
	private ConduitFlowVisualizer.Tuning tuning;

	// Token: 0x04003291 RID: 12945
	private static WorkItemCollection<ConduitFlowVisualizer.RenderMeshTask, ConduitFlowVisualizer.RenderMeshContext> render_mesh_job = new WorkItemCollection<ConduitFlowVisualizer.RenderMeshTask, ConduitFlowVisualizer.RenderMeshContext>();

	// Token: 0x02001807 RID: 6151
	[Serializable]
	public class Tuning
	{
		// Token: 0x04006EBC RID: 28348
		public bool renderMesh;

		// Token: 0x04006EBD RID: 28349
		public float size;

		// Token: 0x04006EBE RID: 28350
		public float spriteCount;

		// Token: 0x04006EBF RID: 28351
		public float framesPerSecond;

		// Token: 0x04006EC0 RID: 28352
		public Texture2D backgroundTexture;

		// Token: 0x04006EC1 RID: 28353
		public Texture2D foregroundTexture;
	}

	// Token: 0x02001808 RID: 6152
	private class ConduitFlowMesh
	{
		// Token: 0x06008CAA RID: 36010 RVA: 0x00303050 File Offset: 0x00301250
		public ConduitFlowMesh()
		{
			this.mesh = new Mesh();
			this.mesh.name = "ConduitMesh";
			this.material = new Material(Shader.Find("Klei/ConduitBall"));
		}

		// Token: 0x06008CAB RID: 36011 RVA: 0x003030C0 File Offset: 0x003012C0
		public void AddQuad(Vector2 pos, Color32 color, float size, float is_foreground, float highlight, Vector2I uvbl, Vector2I uvtl, Vector2I uvbr, Vector2I uvtr)
		{
			float num = size * 0.5f;
			this.positions.Add(new Vector3(pos.x - num, pos.y - num, 0f));
			this.positions.Add(new Vector3(pos.x - num, pos.y + num, 0f));
			this.positions.Add(new Vector3(pos.x + num, pos.y - num, 0f));
			this.positions.Add(new Vector3(pos.x + num, pos.y + num, 0f));
			this.uvs.Add(new Vector4((float)uvbl.x, (float)uvbl.y, is_foreground, highlight));
			this.uvs.Add(new Vector4((float)uvtl.x, (float)uvtl.y, is_foreground, highlight));
			this.uvs.Add(new Vector4((float)uvbr.x, (float)uvbr.y, is_foreground, highlight));
			this.uvs.Add(new Vector4((float)uvtr.x, (float)uvtr.y, is_foreground, highlight));
			this.colors.Add(color);
			this.colors.Add(color);
			this.colors.Add(color);
			this.colors.Add(color);
			this.triangles.Add(this.quadIndex * 4);
			this.triangles.Add(this.quadIndex * 4 + 1);
			this.triangles.Add(this.quadIndex * 4 + 2);
			this.triangles.Add(this.quadIndex * 4 + 2);
			this.triangles.Add(this.quadIndex * 4 + 1);
			this.triangles.Add(this.quadIndex * 4 + 3);
			this.quadIndex++;
		}

		// Token: 0x06008CAC RID: 36012 RVA: 0x003032B3 File Offset: 0x003014B3
		public void SetTexture(string id, Texture2D texture)
		{
			this.material.SetTexture(id, texture);
		}

		// Token: 0x06008CAD RID: 36013 RVA: 0x003032C2 File Offset: 0x003014C2
		public void SetVector(string id, Vector4 data)
		{
			this.material.SetVector(id, data);
		}

		// Token: 0x06008CAE RID: 36014 RVA: 0x003032D1 File Offset: 0x003014D1
		public void Begin()
		{
			this.positions.Clear();
			this.uvs.Clear();
			this.triangles.Clear();
			this.colors.Clear();
			this.quadIndex = 0;
		}

		// Token: 0x06008CAF RID: 36015 RVA: 0x00303308 File Offset: 0x00301508
		public void End(float z, int layer)
		{
			this.mesh.Clear();
			this.mesh.SetVertices(this.positions);
			this.mesh.SetUVs(0, this.uvs);
			this.mesh.SetColors(this.colors);
			this.mesh.SetTriangles(this.triangles, 0, false);
			Graphics.DrawMesh(this.mesh, new Vector3(ConduitFlowVisualizer.GRID_OFFSET.x, ConduitFlowVisualizer.GRID_OFFSET.y, z - 0.1f), Quaternion.identity, this.material, layer);
		}

		// Token: 0x06008CB0 RID: 36016 RVA: 0x0030339E File Offset: 0x0030159E
		public void Cleanup()
		{
			UnityEngine.Object.Destroy(this.mesh);
			this.mesh = null;
			UnityEngine.Object.Destroy(this.material);
			this.material = null;
		}

		// Token: 0x04006EC2 RID: 28354
		private Mesh mesh;

		// Token: 0x04006EC3 RID: 28355
		private Material material;

		// Token: 0x04006EC4 RID: 28356
		private List<Vector3> positions = new List<Vector3>();

		// Token: 0x04006EC5 RID: 28357
		private List<Vector4> uvs = new List<Vector4>();

		// Token: 0x04006EC6 RID: 28358
		private List<int> triangles = new List<int>();

		// Token: 0x04006EC7 RID: 28359
		private List<Color32> colors = new List<Color32>();

		// Token: 0x04006EC8 RID: 28360
		private int quadIndex;
	}

	// Token: 0x02001809 RID: 6153
	private struct AudioInfo
	{
		// Token: 0x04006EC9 RID: 28361
		public int networkID;

		// Token: 0x04006ECA RID: 28362
		public int blobCount;

		// Token: 0x04006ECB RID: 28363
		public float distance;

		// Token: 0x04006ECC RID: 28364
		public Vector3 position;
	}

	// Token: 0x0200180A RID: 6154
	private class RenderMeshContext
	{
		// Token: 0x06008CB1 RID: 36017 RVA: 0x003033C4 File Offset: 0x003015C4
		public RenderMeshContext(ConduitFlowVisualizer outer, float lerp_percent, Vector2I min, Vector2I max)
		{
			this.outer = outer;
			this.lerp_percent = lerp_percent;
			this.visible_conduits = ListPool<int, ConduitFlowVisualizer>.Allocate();
			this.visible_conduits.Capacity = Math.Max(outer.flowManager.soaInfo.NumEntries, this.visible_conduits.Capacity);
			for (int num = 0; num != outer.flowManager.soaInfo.NumEntries; num++)
			{
				Vector2I vector2I = Grid.CellToXY(outer.flowManager.soaInfo.GetCell(num));
				if (min <= vector2I && vector2I <= max)
				{
					this.visible_conduits.Add(num);
				}
			}
		}

		// Token: 0x06008CB2 RID: 36018 RVA: 0x0030346C File Offset: 0x0030166C
		public void Finish()
		{
			this.visible_conduits.Recycle();
		}

		// Token: 0x04006ECD RID: 28365
		public ListPool<int, ConduitFlowVisualizer>.PooledList visible_conduits;

		// Token: 0x04006ECE RID: 28366
		public ConduitFlowVisualizer outer;

		// Token: 0x04006ECF RID: 28367
		public float lerp_percent;
	}

	// Token: 0x0200180B RID: 6155
	private struct RenderMeshTask : IWorkItem<ConduitFlowVisualizer.RenderMeshContext>
	{
		// Token: 0x06008CB3 RID: 36019 RVA: 0x0030347C File Offset: 0x0030167C
		public RenderMeshTask(int start, int end)
		{
			this.start = start;
			this.end = end;
			int num = end - start;
			this.moving_balls = ListPool<ConduitFlowVisualizer.RenderMeshTask.Ball, ConduitFlowVisualizer.RenderMeshTask>.Allocate();
			this.moving_balls.Capacity = num;
			this.static_balls = ListPool<ConduitFlowVisualizer.RenderMeshTask.Ball, ConduitFlowVisualizer.RenderMeshTask>.Allocate();
			this.static_balls.Capacity = num;
			this.moving_conduits = ListPool<ConduitFlow.Conduit, ConduitFlowVisualizer.RenderMeshTask>.Allocate();
			this.moving_conduits.Capacity = num;
		}

		// Token: 0x06008CB4 RID: 36020 RVA: 0x003034E0 File Offset: 0x003016E0
		public void Run(ConduitFlowVisualizer.RenderMeshContext context)
		{
			Element element = null;
			for (int num = this.start; num != this.end; num++)
			{
				ConduitFlow.Conduit conduit = context.outer.flowManager.soaInfo.GetConduit(context.visible_conduits[num]);
				ConduitFlow.ConduitFlowInfo lastFlowInfo = conduit.GetLastFlowInfo(context.outer.flowManager);
				ConduitFlow.ConduitContents initialContents = conduit.GetInitialContents(context.outer.flowManager);
				if (lastFlowInfo.contents.mass > 0f)
				{
					int cell = conduit.GetCell(context.outer.flowManager);
					int cellFromDirection = ConduitFlow.GetCellFromDirection(cell, lastFlowInfo.direction);
					Vector2I vector2I = Grid.CellToXY(cell);
					Vector2I vector2I2 = Grid.CellToXY(cellFromDirection);
					Vector2 vector = ((cell == -1) ? vector2I : Vector2.Lerp(new Vector2((float)vector2I.x, (float)vector2I.y), new Vector2((float)vector2I2.x, (float)vector2I2.y), context.lerp_percent));
					Color32 cellTintColour = context.outer.GetCellTintColour(cell);
					Color32 cellTintColour2 = context.outer.GetCellTintColour(cellFromDirection);
					Color32 color = Color32.Lerp(cellTintColour, cellTintColour2, context.lerp_percent);
					bool flag = false;
					if (context.outer.showContents)
					{
						if (lastFlowInfo.contents.mass >= initialContents.mass)
						{
							this.moving_balls.Add(new ConduitFlowVisualizer.RenderMeshTask.Ball(lastFlowInfo.direction, vector, color, context.outer.tuning.size, false, false));
						}
						if (element == null || lastFlowInfo.contents.element != element.id)
						{
							element = ElementLoader.FindElementByHash(lastFlowInfo.contents.element);
						}
					}
					else
					{
						element = null;
						flag = Grid.PosToCell(new Vector3(vector.x + ConduitFlowVisualizer.GRID_OFFSET.x, vector.y + ConduitFlowVisualizer.GRID_OFFSET.y, 0f)) == context.outer.highlightedCell;
					}
					Color32 contentsColor = context.outer.GetContentsColor(element, color);
					float num2 = 1f;
					if (context.outer.showContents || lastFlowInfo.contents.mass < initialContents.mass)
					{
						num2 = context.outer.CalculateMassScale(lastFlowInfo.contents.mass);
					}
					this.moving_balls.Add(new ConduitFlowVisualizer.RenderMeshTask.Ball(lastFlowInfo.direction, vector, contentsColor, context.outer.tuning.size * num2, true, flag));
					this.moving_conduits.Add(conduit);
				}
				if (initialContents.mass > lastFlowInfo.contents.mass && initialContents.mass > 0f)
				{
					int cell2 = conduit.GetCell(context.outer.flowManager);
					Vector2 vector2 = Grid.CellToXY(cell2);
					float num3 = initialContents.mass - lastFlowInfo.contents.mass;
					bool flag2 = false;
					Color32 cellTintColour3 = context.outer.GetCellTintColour(cell2);
					float num4 = context.outer.CalculateMassScale(num3);
					if (context.outer.showContents)
					{
						this.static_balls.Add(new ConduitFlowVisualizer.RenderMeshTask.Ball(ConduitFlow.FlowDirections.None, vector2, cellTintColour3, context.outer.tuning.size * num4, false, false));
						if (element == null || initialContents.element != element.id)
						{
							element = ElementLoader.FindElementByHash(initialContents.element);
						}
					}
					else
					{
						element = null;
						flag2 = cell2 == context.outer.highlightedCell;
					}
					Color32 contentsColor2 = context.outer.GetContentsColor(element, cellTintColour3);
					this.static_balls.Add(new ConduitFlowVisualizer.RenderMeshTask.Ball(ConduitFlow.FlowDirections.None, vector2, contentsColor2, context.outer.tuning.size * num4, true, flag2));
				}
			}
		}

		// Token: 0x06008CB5 RID: 36021 RVA: 0x00303884 File Offset: 0x00301A84
		public void Finish(ConduitFlowVisualizer.ConduitFlowMesh moving_ball_mesh, ConduitFlowVisualizer.ConduitFlowMesh static_ball_mesh, Vector3 camera_pos, ConduitFlowVisualizer visualizer)
		{
			for (int num = 0; num != this.moving_balls.Count; num++)
			{
				this.moving_balls[num].Consume(moving_ball_mesh);
			}
			this.moving_balls.Recycle();
			for (int num2 = 0; num2 != this.static_balls.Count; num2++)
			{
				this.static_balls[num2].Consume(static_ball_mesh);
			}
			this.static_balls.Recycle();
			if (visualizer != null)
			{
				foreach (ConduitFlow.Conduit conduit in this.moving_conduits)
				{
					visualizer.AddAudioSource(conduit, camera_pos);
				}
			}
			this.moving_conduits.Recycle();
		}

		// Token: 0x04006ED0 RID: 28368
		private ListPool<ConduitFlowVisualizer.RenderMeshTask.Ball, ConduitFlowVisualizer.RenderMeshTask>.PooledList moving_balls;

		// Token: 0x04006ED1 RID: 28369
		private ListPool<ConduitFlowVisualizer.RenderMeshTask.Ball, ConduitFlowVisualizer.RenderMeshTask>.PooledList static_balls;

		// Token: 0x04006ED2 RID: 28370
		private ListPool<ConduitFlow.Conduit, ConduitFlowVisualizer.RenderMeshTask>.PooledList moving_conduits;

		// Token: 0x04006ED3 RID: 28371
		private int start;

		// Token: 0x04006ED4 RID: 28372
		private int end;

		// Token: 0x020020E0 RID: 8416
		public struct Ball
		{
			// Token: 0x0600A563 RID: 42339 RVA: 0x00349C5D File Offset: 0x00347E5D
			public Ball(ConduitFlow.FlowDirections direction, Vector2 pos, Color32 color, float size, bool foreground, bool highlight)
			{
				this.pos = pos;
				this.size = size;
				this.color = color;
				this.direction = direction;
				this.foreground = foreground;
				this.highlight = highlight;
			}

			// Token: 0x0600A564 RID: 42340 RVA: 0x00349C8C File Offset: 0x00347E8C
			public static void InitializeResources()
			{
				ConduitFlowVisualizer.RenderMeshTask.Ball.uv_packs[ConduitFlow.FlowDirections.None] = new ConduitFlowVisualizer.RenderMeshTask.Ball.UVPack
				{
					bl = new Vector2I(0, 0),
					tl = new Vector2I(0, 1),
					br = new Vector2I(1, 0),
					tr = new Vector2I(1, 1)
				};
				ConduitFlowVisualizer.RenderMeshTask.Ball.uv_packs[ConduitFlow.FlowDirections.Left] = new ConduitFlowVisualizer.RenderMeshTask.Ball.UVPack
				{
					bl = new Vector2I(0, 0),
					tl = new Vector2I(0, 1),
					br = new Vector2I(1, 0),
					tr = new Vector2I(1, 1)
				};
				ConduitFlowVisualizer.RenderMeshTask.Ball.uv_packs[ConduitFlow.FlowDirections.Right] = ConduitFlowVisualizer.RenderMeshTask.Ball.uv_packs[ConduitFlow.FlowDirections.Left];
				ConduitFlowVisualizer.RenderMeshTask.Ball.uv_packs[ConduitFlow.FlowDirections.Up] = new ConduitFlowVisualizer.RenderMeshTask.Ball.UVPack
				{
					bl = new Vector2I(1, 0),
					tl = new Vector2I(0, 0),
					br = new Vector2I(1, 1),
					tr = new Vector2I(0, 1)
				};
				ConduitFlowVisualizer.RenderMeshTask.Ball.uv_packs[ConduitFlow.FlowDirections.Down] = ConduitFlowVisualizer.RenderMeshTask.Ball.uv_packs[ConduitFlow.FlowDirections.Up];
			}

			// Token: 0x0600A565 RID: 42341 RVA: 0x00349D91 File Offset: 0x00347F91
			private static ConduitFlowVisualizer.RenderMeshTask.Ball.UVPack GetUVPack(ConduitFlow.FlowDirections direction)
			{
				return ConduitFlowVisualizer.RenderMeshTask.Ball.uv_packs[direction];
			}

			// Token: 0x0600A566 RID: 42342 RVA: 0x00349DA0 File Offset: 0x00347FA0
			public void Consume(ConduitFlowVisualizer.ConduitFlowMesh mesh)
			{
				ConduitFlowVisualizer.RenderMeshTask.Ball.UVPack uvpack = ConduitFlowVisualizer.RenderMeshTask.Ball.GetUVPack(this.direction);
				mesh.AddQuad(this.pos, this.color, this.size, (float)(this.foreground ? 1 : 0), (float)(this.highlight ? 1 : 0), uvpack.bl, uvpack.tl, uvpack.br, uvpack.tr);
			}

			// Token: 0x04009271 RID: 37489
			private Vector2 pos;

			// Token: 0x04009272 RID: 37490
			private float size;

			// Token: 0x04009273 RID: 37491
			private Color32 color;

			// Token: 0x04009274 RID: 37492
			private ConduitFlow.FlowDirections direction;

			// Token: 0x04009275 RID: 37493
			private bool foreground;

			// Token: 0x04009276 RID: 37494
			private bool highlight;

			// Token: 0x04009277 RID: 37495
			private static Dictionary<ConduitFlow.FlowDirections, ConduitFlowVisualizer.RenderMeshTask.Ball.UVPack> uv_packs = new Dictionary<ConduitFlow.FlowDirections, ConduitFlowVisualizer.RenderMeshTask.Ball.UVPack>();

			// Token: 0x02002DB7 RID: 11703
			private class UVPack
			{
				// Token: 0x0400BA5E RID: 47710
				public Vector2I bl;

				// Token: 0x0400BA5F RID: 47711
				public Vector2I tl;

				// Token: 0x0400BA60 RID: 47712
				public Vector2I br;

				// Token: 0x0400BA61 RID: 47713
				public Vector2I tr;
			}
		}
	}
}
