# Identity

# Guid Sample
```csharp
public class GuidPerson
{
    public Guid Id { get; }
    public string Name { get; }
}

var gp = new GuidPerson();

NullableIdentityGuid<GuidPerson> nGpId = Identity.Nullable<GuidPerson>(gp.Id);
IdentityGuid<GuidPerson> gpId = Identity.NonNullable<GuidPerson>(gp.Id);
```

# Int Sample

```csharp
public class IntPerson
{
    public int Id { get; }
    public string Name { get; }
}

var ip = new IntPerson();
NullableIdentityInt<IntPerson> nIpId = Identity.Nullable<IntPerson>(ip.Id);
IdentityInt<IntPerson> ipId = Identity.NonNullable<IntPerson>(ip.Id);
```
