using System;

namespace Indy500
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Indy500Game())
                game.Run();
        }
    }
}
