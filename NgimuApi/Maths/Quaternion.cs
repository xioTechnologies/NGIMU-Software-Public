using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace NgimuApi.Maths
{
    /// <summary>
    /// Quaternion.
    /// </summary>
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct Quaternion : IEquatable<Quaternion>
    {
        /// <summary>
        /// Quaternion identity.  Represents an alignment in orientation.
        /// </summary>
        public static readonly Quaternion Identity = new Quaternion(1f, 0f, 0f, 0f);

        /// <summary>
        /// Quaternion w element.
        /// </summary>
        public float W;

        /// <summary>
        /// Quaternion x element.
        /// </summary>
        public float X;

        /// <summary>
        /// Quaternion y element.
        /// </summary>
        public float Y;

        /// <summary>
        /// Quaternion z element.
        /// </summary>
        public float Z;

        /// <summary>
        /// Gets or sets the quaternion element by index.  The element order is: w, x, y, z.
        /// </summary>
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return W;

                    case 1:
                        return X;

                    case 2:
                        return Y;

                    case 3:
                        return Z;

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
                        W = value;
                        return;

                    case 1:
                        X = value;
                        return;

                    case 2:
                        Y = value;
                        return;

                    case 3:
                        Z = value;
                        return;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Creates a quaternion from individual elements.
        /// </summary>
        public Quaternion(float w, float x, float y, float z)
        {
            W = w;
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Creates a quaternion from an array.  The element order is: w, x, y, z.
        /// </summary>
        public Quaternion(float[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (array.Length != 4)
            {
                throw new ArgumentException(Strings.Quaternion_ArrayLengthNot4, "array");
            }

            int i = 0;

            W = array[i++];
            X = array[i++];
            Y = array[i++];
            Z = array[i++];
        }

        /// <summary>
        /// Returns the quaternion conjugate. 
        /// </summary>
        /// <param name="quaternion">The quaternion.</param>
        /// <returns>Quaternion conjugate.</returns>
        public static Quaternion Conjugate(Quaternion quaternion)
        {
            quaternion.X = -quaternion.X;
            quaternion.Y = -quaternion.Y;
            quaternion.Z = -quaternion.Z;

            return quaternion;
        }

        /// <summary>
        /// Normalises a quaternion.
        /// </summary>
        /// <param name="quaternion">Quaternion to be normalised.</param>
        /// <returns>Normalised quaternion.</returns>
        public static Quaternion Normalise(Quaternion quaternion)
        {
            float vectorNormReciprocal =
                1f / (float)Math.Sqrt(
                    (quaternion.W * quaternion.W) +
                    (quaternion.X * quaternion.X) +
                    (quaternion.Y * quaternion.Y) +
                    (quaternion.Z * quaternion.Z)
                );

            quaternion.W *= vectorNormReciprocal;
            quaternion.X *= vectorNormReciprocal;
            quaternion.Y *= vectorNormReciprocal;
            quaternion.Z *= vectorNormReciprocal;

            return quaternion;
        }

        #region Rotation matrix and Euler angle conversions

        /// <summary>
        /// Converts a quaternion to a rotation matrix.
        /// </summary>
        /// <param name="quaternion">Quaternion to be converted.</param>
        /// <returns>Rotation matrix representation of the quaternion. </returns>
        public static RotationMatrix ToRotationMatrix(Quaternion quaternion)
        {
            float qwqw = quaternion.W * quaternion.W; // calculate common terms to avoid repetition
            float qwqx = quaternion.W * quaternion.X;
            float qwqy = quaternion.W * quaternion.Y;
            float qwqz = quaternion.W * quaternion.Z;
            float qxqy = quaternion.X * quaternion.Y;
            float qxqz = quaternion.X * quaternion.Z;
            float qyqz = quaternion.Y * quaternion.Z;

            RotationMatrix matrix = new RotationMatrix();

            matrix.XX = 2.0f * (qwqw - 0.5f + quaternion.X * quaternion.X);
            matrix.XY = 2.0f * (qxqy + qwqz);
            matrix.XZ = 2.0f * (qxqz - qwqy);
            matrix.YX = 2.0f * (qxqy - qwqz);
            matrix.YY = 2.0f * (qwqw - 0.5f + quaternion.Y * quaternion.Y);
            matrix.YZ = 2.0f * (qyqz + qwqx);
            matrix.ZX = 2.0f * (qxqz + qwqy);
            matrix.ZY = 2.0f * (qyqz - qwqx);
            matrix.ZZ = 2.0f * (qwqw - 0.5f + quaternion.Z * quaternion.Z);

            return matrix;
        }

        /// <summary>
        /// Converts a rotation matrix to a quaternion.
        /// <see href="http://www.euclideanspace.com/maths/geometry/rotations/conversions/matrixToQuaternion/"/>
        /// </summary>
        /// <param name="rotationMatrix">Rotation matrix to be converted. </param>
        /// <returns>Quaternion representation of the rotation matrix.</returns>
        public static Quaternion FromRotationMatrix(RotationMatrix rotationMatrix)
        {
            float w, x, y, z;

            float tr = rotationMatrix.XX + rotationMatrix.YY + rotationMatrix.ZZ;

            if (tr > 0)
            {
                float S = (float)Math.Sqrt(tr + 1.0) * 2; // S=4*w 
                w = 0.25f * S;
                x = (rotationMatrix.ZY - rotationMatrix.YZ) / S;
                y = (rotationMatrix.XZ - rotationMatrix.ZX) / S;
                z = (rotationMatrix.YX - rotationMatrix.XY) / S;
            }
            else if ((rotationMatrix.XX > rotationMatrix.YY) & (rotationMatrix.XX > rotationMatrix.ZZ))
            {
                float S = (float)Math.Sqrt(1.0 + rotationMatrix.XX - rotationMatrix.YY - rotationMatrix.ZZ) * 2; // S=4*x 
                w = (rotationMatrix.ZY - rotationMatrix.YZ) / S;
                x = 0.25f * S;
                y = (rotationMatrix.XY + rotationMatrix.YX) / S;
                z = (rotationMatrix.XZ + rotationMatrix.ZX) / S;
            }
            else if (rotationMatrix.YY > rotationMatrix.ZZ)
            {
                float S = (float)Math.Sqrt(1.0 + rotationMatrix.YY - rotationMatrix.XX - rotationMatrix.ZZ) * 2; // S=4*y
                w = (rotationMatrix.XZ - rotationMatrix.ZX) / S;
                x = (rotationMatrix.XY + rotationMatrix.YX) / S;
                y = 0.25f * S;
                z = (rotationMatrix.YZ + rotationMatrix.ZY) / S;
            }
            else
            {
                float S = (float)Math.Sqrt(1.0 + rotationMatrix.ZZ - rotationMatrix.XX - rotationMatrix.YY) * 2; // S=4*z
                w = (rotationMatrix.YX - rotationMatrix.XY) / S;
                x = (rotationMatrix.XZ + rotationMatrix.ZX) / S;
                y = (rotationMatrix.YZ + rotationMatrix.ZY) / S;
                z = 0.25f * S;
            }

            return Conjugate(new Quaternion(w, x, y, z));
        }

        /// <summary>
        /// Converts a quaternion to Euler angles (in degrees).
        /// </summary>
        /// <param name="quaternion">Quaternion to be converted.</param>
        /// <returns>Euler angles (in degrees) representation of the quaternion.</returns>
        public static EulerAngles ToEulerAngles(Quaternion quaternion)
        {
            float qwqw = quaternion.W * quaternion.W; // calculate common terms to avoid repetition

            EulerAngles angles = EulerAngles.Zero;

            angles.Roll = EulerAngles.RadiansToDegrees((float)Math.Atan2(2.0f * (quaternion.Y * quaternion.Z - quaternion.W * quaternion.X), 2.0f * (qwqw - 0.5f + quaternion.Z * quaternion.Z)));
            angles.Pitch = EulerAngles.RadiansToDegrees(-(float)Math.Asin(2.0f * (quaternion.X * quaternion.Z + quaternion.W * quaternion.Y)));
            angles.Yaw = EulerAngles.RadiansToDegrees((float)Math.Atan2(2.0f * (quaternion.X * quaternion.Y - quaternion.W * quaternion.Z), 2.0f * (qwqw - 0.5f + quaternion.X * quaternion.X)));

            return angles;
        }

        /// <summary>
        /// Converts Euler angles (in degrees) to a quaternion.
        /// <see href="http://www.euclideanspace.com/maths/geometry/rotations/conversions/eulerToQuaternion/index.htm"/>
        /// </summary>
        /// <param name="eulerAngles">Euler angles (in degrees) to be converted.</param>
        /// <returns>Quaternion representation of the Euler angles (in degrees).</returns>
        public static Quaternion FromEulerAngles(EulerAngles eulerAngles)
        {
            float psi = EulerAngles.DegreesToRadians(eulerAngles.Yaw);
            float theta = EulerAngles.DegreesToRadians(eulerAngles.Pitch);
            float phi = EulerAngles.DegreesToRadians(eulerAngles.Roll);

            float cosPsi = (float)Math.Cos(psi * 0.5f);
            float sinPsi = (float)Math.Sin(psi * 0.5f);

            float cosTheta = (float)Math.Cos(theta * 0.5f);
            float sinTheta = (float)Math.Sin(theta * 0.5f);

            float cosPhi = (float)Math.Cos(phi * 0.5f);
            float sinPhi = (float)Math.Sin(phi * 0.5f);

            return new Quaternion(
                cosPsi * cosTheta * cosPhi + sinPsi * sinTheta * sinPhi,
                -(cosPsi * cosTheta * sinPhi - sinPsi * sinTheta * cosPhi),
                -(cosPsi * sinTheta * cosPhi + sinPsi * cosTheta * sinPhi),
                -(sinPsi * cosTheta * cosPhi - cosPsi * sinTheta * sinPhi));
        }

        #endregion

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the value of this instance. </returns>
        public override string ToString()
        {
            return string.Format("W: {0}, X: {1}, Y: {2}, Z: {3}",
                W.ToString(CultureInfo.InvariantCulture),
                X.ToString(CultureInfo.InvariantCulture),
                Y.ToString(CultureInfo.InvariantCulture),
                Z.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Quaternion left, Quaternion right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Quaternion left, Quaternion right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object other)
        {
            return ((other is Quaternion) && (this == ((Quaternion)other)));
        }

        /// <summary>
        /// Returns true if the numeric value of this instance is equal to the other quaternion.
        /// </summary>
        /// <param name="other">The other quaternion to be compared.</param>
        /// <returns>True if the numeric value of this instance is equal to the other quaternion.</returns>
        public bool Equals(Quaternion other)
        {
            return
                (this.X == other.X) &&
                (this.Y == other.Y) &&
                (this.Z == other.Z) &&
                (this.W == other.W);
        }
    }
}
