
namespace SettingsObjectModelCodeGenerator
{
    public static partial class Helper
	{				
		#region String Helpers

		public static bool IsNullOrEmpty(string str)
		{
			if (str == null)
			{
				return true;
			}

			if (str.Trim().Length == 0)
			{
				return true;
			}

			return false;
		}

		public static bool IsNotNullOrEmpty(string str)
		{
			if (str == null)
			{
				return false;
			}

			if (str.Trim().Length == 0)
			{
				return false;
			}

			return true;
		}

		#endregion
	}
}
