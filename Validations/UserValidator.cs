using ApiDevBP.Exceptions;
using ApiDevBP.Models;

namespace ApiDevBP.Validations
{
    public class UserValidator : IUserValidator
    {
        #region Public Methods

        public void Validate(UserModel user)
        {
            if (!ValidateId(user.Id))
                throw new UserException("El ID debe ser mayor que 0.");

            if (!ValidateIsNullOrEmptyAndLength(user.Name))
                throw new UserException("El nombre no debe ser nulo o vacío y debe tener una longitud máxima de 50 caracteres.");

            if (!ValidateIsNullOrEmptyAndLength(user.Lastname))
                throw new UserException("El apellido no debe ser nulo o vacío y debe tener una longitud máxima de 50 caracteres.");

        }

        public void ValidateUserId(int id)
        {
            if (!ValidateId(id))
                throw new UserException("El ID debe ser mayor que 0.");
        }


        public bool ValidateUser(UserModel user)
        {
            return ValidateName(user.Name) && ValidateLastname(user.Lastname);
        }

        #endregion

        #region Private Methods

        private bool ValidateId(int id)
        {
            return id > 0;
        }

        private bool ValidateName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name.Length <= 50;
        }

        private bool ValidateLastname(string lastname)
        {
            return !string.IsNullOrWhiteSpace(lastname) && lastname.Length <= 50;
        }

        private bool ValidateIsNullOrEmptyAndLength(string field)
        {
            return !string.IsNullOrWhiteSpace(field) && field.Length <= 50;
        }

        #endregion
    }

    public interface IUserValidator
    {
        void Validate(UserModel user);
        void ValidateUserId(int id);
        bool ValidateUser(UserModel user);
    }
    
}
