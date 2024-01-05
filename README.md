
# <img src="docs/art/logo.svg" alt="SVG" style="width: 64px; height: 64px; margin-bottom: -16px;"> Fast Types

[![Coverage](https://img.shields.io/badge/build-100%25-brightgreen?label=Coverage)]()
[![Nuget](https://img.shields.io/nuget/v/FastTypes?style=flat-square)](https://www.nuget.org/packages/FastTypes/)
[![License](https://img.shields.io/github/license/yoeden/fasttypes?style=flat-square)](https://github.com/yoeden/fasttypes/blob/master/LICENSE.md)
[![Downloads](https://img.shields.io/nuget/dt/fasttypes?style=flat-square)](https://www.nuget.org/packages/FastTypes/)
[![GitHub](https://img.shields.io/badge/-source-181717.svg?logo=GitHub)](https://github.com/yoeden/fasttypes)

FastTypes is a fast fluent reflection library for .NET !

## Documentation 📚

Documentation available at the link [here](/docs/README.md)


## Quickstart 🏃

```csharp
// Create a fast type
var type = FastType.Of<Student>();

// Create a new student
var student = type.New("Micheal Joradn", 23);

// Get value from a property
var age = type.Property("Age").Get<int>();

// Set property value
type.Property("Name").Set(student, "Scottie Pippen");

// Call a method
type.Method("MarkAsPassed").Invoke(student, gpaScore);
```
## Benchmarks ⏱️

#### New Struct
| Method           |        Mean | Allocated |
| ---------------- | ----------: | --------: |
| New              |   0.0036 ns |         - |
| FastTypes        |   1.9761 ns |         - |
| ActivatorGeneric | 128.8025 ns |      24 B |
| ActivatorType    | 156.3947 ns |      24 B |
| CtorInfo         |  51.3921 ns |      24 B |

#### Get property value

| Method       |       Mean | Allocated |
| ------------ | ---------: | --------: |
| Direct       |  0.0050 ns |         - |
| FastTypes    |  3.9568 ns |         - |
| PropertyInfo | 55.5628 ns |      24 B |

## Roadmap 🚧

- Add query abilities (query types, properties and methods that return a certian type, inherit a certian, etc...)
- Add deep and shallow clone ability
- Optimize further 

## License 🖊️

[Apache-2.0](https://choosealicense.com/licenses/apache-2.0/)
