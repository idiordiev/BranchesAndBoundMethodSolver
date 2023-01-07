namespace BranchesAndBoundMethodSolver.Logic
{
    public interface IAlgorithm
    {
        IEnumerable<Node> Calculate(NodeName endNode);
    }
}