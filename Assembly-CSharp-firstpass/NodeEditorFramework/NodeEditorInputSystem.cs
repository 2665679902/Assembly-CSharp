using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace NodeEditorFramework
{
	// Token: 0x02000480 RID: 1152
	public static class NodeEditorInputSystem
	{
		// Token: 0x06003190 RID: 12688 RVA: 0x0006417C File Offset: 0x0006237C
		public static void SetupInput()
		{
			NodeEditorInputSystem.eventHandlers = new List<KeyValuePair<EventHandlerAttribute, Delegate>>();
			NodeEditorInputSystem.hotkeyHandlers = new List<KeyValuePair<HotkeyAttribute, Delegate>>();
			NodeEditorInputSystem.contextEntries = new List<KeyValuePair<ContextEntryAttribute, PopupMenu.MenuFunctionData>>();
			NodeEditorInputSystem.contextFillers = new List<KeyValuePair<ContextFillerAttribute, Delegate>>();
			foreach (Assembly assembly2 in from assembly in AppDomain.CurrentDomain.GetAssemblies()
				where assembly.FullName.Contains("Assembly")
				select assembly)
			{
				Type[] types = assembly2.GetTypes();
				for (int i = 0; i < types.Length; i++)
				{
					MethodInfo[] methods = types[i].GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
					for (int j = 0; j < methods.Length; j++)
					{
						MethodInfo methodInfo = methods[j];
						Delegate actionDelegate = null;
						PopupMenu.MenuFunctionData <>9__3;
						foreach (object obj in methodInfo.GetCustomAttributes(true))
						{
							Type type = obj.GetType();
							if (type == typeof(EventHandlerAttribute))
							{
								if (EventHandlerAttribute.AssureValidity(methodInfo, obj as EventHandlerAttribute))
								{
									if (actionDelegate == null)
									{
										actionDelegate = Delegate.CreateDelegate(typeof(Action<NodeEditorInputInfo>), methodInfo);
									}
									NodeEditorInputSystem.eventHandlers.Add(new KeyValuePair<EventHandlerAttribute, Delegate>(obj as EventHandlerAttribute, actionDelegate));
								}
							}
							else if (type == typeof(HotkeyAttribute))
							{
								if (HotkeyAttribute.AssureValidity(methodInfo, obj as HotkeyAttribute))
								{
									if (actionDelegate == null)
									{
										actionDelegate = Delegate.CreateDelegate(typeof(Action<NodeEditorInputInfo>), methodInfo);
									}
									NodeEditorInputSystem.hotkeyHandlers.Add(new KeyValuePair<HotkeyAttribute, Delegate>(obj as HotkeyAttribute, actionDelegate));
								}
							}
							else if (type == typeof(ContextEntryAttribute))
							{
								if (ContextEntryAttribute.AssureValidity(methodInfo, obj as ContextEntryAttribute))
								{
									if (actionDelegate == null)
									{
										actionDelegate = Delegate.CreateDelegate(typeof(Action<NodeEditorInputInfo>), methodInfo);
									}
									PopupMenu.MenuFunctionData menuFunctionData;
									if ((menuFunctionData = <>9__3) == null)
									{
										menuFunctionData = (<>9__3 = delegate(object callbackObj)
										{
											if (!(callbackObj is NodeEditorInputInfo))
											{
												throw new UnityException("Callback Object passed by context is not of type NodeEditorMenuCallback!");
											}
											actionDelegate.DynamicInvoke(new object[] { callbackObj as NodeEditorInputInfo });
										});
									}
									PopupMenu.MenuFunctionData menuFunctionData2 = menuFunctionData;
									NodeEditorInputSystem.contextEntries.Add(new KeyValuePair<ContextEntryAttribute, PopupMenu.MenuFunctionData>(obj as ContextEntryAttribute, menuFunctionData2));
								}
							}
							else if (type == typeof(ContextFillerAttribute) && ContextFillerAttribute.AssureValidity(methodInfo, obj as ContextFillerAttribute))
							{
								Delegate @delegate = Delegate.CreateDelegate(typeof(Action<NodeEditorInputInfo, GenericMenu>), methodInfo);
								NodeEditorInputSystem.contextFillers.Add(new KeyValuePair<ContextFillerAttribute, Delegate>(obj as ContextFillerAttribute, @delegate));
							}
						}
					}
				}
			}
			NodeEditorInputSystem.eventHandlers.Sort((KeyValuePair<EventHandlerAttribute, Delegate> handlerA, KeyValuePair<EventHandlerAttribute, Delegate> handlerB) => handlerA.Key.priority.CompareTo(handlerB.Key.priority));
			NodeEditorInputSystem.hotkeyHandlers.Sort((KeyValuePair<HotkeyAttribute, Delegate> handlerA, KeyValuePair<HotkeyAttribute, Delegate> handlerB) => handlerA.Key.priority.CompareTo(handlerB.Key.priority));
		}

		// Token: 0x06003191 RID: 12689 RVA: 0x000644A4 File Offset: 0x000626A4
		private static void CallEventHandlers(NodeEditorInputInfo inputInfo, bool late)
		{
			object[] array = new object[] { inputInfo };
			foreach (KeyValuePair<EventHandlerAttribute, Delegate> keyValuePair in NodeEditorInputSystem.eventHandlers)
			{
				if (keyValuePair.Key.handledEvent != null)
				{
					EventType? handledEvent = keyValuePair.Key.handledEvent;
					EventType type = inputInfo.inputEvent.type;
					if (!((handledEvent.GetValueOrDefault() == type) & (handledEvent != null)))
					{
						continue;
					}
				}
				if (late ? (keyValuePair.Key.priority >= 100) : (keyValuePair.Key.priority < 100))
				{
					keyValuePair.Value.DynamicInvoke(array);
					if (inputInfo.inputEvent.type == EventType.Used)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06003192 RID: 12690 RVA: 0x00064590 File Offset: 0x00062790
		private static void CallHotkeys(NodeEditorInputInfo inputInfo, KeyCode keyCode, EventModifiers mods)
		{
			object[] array = new object[] { inputInfo };
			foreach (KeyValuePair<HotkeyAttribute, Delegate> keyValuePair in NodeEditorInputSystem.hotkeyHandlers)
			{
				if (keyValuePair.Key.handledHotKey == keyCode)
				{
					if (keyValuePair.Key.modifiers != null)
					{
						EventModifiers? modifiers = keyValuePair.Key.modifiers;
						if (!((modifiers.GetValueOrDefault() == mods) & (modifiers != null)))
						{
							continue;
						}
					}
					if (keyValuePair.Key.limitingEventType != null)
					{
						EventType? limitingEventType = keyValuePair.Key.limitingEventType;
						EventType type = inputInfo.inputEvent.type;
						if (!((limitingEventType.GetValueOrDefault() == type) & (limitingEventType != null)))
						{
							continue;
						}
					}
					keyValuePair.Value.DynamicInvoke(array);
					if (inputInfo.inputEvent.type == EventType.Used)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06003193 RID: 12691 RVA: 0x000646A0 File Offset: 0x000628A0
		private static void FillContextMenu(NodeEditorInputInfo inputInfo, GenericMenu contextMenu, ContextType contextType)
		{
			foreach (KeyValuePair<ContextEntryAttribute, PopupMenu.MenuFunctionData> keyValuePair in NodeEditorInputSystem.contextEntries)
			{
				if (keyValuePair.Key.contextType == contextType)
				{
					contextMenu.AddItem(new GUIContent(keyValuePair.Key.contextPath), false, keyValuePair.Value, inputInfo);
				}
			}
			object[] array = new object[] { inputInfo, contextMenu };
			foreach (KeyValuePair<ContextFillerAttribute, Delegate> keyValuePair2 in NodeEditorInputSystem.contextFillers)
			{
				if (keyValuePair2.Key.contextType == contextType)
				{
					keyValuePair2.Value.DynamicInvoke(array);
				}
			}
		}

		// Token: 0x06003194 RID: 12692 RVA: 0x00064784 File Offset: 0x00062984
		public static void HandleInputEvents(NodeEditorState state)
		{
			if (NodeEditorInputSystem.shouldIgnoreInput(state))
			{
				return;
			}
			NodeEditorInputInfo nodeEditorInputInfo = new NodeEditorInputInfo(state);
			NodeEditorInputSystem.CallEventHandlers(nodeEditorInputInfo, false);
			NodeEditorInputSystem.CallHotkeys(nodeEditorInputInfo, Event.current.keyCode, Event.current.modifiers);
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x000647B5 File Offset: 0x000629B5
		public static void HandleLateInputEvents(NodeEditorState state)
		{
			if (NodeEditorInputSystem.shouldIgnoreInput(state))
			{
				return;
			}
			NodeEditorInputSystem.CallEventHandlers(new NodeEditorInputInfo(state), true);
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x000647CC File Offset: 0x000629CC
		internal static bool shouldIgnoreInput(NodeEditorState state)
		{
			if (OverlayGUI.HasPopupControl())
			{
				return true;
			}
			if (!state.canvasRect.Contains(Event.current.mousePosition))
			{
				return true;
			}
			for (int i = 0; i < state.ignoreInput.Count; i++)
			{
				if (state.ignoreInput[i].Contains(Event.current.mousePosition))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003197 RID: 12695 RVA: 0x00064834 File Offset: 0x00062A34
		[EventHandler(-4)]
		private static void HandleFocussing(NodeEditorInputInfo inputInfo)
		{
			NodeEditorState editorState = inputInfo.editorState;
			editorState.focusedNode = NodeEditor.NodeAtPosition(NodeEditor.ScreenToCanvasSpace(inputInfo.inputPos), out editorState.focusedNodeKnob);
			if (NodeEditorInputSystem.unfocusControlsForState == editorState && Event.current.type == EventType.Repaint)
			{
				GUIUtility.hotControl = 0;
				GUIUtility.keyboardControl = 0;
				NodeEditorInputSystem.unfocusControlsForState = null;
			}
		}

		// Token: 0x06003198 RID: 12696 RVA: 0x00064890 File Offset: 0x00062A90
		[EventHandler(EventType.MouseDown, -2)]
		private static void HandleSelecting(NodeEditorInputInfo inputInfo)
		{
			NodeEditorState editorState = inputInfo.editorState;
			if (inputInfo.inputEvent.button == 0 && editorState.focusedNode != editorState.selectedNode)
			{
				NodeEditorInputSystem.unfocusControlsForState = editorState;
				editorState.selectedNode = editorState.focusedNode;
				NodeEditor.RepaintClients();
			}
		}

		// Token: 0x06003199 RID: 12697 RVA: 0x000648DC File Offset: 0x00062ADC
		[EventHandler(EventType.MouseDown, 0)]
		private static void HandleContextClicks(NodeEditorInputInfo inputInfo)
		{
			if (Event.current.button == 1)
			{
				GenericMenu genericMenu = new GenericMenu();
				if (inputInfo.editorState.focusedNode != null)
				{
					NodeEditorInputSystem.FillContextMenu(inputInfo, genericMenu, ContextType.Node);
				}
				else
				{
					NodeEditorInputSystem.FillContextMenu(inputInfo, genericMenu, ContextType.Canvas);
				}
				genericMenu.Show(inputInfo.inputPos, 40f);
				Event.current.Use();
			}
		}

		// Token: 0x04001129 RID: 4393
		private static List<KeyValuePair<EventHandlerAttribute, Delegate>> eventHandlers;

		// Token: 0x0400112A RID: 4394
		private static List<KeyValuePair<HotkeyAttribute, Delegate>> hotkeyHandlers;

		// Token: 0x0400112B RID: 4395
		private static List<KeyValuePair<ContextEntryAttribute, PopupMenu.MenuFunctionData>> contextEntries;

		// Token: 0x0400112C RID: 4396
		private static List<KeyValuePair<ContextFillerAttribute, Delegate>> contextFillers;

		// Token: 0x0400112D RID: 4397
		private static NodeEditorState unfocusControlsForState;
	}
}
