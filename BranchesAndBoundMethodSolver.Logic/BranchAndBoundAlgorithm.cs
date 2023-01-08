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

        public IEnumerable<Node> Calculate(NodeName endNode)
        {
            if (_matrix.Rows == 0 || _matrix.Columns == 0)
            {
                return new List<Node>();
            }

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
                var currentRecordCandidates = _result.Where(n => n.Name == endNode).ToList();
                if (currentRecordCandidates.Any())
                {
                    while (true)
                    {
                        var currentRecord = currentRecordCandidates.OrderBy(r => r.Cost).First();

                        var nodesReadyToTest = _result.Where(n => n.Status == NodeStatus.ReadyToBranch).ToList();

                        if (nodesReadyToTest.Any())
                        {
                            foreach (var node in nodesReadyToTest)
                            {
                                if (node.Cost > currentRecord.Cost)
                                {
                                    node.Status = NodeStatus.ExcludedByTest;
                                }
                                else
                                {
                                    BranchNode(node);
                                }
                            }
                        }
                        else
                        {
                            currentRecordCandidates.ForEach(r => r.Status = NodeStatus.Record);
                            break;
                        }
                    }
                    
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