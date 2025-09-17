using System.Net;

namespace TaskNoteManager.Exceptions.ExceptionsBase
{
    /// <summary>
    /// Exceção lançada quando ocorre falha de validação em regras de negócio ou de entrada de dados.
    /// 
    /// Contém uma lista de mensagens de erro descritivas e retorna o status HTTP 400 (Bad Request)
    /// para o cliente, garantindo que requisições inválidas sejam tratadas de forma apropriada.
    /// </summary>
    public class ErrorOnValidationException : TaskNoteManagerExceptions
    {
        private readonly IList<string> _errorMessages;

        public ErrorOnValidationException(IList<string> message) : base(string.Empty)
        {
            _errorMessages = message;
        }

        public override IList<string> GetErrorMessages() => _errorMessages;

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
