// SPDX-FileCopyrightText: 2025 Cesium contributors <https://github.com/ForNeVeR/Cesium>
//
// SPDX-License-Identifier: MIT

using Cesium.CodeGen.Contexts;
using Cesium.CodeGen.Ir.Types;

namespace Cesium.CodeGen.Ir.Expressions;

internal interface IExpression
{
    IExpression Lower(IDeclarationScope scope);
    void EmitTo(IEmitScope scope);
    IType GetExpressionType(IDeclarationScope scope);
}
