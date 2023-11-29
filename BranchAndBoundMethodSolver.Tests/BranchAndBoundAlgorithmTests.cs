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
    public void Calculate_03_04_input_ReturnsCorrectListOfNodes()
    {
        // Arrange
        var matrix = new Matrix(new[,]
        {
            { 0, 3, 6, 2, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 4, 6, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 4, 5, 6, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 8, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 3, 6, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 4, 2, 7, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 3, 6, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        });
        var expected = new List<Node>
        {
            new Node { Name = NodeName.A, Path = "A", Cost = 0, Status = NodeStatus.Branched },
            new Node { Name = NodeName.B, Path = "AB", Cost = 3, Status = NodeStatus.Branched },
            new Node { Name = NodeName.C, Path = "AC", Cost = 6, Status = NodeStatus.Branched },
            new Node { Name = NodeName.D, Path = "AD", Cost = 2, Status = NodeStatus.Branched },
            new Node { Name = NodeName.F, Path = "ADF", Cost = 5, Status = NodeStatus.Branched },
            new Node { Name = NodeName.G, Path = "ADG", Cost = 10, Status = NodeStatus.Branched },
            new Node { Name = NodeName.E, Path = "ABE", Cost = 7, Status = NodeStatus.Branched },
            new Node { Name = NodeName.F, Path = "ABF", Cost = 9, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.H, Path = "ADFH", Cost = 9, Status = NodeStatus.Branched },
            new Node { Name = NodeName.I, Path = "ADFI", Cost = 7, Status = NodeStatus.Branched },
            new Node { Name = NodeName.J, Path = "ADFJ", Cost = 12, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.E, Path = "ACE", Cost = 10, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.F, Path = "ACF", Cost = 11, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.G, Path = "ACG", Cost = 12, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.K, Path = "ADFIK", Cost = 12, Status = NodeStatus.Record },
            new Node { Name = NodeName.H, Path = "ABEH", Cost = 10, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.I, Path = "ABEI", Cost = 13, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.K, Path = "ADFHK", Cost = 12, Status = NodeStatus.Record },
            new Node { Name = NodeName.I, Path = "ADGI", Cost = 13, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.J, Path = "ADGJ", Cost = 16, Status = NodeStatus.ExcludedByTest },
        };

        var algorithm = new BranchAndBoundAlgorithm(matrix);

        // Act
        var actual = algorithm.Calculate();

        // Assert
        CollectionAssert.AreEquivalent(expected, actual);
    }
    
    [Test]
    public void Calculate_testcase_danila_input_ReturnsCorrectListOfNodes()
    {
        // Arrange
        var matrix = new Matrix(new[,]
        {
            { 0, 1, 5, 3, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 6, 7, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 9, 7, 4, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 7, 6, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 8, 0, 5, 0, 6, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 6, 2, 3, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 5, 2, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        });
        var expected = new List<Node>
        {
            new Node { Name = NodeName.A, Path = "A", Cost = 0, Status = NodeStatus.Branched },
            new Node { Name = NodeName.B, Path = "AB", Cost = 1, Status = NodeStatus.Branched },
            new Node { Name = NodeName.C, Path = "AC", Cost = 5, Status = NodeStatus.Branched },
            new Node { Name = NodeName.D, Path = "AD", Cost = 3, Status = NodeStatus.Branched },
            new Node { Name = NodeName.E, Path = "ABE", Cost = 7, Status = NodeStatus.Branched },
            new Node { Name = NodeName.F, Path = "ABF", Cost = 8, Status = NodeStatus.Branched },
            new Node { Name = NodeName.F, Path = "ADF", Cost = 10, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.G, Path = "ADG", Cost = 9, Status = NodeStatus.Branched },
            new Node { Name = NodeName.E, Path = "ACE", Cost = 14, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.F, Path = "ACF", Cost = 12, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.G, Path = "ACG", Cost = 9, Status = NodeStatus.Branched },
            new Node { Name = NodeName.F, Path = "ABEF", Cost = 15, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.H, Path = "ABEH", Cost = 12, Status = NodeStatus.Branched },
            new Node { Name = NodeName.J, Path = "ABEJ", Cost = 13, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.H, Path = "ABFH", Cost = 14, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.I, Path = "ABFI", Cost = 10, Status = NodeStatus.Branched },
            new Node { Name = NodeName.J, Path = "ABFJ", Cost = 11, Status = NodeStatus.Branched },
            new Node { Name = NodeName.I, Path = "ADGI", Cost = 14, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.J, Path = "ADGJ", Cost = 11, Status = NodeStatus.Branched },
            new Node { Name = NodeName.I, Path = "ACGI", Cost = 14, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.J, Path = "ACGJ", Cost = 11, Status = NodeStatus.Branched },
            new Node { Name = NodeName.K, Path = "ABFIK", Cost = 17, Status = NodeStatus.Record },
            new Node { Name = NodeName.K, Path = "ABFJK", Cost = 18, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.K, Path = "ADGJK", Cost = 18, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.K, Path = "ACGJK", Cost = 18, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.K, Path = "ABEHK", Cost = 17, Status = NodeStatus.Record },
        };

        var algorithm = new BranchAndBoundAlgorithm(matrix);

        // Act
        var actual = algorithm.Calculate();

        // Assert
        CollectionAssert.AreEquivalent(expected, actual);
    }
    
    [Test]
    public void Calculate_input_00_lektsia_ReturnsCorrectListOfNodes()
    {
        // Arrange
        var matrix = new Matrix(new[,]
        {
            { 0, 7, 5, 7, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 6, 8, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 7, 6, 7, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 4, 4, 7, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 5, 5, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 6, 3, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 4, 7, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 8, 5, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        });
        var expected = new List<Node>
        {
            new Node { Name = NodeName.A, Path = "A", Cost = 0, Status = NodeStatus.Branched },
            new Node { Name = NodeName.B, Path = "AB", Cost = 7, Status = NodeStatus.Branched },
            new Node { Name = NodeName.C, Path = "AC", Cost = 5, Status = NodeStatus.Branched },
            new Node { Name = NodeName.D, Path = "AD", Cost = 7, Status = NodeStatus.Branched },
            new Node { Name = NodeName.E, Path = "ACE", Cost = 12, Status = NodeStatus.Branched },
            new Node { Name = NodeName.F, Path = "ACF", Cost = 11, Status = NodeStatus.Branched },
            new Node { Name = NodeName.G, Path = "ACG", Cost = 12, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.E, Path = "ABE", Cost = 13, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.F, Path = "ABF", Cost = 15, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.F, Path = "ADF", Cost = 11, Status = NodeStatus.Branched },
            new Node { Name = NodeName.G, Path = "ADG", Cost = 11, Status = NodeStatus.Branched },
            new Node { Name = NodeName.H, Path = "ADH", Cost = 14, Status = NodeStatus.Branched },
            new Node { Name = NodeName.I, Path = "ACFI", Cost = 17, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.J, Path = "ACFJ", Cost = 14, Status = NodeStatus.Branched },
            new Node { Name = NodeName.I, Path = "ADFI", Cost = 17, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.J, Path = "ADFJ", Cost = 14, Status = NodeStatus.Branched },
            new Node { Name = NodeName.I, Path = "ADGI", Cost = 15, Status = NodeStatus.Branched },
            new Node { Name = NodeName.J, Path = "ADGJ", Cost = 18, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.I, Path = "ACEI", Cost = 17, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.J, Path = "ACEJ", Cost = 17, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.K, Path = "ACFJK", Cost = 19, Status = NodeStatus.Record },
            new Node { Name = NodeName.K, Path = "ADFJK", Cost = 19, Status = NodeStatus.Record },
            new Node { Name = NodeName.I, Path = "ADHI", Cost = 22, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.J, Path = "ADHJ", Cost = 19, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.K, Path = "ADGIK", Cost = 23, Status = NodeStatus.ExcludedByTest },
        };

        var algorithm = new BranchAndBoundAlgorithm(matrix);

        // Act
        var actual = algorithm.Calculate();

        // Assert
        CollectionAssert.AreEquivalent(expected, actual);
    }

    [Test]
    public void Calculate_input_00_lektsia_Example2_ReturnsCorrectListOfNodes()
    {
        // Arrange
        var matrix = new Matrix(new[,]
        {
            { 0, 5, 2, 6, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 2, 5, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 4, 0, 6, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 6, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 8, 0, 7, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 7, 0, 2, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 7, 2, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        });
        var expected = new List<Node>
        {
            new Node { Name = NodeName.A, Path = "A", Cost = 0, Status = NodeStatus.Branched },
            new Node { Name = NodeName.B, Path = "AB", Cost = 5, Status = NodeStatus.Branched },
            new Node { Name = NodeName.C, Path = "AC", Cost = 2, Status = NodeStatus.Branched },
            new Node { Name = NodeName.D, Path = "AD", Cost = 6, Status = NodeStatus.Branched },
            new Node { Name = NodeName.E, Path = "ACE", Cost = 6, Status = NodeStatus.Branched },
            new Node { Name = NodeName.G, Path = "ACG", Cost = 8, Status = NodeStatus.Branched },
            new Node { Name = NodeName.E, Path = "ABE", Cost = 7, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.F, Path = "ABF", Cost = 10, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.H, Path = "ACEH", Cost = 14, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.J, Path = "ACEJ", Cost = 13, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.F, Path = "ADF", Cost = 7, Status = NodeStatus.Branched },
            new Node { Name = NodeName.G, Path = "ADG", Cost = 12, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.H, Path = "ADFH", Cost = 14, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.J, Path = "ADFJ", Cost = 9, Status = NodeStatus.Branched },
            new Node { Name = NodeName.I, Path = "ACGI", Cost = 15, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.J, Path = "ACGJ", Cost = 10, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.K, Path = "ADFJK", Cost = 13, Status = NodeStatus.Record },
        };

        var algorithm = new BranchAndBoundAlgorithm(matrix);

        // Act
        var actual = algorithm.Calculate();

        // Assert
        CollectionAssert.AreEquivalent(expected, actual);
    }

    [Test]
    public void Calculate_input_02_01_ReturnsCorrectListOfNodes()
    {
        // Arrange
        var matrix = new Matrix(new[,]
        {
            { 0, 4, 3, 6, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 6, 5, 0, 0, 0, 0, 0 },
            { 0, 6, 0, 4, 0, 8, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 7, 5, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 8, 3, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 9, 4, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 5, 4, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 5 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 5 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        });
        var expected = new List<Node>
        {
            new Node { Name = NodeName.A, Path = "A", Cost = 0, Status = NodeStatus.Branched },
            new Node { Name = NodeName.B, Path = "AB", Cost = 4, Status = NodeStatus.Branched },
            new Node { Name = NodeName.C, Path = "AC", Cost = 3, Status = NodeStatus.Branched },
            new Node { Name = NodeName.D, Path = "AD", Cost = 6, Status = NodeStatus.Branched },
            new Node { Name = NodeName.B, Path = "ACB", Cost = 9, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.D, Path = "ACD", Cost = 7, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.F, Path = "ACF", Cost = 11, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.E, Path = "ABE", Cost = 10, Status = NodeStatus.Branched },
            new Node { Name = NodeName.F, Path = "ABF", Cost = 9, Status = NodeStatus.Branched },
            new Node { Name = NodeName.F, Path = "ADF", Cost = 13, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.G, Path = "ADG", Cost = 11, Status = NodeStatus.Branched },
            new Node { Name = NodeName.I, Path = "ABFI", Cost = 18, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.J, Path = "ABFJ", Cost = 13, Status = NodeStatus.Branched },
            new Node { Name = NodeName.H, Path = "ABEH", Cost = 18, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.I, Path = "ABEI", Cost = 13, Status = NodeStatus.Branched },
            new Node { Name = NodeName.I, Path = "ADGI", Cost = 16, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.J, Path = "ADGJ", Cost = 15, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.I, Path = "ABFJI", Cost = 19, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.K, Path = "ABFJK", Cost = 18, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.K, Path = "ABEIK", Cost = 16, Status = NodeStatus.Record },
        };

        var algorithm = new BranchAndBoundAlgorithm(matrix);

        // Act
        var actual = algorithm.Calculate();

        // Assert
        CollectionAssert.AreEquivalent(expected, actual);
    }
    
    [Test]
    public void Calculate_input_04_01_ReturnsCorrectListOfNodes()
    {
        // Arrange
        var matrix = new Matrix(new[,]
        {
            { 0, 3, 4, 2, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 3, 4, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 6, 7, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 3, 6, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 8, 2, 7, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 6, 0, 2, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 1, 0, 5 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 9 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        });
        var expected = new List<Node>
        {
            new Node { Name = NodeName.A, Path = "A", Cost = 0, Status = NodeStatus.Branched },
            new Node { Name = NodeName.B, Path = "AB", Cost = 3, Status = NodeStatus.Branched },
            new Node { Name = NodeName.C, Path = "AC", Cost = 4, Status = NodeStatus.Branched },
            new Node { Name = NodeName.D, Path = "AD", Cost = 2, Status = NodeStatus.Branched },
            new Node { Name = NodeName.E, Path = "ADE", Cost = 5, Status = NodeStatus.Branched },
            new Node { Name = NodeName.F, Path = "ADF", Cost = 8, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.E, Path = "ABE", Cost = 6, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.F, Path = "ABF", Cost = 7, Status = NodeStatus.Branched },
            new Node { Name = NodeName.E, Path = "ACE", Cost = 10, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.F, Path = "ACF", Cost = 11, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.F, Path = "ADEF", Cost = 13, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.G, Path = "ADEG", Cost = 7, Status = NodeStatus.Branched },
            new Node { Name = NodeName.H, Path = "ADEH", Cost = 12, Status = NodeStatus.ExcludedByVD },
            new Node { Name = NodeName.H, Path = "ADEGH", Cost = 8, Status = NodeStatus.Branched },
            new Node { Name = NodeName.J, Path = "ADEGJ", Cost = 12, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.G, Path = "ABFG", Cost = 13, Status = NodeStatus.ExcludedByTest },
            new Node { Name = NodeName.I, Path = "ABFI", Cost = 9, Status = NodeStatus.Branched },
            new Node { Name = NodeName.J, Path = "ADEGHJ", Cost = 11, Status = NodeStatus.Record },
            new Node { Name = NodeName.J, Path = "ABFIJ", Cost = 18, Status = NodeStatus.ExcludedByTest },
        };

        var algorithm = new BranchAndBoundAlgorithm(matrix);

        // Act
        var actual = algorithm.Calculate();

        // Assert
        CollectionAssert.AreEquivalent(expected, actual);
    }
}