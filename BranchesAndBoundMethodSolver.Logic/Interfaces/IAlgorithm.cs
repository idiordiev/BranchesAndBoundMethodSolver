using BranchesAndBoundMethodSolver.Logic.Models;

namespace BranchesAndBoundMethodSolver.Logic.Interfaces
{
    public interface IAlgorithm
    {
        IEnumerable<Node> Calculate();
    }
}