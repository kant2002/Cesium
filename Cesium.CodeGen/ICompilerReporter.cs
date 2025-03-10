// SPDX-FileCopyrightText: 2025 Cesium contributors <https://github.com/ForNeVeR/Cesium>
//
// SPDX-License-Identifier: MIT

namespace Cesium.CodeGen;

public interface ICompilerReporter
{
    void ReportError(string message);
    void ReportInformation(string message);
}
