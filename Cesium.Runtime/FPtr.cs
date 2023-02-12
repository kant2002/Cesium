namespace Cesium.Runtime;

/// <summary>A class encapsulating a C function pointer.</summary>
public readonly unsafe struct FPtr<TDelegate> where TDelegate : Delegate // TODO: Think about vararg and empty parameter list encoding.
{
    private readonly long _value;

    public FPtr(void* ptr)
    {
        _value = (long)ptr;
    }

    public void* AsPtr() => (void*)_value;
}
