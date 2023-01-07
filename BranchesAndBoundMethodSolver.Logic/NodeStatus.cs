namespace BranchesAndBoundMethodSolver.Logic
{
    public enum NodeStatus
    {
        ReadyToBranch = 0,
        Branched = 1,
        ExcludedByVD = 2,
        ExcludedByTest = 3,
        Record = 4
    }
}