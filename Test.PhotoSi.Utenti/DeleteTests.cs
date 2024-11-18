using AutoMapper.Features;

using Microsoft.AspNetCore.Http.Features;

using PhotoSi.Utenti.Models;
using PhotoSi.Utenti.Features;
using PhotoSi.Utenti.Features.Utenti;

using Shouldly;

using System.Collections.Immutable;

namespace Test.PhotoSi.Utenti
{
    [Collection(nameof(Fixture))]
    public class DeleteTests
    {

        private readonly Fixture _fixture;

        public DeleteTests(Fixture fixture) => _fixture = fixture;

        [Fact]
        public async Task Should_delete_user()
        {
            var cmd = new Create.Command
            {
                Name = "Michael",
                Surname = "Jackson",
                Email = "michael.jackson@gmail.com"
            };

            var createdUser = await _fixture.SendAsync(cmd);

            var deleteCommand = new Delete.Command
            {
                Id = createdUser.Id
            };

            await _fixture.SendAsync(deleteCommand);

            var user = await _fixture.FindAsync<User>(createdUser.Id);

            user.ShouldBeNull();
        }
    }
}