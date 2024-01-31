# Cloning Enumerables

Enumerables must be mapped to their actual types.
```csharp
public IReadOnlyList<int> Numbers { get; set; } = ImmutableList.Create(1,2,3);
```

The mapping to `Numbers` field cannot be of type `System.Collections.Generic.List` since its not the same underlying type, it must be mapped to `System.Collections.Immutable.ImmutableList`.

## Initialization

### List

List have a [`Capacity`](https://github.com/microsoft/referencesource/blob/51cf7850defa8a17d815b4700b67116e3fa283c2/mscorlib/system/collections/generic/list.cs#L110) property, which initializes the underlying array if its setted.
In the cloning process, working with the underlying array after setting `Capacity` could cause a redudent memory allocation.

If the underlying type if the enumerable is `List`, prefer using the [`List(IEnumerable<T> collection)`](https://github.com/microsoft/referencesource/blob/master/mscorlib/system/collections/generic/list.cs#L74) constructor.