using System;

namespace BunnySimulation
{
#if WINDOWS || XBOX
    static class Program
    {
        static void Main()
        {
            using (Main game = new Main())
            {
                game.Run();
            }
        }
    }
#endif
}

