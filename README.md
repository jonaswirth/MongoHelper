# MongoHelper
C# .NET Core library that helps accessing and performing CRUD operations on a MongoDB database using the official MongoDB driver.

# Install
Get it from nuget.org (https://www.nuget.org/packages/MongoHelper/1.0.1) or install it using the package manager console:
PM> Install-Package MongoHelper

# Dependencies
- MongoDB.Driver (>= 2.4.0) (https://docs.mongodb.com/ecosystem/drivers/csharp/)
- NETStandard.Library (>= 1.6.0)

# Use
**Create instance**

``MongoHelper mongo = new MongoHelper(string DbConnection)``<br/>
Since the MongoHelper class implements IDisposable it can be used in a using statement aswell.<br/>
``using(MongoHelper mongo = new MongoHelper(string DbConnection))
{
  ...
}``
<br/>

**Select Count**<br/>

Select the count of all documents in "collection1"<br/>
``int count = mongo.SelectCount("collection1")``<br/>

Select count with filter criteria (string, object)<br/>
``int count = mongo.SelectCount("collection1", "Key", "Value")``<br/>

**Select**

**Select One**

**Insert**

**Update**

**Delete**

**Builder**



