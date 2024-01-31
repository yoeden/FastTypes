# Complex Types

Complex type are types that will require some action to be copied (unlike [primitives and pure values](primitives.md)).

The copy will be made by fields to fields (in case of properties, backing fields).

## Class

Every reference type (classes) are complex types, even if they have a simple field of int.
We need to go through each field, apply deep copy evaluation (for example if its an array, we need to copy the array, if its a primitive, no action is needed, ...).

#### Performance (generic callback) compared to other libraries

| Method                     | Mean         | Ratio    | Gen0   | Gen1   | Allocated | Alloc Ratio |
|--------------------------- |-------------:|---------:|-------:|-------:|----------:|------------:|
| ManualTest                 |     60.34 ns | baseline | 0.0291 |      - |     488 B |             |
| FastTypesTests             |     81.85 ns |     +36% | 0.0334 |      - |     560 B |        +15% |
| ReubenBond_DeepCopyTests   |    350.91 ns |    +481% | 0.0362 |      - |     608 B |        +25% |
| ShereSoft_DeepCloningTests |    416.00 ns |    +591% | 0.0973 | 0.0005 |    1632 B |       +234% |
| Force_DeepClonerTests      |    602.54 ns |    +901% | 0.0963 |      - |    1624 B |       +233% |
| ObjectClonerTests          |  1,121.69 ns |  +1,748% | 0.2289 |      - |    3848 B |       +689% |
| JsonSerializerTests        |  6,209.85 ns | +10,175% | 0.2975 |      - |    5104 B |       +946% |
| AnyCloneTests              | 15,508.65 ns | +25,658% | 2.5330 | 0.0153 |   42392 B |     +8,587% |

#### Backward compatibility 

 Method         | Runtime       | Mean     | Ratio    | Gen0   | Allocated | Alloc Ratio |
|--------------- |-------------- |---------:|---------:|-------:|----------:|------------:|
| ManualTest     | .NET 5.0      | 77.87 ns | baseline | 0.0291 |     488 B |             |
| FastTypesTests | .NET 5.0      | 88.54 ns |     +14% | 0.0334 |     560 B |        +15% |
|                |               |          |          |        |           |             |
| ManualTest     | .NET 6.0      | 74.01 ns | baseline | 0.0291 |     488 B |             |
| FastTypesTests | .NET 6.0      | 80.60 ns |      +9% | 0.0334 |     560 B |        +15% |
|                |               |          |          |        |           |             |
| ManualTest     | .NET 8.0      | 59.92 ns | baseline | 0.0291 |     488 B |             |
| FastTypesTests | .NET 8.0      | 80.16 ns |     +34% | 0.0334 |     560 B |        +15% |
|                |               |          |          |        |           |             |
| ManualTest     | .NET Core 3.1 | 76.70 ns | baseline | 0.0291 |     488 B |             |
| FastTypesTests | .NET Core 3.1 | 94.81 ns |     +24% | 0.0334 |     560 B |        +15% |

#### Performance (object callback) compared to other libraries

NEED IMPROVEMENT

| Method                     | Mean         | Ratio    | Gen0   | Gen1   | Allocated | Alloc Ratio |
|--------------------------- |-------------:|---------:|-------:|-------:|----------:|------------:|
| ManualTest                 |     60.00 ns | baseline | 0.0291 |      - |     488 B |             |
| FastTypesTests             |    275.18 ns |    +364% | 0.0391 |      - |     656 B |        +34% |
| ReubenBond_DeepCopyTests   |    321.50 ns |    +436% | 0.0362 |      - |     608 B |        +25% |
| ShereSoft_DeepCloningTests |    453.73 ns |    +657% | 0.0973 | 0.0005 |    1632 B |       +234% |
| Force_DeepClonerTests      |    620.67 ns |    +934% | 0.0963 |      - |    1624 B |       +233% |
| ObjectClonerTests          |  1,120.89 ns |  +1,791% | 0.2289 |      - |    3848 B |       +689% |
| JsonSerializerTests        |  6,163.89 ns | +10,085% | 0.2975 |      - |    5104 B |       +946% |
| AnyCloneTests              | 15,710.09 ns | +26,019% | 2.5330 | 0.0305 |   42392 B |     +8,587% |

## Value Type

Even though value type is a 'stack type', when its composing a reference (to a class ofcourse), the reference gets copy by the address, meaning a change in the source will affect the clone as well.
Thus we cant simply copy a 'complex' value type from source to destination.

### Performance value type Composing Reference Type

| Method                     | Mean      | Ratio    | Gen0   | Allocated | Alloc Ratio |
|--------------------------- |----------:|---------:|-------:|----------:|------------:|
| Manual                     |  8.549 ns | baseline | 0.0043 |      72 B |             |
| FastTypesTest              | 10.058 ns |     +18% | 0.0043 |      72 B |         +0% |
| ReubenBond_DeepCopyTests   | 28.740 ns |    +237% | 0.0043 |      72 B |         +0% |
| ShereSoft_DeepCloningTests | 40.964 ns |    +380% | 0.0172 |     288 B |       +300% |