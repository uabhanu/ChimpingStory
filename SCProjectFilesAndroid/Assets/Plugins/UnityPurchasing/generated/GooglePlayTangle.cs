#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("tjU7NAS2NT42tjU1NJDusDBlB0ZJd68m3HPB28Id27Efy2lxvuXs5I93/gFOD0467+yZGiw8c58IA9fu4T9CpQOtVokkd8uDFHfMRN9+yJw6utRzf4588B3AauAUtqO+gke7ewS2NRYEOTI9HrJ8ssM5NTU1MTQ3kfjys3J3yicNS5UCVzrS+wj6e0REQ4S/AjP4PJ8CPd52ybkLhm2iHgcHyF+1J7HUaQbTEhM36HqOq+eJJjVOh0xFyebsKHVbtMz85Zixsfqws/NCiJP3ZHMhvGcmwq6g3SbGnyjWttF8r6HqQuxdOoxYxUoBWA7gb9BCbWYJ1LBk2B6X5u6Bhu37YJJPoAoqFpTpwqJuUiTOfKnuh9x47lDbhSMX7JGPBTY3NTQ1");
        private static int[] order = new int[] { 1,11,9,9,4,11,8,8,13,13,10,13,13,13,14 };
        private static int key = 52;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
