# AoC25.Common API Reference

This document lists the public classes and members in the `AoC25.Common` namespace.

## `Coord2D`
- Type: `class`
- Implements: `IEquatable<Coord2D>`
- Fields: `int x`, `int y`
- Nested: `enum Arrangement { UpDownLeftRight, UpRightDownLeft }`
- Constructor: `Coord2D(int x, int y)`
- Operators: `+`, `-`, `* (Coord2D, int)`, `* (int, Coord2D)`, `/ (Coord2D, int)`, `==`, `!=`
  - These operators return new `Coord2D` instances and perform component-wise arithmetic (e.g., `+` adds `x` and `y` separately). Equality operators compare coordinates for value equality.
- Deconstruct: `void Deconstruct(out int x, out int y)`
  - Assigns the `x` and `y` fields to the provided out parameters for tuple-style deconstruction.
- Equality: `bool Equals(Coord2D? other)`, `override bool Equals(object? other)`, `override int GetHashCode()`
  - `Equals` compares `x` and `y` for equality. `GetHashCode` combines the fields with a small hashing algorithm to produce a stable hash.
- Conversions: `implicit operator (int,int)(Coord2D)`, `implicit operator Coord2D((int X, int Y))`
  - The implicit conversions allow seamless casting between a coordinate and an `(int,int)` tuple by mapping `x` to the first item and `y` to the second.
- Geometry:
  - `int Manhattan(Coord2D other)` — Manhattan distance
    - Computes |x1 - x2| + |y1 - y2| by taking absolute differences of each component and summing them.
  - `double DistanceTo(Coord2D other)` — Euclidean distance
    - Uses the vector difference and returns the Euclidean length (sqrt(dx*dx + dy*dy)).
  - `double VectorModule` — vector magnitude
    - Returns the Euclidean magnitude of the vector from origin (sqrt(x*x + y*y)).
  - `IEnumerable<Coord2D> GetNeighbors(int dist = 1, Arrangement arrange = Arrangement.UpRightDownLeft)` — 4-neighbors
    - Yields the four orthogonal neighbors at distance `dist`. The `arrange` parameter controls the order in which neighbors are yielded.
  - `IEnumerable<Coord2D> GetNeighbors8(int dist = 1)` — 8-neighbors
    - Yields the eight surrounding neighbors (including diagonals) around the point at the given distance.
  - `bool IsInside(int x0, int y0, int x1, int y1)` and `bool IsInside(Coord2D p0, Coord2D p1)`
    - Tests whether the coordinate is within the inclusive rectangle defined by the two corners (handles coordinates in any order by using min/max).
  - `double GetAngle()` — returns angle in degrees
    - Computes an angle in degrees with a custom orientation: converts from atan2, adjusts range to [0,360) and applies a rotation so that `Up` corresponds to 0/360 degrees.
  - `override string ToString()`
    - Returns a comma-separated `"x,y"` representation.

## `Coord3D`
- Type: `class`
- Implements: `IEquatable<Coord3D>`
- Fields: `int x`, `int y`, `int z`
- Nested: `enum Arrangement` (same variants as `Coord2D`)
- Constructor: `Coord3D(int x, int y, int z)`
- Operators: `+`, `-`, `* (Coord3D, int)`, `* (int, Coord3D)`, `/ (Coord3D, int)`, `==`, `!=`
  - Each operator returns a new `Coord3D` computed component-wise (x, y, z). Equality compares all three components.
- Deconstruct: `void Deconstruct(out int x, out int y, out int z)`
  - Assigns `x`, `y`, `z` to the provided out parameters for tuple-style deconstruction.
- Neighbors:
  - `IEnumerable<Coord2D> GetNeighborsXY()` — 4-neighbors on XY plane
  - `IEnumerable<Coord3D> GetNeighbors()` — 6-neighbors (adjacent in 3D)
  - `IEnumerable<Coord3D> GetNeighbors8()` — all neighbors in 3x3x3 excluding self
    - Iterates over the 3x3x3 cube centered on the coordinate and yields every neighbor except the central point itself.
- Equality & hashing: `Equals`, `override Equals(object?)`, `override int GetHashCode()`
- Conversions: `implicit operator (int,int,int)(Coord3D)`, `implicit operator Coord3D((int X, int Y, int Z))`
  - Allows implicit conversion to/from a 3-tuple by mapping components in order (x,y,z).
- Geometry: `int Manhattan(Coord3D other)`, `double DistanceTo(Coord3D other)`, `double VectorModule`, `override string ToString()`
  - `Manhattan` sums absolute differences across x, y and z. `DistanceTo` computes Euclidean distance using sqrt of squared differences. `VectorModule` is the Euclidean norm.

## `Coord4D`
- Type: `class`
- Implements: `IEquatable<Coord4D>`
- Fields: `int x, y, z, t`
- Constructor: `Coord4D(int x, int y, int z, int t)`
- Operators: `+`, `-`, `* (Coord4D, int)`, `* (int, Coord4D)`, `/ (Coord4D, int)`, `==`, `!=`
  - Operators perform component-wise arithmetic across x, y, z and t and return a new `Coord4D` instance. Equality compares all four components.
- Deconstruct: `void Deconstruct(out int x, out int y, out int z, out int t)`
  - Assigns the four coordinate fields to the out parameters for tuple deconstruction.
