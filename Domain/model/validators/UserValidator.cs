namespace Domain.model.validators
{
    public class UserValidator : IValidator<User>
    {
        public void Validate(User entity)
        {
            if (entity.Username == "")
            {
                throw new ValidationException("Username is invalid");
            }
        }
    }
}