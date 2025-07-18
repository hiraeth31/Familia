﻿namespace Familia.Domain.Shared.EntityIds
{
    public record SpeciesId
    {
        private SpeciesId(Guid value)
        {
            Value = value;
        }
        public Guid Value { get; }
        public static SpeciesId NewSpeciesId() => new(Guid.NewGuid());
        public static SpeciesId EmptyId() => new(Guid.Empty);
        public static SpeciesId Create(Guid id) => new(id);
    }
}
