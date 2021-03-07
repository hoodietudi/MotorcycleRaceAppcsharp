namespace MotorcycleContest.repository.validators
{
    public interface IValidator<in TE>
    {
        void Validate(TE entity);
    }
}