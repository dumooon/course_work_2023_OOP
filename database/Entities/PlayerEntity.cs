namespace laba4oop.Entities
{
    public class PlayerEntity
    {
        public int Id { get; set; } 
        public string UserName { get; set; }
        public int CurrentRating { get; set; }
        public int GamesCount { get; set; }
        public void UpdateRating(bool isWinner)
        {
            if (isWinner)
            {
                CurrentRating += 10;
            }
            else
            {
                CurrentRating = Math.Max(0, CurrentRating - 10);
            }
        }
    }
}