namespace IESB_TC2S2015.Model
{
    [SQLite.Net.Attributes.Table(nameof(Contato))]
    public class Contato
    {
        [SQLite.Net.Attributes.Column(nameof(ID)),
            SQLite.Net.Attributes.PrimaryKey,
            SQLite.Net.Attributes.AutoIncrement]
        public int? ID { get; set; }

        [SQLite.Net.Attributes.MaxLength(100),
            SQLite.Net.Attributes.NotNull]
        public string Nome { get; set; }

        [SQLite.Net.Attributes.MaxLength(100)]
        public string Email { get; set; }

        [SQLite.Net.Attributes.MaxLength(40)]
        public string Telefone { get; set; }
        public bool IsFavorito { get; set; }

        public Contato() { }

        public Contato(string nome)
            : this()
        {
            this.Nome = nome;
        }
        public Contato(string nome, string email, string telefone = "", bool isFavorito = false)
            : this(nome)
        {
            this.Nome = nome;
            this.Email = email;
            this.Telefone = telefone;
            this.IsFavorito = isFavorito;
        }
    }
}
