namespace Domain.model.validators
{
    public class ParticipantValidator : IValidator<Participant>
    {
        public void Validate(Participant entity)
        {
            if (entity.Motorcycle == null)
            {
                throw new ValidationException("Motorcycle is null");
            }

            if (entity.Name == "")
            {
                throw new ValidationException("Name is empty");
            }
        }
    }
}