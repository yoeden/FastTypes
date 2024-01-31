# Cloning

## Definitions

#### Shallow Copy

A new object B is created, and the fields values of A are copied over to B.
If the field value is a reference to an object (e.g., a memory address) it copies the reference, hence referring to the same object as A does, and if the field value is a primitive type, it copies the value of the primitive type.

#### Deep Copy

An alternative is a deep copy, meaning that fields are dereferenced: rather than references to objects being copied, new copy of objects are created for any referenced objects, and references to these are placed in B. Later modifications to the contents of either remain unique to A or B, as the contents are not shared.

## Goals

- The goal is to be close as much as possible to a manual clone (near native times).
- Allocation should be minimal and as close as possible to manual clone.
- Runtime (IL) should not be compromised, if comprises are needed they can and should be made on the clone lambda compilation.
- Prioritize using properties (properties are methods and can contain logic to run).

## Object Internals

- [Primitives](primitives/ref.md)
- [Pure ValueTypes](primitives/ref.md)
- [Complex Objects](primitives/ref.md)
- [Array](array/ref.md)
- [Tuples](primitives/ref.md)
- [Enumerable](enumerable/ref.md)

## Limitations 

- Target object must have a default constructor (unless cloning unit specify other wise, for example list clone will seek the `List(int capacity)` ctor).
- Runtime will always have a difference of 20% minimum (thanks to calling to the compiled clone lambda).
- Object lifetime cant be re-created, its current state is cloned (For example a call inside a method for an external api and mutating the object value).
- Custom collections ?