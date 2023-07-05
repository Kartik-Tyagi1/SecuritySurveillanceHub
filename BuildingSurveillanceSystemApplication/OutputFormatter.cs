using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingSurveillanceSystemApplication
{
    public static class OutputFormatter
    {
        public enum TextOutputTheme
        {
            Security,
            Employee,
            Normal
        }

        public static void ChangeOutputTheme(TextOutputTheme textOutputTheme)
        {
            if(textOutputTheme == TextOutputTheme.Employee)
            {
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (textOutputTheme == TextOutputTheme.Security)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (textOutputTheme == TextOutputTheme.Normal)
            {
                Console.ResetColor();
            }
        }
    }
}
