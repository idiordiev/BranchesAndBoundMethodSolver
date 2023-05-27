using BranchesAndBoundMethodSolver.Logic;
using BranchesAndBoundMethodSolver.Logic.Enums;
using BranchesAndBoundMethodSolver.Logic.Models;
using NUnit.Framework;

namespace BranchAndBoundMethodSolver.Tests;

[TestFixture]
public class BranchAndBoundAlgorithmTests
{
    [Test]
    public void Calculate_TestCase1_ReturnsCorrectListOfNodes()
    {
        // Arrange
        var matrix = new Matrix(new[,]
        {
            { 0, 3, 6, 4, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 2, 6, 4, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 4, 5, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 8, 4, 0, 0 },
            { 0, 0, 0, 0, 1, 0, 0, 5, 5, 7, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 7, 3, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        });
        var expected = new List<Node>
        {
            new Node { Name = NodeName.A, Path = "A", Cost = 0, Status = NodeStatus.Branched },
            new Node { Name = NodeName.B, Path = "AB", Cost = 3, Status = NodeStatus.Branched },
            new Node { Name = NodeName.C, Path = "AC", Cost = 6, Status = NodeStatus.Branched },
            new Node { Name = NodeName.D, Path = "AD", Cost = 4, Status = NodeStatus.Branched },
            new Node { Name = NodeName.E, Path = "ABE", Cost = 9, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.F, Path = "ADF", Cost = 8, Status = NodeStatus.Branched },
            new Node { Name = NodeName.G, Path = "ADG", Cost = 9, Status = NodeStatus.Branched },
            new Node { Name = NodeName.E, Path = "ACE", Cost = 8, Status = NodeStatus.Branched },
            new Node { Name = NodeName.F, Path = "ACF", Cost = 12, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.G, Path = "ACG", Cost = 10, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.E, Path = "ADFE", Cost = 9, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.H, Path = "ADFH", Cost = 13, Status = NodeStatus.Branched },
            new Node { Name = NodeName.I, Path = "ADFI", Cost = 13, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.J, Path = "ADFJ", Cost = 15, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.H, Path = "ACEH", Cost = 16, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.I, Path = "ACEI", Cost = 12, Status = NodeStatus.Branched },
            new Node { Name = NodeName.I, Path = "ADGI", Cost = 16, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.J, Path = "ADGJ", Cost = 12, Status = NodeStatus.Branched },
            new Node { Name = NodeName.K, Path = "ACEIK", Cost = 15, Status = NodeStatus.Record },
            new Node { Name = NodeName.K, Path = "ADGJK", Cost = 17, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.K, Path = "ADFHK", Cost = 19, Status = NodeStatus.ExcludedByTest }
        };

        var algorithm = new BranchAndBoundAlgorithm(matrix);

        // Act
        var actual = algorithm.Calculate();

        // Assert
        CollectionAssert.AreEquivalent(expected, actual);
    }

    [Test]
    public void Calculate_TestCase2_ReturnsCorrectListOfNodes()
    {
        // Arrange
        var matrix = new Matrix(new[,]
        {
            { 0, 3, 9, 2, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 2, 6, 4, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 4, 5, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 8, 6, 0, 0 },
            { 0, 0, 0, 0, 1, 0, 0, 5, 5, 7, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 7, 3, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        });
        var expected = new List<Node>
        {
            new Node { Name = NodeName.A, Path = "A", Cost = 0, Status = NodeStatus.Branched },
            new Node { Name = NodeName.B, Path = "AB", Cost = 3, Status = NodeStatus.Branched },
            new Node { Name = NodeName.C, Path = "AC", Cost = 9, Status = NodeStatus.Branched },
            new Node { Name = NodeName.D, Path = "AD", Cost = 2, Status = NodeStatus.Branched },
            new Node { Name = NodeName.F, Path = "ADF", Cost = 6, Status = NodeStatus.Branched },
            new Node { Name = NodeName.G, Path = "ADG", Cost = 7, Status = NodeStatus.Branched },
            new Node { Name = NodeName.E, Path = "ABE", Cost = 9, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.E, Path = "ADFE", Cost = 7, Status = NodeStatus.Branched },
            new Node { Name = NodeName.H, Path = "ADFH", Cost = 11, Status = NodeStatus.Branched },
            new Node { Name = NodeName.I, Path = "ADFI", Cost = 11, Status = NodeStatus.Branched },
            new Node { Name = NodeName.J, Path = "ADFJ", Cost = 13, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.H, Path = "ADFEH", Cost = 15, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.I, Path = "ADFEI", Cost = 13, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.I, Path = "ADGI", Cost = 14, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.J, Path = "ADGJ", Cost = 10, Status = NodeStatus.Branched },
            new Node { Name = NodeName.E, Path = "ACE", Cost = 11, Status = NodeStatus.Branched },
            new Node { Name = NodeName.F, Path = "ACF", Cost = 15, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.G, Path = "ACG", Cost = 13, Status = NodeStatus.Branched },
            new Node { Name = NodeName.K, Path = "ADGJK", Cost = 15, Status = NodeStatus.Record },
            new Node { Name = NodeName.K, Path = "ADFHK", Cost = 17, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.K, Path = "ADFIK", Cost = 18, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.H, Path = "ACEH", Cost = 19, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.I, Path = "ACEI", Cost = 17, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.I, Path = "ACGI", Cost = 20, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.J, Path = "ACGJ", Cost = 16, Status = NodeStatus.ExcludedByTest }
        };

        var algorithm = new BranchAndBoundAlgorithm(matrix);

        // Act
        var actual = algorithm.Calculate();

        // Assert
        CollectionAssert.AreEquivalent(expected, actual);
    }
    
    [Test]
    public void Calculate_TestCase3_ReturnsCorrectListOfNodes()
    {
        // Arrange
        var matrix = new Matrix(new[,]
        {
            { 0, 8, 2, 4, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 2, 6, 4, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 6, 5, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 8, 4, 0, 0 },
            { 0, 0, 0, 0, 3, 0, 0, 1, 5, 7, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 7, 3, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 4 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        });
        var expected = new List<Node>
        {
            new Node { Name = NodeName.A, Path = "A", Cost = 0, Status = NodeStatus.Branched },
            new Node { Name = NodeName.B, Path = "AB", Cost = 8, Status = NodeStatus.Branched },
            new Node { Name = NodeName.C, Path = "AC", Cost = 2, Status = NodeStatus.Branched },
            new Node { Name = NodeName.D, Path = "AD", Cost = 4, Status = NodeStatus.Branched },
            new Node { Name = NodeName.E, Path = "ACE", Cost = 4, Status = NodeStatus.Branched },
            new Node { Name = NodeName.F, Path = "ACF", Cost = 8, Status = NodeStatus.Branched },
            new Node { Name = NodeName.G, Path = "ACG", Cost = 6, Status = NodeStatus.Branched },
            new Node { Name = NodeName.H, Path = "ACEH", Cost = 12, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.I, Path = "ACEI", Cost = 8, Status = NodeStatus.Branched },
            new Node { Name = NodeName.F, Path = "ADF", Cost = 10, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.G, Path = "ADG", Cost = 9, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.I, Path = "ACGI", Cost = 13, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.J, Path = "ACGJ", Cost = 9, Status = NodeStatus.Branched },
            new Node { Name = NodeName.K, Path = "ACEIK", Cost = 15, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.E, Path = "ACFE", Cost = 11, Status = NodeStatus.Branched },
            new Node { Name = NodeName.H, Path = "ACFH", Cost = 9, Status = NodeStatus.Branched },
            new Node { Name = NodeName.I, Path = "ACFI", Cost = 13, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.J, Path = "ACFJ", Cost = 15, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.E, Path = "ABE", Cost = 14, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.K, Path = "ACGJK", Cost = 14, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.I, Path = "ACFHI", Cost = 12, Status = NodeStatus.Branched },
            new Node { Name = NodeName.K, Path = "ACFHK", Cost = 13, Status = NodeStatus.Record },
            new Node { Name = NodeName.H, Path = "ACFEH", Cost = 19, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.I, Path = "ACFEI", Cost = 15, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.K, Path = "ACFHIK", Cost = 19, Status = NodeStatus.ExcludedByTest }
        };

        var algorithm = new BranchAndBoundAlgorithm(matrix);

        // Act
        var actual = algorithm.Calculate();

        // Assert
        CollectionAssert.AreEquivalent(expected, actual);
    }
    
    [Test]
    public void Calculate_TestCase4_ReturnsCorrectListOfNodes()
    {
        // Arrange
        var matrix = new Matrix(new[,]
        {
            { 0, 2, 6, 5, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 8, 6, 4, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 4, 6, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 8, 4, 0, 0 },
            { 0, 0, 0, 0, 1, 0, 0, 5, 5, 7, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 7, 3, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        });
        var expected = new List<Node>
        {
            new Node { Name = NodeName.A, Path = "A", Cost = 0, Status = NodeStatus.Branched },
            new Node { Name = NodeName.B, Path = "AB", Cost = 2, Status = NodeStatus.Branched },
            new Node { Name = NodeName.C, Path = "AC", Cost = 6, Status = NodeStatus.Branched },
            new Node { Name = NodeName.D, Path = "AD", Cost = 5, Status = NodeStatus.Branched },
            new Node { Name = NodeName.E, Path = "ABE", Cost = 3, Status = NodeStatus.Branched },
            new Node { Name = NodeName.H, Path = "ABEH", Cost = 11, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.I, Path = "ABEI", Cost = 7, Status = NodeStatus.Branched },
            new Node { Name = NodeName.F, Path = "ADF", Cost = 9, Status = NodeStatus.Branched },
            new Node { Name = NodeName.G, Path = "ADG", Cost = 11, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.E, Path = "ACE", Cost = 14, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.F, Path = "ACF", Cost = 12, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.G, Path = "ACG", Cost = 10, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.K, Path = "ABEIK", Cost = 10, Status = NodeStatus.Record },
            new Node { Name = NodeName.E, Path = "ADFE", Cost = 10, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.H, Path = "ADFH", Cost = 14, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.I, Path = "ADFI", Cost = 14, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.J, Path = "ADFJ", Cost = 16, Status = NodeStatus.ExcludedByTest }
        };

        var algorithm = new BranchAndBoundAlgorithm(matrix);

        // Act
        var actual = algorithm.Calculate();

        // Assert
        CollectionAssert.AreEquivalent(expected, actual);
    }
    
    [Test]
    public void Calculate_TestCase5_ReturnsCorrectListOfNodes()
    {
        // Arrange
        var matrix = new Matrix(new[,]
        {
            { 0, 3, 6, 4, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 2, 6, 4, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 4, 5, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 4, 8, 0, 0 },
            { 0, 0, 0, 0, 1, 0, 0, 5, 5, 7, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 7, 3, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 3 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        });
        var expected = new List<Node>
        {
            new Node { Name = NodeName.A, Path = "A", Cost = 0, Status = NodeStatus.Branched },
            new Node { Name = NodeName.B, Path = "AB", Cost = 3, Status = NodeStatus.Branched },
            new Node { Name = NodeName.C, Path = "AC", Cost = 6, Status = NodeStatus.Branched },
            new Node { Name = NodeName.D, Path = "AD", Cost = 4, Status = NodeStatus.Branched },
            new Node { Name = NodeName.E, Path = "ABE", Cost = 7, Status = NodeStatus.Branched },
            new Node { Name = NodeName.F, Path = "ADF", Cost = 8, Status = NodeStatus.Branched },
            new Node { Name = NodeName.G, Path = "ADG", Cost = 9, Status = NodeStatus.Branched },
            new Node { Name = NodeName.E, Path = "ACE", Cost = 8, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.F, Path = "ACF", Cost = 12, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.G, Path = "ACG", Cost = 10, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.H, Path = "ABEH", Cost = 11, Status = NodeStatus.Branched },
            new Node { Name = NodeName.I, Path = "ABEI", Cost = 15, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.E, Path = "ADFE", Cost = 9, Status = NodeStatus.Branched },
            new Node { Name = NodeName.H, Path = "ADFH", Cost = 13, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.I, Path = "ADFI", Cost = 13, Status = NodeStatus.Branched },
            new Node { Name = NodeName.J, Path = "ADFJ", Cost = 15, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.H, Path = "ADFEH", Cost = 13, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.I, Path = "ADFEI", Cost = 17, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.I, Path = "ADGI", Cost = 16, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.J, Path = "ADGJ", Cost = 12, Status = NodeStatus.Branched },
            new Node { Name = NodeName.I, Path = "ABEHI", Cost = 14, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.K, Path = "ABEHK", Cost = 14, Status = NodeStatus.Record },
            new Node { Name = NodeName.K, Path = "ADGJK", Cost = 17, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.K, Path = "ADFIK", Cost = 19, Status = NodeStatus.ExcludedByTest }
        };

        var algorithm = new BranchAndBoundAlgorithm(matrix);

        // Act
        var actual = algorithm.Calculate();

        // Assert
        CollectionAssert.AreEquivalent(expected, actual);
    }
    
    [Test]
    public void Calculate_TestCase6_ReturnsCorrectListOfNodes()
    {
        // Arrange
        var matrix = new Matrix(new[,]
        {
            { 0, 4, 5, 6, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 3, 1, 4, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 4, 5, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 8, 4, 0, 0 },
            { 0, 0, 0, 0, 4, 0, 0, 5, 2, 7, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 7, 3, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        });
        var expected = new List<Node>
        {
            new Node { Name = NodeName.A, Path = "A", Cost = 0, Status = NodeStatus.Branched },
            new Node { Name = NodeName.B, Path = "AB", Cost = 4, Status = NodeStatus.Branched },
            new Node { Name = NodeName.C, Path = "AC", Cost = 5, Status = NodeStatus.Branched },
            new Node { Name = NodeName.D, Path = "AD", Cost = 6, Status = NodeStatus.Branched },
            new Node { Name = NodeName.E, Path = "ABE", Cost = 10, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.E, Path = "ACE", Cost = 8, Status = NodeStatus.Branched },
            new Node { Name = NodeName.F, Path = "ACF", Cost = 6, Status = NodeStatus.Branched },
            new Node { Name = NodeName.G, Path = "ACG", Cost = 9, Status = NodeStatus.Branched },
            new Node { Name = NodeName.E, Path = "ACFE", Cost = 10, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.H, Path = "ACFH", Cost = 11, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.I, Path = "ACFI", Cost = 8, Status = NodeStatus.Branched },
            new Node { Name = NodeName.J, Path = "ACFJ", Cost = 13, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.F, Path = "ADF", Cost = 10, Status = NodeStatus.Branched },
            new Node { Name = NodeName.G, Path = "ADG", Cost = 11, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.K, Path = "ACFIK", Cost = 11, Status = NodeStatus.Record },
            new Node { Name = NodeName.H, Path = "ACEH", Cost = 16, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.I, Path = "ACEI", Cost = 12, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.I, Path = "ACGI", Cost = 16, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.J, Path = "ACGJ", Cost = 12, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.E, Path = "ADFE", Cost = 14, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.H, Path = "ADFH", Cost = 15, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.I, Path = "ADFI", Cost = 12, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.J, Path = "ADFJ", Cost = 17, Status = NodeStatus.ExcludedByTest }
        };

        var algorithm = new BranchAndBoundAlgorithm(matrix);

        // Act
        var actual = algorithm.Calculate();

        // Assert
        CollectionAssert.AreEquivalent(expected, actual);
    }
}