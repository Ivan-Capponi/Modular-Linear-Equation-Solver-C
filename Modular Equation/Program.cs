using System;
using Modular_Linear_Equation;

namespace Modular_Equation
{
    class Program
    {
        static void Main(string[] args)
        {
            ModularLinearEquationSolver solver = new ModularLinearEquationSolver(6, 14, 6);
            Console.WriteLine("Solution number: " + solver.GetSolutionsNumber());
            foreach (var solution in solver.SolveLinearEquation())
                Console.WriteLine(solution);
            Console.ReadLine();
        }
    }
}
