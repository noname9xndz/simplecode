namespace GenericRepository.Entities
{
    using GenericRepository.Entities.Interfaces;
    using System;

    /// <summary>
    /// Entity class with auditable fields
    /// </summary>
    public abstract partial class AuditableAddDelete<TId> : Entity<TId>, IAuditableEntity, IsDeleteEntity where TId : struct
    {
        /// <summary>
        /// Gets or sets primary key or Id field
        /// </summary>
        object IAuditableEntity.Id { get { return this.Id; } set { } }

        /// <summary>
        /// Gets or sets CreatedBy
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets Created Date
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets Modified by
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets ModifiedDate
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets status
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets DeletedAt
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// Gets or sets IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}