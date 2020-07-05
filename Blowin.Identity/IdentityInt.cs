using System;

namespace Blowin
{
    public static partial class Identity
    {
        public static NullableIdentityInt<T> Nullable<T>(int? id) => (NullableIdentityInt<T>)id;
        public static NullableIdentityInt<T> Nullable<T>(int id) => (NullableIdentityInt<T>)id;

        public static IdentityInt<T> NonNullable<T>(int? id) => (IdentityInt<T>)id;
        public static IdentityInt<T> NonNullable<T>(int id) => (IdentityInt<T>)id;

        public static IdentityInt<T> NonNullable<T>(NullableIdentityInt<T> identityInt) => (IdentityInt<T>)identityInt;
    }
    
    public readonly struct IdentityInt<T> : IEquatable<IdentityInt<T>>, IEquatable<NullableIdentityInt<T>>
    {
        public int Id { get; }

        public IdentityInt(int id) => Id = id;

        public bool Equals(IdentityInt<T> other) => Id == other.Id;
        
        public bool Equals(NullableIdentityInt<T> other) => Id == other.Id;

        public override bool Equals(object obj) => obj is IdentityInt<T> other && Equals(other);

        public override int GetHashCode() => Id;

        public override string ToString() => Id.ToString();

        #region Cast

        public static implicit operator int(IdentityInt<T> self) => self.Id;
        
        public static implicit operator int?(IdentityInt<T> self) => self.Id;

        public static explicit operator IdentityInt<T>(int id) => new IdentityInt<T>(id);
        
        public static explicit operator IdentityInt<T>(int? id) => new IdentityInt<T>(id.Value);

        #endregion

        public NullableIdentityInt<T> AsNullableIdentity() => this;
    }

    public readonly struct NullableIdentityInt<T> : IEquatable<NullableIdentityInt<T>>, IEquatable<IdentityInt<T>>
    {
        public int? Id { get; }

        public bool HasId => Id.HasValue;

        public int UnwrapId => Id.Value;

        public NullableIdentityInt(int? id) => Id = id;

        public int ValueOr(int defaultId = default) => Id ?? defaultId;

        public int ValueOr(Func<int> defaultIdFactory)
        {
            if(defaultIdFactory == null)
                throw new ArgumentNullException(nameof(defaultIdFactory));

            return Id ?? defaultIdFactory();
        }

        public bool Equals(NullableIdentityInt<T> other) => Id == other.Id;
        public bool Equals(IdentityInt<T> other) => Id == other.Id;

        public override bool Equals(object obj) => obj is NullableIdentityInt<T> other && Equals(other);

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => Id.HasValue ? Id.ToString() : "NULL";

        public IdentityInt<T> AsIdentityOrThrow() => (IdentityInt<T>)this;

        #region Cast

        public static implicit operator NullableIdentityInt<T>(IdentityInt<T> self) => new NullableIdentityInt<T>(self.Id);

        public static implicit operator int?(NullableIdentityInt<T> self) => self.Id;

        public static explicit operator NullableIdentityInt<T>(int? id) => new NullableIdentityInt<T>(id);
        
        public static explicit operator NullableIdentityInt<T>(int id) => new NullableIdentityInt<T>(id);
        
        public static explicit operator int(NullableIdentityInt<T> self) => self.UnwrapId;

        public static explicit operator IdentityInt<T>(NullableIdentityInt<T> self) => new IdentityInt<T>(self.UnwrapId);

        #endregion
    }
}