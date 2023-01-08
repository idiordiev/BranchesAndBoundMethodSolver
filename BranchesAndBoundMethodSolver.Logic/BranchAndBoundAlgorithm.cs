using BranchesAndBoundMethodSolver.Logic.Enums;
using BranchesAndBoundMethodSolver.Logic.Interfaces;
using BranchesAndBoundMethodSolver.Logic.Models;

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

            var endNode = (NodeName)_matrix.Rows - 1;

            var currentNode = new Node
            {
                Path = NodeName.A.ToString(),
                Name = NodeName.A,
                Cost = 0,
                Status = NodeStatus.ReadyToBranch
            };
            
            _result.Add(currentNode);

            while (_result.Any(n => n.Status == NodeStatus.ReadyToBranch))
            {
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

                ExcludeSameNodesWithGreaterPathCost(currentNode);
                ExcludeNodesWithSameCostButShorterPath(currentNode);

                BranchNode(currentNode);
            }

            return _result;
        }

        private void FindRecords(NodeName endNode)
        {
            while (true)
            {
                var currentRecordCandidates = _result.Where(n => n.Name == endNode && n.Status == NodeStatus.ReadyToBranch);
                var currentRecord = currentRecordCandidates.OrderBy(r => r.Cost).First();

                var nodesWithGreaterCost = _result.Where(n => n.Cost > currentRecord.Cost && n.Status == NodeStatus.ReadyToBranch);
                foreach (var node in nodesWithGreaterCost)
                {
                    node.Status = NodeStatus.ExcludedByTest;
                }

                var nodesReadyToBranch = _result.Where(n => n.Status == NodeStatus.ReadyToBranch && n.Name != endNode)
                    .OrderBy(n => n.Cost)
                    .ThenByDescending(n => n.Path.Length)
                    .ThenBy(n => n.Name);

                if (nodesReadyToBranch.Any())
                {
                    foreach (var node in nodesReadyToBranch)
                    {
                        BranchNode(node);
                        break;
                    }
                }
                else
                {
                    foreach (var node in currentRecordCandidates)
                    {
                        node.Status = NodeStatus.Record;
                    }
                    break;
                }
            }
        }

        private void BranchNode(Node node)
        {
            node.Status = NodeStatus.Branched;
            for (var column = 0; column <= _matrix.Columns - 1; column++)
            {
                if (_matrix[(int)node.Name, column] > 0)
                {
                    _result.Add(new Node
                    {
                        Path = node.Path + (NodeName)column,
                        Name = (NodeName)column,
                        Cost = node.Cost + _matrix[(int)node.Name, column],
                        Status = NodeStatus.ReadyToBranch
                    });
                }
            }
        }

        private void ExcludeNodesWithSameCostButShorterPath(Node currentNode)
        {
            var nodesWithSameCostButShorterPath = _result.Where(n => n.Cost == currentNode.Cost
                                                                     && n.Path.Length < currentNode.Path.Length
                                                                     && n.Status == NodeStatus.ReadyToBranch);
            foreach (var node in nodesWithSameCostButShorterPath)
            {
                node.Status = NodeStatus.ExcludedByVD;
            }
        }

        private void ExcludeSameNodesWithGreaterPathCost(Node currentNode)
        {
            var sameNodesWithGreaterCost = _result.Where(n => n.Name == currentNode.Name
                                                              && n.Cost > currentNode.Cost
                                                              && n.Status == NodeStatus.ReadyToBranch);
            foreach (var node in sameNodesWithGreaterCost)
            {
                node.Status = NodeStatus.ExcludedByVD;
            }
        }
    }
}