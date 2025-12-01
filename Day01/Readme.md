# AoC25 - Day01
__Note: This readme file has been auto-generated from code using an AI tool.__

<p align="center">
<img src="AoC25_day1.jpg" style="width:640px" alt="Day 1: Secret Entrance"/>
</p>

## Problem summary
We have a circular dial with 100 discrete positions numbered 0..99. The pointer starts at position 50. The puzzle input is a list of rotations, each given as a string with a direction letter (`L` or `R`) followed by a positive integer amount (for example `L37`, `R260`). Each rotation turns the pointer left (decreasing positions) or right (increasing positions) by the given amount; the dial wraps modulo 100.

You must count how many times the pointer lands (or passes) on position 0 according to two different rules:

- Part 1: After applying each rotation, check the pointer's final position. Count how many rotations leave the pointer exactly at 0.
- Part 2: Each rotation can be larger than 100 and therefore can pass position 0 multiple times within the same rotation. Count every time the pointer passes or lands on position 0 while performing the rotation (including complete 100-position turns inside a single rotation).

## Input format
- A list of strings (one per rotation), each of the form `L<number>` or `R<number>`.
- Example lines: `R60`, `L150`, `R40`.

## How the solution works

Common setup:
- Start position: pos = 50.
- Use a positive modulo helper to keep pos in range 0..99:
  - Modulo implemented as `(a % b + b) % b` to avoid negative results.

Part 1 (SolvePart1):
- For each rotation:
  - Parse `rotationDir` = first character (`'L'` or `'R'`).
  - Parse `rotationAmount` = integer part after the first character.
  - Update position: pos += (rotationDir == 'L' ? -rotationAmount : rotationAmount).
  - Wrap with modulo 100.
  - If pos == 0 after the update, increment the counter.
- Complexity: O(n) where n is the number of rotations.

Part 2 (SolvePart2):
- The key difference: a single rotation may pass position 0 multiple times.
- For each rotation:
  - Compute `distance_to_Zero` as the number of steps from the current position to reach 0 in the rotation direction:
    - If pos == 0, treat `distance_to_Zero` as 100 (a full turn needed to come back to 0).
    - If rotating left (decreasing indices), `distance_to_Zero = pos`.
    - If rotating right (increasing indices), `distance_to_Zero = 100 - pos`.
  - Decompose the rotation amount into:
    - `fullTurns = rotationAmount / 100` — each full 100-step turn crosses position 0 exactly once.
    - `remainingRotation = rotationAmount % 100` — the leftover steps after full turns.
  - Increment the counter by `fullTurns`.
  - If `remainingRotation >= distance_to_Zero`, the leftover steps will reach or pass 0 once more, so increment the counter by 1.
  - Finally, update `pos` by adding/subtracting `rotationAmount` and wrap with modulo 100.
- This correctly counts every pass or landing on 0 that occurs during each rotation.

## Examples
- Starting pos = 50, rotation `R60`:
  - distance_to_Zero = 100 - 50 = 50
  - fullTurns = 60 / 100 = 0
  - remainingRotation = 60
  - remainingRotation >= distance_to_Zero → counts 1 crossing
  - New pos = (50 + 60) % 100 = 10

- Rotation `L150` from pos = 10:
  - distance_to_Zero = pos = 10 (because rotating left)
  - fullTurns = 150 / 100 = 1 → counts 1 crossing
  - remainingRotation = 50
  - remainingRotation >= distance_to_Zero (50 >= 10) → counts 1 more
  - Total 2 crossings for that rotation
  - New pos = (10 - 150) mod 100 = (10 - 150 + 100*2) % 100 = 60

## Implementation notes
- The code uses a small utility `MathHelper.Modulo(int a, int b)` to produce a non-negative modulo result.
- Input parsing assumes valid lines beginning with `L` or `R` followed by a non-negative integer.
- Both solutions are linear in the number of rotations and use constant extra memory.