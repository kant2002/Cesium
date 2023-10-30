using Cesium.CodeGen.Contexts;
using Cesium.CodeGen.Ir.Expressions;
using Mono.Cecil;

namespace Cesium.CodeGen.Ir.Types;

internal record ConstType(IType Base) : IType
{
    public TypeReference Resolve(TranslationUnitContext context) => Base.Resolve(context);

    public int? GetSizeInBytes(TargetArchitectureSet arch) =>
        Base.GetSizeInBytes(arch);

    public IExpression GetSizeInBytesExpression(TargetArchitectureSet arch) => Base.GetSizeInBytesExpression(arch);
}
