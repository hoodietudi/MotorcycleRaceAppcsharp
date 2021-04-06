namespace Domain.model.validators
{
    public class EntryValidator : IValidator<Entry>
    {
        public void Validate(Entry entity)
        {
            if (entity.ParticipantId < 0)
            {
                throw new ValidationException("ParticipantId is not valid!");
            }
        }
    }
}