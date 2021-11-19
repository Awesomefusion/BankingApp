using System;
using System.Runtime.InteropServices;
using Figgle;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace PrisonEscape
{
   public class DataContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public string DbPath { get; private set; }

        public DataContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}accounts.db";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
    }
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
                Console.WriteLine(FiggleFonts.Standard.Render("Equibank App"));
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

                static string LogIn()
                {
                    Console.Write("Logged In.");
                    return Console.ReadLine();
                }
                static string OpenAccount()
                {
                    using (var db = new DataContext())
                    {
                        Console.WriteLine(FiggleFonts.Standard.Render("Thank you for choosing Equibank"));

                        var account = new Account();
                        account.Id = new Guid();

                        Console.WriteLine("Enter your Name, no longer than 21 characters: ");
                        account.Name = Console.ReadLine();
                            if (account.Name.Length > 21)
                            {
                                Console.WriteLine("Sorry, your name is too long!");
                            }
                            else
                            {
                                Console.WriteLine("Accepted");
                            }

                        Console.WriteLine("Enter your Account Number, it must be 8 digits: ");
                        int accountNumber = int.Parse(Console.ReadLine());
                            if (accountNumber != 8)
                            {
                                Console.WriteLine("Number must be 8 digits.");
                            }
                            else
                            {
                                Console.WriteLine("Accepted");
                            }
                            account.AccountNumber = accountNumber;

                        Console.WriteLine("Enter your Password, no longer than 14 characters: ");
                        account.Password = Console.ReadLine();
                            if (account.Password.Length > 14)
                            {
                                Console.WriteLine("Sorry, your password is too long!");
                            }
                            else
                            {
                                Console.WriteLine("Accepted");
                            }

                        Console.WriteLine("At Equibank we are generous, you have been alotted a balance of 50 to use for free, use it wisely.");
                        Console.WriteLine($"Account Balance: {account.Balance}");
                        db.SaveChanges();
                        Console.WriteLine(FiggleFonts.Standard.Render("Equibank Account Registration Complete"));
                    }
                    string s = "Success";
                    return s;
                }
            }
        }
    }
}