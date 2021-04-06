namespace Domain.model.validators
{
    public class RaceValidator : IValidator<Race>
    {
        public void Validate(Race entity)
        {
            if (entity.Name == "")
            {
                throw new ValidationException("Name is null!");
            }
        }
    }
}