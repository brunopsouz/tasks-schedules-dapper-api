using System.Net;

namespace TaskNoteManager.Exceptions.ExceptionsBase
{
    /// <summary>
    /// Classe base abstrata para todas as exceções customizadas da aplicação TaskNoteManager.
    /// 
    /// Define o contrato que obriga as exceções derivadas a fornecerem:
    /// - Uma lista de mensagens de erro detalhadas (<see cref="GetErrorMessages"/>);
    /// - Um código de status HTTP correspondente (<see cref="GetStatusCode"/>).
    /// 
    /// Herda de <see cref="SystemException"/> para permitir integração com o pipeline
    /// de tratamento global, garantindo que exceções de domínio sejam convertidas
    /// em respostas HTTP consistentes e apropriadas.
    /// </summary>
    public abstract class TaskNoteManagerExceptions : SystemException
    {
        protected TaskNoteManagerExceptions(string message) : base(message) { }

        public abstract IList<string> GetErrorMessages();
        public abstract HttpStatusCode GetStatusCode();

    }
}
