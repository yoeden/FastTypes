using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Running;
using FastTypes.Clone;

public class Program
{
    public static void Main()
    {
        var switcher = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run();
        Console.WriteLine(switcher.First().Title);
    }
}