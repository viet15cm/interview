﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookLibrary.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishDate = table.Column<string>(name: "Publish_Date", type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Author", "Description", "Genre", "Price", "Publish_Date", "Title" },
                values: new object[,]
                {
                    { "B1", "Kutner, Joe", "Deploying with JRuby is the missing link between enjoying JRuby and using it in the real world to build high-performance, scalable applications.", "Computer", "33.00", "2012-08-15", "Deploying with JRuby" },
                    { "B10", "O'Brien, Tim", "Microsoft's .NET initiative is explored in detail in this deep programmer's reference.", "Computer", "36.95", "2000-12-09", "Microsoft .NET: The Programming Bible" },
                    { "B11", "Sydik, Jeremy J", "Accessibility has a reputation of being dull, dry, and unfriendly toward graphic design. But there is a better way: well-styled semantic markup that lets you provide the best possible results for all of your users. This book will help you provide images, video, Flash and PDF in an accessible way that looks great to your sighted users, but is still accessible to all users.", "Computer", "34.95", "2007-12-01", "Design Accessible Web Sites" },
                    { "B12", "Russell, Alex", "The last couple of years have seen big changes in server-side web programming. Now it’s the client’s turn; Dojo is the toolkit to make it happen and Mastering Dojo shows you how.", "Computer", "38.95", "2008-06-01", "Mastering Dojo" },
                    { "B13", "Copeland, David Bryant", "Speak directly to your system. With its simple commands, flags, and parameters, a well-formed command-line application is the quickest way to automate a backup, a build, or a deployment and simplify your life.", "Computer", "20.00", "2012-03-01", "Build Awesome Command-Line Applications in Ruby" },
                    { "B2", "Ralls, Kim", "A former architect battles corporate zombies, an evil sorceress, and her own childhood to become queen of the world.", "Fantasy", "5.95", "2000-12-16", "Midnight Rain" },
                    { "B3", "Corets, Eva", "After the collapse of a nanotechnology society in England, the young survivors lay the foundation for a new society.", "Fantasy", "5.95", "2000-11-17", "Maeve Ascendant" },
                    { "B4", "Corets, Eva", "In post-apocalypse England, the mysterious agent known only as Oberon helps to create a new life for the inhabitants of London. Sequel to Maeve Ascendant.", "Fantasy", "5.95", "2001-03-10", "Oberon's Legacy" },
                    { "B5", "Tolkien, JRR", "If you care for journeys there and back, out of the comfortable Western world, over the edge of the Wild, and home again, and can take an interest in a humble hero blessed with a little wisdom and a little courage and considerable good luck, here is a record of such a journey and such a traveler.", "Fantasy", "11.95", "1985-09-10", "The Hobbit" },
                    { "B6", "Randall, Cynthia", "When Carla meets Paul at an ornithology conference, tempers fly as feathers get ruffled.", "Romance", "4.95", "2000-09-02", "Lover Birds" },
                    { "B7", "Thurman, Paula", "A deep sea diver finds true love twenty thousand leagues beneath the sea.", "Romance", "4.95", "2000-11-02", "Splish Splash" },
                    { "B8", "Knorr, Stefan", "An anthology of horror stories about roaches, centipedes, scorpions  and other insects.", "Horror", "4.95", "2000-12-06", "Creepy Crawlies" },
                    { "B9", "Kress, Peter", "After an inadvertant trip through a Heisenberg Uncertainty Device, James Salway discovers the problems of being quantum.", "Science Fiction", "6.95", "2000-11-02", "Paradox Lost" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
