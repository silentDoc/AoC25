# AoC25

Advent of Code 2025

So sorry there's only 12 puzzles this year... Only half the fun! :(

This year I will be adding a README file for each day's puzzle, explaining how I approached the solution, auto-generated from the code using an AI tool (VS Copilot) and also containing a generated AI image (an idea I gently borrowed from [encse](https://github.com/encse) as I did last year). I encourage you again to check his solutions as they're super readable and elegant; I learned a lot from them.

**No AI was used to solve the puzzles, only to generate the explanations and images after solving them.**

**All the puzzle creation credit goes to the [Advent of Code](https://adventofcode.com/) crew.**

The puzzles of this repository can be found at: [Advent of Code - 2025](https://adventofcode.com/2025/)

## Notas de refactorización

Se ha actualizado `Day01/Solver.cs` y se documentan aquí los cambios relevantes:

- **Un único punto público de entrada**: el método `Solve(List<string>, int)` emplea una _switch expression_ para delegar en un único método interno `Solution`, que implementa ambas partes (1 y 2) evitando duplicación de código.
- **Parseo compacto**: se extraen el carácter de dirección y la cantidad con deconstrucción de tupla: `var (rotationDir, rotationAmount) = (rotation[0], int.Parse(rotation[1..]));`.
- **Actualización y normalización de posición** en una sola operación seguida de `MathHelper.Modulo(pos, 100)` para garantizar un resultado en 0..99.
- **Cálculo de `distance_to_Zero` centralizado**; si `pos == 0` se trata como 100 para contabilizar correctamente una vuelta completa.
- **Parte 2**: el conteo de cruces sobre la posición 0 se realiza con `fullTurns = rotationAmount / 100` y `remainingRotation = rotationAmount % 100`; se suman `fullTurns` cruces y se añade una más si `remainingRotation >= distance_to_Zero`.
- **Resultado**: la refactorización mejora claridad y reduce duplicación sin cambiar el comportamiento funcional del algoritmo.
