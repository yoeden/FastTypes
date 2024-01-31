# Collections

Collection are the most challenging part of deep copying.
* Not all collection respect the [`ICollection<T>.Add(T)`](https://github.com/microsoft/referencesource/blob/51cf7850defa8a17d815b4700b67116e3fa283c2/mscorlib/system/collections/generic/icollection.cs#L41) method (for example [`ImmutableList`](https://github.com/mono/ImmutableCollections/blob/99b305732ceb944c036ce1a40fd28cbd192c564a/System.Collections.Immutable/System.Collections.Immutable/ImmutableList.cs#L360)).
* Copying the state (fields) of each collection is maybe impossible (since some contain semaphores, etc...)
* Some collections may contain locking mechanisms
* Some collections may contain additional data structures within them (most of them initialize by ctor)
* Some collections need `IEqualityComparer` (for example `HashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer)`).

<table>
  <thead>
    <tr>
      <th>Name</th>
      <th>Interfaces</th>
      <th>Constructors</th>
      <th>Useable Add Methods</th>
      <th>Unusable Add Methods</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>List&lt;T&gt;</td>
      <td>
        <li><code>IList&lt;T&gt;</code></li>
        <li><code>ICollections&lt;T&gt;</code></li>
        <li><code>System.Collections.IList</code></li>
        <li><code>IReadOnlyList&lt;T&gt;</code></li>
      </td>
      <td>
        <li><code>List()</code></li>
        <li><code>List(int capacity)</code></li>
        <li><code>List(IEnumerable&lt;T&gt; collection)</code></li>
      </td>
      <td>
        <li><code>void Add(T item)</code></li>
        <li><code>int IList.Add(Object item)</code></li>
      </td>
      <td>-</td>
    </tr>
     <tr>
      <td>Dictionary&lt;TKey,TValue&gt;</td>
      <td>
        <li><code>IDictionary&lt;TKey,TValue&gt;</code></li>
        <li><code>IReadOnlyDictionary&lt;TKey,TValue&gt;</code></li>
        <li><code>IDictionary</code></li>
        <li><code>ICollection&lt;KeyValuePair&lt;TKey,TValue&gt;&gt;</code></li>
        <li><code>IReadOnlyCollection&lt;KeyValuePair&lt;TKey,TValue&gt;&gt;</code></li>
      </td>
      <td>
        <li><code>Dictionary()</code></li>
        <li><code>Dictionary(int capacity)</code></li>
        <li><code>(IEqualityComparer&lt;TKey&gt; comparer)</code></li>
        <li><code>(int capacity, IEqualityComparer&lt;TKey&gt; comparer)</code></li>
        <li><code>(IDictionary&lt;TKey,TValue&gt; dictionary)</code></li>
        <li><code>(IDictionary&lt;TKey,TValue&gt; dictionary, IEqualityComparer&lt;TKey&gt; comparer)</code></li>
      </td>
      <td>
        <li><code>void Add(TKey key, TValue value)</code></li>
        <li><code>ICollection&lt;KeyValuePair&lt;TKey, TValue&gt;&gt;.Add(KeyValuePair&lt;TKey, TValue&gt; keyValuePair)</code></li>
      </td>
      <td>-</td>
    </tr>
      <tr>
      <td>HashSet&lt;T&gt;</td>
      <td>
        <li><code>ISet&lt;T&gt;</code></li>
        <li><code>ICollections&lt;T&gt;</code></li>
        <li><code>IReadOnlyCollection&lt;T&gt;</code></li>
      </td>
      <td>
        <li><code>HashSet()</code></li>
        <li><code>HashSet(int capacity)</code></li>
        <li><code>HashSet(IEnumerable&lt;T&gt; collection)</code></li>
        <li><code>HashSet(IEqualityComparer&lt;T&gt; comparer)</code></li>
        <li><code>HashSet(IEnumerable&lt;T&gt; collection, IEqualityComparer&lt;T&gt; comparer)</code></li>
      </td>
      <td>
        <li><code>ICollection&lt;<T>&gt;.Add(T item)</code></li>
        <li><code>bool Add(T item)</code></li>
      </td>
      <td>-</td>
    </tr>
  </tbody>
</table>

* [List<T>](https://github.com/microsoft/referencesource/blob/master/mscorlib/system/collections/generic/list.cs)
* [Dictionary<TKey,TValue>](https://github.com/microsoft/referencesource/blob/master/mscorlib/system/collections/generic/dictionary.cs)
* [ConcurrentBag](https://github.com/microsoft/referencesource/blob/master/System/sys/system/collections/concurrent/ConcurrentBag.cs)