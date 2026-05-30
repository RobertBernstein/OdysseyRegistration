using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JudgeRegistrationRazor.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProblemSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Problem",
                columns: new[] { "Id", "CostLimit", "Divisions", "Notes", "PCAddress", "PCCity", "PCEmail1", "PCEmail2", "PCFaxNumber", "PCFirstName", "PCHomePhone", "PCLastName", "PCMobilePhone", "PCPostalCode", "PCStateOrProvince", "PCWorkPhone", "ProblemCaptainID", "ProblemCategory", "ProblemDescription", "ProblemName" },
                values: new object[,]
                {
                    { 1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "I Don't Know" },
                    { 2, "$145 USD", "I, II, III, & IV", "", "", "", "", "", "", "", "", "", "", "", "", "", "1", "Vehicle", "", "P1:Drive-in Movie" },
                    { 3, "$145 USD", "I, II, & III", "", "", "", "", "", "", "", "", "", "", "", "", "", "2", "Technical", "", "P2:AI Tech-No-Art" },
                    { 4, "$125 USD", "I, II, III & IV", "", "", "", "", "", "", "", "", "", "", "", "", "", "3", "Classics", "", "P3: Classics... Opening Night Antics" },
                    { 5, "$145 USD", "I, II, III & IV", "", "", "", "", "", "", "", "", "", "", "", "", "", "4", "Structure", "", "P4: Deep Space Structure" },
                    { 6, "$125 USD", "I, II, III, & IV", "", "", "", "", "", "", "", "", "", "", "", "", "", "5", "Drama", "", "P5:Rocking World Detour" },
                    { 7, "$125 USD", "Grades K-2", "", "", "", "", "", "", "", "", "", "", "", "", "", "6", "Primary", "", "Dinos on Parade" },
                    { 8, "N/A", "All", "", "", "", "", "", "", "", "", "", "", "", "", "", "7", null, "No synopsis. – The team is presented with a problem they have never seen before, to be solved on-the-spot.  The problem may be verbal, hands-on, or a combination.", "Spontaneous" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Problem",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Problem",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Problem",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Problem",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Problem",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Problem",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Problem",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Problem",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
