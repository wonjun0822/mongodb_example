namespace mongodb_example.Attribute;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class MongoCollectionAttribute : System.Attribute
{
    public MongoCollectionAttribute(string collectionName)
    {
        CollectionName = collectionName;
    }

    public string CollectionName { get; }
}