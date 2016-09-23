using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace NgimuApi.Maths
{
    /// <summary>
    /// Rotation matrix in row-major order.
    /// <see href="http://en.wikipedia.org/wiki/Row-major_order"/>
    /// </summary>
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct RotationMatrix
    {
        /// <summary>
        /// Rotation matrix identity.  Represents an alignment in orientation.
        /// </summary>
        public static readonly RotationMatrix Identity =
            new RotationMatrix(
                1, 0, 0,
                0, 1, 0,
                0, 0, 1
            );

        /// <summary>
        /// Rotation matrix xx element.
        /// </summary>
        public float XX;

        /// <summary>
        /// Rotation matrix xy element.
        /// </summary>
        public float XY;

        /// <summary>
        /// Rotation matrix xz element.
        /// </summary>
        public float XZ;

        /// <summary>
        /// Rotation matrix yx element.
        /// </summary>
        public float YX;

        /// <summary>
        /// Rotation matrix yy element.
        /// </summary>
        public float YY;

        /// <summary>
        /// Rotation matrix yz element.
        /// </summary>
        public float YZ;

        /// <summary>
        /// Rotation matrix zx element.
        /// </summary>
        public float ZX;

        /// <summary>
        /// Rotation matrix zy element.
        /// </summary>
        public float ZY;

        /// <summary>
        /// Rotation matrix zz element.
        /// </summary>
        public float ZZ;

        /// <summary>
        /// Gets or sets the rotation matrix element by index.  The element order is: xx, xy, xz, yx, yy, yz, zx, zy, zz.
        /// </summary>
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return XX;

                    case 1:
                        return XY;

                    case 2:
                        return XZ;

                    case 3:
                        return YX;

                    case 4:
                        return YY;

                    case 5:
                        return YZ;

                    case 6:
                        return ZX;

                    case 7:
                        return ZY;

                    case 8:
                        return ZZ;

                    default:
                        break;
                }

                return 0;
            }

            set
            {
                switch (index)
                {
                    case 0:
                        XX = value;
                        return;

                    case 1:
                        XY = value;
                        return;

                    case 2:
                        XZ = value;
                        return;

                    case 3:
                        YX = value;
                        return;

                    case 4:
                        YY = value;
                        return;

                    case 5:
                        YZ = value;
                        return;

                    case 6:
                        ZX = value;
                        return;

                    case 7:
                        ZY = value;
                        return;

                    case 8:
                        ZZ = value;
                        return;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Creates a rotation matrix from individual elements.
        /// </summary>
        public RotationMatrix(float xx, float xy, float xz,
                              float yx, float yy, float yz,
                              float zx, float zy, float zz)
        {
            XX = xx;
            XY = xy;
            XZ = xz;

            YX = yx;
            YY = yy;
            YZ = yz;

            ZX = zx;
            ZY = zy;
            ZZ = zz;
        }

        /// <summary>
        /// Creates a rotation matrix from an array.  The element order is: xx, xy, xz, yx, yy, yz, zx, zy, zz.
        /// </summary>
        public RotationMatrix(float[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (array.Length != 9)
            {
                throw new ArgumentException(Strings.RotationMatrix_ArrayLengthNot9, "array");
            }

            int i = 0;

            XX = array[i++];
            XY = array[i++];
            XZ = array[i++];

            YX = array[i++];
            YY = array[i++];
            YZ = array[i++];

            ZX = array[i++];
            ZY = array[i++];
            ZZ = array[i++];
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the value of this instance.</returns>
        public override string ToString()
        {
            return String.Format("XX: {0}, XY: {1}, XZ: {2}" +
                                 "YX: {3}, YY: {4}, YZ: {5}" +
                                 "ZX: {6}, ZY: {7}, ZZ: {8}",
                XX.ToString(CultureInfo.InvariantCulture),
                XY.ToString(CultureInfo.InvariantCulture),
                XZ.ToString(CultureInfo.InvariantCulture),
                YX.ToString(CultureInfo.InvariantCulture),
                YY.ToString(CultureInfo.InvariantCulture),
                YZ.ToString(CultureInfo.InvariantCulture),
                ZX.ToString(CultureInfo.InvariantCulture),
                ZY.ToString(CultureInfo.InvariantCulture),
                ZZ.ToString(CultureInfo.InvariantCulture));
        }
    }
}
