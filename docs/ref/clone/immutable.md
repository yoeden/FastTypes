# Immutable

Immutable types are type that mutates its state, only by cloning it self.

For example, take a look at the [`ImmutableList.Add`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.immutable.immutablelist-1.add) method, its return type is `ImmutableList<T>`, as you guessed.

Immutable can be simply cloned by its internal state (fields).
But, in case of immutability like `ImmutableList` (can be easily copied by fields as well by just for the example), we should expect the return type to be the type it self.