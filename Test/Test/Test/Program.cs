using System;

namespace Test
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
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

