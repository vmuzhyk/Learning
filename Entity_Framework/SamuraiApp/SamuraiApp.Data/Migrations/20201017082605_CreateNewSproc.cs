﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace SamuraiApp.Data.Migrations
{
    public partial class CreateNewSproc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE PROCEDURE dbo.SamuraisWhoSaidWord
                    @text VARCHAR(20)
                    AS
                    SELECT  Samurais.Id, Samurais.Name, Samurais.ClanId
                    FROM    Samurais INNER JOIN
                            Quotes ON Samurais.Id = Quotes.SamiraiId
                    WHERE   (Quotes.Text LIKE '%'+@text+'%')");

            migrationBuilder.Sql(
                @"CREATE PROCEDURE dbo.DeleteQuotesForSamurai
                    @samuraiId int
                    AS DELETE FROM Quotes
                    WHERE Quotes.SamuraiId=@samuraiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