- Neighbors:
  - `IEnumerable<Coord2D> GetNeighborsXY()`
  - `IEnumerable<Coord4D> GetNeighbors8()` — all neighbors in 4D hypercube excluding self
    - Iterates the 4D hypercube in the range [coord -1, coord +1] on each axis and yields all combinations except the original coordinate.
- Equality, conversions, geometry: `Equals`, `override Equals(object?)`, `implicit operator (int,int,int,int)(Coord4D)`, `implicit operator Coord4D((int X, int Y, int Z, int T))`, `int Manhattan(Coord4D other)`, `double VectorModule`, `override int GetHashCode()`
  - Conversions map between the 4-tuple and the coordinate fields. `Manhattan` sums absolute differences across four axes. `VectorModule` returns the Euclidean norm in 4D.

## `Coord2DL`
- Type: `class` (uses `long` coordinates)
- Implements: `IEquatable<Coord2DL>`
- Fields: `long x`, `long y`
- Nested: `enum Arrangement` (same variants)
- Constructor: `Coord2DL(long x, long y)`
- Operators: arithmetic and equality similar to `Coord2D` but with `long`/`long` results
  - Performs the same component-wise arithmetic as `Coord2D` but using `long` to avoid overflow for larger coordinates.
- Deconstruct: `void Deconstruct(out long x, out long y)`
  - Assigns `x` and `y` (long) to the provided out parameters.
- Conversions: `implicit operator (long,long)(Coord2DL)`, `implicit operator Coord2DL((long X, long Y))`
  - Converts to/from a `(long,long)` tuple mapping fields to tuple items.
- Geometry: `long Manhattan(Coord2DL other)`, `double DistanceTo(Coord2DL other)`, `double VectorModule`, `IEnumerable<Coord2DL> GetNeighbors(Arrangement arrange = ...)`, `IEnumerable<Coord2DL> GetNeighbors8()`, `bool IsInside(...)`, `double GetAngle()`, `override string ToString()`, `override int GetHashCode()`
  - `Manhattan` returns a `long` sum of absolute differences. `DistanceTo` and `VectorModule` compute Euclidean values using `double`. `GetNeighbors` yields orthogonal neighbors in the configured order and `GetNeighbors8` yields the surrounding eight points.

## `Coord3DL`
- Type: `class` (uses `long` coordinates)
- Implements: `IEquatable<Coord3DL>`
- Fields: `long x, y, z`
- Constructor: `Coord3DL(long x, long y, long z)`
- Operators: arithmetic and equality
  - Component-wise arithmetic and equality using `long` coordinates.
- Deconstruct: `void Deconstruct(out long x, out long y, out long z)`
  - Assigns the three `long` fields to the out parameters.
- Neighbors:
  - `IEnumerable<Coord2DL> GetNeighborsXY()`
  - `IEnumerable<Coord3DL> GetNeighbors()` — 6-neighbors
  - `IEnumerable<Coord3DL> GetNeighbors8()` — all neighbors in 3x3x3 excluding self
    - Iterates the 3x3x3 cube with `long` indices and yields all neighbors except the coordinate itself.
- Conversions: `implicit operator (long,long,long)(Coord3DL)`, `implicit operator Coord3DL((int X, int Y, int Z))`
  - Enables implicit tuple conversion; note the conversion from `(int,int,int)` to `Coord3DL` will widen ints to longs.
- Geometry & utility: `long Manhattan(Coord3DL other)`, `double DistanceTo(Coord3DL other)`, `double VectorModule`, `override int GetHashCode()`, `override string ToString()`
  - `DistanceTo` uses `double` for the Euclidean calculation even though coordinates are `long`. `GetHashCode` mixes components into an integer hash.

## `MathHelper`
- Type: `static class`
- Methods:
  - `static long GCD(long num1, long num2)` — greatest common divisor (Euclidean recursion)
    - Uses the Euclidean algorithm recursively: GCD(a,b) = GCD(b, a % b) until the second argument is zero.
  - `static long LCM(List<long> numbers)` — least common multiple of a list
    - Reduces the list by combining pairs with LCM(a,b) = a * b / GCD(a,b) using `Aggregate` to produce the overall LCM.
  - `static int Modulo(int a, int b)` — mathematical modulo that returns non-negative result
    - Computes a modulo b but adjusts the result to always be in the range [0, b-1] even when `a` is negative.

## `ParseUtils`
- Type: `static class`
- Methods:
  - `static List<List<string>> SplitBy(List<string> elements, string splitValue)` — split a list into sublists using `splitValue` as separator
    - Iterates the input list, collects consecutive elements into a sublist, and starts a new sublist each time an element equals `splitValue`. The final sublist is added even if empty.

## `ListUtils`
- Type: `static class`
- Methods:
  - `static IEnumerable<T[]> Windowed<T>(this IEnumerable<T> enumerable, int size, int step = 1)` — sliding window over a sequence, returns arrays of length `size`
    - Materializes the sequence to a list, then yields windows by slicing the list with the given `size` and advancing by `step` until there is not enough remaining elements.

---
For more details, inspect the source files in the `Common` directory (`Coord2D.cs`, `Coord3D.cs`, `Coord4D.cs`, `Coord2DL.cs`, `Coord3DL.cs`, `MathHelper.cs`, `ParseUtils.cs`, `ListUtils.cs`).