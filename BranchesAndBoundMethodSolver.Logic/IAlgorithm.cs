namespace BranchesAndBoundMethodSolver.Logic
{
    public interface IAlgorithm
    {
        IEnumerable<Subset> Calculate(Matrix matrix);
    }
}