# AoC25 - Day03

<p align="center">
<img src="AoC25_day3.jpg" style="width:640px" alt="Day 3: Lobby"/>
</p>

**Image Prompt:** _"Generate an image of an elf in vast lobby in a christmas environment. He is beside the doors of an elevator, looking to a hatch in the wall opened where he is manipulating few batteries. The style of the image looks like a Pixar movie"_

## Problem summary (English)
Each input line is a sequence of decimal digits (a "battery bank"). For each bank you must construct the largest possible integer with a fixed number of digits by selecting digits from the bank in their original order (you may drop digits but not reorder them). The solver computes this maximal integer for every bank and returns the sum of those maxima.

The code uses two different target lengths:
- Part 1: build the largest 2-digit number from each bank.
- Part 2: build the largest 12-digit number from each bank.

## Input format
- Each line of input is a string composed only of digits, e.g. `59134`.
- Each line is processed independently; the result is the sum of per-line maxima.

## How the solution works

Parsing
- ParseBank converts every input line into a List<int> of its digits.

Greedy selection (core idea)
- To form the largest number of length k from a sequence while keeping relative order, the algorithm picks digits greedily:
  - For the next digit to place, consider only those digits that leave enough remaining digits to complete the number.
  - From that prefix choose the maximum digit, then continue from the element after the chosen digit.
- This is implemented by SelectMax and MaxJoltage:
  - SelectMax(bank, numDigits) finds the maximum digit inside bank.Take(bank.Count - numDigits) (i.e., the prefix that still allows selecting numDigits later) and returns that digit and the remaining suffix after it. This ensures that enough digits remain to complete the number.
  - MaxJoltage(bank, numDigits) repeats SelectMax for each position (from most significant to least), assembling the final integer (result = result * 10 + digit).

Example (illustrative)
- Bank digits: [5, 1, 9, 3], target length = 2
  - First pick: consider bank.Take(4 - 1) = first 3 digits [5,1,9] -> pick 9 (index 2)
  - Remaining suffix after 9 -> [3]
  - Next pick: consider [3] -> pick 3
  - Constructed number = 9 * 10 + 3 = 93

Complexity
- For a bank of length n and target k, the naive implementation scans prefixes and uses Max repeatedly, giving roughly O(k * n) time per bank. Memory overhead is proportional to the bank size.

## Implementation notes
- The solver is concise and follows a clear greedy approach to ensure the maximum lexicographic numeric result while preserving order.
- Edge cases:
  - If a line has fewer digits than the requested target length, the current code behavior will throw when Take(...).Max() is called; input must provide at least that many digits.
  - Input lines are assumed to contain only digits; no trimming/validation is performed.
- The code returns the total sum of the per-line maxima as a string.
