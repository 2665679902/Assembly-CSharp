﻿using System;
using UnityEngine;

// Token: 0x020004A6 RID: 1190
[AddComponentMenu("KMonoBehaviour/scripts/NavPathDrawer")]
public class NavPathDrawer : KMonoBehaviour
{
	// Token: 0x17000108 RID: 264
	// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x0008FEA7 File Offset: 0x0008E0A7
	// (set) Token: 0x06001AE9 RID: 6889 RVA: 0x0008FEAE File Offset: 0x0008E0AE
	public static NavPathDrawer Instance { get; private set; }

	// Token: 0x06001AEA RID: 6890 RVA: 0x0008FEB6 File Offset: 0x0008E0B6
	public static void DestroyInstance()
	{
		NavPathDrawer.Instance = null;
	}

	// Token: 0x06001AEB RID: 6891 RVA: 0x0008FEC0 File Offset: 0x0008E0C0
	protected override void OnPrefabInit()
	{
		Shader shader = Shader.Find("Lines/Colored Blended");
		this.material = new Material(shader);
		NavPathDrawer.Instance = this;
	}

	// Token: 0x06001AEC RID: 6892 RVA: 0x0008FEEA File Offset: 0x0008E0EA
	protected override void OnCleanUp()
	{
		NavPathDrawer.Instance = null;
	}

	// Token: 0x06001AED RID: 6893 RVA: 0x0008FEF2 File Offset: 0x0008E0F2
	public void DrawPath(Vector3 navigator_pos, PathFinder.Path path)
	{
		this.navigatorPos = navigator_pos;
		this.navigatorPos.y = this.navigatorPos.y + 0.5f;
		this.path = path;
	}

	// Token: 0x06001AEE RID: 6894 RVA: 0x0008FF1E File Offset: 0x0008E11E
	public Navigator GetNavigator()
	{
		return this.navigator;
	}

	// Token: 0x06001AEF RID: 6895 RVA: 0x0008FF26 File Offset: 0x0008E126
	public void SetNavigator(Navigator navigator)
	{
		this.navigator = navigator;
	}

	// Token: 0x06001AF0 RID: 6896 RVA: 0x0008FF2F File Offset: 0x0008E12F
	public void ClearNavigator()
	{
		this.navigator = null;
	}

	// Token: 0x06001AF1 RID: 6897 RVA: 0x0008FF38 File Offset: 0x0008E138
	private void DrawPath(PathFinder.Path path, Vector3 navigator_pos, Color color)
	{
		if (path.nodes != null && path.nodes.Count > 1)
		{
			GL.PushMatrix();
			this.material.SetPass(0);
			GL.Begin(1);
			GL.Color(color);
			GL.Vertex(navigator_pos);
			GL.Vertex(NavTypeHelper.GetNavPos(path.nodes[1].cell, path.nodes[1].navType));
			for (int i = 1; i < path.nodes.Count - 1; i++)
			{
				if ((int)Grid.WorldIdx[path.nodes[i].cell] == ClusterManager.Instance.activeWorldId && (int)Grid.WorldIdx[path.nodes[i + 1].cell] == ClusterManager.Instance.activeWorldId)
				{
					Vector3 navPos = NavTypeHelper.GetNavPos(path.nodes[i].cell, path.nodes[i].navType);
					Vector3 navPos2 = NavTypeHelper.GetNavPos(path.nodes[i + 1].cell, path.nodes[i + 1].navType);
					GL.Vertex(navPos);
					GL.Vertex(navPos2);
				}
			}
			GL.End();
			GL.PopMatrix();
		}
	}

	// Token: 0x06001AF2 RID: 6898 RVA: 0x00090084 File Offset: 0x0008E284
	private void OnPostRender()
	{
		this.DrawPath(this.path, this.navigatorPos, Color.white);
		this.path = default(PathFinder.Path);
		this.DebugDrawSelectedNavigator();
		if (this.navigator != null)
		{
			GL.PushMatrix();
			this.material.SetPass(0);
			GL.Begin(1);
			PathFinderQuery pathFinderQuery = PathFinderQueries.drawNavGridQuery.Reset(null);
			this.navigator.RunQuery(pathFinderQuery);
			GL.End();
			GL.PopMatrix();
		}
	}

	// Token: 0x06001AF3 RID: 6899 RVA: 0x00090104 File Offset: 0x0008E304
	private void DebugDrawSelectedNavigator()
	{
		if (!DebugHandler.DebugPathFinding)
		{
			return;
		}
		if (SelectTool.Instance == null)
		{
			return;
		}
		if (SelectTool.Instance.selected == null)
		{
			return;
		}
		Navigator component = SelectTool.Instance.selected.GetComponent<Navigator>();
		if (component == null)
		{
			return;
		}
		int mouseCell = DebugHandler.GetMouseCell();
		if (Grid.IsValidCell(mouseCell))
		{
			PathFinder.PotentialPath potentialPath = new PathFinder.PotentialPath(Grid.PosToCell(component), component.CurrentNavType, component.flags);
			PathFinder.Path path = default(PathFinder.Path);
			PathFinder.UpdatePath(component.NavGrid, component.GetCurrentAbilities(), potentialPath, PathFinderQueries.cellQuery.Reset(mouseCell), ref path);
			string text = "";
			text = text + "Source: " + Grid.PosToCell(component).ToString() + "\n";
			text = text + "Dest: " + mouseCell.ToString() + "\n";
			text = text + "Cost: " + path.cost.ToString();
			this.DrawPath(path, component.GetComponent<KAnimControllerBase>().GetPivotSymbolPosition(), Color.green);
			DebugText.Instance.Draw(text, Grid.CellToPosCCC(mouseCell, Grid.SceneLayer.Move), Color.white);
		}
	}

	// Token: 0x04000EF2 RID: 3826
	private PathFinder.Path path;

	// Token: 0x04000EF3 RID: 3827
	public Material material;

	// Token: 0x04000EF4 RID: 3828
	private Vector3 navigatorPos;

	// Token: 0x04000EF5 RID: 3829
	private Navigator navigator;
}
