# Reflection

## Content

**[1. Intro](#heading--intro)**

**[2. Perfomance](#heading--perf)**
  * [2.1. Boxing](#heading--perf-boxing)
  * [2.2. Reflection](#heading--perf-bts)

**[3. Api](#heading--api)**
  * [3.1. Create a FastType](#heading--api-create)
  * [3.2. Instantiate](#heading--api-instance)
  * [3.3. Properties](#heading--api-props)
  * [3.4. Methods](#heading--api-methods)
  * [3.4. Static members](#heading--api-static)

<div id="heading--intro"/>

## Intro

.NET 7 introduced a [significant performance improvements in the reflection domain](https://devblogs.microsoft.com/dotnet/performance_improvements_in_net_7/#reflection).

From the link above :
| Method           | Runtime  | Mean      |
| ---------------- | -------- | --------- |
| MethodInfoInvoke | .NET 6.0 | 43.846 ns |
| MethodInfoInvoke | .NET 7.0 | 8.078 ns  |

But still, the best way to squeeze the best performance out of reflection still requires some manual work from the developer (`the best way to optimize invocation speed is to create a delegate from the MethodBase via CreateDelegate<T>`).
In addition, [boxing](#heading--perf-boxing) still remains a huge problem in the given reflection Api by .NET.
Not to mention that the user needs to fiddle with `Type` to find what the target member (`GetProperty` for example).

<div id="heading--perf"/>

## Performance

TBD

<div id="heading--perf-boxing"/>

### Boxing

Boxing is the process of taking a value type (living on the stack, close to our program) and wrapping it in an object (box), so it can live on the heap.
Boxing a simple 4 bytes integer as a object, costs us with extra 16 bytes (object header), and heap allocation (triggering the GC).

[`Boxing and unboxing are computationally expensive processes. When a value type is boxed, an entirely new object must be created. This can take up to 20 times longer than a simple reference assignment. When unboxing, the casting process can take four times as long as an assignment.`](https://learn.microsoft.com/en-us/dotnet/framework/performance/performance-tips#boxing-and-unboxing)

<div id="heading--perf-bts"/>

### Reflection

TBD

<div id="heading--api"/>

## Api

> [!IMPORTANT]   
> Always prefer using generic `FastType<T>` given by `Of<T>()` to avoid boxing whenever passing an instance.

<div id="heading--api-create"/>

### Create a FastType

`FastType` is the first stone in your journey, it contains the methods and properties that are accessible from the given type.

#### Type is known at compile time
```csharp
var type = FastType.Of<MyCustomObject>();
```
#### Type is known at runtime
```csharp
var type = FastType.Of(typeof(MyCustomObject));
```

The `Of` method returns [`IFastType`](https://github.com/yoeden/FastTypes/blob/master/src/FastTypes/FastType.cs) interface (one generic, and one object based).

> [!NOTE]  
> The `FastType` type is cached behind the scenes as a singleton, saving re-allocation and object scanning.
> Calling `Of` multiple times will return the same instance.

<div id="heading--api-instance"/>

### Instantiate

```csharp
//Parameterless constructor
var instance = fastType.New();

//If arguments must be provided
var instance = fastType.New(arg1, arg2, ...);
```

<div id="heading--api-props"/>

### Properties

#### Getter

```csharp
//Return type is known (avoid boxing)
var age = fastType.Property("Age").Get<int>(instance);

//Return type is unknown
var age = fastType.Property("Age").Get(instance);

//Static
var age = fastType.Property("Age").Get(null);
```

#### Setter

```csharp
//Set type is known (avoid boxing)
var age = fastType.Property("Age").Set<int>(instance, 23);

//Set type is unknown
var age = fastType.Property("Age").Set(instance, 23);

//Static
var age = fastType.Property("Age").Set(null, 23);
```

> [!TIP]  
> If the return type is known, always prefer using the generic version to avoid boxing.

<div id="heading--api-methods"/>

### Methods

#### Void

```csharp
//Invoking without arguments
fastType.Method("Calc").Invoke(instance);

//Invoking with arguments
fastType.Method("Calc").Invoke(instance, arg1, arg2, ...);

//Static
fastType.Method("Calc").Invoke(null, arg1, arg2, ...);
```

#### Return

```csharp
//Invoking without arguments, returns object
var result = fastType.Method("Calc").InvokeWithResult(instance);

//Invoking with arguments, returns T (avoid boxing)
var result = fastType.Method("Calc").InvokeWithResult<T>(instance, arg1, arg2, ...);

//Static
var result = fastType.Method("Calc").InvokeWithResult<T>(null, arg1, arg2, ...);
```

<div id="heading--api-static"/>

### Static members

Instead of passing an instance to member methods, pass null (see examples).