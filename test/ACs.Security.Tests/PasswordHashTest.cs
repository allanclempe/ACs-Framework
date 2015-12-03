using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ACs.Security.Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class PasswordHashTest
    {

        [Fact]
        public void ValidatePassword()
        {
            var hash = PasswordHash.CreateHash("passwordtest");
            var isValid = PasswordHash.ValidatePassword("passwordtest", hash);

            Assert.True(isValid);
        }

        [Fact]
        public void ValidateHashLenght()
        {
            var hash1 = PasswordHash.CreateHash("passwordtest");
            var hash2 = PasswordHash.CreateHash("passwordtest-long");

            Assert.Equal(hash1.Length, hash2.Length);
            Assert.Equal(70, hash1.Length);
        }

        [Fact]
        public void ValidateNotValidPassword()
        {
            var hash = PasswordHash.CreateHash("passwordtest");
            var isValid = PasswordHash.ValidatePassword("pasSwordtest", hash);

            Assert.False(isValid);
        }

        [Fact]
        public void ValidatePasswordWithAccents()
        {
            var hash = PasswordHash.CreateHash("passwordtést");
            var isValid = PasswordHash.ValidatePassword("passwordtést", hash);

            Assert.True(isValid);
        }


        [Fact]
        public void ValidateNotValidPasswordWithAccents()
        {
            var hash = PasswordHash.CreateHash("passwordtést");
            var isValid = PasswordHash.ValidatePassword("passwordtíst", hash);

            Assert.False(isValid);
        }


        [Fact]
        public void ValidatePasswordEspecialChars()
        {
            var hash = PasswordHash.CreateHash("!#@páss$ordtést");
            var isValid = PasswordHash.ValidatePassword("!#@páss$ordtést", hash);

            Assert.True(isValid);
        }


    }
}
