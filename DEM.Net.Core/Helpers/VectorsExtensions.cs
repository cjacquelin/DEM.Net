﻿// VectorsExtensions.cs
//
// Author:
//       Xavier Fischer 
//
// Copyright (c) 2019 
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using DEM.Net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DEM.Net.Core
{
    public static class VectorsExtensions
    {
        public static IEnumerable<Vector3> ToVector3(this IEnumerable<GeoPoint> geoPoint)
        {
            return geoPoint.Select(p => p.ToVector3());
        }
        public static Vector3 ToVector3(this GeoPoint geoPoint)
        {
            return new Vector3((float)geoPoint.Longitude, (float)geoPoint.Elevation, -(float)geoPoint.Latitude);
        }
        public static Vector4 ToVector4(this Vector3 vector3, float w = 0f)
        {
            return new Vector4(vector3, w);
        }
        public static IEnumerable<Vector3> ToQuadPoints(this Vector3 vertex, float pointSize)
        {
            float halfSize = pointSize / 2f;
            //List<Vector3> outList = new List<Vector3>(4);

            //outList.Add(new Vector3(vertex.X - halfSize, vertex.Y, vertex.Z - halfSize));
            //outList.Add(new Vector3(vertex.X + halfSize, vertex.Y, vertex.Z - halfSize));
            //outList.Add(new Vector3(vertex.X + halfSize, vertex.Y, vertex.Z + halfSize));
            //outList.Add(new Vector3(vertex.X - halfSize, vertex.Y, vertex.Z + halfSize));
            //return outList;
            yield return new Vector3(vertex.X - halfSize, vertex.Y, vertex.Z - halfSize);
            yield return new Vector3(vertex.X + halfSize, vertex.Y, vertex.Z - halfSize);
            yield return new Vector3(vertex.X + halfSize, vertex.Y, vertex.Z + halfSize);
            yield return new Vector3(vertex.X - halfSize, vertex.Y, vertex.Z + halfSize);
        }

        public static IEnumerable<int> TriangulateQuadIndices(int startindex)
        {
            yield return startindex + 0;
            yield return startindex + 2;
            yield return startindex + 1;
            yield return startindex + 0;
            yield return startindex + 3;
            yield return startindex + 2;
        }

        // Not optimized but useful
        public static void GetStats(this IEnumerable<Vector3> vectors, out Vector3 average, out Vector3 min, out Vector3 max)
        {
            average = new Vector3(
                    vectors.Average(v => v.X),
                    vectors.Average(v => v.Y),
                    vectors.Average(v => v.Z));
            min = new Vector3(
                    vectors.Min(v => v.X),
                    vectors.Min(v => v.Y),
                    vectors.Min(v => v.Z));
            max = new Vector3(
                    vectors.Max(v => v.X),
                    vectors.Max(v => v.Y),
                    vectors.Max(v => v.Z));
        }
    }
}
