namespace SelfiePuss.Utilities
{
	public static class ExtensionMethods
	{
		public static float Map(this float value , float inputFrom , float inputTo , float outputFrom , float outputTo)
		{
			var slope      = (outputTo - outputFrom) / (inputTo - inputFrom);
			var yintercept = outputFrom - slope * inputFrom;

			var mappedValue = slope * value + yintercept;

			return mappedValue;
		}

		public static int Map(this int value , float inputFrom , float inputTo , float outputFrom , float outputTo)
		{
			return (int) ((float) value).Map(inputFrom , inputTo , outputFrom , outputTo);
		}
	}
}