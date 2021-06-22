using System;
using System.Collections.Generic;

namespace Modular_Linear_Equation
{
    public class ModularLinearEquationSolver
    {
        private readonly int _a;
        private readonly int _b;
        private readonly int _n;
        public ModularLinearEquationSolver(int a, int b, int n)
        {
            if (a <= 0)
                throw new ArgumentException("Invalid a parameter", nameof(a));
            if (n <= 0)
                throw new ArgumentException("Invalid argument n", nameof(n));
            _a = a;
            _n = n;
            _b = b;
        }

        private static (int x, int y, int z) ExtendedEuclid(int a, int b)
        {
            if (b == 0)
                return (a, 1, 0);
            (int d1, int x1, int y1) = ExtendedEuclid(b, a % b);
            int d = d1;
            int x = y1;
            int y = x1 - a / b * y1;
            return (d, x, y);
        }

        public bool HasSolution()
        {
            return _b % ExtendedEuclid(_a, _n).x == 0;
        }

        public bool HasInverse()
        {
            return ExtendedEuclid(_a, _n).x == 1;
        }

        public int GetSolutionsNumber()
        {
            return ExtendedEuclid(_a, _n).x;
        }

        public IEnumerable<ModularEquationSolution> SolveLinearEquation()
        {
            if (!HasSolution())
                throw new InvalidOperationException("The equation has no solution.");
            return SolutionsYielder();
        }

        private IEnumerable<ModularEquationSolution> SolutionsYielder()
        {
            (int d, int x1, _) = ExtendedEuclid(_a, _n);
            int x0 = x1 * (_b / d) % _n;
            x0 = x0 > 0 ? x0 : x0 + _n;
            for (int i = 0; i <= d - 1; i++)
            {
                int solution = x0 + i * (_n / d) % _n;
                solution = solution > _n ? solution - _n : solution;
                yield return new ModularEquationSolution(solution, _n);
            }
        }
    }

    public class ModularEquationSolution
    {
        public ModularEquationSolution(int value, int module)
        {
            Value = value;
            Module = module;
        }
        public int Value { get; }
        public int Module { get; }

        public override string ToString()
        {
            return Value + " (mod " + Module + ")";
        }
    }
}
