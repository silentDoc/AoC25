# AoC25 - Day06

<p align="center">
<img src="AoC25_Day06.jpg" style="width:640px" alt="Day 6: Trash Compactor"/>
</p>

**Image prompt:** _Generate a picture of an elf in a garbage smasher during christmas being asked by a group of cephalopods to help with their math homework. The image must have the style of a the movie "Coco" by Pixar._

## Problem summary
You are given a set of rows of integer values and a final line that specifies an operation per column. For each column, apply its operation to all numbers in that column (either sum with `+` or product with any other operator), producing one result per column. The final answer is the sum of all column results.

There are two parsing modes:
- Part 1: numbers are provided as space-separated tokens on each row; columns align by token index (left-to-right).
- Part 2: numbers on each row can be right-aligned using spaces. Columns must be reconstructed by reading characters vertically from right to left to recover each column's list of numbers.

## Input format
- Multiple lines where all lines except the last contain numeric rows.
  - Part 1 example row: `12 3 45`
  - Part 2 rows may contain spaces used for right alignment, for example:
    " 12  3 45"
- The last line contains a sequence of operators separated by spaces, one operator per column (e.g. `+ * +`).
- Operators: `'+'` means column sum; any other character is treated as multiplication in the current implementation.

## How the solution works

Parsing
- Part 1 (ParseInput):
  - Each row is split on whitespace and parsed as longs producing a List<List<long>> called rows.
  - The final line is parsed to a List<char> ops (operators per column).
- Part 2 (ParseInputRightMost):
  - Rows are treated as fixed-width strings. The parser scans columns from right to left.
  - For each character column it collects vertical characters across rows, assembles the contiguous non-space characters into a number and parses it.
  - The collected columns are stored in cols (List<List<long>>) in right-to-left order; ops are parsed the same way as part 1.

Computation (Calculate)
- For every column index:
  - If the operator for that column is `'+'`, compute the sum of that column's values.
  - Otherwise compute the product (multiplicative aggregation) of that column's values.
- For part 1 the implementation uses rows[col] per row; for part 2 it uses the prebuilt cols list (accounting for the reversed column ordering).
- The final result is the sum of all column results, returned as a string.

## Examples

### Part 1 (left-aligned)
Input:
~~~
1 2 3
4 5 6
+ * +
~~~

Explanation:
- Column 0 (`+`): 1 + 4 = 5  
- Column 1 (`*`): 2 * 5 = 10  (any operator other than `+` is treated as multiplication)  
- Column 2 (`+`): 3 + 6 = 9  

Final result (sum of column results): 5 + 10 + 9 = 24

Expected output:
~~~
24
~~~

---

### Part 2 (right-aligned — spaces are significant)
Input:
~~~
 1  2  3
 4  5  6
+ * +
~~~

Explanation:
- The parser reconstructs columns by reading vertically from right to left and recovers the same numeric columns:
  - Column 0 (`+`): 1 + 4 = 5
  - Column 1 (`*`): 2 * 5 = 10
  - Column 2 (`+`): 3 + 6 = 9
- Final result: 5 + 10 + 9 = 24

Expected output:
~~~
24
~~~

---

### Optional: Part 2 with multi-digit right-aligned numbers
Input:
~~~
  1  2 10
  4 50  6
+ * +
~~~

Explanation:
- Column 0 (`+`): 1 + 4 = 5
- Column 1 (`*`): 2 * 50 = 100
- Column 2 (`+`): 10 + 6 = 16
- Final result: 5 + 100 + 16 = 121

Expected output:
~~~
121
~~~


## Implementation notes
- Parsed data is currently stored in static fields: `rows`, `cols`, and `ops`.
- `ParseInput` uses whitespace splitting and `long.Parse`.
- `ParseInputRightMost` constructs columns by scanning character positions from right to left and parsing contiguous non-space characters as numbers; columns are appended in right-to-left order.
- Calculation uses LINQ:
  - Sum with `Sum()`.
  - Product with `Aggregate(1, (acc, val) => acc * val)`.
- The code returns the total sum of per-column results as a string.

## Edge cases and caveats
- No robust input validation: the code assumes consistent row widths, valid integer tokens, and that the number of operators matches the number of columns.
- `ParseInputRightMost` may behave unexpectedly if rows have different lengths or inconsistent spacing.
- Product aggregation starts from 1: an empty column (if it ever occurs) would yield 1 — input should guarantee non-empty columns.
- Potential for overflow: multiplication of many or large numbers may exceed `long` range.

## Possible improvements
- Remove static mutable state (clear collections or avoid statics) to make `Solve` reentrant and unit-testable.
- Add input validation and use `TryParse` to avoid exceptions on malformed input.
- Make operator semantics explicit and extensible (support `*` explicitly, add `-`, `/`, etc.).
- Use `BigInteger` or checked arithmetic for safe handling of large products.
- Simplify `ParseInputRightMost` by normalizing row lengths first, or use `Span<char>`/`ReadOnlySpan<char>` to reduce allocations.
- Add unit tests for left/right aligned inputs, mismatched operator counts, empty columns, and overflow conditions.
