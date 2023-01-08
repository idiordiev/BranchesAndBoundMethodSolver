using BranchesAndBoundMethodSolver.Logic;

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
            OpenFileDialog openFileDialog = new();
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
                Clipboard.SetText(outputText);
            else
                MessageBox.Show("Вивід пустий!", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            Matrix inputMatrix = MatrixReader.ReadMatrix(inputFilePath);

            IAlgorithm bnbAlgorithm = new BranchAndBoundAlgorithm(inputMatrix);
            
            // Needs update~ Defining last element of sequence to put there \/
            IEnumerable<Node> result = bnbAlgorithm.Calculate(NodeName.K);

            string htmlOutput = HtmlWrapper.Wrapp(result);

            return htmlOutput;
        }
    }
}