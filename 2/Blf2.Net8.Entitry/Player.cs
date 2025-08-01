namespace Blf2.Net8.Entitry {
    public class Player {
        public Guid Id { get; set; }
        public string Account { get; set; }
        public string AccountType { get; set; }
        public DateTime DateCreate { get; set; }
        public ICollection<Character> Characters { get; set; }

        public Player() {
            this.DateCreate = DateTime.UtcNow; 
        }
        


    }
}
