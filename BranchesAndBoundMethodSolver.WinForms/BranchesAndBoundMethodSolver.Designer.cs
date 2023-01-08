namespace BranchesAndBoundMethodSolver.WinForms
{
    partial class BranchesAndBoundMethodSolver
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BranchesAndBoundMethodSolver));
            this.SelectFileButton = new System.Windows.Forms.Button();
            this.HtmlOutput = new System.Windows.Forms.RichTextBox();
            this.FilePath = new System.Windows.Forms.Label();
            this.PathString = new System.Windows.Forms.TextBox();
            this.ProceedButton = new System.Windows.Forms.Button();
            this.HtmlOutputLabel = new System.Windows.Forms.Label();
            this.CopyButton = new System.Windows.Forms.Button();
            this.resultLabel1 = new System.Windows.Forms.Label();
            this.Path = new System.Windows.Forms.Label();
            this.resultLabel2 = new System.Windows.Forms.Label();
            this.PathValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SelectFileButton
            // 
            this.SelectFileButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SelectFileButton.Location = new System.Drawing.Point(472, 275);
            this.SelectFileButton.Name = "SelectFileButton";
            this.SelectFileButton.Size = new System.Drawing.Size(75, 40);
            this.SelectFileButton.TabIndex = 0;
            this.SelectFileButton.Text = "Вибрати файл";
            this.SelectFileButton.UseVisualStyleBackColor = true;
            this.SelectFileButton.Click += new System.EventHandler(this.SetFileButton_Click);
            // 
            // HtmlOutput
            // 
            this.HtmlOutput.Location = new System.Drawing.Point(12, 35);
            this.HtmlOutput.Name = "HtmlOutput";
            this.HtmlOutput.ReadOnly = true;
            this.HtmlOutput.Size = new System.Drawing.Size(424, 187);
            this.HtmlOutput.TabIndex = 1;
            this.HtmlOutput.Text = "";
            // 
            // FilePath
            // 
            this.FilePath.AutoSize = true;
            this.FilePath.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FilePath.Location = new System.Drawing.Point(11, 288);
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(81, 15);
            this.FilePath.TabIndex = 2;
            this.FilePath.Text = "Шлях файлу :";
            // 
            // PathString
            // 
            this.PathString.Location = new System.Drawing.Point(89, 285);
            this.PathString.Name = "PathString";
            this.PathString.Size = new System.Drawing.Size(377, 23);
            this.PathString.TabIndex = 3;
            // 
            // ProceedButton
            // 
            this.ProceedButton.BackColor = System.Drawing.Color.Transparent;
            this.ProceedButton.FlatAppearance.BorderSize = 0;
            this.ProceedButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ProceedButton.ForeColor = System.Drawing.Color.DarkGreen;
            this.ProceedButton.Location = new System.Drawing.Point(458, 150);
            this.ProceedButton.Name = "ProceedButton";
            this.ProceedButton.Size = new System.Drawing.Size(89, 33);
            this.ProceedButton.TabIndex = 4;
            this.ProceedButton.Text = "Виконати";
            this.ProceedButton.UseVisualStyleBackColor = false;
            this.ProceedButton.Click += new System.EventHandler(this.ProceedButton_ClickAsync);
            // 
            // HtmlOutputLabel
            // 
            this.HtmlOutputLabel.AutoSize = true;
            this.HtmlOutputLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.HtmlOutputLabel.Location = new System.Drawing.Point(11, 15);
            this.HtmlOutputLabel.Name = "HtmlOutputLabel";
            this.HtmlOutputLabel.Size = new System.Drawing.Size(77, 17);
            this.HtmlOutputLabel.TabIndex = 5;
            this.HtmlOutputLabel.Text = "HTML вивід";
            // 
            // CopyButton
            // 
            this.CopyButton.Enabled = false;
            this.CopyButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CopyButton.Location = new System.Drawing.Point(442, 189);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(116, 33);
            this.CopyButton.TabIndex = 6;
            this.CopyButton.Text = "Скопіювати";
            this.CopyButton.UseVisualStyleBackColor = true;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // resultLabel1
            // 
            this.resultLabel1.AutoSize = true;
            this.resultLabel1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.resultLabel1.Location = new System.Drawing.Point(12, 238);
            this.resultLabel1.Name = "resultLabel1";
            this.resultLabel1.Size = new System.Drawing.Size(49, 19);
            this.resultLabel1.TabIndex = 7;
            this.resultLabel1.Text = "Шлях :";
            // 
            // Path
            // 
            this.Path.AutoSize = true;
            this.Path.Location = new System.Drawing.Point(67, 242);
            this.Path.Name = "Path";
            this.Path.Size = new System.Drawing.Size(0, 15);
            this.Path.TabIndex = 8;
            // 
            // resultLabel2
            // 
            this.resultLabel2.AutoSize = true;
            this.resultLabel2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.resultLabel2.Location = new System.Drawing.Point(306, 238);
            this.resultLabel2.Name = "resultLabel2";
            this.resultLabel2.Size = new System.Drawing.Size(109, 19);
            this.resultLabel2.TabIndex = 9;
            this.resultLabel2.Text = "Вартість шляху :";
            // 
            // PathValue
            // 
            this.PathValue.AutoSize = true;
            this.PathValue.Location = new System.Drawing.Point(417, 240);
            this.PathValue.Name = "PathValue";
            this.PathValue.Size = new System.Drawing.Size(0, 15);
            this.PathValue.TabIndex = 10;
            // 
            // BranchesAndBoundMethodSolver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 327);
            this.Controls.Add(this.PathValue);
            this.Controls.Add(this.resultLabel2);
            this.Controls.Add(this.Path);
            this.Controls.Add(this.resultLabel1);
            this.Controls.Add(this.CopyButton);
            this.Controls.Add(this.HtmlOutputLabel);
            this.Controls.Add(this.ProceedButton);
            this.Controls.Add(this.PathString);
            this.Controls.Add(this.FilePath);
            this.Controls.Add(this.HtmlOutput);
            this.Controls.Add(this.SelectFileButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BranchesAndBoundMethodSolver";
            this.Text = "Метод меж та гілок";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button SelectFileButton;
        private RichTextBox HtmlOutput;
        private Label FilePath;
        private TextBox PathString;
        private Button ProceedButton;
        private Label HtmlOutputLabel;
        private Button CopyButton;
        private Label resultLabel1;
        private Label Path;
        private Label resultLabel2;
        private Label PathValue;
    }
}