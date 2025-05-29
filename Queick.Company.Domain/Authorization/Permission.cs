//namespace Queick.Shared.Domain.Authorization;

// public sealed class Permission : ValueObject
// {
//     public string Name { get; }
//     
//     private Permission(string name)
//     {
//         Name = name;
//     }
//     
//     public static Permission Create(string name) => new(name);
//     
//     protected override IEnumerable<object> GetEqualityComponents()
//     {
//         yield return Name;
//     }
//     
//     // Common Permission Constants
//     public static class Common
//     {
//         public static Permission Read => Create("read");
//         public static Permission Create => Create("create");
//         public static Permission Update => Create("update");
//         public static Permission Delete => Create("delete");
//     }
// }