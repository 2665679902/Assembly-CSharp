using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace NodeEditorFramework
{
	// Token: 0x02000478 RID: 1144
	public class NodeCanvasManager
	{
		// Token: 0x06003147 RID: 12615 RVA: 0x00062940 File Offset: 0x00060B40
		public static void GetAllCanvasTypes()
		{
			NodeCanvasManager.TypeOfCanvases = new Dictionary<Type, NodeCanvasTypeData>();
			foreach (Assembly assembly2 in from assembly in AppDomain.CurrentDomain.GetAssemblies()
				where assembly.FullName.Contains("Assembly")
				select assembly)
			{
				foreach (Type type in from T in assembly2.GetTypes()
					where T.IsClass && !T.IsAbstract && T.GetCustomAttributes(typeof(NodeCanvasTypeAttribute), false).Length != 0
					select T)
				{
					NodeCanvasTypeAttribute nodeCanvasTypeAttribute = type.GetCustomAttributes(typeof(NodeCanvasTypeAttribute), false)[0] as NodeCanvasTypeAttribute;
					NodeCanvasManager.TypeOfCanvases.Add(type, new NodeCanvasTypeData
					{
						CanvasType = type,
						DisplayString = nodeCanvasTypeAttribute.Name
					});
				}
			}
		}

		// Token: 0x06003148 RID: 12616 RVA: 0x00062A58 File Offset: 0x00060C58
		private static void CreateNewCanvas(object userdata)
		{
			NodeCanvasTypeData nodeCanvasTypeData = (NodeCanvasTypeData)userdata;
			NodeCanvasManager._callBack(nodeCanvasTypeData.CanvasType);
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x00062A7C File Offset: 0x00060C7C
		public static void PopulateMenu(ref GenericMenu menu, Action<Type> newNodeCanvas)
		{
			NodeCanvasManager._callBack = newNodeCanvas;
			foreach (KeyValuePair<Type, NodeCanvasTypeData> keyValuePair in NodeCanvasManager.TypeOfCanvases)
			{
				menu.AddItem(new GUIContent(keyValuePair.Value.DisplayString), false, new PopupMenu.MenuFunctionData(NodeCanvasManager.CreateNewCanvas), keyValuePair.Value);
			}
		}

		// Token: 0x040010FF RID: 4351
		public static Dictionary<Type, NodeCanvasTypeData> TypeOfCanvases;

		// Token: 0x04001100 RID: 4352
		private static Action<Type> _callBack;
	}
}
