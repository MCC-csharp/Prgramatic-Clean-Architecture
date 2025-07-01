using Bookify.Domain.Users;
using FluentAssertions;
using Xunit;


namespace Bookify.Domaine.UnitTests.Users
{
    public class UserTests
    {
        [Fact]
        public void Create_Should_SetPropertyValues()
        {
            // Act
            var user = User.Create(UserData.FirstName,
                UserData.LastName,
                UserData.Email
            );

            // Assert
            user.FirstName.Should().Be(UserData.FirstName);
            user.LastName.Should().Be(UserData.LastName);
            user.Email.Should().Be(UserData.Email);
        }
    }
}
