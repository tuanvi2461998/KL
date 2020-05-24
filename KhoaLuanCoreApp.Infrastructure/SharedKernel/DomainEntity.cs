namespace KhoaLuanCoreApp.Infrastructure.SharedKernel
{
    public abstract class DomainEntity<T>
    {
        public T Id { get; set; }

        /// <summary>
        /// True if domain entity has an Identity
        /// </summary>
        /// <returns></returns>
        public bool isTransient()
        {
            return Id.Equals(default(T));
        }
    }
}