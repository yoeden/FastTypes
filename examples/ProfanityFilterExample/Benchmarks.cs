using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using FastTypes;
using FastTypes.Reflection;

public static class Censor
{
    private static readonly string[] badWords = new string[]
    {
        "@sshole",
        "b!tch",
        "Fu^k",
        "id1ot"
    };

    public static string CensorText(string text)
    {
        foreach (string badWord in badWords)
        {
            text = text.Replace(badWord, new string('*', badWord.Length));
        }

        return text;
    }
}

[SimpleJob(RuntimeMoniker.Net50)]
[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.NetCoreApp31)]
[MemoryDiagnoser()]
public class Benchmarks
{
    public static IEnumerable<object> ValuesForB() => CreateObjects();


    [Benchmark]
    [ArgumentsSource(nameof(ValuesForB))]
    public void WithReflection(object obj)
    {
        var props = obj.GetType().GetProperties();
        foreach (PropertyInfo p in props)
        {
            var value = p.GetValue(obj);
            if (p.PropertyType.IsPrimitive) continue;
            if (p.PropertyType != typeof(string))
            {
                WithReflection(p.GetValue(obj));
                continue;
            }

            var clean = Censor.CensorText(value.ToString());
            p.SetValue(obj, clean);
        }
    }


    [Benchmark()]
    [ArgumentsSource(nameof(ValuesForB))]
    public void WithFastType(object obj)
    {
        var type = FastType.Of(obj.GetType());
        var props = type.Properties() as IReadOnlyList<FastProperty>;

        for (var i = 0; i < props.Count; i++)
        {
            var p = props[i];

            if (p.PropertyType.IsPrimitive) continue;
            if (p.PropertyType != typeof(string))
            {
                WithFastType(p.Get(obj));
                continue;
            }

            var clean = Censor.CensorText(p.Get<string>(obj));
            p.Set(obj, clean);
        }
    }

    public static IEnumerable<object> CreateObjects()
    {
        yield return new Object2()
        {
            Id = "ID1234",
            Description = "@sshole, you better shut your mouth b!tch",
            Nested = new NestedObject()
            {
                Text = "Fu^k you, id1ot !"
            }
        };
    }
}