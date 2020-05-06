namespace TrackerUI
{
    partial class CreateTournamentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateTournamentForm));
            this.headerLable = new System.Windows.Forms.Label();
            this.tournamentNameLabel = new System.Windows.Forms.Label();
            this.tournamentNameValue = new System.Windows.Forms.TextBox();
            this.entryFeeLable = new System.Windows.Forms.Label();
            this.entryFeeValue = new System.Windows.Forms.TextBox();
            this.selectTeamLabel = new System.Windows.Forms.Label();
            this.selectTeamDropDown = new System.Windows.Forms.ComboBox();
            this.addTeamButton = new System.Windows.Forms.Button();
            this.createPrizeButton = new System.Windows.Forms.Button();
            this.tournamentPlayersLabel = new System.Windows.Forms.Label();
            this.tournamentPrizesLable = new System.Windows.Forms.Label();
            this.tournamentPlayersListBox = new System.Windows.Forms.ListBox();
            this.tournamentPrizesListBox = new System.Windows.Forms.ListBox();
            this.createNewTeamLink = new System.Windows.Forms.LinkLabel();
            this.deleteSelectedTeamButton = new System.Windows.Forms.Button();
            this.deleteSelectedPrizeButton = new System.Windows.Forms.Button();
            this.createTournamentButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // headerLable
            // 
            this.headerLable.AutoSize = true;
            this.headerLable.Font = new System.Drawing.Font("Segoe UI Light", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerLable.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.headerLable.Location = new System.Drawing.Point(12, 9);
            this.headerLable.Name = "headerLable";
            this.headerLable.Size = new System.Drawing.Size(317, 50);
            this.headerLable.TabIndex = 1;
            this.headerLable.Text = "Create Tournament";
            // 
            // tournamentNameLabel
            // 
            this.tournamentNameLabel.AutoSize = true;
            this.tournamentNameLabel.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tournamentNameLabel.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.tournamentNameLabel.Location = new System.Drawing.Point(19, 59);
            this.tournamentNameLabel.Name = "tournamentNameLabel";
            this.tournamentNameLabel.Size = new System.Drawing.Size(236, 37);
            this.tournamentNameLabel.TabIndex = 3;
            this.tournamentNameLabel.Text = "Tournament Name";
            // 
            // tournamentNameValue
            // 
            this.tournamentNameValue.Location = new System.Drawing.Point(26, 99);
            this.tournamentNameValue.Name = "tournamentNameValue";
            this.tournamentNameValue.Size = new System.Drawing.Size(352, 43);
            this.tournamentNameValue.TabIndex = 9;
            // 
            // entryFeeLable
            // 
            this.entryFeeLable.AutoSize = true;
            this.entryFeeLable.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entryFeeLable.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.entryFeeLable.Location = new System.Drawing.Point(19, 161);
            this.entryFeeLable.Name = "entryFeeLable";
            this.entryFeeLable.Size = new System.Drawing.Size(125, 37);
            this.entryFeeLable.TabIndex = 10;
            this.entryFeeLable.Text = "Entry Fee";
            // 
            // entryFeeValue
            // 
            this.entryFeeValue.Location = new System.Drawing.Point(158, 158);
            this.entryFeeValue.Name = "entryFeeValue";
            this.entryFeeValue.Size = new System.Drawing.Size(117, 43);
            this.entryFeeValue.TabIndex = 11;
            this.entryFeeValue.Text = "0";
            // 
            // selectTeamLabel
            // 
            this.selectTeamLabel.AutoSize = true;
            this.selectTeamLabel.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectTeamLabel.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.selectTeamLabel.Location = new System.Drawing.Point(19, 228);
            this.selectTeamLabel.Name = "selectTeamLabel";
            this.selectTeamLabel.Size = new System.Drawing.Size(156, 37);
            this.selectTeamLabel.TabIndex = 12;
            this.selectTeamLabel.Text = "Select Team";
            // 
            // selectTeamDropDown
            // 
            this.selectTeamDropDown.BackColor = System.Drawing.SystemColors.ControlDark;
            this.selectTeamDropDown.FormattingEnabled = true;
            this.selectTeamDropDown.Location = new System.Drawing.Point(26, 269);
            this.selectTeamDropDown.Name = "selectTeamDropDown";
            this.selectTeamDropDown.Size = new System.Drawing.Size(352, 45);
            this.selectTeamDropDown.TabIndex = 13;
            // 
            // addTeamButton
            // 
            this.addTeamButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.addTeamButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.addTeamButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.addTeamButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.addTeamButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addTeamButton.Location = new System.Drawing.Point(115, 333);
            this.addTeamButton.Name = "addTeamButton";
            this.addTeamButton.Size = new System.Drawing.Size(175, 53);
            this.addTeamButton.TabIndex = 14;
            this.addTeamButton.Text = "Add Team";
            this.addTeamButton.UseVisualStyleBackColor = false;
            // 
            // createPrizeButton
            // 
            this.createPrizeButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.createPrizeButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.createPrizeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.createPrizeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.createPrizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createPrizeButton.Location = new System.Drawing.Point(115, 414);
            this.createPrizeButton.Name = "createPrizeButton";
            this.createPrizeButton.Size = new System.Drawing.Size(175, 53);
            this.createPrizeButton.TabIndex = 15;
            this.createPrizeButton.Text = "Create Prize";
            this.createPrizeButton.UseVisualStyleBackColor = false;
            // 
            // tournamentPlayersLabel
            // 
            this.tournamentPlayersLabel.AutoSize = true;
            this.tournamentPlayersLabel.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tournamentPlayersLabel.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.tournamentPlayersLabel.Location = new System.Drawing.Point(457, 59);
            this.tournamentPlayersLabel.Name = "tournamentPlayersLabel";
            this.tournamentPlayersLabel.Size = new System.Drawing.Size(198, 37);
            this.tournamentPlayersLabel.TabIndex = 16;
            this.tournamentPlayersLabel.Text = "Teams / Players";
            // 
            // tournamentPrizesLable
            // 
            this.tournamentPrizesLable.AutoSize = true;
            this.tournamentPrizesLable.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tournamentPrizesLable.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.tournamentPrizesLable.Location = new System.Drawing.Point(457, 277);
            this.tournamentPrizesLable.Name = "tournamentPrizesLable";
            this.tournamentPrizesLable.Size = new System.Drawing.Size(85, 37);
            this.tournamentPrizesLable.TabIndex = 17;
            this.tournamentPrizesLable.Text = "Prizes";
            // 
            // tournamentPlayersListBox
            // 
            this.tournamentPlayersListBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tournamentPlayersListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tournamentPlayersListBox.FormattingEnabled = true;
            this.tournamentPlayersListBox.ItemHeight = 37;
            this.tournamentPlayersListBox.Location = new System.Drawing.Point(464, 99);
            this.tournamentPlayersListBox.Name = "tournamentPlayersListBox";
            this.tournamentPlayersListBox.Size = new System.Drawing.Size(334, 150);
            this.tournamentPlayersListBox.TabIndex = 18;
            // 
            // tournamentPrizesListBox
            // 
            this.tournamentPrizesListBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tournamentPrizesListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tournamentPrizesListBox.FormattingEnabled = true;
            this.tournamentPrizesListBox.ItemHeight = 37;
            this.tournamentPrizesListBox.Location = new System.Drawing.Point(464, 312);
            this.tournamentPrizesListBox.Name = "tournamentPrizesListBox";
            this.tournamentPrizesListBox.Size = new System.Drawing.Size(334, 150);
            this.tournamentPrizesListBox.TabIndex = 19;
            this.tournamentPrizesListBox.SelectedIndexChanged += new System.EventHandler(this.tournamentPrizesListBox_SelectedIndexChanged);
            // 
            // createNewTeamLink
            // 
            this.createNewTeamLink.AutoSize = true;
            this.createNewTeamLink.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createNewTeamLink.Location = new System.Drawing.Point(264, 234);
            this.createNewTeamLink.Name = "createNewTeamLink";
            this.createNewTeamLink.Size = new System.Drawing.Size(114, 30);
            this.createNewTeamLink.TabIndex = 20;
            this.createNewTeamLink.TabStop = true;
            this.createNewTeamLink.Text = "create new";
            // 
            // deleteSelectedTeamButton
            // 
            this.deleteSelectedTeamButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.deleteSelectedTeamButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.deleteSelectedTeamButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.deleteSelectedTeamButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.deleteSelectedTeamButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteSelectedTeamButton.Location = new System.Drawing.Point(804, 136);
            this.deleteSelectedTeamButton.Name = "deleteSelectedTeamButton";
            this.deleteSelectedTeamButton.Size = new System.Drawing.Size(131, 86);
            this.deleteSelectedTeamButton.TabIndex = 21;
            this.deleteSelectedTeamButton.Text = "Delete Selected";
            this.deleteSelectedTeamButton.UseVisualStyleBackColor = false;
            // 
            // deleteSelectedPrizeButton
            // 
            this.deleteSelectedPrizeButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.deleteSelectedPrizeButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.deleteSelectedPrizeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.deleteSelectedPrizeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.deleteSelectedPrizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteSelectedPrizeButton.Location = new System.Drawing.Point(804, 350);
            this.deleteSelectedPrizeButton.Name = "deleteSelectedPrizeButton";
            this.deleteSelectedPrizeButton.Size = new System.Drawing.Size(131, 85);
            this.deleteSelectedPrizeButton.TabIndex = 22;
            this.deleteSelectedPrizeButton.Text = "Delete Selected";
            this.deleteSelectedPrizeButton.UseVisualStyleBackColor = false;
            this.deleteSelectedPrizeButton.Click += new System.EventHandler(this.deleteSelectedPrizeButton_Click);
            // 
            // createTournamentButton
            // 
            this.createTournamentButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.createTournamentButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.createTournamentButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.createTournamentButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.createTournamentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createTournamentButton.Location = new System.Drawing.Point(366, 514);
            this.createTournamentButton.Name = "createTournamentButton";
            this.createTournamentButton.Size = new System.Drawing.Size(253, 53);
            this.createTournamentButton.TabIndex = 23;
            this.createTournamentButton.Text = "Create Tournament";
            this.createTournamentButton.UseVisualStyleBackColor = false;
            // 
            // CreateTournamentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(982, 579);
            this.Controls.Add(this.createTournamentButton);
            this.Controls.Add(this.deleteSelectedPrizeButton);
            this.Controls.Add(this.deleteSelectedTeamButton);
            this.Controls.Add(this.createNewTeamLink);
            this.Controls.Add(this.tournamentPrizesListBox);
            this.Controls.Add(this.tournamentPlayersListBox);
            this.Controls.Add(this.tournamentPrizesLable);
            this.Controls.Add(this.tournamentPlayersLabel);
            this.Controls.Add(this.createPrizeButton);
            this.Controls.Add(this.addTeamButton);
            this.Controls.Add(this.selectTeamDropDown);
            this.Controls.Add(this.selectTeamLabel);
            this.Controls.Add(this.entryFeeValue);
            this.Controls.Add(this.entryFeeLable);
            this.Controls.Add(this.tournamentNameValue);
            this.Controls.Add(this.tournamentNameLabel);
            this.Controls.Add(this.headerLable);
            this.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.Name = "CreateTournamentForm";
            this.Text = "Create Tournament";
            this.Load += new System.EventHandler(this.CreateTournamentForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label headerLable;
        private System.Windows.Forms.Label tournamentNameLabel;
        private System.Windows.Forms.TextBox tournamentNameValue;
        private System.Windows.Forms.Label entryFeeLable;
        private System.Windows.Forms.TextBox entryFeeValue;
        private System.Windows.Forms.Label selectTeamLabel;
        private System.Windows.Forms.ComboBox selectTeamDropDown;
        private System.Windows.Forms.Button addTeamButton;
        private System.Windows.Forms.Button createPrizeButton;
        private System.Windows.Forms.Label tournamentPlayersLabel;
        private System.Windows.Forms.Label tournamentPrizesLable;
        private System.Windows.Forms.ListBox tournamentPlayersListBox;
        private System.Windows.Forms.ListBox tournamentPrizesListBox;
        private System.Windows.Forms.LinkLabel createNewTeamLink;
        private System.Windows.Forms.Button deleteSelectedTeamButton;
        private System.Windows.Forms.Button deleteSelectedPrizeButton;
        private System.Windows.Forms.Button createTournamentButton;
    }
}