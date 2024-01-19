using FastTypes.Clone;
using FluentAssertions;

namespace FastTypes.Tests.Copy
{
    public partial class FastCopyTests
    {
        //Properties: 
        //  private value Type
        //  private readonly value type
        //  private writeonly value type
        //  private ref Type
        //  private readonly ref type
        //  private writeonly ref type

        //  public value Type
        //  public readonly value type
        //  public writeonly value type
        //  public ref Type
        //  public readonly ref type
        //  public writeonly ref type

        //Fields:
        //  value type field
        //  readonly value type field
        //  ref type field
        //  readonly ref type field
        //  backing field

        protected T InvokeGenericDeepCopy<T>(T src) => FastCopy.DeepCopy(src);
        protected T InvokeObjectDeepCopy<T>(T src) => (T)FastCopy.DeepCopy((object)src);

        private class StubRefType
        {
            public int Number { get; set; }
        }
    }
}
