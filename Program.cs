using System;
using System.Runtime.InteropServices;
using Figgle;

namespace PrisonEscape
{
    class Program
    {
        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        static void Main()
        {
            if (OperatingSystem.IsWindows())
            {
                Console.Title = "Prison Escape";
                Console.WindowHeight = 50;
                Console.WindowWidth = 125;
            }

            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MINIMIZE, MF_BYCOMMAND);
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MAXIMIZE, MF_BYCOMMAND);
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_SIZE, MF_BYCOMMAND);

            bool showMenu = true;
            while (showMenu)
            {
                showMenu = Menu();
            }

            static bool Menu()
            {
                Console.Clear();
                Console.WriteLine(FiggleFonts.Standard.Render("Banking App"));
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1) Login to your Account");
                Console.WriteLine("2) Open an Account");
                Console.WriteLine("3) Exit");
                Console.Write("\r\nSelect an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        LogIn();
                        return true;
                    case "2":
                        OpenAccount();
                        return true;
                    case "3":
                        return false;
                    default:
                        return true;
                }

                static string? LogIn()
                {
                    Console.Write("Logged In.");
                    return Console.ReadLine();
                }
                static string? OpenAccount()
                {
                    Console.Write("Account Opened.");
                    return Console.ReadLine();
                }
            }
        }
    }
}