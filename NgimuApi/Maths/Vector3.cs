using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace NgimuApi.Maths
{
    /// <summary>
    /// Three-dimensional vector.
    /// </summary>
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct Vector3
    {
        /// <summary>
        /// Vector value of zero.
        /// </summary>
        public static readonly Vector3 Zero = new Vector3();

        /// <summary>
        /// Vector value of one.  Each element value is one;
        /// </summary>
        public static readonly Vector3 One = new Vector3(1, 1, 1);

        /// <summary>
        /// Unit vector in x direction.
        /// </summary>
        public static readonly Vector3 UnitX = new Vector3(1, 0, 0);

        /// <summary>
        /// Unit vector in y direction.
        /// </summary>
        public static readonly Vector3 UnitY = new Vector3(0, 1, 0);

        /// <summary>
        /// Unit vector in z direction.
        /// </summary>
        public static readonly Vector3 UnitZ = new Vector3(0, 0, 1);

        /// <summary>
        /// Vector x element.
        /// </summary>
        public float X;

        /// <summary>
        /// Vector y element.
        /// </summary>
        public float Y;

        /// <summary>
        /// Vector z element.
        /// </summary>
        public float Z;

        /// <summary>
        /// Creates a vector from individual elements.
        /// </summary>
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Creates vector from an array.  The element order is: x, y, z.
        /// </summary>
        public Vector3(float[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (array.Length != 3)
            {
                throw new ArgumentException(Strings.EulerAngles_ArrayLengthNot3, "array");
            }

            int i = 0;

            X = array[i++];
            Y = array[i++];
            Z = array[i++];
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the value of this instance. </returns>
        public override string ToString()
        {
            return String.Format("X: {0}, Y: {1}, Z: {2}",
                X.ToString(CultureInfo.InvariantCulture),
                Y.ToString(CultureInfo.InvariantCulture),
                Z.ToString(CultureInfo.InvariantCulture));
        }
    }
}
