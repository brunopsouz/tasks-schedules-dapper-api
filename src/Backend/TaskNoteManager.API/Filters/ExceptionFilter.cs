using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskNoteManager.Communication.Responses;
using TaskNoteManager.Exceptions.ExceptionsBase;

namespace TaskNoteManager.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Filtro global de tratamento de exceções da aplicação.
        /// Intercepta exceções lançadas durante a execução das actions e converte em respostas HTTP padronizadas.
        /// 
        /// - Se a exceção for do tipo <see cref="TaskNoteManagerExceptions"/>, retorna o status code e as mensagens de erro definidos na exceção.
        /// - Caso contrário, retorna um erro genérico 500 (Internal Server Error) com mensagem padrão.
        /// 
        /// Esse filtro garante que as respostas de erro sejam consistentes e adequadas,
        /// evitando que exceções não tratadas exponham detalhes internos da aplicação.
        /// </summary>
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is TaskNoteManagerExceptions exceptions)
                HandleProjectException(exceptions, context);
            else
                ThrowUnknowException(context);
        }

        private static void HandleProjectException(TaskNoteManagerExceptions exceptions, ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)exceptions.GetStatusCode();
            context.Result = new ObjectResult(new ResponseErrorJson(exceptions.GetErrorMessages()));

        }

        private static void ThrowUnknowException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorJson("ResourceMessagesException.UNKNOWN_ERROR"));
        }
    }
}
