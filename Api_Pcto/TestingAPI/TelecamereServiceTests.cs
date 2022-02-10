using Xunit;
using Api_Pcto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Api_Pcto.Data;

namespace Api_Pcto.Models.Tests
{


    public class TelecamereServiceTests_Fixture : IDisposable
    {
        private bool disposedValue;
        private SqliteConnection _connection;
        public MyDbContext _ContextTest;

        public TelecamereServiceTests_Fixture()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            // These options will be used by the context instances in this test suite, including the connection opened above.
            var _contextOptions = new DbContextOptionsBuilder<MyDbContext>()
                .UseSqlite(_connection)
                .Options;

            // Create the schema and seed some data
            _ContextTest = new MyDbContext(_contextOptions);

            _ContextTest.Database.EnsureCreated();


            _ContextTest.eletelecamere.AddRange(
                new Telecamera_Data("Milano", "link", 10, 100),
                new Telecamera_Data("Bergamo", "link", 3, 40),
                new Telecamera_Data("Chinatown", "link", 5, 20),
                new Telecamera_Data("New York", "link", 6, 30),
                new Telecamera_Data("Londra", "link", 4, 10));
            _ContextTest.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {

                _connection.Close();
                
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Non modificare questo codice. Inserire il codice di pulizia nel metodo 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
    public class TelecamereServiceTests : IClassFixture<TelecamereServiceTests_Fixture>
    {
        private static TelecamereServiceTests_Fixture _fixture;
        public TelecamereServiceTests(TelecamereServiceTests_Fixture fixture)
        {
            _fixture = fixture;
        }

        [Fact()]
        public void GetAllTest()
        {
            var x = new TelecamereService(_fixture._ContextTest).GetAll().Result.Count();
            Assert.True(x==5,"Le telecamere non vengono restituite");
        }

        [Fact()]
        public void GetByIdTest_IdFound()
        {
            var x = new TelecamereService(_fixture._ContextTest);
            var result=x.GetById(1).Result;
            Assert.True(result.Success, "Non si riesce a ricavare la telecamera");
        }
        [Fact()]
        public void GetByIdTest_NoId()
        {
            var x = new TelecamereService(_fixture._ContextTest);
            var result = x.GetById(100).Result;
            Assert.True(!result.Success, "La funzione non restituisce errore per una telecamera inesistente");
        }
        [Fact()]
        public void GetByNameTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void GetRandomTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void PostTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void PutTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void DeleteTest()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}