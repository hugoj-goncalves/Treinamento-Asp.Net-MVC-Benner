using TreinamentoBenner.Core.Model;

namespace TreinamentoBennerTests.Stub
{
    public class PessoaStub
    {
        public static Pessoa Valido()
        {
            return new Pessoa { Id = 1, Nome = "Erick", Cidade = new Cidade {Nome = "Maringá", Uf = "PR"} };
        }

        public static Pessoa Invalido()
        {
            return new Pessoa();
        }
    }
}