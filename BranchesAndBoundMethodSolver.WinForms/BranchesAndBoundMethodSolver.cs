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
                MessageBox.Show("Вивід пустий!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}