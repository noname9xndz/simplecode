using System;

namespace GenericRepository.Entities.Interfaces
{
    public interface IsDeleteEntity
    {
        DateTime? DeletedAt { get; set; }
        bool IsDeleted { get; set; }
    }
}