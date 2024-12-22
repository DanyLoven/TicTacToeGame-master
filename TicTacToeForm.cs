using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace TicTacToeGame
{
    public class TicTacToeForm : Form
    {
        private TicTacToe ticTacToeGame;
        private Button[,] buttons;
        private int playerScore;
        private int iAscore;
        int lastPlayer;
        private readonly Label pScore;
        private readonly Label iaScore;
        private readonly string playerType;


        public TicTacToeForm(string playerType, string level)
        {
            // Initialisation de la fenêtre
            Text = "Tic Tac Toe";
            Size = new Size(300, 300);
            pScore = new Label();
            iaScore = new Label();
            ticTacToeGame = new TicTacToe(level);
            this.playerType = playerType;
            ScoreDisplay();
            InitializeButtons();
            lastPlayer = 1;
            playerScore = 0;
            iAscore = 0;
            Console.WriteLine("ticTacToeGame.GetCurrentPlayer()");
            Console.WriteLine(ticTacToeGame.GetCurrentPlayer());
            Console.WriteLine("level");
            Console.WriteLine(level);

            //CheckPlayer();
        }

        private void InitializeButtons()
        {
            // Créer et initialiser les boutons pour le plateau de jeu
            buttons = new Button[3, 3];
            int buttonSize = 88; // Taille des boutons
            int padding = 10; // Espacement entre les boutons

            // Calculer la taille du plateau de jeu en fonction de la taille des boutons et de l'espacement
            int boardWidth = 3 * buttonSize + 2 * padding;
            int boardHeight = 3 * buttonSize + 2 * padding;

            //les marges pour centrer le plateau de jeu dans le formulaire
            int marginLeft = 200;
            int marginTop = 80;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button button = new Button();
                    button.Name = $"button{i}{j}";
                    button.Text = ".";
                    button.Font = new Font("Arial", 40F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    button.Size = new Size(buttonSize, buttonSize);
                    button.BackColor = Color.LightGoldenrodYellow;
                    // Calculer les coordonnées de positionnement du bouton en fonction des marges
                    int buttonX = marginLeft + j * (buttonSize + padding);
                    int buttonY = marginTop + i * (buttonSize + padding);
                    button.Location = new Point(buttonX, buttonY);
                    button.Cursor = Cursors.Hand;

                    button.Click += new EventHandler(OnButtonClick);
                    buttons[i, j] = button;
                    this.Controls.Add(button);
                }
            }


        }



        public void OnButtonClick(object sender, EventArgs e)
        {
            RefreshBoard();


            // Gérez le clic sur un bouton
            Button clickedButton = (Button)sender;
            int x = int.Parse(clickedButton.Name.Substring(6, 1));
            int y = int.Parse(clickedButton.Name.Substring(7, 1));
            Console.WriteLine("playerType");
            Console.WriteLine(playerType);
            if (ticTacToeGame.MakeMove(x, y))
            {
                if (playerType == "PLAYER")
                {
                    Console.WriteLine(ticTacToeGame.GetCurrentPlayer());

                    clickedButton.Text = ticTacToeGame.GetCurrentPlayer().ToString();
                    ticTacToeGame.HumanPlayer(x, y);
                    RefreshBoard();
                    HandleGameResult();

                }
                else
                {
                    ticTacToeGame.SetCurrentPlayer('X');

                    if (ticTacToeGame.GetCurrentPlayer() == 'X')
                    {
                        clickedButton.Text = ticTacToeGame.GetCurrentPlayer().ToString();
                        ticTacToeGame.HumanPlayer(x, y);
                        RefreshBoard();
                        HandleGameResult();
                    }
                }
            }
            else
            {
                MessageBox.Show("L'emplacement est déjà occupé !");
            }
        }

        private void ScoreDisplay()
        {


            // Créer un nouveau label avec le texte spécifié
            Label plabel = new Label();
            Label ilabel = new Label();
            Label goBackMenu = new Label();

            // Retour Menu
            goBackMenu.Font = new Font("Arial", 10F, FontStyle.Bold);
            goBackMenu.ForeColor = Color.Orange;
            goBackMenu.Cursor = Cursors.Hand;
            goBackMenu.Text = "Retour";
            goBackMenu.Click += new System.EventHandler(this.goBackMenu_Click);


            plabel.Font = new Font("Arial", 30F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            plabel.ForeColor = Color.Black;
            plabel.Text = "PLAYER";

            ilabel.Font = new Font("Arial", 30F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            ilabel.ForeColor = Color.Black;
            ilabel.Text = this.playerType;

            pScore.Font = new Font("Arial", 30F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            pScore.ForeColor = Color.SpringGreen;
            pScore.Text = playerScore.ToString();

            iaScore.Font = new Font("Arial", 30F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            iaScore.ForeColor = Color.OrangeRed;
            iaScore.Text = iAscore.ToString();


            // Positionner les labels sur l'écran
            goBackMenu.Location = new Point(5, 5);
            goBackMenu.AutoSize = true;

            plabel.Location = new Point(50, 25);
            plabel.AutoSize = true;

            pScore.Location = new Point(270, 25);
            pScore.AutoSize = true;

            iaScore.Location = new Point(370, 25);
            iaScore.AutoSize = true;

            ilabel.Location = new Point(450, 25);
            ilabel.AutoSize = true;

            // Ajouter les labels à la fenêtre principale de l'application
            this.Controls.Add(plabel);
            this.Controls.Add(pScore);
            this.Controls.Add(iaScore);
            this.Controls.Add(ilabel);
            this.Controls.Add(goBackMenu);
        }

        private void goBackMenu_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                // Recherchez la fenêtre précédente (Form1, par exemple)
                if (form is Form1)
                {
                    // Affichez la fenêtre précédente et quittez la boucle
                    form.Show();
                    //this.humanVsComputerButton.Show();
                    //this.humanVsHumaanButton.Show();
                    //this.quitGame.Show();
                    break;
                }
            }
            // Fermez la fenêtre actuelle (TicTacToeForm)
            this.Close();
        }


        private void ChangeStartPlayer()
        {
            if (lastPlayer == 1)
            {
                lastPlayer = 0;
            }
            else
            {
                lastPlayer = 1; 
            }
        }

        private void HandleGameResult()
        {
            if (ticTacToeGame.CheckWin('X'))
            {
                playerScore += 1;
                ScoreDisplay();
                ColoriedWinnerTouple('X');
                ShowMessageBox(ticTacToeGame.GetCurrentPlayer() + " a gagné !", "Souhaitez-vous rejouer ?");
                ChangeStartPlayer();
                ResetGame();
                ColoriedWinnerTouple('R');
            }
            else if (ticTacToeGame.CheckWin('O'))
            {
                iAscore += 1;
                ScoreDisplay();
                ColoriedWinnerTouple('O');
                ShowMessageBox(ticTacToeGame.GetCurrentPlayer() + " a gagné !", "Souhaitez-vous rejouer ?");
                ChangeStartPlayer();
                ResetGame();
                ColoriedWinnerTouple('R');
            }
            else if (ticTacToeGame.IsBoardFull())
            {
                ShowMessageBox("Match nul !", "Souhaitez-vous rejouer ?");
                ChangeStartPlayer();
                ResetGame();
            }
            else
            {
                if (ticTacToeGame.GetCurrentPlayer() == 'X' && ticTacToeGame.availablePositions.Count > 0 && playerType == "COMPUTER")
                {
                    ComputerPlay();
                }
                else
                {
                    ticTacToeGame.SetCurrentPlayer(ticTacToeGame.GetCurrentPlayer() == 'X' ? 'O' : 'X');

                    Console.WriteLine("getcurrentplayer");
                    Console.WriteLine(ticTacToeGame.GetCurrentPlayer());
                }
            }
        }

        public void ShowMessageBox(string messageWinner, string replay)
        {
            DialogResult result = MessageBox.Show(replay, messageWinner, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {

                // Parcourir toutes les fenêtres ouvertes dans l'application
                foreach (Form form in Application.OpenForms)
                {
                    // Recherchez la fenêtre précédente (Form1, par exemple)
                    if (form is Form1)
                    {
                        // Affichez la fenêtre précédente et quittez la boucle
                        form.Show();
                        break;
                    }
                }
                // Fermez la fenêtre actuelle (TicTacToeForm)
                this.Close();
            }
        }

        private void ResetGame()
        {
            ticTacToeGame.ResetGame();
            // Réinitialisez l'état du jeu et réinitialisez les boutons
            foreach (Button button in buttons)
            {
                button.Text = ".";
            }
            RefreshBoard();
            //Console.WriteLine("Partie terminé");
            if (lastPlayer != 1 && playerType != "PLAYER")
            {
                ComputerPlay();
            }
        }

        public void RefreshBoard()
        {
            // Mettez à jour le texte des boutons en fonction de l'état du jeu
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i, j].Text = ticTacToeGame.GetBoard()[i, j].ToString();
                }
            }
        }

        public void ColoriedWinnerTouple(char winner)
        {
            if (winner == 'O')
            {
                buttons[ticTacToeGame.troupleWinnerCoordonnate[0].Item1, ticTacToeGame.troupleWinnerCoordonnate[0].Item2].ForeColor = Color.OrangeRed;
                buttons[ticTacToeGame.troupleWinnerCoordonnate[1].Item1, ticTacToeGame.troupleWinnerCoordonnate[1].Item2].ForeColor = Color.OrangeRed;
                buttons[ticTacToeGame.troupleWinnerCoordonnate[2].Item1, ticTacToeGame.troupleWinnerCoordonnate[2].Item2].ForeColor = Color.OrangeRed;
            }
            else if (winner == 'X')
            {
                buttons[ticTacToeGame.troupleWinnerCoordonnate[0].Item1, ticTacToeGame.troupleWinnerCoordonnate[0].Item2].ForeColor = Color.SpringGreen;
                buttons[ticTacToeGame.troupleWinnerCoordonnate[1].Item1, ticTacToeGame.troupleWinnerCoordonnate[1].Item2].ForeColor = Color.SpringGreen;
                buttons[ticTacToeGame.troupleWinnerCoordonnate[2].Item1, ticTacToeGame.troupleWinnerCoordonnate[2].Item2].ForeColor = Color.SpringGreen;
            }
            else
            {
                buttons[ticTacToeGame.troupleWinnerCoordonnate[0].Item1, ticTacToeGame.troupleWinnerCoordonnate[0].Item2].ForeColor = Color.Black;
                buttons[ticTacToeGame.troupleWinnerCoordonnate[1].Item1, ticTacToeGame.troupleWinnerCoordonnate[1].Item2].ForeColor = Color.Black;
                buttons[ticTacToeGame.troupleWinnerCoordonnate[2].Item1, ticTacToeGame.troupleWinnerCoordonnate[2].Item2].ForeColor = Color.Black;
            }
        }


        public void ComputerPlay()
        {
            ticTacToeGame.SetCurrentPlayer('O');
            ticTacToeGame.ComputerPlayer();
            RefreshBoard();
            HandleGameResult();
            RefreshBoard();
            ticTacToeGame.SetCurrentPlayer('X');
        }


    }
}
