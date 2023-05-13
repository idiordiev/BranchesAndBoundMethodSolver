using System.Diagnostics;
using BranchesAndBoundMethodSolver.Logic.Enums;

namespace BranchesAndBoundMethodSolver.Logic.Models
{
    [DebuggerDisplay("{Path} {Cost} {Status}")]
    public class Node
    {
        public string Path { get; set; }
        public NodeName Name { get; set; }
        public int Cost { get; set; }
        public NodeStatus Status { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Node other)
            {
                return Path == other.Path
                       && Name == other.Name
                       && Cost == other.Cost
                       && Status == other.Status;
            }

            return false;
        }
    }
}