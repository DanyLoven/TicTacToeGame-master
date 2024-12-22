using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    /*public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }*/



    class TicTacToe
    {
        private char[,] gameBoard;
        private char[,] boardPositions;
        public char currentPlayer;
        public List<(int, int)> availablePositions;
        public List<(int, int)> troupleWinnerCoordonnate;
        private Random random;
        private string difficultyLevel;
        public bool confirmIAalreadyPlayed;
        private int iaPriorityToPlay;
        private (int, int) lastIaCornerLocation;
        private List<(int, int)> cornerLocation;




        public TicTacToe(string level)
        {
            gameBoard = new char[3, 3];
            availablePositions = new List<(int, int)>();
            troupleWinnerCoordonnate= new List<(int, int)>();
          
            currentPlayer = 'X';
            random = new Random();
            difficultyLevel = level;
            confirmIAalreadyPlayed = false;
            InitializeBoardAndAvailablePositions();
            iaPriorityToPlay = 0;
            cornerLocation = new List<(int, int)> { (0, 0), (0, 2), (2, 0), (2, 2) };
        }

        public void InitializeBoardAndAvailablePositions()
        {
            availablePositions = new List<(int, int)>();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    gameBoard[i, j] = '.';
                    availablePositions.Add((i,j));
                }
            }
        }

      


        public void PrintBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write($"  {gameBoard[i, j]}   ");
                }
                Console.Write($"\n");
                Console.WriteLine();
            }
        }


        public char GetPlayerSymbol(int x, int y)
        {
            char player = gameBoard[x, y];
            return player;
        }



        public void PrintAvailablePositions()
        {
            Console.Write($"\n");
            int t = 0;
            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 3; j++)
                {
                    Console.Write($"{availablePositions[t]}");
                    t++;
                }
                Console.Write($"\n");
                Console.WriteLine();

            }
        }

        public bool MakeMove(int x, int y)
        {
            if (gameBoard[x, y] == '.')
            {
                gameBoard[x, y] = currentPlayer;
                availablePositions.Remove((x, y));
                return true;
            }
            else
            {
                return false;
            }
        }

        public char[,] GetBoard()
        {
            return gameBoard;
        }

        public char GetCurrentPlayer()
        {
            return currentPlayer;
        }




        public void SetCurrentPlayer(char player)
        {
            currentPlayer = player;
        }


        public void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
        }

        public bool CheckWin(char currentPlayer)
        {


            for (int i = 0; i < 3; i++)
            {
                // Vérification des Lignes
                if (gameBoard[i, 0] == currentPlayer && gameBoard[i, 1] == currentPlayer && gameBoard[i, 2] == currentPlayer)
                {
                    SetTroupleWinner((i,0), (i, 1), (i, 2));
                    return true;
                }

                // Vérification des colonnes
                if (gameBoard[0, i] == currentPlayer && gameBoard[1, i] == currentPlayer && gameBoard[2, i] == currentPlayer)
                {
                    SetTroupleWinner((0, i), (1, i), (2, i));
                    return true;
                }
            }

            // Vérification des diagonales
            if (gameBoard[0, 0] == currentPlayer && gameBoard[1, 1] == currentPlayer && gameBoard[2, 2] == currentPlayer) 
            {
                SetTroupleWinner((0, 0), (1, 1), (2, 2));
                return true;
            }
            // Vérification des diagonales
            if  (gameBoard[0, 2] == currentPlayer && gameBoard[1, 1] == currentPlayer && gameBoard[2, 0] == currentPlayer)
            {
                SetTroupleWinner((0, 2), (1, 1), (2, 0));
                return true;
            }
            return false;
        }

        public void SetTroupleWinner((int, int)p1, (int, int) p2, (int, int) p3)
        {
            troupleWinnerCoordonnate = new List<(int, int)>
            {
                p1,
                p2,
                p3
            };
        }
        public bool IsBoardFull()
        {
            return availablePositions.Count == 0;

        }

        public void HumanPlayer(int x, int y)
        {
            iaPriorityToPlay = 0;

            if (gameBoard[x, y] == '.')
            {

                gameBoard[x, y] = currentPlayer;
                Console.WriteLine("currentPlayer");
                Console.WriteLine(currentPlayer);
                availablePositions.Remove((x, y));
            }
            else
            {
                Console.WriteLine("Cette position est déjà occupé, veuillez entrer une autre position : ");
            }

        }

        //---------------------------------------------------- IA program Début ----------------------------------------------------------

        public void IaRandomPlay(int priority)
        {
            int index = random.Next(availablePositions.Count);
           
                var position = availablePositions[index];
                gameBoard[position.Item1, position.Item2] = 'O';
                availablePositions.RemoveAt(index);
               
            
            iaPriorityToPlay = priority;
        }

        public void IaAttackCheckLineCoupleCaseTowin()
        {
            int placeOccupedByIA = 0;
            int placeOccupedByPlayer = 0;
            var coordonateToIAplayLine = (0, 0);

            // Vérification des Lignes
            for (int i = 0; i < 3; i++)
            {
                placeOccupedByIA = 0;
                placeOccupedByPlayer = 0;
                for (int j = 0; j < 3; j++)
                {

                    if (gameBoard[i, j] == 'O')
                    {
                        placeOccupedByIA++;
                    }
                    else if (gameBoard[i, j] == 'X')
                    {
                        placeOccupedByPlayer++;
                    }
                    else
                    {
                        coordonateToIAplayLine = (i, j);
                    }
                }

                if (placeOccupedByPlayer == 0 && placeOccupedByIA == 2)
                {
                    gameBoard[coordonateToIAplayLine.Item1, coordonateToIAplayLine.Item2] = 'O';

                    availablePositions.Remove((coordonateToIAplayLine.Item1, coordonateToIAplayLine.Item2));

                    iaPriorityToPlay = -1;
                    break;
                }
                else
                {
                    if (i != 2)
                    {
                        continue;
                    }
                    else
                    {
                        iaPriorityToPlay = 1;
                        break;

                    }

                }
            }
        }

        public void IaAttackCheckColumnCoupleCaseToWin()
        {
            int placeOccupedByIA = 0;
            int placeOccupedByPlayer = 0;
            var coordonateToIAplayColumn = (0, 0);

            // Vérification des colonnes
            for (int i = 0; i < 3; i++)
            {
                placeOccupedByIA = 0;
                placeOccupedByPlayer = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (gameBoard[j, i] == 'O')
                    {
                        placeOccupedByIA++;
                    }
                    else if (gameBoard[j, i] == 'X')
                    {
                        placeOccupedByPlayer++;
                    }
                    else
                    {
                        coordonateToIAplayColumn = (j, i);
                    }
                }

                if (placeOccupedByPlayer == 0 && placeOccupedByIA == 2)
                {

                    gameBoard[coordonateToIAplayColumn.Item1, coordonateToIAplayColumn.Item2] = 'O';
                    availablePositions.Remove((coordonateToIAplayColumn.Item1, coordonateToIAplayColumn.Item2));
                    iaPriorityToPlay = -1;
                    break;
                }
                else
                {
                    if (i != 2)
                    {
                        continue;
                    }
                    else
                    {
                        iaPriorityToPlay = 2;
                        break;
                    }
                }
            }
        }

        public void IaAttackCheckLeftDiagonalCoupleCaseTowin()
        {
            int placeOccupedByIA = 0;
            int placeOccupedByPlayer = 0;
            var coordonateToIAplayLeftDiagonal = (0, 0);

            // Vérification de la diagonale gauche
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == j && gameBoard[i, j] == 'O')
                    {
                        placeOccupedByIA++;
                    }
                    else if (i == j && gameBoard[i, j] == 'X')
                    {
                        placeOccupedByPlayer++;
                    }
                    else if (i == j && gameBoard[i, j] == '.')
                    {
                        coordonateToIAplayLeftDiagonal = (i, j);
                    }
                }
            }
            if (placeOccupedByPlayer == 0 && placeOccupedByIA == 2)
            {
                gameBoard[coordonateToIAplayLeftDiagonal.Item1, coordonateToIAplayLeftDiagonal.Item2] = 'O';
                availablePositions.Remove((coordonateToIAplayLeftDiagonal.Item1, coordonateToIAplayLeftDiagonal.Item2));
                iaPriorityToPlay = -1;

            }
            else
            {
                iaPriorityToPlay = 3;
            }

        }

        public void IaAttackCheckRightDiagonalCoupleCaseTowin()
        {
            int placeOccupedByIA = 0;
            int placeOccupedByPlayer = 0;
            var coordonateToIAplayRightDiagonal = (0, 0);
            double gameBordLenght = gameBoard.Length;
            int constantRightDiagonalcoordonateSum = Convert.ToInt32(Math.Sqrt(gameBordLenght)) - 1;
            // Vérification de la diagonale droite
            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 3; j++)
                {
                    if (i + j == constantRightDiagonalcoordonateSum && gameBoard[i, j] == 'O')
                    {
                        placeOccupedByIA++;
                    }
                    else if (i + j == constantRightDiagonalcoordonateSum && gameBoard[i, j] == 'X')
                    {
                        placeOccupedByPlayer++;
                    }
                    else if (i + j == constantRightDiagonalcoordonateSum && gameBoard[i, j] == '.')
                    {
                        coordonateToIAplayRightDiagonal = (i, j);
                    }
                }
            }
            if (placeOccupedByPlayer == 0 && placeOccupedByIA == 2)
            {
                gameBoard[coordonateToIAplayRightDiagonal.Item1, coordonateToIAplayRightDiagonal.Item2] = 'O';
                availablePositions.Remove((coordonateToIAplayRightDiagonal.Item1, coordonateToIAplayRightDiagonal.Item2));
                iaPriorityToPlay = -1;

            }
            else
            {
                iaPriorityToPlay = 4;
            }

        }

        public void IaDefenseCheckLinePlayerCoupleCase()
        {
            int placeOccupedByIA = 0;
            int placeOccupedByPlayer = 0;
            var coordonateToIAplayLine = (0, 0);

            // Vérification des Lignes
            for (int i = 0; i < 3; i++)
            {
                placeOccupedByIA = 0;
                placeOccupedByPlayer = 0;
                for (int j = 0; j < 3; j++)
                {

                    if (gameBoard[i, j] == 'O')
                    {
                        placeOccupedByIA++;
                    }
                    else if (gameBoard[i, j] == 'X')
                    {
                        placeOccupedByPlayer++;
                    }
                    else
                    {
                        coordonateToIAplayLine = (i, j);
                    }
                }

                if (placeOccupedByPlayer == 2 && placeOccupedByIA == 0)
                {

                    gameBoard[coordonateToIAplayLine.Item1, coordonateToIAplayLine.Item2] = 'O';
                    availablePositions.Remove((coordonateToIAplayLine.Item1, coordonateToIAplayLine.Item2));
                    iaPriorityToPlay = -1;
                    break;
                }
                else
                {
                    if (i != 2)
                    {
                        continue;
                    }
                    else
                    {
                        iaPriorityToPlay = 5;
                        break;
                    }
                }
            }
        }

        public void IaDefenseCheckColumnPlayerCoupleCase()
        {
            int placeOccupedByIA = 0;
            int placeOccupedByPlayer = 0;
            var coordonateToIAplayColumn = (0, 0);

            // Vérification des colonnes
            for (int i = 0; i < 3; i++)
            {
                placeOccupedByIA = 0;
                placeOccupedByPlayer = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (gameBoard[j, i] == 'O')
                    {
                        placeOccupedByIA++;
                    }
                    else if (gameBoard[j, i] == 'X')
                    {
                        placeOccupedByPlayer++;
                    }
                    else
                    {
                        coordonateToIAplayColumn = (j, i);
                    }
                }


                if (placeOccupedByPlayer == 2 && placeOccupedByIA == 0)
                {

                    gameBoard[coordonateToIAplayColumn.Item1, coordonateToIAplayColumn.Item2] = 'O';
                    availablePositions.Remove((coordonateToIAplayColumn.Item1, coordonateToIAplayColumn.Item2));
                    iaPriorityToPlay = -1;
                    break;
                }
                else
                {
                    if (i != 2)
                    {
                        continue;
                    }
                    else
                    {
                        iaPriorityToPlay = 6;
                        break;
                    }
                }
            }
        }

        public void IaDefenseCheckLeftDiagonalPlayerCoupleCase()
        {
            int placeOccupedByIA = 0;
            int placeOccupedByPlayer = 0;
            var coordonateToIAplayLeftDiagonal = (0, 0);

            // Vérification de la diagonale gauche
            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 3; j++)
                {
                    if (i == j && gameBoard[i, j] == 'O')
                    {
                        placeOccupedByIA++;
                    }
                    else if (i == j && gameBoard[i, j] == 'X')
                    {
                        placeOccupedByPlayer++;
                    }
                    else if (i == j && gameBoard[i, j] == '.')
                    {
                        coordonateToIAplayLeftDiagonal = (i, j);
                    }
                }
            }
            if (placeOccupedByPlayer == 2 && placeOccupedByIA == 0)
            {
                gameBoard[coordonateToIAplayLeftDiagonal.Item1, coordonateToIAplayLeftDiagonal.Item2] = 'O';
                availablePositions.Remove((coordonateToIAplayLeftDiagonal.Item1, coordonateToIAplayLeftDiagonal.Item2));
                iaPriorityToPlay = -1;

            }
            else
            {
                iaPriorityToPlay = 7;
            }

        }

        public void IaDefenseCheckRightDiagonalPlayerCoupleCase()
        {
            int placeOccupedByIA = 0;
            int placeOccupedByPlayer = 0;
            var coordonateToIAplayRightDiagonal = (0, 0);
            double gameBordLenght = gameBoard.Length;
            int constantRightDiagonalcoordonateSum = Convert.ToInt32(Math.Sqrt(gameBordLenght)) - 1;

            // Vérification de la diagonale droite
            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 3; j++)
                {
                    if (i + j == constantRightDiagonalcoordonateSum && gameBoard[i, j] == 'O')
                    {
                        placeOccupedByIA++;
                    }
                    else if (i + j == constantRightDiagonalcoordonateSum && gameBoard[i, j] == 'X')
                    {
                        placeOccupedByPlayer++;
                    }
                    else if (i + j == constantRightDiagonalcoordonateSum && gameBoard[i, j] == '.')
                    {
                        coordonateToIAplayRightDiagonal = (i, j);
                    }
                }
            }
            if (placeOccupedByPlayer == 2 && placeOccupedByIA == 0)
            {
                gameBoard[coordonateToIAplayRightDiagonal.Item1, coordonateToIAplayRightDiagonal.Item2] = 'O';
                availablePositions.Remove((coordonateToIAplayRightDiagonal.Item1, coordonateToIAplayRightDiagonal.Item2));
                iaPriorityToPlay = -1;

            }
            else
            {
                iaPriorityToPlay = 8;
            }

        }

        public void IaAttackCheckLineToGetCouple()
        {
            int placeOccupedByIA = 0;
            int placeOccupedByPlayer = 0;
            var coordonateToIAplayLine = (0, 0);

            // Vérification des Lignes
            for (int i = 0; i < 3; i++)
            {
                placeOccupedByIA = 0;
                placeOccupedByPlayer = 0;
                for (int j = 0; j < 3; j++)
                {

                    if (gameBoard[i, j] == 'O')
                    {
                        placeOccupedByIA++;
                    }
                    else if (gameBoard[i, j] == 'X')
                    {
                        placeOccupedByPlayer++;
                    }
                    else
                    {
                        coordonateToIAplayLine = (i, j);
                    }
                }

                if (placeOccupedByPlayer == 0 && placeOccupedByIA == 1)
                {

                    gameBoard[coordonateToIAplayLine.Item1, coordonateToIAplayLine.Item2] = 'O';
                    availablePositions.Remove((coordonateToIAplayLine.Item1, coordonateToIAplayLine.Item2));
                    iaPriorityToPlay = -1;
                    break;
                }
                else
                {
                    if (i != 2)
                    {
                        continue;
                    }
                    else
                    {
                        iaPriorityToPlay = 9;
                        break;
                    }
                }
            }
        }

        public void IaAttackCheckColumnToGetCouple()
        {
            int placeOccupedByIA = 0;
            int placeOccupedByPlayer = 0;
            var coordonateToIAplayColumn = (0, 0);

            // Vérification des colonnes
            for (int i = 0; i < 3; i++)
            {
                placeOccupedByIA = 0;
                placeOccupedByPlayer = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (gameBoard[j, i] == 'O')
                    {
                        placeOccupedByIA++;
                    }
                    else if (gameBoard[j, i] == 'X')
                    {
                        placeOccupedByPlayer++;
                    }
                    else
                    {
                        coordonateToIAplayColumn = (j, i);
                    }
                }


                if (placeOccupedByPlayer == 0 && placeOccupedByIA == 1)
                {

                    gameBoard[coordonateToIAplayColumn.Item1, coordonateToIAplayColumn.Item2] = 'O';
                    availablePositions.Remove((coordonateToIAplayColumn.Item1, coordonateToIAplayColumn.Item2));
                    iaPriorityToPlay = -1;
                    break;
                }
                else
                {
                    if (i != 2)
                    {
                        continue;
                    }
                    else
                    {
                        iaPriorityToPlay = 10;
                        break;
                    }
                }
            }
        }

        public void IaAttackCheckLeftDiagonalToGetCouple()
        {
            int placeOccupedByIA = 0;
            int placeOccupedByPlayer = 0;
            var coordonateToIAplayLeftDiagonal = (0, 0);

            // Vérification de la diagonale gauche
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == j && gameBoard[i, j] == 'O')
                    {
                        placeOccupedByIA++;
                    }
                    else if (i == j && gameBoard[i, j] == 'X')
                    {
                        placeOccupedByPlayer++;
                    }
                    else if (i == j && gameBoard[i, j] == '.')
                    {
                        coordonateToIAplayLeftDiagonal = (i, j);
                    }
                }
            }
            if (placeOccupedByPlayer == 0 && placeOccupedByIA == 1)
            {
                gameBoard[coordonateToIAplayLeftDiagonal.Item1, coordonateToIAplayLeftDiagonal.Item2] = 'O';
                availablePositions.Remove((coordonateToIAplayLeftDiagonal.Item1, coordonateToIAplayLeftDiagonal.Item2));
                iaPriorityToPlay = -1;

            }
            else
            {
                iaPriorityToPlay = 11;
            }

        }

        public void IaAttackCheckRightDiagonalToGetCouple()
        {
            int placeOccupedByIA = 0;
            int placeOccupedByPlayer = 0;
            var coordonateToIAplayRightDiagonal = (0, 0);
            double gameBordLenght = gameBoard.Length;
            int constantRightDiagonalcoordonateSum = Convert.ToInt32(Math.Sqrt(gameBordLenght)) - 1;

            // Vérification de la diagonale droite
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i + j == constantRightDiagonalcoordonateSum && gameBoard[i, j] == 'O')
                    {
                        placeOccupedByIA++;
                    }
                    else if (i + j == constantRightDiagonalcoordonateSum && gameBoard[i, j] == 'X')
                    {
                        placeOccupedByPlayer++;
                    }
                    else if (i + j == constantRightDiagonalcoordonateSum && gameBoard[i, j] == '.')
                    {
                        coordonateToIAplayRightDiagonal = (i, j);
                    }
                }
            }
            if (placeOccupedByPlayer == 0 && placeOccupedByIA == 1)
            {
                gameBoard[coordonateToIAplayRightDiagonal.Item1, coordonateToIAplayRightDiagonal.Item2] = 'O';
                availablePositions.Remove((coordonateToIAplayRightDiagonal.Item1, coordonateToIAplayRightDiagonal.Item2));
                iaPriorityToPlay = -1;

            }
            else
            {
                iaPriorityToPlay = 12;
            }

        }

        public void IaStrategicFirstPlay()
        {
            int randomPlay = random.Next(2);
            int index = random.Next(cornerLocation.Count);
            var position = cornerLocation[index];
            if (availablePositions.Count == 9)
            {
                if(randomPlay < 3)
                {
                    gameBoard[1,1] = 'O';
                    availablePositions.Remove((1, 1));
                }
                else
                {
                    gameBoard[position.Item1, position.Item2] = 'O';
                    lastIaCornerLocation = position;
                    availablePositions.Remove((position.Item1, position.Item2));
                    cornerLocation.RemoveAt(index);
                }
            }
            else if (availablePositions.Count == 8)
            {
                if (gameBoard[1, 1] == '.')
                {
                    gameBoard[1, 1] = 'O';
                    availablePositions.Remove((1, 1));
                }
                else
                {
                    gameBoard[position.Item1, position.Item2] = 'O';
                    lastIaCornerLocation = position;
                    availablePositions.Remove((position.Item1, position.Item2));
                    cornerLocation.RemoveAt(index);
                }
            }
            iaPriorityToPlay = -1;
        }

        public (int, int) CountCornerPlayerAndIaPosition()
        {
            var cornerPosition = new List<(int, int)> { (0, 0), (0, 2), (2, 0), (2, 2) };
            int playerIn = 0;
            int iaIn = 0;
            foreach (var couple in cornerPosition)
            {
                if (gameBoard[couple.Item1, couple.Item2] == 'X')
                {
                    playerIn++;
                }
                else if (gameBoard[couple.Item1, couple.Item2] == 'O')
                {
                    iaIn++;
                }
            }
            return (playerIn, iaIn);
        }

        private bool CheckSidePosition(int x, int y)
        {
            bool goodPosition=false;


            // Tableau pour stocker les coordonnées des cases voisines
            var sidePositions = new List<(int, int)>();

            // Coordonnées des cases adjacentes possibles
            int[,] offsets = { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };

            for (int i = 0; i < offsets.GetLength(0); i++)
            {
                int newX = x + offsets[i, 0];
                int newY = y + offsets[i, 1];


                // Vérifier si les nouvelles coordonnées sont valides
                if (newX >= 0 && newX < 3 && newY >= 0 && newY < 3)
                {
                    sidePositions.Add((newX, newY));
                }
            }

            if ((gameBoard[sidePositions[0].Item1, sidePositions[0].Item2] == '.' && gameBoard[sidePositions[1].Item1, sidePositions[1].Item2] == '.'))
            {
                goodPosition = true;
            }
            else
            {
                goodPosition = false;
            }

            return goodPosition;
        }

        public (int, int) CornerAvailableToPlayTriangleAttack()
        {
            int x = 0;
            int y = 0;
            //get opposite coordinates of lastIaCornerLocation
            int xo = Math.Abs(lastIaCornerLocation.Item1 - 2);
            int yo = Math.Abs(lastIaCornerLocation.Item2 - 2);
            foreach (var couple in cornerLocation)
            {
                if (couple != (xo, yo) && CheckSidePosition(couple.Item1, couple.Item1) && gameBoard[couple.Item1, couple.Item2] == '.')
                {
                    x = couple.Item1;
                    y = couple.Item2;
                }

            }
            return (x, y);
        }
        public (int, int) CornerAvailableToPlayDefense()
        {
            int x = 0;
            int y = 0;
           
            foreach (var couple in cornerLocation)
            {
                if (!CheckSidePosition(couple.Item1, couple.Item1) && gameBoard[couple.Item1,couple.Item2]=='.')
                {
                    x = couple.Item1;
                    y = couple.Item2;
                }

            }
            return (x, y);
        }
        public void setIaCornerOppositePlayer()
        {
            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 3; j++)
                {
                    if (gameBoard[i,j] =='X')
                    {
                        i = Math.Abs(i - 2);
                        j = Math.Abs(j- 2);
                        gameBoard[i, j] = 'O';
                        availablePositions.Remove((i, j));
                        iaPriorityToPlay = -1;
                        return;
                    }
                }
                Console.Write($"\n");
                Console.WriteLine();

            }
        }


        public void IaCornePlay((int, int) position, int index)
        {
            
                gameBoard[position.Item1, position.Item2] = 'O';
                lastIaCornerLocation = position;
                availablePositions.Remove((position.Item1, position.Item2));
                cornerLocation.RemoveAt(index);
                iaPriorityToPlay = -1;
        }

        public void IaCorneAttack((int, int) iaCornerToplay)
        {
            gameBoard[iaCornerToplay.Item1, iaCornerToplay.Item2] = 'O';
            availablePositions.Remove((iaCornerToplay.Item1, iaCornerToplay.Item2));
            cornerLocation.Remove((iaCornerToplay.Item1, iaCornerToplay.Item2));
            iaPriorityToPlay = -1;
        }

        public void IaCorneDefense((int, int) iaCornerToplay)
        {
            gameBoard[iaCornerToplay.Item1, iaCornerToplay.Item2] = 'O';
            availablePositions.Remove((iaCornerToplay.Item1, iaCornerToplay.Item2));
            cornerLocation.Remove((iaCornerToplay.Item1, iaCornerToplay.Item2));
            iaPriorityToPlay = -1;
        }

        public void IaStrategicPlayToGetCouple()
        {
            int index = random.Next(cornerLocation.Count);
            var position = cornerLocation[index];

            if (availablePositions.Count == 7 && gameBoard[1, 1] == '.')
            {
                gameBoard[1, 1] = 'O';
                availablePositions.Remove((1, 1));
                iaPriorityToPlay = -1;
            }

            else if (availablePositions.Count == 7 && gameBoard[1, 1] == 'X')
            {
                //get opposite coordinates of lastIaCornerLocation
                int x = Math.Abs(lastIaCornerLocation.Item1 - 2);
                int y = Math.Abs(lastIaCornerLocation.Item2 - 2);

                gameBoard[x, y] = 'O';
                cornerLocation.RemoveAt(index);
                availablePositions.Remove((x, y));
                iaPriorityToPlay = -1;
            }
            else if (availablePositions.Count == 7 && gameBoard[1, 1] == 'O' && CountCornerPlayerAndIaPosition().Item1 == 1)
            {
                setIaCornerOppositePlayer();
            }
            else if (availablePositions.Count == 6 && gameBoard[1, 1] == 'O' && CountCornerPlayerAndIaPosition().Item1 == 1)
            {
                (int, int) iaCornerToplay = CornerAvailableToPlayDefense();
                IaCorneDefense(iaCornerToplay);
            }
            else if (availablePositions.Count == 6 && gameBoard[1, 1] == 'O' && CountCornerPlayerAndIaPosition().Item1 == 0)
            {
                (int, int) iaCornerToplay = CornerAvailableToPlayDefense();
                IaCorneDefense(iaCornerToplay);
            }
            else if (availablePositions.Count == 6 && gameBoard[1, 1] == 'X' && CountCornerPlayerAndIaPosition().Item1 == 1)
            {
                (int, int) iaCornerToplay = CornerAvailableToPlayTriangleAttack();
                IaCorneAttack(iaCornerToplay);
            }

            else if (availablePositions.Count == 5 && gameBoard[1, 1] == 'O' && CountCornerPlayerAndIaPosition().Item1 == 1)
            {
                (int, int) iaCornerToplay = CornerAvailableToPlayTriangleAttack();
                IaCorneAttack(iaCornerToplay);
            }
            else if (availablePositions.Count == 5 && gameBoard[1, 1] == 'O' && CountCornerPlayerAndIaPosition().Item1 == 2)
            {
                IaCornePlay(position, index);
            }
            else if (availablePositions.Count == 5 && gameBoard[1, 1] == 'X' && CountCornerPlayerAndIaPosition().Item2 != 0)
            {
                IaCornePlay(position, index);
            }
            else
            {
                IaAttackCheckLineToGetCouple();
                Console.WriteLine($"piority :{iaPriorityToPlay}");
            }
        }

        public void IaDiffucultyLevelEasy()
        {

            while (iaPriorityToPlay != -1)
            {
                switch (iaPriorityToPlay)
                {
                    case 0:
                        IaAttackCheckLineCoupleCaseTowin();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 1:
                        IaAttackCheckColumnCoupleCaseToWin();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;


                    case 2:
                        IaAttackCheckLeftDiagonalCoupleCaseTowin();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 3:
                        IaAttackCheckRightDiagonalCoupleCaseTowin();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;
                    case 4:
                        IaAttackCheckLineToGetCouple();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 9:
                        IaAttackCheckColumnToGetCouple();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 10:
                        IaAttackCheckLeftDiagonalToGetCouple();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 11:
                        IaAttackCheckRightDiagonalToGetCouple();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;


                    default:
                        IaRandomPlay(-1);
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;
                }
            }

        }

        public void IaDiffucultyLevelMedium()
        {

            while (iaPriorityToPlay != -1)
            {
                switch (iaPriorityToPlay)
                {
                    case 0:
                        IaAttackCheckLineCoupleCaseTowin();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 1:
                        IaAttackCheckColumnCoupleCaseToWin();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;


                    case 2:
                        IaAttackCheckLeftDiagonalCoupleCaseTowin();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 3:
                        IaAttackCheckRightDiagonalCoupleCaseTowin();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 4:
                        IaDefenseCheckLinePlayerCoupleCase();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 5:
                        IaDefenseCheckColumnPlayerCoupleCase();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 6:
                        IaDefenseCheckLeftDiagonalPlayerCoupleCase();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 7:
                        IaDefenseCheckRightDiagonalPlayerCoupleCase();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 8:
                        IaAttackCheckLineToGetCouple();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 9:
                        IaAttackCheckColumnToGetCouple();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 10:
                        IaAttackCheckLeftDiagonalToGetCouple();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 11:
                        IaAttackCheckRightDiagonalToGetCouple();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 12:
                        IaRandomPlay(-1);
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;
                }
            }

        }

        public void IaDiffucultyLevelHard()
        {
            while (iaPriorityToPlay != -1)
            {
                switch (iaPriorityToPlay)
                {
                    case 0:
                        IaAttackCheckLineCoupleCaseTowin();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 1:
                        IaAttackCheckColumnCoupleCaseToWin();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;


                    case 2:
                        IaAttackCheckLeftDiagonalCoupleCaseTowin();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 3:
                        IaAttackCheckRightDiagonalCoupleCaseTowin();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 4:
                        IaDefenseCheckLinePlayerCoupleCase();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 5:
                        IaDefenseCheckColumnPlayerCoupleCase();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 6:
                        IaDefenseCheckLeftDiagonalPlayerCoupleCase();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 7:
                        IaDefenseCheckRightDiagonalPlayerCoupleCase();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 8:
                        IaStrategicPlayToGetCouple();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 9:
                        IaAttackCheckColumnToGetCouple();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 10:
                        IaAttackCheckLeftDiagonalToGetCouple();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 11:
                        IaAttackCheckRightDiagonalToGetCouple();
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;

                    case 12:
                        IaRandomPlay(-1);
                        Console.WriteLine($"piority :{iaPriorityToPlay}");
                        break;
                }
            }
        }

        //---------------------------------------------------- IA program Fin ----------------------------------------------------------

        public void ComputerPlayer()
        {
            Console.WriteLine("confirmIAalreadyPlayed");
            Console.WriteLine(confirmIAalreadyPlayed);
            if (availablePositions.Count == 9 || availablePositions.Count == 8)
            {
                if (difficultyLevel == "hard")
                {
                    IaStrategicFirstPlay();
                }
                else
                {
                    IaRandomPlay(0);
                }
            }
            else
            {
                if (difficultyLevel == "easy")
                {
                    IaDiffucultyLevelEasy();
                }
                else if (difficultyLevel == "medium")
                {
                    IaDiffucultyLevelMedium();

                }
                else
                {
                    IaDiffucultyLevelHard();
                }
            }
        }

            public void ResetGame()
        {
            InitializeBoardAndAvailablePositions();
            iaPriorityToPlay = 0;
            cornerLocation = new List<(int, int)> { (0, 0), (0, 2), (2, 0), (2, 2) };
        }
    }

}
