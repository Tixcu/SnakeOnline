using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;

namespace SnakeGame
{
    public class Game
    {
        public Cell[] Cells;
        private int Width;
        private int Height;

        string Token;
        string User;
        Label UserScore;

        Tuple<int, string> TopScore = new Tuple<int, string>(0,"0");
        TextBlock TopScorer;

        public int CurrentRound;
        GameboardResponse CurrentState;
        List<PlayerState> OldPlayers;

        

        public Game(ItemsControl Board, Label ScoreLabel, TextBlock TopScorer, string Token, int Width = 50, int Height = 60, bool Spectate = false)
        {
            this.Token = Token;
            this.Width = Width;
            this.Height = Height;
            UserScore = ScoreLabel;
            this.TopScorer = TopScorer;

            Cells = new Cell[Width * Height];
            for (int i = 0; i < Width * Height; i++)
            {
                Cells[i] = new Cell(Piece.Free, i);
            }
            Board.ItemsSource = Cells;
        }

        public async void StartGame()
        {
            await GetName();
            await Update();
            InitialDraw();
            DrawSnakes();
            System.Threading.Thread.Sleep(CurrentState.TimeUntilNextTurnMilliseconds);
            CurrentRound = CurrentState.TurnNumber;

            Timer _Timer = new Timer(CurrentState.TurnTimeMilliseconds);
            _Timer.Elapsed += async (object Sender, ElapsedEventArgs e) =>
            {
                CurrentRound = CurrentState.RoundNumber;
                await Update();
                TopScore = new Tuple<int, string>(0, "0");
                if (CurrentState.RoundNumber != CurrentRound)
                {
                    _Timer.Dispose();
                    ClearField();
                    StartGame();
                }
                DrawSnakes();

            };
            _Timer.Start();
        }

        public void ClearField()
        {
            for (int i = 0; i < Width * Height; i++)
            {
                Cells[i].ChangeType(Piece.Free);
            }
        }

        public void DrawSnakes()
        {
            foreach (PlayerState Player in OldPlayers ?? Enumerable.Empty<PlayerState>()) // Clears Old Snakes
            {
                foreach (Point BodyPiece in Player.Snake ?? Enumerable.Empty<Point>())
                {
                    Cells[BodyPiece.X + Width * BodyPiece.Y].ChangeType(Piece.Free);
                }
            }


            foreach (PlayerState Player in CurrentState.Players.Skip(1))
            {
                if (Player.Snake != null)
                {
                    if (Player.IsSpawnProtected)
                    {
                        foreach (Point BodyPiece in Player.Snake)
                        {
                            Cells[BodyPiece.X + Width * BodyPiece.Y].ChangeType(Piece.Head);
                        }
                    }
                    else
                    {
                        if (Player.Name == User)
                        {
                            UserScore.Dispatcher.BeginInvoke((Action)(() => { UserScore.Content = Player.Snake.Count.ToString(); }));
                            int i = Player.Snake.Count;
                            Cells[Player.Snake[0].X + Width * Player.Snake[0].Y].ChangeType(Piece.Head);
                            foreach (Point BodyPiece in Player.Snake.Skip(1))
                            {
                                Cells[BodyPiece.X + Width * BodyPiece.Y].ChangeType(Piece.Body);
                            }
                        } else
                        {
                            Cells[Player.Snake[0].X + Width * Player.Snake[0].Y].ChangeType(Piece.Head);
                            foreach (Point BodyPiece in Player.Snake.Skip(1))
                            {
                                Cells[BodyPiece.X + Width * BodyPiece.Y].ChangeType(Piece.Body1);
                            }
                        }
                        if (Player.Snake.Count > TopScore.Item1)
                        {
                            TopScore = new Tuple<int, string>(Player.Snake.Count, Player.Name); //Top score for round
                        }
                    }
                }
            }

            TopScorer.Dispatcher.BeginInvoke((Action)(() => { TopScorer.Text = (TopScore.Item1.ToString() + " - " + TopScore.Item2); })); //Update Top Score

            foreach (Point Food in CurrentState.Food) //DrawsFood
            {
                if (Cells[Food.X + Food.Y * 50].Type != Piece.Food)
                {
                    Cells[Food.X + Food.Y * 50].ChangeType(Piece.Food);
                }
            }
            OldPlayers = CurrentState.Players;
        }

        public void InitialDraw() //Draw walls only once per game round
        {
            HashSet<int> FlatenedCords = new HashSet<int>();
            foreach (Wall Wall in CurrentState.Walls)
            {
                for (int i = 0; i < Wall.Height; ++i) // Check if width + 1 or width
                {
                    for (int j = 0; j < Wall.Width; ++j)
                    {
                        FlatenedCords.Add((j + Wall.X) + (Wall.Y + i) * Width);
                    }
                }
            }
            foreach (int Cord in FlatenedCords)
            {
                Cells[Cord].ChangeType(Piece.Wall);
            }
        }

        public async Task  GetName()
        {
            SnakeOnlineClient Geter = new SnakeOnlineClient();
            User = await Geter.GetNameAsync(Token);
            if (User == null)
            {
                throw new ArgumentException("No access to server");
            }
        }

        public async Task Update()
        {
            SnakeOnlineClient Geter = new SnakeOnlineClient();
            CurrentState = await Geter.GetGameboardAsync();
        }
    }
}
