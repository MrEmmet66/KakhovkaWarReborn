using SampSharp.Core;
using SampSharp.Entities;
using System.Text;

namespace NazismRp
{
	public class Program
	{
		static void Main(string[] args)
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			new GameModeBuilder()
				.UseEcs<Startup>().UseEncoding(Encoding.GetEncoding("windows-1251"))
				.Run();
		}
	}
}
