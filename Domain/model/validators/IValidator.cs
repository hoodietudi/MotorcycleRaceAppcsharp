namespace Domain.model.validators
{
    public interface IValidator<in TE>
    {
        void Validate(TE entity);
    }
}