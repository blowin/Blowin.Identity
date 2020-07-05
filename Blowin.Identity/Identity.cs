using System;

namespace Blowin
{
    public static class Identity
    {
        public static NullableIdentity<T> Nullable<T>(int? id) => (NullableIdentity<T>)id;
        public static NullableIdentity<T> Nullable<T>(int id) => (NullableIdentity<T>)id;

        public static Identity<T> NonNullable<T>(int? id) => (Identity<T>)id;
        public static Identity<T> NonNullable<T>(int id) => (Identity<T>)id;

        public static Identity<T> NonNullable<T>(NullableIdentity<T> identity) => (Identity<T>)identity;
    }

    public readonly struct Identity<T> : IEquatable<Identity<T>>, IEquatable<NullableIdentity<T>>
    {
        public int Id { get; }

        public Identity(int id) => Id = id;

        public bool Equals(Identity<T> other) => Id == other.Id;
        
        public bool Equals(NullableIdentity<T> other) => Id == other.Id;

        public override bool Equals(object obj) => obj is Identity<T> other && Equals(other);

        public override int GetHashCode() => Id;

        public override string ToString() => Id.ToString();

        #region Cast

        public static implicit operator int(Identity<T> self) => self.Id;
        
        public static implicit operator int?(Identity<T> self) => self.Id;

        public static explicit operator Identity<T>(int id) => new Identity<T>(id);
        
        public static explicit operator Identity<T>(int? id) => new Identity<T>(id.Value);

        #endregion

        public NullableIdentity<T> AsNullableIdentity() => this;
    }

    public readonly struct NullableIdentity<T> : IEquatable<NullableIdentity<T>>, IEquatable<Identity<T>>
    {
        public int? Id { get; }

        public bool HasId => Id.HasValue;

        public int UnwrapId => Id.Value;

        public NullableIdentity(int? id) => Id = id;

        public int ValueOr(int defaultId = default) => Id ?? defaultId;

        public int ValueOr(Func<int> defaultIdFactory)
        {
            if(defaultIdFactory == null)
                throw new ArgumentNullException(nameof(defaultIdFactory));

            return Id ?? defaultIdFactory();
        }

        public bool Equals(NullableIdentity<T> other) => Id == other.Id;
        public bool Equals(Identity<T> other) => Id == other.Id;

        public override bool Equals(object obj) => obj is NullableIdentity<T> other && Equals(other);

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => Id.HasValue ? Id.ToString() : "NULL";

        public Identity<T> AsIdentityOrThrow() => (Identity<T>)this;

        #region Cast

        public static implicit operator NullableIdentity<T>(Identity<T> self) => new NullableIdentity<T>(self.Id);

        public static implicit operator int?(NullableIdentity<T> self) => self.Id;

        public static explicit operator NullableIdentity<T>(int? id) => new NullableIdentity<T>(id);
        
        public static explicit operator NullableIdentity<T>(int id) => new NullableIdentity<T>(id);
        
        public static explicit operator int(NullableIdentity<T> self) => self.UnwrapId;

        public static explicit operator Identity<T>(NullableIdentity<T> self) => new Identity<T>(self.UnwrapId);

        #endregion
    }
}