namespace BranchesAndBoundMethodSolver.Logic
{
    public class Node
    {
        public string Path { get; set; }
        public NodeName Name { get; set; }
        public int Cost { get; set; }
        public NodeStatus Status { get; set; }
    }
}