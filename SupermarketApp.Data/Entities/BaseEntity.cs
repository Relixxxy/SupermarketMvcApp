using SupermarketApp.Data.Entities.Interfaces;

namespace SupermarketApp.Data.Entities
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
