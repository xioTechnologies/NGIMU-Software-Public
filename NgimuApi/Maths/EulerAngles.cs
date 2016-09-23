using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace NgimuApi.Maths
{
    /// <summary>
    /// Euler angles (in degrees).  The Euler angles are in the Aerospace sequence also known as the ZYX sequence.
    /// </summary>
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct EulerAngles
    {
        /// <summary>
        /// Euler angle values of zero.  Represents an alignment in orientation.
        /// </summary>
        public static readonly EulerAngles Zero = new EulerAngles(0f, 0f, 0f);

        /// <summary>
        /// Roll angle (rotation around x axis) in degrees.
        /// </summary>
        public float Roll;

        /// <summary>
        /// Pitch angle (rotation around y axis) in degrees.
        /// </summary>
        public float Pitch;

        /// <summary>
        /// Yaw angle (rotation around z axis) in degrees.
        /// </summary>
        public float Yaw;

        /// <summary>
        /// Gets or sets the Euler angles by index.  The element order is: roll, pitch, yaw.
        /// </summary>
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return Roll;

                    case 1:
                        return Pitch;

                    case 2:
                        return Yaw;

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
                        Roll = value;
                        return;

                    case 1:
                        Pitch = value;
                        return;

                    case 2:
                        Yaw = value;
                        return;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Creates Euler angles from individual angles.
        /// </summary>
        public EulerAngles(float roll, float pitch, float yaw)
        {
            Roll = roll;
            Pitch = pitch;
            Yaw = yaw;
        }

        /// <summary>
        /// Creates Euler angles from an array.  The element order is: roll, pitch, yaw.
        /// </summary>
        public EulerAngles(float[] array)
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

            Roll = array[i++];
            Pitch = array[i++];
            Yaw = array[i++];
        }

        private const float RadiansToDegreesFactor = 180f / (float)Math.PI;
        private const float DegreesToRadiansFactor = (float)Math.PI / 180f;

        /// <summary>
        /// Converts from radians to degrees.
        /// </summary>
        public static float RadiansToDegrees(float radians)
        {
            return radians * RadiansToDegreesFactor;
        }

        /// <summary>
        /// Converts from degrees to radians.
        /// </summary>
        public static float DegreesToRadians(float degrees)
        {
            return degrees * DegreesToRadiansFactor;
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation.
        /// </summary>
        /// <returns> The string representation of the value of this instance.</returns>
        public override string ToString()
        {
            return String.Format("Roll: {0}, Pitch: {1}, Yaw: {2}",
                Roll.ToString(CultureInfo.InvariantCulture),
                Pitch.ToString(CultureInfo.InvariantCulture),
                Yaw.ToString(CultureInfo.InvariantCulture));
        }
    }
}
