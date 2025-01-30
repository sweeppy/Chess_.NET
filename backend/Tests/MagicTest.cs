using Chess.Main.Core.Helpers.MagicBitboards;
using Tests.Helpers;

namespace Tests
{
    public class MagicTest
    {
        [Fact]
        public void TestMagicNumbersInitialization()
        {
            ulong magic = MagicsStore.GetMagicNumberValue(0, false);
            Console.WriteLine($"Magic number: {magic}");
        }
        
    }
}