using Auctionsite_Backend.Models;
using BC = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;

namespace Auctionsite_Backend.Data.Seeders
{
    public static class UserSeeder
    {
        private static readonly List<(string Name, string Email, string Password, string Role, bool IsActive)> SeedUsers = new()
        {
            ("Admin",             "admin@grabowskis.se",            "Password1!",  "admin", true),
            ("Björn Lindqvist",   "bjorn.lindqvist@outlook.com",    "Password2!",  "user", true),
            ("Cecilia Holm",      "cecilia.holm@hotmail.com",       "Password3!",  "user", true),
            ("David Sjöberg",     "david.sjoberg@yahoo.com",        "Password4!",  "user", true),
            ("Emma Karlsson",     "emma.karlsson@gmail.com",        "Password5!",  "user", true),
            ("Fredrik Nilsson",   "fredrik.nilsson@protonmail.com", "Password6!",  "user", true),
            ("Gabriella Berg",    "gabriella.berg@outlook.com",     "Password7!",  "user", true),
            ("Hans Magnusson",    "hans.magnusson@hotmail.com",     "Password8!",  "user", true),
            ("Ingrid Åström",     "ingrid.astrom@gmail.com",        "Password9!",  "user", true),
            ("Johan Persson",     "johan.persson@yahoo.com",        "Password10!", "user", true),
            ("Karin Gustafsson",  "karin.gustafsson@gmail.com",     "Password11!", "user", true),
            ("Lars Henriksson",   "lars.henriksson@outlook.com",    "Password12!", "user", true),
            ("Maria Svensson",    "maria.svensson@hotmail.com",     "Password13!", "user", true),
            ("Niklas Johansson",  "niklas.johansson@protonmail.com","Password14!", "user", true),
            ("Olivia Andersson",  "olivia.andersson@gmail.com",     "Password15!", "user", true),
            ("Peter Eklund",      "peter.eklund@yahoo.com",         "Password16!", "user", true),
            ("قاسم Rahimi",       "qasim.rahimi@gmail.com",         "Password17!", "user", true),
            ("Rebecca Lund",      "rebecca.lund@outlook.com",       "Password18!", "user", true),
            ("Stefan Wallin",     "stefan.wallin@hotmail.com",      "Password19!", "user", true),
            ("Therese Björk",     "therese.bjork@gmail.com",        "Password20!", "user", true),
            ("Ulf Martinsson",    "ulf.martinsson@yahoo.com",       "Password21!", "user", true),
            ("Veronica Strand",   "veronica.strand@outlook.com",    "Password22!", "user", true),
            ("Wilhelm Axelsson",  "wilhelm.axelsson@gmail.com",     "Password23!", "user", true),
            ("Xenia Lindgren",    "xenia.lindgren@protonmail.com",  "Password24!", "user", true),
            ("Ylva Sundqvist",    "ylva.sundqvist@hotmail.com",     "Password25!", "user", true),
            ("Zacharias Nordin",  "zacharias.nordin@gmail.com",     "Password26!", "user", true),
            ("Anna Hellström",    "anna.hellstrom@yahoo.com",       "Password27!", "user", true),
            ("Bertil Öberg",      "bertil.oberg@outlook.com",       "Password28!", "user", true),
            ("Christina Viklund", "christina.viklund@gmail.com",    "Password29!", "user", true),
            ("Daniel Engström",   "daniel.engstrom@hotmail.com",    "Password30!", "user", true),
            ("Elina Forsgren",    "elina.forsgren@gmail.com",       "Password31!", "user", true),
            ("Filip Holmberg",    "filip.holmberg@protonmail.com",  "Password32!", "user", true),
            ("Gunilla Rydberg",   "gunilla.rydberg@yahoo.com",      "Password33!", "user", true),
            ("Henrik Sandström",  "henrik.sandstrom@outlook.com",   "Password34!", "user", true),
            ("Ida Blomqvist",     "ida.blomqvist@gmail.com",        "Password35!", "user", true),
            ("Jonas Hedlund",     "jonas.hedlund@hotmail.com",      "Password36!", "user", true),
            ("Kristina Sjögren",  "kristina.sjogren@gmail.com",     "Password37!", "user", true),
            ("Lennart Åberg",     "lennart.aberg@yahoo.com",        "Password38!", "user", true),
            ("Monica Lundgren",   "monica.lundgren@outlook.com",    "Password39!", "user", true),
            ("Nils Bergström",    "nils.bergstrom@protonmail.com",  "Password40!", "user", true),
            ("Oskar Lindblom",    "oskar.lindblom@gmail.com",       "Password41!", "user", true),
            ("Petra Engberg",     "petra.engberg@hotmail.com",      "Password42!", "user", true),
            ("Ragnar Söderberg",  "ragnar.soderberg@yahoo.com",     "Password43!", "user", true),
            ("Sofie Nyström",     "sofie.nystrom@gmail.com",        "Password44!", "user", true),
            ("Tobias Björklund",  "tobias.bjorklund@outlook.com",   "Password45!", "user", true),
            ("Ulrika Dahlgren",   "ulrika.dahlgren@hotmail.com",    "Password46!", "user", true),
            ("Viktor Isaksson",   "viktor.isaksson@gmail.com",      "Password47!", "user", true),
            ("Wendy Carlsson",    "wendy.carlsson@protonmail.com",  "Password48!", "user", true),
            ("Åsa Magnusdotter",  "asa.magnusdotter@yahoo.com",     "Password49!", "user", true),
            ("Örjan Pettersson",  "orjan.pettersson@gmail.com",     "Password50!", "user", true),
        };
        public static async Task CreateUsers(AuctionSiteDbContext dbContext)
        {
            if (await dbContext.Users.AnyAsync()) return;

            var users = SeedUsers.Select(u => new User
            {
                Name = u.Name,
                Email = u.Email,
                PasswordHash = BC.HashPassword(u.Password, workFactor: 12),
                Role = u.Role,
                IsActive = u.IsActive
            });

            await dbContext.Users.AddRangeAsync(users);
            await dbContext.SaveChangesAsync();
        }
    }
}
