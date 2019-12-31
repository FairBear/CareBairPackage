using UnityEngine;

namespace CareBairPackage
{
	public static partial class HOverhaul
	{
		static float GetMeterAt(bool can, float meter, float at)
		{
			if (can)
			{
				if (at < 0f)
					return Random.value * (1f - meter) + meter;
				else
					return at;
			}
			
			return -1f;
		}

		static bool BothCan(HSceneFlagCtrl flagCtrl)
		{
			return flagCtrl.feel_m >= bothMeter & flagCtrl.feel_f >= bothMeter;
		}
	}
}
