using System;

namespace Blowin
{
    public static partial class Identity
    {
        public static NullableIdentityGuid<T> Nullable<T>(Guid? id) => (NullableIdentityGuid<T>)id;
        public static NullableIdentityGuid<T> Nullable<T>(Guid id) => (NullableIdentityGuid<T>)id;

        public static IdentityGuid<T> NonNullable<T>(Guid? id) => (IdentityGuid<T>)id;
        public static IdentityGuid<T> NonNullable<T>(Guid id) => (IdentityGuid<T>)id;

        public static IdentityGuid<T> NonNullable<T>(NullableIdentityGuid<T> identityInt) => (IdentityGuid<T>)identityInt;
    }
    
    public readonly struct IdentityGuid<T> : IEquatable<IdentityGuid<T>>, IEquatable<NullableIdentityGuid<T>>
    {
        public Guid Id { get; }

        public IdentityGuid(Guid id) => Id = id;

        public bool Equals(IdentityGuid<T> other) => Id == other.Id;
        
        public bool Equals(NullableIdentityGuid<T> other) => Id == other.Id;

        public override bool Equals(object obj) => obj is IdentityGuid<T> other && Equals(other);

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => Id.ToString();

        #region Cast

        public static implicit operator Guid(IdentityGuid<T> self) => self.Id;
        
        public static implicit operator Guid?(IdentityGuid<T> self) => self.Id;

        public static explicit operator IdentityGuid<T>(Guid id) => new IdentityGuid<T>(id);
        
        public static explicit operator IdentityGuid<T>(Guid? id) => new IdentityGuid<T>(id.Value);

        #endregion

        public NullableIdentityGuid<T> AsNullableIdentity() => this;
    }

    public readonly struct NullableIdentityGuid<T> : IEquatable<NullableIdentityGuid<T>>, IEquatable<IdentityGuid<T>>
    {
        public Guid? Id { get; }

        public bool HasId => Id.HasValue;

        public Guid UnwrapId => Id.Value;

        public NullableIdentityGuid(Guid? id) => Id = id;

        public Guid ValueOr(Guid defaultId = default) => Id ?? defaultId;

        public Guid ValueOr(Func<Guid> defaultIdFactory)
        {
            if(defaultIdFactory == null)
                throw new ArgumentNullException(nameof(defaultIdFactory));

            return Id ?? defaultIdFactory();
        }

        public bool Equals(NullableIdentityGuid<T> other) => Id == other.Id;
        public bool Equals(IdentityGuid<T> other) => Id == other.Id;

        public override bool Equals(object obj) => obj is NullableIdentityGuid<T> other && Equals(other);

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => Id.HasValue ? Id.ToString() : "NULL";

        public IdentityGuid<T> AsIdentityOrThrow() => (IdentityGuid<T>)this;

        #region Cast

        public static implicit operator NullableIdentityGuid<T>(IdentityGuid<T> self) => new NullableIdentityGuid<T>(self.Id);

        public static implicit operator Guid?(NullableIdentityGuid<T> self) => self.Id;

        public static explicit operator NullableIdentityGuid<T>(Guid? id) => new NullableIdentityGuid<T>(id);
        
        public static explicit operator NullableIdentityGuid<T>(Guid id) => new NullableIdentityGuid<T>(id);
        
        public static explicit operator Guid(NullableIdentityGuid<T> self) => self.UnwrapId;

        public static explicit operator IdentityGuid<T>(NullableIdentityGuid<T> self) => new IdentityGuid<T>(self.UnwrapId);

        #endregion
    }
}