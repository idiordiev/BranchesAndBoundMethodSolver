using BranchesAndBoundMethodSolver.Logic.Enums;
using BranchesAndBoundMethodSolver.Logic.Interfaces;
using BranchesAndBoundMethodSolver.Logic.Models;
using Serilog;

namespace BranchesAndBoundMethodSolver.Logic;

public class BranchAndBoundAlgorithm : IAlgorithm
{
    private readonly Matrix _matrix;
    private readonly ICollection<Node> _result;

    public BranchAndBoundAlgorithm(Matrix matrix)
    {
        _matrix = matrix;
        _result = new List<Node>();
    }

    public IEnumerable<Node> Calculate()
    {
        if (_matrix.Rows == 0 || _matrix.Columns == 0)
        {
            return new List<Node>();
        }

        Log.Information("Start calculation. Number of nodes {Count}", _matrix.Rows);
        var endNode = (NodeName)_matrix.Rows - 1;
        Log.Information("Name of end node {Name}", endNode);

        var currentNode = new Node
        {
            Path = NodeName.A.ToString(),
            Name = NodeName.A,
            Cost = 0,
            Status = NodeStatus.ReadyToBranch
        };

        _result.Add(currentNode);

        var iteration = 0;

        while (_result.Any(n => n.Status == NodeStatus.ReadyToBranch))
        {
            iteration++;
            Log.Information("Iteration {Iteration}", iteration);
            Log.Information("Nodes: [{Nodes}]", string.Join(", ", _result.Select(n => $"{n.Path}:{n.Cost}:{n.Status}")));

            // Check if any records
            if (_result.Any(n => n.Name == endNode))
            {
                FindRecords(endNode);
                break;
            }

            // Branch node
            currentNode = _result
                .Where(n => n.Status == NodeStatus.ReadyToBranch)
                .OrderBy(n => n.Cost)
                .ThenByDescending(n => n.Path.Length)
                .First();
            Log.Information("Current node {Path}, cost {Cost}", currentNode.Path, currentNode.Cost);

            BranchNode(currentNode);

            // Exclude by VD
            var nodesToCheckByVd = _result
                .Where(x => x.Status is NodeStatus.ReadyToBranch or NodeStatus.Branched)
                .OrderBy(x => x.Cost).ToList();

            foreach (var node in nodesToCheckByVd)
            {
                ExcludeByVD(node);
            }
        }

        Log.Information("Calculation ended");
        
        return _result;
    }

    private void FindRecords(NodeName endNode)
    {
        Log.Information("Start finding records");

        var iteration = 0;
        while (true)
        {
            iteration++;
            Log.Information("Record iteration {Iteration}", iteration);
            Log.Information("Nodes: [{Nodes}]",
                string.Join(", ", _result.Select(n => $"{n.Path}:{n.Cost}:{n.Status}")));

            var currentRecordCandidates = _result.Where(n => n.Name == endNode && n.Status == NodeStatus.ReadyToBranch);
            Log.Information("Current record candidates {Candidates}", string.Join(", ", currentRecordCandidates.Select(x => x.Path)));

            var currentRecordCost = currentRecordCandidates.OrderBy(r => r.Cost).First().Cost;
            Log.Information("Current record cost {Cost}", currentRecordCost);

            var currentRecords = currentRecordCandidates.Where(n => n.Cost == currentRecordCost);
            Log.Information("Current records {Records}", string.Join(", ", currentRecords.Select(x => x.Path)));
            
            ExcludeByTest(currentRecordCost, currentRecords);
            
            // Branch node
            Log.Information("Start branching nodes");
            var nodeToBranch = _result
                .Where(n => n.Status == NodeStatus.ReadyToBranch && n.Name != endNode)
                .OrderBy(n => n.Cost)
                .ThenByDescending(n => n.Path.Length)
                .FirstOrDefault();

            if (nodeToBranch is null)
            {
                Log.Information("Record nodes {Nodes}", string.Join(", ", currentRecordCandidates.Select(x => x.Path)));
                foreach (var node in currentRecordCandidates)
                {
                    Log.Information("Node {Node} with cost {Cost} is a record", node.Path, node.Cost);
                    node.Status = NodeStatus.Record;
                }

                break;
            }

            BranchNode(nodeToBranch);
            
            ExcludeByTest(currentRecordCost, currentRecords);

            // Exclude by VD
            var nodesToCheckByVd = _result
                .Where(x => x.Status is NodeStatus.ReadyToBranch or NodeStatus.Branched
                            && x.Name != endNode)
                .OrderBy(x => x.Cost)
                .ToList();

            foreach (var node in nodesToCheckByVd)
            {
                ExcludeByVD(node);
            }
        }
    }

    private void ExcludeByTest(int currentRecordCost, IEnumerable<Node> currentRecords)
    {
        // Exclude by Test
        var nodesWithGreaterCost = _result.Where(n => n.Cost >= currentRecordCost
                                                      && n.Status == NodeStatus.ReadyToBranch
                                                      && !currentRecords.Contains(n));
        Log.Information("Nodes with greater cost {Nodes}",
            string.Join(", ", nodesWithGreaterCost.Select(x => x.Path)));

        foreach (var node in nodesWithGreaterCost)
        {
            Log.Information("Excluding node {Path} with cost {Cost} by test", node.Path, node.Cost);
            node.Status = NodeStatus.ExcludedByTest;
        }
    }

    private void BranchNode(Node node)
    {
        if (node.Status != NodeStatus.ReadyToBranch)
        {
            return;
        }

        Log.Information("Branching node {Path}", node.Path);
        node.Status = NodeStatus.Branched;
        for (var column = 0; column <= _matrix.Columns - 1; column++)
        {
            if (_matrix[(int)node.Name, column] > 0)
            {
                var newNode = new Node
                {
                    Path = node.Path + (NodeName)column,
                    Name = (NodeName)column,
                    Cost = node.Cost + _matrix[(int)node.Name, column],
                    Status = NodeStatus.ReadyToBranch
                };
                Log.Information("Adding new node {Path} with cost {Cost}", newNode.Path, newNode.Cost);

                _result.Add(newNode);
            }
        }
    }

    private void ExcludeByVD(Node currentNode)
    {
        var nodesToExclude = _result.Where(n => n.Name == currentNode.Name
                                                          && n.Cost > currentNode.Cost
                                                          && n.Status is NodeStatus.ReadyToBranch or NodeStatus.Branched);
        foreach (var node in nodesToExclude)
        {
            Log.Information("Excluding node {Path} with cost {Cost} by VD", node.Path, node.Cost);
            node.Status = NodeStatus.ExcludedByVD;
        }
    }
}