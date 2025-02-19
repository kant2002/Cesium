// SPDX-FileCopyrightText: 2025 Cesium contributors <https://github.com/ForNeVeR/Cesium>
//
// SPDX-License-Identifier: MIT

namespace Cesium.Core;

public sealed class AssertException : CesiumException
{
    public AssertException(string message) : base(message)
    {
    }
}
