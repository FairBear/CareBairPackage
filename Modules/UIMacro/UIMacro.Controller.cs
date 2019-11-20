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
		static readonly ADMSheet sheet = new ADMSheet("UI Macro", null, Condition);
		static DevicePoint device;
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
			if (!Enabled.Value || !MapUIContainer.IsInstance())
			{
				if (current != null)
					current = null;

				return false;
			}

			if (current != null)
			{
				if (!current.IsActiveControl)
				{
					current = null;

					Manager.Input.Instance.ReserveState(Manager.Input.ValidType.Action);
					Manager.Input.Instance.SetupState();
					Map.Instance.Player.Controller.ChangeState("Normal");
				}
			}

			var ui = MapUIContainer.SystemMenuUI;

			if (!ui.IsActiveControl || !ui.HomeMenu.IsActiveControl)
			{
				if (device != null)
					device = null;

				return false;
			}

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
			ActorController controller = player.Controller;

			if (device == null)
			{
				DevicePoint[] devices = Object.FindObjectsOfType<DevicePoint>();

				if (devices != null && devices.Length > 0)
				{
					device = devices.FirstOrDefault(v => v.ID == 0);

					if (device != null)
					{
						sheets.Add(new ADMSheet("Data Terminal", () =>
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
			}

			sheets.Add(MakeSheet(controller, "Item Box", "ItemBox", MapUIContainer.ItemBoxUI));
			sheets.Add(MakeSheet(controller, "Bathing Outfits", "DressRoom", MapUIContainer.DressRoomUI));
			sheets.Add(MakeSheet(controller, "Closet Outfits", "ClothChange", MapUIContainer.ClosetUI));
			sheets.Add(MakeSheet(controller, "Shan's Shop", null, MapUIContainer.ShopUI, true));

			sheets.Add(new ADMSheet("Kitchen", () =>
			{
				MapUIContainer.SystemMenuUI.IsActiveControl = false;

				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
				MapUIContainer.RefreshCommands(0, player.CookCommandInfos);
				MapUIContainer.SetActiveCommandList(true, "料理");
				controller.ChangeState("Kitchen");
			}));

			sheets.Add(MakeSheet(controller, "Pet Synthesis", "Craft", MapUIContainer.PetCraftUI, true));
			sheets.Add(MakeSheet(controller, "Medicine Table", "Craft", MapUIContainer.MedicineCraftUI, true));
			sheets.Add(MakeSheet(controller, "Jukebox", null, MapUIContainer.JukeBoxUI, true));

			sheet.SetSheets(sheets);
		}
	}
}
