using EShopper.Core.Entities;

namespace EShopper.Entities.Models
{
    public class Permission : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}