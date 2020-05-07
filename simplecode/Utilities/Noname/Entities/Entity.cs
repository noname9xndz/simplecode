namespace GenericRepository.Entities
{
    using GenericRepository.Entities.Interfaces;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///  Abstract entity class
    /// </summary>
    /// <typeparam name="TId">The Id type</typeparam>
    public abstract class Entity<TId> : IEntity<TId> where TId : struct
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TId Id { get; set; }
    }
}