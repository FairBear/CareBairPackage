using AIProject;
using Manager;

namespace CareBairPackage
{
	public static partial class ProfileInspector
	{
		public static void LocomotionProfile()
		{
			Resources resources = Resources.Instance;
			LocomotionProfile profile = resources.LocomotionProfile;

			string PlayerSpeedSetting = "PlayerSpeedSetting";
			Struct<float>(
				profile.PlayerSpeed,
				() =>
				{
					SetValue(
						Resources.Instance.LocomotionProfile,
						"_playerSpeed",
						new LocomotionProfile.PlayerSpeedSetting
						{
							normalSpeed = (float)entries[$"{PlayerSpeedSetting} normalSpeed"].value,
							walkSpeed = (float)entries[$"{PlayerSpeedSetting} walkSpeed"].value
						}
					);
				},
				PlayerSpeedSetting,
				"normalSpeed",
				"walkSpeed"
			);

			string AgentSpeedSetting = "AgentSpeedSetting";
			Struct<float>(
				profile.AgentSpeed,
				() =>
				{
					SetValue(
						profile,
						"_agentSpeed",
						new LocomotionProfile.AgentSpeedSetting
						{
							walkSpeed = (float)entries[$"{AgentSpeedSetting} walkSpeed"].value,
							runSpeed = (float)entries[$"{AgentSpeedSetting} runSpeed"].value,
							hurtSpeed = (float)entries[$"{AgentSpeedSetting} hurtSpeed"].value,
							escapeSpeed = (float)entries[$"{AgentSpeedSetting} escapeSpeed"].value,
							followRunSpeed = (float)entries[$"{AgentSpeedSetting} followRunSpeed"].value
						}
					);
				},
				AgentSpeedSetting,
				"walkSpeed",
				"runSpeed",
				"hurtSpeed",
				"escapeSpeed",
				"followRunSpeed"
			);

			string MerchantSpeedSetting = "MerchantSpeedSetting";
			Struct<float>(
				profile.MerchantSpeed,
				() =>
				{
					SetValue(
						profile,
						"_merchantSpeed",
						new LocomotionProfile.MerchantSpeedSetting
						{
							walkSpeed = (float)entries[$"{MerchantSpeedSetting} walkSpeed"].value,
							runSpeed = (float)entries[$"{MerchantSpeedSetting} runSpeed"].value
						}
					);
				},
				MerchantSpeedSetting,
				"walkSpeed",
				"runSpeed"
			);
		}
	}
}
