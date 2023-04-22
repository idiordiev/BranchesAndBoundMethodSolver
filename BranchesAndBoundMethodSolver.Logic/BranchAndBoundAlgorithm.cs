using BranchesAndBoundMethodSolver.Logic.Enums;
using BranchesAndBoundMethodSolver.Logic.Interfaces;
using BranchesAndBoundMethodSolver.Logic.Models;
using Serilog;

namespace BranchesAndBoundMethodSolver.Logic
{
    public class BranchAndBoundAlgorithm : IAlgorithm
    {
        private Matrix _matrix;
        private ICollection<Node> _result;

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
                
                var currentRecordCandidates = _result.Where(n => n.Name == endNode);
                if (currentRecordCandidates.Any())
                {
                    FindRecords(endNode);
                    break;
                }

                currentNode = _result
                    .Where(n => n.Status == NodeStatus.ReadyToBranch)
                    .OrderBy(n => n.Cost)
                    .ThenByDescending(n => n.Path.Length)
                    .ThenBy(n => n.Name)
                    .First();
                Log.Information("Current node {Path}, cost {Cost}", currentNode.Path, currentNode.Cost);
                
                BranchNode(currentNode);
                //ExcludeNodesWithSameCostButShorterPath(currentNode);
                
                ExcludeSameNodesWithGreaterPathCost(currentNode);
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
                Log.Information("Nodes: [{Nodes}]", string.Join(", ", _result.Select(n => $"{n.Path}:{n.Cost}:{n.Status}")));
                
                var currentRecordCandidates =
                    _result.Where(n => n.Name == endNode && n.Status == NodeStatus.ReadyToBranch);
                Log.Information("Current record candidates {Candidates}", string.Join(", ", currentRecordCandidates.Select(x => x.Path)));

                int currentRecordCost = currentRecordCandidates.OrderBy(r => r.Cost).First().Cost;
                Log.Information("Current record cost {Cost}", currentRecordCost);
                
                var currentRecords = currentRecordCandidates.Where(n => n.Cost == currentRecordCost);
                Log.Information("Current records {Records}", string.Join(", ", currentRecords.Select(x => x.Path)));

                var nodesWithGreaterCost =
                    _result.Where(n => n.Cost >= currentRecordCost 
                        && n.Status == NodeStatus.ReadyToBranch
                        && !currentRecords.Contains(n));
                Log.Information("Nodes with greater cost {Nodes}", string.Join(", ", nodesWithGreaterCost.Select(x => x.Path)));

                foreach (Node node in nodesWithGreaterCost)
                {
                    Log.Information("Excluding node {Path} with cost {Cost} by test", node.Path, node.Cost);
                    node.Status = NodeStatus.ExcludedByTest;
                }

                Log.Information("Start branching nodes");
                var nodesReadyToBranch = _result.Where(n => n.Status == NodeStatus.ReadyToBranch && n.Name != endNode)
                    .OrderBy(n => n.Cost)
                    .ThenByDescending(n => n.Path.Length)
                    .ThenBy(n => n.Name);
                
                if (nodesReadyToBranch.Any())
                {
                    Log.Information("Nodes ready to branch {Nodes}", string.Join(", ", nodesReadyToBranch.Select(x => x.Path)));
                    foreach (var node in nodesReadyToBranch)
                    {
                        var smallestNode = _result.Where(n => n.Name == node.Name && !currentRecords.Contains(n))
                            .OrderBy(n => n.Cost)
                            .First();
                        
                        ExcludeSameNodesWithGreaterPathCost(smallestNode);

                        BranchNode(node);
                        break;
                    }
                }
                else
                {
                    Log.Information("Record nodes {Nodes}", string.Join(", ", currentRecordCandidates.Select(x => x.Path)));
                    foreach (var node in currentRecordCandidates)
                    {
                        Log.Information("Node {Node} with cost {Cost} is a record", node.Path, node.Cost);
                        node.Status = NodeStatus.Record;
                    }

                    break;
                }
            }
        }

        private void BranchNode(Node node)
        {
            if (node.Status != NodeStatus.ReadyToBranch)
                return;
            
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

        private void ExcludeSameNodesWithGreaterPathCost(Node currentNode)
        {
            var sameNodesWithGreaterCost = _result.Where(n => n.Name == currentNode.Name
                                                              && n.Cost > currentNode.Cost
                                                              && n.Status == NodeStatus.ReadyToBranch);
            foreach (Node node in sameNodesWithGreaterCost)
            {
                Log.Information("Excluding node {Path} with cost {Cost} by VD", node.Path, node.Cost);
                node.Status = NodeStatus.ExcludedByVD;
            }
        }
    }
}