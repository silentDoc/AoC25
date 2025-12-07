# AoC25 - Day07

<p align="center">
<img src="AoC25_Day07.jpg" style="width:640px" alt="Day 7: Laboratories"/>
</p>

**Image prompt:** _Generate image depicting an elf in a science lab where there are spare parts, manuals, and diagnostic equipment lying around. There is a display that shows "0H-N0". The style of the image must be like a pixar movie._

## Problem summary
A beam (tachyon) starts at the position of the character 'S' in the first line of a rectangular grid of characters. The grid is processed row by row downward. Each cell is either:
- '.' — the beam continues straight down in the same column,
- any other character (for example '^') — the beam splits and sends one beam to the column on the left and one to the column on the right.

Two answers are computed:
- Part 1: count how many split events happen while beams encounter explicit split markers (the code treats cells equal to '^' as counted split events for tracked beam columns).
- Part 2: compute how many beams reach the bottom row (sum of beams across all columns after processing all rows).

## Input format
- A list of equal-length strings (grid rows). The first row contains exactly one 'S' marking the source column.
- Remaining rows contain '.' or split characters (for example '^'). Rows must all have the same length.
- No extra metadata: the solver reads the full supplied grid.

Example grid format:
..S.. 
..^.. 
.....


## How the solution works
- Initialize:
  - Find the column index of 'S' in the first row.
  - `tachyonCols` is a set of columns currently tracked for split-detection (starts containing the source column).
  - `timelines` is an array with one entry per column that holds how many beams are present in that column for the current row; set the source column to 1.
- For each subsequent row:
  - Determine `splitBeams`: those tracked columns where the current cell equals '^'. Each such occurrence is counted as a split (increment `splits`), removed from the tracked set and replaced by its left and right neighbour columns in the tracked set.
  - Build the next row's `timelines` (`next` array) by propagating beams:
    - If a cell is '.', beams in that column continue straight down: `next[col] += timelines[col]`.
    - Otherwise (any non-'.' cell) beams split to left and right: `next[col-1] += timelines[col]` and `next[col+1] += timelines[col]`.
  - Set `timelines = next` and continue to the next row.
- At the end:
  - Part 1 returns the counted `splits` (number of times tracked beams encountered '^').
  - Part 2 returns the sum of `timelines` after processing the last row (total beams that reached the bottom).

Note: the code assumes splits do not occur at the grid edges (accesses col-1 and col+1); input should ensure safe indexing.

## Examples

### Example 1 — simple split (both parts)
Input grid:
..S.. 
..^.. 
.....

Step-by-step:
- Row 0: source at column 2 (0-based). timelines = [0,0,1,0,0], tachyonCols = {2}.
- Row 1: at column 2 there is '^' and tachyonCols contains 2 → this is a counted split (splits = 1). The tracked column 2 is replaced by columns 1 and 3. Propagation also splits numeric beams: timelines[2] = 1 causes next[1] += 1 and next[3] += 1 → next = [0,1,0,1,0].
- Row 2: all '.' so beams at columns 1 and 3 continue straight → timelines remain [0,1,0,1,0].

Outputs:
- Part 1 (counted '^' splits): 1
- Part 2 (total beams reaching bottom): 2

Expected outputs:
Part 1: 1
Part 2: 2

(Describe expected behaviour: beams may split multiple times and propagate; final counts depend on positions. Use these small grids to verify logic and indexing.)

## Implementation notes
- The solver keeps two main pieces of state:
  - `timelines`: long[] with the count of beams per column for the current row.
  - `tachyonCols`: HashSet<int> with columns used to detect and count explicit split markers '^'.
- The solver uses long arithmetic for counts to avoid early overflow for moderate input sizes; final part 2 returns the sum of long values.
- Edge indexing: the code writes to `next[col - 1]` and `next[col + 1]` when encountering a split cell — ensure input avoids splits at column 0 or last column.
- Part 1's counted splits only consider tracked columns (`tachyonCols`) and cells that equal '^'.

## Possible improvements
- Add robust input validation: check uniform row length, ensure 'S' exists and is unique, and guard against splits at edges.
- Unify split semantics: currently any non-'.' causes numeric splitting, while only '^' increments the split counter for tracked columns. Consider making these semantics explicit and documented or unify them.
- Make the algorithm safer at boundaries by ignoring splits that would go out of range or defining behaviour for edge splits.
- Add unit tests that cover edge cases: source at edges, multiple simultaneous splits, large grids, and overflow scenarios.
- If counts can grow large, consider using BigInteger or saturating arithmetic.