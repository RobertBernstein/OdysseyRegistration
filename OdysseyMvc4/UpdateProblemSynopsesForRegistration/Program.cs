using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateProblemSynopsesForRegistration
{
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text.RegularExpressions;

    class Program
    {
        static void Main(string[] args)
        {
            string internationalSiteUrl = ProcessParameters(args);

            if (string.IsNullOrEmpty(internationalSiteUrl))
            {
                return;
            }

            List<Problem> oldSynopses;
            using (DB_12824_registrationEntities dbContext = new DB_12824_registrationEntities())
            {
                oldSynopses = GetCurrentSynopsesDataFromDatabase(dbContext);
            }

            string webPage = ReadSynopsesPageFromInternationalSite(internationalSiteUrl);

            WriteWebPageToAFile(webPage);

            List<Problem> newSynopses = ExtractDataFromWebPage(webPage, oldSynopses);
            DisplayData();
            ConfirmDataWithUser();
            UpdateDatabaseWithSynopses();
        }

        private static void WriteWebPageToAFile(string webPage)
        {
            using (StreamWriter sw = new StreamWriter("synopses.html", false))
            {
                sw.Write(webPage);
            }
        }

        private static string ProcessParameters(string[] args)
        {
            if (args.Length < 1)
            {
                DisplayUsage();
                return null;
            }

            // Treat the first parameter as the internationalSiteUrl from which to extract synopses data.
            return args[0];
        }

        private static void DisplayUsage()
        {
            Console.WriteLine("Usage: UpdateProblemSynopsesForRegistration <url>");
            Console.WriteLine("    url: URL of International Odyssey of the Mind synopses page.");
            Console.WriteLine("    Example: UpdateProblemSynopsesForRegistration http://www.odysseyofthemind.com/materials/2015problems.php");
            Console.WriteLine();
        }

        private static List<Problem> GetCurrentSynopsesDataFromDatabase(DB_12824_registrationEntities dbContext)
        {
            List<Problem> oldSynopses = dbContext.Problems.ToList();
            return oldSynopses;
        }

        private static string ReadSynopsesPageFromInternationalSite(string internationalSiteUrl)
        {
            string webPageContents;
            using (WebClient webClient = new WebClient())
            {
                webPageContents = webClient.DownloadString(internationalSiteUrl);
            }

            return webPageContents;
        }

        private static List<Problem> ExtractDataFromWebPage(string webPage, List<Problem> synopses)
        {
            for (int problemNumber = 1; problemNumber <= 5; problemNumber++)
            {
                string stringToFind = string.Format("Problem {0}:", problemNumber);
                int endPosition = FindDivisionNumbers(webPage, stringToFind, problemNumber, 0, ref synopses);

                stringToFind = "Divisions";
                endPosition = FindDivisionNumbers(webPage, stringToFind, problemNumber, endPosition, ref synopses);
            }

            return synopses;
        }

        private static int FindDivisionNumbers(string webPage, string stringToFind, int problemNumber, int endPosition, ref List<Problem> synopses)
        {
            int startPosition = webPage.IndexOf(stringToFind, endPosition, StringComparison.OrdinalIgnoreCase);

            startPosition += stringToFind.Length;

            endPosition = webPage.IndexOf("</font>", startPosition, StringComparison.OrdinalIgnoreCase);
            string divisions = webPage.Substring(startPosition, endPosition - startPosition).Trim();

            string withoutHtml = Regex.Replace(divisions, @"<[^>]+>|&nbsp;", string.Empty).Trim();

            synopses[problemNumber].Divisions = withoutHtml;

            return endPosition;
        }

        private static int FindProblemName(string webPage, int problemNumber, ref List<Problem> synopses)
        {
            string stringToFind = string.Format("Problem {0}:", problemNumber);
            int startPosition = webPage.IndexOf(stringToFind, StringComparison.OrdinalIgnoreCase);

            startPosition += stringToFind.Length;

            int endPosition = webPage.IndexOf("</font>", startPosition, StringComparison.OrdinalIgnoreCase);
            string problemName = webPage.Substring(startPosition, endPosition - startPosition).Trim();

            synopses[problemNumber].ProblemName = problemName;

            return endPosition;
        }

        private static void DisplayData()
        {
            throw new NotImplementedException();
        }

        private static void ConfirmDataWithUser()
        {
            throw new NotImplementedException();
        }

        private static void UpdateDatabaseWithSynopses()
        {
            throw new NotImplementedException();
        }
    }
}
