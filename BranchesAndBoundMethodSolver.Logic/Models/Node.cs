using BranchesAndBoundMethodSolver.Logic.Enums;

namespace BranchesAndBoundMethodSolver.Logic.Models
{
    public class Node
    {
        public string Path { get; set; }
        public NodeName Name { get; set; }
        public int Cost { get; set; }
        public NodeStatus Status { get; set; }
    }
}