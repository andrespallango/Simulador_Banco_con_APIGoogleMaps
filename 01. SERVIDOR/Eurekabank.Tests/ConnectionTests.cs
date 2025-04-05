using Xunit;
using Eurekabank_Dotnet_RestFul_G6.ec.edu.monster.pruebas;

namespace Eurekabank.Tests
{
    public class ConnectionTests
    {
        [Fact]
        public void Test_Connection_ShouldReturnTrue()
        {
            // Arrange
            var connection = new Connection();

            // Act
            bool result = connection.TestConnection();

            // Assert
            Assert.True(result, "La conexión a la base de datos falló.");
        }
    }
}
