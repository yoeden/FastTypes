# Deep Copy

## Content

* **[Intro](#heading--intro)**

* **[Quick start](#heading--quickstart)**
  * [Example](#heading--quickstart-example)

* **[Limitations](#heading--limitations)**

## <div id="heading--intro"/> Intro

A deep copy is a clone of an object that is by reference different.
Meaning you can deep copy object A, modify it and it wont affect the original object.
Its a more complicated task than it sound, since object state must be cloned internally by its fields.
For example, the [`List`](https://github.com/microsoft/referencesource/blob/master/mscorlib/system/collections/generic/list.cs) class is only composed of four fields:
```
T[] _items;
int _version;
int _size;
Object _syncRoot;
```
but [`BlockingCollection`](https://github.com/microsoft/referencesource/blob/master/System/sys/system/collections/concurrent/BlockingCollection.cs) contains more fields and more complex ones (including two semaphores).

## <div id="heading--quickstart"/> Quick start

### <div id="heading--quickstart-example"/> Example

#### If the type is known (preferred)

```csharp
var objectToClone = ...;
var cloned = FastCopy.DeepCopy(objectToClone);

// Wont affect objectToClone
cloned.Name = "Clone Beth"ף
```

#### If the type is unknown (working with `object`)

```csharp
object objectToClone = ...;
(T)FastCopy.DeepCopy(objectToClone);
```

_*Always prefer using the generic version (where you know the type beforehand) since it is faster due to avoiding boxing_

## <div id="heading--limitations"/> Known Limitations

* Only the state of the object is cloned (not including static fields, etc...), fields to fields cloning
* Clone target must have a default constructor (doesn't matter if its private or public)
* Only instance fields will be cloned (readonly as well)
* Collections must be inherit either `IReadOnlyCollection<>` or `ICollection`
* Collections must have a default, capacity or `IEnumerable<>` constructors
* Custom collections may not work as intended.