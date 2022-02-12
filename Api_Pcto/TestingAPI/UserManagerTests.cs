using Api_Pcto;
using Api_Pcto.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Api_Pcto.Models.Modelli;
using Api_Pcto.Models.Servizi;
using Api_Pcto.Models.DTOS.Requests;

namespace TestingAPI
{

    public class UserManagerTests_Fixture : IDisposable
    {
        private bool disposedValue;
        private SqliteConnection _connection;
        public MyTokenDbContext _ContextTest;

        public UserManagerTests_Fixture()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            // These options will be used by the context instances in this test suite, including the connection opened above.
            var _contextOptions = new DbContextOptionsBuilder<MyTokenDbContext>()
                .UseSqlite(_connection)
                .Options;

            // Create the schema and seed some data
            _ContextTest = new MyTokenDbContext(_contextOptions);

            _ContextTest.Database.EnsureCreated();


            _ContextTest.eletokens.AddRange( 
                new UserToken { Id=1,Name="Marco",Token="dwawafwafwasdwafwa",CreationTime=DateTime.Now},
                new UserToken { Id = 2, Name = "Luca", Token = "dwadwaf23fwad", CreationTime = DateTime.Now },
                new UserToken { Id = 3, Name = "Tudo", Token = "d3dwadwad34arda", CreationTime = DateTime.Now },
                new UserToken { Id = 4, Name = "Dario", Token = "32131dsadwagasdw", CreationTime = DateTime.Now }
                );
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
    public class UserManagerTests : IClassFixture<UserManagerTests_Fixture>
    {
        private static UserManagerTests_Fixture _fixture;
        public UserManagerTests(UserManagerTests_Fixture fixture)
        {
            _fixture = fixture;
        }

        [Fact()]
        public void AddUserTokenTest()
        {
            var x = new UserManager(_fixture._ContextTest);

            var result = x.AddUserToken(new UserTokenRequest { Name = "Mario",Token="ewdwafa311frqfdwadw" }).Result;
            Assert.True(_fixture._ContextTest.eletokens.Where(x => x.Name == result.Username&& result.Token==x.Token).Count() == 1, "Non si riesce ad aggiungere il token all'utente");
        }
        [Fact()]
        public void AddUserTokenTest_AlreadyAdded()
        {
            var x = new UserManager(_fixture._ContextTest);

            var result = x.AddUserToken(new UserTokenRequest { Name = "Marco", Token = "ewdwafa311frqfdwadw" }).Result;
            Assert.True(_fixture._ContextTest.eletokens.Where(x => x.Name == result.Username && x.Token!= result.Token).Count() == 1, "La funzione non da errore se viene assegnato il token ad un utente già munito di esso");
        }

        [Fact()]
        public void GetUserTokenTest()
        {
            var x = new UserManager(_fixture._ContextTest);
            var result = x.GetUserToken("Marco").Result;
            Assert.True(result.Errors == null, "Non si riesce a ricavare l'utente");

        }
        [Fact()]
        public void GetUserTokenTest_NotFound()
        {
            var x = new UserManager(_fixture._ContextTest);
            var result = x.GetUserToken("Gianmarco").Result;
            Assert.True(result.Errors!= null, "La funzione non da errore se si prova ad ottenere un utente inesistente");

        }
        [Fact()]
        public void DeleteUserToken()
        {
            var x = new UserManager(_fixture._ContextTest);
            var result = x.DeleteUserToken("Luca").Result;
            Assert.True(result.Errors == null, "Non si riesce ad eliminare il token di un utente");
        }
        [Fact()]
        public void DeleteUserToken_NotFound()
        {
            var x = new UserManager(_fixture._ContextTest);
            var result = x.DeleteUserToken("Gianmarco").Result;
            Assert.True(result.Errors != null, "La funzione non da errore se si prova ad eliminare un utente inesistente");
        }
    }
}
