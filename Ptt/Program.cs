using System;
using PttLoginner;
namespace Ptt
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hi");
            PttLogin pl = new PttLogin();
            pl.user = "username";
            pl.pass = "password";

            pl.loginN();
        }
    }
}
