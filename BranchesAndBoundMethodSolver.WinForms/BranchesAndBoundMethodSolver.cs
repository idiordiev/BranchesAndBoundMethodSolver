using System.IO.IsolatedStorage;
using BranchesAndBoundMethodSolver.Logic;
using BranchesAndBoundMethodSolver.Logic.Enums;
using BranchesAndBoundMethodSolver.Logic.Interfaces;
using BranchesAndBoundMethodSolver.Logic.Models;
using BranchesAndBoundMethodSolver.WinForms.Helpers;

namespace BranchesAndBoundMethodSolver.WinForms
{
    public partial class BranchesAndBoundMethodSolver : Form
    {
        public BranchesAndBoundMethodSolver()
        {
            InitializeComponent();
        }

        private void SetFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                PathString.Text = openFileDialog.FileName;
            }
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            string outputText = HtmlOutput.Text;

            if (!string.IsNullOrEmpty(outputText))
            {
                Clipboard.SetText(outputText);
            }
            else
            {
                MessageBox.Show("Вивід пустий!", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ProceedButton_ClickAsync(object sender, EventArgs e)
        {
            string inputFilePath = PathString.Text;

            if (!string.IsNullOrEmpty(inputFilePath))
            {
                Task<string> task = Task.Run(() => ProceedOperation(inputFilePath));

                if (!task.IsCompleted)
                {
                    HtmlOutput.Text = "Будь ласка, зачекайте";
                }

                await task;

                HtmlOutput.Text = task.Result;
                CopyButton.Enabled = true;
            }
            else
            {
                MessageBox.Show("Шлях не задано!", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ProceedOperation(string inputFilePath)
        {
            try
            {
                Matrix inputMatrix = MatrixReader.ReadMatrix(inputFilePath);

                IAlgorithm bnbAlgorithm = new BranchAndBoundAlgorithm(inputMatrix);

                IEnumerable<Node> result = bnbAlgorithm.Calculate()
                    .OrderBy(n => n.Path.Length)
                    .ThenBy(n => n.Path);

                SaveNodesToFile(result);

                string htmlOutput = HtmlWrapper.Wrapp(result);

                var path = "";
                var pathValue = "";

                foreach (Node? node in result)
                {
                    if (node.Status == NodeStatus.Record)
                    {
                        path += $"{node.Path} ";
                        pathValue = $"{node.Cost} ";
                    }
                }

                ResultPath.Invoke((MethodInvoker)(() => ResultPath.Text = path));
                ResultPathValue.Invoke((MethodInvoker)(() => ResultPathValue.Text = pathValue));

                return htmlOutput;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        private void CleanButton_Click(object sender, EventArgs e)
        {
            ResultPath.Text = "";
            ResultPathValue.Text = "";
            HtmlOutput.Text = "";
            PathString.Text = "";
            CopyButton.Enabled = false;
        }

        private void SaveNodesToFile(IEnumerable<Node> nodes)
        {
            string folderPath = Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory) + "/logs";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string saveFilePath = Path.Combine(folderPath, $"logs_{DateTime.UtcNow.ToString("yyyyMMddHHmmss")}.txt");
            StreamWriter w = new StreamWriter(saveFilePath, true);

            foreach (var node in nodes)
                w.WriteLine($"Name: {node.Name}  Path: {node.Path}  Cost: {node.Cost}  Status: {node.Status}");

            w.Close();
        }
    }
}