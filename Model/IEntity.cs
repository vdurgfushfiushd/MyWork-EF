using MongoDB.Bson;

namespace Model
{
    public interface IEntity
    {
        ObjectId Id { get; set; }
    }
}
