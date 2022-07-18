namespace CofD_NPC
{
    partial class CoreForm
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
            this.cfTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.cfDeleteNPCButton = new System.Windows.Forms.Button();
            this.cfDataGrid = new System.Windows.Forms.DataGridView();
            this.cfNPCNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfNPCDescriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfNPCIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cfNewNPCButton = new System.Windows.Forms.Button();
            this.cfRollButton = new System.Windows.Forms.Button();
            this.cfRadio8Again = new System.Windows.Forms.RadioButton();
            this.cfRadio9Again = new System.Windows.Forms.RadioButton();
            this.cfRoteCheck = new System.Windows.Forms.CheckBox();
            this.cfDiceNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.cfRadio10Again = new System.Windows.Forms.RadioButton();
            this.cfTableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfDiceNumUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // cfTableLayout
            // 
            this.cfTableLayout.ColumnCount = 2;
            this.cfTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.cfTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.cfTableLayout.Controls.Add(this.cfDeleteNPCButton, 1, 1);
            this.cfTableLayout.Controls.Add(this.cfDataGrid, 0, 0);
            this.cfTableLayout.Controls.Add(this.cfNewNPCButton, 0, 1);
            this.cfTableLayout.Controls.Add(this.cfRollButton, 0, 5);
            this.cfTableLayout.Controls.Add(this.cfRadio8Again, 1, 5);
            this.cfTableLayout.Controls.Add(this.cfRadio9Again, 1, 4);
            this.cfTableLayout.Controls.Add(this.cfRoteCheck, 0, 4);
            this.cfTableLayout.Controls.Add(this.cfDiceNumUpDown, 0, 3);
            this.cfTableLayout.Controls.Add(this.cfRadio10Again, 1, 3);
            this.cfTableLayout.Location = new System.Drawing.Point(2, 3);
            this.cfTableLayout.Name = "cfTableLayout";
            this.cfTableLayout.RowCount = 6;
            this.cfTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.cfTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.cfTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.cfTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.cfTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.cfTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.cfTableLayout.Size = new System.Drawing.Size(230, 472);
            this.cfTableLayout.TabIndex = 0;
            // 
            // cfDeleteNPCButton
            // 
            this.cfDeleteNPCButton.Location = new System.Drawing.Point(118, 353);
            this.cfDeleteNPCButton.Name = "cfDeleteNPCButton";
            this.cfDeleteNPCButton.Size = new System.Drawing.Size(109, 23);
            this.cfDeleteNPCButton.TabIndex = 9;
            this.cfDeleteNPCButton.Text = "Delete NPC";
            this.cfDeleteNPCButton.UseVisualStyleBackColor = true;
            this.cfDeleteNPCButton.Click += new System.EventHandler(this.DeleteNPCButton_Click);
            // 
            // cfDataGrid
            // 
            this.cfDataGrid.AllowUserToAddRows = false;
            this.cfDataGrid.AllowUserToDeleteRows = false;
            this.cfDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cfDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cfDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cfNPCNameColumn,
            this.cfNPCDescriptionColumn,
            this.cfNPCIDColumn});
            this.cfTableLayout.SetColumnSpan(this.cfDataGrid, 2);
            this.cfDataGrid.Location = new System.Drawing.Point(3, 3);
            this.cfDataGrid.MultiSelect = false;
            this.cfDataGrid.Name = "cfDataGrid";
            this.cfDataGrid.ReadOnly = true;
            this.cfDataGrid.RowHeadersVisible = false;
            this.cfDataGrid.RowTemplate.Height = 25;
            this.cfDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.cfDataGrid.Size = new System.Drawing.Size(224, 344);
            this.cfDataGrid.TabIndex = 0;
            this.cfDataGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGrid_CellDoubleClick);
            // 
            // cfNPCNameColumn
            // 
            this.cfNPCNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cfNPCNameColumn.HeaderText = "Name";
            this.cfNPCNameColumn.Name = "cfNPCNameColumn";
            this.cfNPCNameColumn.ReadOnly = true;
            this.cfNPCNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // cfNPCDescriptionColumn
            // 
            this.cfNPCDescriptionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.cfNPCDescriptionColumn.HeaderText = "Description";
            this.cfNPCDescriptionColumn.Name = "cfNPCDescriptionColumn";
            this.cfNPCDescriptionColumn.ReadOnly = true;
            this.cfNPCDescriptionColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cfNPCDescriptionColumn.Width = 92;
            // 
            // cfNPCIDColumn
            // 
            this.cfNPCIDColumn.HeaderText = "ID";
            this.cfNPCIDColumn.Name = "cfNPCIDColumn";
            this.cfNPCIDColumn.ReadOnly = true;
            this.cfNPCIDColumn.Visible = false;
            // 
            // cfNewNPCButton
            // 
            this.cfNewNPCButton.Location = new System.Drawing.Point(3, 353);
            this.cfNewNPCButton.Name = "cfNewNPCButton";
            this.cfNewNPCButton.Size = new System.Drawing.Size(109, 23);
            this.cfNewNPCButton.TabIndex = 8;
            this.cfNewNPCButton.Text = "New NPC";
            this.cfNewNPCButton.UseVisualStyleBackColor = true;
            this.cfNewNPCButton.Click += new System.EventHandler(this.NewNPCButton_Click);
            // 
            // cfRollButton
            // 
            this.cfRollButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cfRollButton.Location = new System.Drawing.Point(42, 446);
            this.cfRollButton.Name = "cfRollButton";
            this.cfRollButton.Size = new System.Drawing.Size(70, 23);
            this.cfRollButton.TabIndex = 7;
            this.cfRollButton.Text = "Roll";
            this.cfRollButton.UseVisualStyleBackColor = true;
            this.cfRollButton.Click += new System.EventHandler(this.RollButton_Click);
            // 
            // cfRadio8Again
            // 
            this.cfRadio8Again.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cfRadio8Again.AutoSize = true;
            this.cfRadio8Again.Location = new System.Drawing.Point(118, 448);
            this.cfRadio8Again.Name = "cfRadio8Again";
            this.cfRadio8Again.Size = new System.Drawing.Size(65, 19);
            this.cfRadio8Again.TabIndex = 3;
            this.cfRadio8Again.TabStop = true;
            this.cfRadio8Again.Text = "8 Again";
            this.cfRadio8Again.UseVisualStyleBackColor = true;
            // 
            // cfRadio9Again
            // 
            this.cfRadio9Again.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cfRadio9Again.AutoSize = true;
            this.cfRadio9Again.Location = new System.Drawing.Point(118, 419);
            this.cfRadio9Again.Name = "cfRadio9Again";
            this.cfRadio9Again.Size = new System.Drawing.Size(65, 19);
            this.cfRadio9Again.TabIndex = 2;
            this.cfRadio9Again.TabStop = true;
            this.cfRadio9Again.Text = "9 Again";
            this.cfRadio9Again.UseVisualStyleBackColor = true;
            // 
            // cfRoteCheck
            // 
            this.cfRoteCheck.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cfRoteCheck.AutoSize = true;
            this.cfRoteCheck.Location = new System.Drawing.Point(62, 419);
            this.cfRoteCheck.Name = "cfRoteCheck";
            this.cfRoteCheck.Size = new System.Drawing.Size(50, 19);
            this.cfRoteCheck.TabIndex = 6;
            this.cfRoteCheck.Text = "Rote";
            this.cfRoteCheck.UseVisualStyleBackColor = true;
            // 
            // cfDiceNumUpDown
            // 
            this.cfDiceNumUpDown.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cfDiceNumUpDown.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cfDiceNumUpDown.Location = new System.Drawing.Point(42, 388);
            this.cfDiceNumUpDown.Name = "cfDiceNumUpDown";
            this.cfDiceNumUpDown.Size = new System.Drawing.Size(70, 23);
            this.cfDiceNumUpDown.TabIndex = 4;
            // 
            // cfRadio10Again
            // 
            this.cfRadio10Again.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cfRadio10Again.AutoSize = true;
            this.cfRadio10Again.Location = new System.Drawing.Point(118, 390);
            this.cfRadio10Again.Name = "cfRadio10Again";
            this.cfRadio10Again.Size = new System.Drawing.Size(71, 19);
            this.cfRadio10Again.TabIndex = 1;
            this.cfRadio10Again.TabStop = true;
            this.cfRadio10Again.Text = "10 Again";
            this.cfRadio10Again.UseVisualStyleBackColor = true;
            // 
            // CoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 478);
            this.Controls.Add(this.cfTableLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CoreForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CofD NPC";
            this.cfTableLayout.ResumeLayout(false);
            this.cfTableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfDiceNumUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel cfTableLayout;
        private DataGridView cfDataGrid;
        private RadioButton cfRadio10Again;
        private RadioButton cfRadio9Again;
        private RadioButton cfRadio8Again;
        private NumericUpDown cfDiceNumUpDown;
        private Button cfDeleteNPCButton;
        private CheckBox cfRoteCheck;
        private Button cfRollButton;
        private Button cfNewNPCButton;
        private DataGridViewTextBoxColumn cfNPCNameColumn;
        private DataGridViewTextBoxColumn cfNPCDescriptionColumn;
        private DataGridViewTextBoxColumn cfNPCIDColumn;
    }
}