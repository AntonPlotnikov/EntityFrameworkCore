using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json.Linq;
// ReSharper disable InconsistentNaming

namespace CosmosConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting sample...");
            new Sample().Run();
            Console.WriteLine("The end.");
        }
    }

    public class Sample
    {
#pragma warning disable IDE1006 // Naming Styles
        private static readonly string Endpoint = "https://localhost:8081";
        private static readonly string Key = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        //private static readonly string DatabaseId = "Northwind";
        //private static readonly string CollectionId = "Customers";
        private readonly DocumentClient _client;

        public Sample()
        {
            _client = new DocumentClient(new Uri(Endpoint), Key);
        }

        public void Run()
        {
            // Database/Collection management
            //CheckDatabase("Northwind");
            //CheckHasCollections("Northwind");
            //CreateDatabase("Northwind");
            //CreateCollection("Northwind", "Customers");

            //CheckDatabase("GearsOfWar");
            //CreateDatabase("GearsOfWar");
            //CreateCollection("GearsOfWar", "Customers");

            //DeleteDatabase("GearsOfWar");

            //// DML
            //CreateDatabase("Sample");
            //CreateCollection("Sample", "Blogs");
            //SaveBlogs("Sample", "Blogs");
            //DeleteDatabase("Sample");

            //// Query
            //DoQuery();

            var dateTime = DateTime.Parse("1/1/1998 12:00:00 PM");

            var query = _client.CreateDocumentQuery(
                UriFactory.CreateDocumentCollectionUri("Northwind", "Orders"),
                new SqlQuerySpec
                {
                    QueryText = "Select * from Orders as c where c.OrderDate > @p1",
                    Parameters = new SqlParameterCollection(new[] { new SqlParameter("@p1", dateTime) })
                },
                new FeedOptions()
                {
                    EnableCrossPartitionQuery = true,
                    EnableScanInQuery = true
                })
                .ToList();

        }

        public class Blog
        {
            public string id { get; set; }
            public string Name { get; set; }
            public int Value { get; set; }
        }


        private void CheckDatabase(string databaseId)
        {
            Console.WriteLine("Checking existence of database:" + databaseId);
            try
            {
                _client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseId)).GetAwaiter().GetResult();
                Console.WriteLine("Database exists.");
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Database does not exist.");
                }
                else
                {
                    throw;
                }
            }
        }

        private void CheckHasCollections(string databaseId)
        {
            var value = _client.ReadDocumentCollectionFeedAsync(UriFactory.CreateDatabaseUri(databaseId)).GetAwaiter().GetResult().Any();

            Console.WriteLine($"Does {databaseId} have tables? {value}");
        }

        private void CreateDatabase(string databaseId)
        {
            Console.WriteLine("Creating database " + databaseId);
            _client.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseId }).GetAwaiter().GetResult();
        }

        private void CreateCollection(string databaseId, string collectionId)
        {
            Console.WriteLine($"Creating {collectionId} in {databaseId}");
            _client.CreateDocumentCollectionIfNotExistsAsync(
                UriFactory.CreateDatabaseUri(databaseId),
                new DocumentCollection { Id = collectionId })
                .GetAwaiter().GetResult();
        }

        private void DeleteDatabase(string databaseId)
        {
            Console.WriteLine($"Deleting database {databaseId}");
            var a = _client.DeleteDatabaseAsync(UriFactory.CreateDatabaseUri(databaseId)).GetAwaiter().GetResult();

#pragma warning disable CS0219 // Variable is assigned but its value is never used
            var c = 1 + 2;
#pragma warning restore CS0219 // Variable is assigned but its value is never used
        }

        private void SaveBlogs(string databaseId, string collectionId)
        {
            var collectionUri = UriFactory.CreateDocumentCollectionUri(databaseId, collectionId);

            Console.WriteLine("Insert examples");

            Console.WriteLine("Typed blog");
            var blog = new Blog { id = "1", Name = "EF Core" };
            _client.CreateDocumentAsync(collectionUri, blog).GetAwaiter().GetResult();

            Console.WriteLine("Document");
            var doc = new Document();
            doc.SetPropertyValue("id", "2");
            doc.SetPropertyValue("Name", "asp.net Core");
            _client.CreateDocumentAsync(collectionUri, doc).GetAwaiter().GetResult();

            Console.WriteLine("JObject");
            var entity = new JObject
            {
                ["id"] = "3",
                ["Addresses"] = new JArray
                {
                    new JObject
                    {
                        ["Name"] = "A"
                    }
                }
            };
            _client.CreateDocumentAsync(collectionUri, entity).GetAwaiter().GetResult();


            Console.WriteLine("Replace examples");

            Console.WriteLine("Typed blog");
            blog = new Blog { id = "1", Name = "No Core" };
            _client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, "1"), blog).GetAwaiter().GetResult();

            Console.WriteLine("Document");
            doc = new Document();
            //doc.
            doc.SetPropertyValue("shadow", "true");
            doc.SetPropertyValue("id", "2");
            doc.SetPropertyValue("Name", "asp.net Core");
            _client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, "2"), doc).GetAwaiter().GetResult();

            Console.WriteLine("JObject");
            entity = new JObject
            {
                ["id"] = "3",
                ["extra"] = 1,
                ["Addresses"] = new JArray
                {
                    new JObject
                    {
                        ["Name"] = "A"
                    }
                }
            };
            _client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, "3"), entity).GetAwaiter().GetResult();


            Console.WriteLine("Upsert examples");

            Console.WriteLine("Typed blog");
            blog = new Blog { id = "1", Value = 10 };

            _client.UpsertDocumentAsync(collectionUri, blog).GetAwaiter().GetResult();

            Console.WriteLine("Document");
            doc = new Document();
            doc.SetPropertyValue("id", "2");
            doc.SetPropertyValue("Value", 10);
            _client.UpsertDocumentAsync(collectionUri, doc).GetAwaiter().GetResult();

            Console.WriteLine("JObject");
            entity = new JObject
            {
                ["id"] = "3",
                ["Value"] = 10
            };
            _client.UpsertDocumentAsync(collectionUri, entity).GetAwaiter().GetResult();

            _client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, "3")).GetAwaiter().GetResult();

            // ReSharper disable once UnusedVariable
            var a = _client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, "2")).GetAwaiter().GetResult().Resource;
        }

        private void DoQuery()
        {
            var collectionUri = UriFactory.CreateDocumentCollectionUri("Northwind", "Customers");

            TypedQueries(collectionUri);
            UnTypedQueries(collectionUri);
        }

        private void TypedQueries(Uri collectionUri)
        {
            // All
            var allCustomers = _client.CreateDocumentQuery<Customer>(
                collectionUri);
            Console.WriteLine(allCustomers);

            var ac = allCustomers.ToList();
            Console.WriteLine(ac.Count);

            // Take
            var takeCustomers = _client.CreateDocumentQuery<Customer>(
                collectionUri)
                .Take(10);
            Console.WriteLine(takeCustomers);

            var tc = takeCustomers.ToList();
            Console.WriteLine(tc.Count);

            // Where
            var whereCustomers = _client.CreateDocumentQuery<Customer>(
                collectionUri)
                .Where(c => c.CustomerID == "ALFKI");
            Console.WriteLine(whereCustomers);

            var wc = whereCustomers.ToList();
            Console.WriteLine(wc.Count);

            // Project
            var projectCustomers = _client.CreateDocumentQuery<Customer>(
                    collectionUri)
                .Select(c => new { c.CustomerID });
            Console.WriteLine(projectCustomers);

            var pc = projectCustomers.ToList();
            Console.WriteLine(pc.Count);

            // Project
            var dtoProjection = _client.CreateDocumentQuery<Customer>(
                collectionUri)
                .Select(c => new Temp
                {
                    CustomerID = c.CustomerID
                }
                );
            Console.WriteLine(dtoProjection);

            var dp = dtoProjection.ToList();
            Console.WriteLine(dp.Count);

            // Parameter
            var a = "ALFKI";
            var whereParameter = _client.CreateDocumentQuery<Customer>(
                    collectionUri)
                // ReSharper disable once AccessToModifiedClosure
                .Where(c => c.CustomerID == a);
            Console.WriteLine(whereParameter);

            var wp = whereParameter.ToList();
            Console.WriteLine(wp.Count);

            a = "FISSA";
            Console.WriteLine(whereParameter);

            wp = whereParameter.ToList();
            Console.WriteLine(wp.Count);
        }

        private void UnTypedQueries(Uri collectionUri)
        {
            // All
            var allCustomers = _client.CreateDocumentQuery(
                collectionUri,
                new SqlQuerySpec
                {
                    QueryText = "Select * from root"
                });
            Console.WriteLine(allCustomers);

            var ac = allCustomers.ToList();
            Console.WriteLine(ac.Count);

            // Take
            var takeCustomers = _client.CreateDocumentQuery(
                collectionUri,
                new SqlQuerySpec
                {
                    QueryText = "Select TOP 10 * from root"
                });
            Console.WriteLine(takeCustomers);

            var tc = takeCustomers.ToList();
            Console.WriteLine(tc.Count);

            // Where
            var whereCustomers = _client.CreateDocumentQuery(
                collectionUri,
                new SqlQuerySpec
                {
                    QueryText = "Select * from root c where c.CustomerID = \"ALFKI\""
                });
            Console.WriteLine(whereCustomers);

            foreach (Document whereCustomer in whereCustomers)
            {
                Console.WriteLine(whereCustomer.GetPropertyValue<string>("CustomerID"));
            }

            var wc = whereCustomers.ToList();
            Console.WriteLine(wc.Count);

            // Project
            var projectCustomers = _client.CreateDocumentQuery(
                collectionUri,
                new SqlQuerySpec
                {
                    QueryText = "Select c.CustomerID from c"
                });
            Console.WriteLine(projectCustomers);

            var pc = projectCustomers.ToList();
            Console.WriteLine(pc.Count);
            foreach (Document doc in projectCustomers)
            {
                Console.WriteLine(doc.GetPropertyValue<string>("CustomerID"));
            }

            // Parameter
            var param = new SqlParameter("@id")
            {
                Value = "ALFKI"
            };
            var whereParameter = _client.CreateDocumentQuery(
                collectionUri,
                new SqlQuerySpec
                {
                    QueryText = "Select * from Customers c where c.CustomerID = @id",
                    Parameters = new SqlParameterCollection
                    {
                        param
                    }
                });
            Console.WriteLine(whereParameter);

            var wp = whereParameter.ToList();
            Console.WriteLine(wp.Count);

            wp = whereParameter.ToList();
            Console.WriteLine(wp.Count);

            param.Value = "FISSA";
            Console.WriteLine(whereParameter);

            wp = whereParameter.ToList();
            Console.WriteLine(wp.Count);
        }

        public class Temp
        {
            public Temp()
            {

            }
            public Temp(string customerId)
            {
                CustomerID = customerId;
            }

            public string CustomerID { get; set; }
        }

        public class Customer
        {
            public string CustomerID { get; set; }
            public string CompanyName { get; set; }
            public string ContactName { get; set; }
            public string ContactTitle { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string Region { get; set; }
            public string PostalCode { get; set; }
            public string Country { get; set; }
            public string Phone { get; set; }
            public string Fax { get; set; }

            //public virtual ICollection<Order> Orders { get; set; }

            [NotMapped]
            public bool IsLondon => City == "London";

            protected bool Equals(Customer other) => string.Equals(CustomerID, other.CustomerID);

            public override bool Equals(object obj)
            {
                if (obj is null)
                {
                    return false;
                }
                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                return obj.GetType() == GetType()
                       && Equals((Customer)obj);
            }

            public static bool operator ==(Customer left, Customer right) => Equals(left, right);

            public static bool operator !=(Customer left, Customer right) => !Equals(left, right);

            // ReSharper disable once NonReadonlyMemberInGetHashCode
            public override int GetHashCode() => CustomerID.GetHashCode();

            public override string ToString() => "Customer " + CustomerID;
        }
    }
}
