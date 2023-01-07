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

        private void ProceedButton_Click(object sender, EventArgs e)
        {
            string inputFilePath = PathString.Text;

            if (!string.IsNullOrEmpty(inputFilePath))
            {
                Matrix inputMatrix = MatrixReader.ReadMatrix(inputFilePath);
                IAlgorithm bnbAlgorithm = new BranchAndBoundAlgorithm(inputMatrix);

                // Needs update~ Defining last element of sequence to put there \/
                IEnumerable<Node> result = bnbAlgorithm.Calculate(NodeName.K);

                string htmlOutput = HtmlWrapper.Wrapp(result);

                HtmlOutput.Text = htmlOutput;
            }
            else
            {
                MessageBox.Show("Шлях не задано!", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}