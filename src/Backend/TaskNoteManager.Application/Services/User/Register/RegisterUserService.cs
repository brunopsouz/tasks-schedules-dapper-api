using AutoMapper;
using FluentValidation.Results;
using TaskNoteManager.Communication.Requests;
using TaskNoteManager.Communication.Responses;
using TaskNoteManager.Domain.DataAccess.UnitOfWork;
using TaskNoteManager.Domain.Repositories.User;
using TaskNoteManager.Exceptions.ExceptionsBase;

namespace TaskNoteManager.Application.Services.User.Register
{
    /// <summary>
    /// Service responsible to registration new users.
    /// Performs validation, maps incoming data to the domain entity,
    /// persists it to the database, and manages the transaction lifecycle.
    /// </summary>
    public class RegisterUserService : IRegisterUserService
    {
        private readonly IUserReadOnlyRepository _readOnlyRepository;
        private readonly IUserWriteOnlyRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserService(
            IUserReadOnlyRepository readOnlyRepository,
            IUserWriteOnlyRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _readOnlyRepository = readOnlyRepository;
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Registers a new user in the system.
        /// Executes validation, starts a transaction, saves the data, and returns a formatted response.
        /// In case of failure, rolls back the transaction.
        /// </summary>
        /// <param name="request">User data to be registered.</param>
        /// <returns>Information about the newly registered user.</returns>
        public async Task<ResponseRegisterUser> Register(RequestRegisterUser request)
        {
            // Begin tran
            _unitOfWork.BeginTransaction();

            try
            {
                // 1. Validate.
                await Validate(request);

                // 2. Map.
                var user = _mapper.Map<Domain.Entities.User>(request);

                // 3.Add in database.
                await _repository.AddAsync(user);

                // 4. Commit transaction.
                _unitOfWork.Commit();

                // 5. Return response.
                return new ResponseRegisterUser
                {
                    Name = request.Name,
                    Email = request.Email,
                    Position = request.Position,
                    UserType = request.UserType,
                };
            }
            catch
            {
                // Rollback Transaction
                _unitOfWork.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Validates the incoming user data, including business rules such as duplicate email checks.
        /// </summary>
        /// <param name="request">User data to validate.</param>
        /// <returns>Asynchronous task that throws an exception if validation fails.</returns>
        /// <exception cref="Exception">Thrown when validation fails.</exception>
        private async Task Validate(RequestRegisterUser request)
        {
            var validator = new RegisterUserValidator();

            var result = await validator.ValidateAsync(request);

            // Method to check if e-mail exists.
            var existsEmail = await _readOnlyRepository.ExistsUserWithEmail(request.Email);

            if (existsEmail)
                result.Errors.Add(new ValidationFailure(string.Empty,"E-mail already exists."));

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }

        }
    }
}
