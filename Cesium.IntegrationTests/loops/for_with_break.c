/*
 * SPDX-FileCopyrightText: 2025 Cesium contributors <https://github.com/ForNeVeR/Cesium>
 *
 * SPDX-License-Identifier: MIT
 */

int main(int argc, char *argv[])
{
    int i;
    int j = 0;
    for (i = 0; i < 10000; ++i)
    {
        if (j == 42)
            break;
        ++j;
    }
    return j;
}