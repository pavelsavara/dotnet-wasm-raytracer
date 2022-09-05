using System;
using System.Runtime.Intrinsics;

namespace RayTracer
{
    /// <summary>
    /// Represents a ray primitive. Used as a basis for intersection calculations.
    /// </summary>
    public struct Ray
    {
        public readonly Vector128<float> Origin;
        public readonly Vector128<float> Direction;
        public readonly float Distance;
        public Ray(Vector128<float> start, Vector128<float> direction, float distance)
        {
            this.Origin = start;
            this.Direction = direction.Normalize();
            this.Distance = distance;
        }
        public Ray(Vector128<float> start, Vector128<float> direction) : this(start, direction, float.PositiveInfinity) { }

    }

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
