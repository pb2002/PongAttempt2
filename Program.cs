﻿using System;

namespace Pong
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new PongGame())
                game.Run();
        }
    }
}