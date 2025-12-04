# AoC25 - Day02
_Note: this README was generated from the implementation in `Day02/Solver.cs`._

<p align="center">
<img src="AoC25_day2.jpg" style="width:640px" alt="Day 2: Gift Shop"/>
</p>

**Image Prompt:** _"Generate an image of an elf in a gift shop in the north pole, in a christmas environment. There has to be a sign with the text "Thank you for visiting the North Pole!" and an extensive gift selection, as well as a clerk with a computer."_

## Problem summary (English)
Given a single-line input that contains one or more comma-separated inclusive ranges (for example `100-999,1000-1999`), iterate every integer inside each range and sum those integers that satisfy a repetition property.

There are two different repetition rules, one for each part:
- Part 1: the decimal representation has even length and the first half equals the second half (e.g. 1212, 44).
- Part 2: the decimal representation can be split into equal-length blocks (block length divides the total length) where every block is identical (e.g. 121212, 333, 7777).

Return the sum of all numbers that match the corresponding rule.

## Input format
- A single string in input[0], containing comma-separated ranges.
- Each range is two integers separated by `-`, inclusive.
- Example: `10-50,100-200`

## How the solution works

Parsing
- ParsePairs reads `input[0]`, splits on `,` to get ranges, then splits each range on `-` and yields the numeric pair (start, end). The solver iterates every integer i from start to end inclusively.

Common loop
- For each i in each parsed range, convert i to string and test the appropriate predicate:
  - If part == 1 -> HasRepeats(i)
  - If part == 2 -> HasRepeatsPart2(i)
- If the predicate is true, add i to the running sum.
- Return the sum as a string.

Part 1 (HasRepeats)
- Convert the number to its decimal string.
- If the length is even and the substring consisting of the first half equals the substring of the second half, the number matches.
- Example: "1212" -> "12" == "12" -> match. "123" -> length odd -> not match.

Part 2 (HasRepeatsPart2)
- Convert the number to its decimal string.
- For every candidate block size b from 1 to length/2:
  - Skip b if total length is not divisible by b.
  - Split the string into contiguous blocks of size b (the code uses a Windowed helper to do this).
  - If all blocks are identical, the number matches and the function returns true.
- This detects repetitions of any number of identical blocks (>=2). Part 1 cases (two identical halves) are a subset of Part 2 if present.

## Examples
- Input: `10-20`
  - Part 1: only `11` matches -> sum = 11
  - Part 2: `11` also matches (two identical blocks of length 1) -> sum = 11
- Input: `100-999`
  - Part 1: matches are three-digit numbers with even length (none) → sum = 0
  - Part 2: matches include `111`, `222`, ... `999` (single-digit block repeated three times) → sum = 111+222+...+999

## Implementation notes
- ParsePairs uses `input[0]` only — the input must place the ranges on the first line.
- The code uses string-based checks (ToString, substring comparisons and a Windowed helper) which makes the implementation simple and correct for typical Advent of Code input sizes.
- Time complexity: proportional to the total size of all ranges times the cost to test the predicates (string allocation and comparisons).
- Memory: minimal, only a few small allocations per tested number (string and temporary block list).

## Possible improvements
- Avoid repeated string allocations by using Span<char> or integer arithmetic to test repetition patterns.
- If ranges are large, iterate and test using faster numeric checks or filter possible candidate numbers (for example, for Part 1 you can generate numbers by constructing the repeated half rather than scanning every integer).
- Add validation and robust parsing (trim, TryParse) to handle malformed input gracefully.
- Add unit tests covering edge cases: single-digit numbers, odd lengths, maximum/minimum range values, and boundary parsing errors.
