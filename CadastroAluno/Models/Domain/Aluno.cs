namespace CadastroAluno.Models.Domain
{
    public class Aluno
    {
        public Guid Id { get; set; }    
        public string Nome {get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }    

    }
}
