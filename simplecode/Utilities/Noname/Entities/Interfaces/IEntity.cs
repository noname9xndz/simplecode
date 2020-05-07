namespace GenericRepository.Entities.Interfaces
{
    /// <summary>
    /// Generic interface for basic EF entity with Id field
    /// </summary>
    /// <typeparam name="TId">Id data type</typeparam>
    public interface IEntity<TId> : IEntity where TId : struct
    {
        /// <summary>
        /// Gets or sets Id (Primary key) field
        /// </summary>
        TId Id { get; set; }
    }

    /// <summary>
    /// Marker interface for basic EF entity without id field
    /// </summary>
    public interface IEntity { }
}