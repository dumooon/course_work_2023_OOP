using System;
using laba4oop.Entities;
using laba4oop.Service.Base;
using laba4oop.Commands.Base;
using laba4oop.Simulation;

namespace laba4oop.Commands
{
    public class StartGameCommand : ICommand
    {
        private readonly IPlayerService _playerService;
        private readonly IGameService _gameService;
        private readonly GameFactory _gameFactory;

        public StartGameCommand(IPlayerService playerService, IGameService gameService, GameFactory gameFactory)
        {
            _playerService = playerService;
            _gameService = gameService;
            _gameFactory = gameFactory;
        }
        public string GetCommandInfo()
        {
            return "Start a new Tic-Tac-Toe game";
        }

        public void Execute()
        {
            Console.WriteLine("Enter ID of the first player");
            var player1Id = int.Parse(Console.ReadLine() ?? string.Empty);
            var player1 = _playerService.GetPlayerById(player1Id);

            Console.WriteLine("Enter ID of the second player");
            var player2Id = int.Parse(Console.ReadLine() ?? string.Empty);
            var player2 = _playerService.GetPlayerById(player2Id);

            Console.WriteLine("Starting Tic-Tac-Toe game...");

            var ticTacToeGame = new TicTacToeGame(player1, player2);
            var currentPlayer = player1;
            var isGameOver = false;
            string winner = null;

            // Main game loop
            while (!isGameOver)
            {
                Console.Clear();
                ticTacToeGame.PrintBoard();

                Console.WriteLine($"Player {currentPlayer.UserName}'s turn (Enter row and column number): ");
                int row = Convert.ToInt32(Console.ReadLine());
                
                int col = Convert.ToInt32(Console.ReadLine());

                if (!ticTacToeGame.IsMoveValid(row, col))
                {
                    Console.WriteLine("Invalid move. Try again.");
                    continue;
                }

                ticTacToeGame.MakeMove(row, col);
                isGameOver = ticTacToeGame.CheckForWin() || ticTacToeGame.CheckForDraw();

                if (isGameOver)
                {
                    winner = currentPlayer.UserName;
                    Console.Clear();
                    ticTacToeGame.PrintBoard();
                    Console.WriteLine(winner == null ? "It's a draw!" : $"Player {winner} wins!");
                }

                currentPlayer = currentPlayer == player1 ? player2 : player1;
            }
        }
    }
}
