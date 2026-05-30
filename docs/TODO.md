# Project TODO

1. [x] Update (all) the projects to use ASP.NET (Core) 10.0 or later.
2. [ ] Document how I added the SQL Project to Visual Studio and how to modify, use, and deploy it.
3. [ ] Rolled back to EF 4.4 to make sure everything worked. See if the code works as-is with EF 6.x.
4. [ ] Create Elmah test DB
5. [ ] Determine what all the projects in this solution are for.
6. [ ] Upgrade the Odyssey.Database project to SQL Server 2022.
7. [ ] Clean up the "Purpose" column in the Projects table above.
8. [ ] Add logging to all projects!
9. [ ] Make all "Return to the Home Page" buttons return to the home page at the current base URL, not hard-coded to a specific Odyssey Registration home page.
10. [ ] Move docker-compose.dcproj and its associated files into its own subdirectory.
11. [ ] Set up automated SQL Server Backups for Odyssey Registration.
12. [ ] 09/29/2024: Read "Get started with Entity Framework Core and an existing database in minutes - Quick Start Guide":
   1. https://x.com/ErikEJ/status/1740635086742069720, 2:25 AM · Dec 29, 2023.
   2. This can produce Mermaid diagrams, as well.
13. [ ] 11/09/2024: Add [sweetalert2](https://github.com/sweetalert2/sweetalert2) to replace alerts.
   1. A beautiful, responsive, customizable, accessible (WAI-ARIA) replacement for JavaScript's popup boxes. Zero dependencies.
14. [ ] 11/17/2024: Mandatory: Add the membership name and number to the tournament registration email sent to the coach.
15. [ ] 11/17/2024: Preferable: Populate the membership name and number from the schools table into the tournament registration table at the time of registration.
16. [ ] 01/01/2025: Roll back anything in the .NET updates that are incompatible with the .NET Framework / ASP.NET v4 version of the website just to get onto .NET (Core) and be finished with .NET Framework once and for all.
   1. [ ] 01/01/2025: This includes the changes I made to the Odyssey database schema.
17. [ ] 01/01/2025: Once the .NET (not Framework) site is in production, then re-add updates/changes that were checked in today.
18. [x] 01/01/2025: Convert the [Run the SQL Server database in a Docker container (manually)](../README.md#run-the-sql-server-database-in-a-docker-container-manually) section into a Dockerfile that can be built instead of step-by-step PowerShell cmdlets.
    1. Deleted the section from the README.md file.
19. [ ] 04/06/2025: Add [Humanizer: meets all your .NET needs for manipulating and displaying strings, enums, dates, times, timespans, numbers and quantities](https://github.com/Humanizr/Humanizer).
20. [ ] 04/06/2025: Figure out why the `sqlserver.configurator` docker container doesn't run once the `sqlserver` container is healthy.
21. [ ] 05/30/2026: Judges Registration (Page 3 of 3) - allow registration to complete even if sending email fails; add an explicit "Skip email and complete registration" option.
22. [ ] 05/30/2026: Judges Registration (Page 3 of 3) - if submit is attempted twice, do not redirect to the Odyssey home page; keep user on the same page and allow retry or skip; log an error for duplicate/failed submit flow.
23. [ ] 05/30/2026: Determine and document the standard error logging destination(s) for OdysseyRegistration (dev, test, prod) and wire Judges Registration errors to that destination.
24. [x] 05/30/2026: Enable and validate email sending in dev and test environments; confirm SMTP settings/credentials (including password/secret management) and document setup steps.
    1. Docker config and docs updated to automatically set the webmaster email password.
25. [x] 05/30/2026: Judges Registration (Page 3 of 3) - fix "Click for Printer Friendly Version" so it opens/loads a printable view instead of doing nothing.
    1. Copilot rewrote all the code and it works well in a standard browser.
    2. Although the popup window launches from a VS Code integrated browser window, there was not much Copilot could do to enable the `Print This Page` to work in the integrated browser window. So, I left it as-is.
