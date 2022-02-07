using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace Cesium.CodeGen.Ir.Types;

internal record PointerType(IType Base) : IType
{
    public TypeReference Resolve(TypeSystem typeSystem) => Base.Resolve(typeSystem).MakePointerType();
}