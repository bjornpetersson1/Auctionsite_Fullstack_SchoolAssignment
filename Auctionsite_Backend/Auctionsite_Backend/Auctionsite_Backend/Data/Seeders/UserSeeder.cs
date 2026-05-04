using Auctionsite_Backend.Models;
using BC = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;

namespace Auctionsite_Backend.Data.Seeders
{
    public static class UserSeeder
    {
        private static readonly List<(string Name, string Email, string Password, string Role)> SeedUsers = new()
        {
            ("Admin",             "admin@grabowskis.se",            "Password1!",  "Admin"),
            ("björn Lindqvist",   "bjorn.lindqvist@outlook.com",    "Password2!",  "User"),
            ("Cecilia Holm",      "cecilia.holm@hotmail.com",       "Password3!",  "User"),
            ("David Sjöberg",     "david.sjoberg@yahoo.com",        "Password4!",  "User"),
            ("Emma Karlsson",     "emma.karlsson@gmail.com",        "Password5!",  "User"),
            ("Fredrik Nilsson",   "fredrik.nilsson@protonmail.com", "Password6!",  "User"),
            ("Gabriella Berg",    "gabriella.berg@outlook.com",     "Password7!",  "User"),
            ("Hans Magnusson",    "hans.magnusson@hotmail.com",     "Password8!",  "User"),
            ("Ingrid Åström",     "ingrid.astrom@gmail.com",        "Password9!",  "User"),
            ("Johan Persson",     "johan.persson@yahoo.com",        "Password10!", "User"),
            ("Karin Gustafsson",  "karin.gustafsson@gmail.com",     "Password11!", "User"),
            ("Lars Henriksson",   "lars.henriksson@outlook.com",    "Password12!", "User"),
            ("Maria Svensson",    "maria.svensson@hotmail.com",     "Password13!", "User"),
            ("Niklas Johansson",  "niklas.johansson@protonmail.com","Password14!", "User"),
            ("Olivia Andersson",  "olivia.andersson@gmail.com",     "Password15!", "User"),
            ("Peter Eklund",      "peter.eklund@yahoo.com",         "Password16!", "User"),
            ("قاسم Rahimi",       "qasim.rahimi@gmail.com",         "Password17!", "User"),
            ("Rebecca Lund",      "rebecca.lund@outlook.com",       "Password18!", "User"),
            ("Stefan Wallin",     "stefan.wallin@hotmail.com",      "Password19!", "User"),
            ("Therese Björk",     "therese.bjork@gmail.com",        "Password20!", "User"),
            ("Ulf Martinsson",    "ulf.martinsson@yahoo.com",       "Password21!", "User"),
            ("Veronica Strand",   "veronica.strand@outlook.com",    "Password22!", "User"),
            ("Wilhelm Axelsson",  "wilhelm.axelsson@gmail.com",     "Password23!", "User"),
            ("Xenia Lindgren",    "xenia.lindgren@protonmail.com",  "Password24!", "User"),
            ("Ylva Sundqvist",    "ylva.sundqvist@hotmail.com",     "Password25!", "User"),
            ("Zacharias Nordin",  "zacharias.nordin@gmail.com",     "Password26!", "User"),
            ("Anna Hellström",    "anna.hellstrom@yahoo.com",       "Password27!", "User"),
            ("Bertil Öberg",      "bertil.oberg@outlook.com",       "Password28!", "User"),
            ("Christina Viklund", "christina.viklund@gmail.com",    "Password29!", "User"),
            ("Daniel Engström",   "daniel.engstrom@hotmail.com",    "Password30!", "User"),
            ("Elina Forsgren",    "elina.forsgren@gmail.com",       "Password31!", "User"),
            ("Filip Holmberg",    "filip.holmberg@protonmail.com",  "Password32!", "User"),
            ("Gunilla Rydberg",   "gunilla.rydberg@yahoo.com",      "Password33!", "User"),
            ("Henrik Sandström",  "henrik.sandstrom@outlook.com",   "Password34!", "User"),
            ("Ida Blomqvist",     "ida.blomqvist@gmail.com",        "Password35!", "User"),
            ("Jonas Hedlund",     "jonas.hedlund@hotmail.com",      "Password36!", "User"),
            ("Kristina Sjögren",  "kristina.sjogren@gmail.com",     "Password37!", "User"),
            ("Lennart Åberg",     "lennart.aberg@yahoo.com",        "Password38!", "User"),
            ("Monica Lundgren",   "monica.lundgren@outlook.com",    "Password39!", "User"),
            ("Nils Bergström",    "nils.bergstrom@protonmail.com",  "Password40!", "User"),
            ("Oskar Lindblom",    "oskar.lindblom@gmail.com",       "Password41!", "User"),
            ("Petra Engberg",     "petra.engberg@hotmail.com",      "Password42!", "User"),
            ("Ragnar Söderberg",  "ragnar.soderberg@yahoo.com",     "Password43!", "User"),
            ("Sofie Nyström",     "sofie.nystrom@gmail.com",        "Password44!", "User"),
            ("Tobias Björklund",  "tobias.bjorklund@outlook.com",   "Password45!", "User"),
            ("Ulrika Dahlgren",   "ulrika.dahlgren@hotmail.com",    "Password46!", "User"),
            ("Viktor Isaksson",   "viktor.isaksson@gmail.com",      "Password47!", "User"),
            ("Wendy Carlsson",    "wendy.carlsson@protonmail.com",  "Password48!", "User"),
            ("Åsa Magnusdotter",  "asa.magnusdotter@yahoo.com",     "Password49!", "User"),
            ("Örjan Pettersson",  "orjan.pettersson@gmail.com",     "Password50!", "User"),
        };
        public static async Task CreateUsers(AuctionSiteDbContext dbContext)
        {
            if (await dbContext.Users.AnyAsync()) return;

            var users = SeedUsers.Select(u => new User
            {
                Name = u.Name,
                Email = u.Email,
                PasswordHash = BC.HashPassword(u.Password, workFactor: 12),
                Role = u.Role
            });

            await dbContext.Users.AddRangeAsync(users);
            await dbContext.SaveChangesAsync();
        }
    }
}
