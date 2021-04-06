namespace Domain.model.validators
{
    public class TeamValidator : IValidator<Team>
    {
        public void Validate(Team entity)
        {
            if (entity.Name == "")
            {
                throw new ValidationException("Name is null");
            }
        }
    }
}