using System;

namespace Blowin
{
    public static partial class Identity
    {
        
    }

    public class s
    {
        public class GuidPerson
        {
            public Guid Id { get; }
            public string Name { get; }
        }
        
        public class IntPerson
        {
            public int Id { get; }
            public string Name { get; }
        }
        
        public void Run()
        {
            var gp = new GuidPerson();

            NullableIdentityGuid<GuidPerson> nGpId = Identity.Nullable<GuidPerson>(gp.Id);
            IdentityGuid<GuidPerson> gpId = Identity.NonNullable<GuidPerson>(gp.Id);
            
            var ip = new IntPerson();
            NullableIdentityInt<IntPerson> nIpId = Identity.Nullable<IntPerson>(ip.Id);
            IdentityInt<IntPerson> ipId = Identity.NonNullable<IntPerson>(ip.Id);
        }
    }
}