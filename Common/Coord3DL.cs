namespace AoC25.Common
{
    public class Coord3DL : IEquatable<Coord3DL>
    {
        public enum Arrangement
        { 
            UpDownLeftRight = 0,
            UpRightDownLeft = 1
        }

        public long x = 0; 
        public long y = 0;
        public long z = 0;


        public Coord3DL(long x, long y, long z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Coord3DL operator +(Coord3DL coord_a, Coord3DL coord_b)
            => new Coord3DL(coord_a.x + coord_b.x, coord_a.y + coord_b.y, coord_a.z + coord_b.z);

        public static Coord3DL operator -(Coord3DL coord_a, Coord3DL coord_b)
            => new Coord3DL(coord_a.x - coord_b.x, coord_a.y - coord_b.y, coord_a.z - coord_b.z);

        public static Coord3DL operator *(Coord3DL coord, int scalar)
            => new Coord3DL(coord.x * scalar, coord.y * scalar, coord.z * scalar);

        public static Coord3DL operator *(int scalar, Coord3DL coord)
            => new Coord3DL(coord.x * scalar, coord.y * scalar, coord.z * scalar);
        public static Coord3DL operator /(Coord3DL coord, int scalar)
            => new Coord3DL(coord.x / scalar, coord.y / scalar, coord.z/scalar);

        public static bool operator ==(Coord3DL coord_a, Coord3DL coord_b)
           => coord_a.Equals(coord_b);

        public static bool operator !=(Coord3DL coord_a, Coord3DL coord_b)
           => !coord_a.Equals(coord_b);

        public void Deconstruct(out long x, out long y, out long z)
        {
            x = this.x;
            y = this.y;
            z = this.z;
        }

        public IEnumerable<Coord2DL> GetNeighborsXY()
        {
            // Up - Right - Down - Left
            yield return new Coord2DL(x, y - 1);
            yield return new Coord2DL(x + 1, y);
            yield return new Coord2DL(x, y + 1);
            yield return new Coord2DL(x - 1, y);
        }

        public IEnumerable<Coord3DL> GetNeighbors()
        {
            yield return new Coord3DL(x, y, z - 1);
            yield return new Coord3DL(x, y, z + 1);
            yield return new Coord3DL(x, y - 1, z);
            yield return new Coord3DL(x, y + 1, z);
            yield return new Coord3DL(x - 1, y, z);
            yield return new Coord3DL(x + 1, y, z);
        }

        public IEnumerable<Coord3DL> GetNeighbors8()
        {
            for (long i = x - 1; i <= x + 1; i++)
                for (long j = y - 1; j <= y + 1; j++)
                    for (long k = z - 1; k <= z + 1; k++)
                        {
                            if (i == x && j == y && k == z)
                                continue;
                            yield return new Coord3DL(i, j, k);
                        }
        }


        public bool Equals(Coord3DL? other)
            => other is null ? false : other.x == x && other.y == y && other.z == z;

        public override bool Equals(object? other) 
            => other is Coord3DL c && c.x.Equals(x)  && c.y.Equals(y) && c.z.Equals(z);

        public static implicit operator (long, long, long)(Coord3DL c)       // Cast bw Coord and tuple
            => (c.x, c.y, c.z);

        public static implicit operator Coord3DL((int X, int Y, int Z) c) 
            => new Coord3DL(c.X, c.Y, c.Z);

        public long Manhattan(Coord3DL other)
            => Math.Abs(x - other.x) + Math.Abs(y - other.y) + Math.Abs(z - other.z);

        public double DistanceTo(Coord3DL other)
           => Math.Sqrt( ((x - other.x)* (x - other.x)) + ((y - other.y)* (y - other.y)) + ((z - other.z)* (z - other.z)));

        public double VectorModule
            => Math.Sqrt(x * x + y * y + z * z);

        public override int GetHashCode()
        {
            unchecked // Wraps around max value
            {
                long hash = 17;
                hash = hash * 23 + x;
                hash = hash * 23 + y;
                hash = hash * 23 + z;
                return (int) hash;
            }
        }
        public override string ToString()
        {
            return x.ToString() + "," + y.ToString() + "," + z.ToString();
        }
    }
}
