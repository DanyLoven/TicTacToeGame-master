using System.Windows.Forms;
using System;

namespace TicTacToeGame
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Button easyLevel;
        private Button mediumLevel;
        private Button hardLevel;
        private Button goBackMenu;
        private Button humanVsComputerButton;
        private Button humanVsHumaanButton;
        private Button quitGame;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        private void InitializeComponent()
        {
            this.humanVsComputerButton = new System.Windows.Forms.Button();
            this.humanVsHumaanButton = new System.Windows.Forms.Button();



            this.quitGame = new System.Windows.Forms.Button();

            this.SuspendLayout();


            // Humain VS Ordinateur
            this.humanVsComputerButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.humanVsComputerButton.Location = new System.Drawing.Point(520, 250);
            this.humanVsComputerButton.Name = "humanVsComputerButton";
            this.humanVsComputerButton.Size = new System.Drawing.Size(350, 50);
            this.humanVsComputerButton.TabIndex = 0;
            this.humanVsComputerButton.Text = "Humain VS Ordinateur";
            this.humanVsComputerButton.Click += new System.EventHandler(this.humainVsComputer_Click);
            this.humanVsComputerButton.Cursor = Cursors.Hand;

            // Humain VS Humain
            this.humanVsHumaanButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.humanVsHumaanButton.Location = new System.Drawing.Point(520, 350);
            this.humanVsHumaanButton.Name = "humanVsHumanButton";
            this.humanVsHumaanButton.Size = new System.Drawing.Size(350, 50);
            this.humanVsHumaanButton.TabIndex = 0;
            this.humanVsHumaanButton.Text = " Humain VS Humain";
            this.humanVsHumaanButton.Click += new System.EventHandler(this.humainVsHuman_Click);
            this.humanVsHumaanButton.Cursor = Cursors.Hand;

            // Quitter le jeu
            this.quitGame.BackColor = System.Drawing.Color.WhiteSmoke;
            this.quitGame.Location = new System.Drawing.Point(600, 650);
            this.quitGame.Name = "button2";
            this.quitGame.Size = new System.Drawing.Size(200, 50);
            this.quitGame.TabIndex = 1;
            this.quitGame.Text = "Quitter le jeu";
            this.quitGame.Click += new System.EventHandler(this.quit_Game);
            this.quitGame.Cursor = Cursors.Hand;

            this.Controls.Add(this.humanVsComputerButton);
            this.Controls.Add(this.humanVsHumaanButton);
            this.Controls.Add(this.quitGame);

            // Form1
            int w = 1450;
            int h = 800;

            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 21F);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.Khaki;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(w, h);
            this.Name = "Form1";
            this.Text = "Tic Tac Toe";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private void humainVsComputer_Click(object sender, EventArgs e)
        {

            this.easyLevel = new System.Windows.Forms.Button();
            this.mediumLevel = new System.Windows.Forms.Button();
            this.hardLevel = new System.Windows.Forms.Button();
            this.goBackMenu = new System.Windows.Forms.Button();


            this.humanVsComputerButton.Hide();
            this.humanVsHumaanButton.Hide();
            this.quitGame.Hide();

            // Niveau Facile
            this.easyLevel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.easyLevel.Location = new System.Drawing.Point(250, 100);
            this.easyLevel.Name = "easyLevel";
            this.easyLevel.Size = new System.Drawing.Size(200, 25);
            this.easyLevel.TabIndex = 0;
            this.easyLevel.Text = "Niveau Facile";
            this.easyLevel.Click += new System.EventHandler(this.easyLevel_Click);
            this.easyLevel.Cursor = Cursors.Hand;

            // Niveau Moyen
            this.mediumLevel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.mediumLevel.Location = new System.Drawing.Point(250, 150);
            this.mediumLevel.Name = "mediumLevel";
            this.mediumLevel.Size = new System.Drawing.Size(200, 25);
            this.mediumLevel.TabIndex = 0;
            this.mediumLevel.Text = "Niveau Moyen";
            this.mediumLevel.Click += new System.EventHandler(this.mediumLevel_Click);
            this.mediumLevel.Cursor = Cursors.Hand;

            // Niveau Difficile
            this.hardLevel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.hardLevel.Location = new System.Drawing.Point(250, 200);
            this.hardLevel.Name = "hardLevel";
            this.hardLevel.Size = new System.Drawing.Size(200, 25);
            this.hardLevel.TabIndex = 0;
            this.hardLevel.Text = "Niveau Difficile";
            this.hardLevel.Click += new System.EventHandler(this.hardLevel_Click);
            this.hardLevel.Cursor = Cursors.Hand;

            // Retour Menu
            this.goBackMenu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.goBackMenu.Location = new System.Drawing.Point(250, 350);
            this.goBackMenu.Name = "goBackMenu";
            this.goBackMenu.Size = new System.Drawing.Size(200, 25);
            this.goBackMenu.TabIndex = 0;
            this.goBackMenu.Text = "Retour Menu";
            this.goBackMenu.Click += new System.EventHandler(this.goBackMenu_Click);
            this.goBackMenu.Cursor = Cursors.Hand;


            this.Controls.Add(this.easyLevel);
            this.Controls.Add(this.mediumLevel);
            this.Controls.Add(this.hardLevel);
            this.Controls.Add(this.goBackMenu);

        }

        private void humainVsHuman_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e, "PLAYER");
        }
        private void easyLevel_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e, "COMPUTER", "easy");
        }

        private void mediumLevel_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e, "COMPUTER", "medium");
        }

        private void hardLevel_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e, "COMPUTER", "hard");
        }

        private void goBackMenu_Click(object sender, EventArgs e)
        {
            this.easyLevel.Hide();
            this.mediumLevel.Hide();
            this.hardLevel.Hide();
            this.goBackMenu.Hide();

            this.humanVsComputerButton.Show();
            this.humanVsHumaanButton.Show();
            this.quitGame.Show();
        }

        // Événement de clic du bouton
        private void button1_Click(object sender, EventArgs e, string playerType, string level = "")
        {
            /// Cachez la fenêtre actuelle
            this.Hide();

            int w = 725;
            int h = 500;

            // Crée un nouveau formulaire TicTacToeForm et l'ajoute aux contrôles
            TicTacToeForm ticTacToeForm = new TicTacToeForm(playerType, level);
            ticTacToeForm.Dock = DockStyle.Fill;
            ticTacToeForm.StartPosition = FormStartPosition.CenterScreen;
            ticTacToeForm.BackColor = System.Drawing.Color.Khaki;
            ticTacToeForm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ticTacToeForm.ClientSize = new System.Drawing.Size(w, h);
            ticTacToeForm.Name = "Game";
            ticTacToeForm.Text = "Tic Tac Toe";
            ticTacToeForm.Load += new System.EventHandler(this.Form1_Load);
            ticTacToeForm.ResumeLayout(false);

            ticTacToeForm.Show();
        }

        private void quit_Game(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
