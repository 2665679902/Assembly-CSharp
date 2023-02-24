using System;
using System.Collections.Generic;
using FMOD.Studio;
using KSerialization;
using UnityEngine;

// Token: 0x020009B7 RID: 2487
[AddComponentMenu("KMonoBehaviour/scripts/UserNavigation")]
public class UserNavigation : KMonoBehaviour
{
	// Token: 0x060049E8 RID: 18920 RVA: 0x0019E49C File Offset: 0x0019C69C
	public UserNavigation()
	{
		for (global::Action action = global::Action.SetUserNav1; action <= global::Action.SetUserNav10; action++)
		{
			this.hotkeyNavPoints.Add(UserNavigation.NavPoint.Invalid);
		}
	}

	// Token: 0x060049E9 RID: 18921 RVA: 0x0019E4E3 File Offset: 0x0019C6E3
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Game.Instance.Subscribe(1983128072, delegate(object worlds)
		{
			global::Tuple<int, int> tuple = (global::Tuple<int, int>)worlds;
			int first = tuple.first;
			int second = tuple.second;
			int num = Grid.PosToCell(CameraController.Instance.transform.position);
			if (!Grid.IsValidCell(num) || (int)Grid.WorldIdx[num] != second)
			{
				WorldContainer world = ClusterManager.Instance.GetWorld(second);
				float num2 = Mathf.Clamp(CameraController.Instance.transform.position.x, world.minimumBounds.x, world.maximumBounds.x);
				float num3 = Mathf.Clamp(CameraController.Instance.transform.position.y, world.minimumBounds.y, world.maximumBounds.y);
				Vector3 vector = new Vector3(num2, num3, CameraController.Instance.transform.position.z);
				CameraController.Instance.SetPosition(vector);
			}
			this.worldCameraPositions[second] = new UserNavigation.NavPoint
			{
				pos = CameraController.Instance.transform.position,
				orthoSize = CameraController.Instance.targetOrthographicSize
			};
			if (!this.worldCameraPositions.ContainsKey(first))
			{
				WorldContainer world2 = ClusterManager.Instance.GetWorld(first);
				Vector2I vector2I = world2.WorldOffset + new Vector2I(world2.Width / 2, world2.Height / 2);
				this.worldCameraPositions.Add(first, new UserNavigation.NavPoint
				{
					pos = new Vector3((float)vector2I.x, (float)vector2I.y),
					orthoSize = CameraController.Instance.targetOrthographicSize
				});
			}
			CameraController.Instance.SetTargetPosForWorldChange(this.worldCameraPositions[first].pos, this.worldCameraPositions[first].orthoSize, false);
		});
	}

	// Token: 0x060049EA RID: 18922 RVA: 0x0019E508 File Offset: 0x0019C708
	public void SetWorldCameraStartPosition(int world_id, Vector3 start_pos)
	{
		if (!this.worldCameraPositions.ContainsKey(world_id))
		{
			this.worldCameraPositions.Add(world_id, new UserNavigation.NavPoint
			{
				pos = new Vector3(start_pos.x, start_pos.y),
				orthoSize = CameraController.Instance.targetOrthographicSize
			});
			return;
		}
		this.worldCameraPositions[world_id] = new UserNavigation.NavPoint
		{
			pos = new Vector3(start_pos.x, start_pos.y),
			orthoSize = CameraController.Instance.targetOrthographicSize
		};
	}

	// Token: 0x060049EB RID: 18923 RVA: 0x0019E5A0 File Offset: 0x0019C7A0
	private static int GetIndex(global::Action action)
	{
		int num = -1;
		if (global::Action.SetUserNav1 <= action && action <= global::Action.SetUserNav10)
		{
			num = action - global::Action.SetUserNav1;
		}
		else if (global::Action.GotoUserNav1 <= action && action <= global::Action.GotoUserNav10)
		{
			num = action - global::Action.GotoUserNav1;
		}
		return num;
	}

	// Token: 0x060049EC RID: 18924 RVA: 0x0019E5D0 File Offset: 0x0019C7D0
	private void SetHotkeyNavPoint(global::Action action, Vector3 pos, float ortho_size)
	{
		int index = UserNavigation.GetIndex(action);
		if (index < 0)
		{
			return;
		}
		this.hotkeyNavPoints[index] = new UserNavigation.NavPoint
		{
			pos = pos,
			orthoSize = ortho_size
		};
		EventInstance eventInstance = KFMOD.BeginOneShot(GlobalAssets.GetSound("UserNavPoint_set", false), Vector3.zero, 1f);
		eventInstance.setParameterByName("userNavPoint_ID", (float)index, false);
		KFMOD.EndOneShot(eventInstance);
	}

	// Token: 0x060049ED RID: 18925 RVA: 0x0019E640 File Offset: 0x0019C840
	private void GoToHotkeyNavPoint(global::Action action)
	{
		int index = UserNavigation.GetIndex(action);
		if (index < 0)
		{
			return;
		}
		UserNavigation.NavPoint navPoint = this.hotkeyNavPoints[index];
		if (navPoint.IsValid())
		{
			CameraController.Instance.SetTargetPos(navPoint.pos, navPoint.orthoSize, true);
			EventInstance eventInstance = KFMOD.BeginOneShot(GlobalAssets.GetSound("UserNavPoint_recall", false), Vector3.zero, 1f);
			eventInstance.setParameterByName("userNavPoint_ID", (float)index, false);
			KFMOD.EndOneShot(eventInstance);
		}
	}

	// Token: 0x060049EE RID: 18926 RVA: 0x0019E6B8 File Offset: 0x0019C8B8
	public bool Handle(KButtonEvent e)
	{
		bool flag = false;
		for (global::Action action = global::Action.GotoUserNav1; action <= global::Action.GotoUserNav10; action++)
		{
			if (e.TryConsume(action))
			{
				this.GoToHotkeyNavPoint(action);
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			for (global::Action action2 = global::Action.SetUserNav1; action2 <= global::Action.SetUserNav10; action2++)
			{
				if (e.TryConsume(action2))
				{
					Camera baseCamera = CameraController.Instance.baseCamera;
					Vector3 position = baseCamera.transform.GetPosition();
					this.SetHotkeyNavPoint(action2, position, baseCamera.orthographicSize);
					flag = true;
					break;
				}
			}
		}
		return flag;
	}

	// Token: 0x0400308F RID: 12431
	[Serialize]
	private List<UserNavigation.NavPoint> hotkeyNavPoints = new List<UserNavigation.NavPoint>();

	// Token: 0x04003090 RID: 12432
	[Serialize]
	private Dictionary<int, UserNavigation.NavPoint> worldCameraPositions = new Dictionary<int, UserNavigation.NavPoint>();

	// Token: 0x020017BE RID: 6078
	[Serializable]
	private struct NavPoint
	{
		// Token: 0x06008BB8 RID: 35768 RVA: 0x00300397 File Offset: 0x002FE597
		public bool IsValid()
		{
			return this.orthoSize != 0f;
		}

		// Token: 0x04006DE5 RID: 28133
		public Vector3 pos;

		// Token: 0x04006DE6 RID: 28134
		public float orthoSize;

		// Token: 0x04006DE7 RID: 28135
		public static readonly UserNavigation.NavPoint Invalid = new UserNavigation.NavPoint
		{
			pos = Vector3.zero,
			orthoSize = 0f
		};
	}
}
