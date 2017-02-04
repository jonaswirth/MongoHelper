# MongoHelper
C# .NET Core library that helps accessing and performing CRUD operations on a MongoDB database using the official driver 

# Install
Get it from nuget.org (https://www.nuget.org/packages/MongoHelper/1.0.0) or install it using the package manager console:
PM> Install-Package MongoHelper

# Dependencies
- MongoDB.Driver (>= 2.4.0) (https://docs.mongodb.com/ecosystem/drivers/csharp/)
- NETStandard.Library (>= 1.6.0)

# Use
**Create instance**

``MongoHelper mongo = new MongoHelper(string DbConnection)``

**Select Count**<br/>

Select the count of all documents in "collection1"<br/>
``int count = mongo.SelectCount("collection1")``<br/>

Select count with filter criteria (string, object)<br/>
``int count = mongo.SelectCount("collection1", "Key", "Value")``<br/>

**Select**

**Insert**

**Update**

**Delete**

**Builder**



