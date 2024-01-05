namespace laba4oop.Entities
{
    public class TicTacToeGame
    {
        public PlayerEntity _player1;
        public PlayerEntity _player2;
        private readonly string[,] _board = new string[3, 3];
        private string _currentPlayerSymbol = "X";
        public PlayerEntity Winner { get; private set; }
        public bool IsDraw { get; private set; }

        public TicTacToeGame(PlayerEntity player1, PlayerEntity player2)
        {
            _player1 = player1;
            _player2 = player2;
        }
        public PlayerEntity Player1
        {
            get { return _player1; }
        }

        public PlayerEntity Player2
        {
            get { return _player2; }
        }

        public void EndGame()
        {
            if (CheckForWin())
            {
                Winner = _currentPlayerSymbol == "X" ? _player1 : _player2;
            }
            else if (CheckForDraw())
            {
                IsDraw = true;
            }
          
        }


        public void MakeMove(int row, int col)
        {
            _board[row, col] = _currentPlayerSymbol;
            if (CheckForWin())
            {
                Winner = _currentPlayerSymbol == "X" ? _player1 : _player2;
                Winner.UpdateRating(true);
                var loser = Winner == _player1 ? _player2 : _player1;
                loser.UpdateRating(false);
                FinishGame();
            }
            else if (CheckForDraw())
            {
                IsDraw = true;
            }
            _currentPlayerSymbol = _currentPlayerSymbol == "X" ? "O" : "X";
            
        }
        public void FinishGame()
        {
            _player1.GamesCount++;
            _player2.GamesCount++;
        }

        public bool IsMoveValid(int row, int col)
        {
            return row >= 0 && row < 3 && col >= 0 && col < 3 && _board[row, col] == null;
        }

        public bool CheckForWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (_board[i, 0] == _board[i, 1] && _board[i, 1] == _board[i, 2] && _board[i, 0] != null)
                    return true;
            }

            for (int i = 0; i < 3; i++)
            {
                if (_board[0, i] == _board[1, i] && _board[1, i] == _board[2, i] && _board[0, i] != null)
                    return true;
            }

            if (_board[0, 0] == _board[1, 1] && _board[1, 1] == _board[2, 2] && _board[0, 0] != null)
                return true;
            if (_board[0, 2] == _board[1, 1] && _board[1, 1] == _board[2, 0] && _board[0, 2] != null)
                return true;

            return false;
        }

        public bool CheckForDraw()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (_board[row, col] == null)
                        return false; 
                }
            }
            return !CheckForWin();
        }


        public void PrintBoard()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Console.Write(_board[row, col] ?? "-");
                    if (col < 2)
                        Console.Write("|");
                }
                Console.WriteLine();
            }
        }
    }
}
