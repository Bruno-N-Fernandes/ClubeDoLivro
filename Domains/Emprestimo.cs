namespace ClubeDoLivro.Domains
{
    public class Emprestimo
    {
        public int Id { get; set; }
        public Livro Livro { get; set; }
        public Usuario Dono { get; set; }
        public Usuario Solicitante { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataPrevistaDevolucao { get; set; }
        public DateTime DataDevolucao { get; set; }

    }
}
