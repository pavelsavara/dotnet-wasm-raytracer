using System;
using System.Runtime.Intrinsics;

namespace RayTracer
{
    public static class Extensions
    {
        static public float Magnitude(this Vector128<float> v)
        {
            return (float)Math.Sqrt(Vector128.Dot(v, v));
        }

        static public Vector128<float> Normalize(this Vector128<float> v)
        {
            return v / Vector128.Create(v.Magnitude());
        }

        static public float X(this Vector128<float> v)
        {
            return v.GetElement(0);
        }
        static public float Y(this Vector128<float> v)
        {
            return v.GetElement(1);
        }
        static public float Z(this Vector128<float> v)
        {
            return v.GetElement(2);
        }
    }
}
