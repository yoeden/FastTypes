namespace FastTypes.Tests.Reflection.Activator
{
    public class StubActivatorClass
    {
        public int A1 { get; }
        public int A2 { get; }
        public int A3 { get; }
        public int A4 { get; }
        public int A5 { get; }

        public StubActivatorClass()
        {

        }

        public StubActivatorClass(int a1, int a2, int a3, int a4, int a5)
        {
            A1 = a1;
            A2 = a2;
            A3 = a3;
            A4 = a4;
            A5 = a5;
        }

        public StubActivatorClass(int a1, int a2, int a3, int a4)
        {
            A1 = a1;
            A2 = a2;
            A3 = a3;
            A4 = a4;
        }

        public StubActivatorClass(int a1, int a2, int a3)
        {
            A1 = a1;
            A2 = a2;
            A3 = a3;
        }

        public StubActivatorClass(int a1, int a2)
        {
            A1 = a1;
            A2 = a2;
        }

        public StubActivatorClass(int a1)
        {
            A1 = a1;
        }

        protected bool Equals(StubActivatorClass other)
        {
            return A1 == other.A1 && A2 == other.A2 && A3 == other.A3 && A4 == other.A4 && A5 == other.A5;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((StubActivatorClass)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(A1, A2, A3, A4, A5);
        }
    }
}