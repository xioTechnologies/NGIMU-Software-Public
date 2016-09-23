using System.IO;

namespace SettingsObjectModelCodeGenerator
{
    public static partial class Helper
	{				
		public static void CopyStream(Stream input, Stream output)
		{
			byte[] buffer = new byte[2048];

			while (true)
			{
				int read = input.Read(buffer, 0, buffer.Length);

				if (read <= 0)
				{
					return;
				}

				output.Write(buffer, 0, read);
			}
		}
	}
}
