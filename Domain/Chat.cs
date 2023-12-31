using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sparkle.Domain;

[BsonDiscriminator(RootClass = true)]
[BsonKnownTypes(typeof(PersonalChat), typeof(GroupChat), typeof(Channel))]
public abstract class Chat
{
    /// <summary>
    /// Unique Id as an string representation of an ObjectId type
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    /// <summary>
    /// Members of the chat
    /// </summary>
    public virtual List<Guid> Profiles { get; set; } = new();

    public virtual DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public virtual DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

    public Chat()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
}