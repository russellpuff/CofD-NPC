namespace CofD_NPC
{
    partial class DiceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dfCoreComboBox = new System.Windows.Forms.ComboBox();
            this.dfAttributes1ComboBox = new System.Windows.Forms.ComboBox();
            this.dfAttributes2ComboBox = new System.Windows.Forms.ComboBox();
            this.dfSkillsComboBox = new System.Windows.Forms.ComboBox();
            this.dfMeritsComboBox = new System.Windows.Forms.ComboBox();
            this.dfModifierNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.dfRollButton = new System.Windows.Forms.Button();
            this.dfRadio8Again = new System.Windows.Forms.RadioButton();
            this.dfRadio9Again = new System.Windows.Forms.RadioButton();
            this.dfRoteCheck = new System.Windows.Forms.CheckBox();
            this.dfRadio10Again = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dfModifierNumUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // dfCoreComboBox
            // 
            this.dfCoreComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dfCoreComboBox.FormattingEnabled = true;
            this.dfCoreComboBox.Items.AddRange(new object[] {
            "(none)",
            "Health",
            "Willpower",
            "Integrity"});
            this.dfCoreComboBox.Location = new System.Drawing.Point(12, 12);
            this.dfCoreComboBox.Name = "dfCoreComboBox";
            this.dfCoreComboBox.Size = new System.Drawing.Size(121, 23);
            this.dfCoreComboBox.TabIndex = 0;
            // 
            // dfAttributes1ComboBox
            // 
            this.dfAttributes1ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dfAttributes1ComboBox.FormattingEnabled = true;
            this.dfAttributes1ComboBox.Items.AddRange(new object[] {
            "(none)",
            "Intelligence",
            "Wits",
            "Resolve",
            "Strength",
            "Dexterity",
            "Stamina",
            "Presence",
            "Manipulation",
            "Composure"});
            this.dfAttributes1ComboBox.Location = new System.Drawing.Point(170, 12);
            this.dfAttributes1ComboBox.Name = "dfAttributes1ComboBox";
            this.dfAttributes1ComboBox.Size = new System.Drawing.Size(121, 23);
            this.dfAttributes1ComboBox.TabIndex = 1;
            // 
            // dfAttributes2ComboBox
            // 
            this.dfAttributes2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dfAttributes2ComboBox.FormattingEnabled = true;
            this.dfAttributes2ComboBox.Items.AddRange(new object[] {
            "(none)",
            "Intelligence",
            "Wits",
            "Resolve",
            "Strength",
            "Dexterity",
            "Stamina",
            "Presence",
            "Manipulation",
            "Composure"});
            this.dfAttributes2ComboBox.Location = new System.Drawing.Point(328, 12);
            this.dfAttributes2ComboBox.Name = "dfAttributes2ComboBox";
            this.dfAttributes2ComboBox.Size = new System.Drawing.Size(121, 23);
            this.dfAttributes2ComboBox.TabIndex = 2;
            // 
            // dfSkillsComboBox
            // 
            this.dfSkillsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dfSkillsComboBox.FormattingEnabled = true;
            this.dfSkillsComboBox.Items.AddRange(new object[] {
            "(none)"});
            this.dfSkillsComboBox.Location = new System.Drawing.Point(12, 53);
            this.dfSkillsComboBox.Name = "dfSkillsComboBox";
            this.dfSkillsComboBox.Size = new System.Drawing.Size(121, 23);
            this.dfSkillsComboBox.TabIndex = 5;
            // 
            // dfMeritsComboBox
            // 
            this.dfMeritsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dfMeritsComboBox.FormattingEnabled = true;
            this.dfMeritsComboBox.Items.AddRange(new object[] {
            "(none)"});
            this.dfMeritsComboBox.Location = new System.Drawing.Point(170, 53);
            this.dfMeritsComboBox.Name = "dfMeritsComboBox";
            this.dfMeritsComboBox.Size = new System.Drawing.Size(121, 23);
            this.dfMeritsComboBox.TabIndex = 6;
            // 
            // dfModifierNumUpDown
            // 
            this.dfModifierNumUpDown.Location = new System.Drawing.Point(328, 53);
            this.dfModifierNumUpDown.Name = "dfModifierNumUpDown";
            this.dfModifierNumUpDown.Size = new System.Drawing.Size(121, 23);
            this.dfModifierNumUpDown.TabIndex = 7;
            // 
            // dfRollButton
            // 
            this.dfRollButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.dfRollButton.Location = new System.Drawing.Point(328, 107);
            this.dfRollButton.Name = "dfRollButton";
            this.dfRollButton.Size = new System.Drawing.Size(121, 44);
            this.dfRollButton.TabIndex = 7;
            this.dfRollButton.Text = "Roll";
            this.dfRollButton.UseVisualStyleBackColor = true;
            this.dfRollButton.Click += new System.EventHandler(this.RollButton_Click);
            // 
            // dfRadio8Again
            // 
            this.dfRadio8Again.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dfRadio8Again.AutoSize = true;
            this.dfRadio8Again.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.dfRadio8Again.Location = new System.Drawing.Point(225, 132);
            this.dfRadio8Again.Name = "dfRadio8Again";
            this.dfRadio8Again.Size = new System.Drawing.Size(65, 19);
            this.dfRadio8Again.TabIndex = 3;
            this.dfRadio8Again.TabStop = true;
            this.dfRadio8Again.Text = "8 Again";
            this.dfRadio8Again.UseVisualStyleBackColor = true;
            // 
            // dfRadio9Again
            // 
            this.dfRadio9Again.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dfRadio9Again.AutoSize = true;
            this.dfRadio9Again.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.dfRadio9Again.Location = new System.Drawing.Point(225, 107);
            this.dfRadio9Again.Name = "dfRadio9Again";
            this.dfRadio9Again.Size = new System.Drawing.Size(65, 19);
            this.dfRadio9Again.TabIndex = 2;
            this.dfRadio9Again.TabStop = true;
            this.dfRadio9Again.Text = "9 Again";
            this.dfRadio9Again.UseVisualStyleBackColor = true;
            // 
            // dfRoteCheck
            // 
            this.dfRoteCheck.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.dfRoteCheck.AutoSize = true;
            this.dfRoteCheck.Location = new System.Drawing.Point(328, 82);
            this.dfRoteCheck.Name = "dfRoteCheck";
            this.dfRoteCheck.Size = new System.Drawing.Size(50, 19);
            this.dfRoteCheck.TabIndex = 6;
            this.dfRoteCheck.Text = "Rote";
            this.dfRoteCheck.UseVisualStyleBackColor = true;
            // 
            // dfRadio10Again
            // 
            this.dfRadio10Again.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dfRadio10Again.AutoSize = true;
            this.dfRadio10Again.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.dfRadio10Again.Location = new System.Drawing.Point(219, 82);
            this.dfRadio10Again.Name = "dfRadio10Again";
            this.dfRadio10Again.Size = new System.Drawing.Size(71, 19);
            this.dfRadio10Again.TabIndex = 1;
            this.dfRadio10Again.TabStop = true;
            this.dfRadio10Again.Text = "10 Again";
            this.dfRadio10Again.UseVisualStyleBackColor = true;
            // 
            // DiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 159);
            this.Controls.Add(this.dfModifierNumUpDown);
            this.Controls.Add(this.dfRadio8Again);
            this.Controls.Add(this.dfRollButton);
            this.Controls.Add(this.dfRadio9Again);
            this.Controls.Add(this.dfMeritsComboBox);
            this.Controls.Add(this.dfRadio10Again);
            this.Controls.Add(this.dfSkillsComboBox);
            this.Controls.Add(this.dfAttributes2ComboBox);
            this.Controls.Add(this.dfRoteCheck);
            this.Controls.Add(this.dfAttributes1ComboBox);
            this.Controls.Add(this.dfCoreComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NPC Dice Roller";
            ((System.ComponentModel.ISupportInitialize)(this.dfModifierNumUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox dfCoreComboBox;
        private ComboBox dfAttributes1ComboBox;
        private ComboBox dfAttributes2ComboBox;
        private ComboBox dfSkillsComboBox;
        private ComboBox dfMeritsComboBox;
        private NumericUpDown dfModifierNumUpDown;
        private Button dfRollButton;
        private RadioButton dfRadio8Again;
        private RadioButton dfRadio9Again;
        private CheckBox dfRoteCheck;
        private RadioButton dfRadio10Again;
    }
}