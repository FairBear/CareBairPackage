namespace CareBairPackage
{
	public class ADMSheetSlider
	{
		public int value;
		public int min;
		public int max;

		public ADMSheetSlider(int value = 0, int min = 0, int max = 10)
		{
			this.value = value;
			this.min = min;
			this.max = max;
		}
	}
}
