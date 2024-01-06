// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;

//| Method         | Job           | Runtime       | obj     | Mean       | Error    | StdDev   | Gen0   | Allocated |
//|--------------- |-------------- |-------------- |-------- |-----------:|---------:|---------:|-------:|----------:|
//| WithReflection | .NET 5.0      | .NET 5.0      | Object2 |   928.1 ns | 16.36 ns | 13.67 ns | 0.0401 |     680 B |
//| WithFastType   | .NET 5.0      | .NET 5.0      | Object2 |   428.4 ns |  7.47 ns |  7.34 ns | 0.0243 |     408 B |
//| WithReflection | .NET 8.0      | .NET 8.0      | Object2 |   334.4 ns |  3.48 ns |  3.08 ns | 0.0291 |     488 B |
//| WithFastType   | .NET 8.0      | .NET 8.0      | Object2 |   309.7 ns |  4.92 ns |  4.36 ns | 0.0243 |     408 B |
//| WithReflection | .NET Core 3.1 | .NET Core 3.1 | Object2 | 1,052.1 ns | 20.89 ns | 23.22 ns | 0.0401 |     680 B |
//| WithFastType   | .NET Core 3.1 | .NET Core 3.1 | Object2 |   493.9 ns |  4.71 ns |  3.93 ns | 0.0238 |     408 B |
BenchmarkRunner.Run<Benchmarks>();

void Runner(Func<object,object> run)
{
    var last = DateTime.Now;
    var itemsCount = new List<int>();
    var counter = 0;

    while (itemsCount.Count != 5)
    {
        //var items = CreateObjects();
        //foreach (object item in items)
        //{
        //    run(item);
        //}

        counter++;

        if (DateTime.Now - last > TimeSpan.FromSeconds(1))
        {
            itemsCount.Add(counter);
            counter = 0;
            last = DateTime.Now;
        }
    }

    Console.WriteLine($"{itemsCount.Average():N} items per second.");
}


public class Object2
{
    public string Id { get; set; }
    public string Description { get; set; }
    public NestedObject Nested { get; set; }
}

public class NestedObject
{
    public string Text { get; set; }
}