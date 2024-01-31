# Copying primitives

Copying primitives is simple re-assign from to destination.

## Simple Primitives

```csharp
dst.Number = dst.Number;
```

### Performance

Performance are expected to be native-like.

## Value Types

Value type get treated as a primitive in by the CLR.
If a value 

_It works for every **pure** value type (value type that holds only primitives and value types)._

### Performance

Performance are expected to be native-like (plus method calling overhead, some if checks).

| Method                     |      Mean |    Ratio |   Gen0 | Allocated | Alloc Ratio |
| -------------------------- | --------: | -------: | -----: | --------: | ----------: |
| Manual                     | 0.4670 ns | baseline |      - |         - |          NA |
| FastTypesTest              | 1.5702 ns |    +237% |      - |         - |          NA |
| ReubenBond_DeepCopyTests   | 6.8376 ns |  +1,367% |      - |         - |          NA |
| ShereSoft_DeepCloningTests | 9.3534 ns |  +1,905% | 0.0048 |      80 B |          NA |