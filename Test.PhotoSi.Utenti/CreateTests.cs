using AutoMapper.Features;

using Microsoft.AspNetCore.Http.Features;

using PhotoSi.Utenti.Models;
using PhotoSi.Utenti.Features;
using PhotoSi.Utenti.Features.Utenti;

using Shouldly;

//using Create = PhotoSi.Utenti.Features.Utenti.Create;
using System.Collections.Immutable;

namespace Test.PhotoSi.Utenti
{
    [Collection(nameof(Fixture))]
    public class CreateTests
    {

        private readonly Fixture _fixture;

        public CreateTests(Fixture fixture) => _fixture = fixture;

        [Fact]
        public async Task Should_create_user()
        {
            var cmd = new Create.Command
            {
                Name = "Michael",
                Surname = "Jackson",
                Email = "michael.jackson@gmail.com"
            };

            var createdUser = await _fixture.SendAsync(cmd);

            var searchUser = await _fixture.FindAsync<User>(createdUser.Id);

            searchUser.ShouldNotBeNull();
            searchUser.Email.ShouldBe(cmd.Email);
            searchUser.Name.ShouldBe(cmd.Name);
            searchUser.Surname.ShouldBe(cmd.Surname);
        }
    }
}