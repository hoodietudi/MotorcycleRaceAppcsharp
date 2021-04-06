namespace Domain.model.validators
{
    public class MotorcycleValidator : IValidator<Motorcycle>
    {
        public void Validate(Motorcycle entity)
        {
            if (entity.Id < 0)
            {
                throw new ValidationException("Id is invalid!");
            }
        }
    }
}