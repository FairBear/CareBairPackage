using AIProject;
using AIProject.UI;
using Manager;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CareBairPackage
{
	public static partial class UIMacro
	{
		static readonly ADMSheet sheet = new ADMSheet("UI快捷菜单", null, Condition);
		static MenuUIBehaviour current;

		public static void Update()
		{
			if (AltDialogMenu.IsSheet(sheet) || !Condition())
				return;

			RefreshSheets();
			AltDialogMenu.AddSheet(sheet);
		}

		public static void LateUpdate()
		{
			if (current != null)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}

		public static bool Condition()
		{
			if (!MapUIContainer.IsInstance())
				return false;

			if (current != null && !current.IsActiveControl)
			{
				Manager.Input input = Manager.Input.Instance;
				current = null;

				input.ReserveState(Manager.Input.ValidType.Action);
				input.SetupState();
				Map.Instance.Player.Controller.ChangeState("Normal");
			}

			SystemMenuUI ui = MapUIContainer.SystemMenuUI;

			if (!ui.IsActiveControl || !ui.HomeMenu.IsActiveControl)
				return false;

			return true;
		}

		private static ADMSheet MakeSheet(ActorController controller,
										  string label,
										  string state,
										  MenuUIBehaviour ui,
										  bool special = false)
		{
			return new ADMSheet(label, () =>
			{
				MapUIContainer.SystemMenuUI.IsActiveControl = false;
				ui.IsActiveControl = true;

				if (state != null)
					controller.ChangeState(state);

				if (special)
					current = ui;
			});
		}

		public static void RefreshSheets()
		{
			List<ADMSheet> sheets = new List<ADMSheet>();
			PlayerActor player = Map.Instance.Player;
			PlayerController controller = player.PlayerController;
			DevicePoint[] devices = Object.FindObjectsOfType<DevicePoint>();

			if (devices != null && devices.Length > 0)
			{
				DevicePoint device = devices.FirstOrDefault(v => v.ID == 0);

				if (device != null)
				{
					sheets.Add(new ADMSheet("数据终端", () =>
					{
						MapUIContainer.SystemMenuUI.IsActiveControl = false;

						Manager.Resources.Instance.SoundPack.Play(SoundPack.SystemSE.BootDevice);
						MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);

						player.CurrentDevicePoint = device;

						MapUIContainer.RefreshCommands(0, player.DeviceCommandInfos);
						MapUIContainer.SetActiveCommandList(true, "データ端末");
						controller.ChangeState("DeviceMenu");
					}));
				}
			}

			sheets.Add(MakeSheet(controller, /*"Item Box"*/"物品箱", "ItemBox", MapUIContainer.ItemBoxUI));
			sheets.Add(MakeSheet(controller, /*"Bathing Outfits"*/"更衣处", "DressRoom", MapUIContainer.DressRoomUI));
			sheets.Add(MakeSheet(controller, /*"Closet Outfits"*/ "服装棚", "ClothChange", MapUIContainer.ClosetUI));
			sheets.Add(MakeSheet(controller, /*"Shan's Shop"*/"商店", null, MapUIContainer.ShopUI, true));

			sheets.Add(new ADMSheet(/*"Kitchen"*/"灶台", () =>
			{
				MapUIContainer.SystemMenuUI.IsActiveControl = false;

				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
				MapUIContainer.RefreshCommands(0, player.CookCommandInfos);
				MapUIContainer.SetActiveCommandList(true, "料理");
				controller.ChangeState("Kitchen");
			}));

			sheets.Add(MakeSheet(controller, /*"Pet Synthesis"*/"宠物合成", "Craft", MapUIContainer.PetCraftUI, true));
			sheets.Add(MakeSheet(controller, /*"Medicine Table"*/"制药台", "Craft", MapUIContainer.MedicineCraftUI, true));
			sheets.Add(MakeSheet(controller, /*"Jukebox"*/"更改BGM", null, MapUIContainer.JukeBoxUI, true));

			sheet.SetSheets(sheets);
		}
	}
}
