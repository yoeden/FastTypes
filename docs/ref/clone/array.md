# Array

Iterating through the array and evaluating each element.

## Primitive Array

In the case of primitive array, Array copy is the performant way to copy them (and easiest).

### Performance

| Method         | Runtime       | Mean     | Ratio    | Gen0     | Gen1     | Gen2     | Allocated | Alloc Ratio |
|--------------- |-------------- |---------:|---------:|---------:|---------:|---------:|----------:|------------:|
| ArrayCopy      | .NET 5.0      | 129.2 us | baseline | 124.7559 | 124.7559 | 124.7559 | 390.65 KB |             |
| FastTypesClone | .NET 5.0      | 130.2 us |      +1% | 124.7559 | 124.7559 | 124.7559 | 390.65 KB |         +0% |
| ArrayCopy      | .NET 6.0      | 134.2 us | baseline | 124.7559 | 124.7559 | 124.7559 | 390.69 KB |             |
| FastTypesClone | .NET 6.0      | 136.3 us |      +1% | 124.7559 | 124.7559 | 124.7559 | 390.69 KB |         +0% |
| ArrayCopy      | .NET 8.0      | 136.1 us | baseline | 124.7559 | 124.7559 | 124.7559 | 390.69 KB |             |
| FastTypesClone | .NET 8.0      | 136.3 us |      +1% | 124.7559 | 124.7559 | 124.7559 | 390.69 KB |         +0% |
| ArrayCopy      | .NET Core 3.1 | 128.7 us | baseline | 124.7559 | 124.7559 | 124.7559 | 390.65 KB |             |
| FastTypesClone | .NET Core 3.1 | 133.5 us |      +4% | 124.7559 | 124.7559 | 124.7559 | 390.65 KB |         +0% |

## Ref Array

When each array cannot be copied from source to destination we need to iterate on it (just like we would manually) and deep clone each element.

### Performance

| Method            | Runtime       | Mean     | Ratio    | Gen0   | Gen1   | Allocated | Alloc Ratio |
|------------------ |-------------- |---------:|---------:|-------:|-------:|----------:|------------:|
| ManualRefTests    | .NET 5.0      | 5.584 us | baseline | 1.9073 | 0.2060 |  31.27 KB |             |
| FastTypesCloneRef | .NET 5.0      | 5.721 us |      -1% | 1.9073 | 0.2060 |  31.27 KB |         +0% |
| ManualRefTests    | .NET 6.0      | 5.264 us | baseline | 1.9073 | 0.2060 |  31.27 KB |             |
| FastTypesCloneRef | .NET 6.0      | 5.862 us |     +11% | 1.9073 | 0.2060 |  31.27 KB |         +0% |
| ManualRefTests    | .NET 8.0      | 4.534 us | baseline | 1.9150 | 0.2060 |  31.27 KB |             |
| FastTypesCloneRef | .NET 8.0      | 5.570 us |     +23% | 1.9150 | 0.2060 |  31.27 KB |         +0% |
| ManualRefTests    | .NET Core 3.1 | 5.166 us | baseline | 1.9073 | 0.2060 |  31.27 KB |             |
| FastTypesCloneRef | .NET Core 3.1 | 5.905 us |     +14% | 1.9073 | 0.2060 |  31.27 KB |         +0% |