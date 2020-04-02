# quick-redirects-for-azure-websites
This is a simple middleware example which helps developers to open frequently used links 
just by typing the path at the end of their application.

Since we are working with Azure Websites, I added VS Online Editor, Kudu and Portal paths.
You can add/amend as many urls as you wish and define your endpoints and urls. 

Check Startup.cs for usage.
Check QuickRedirectMiddleware.cs for edit links.

| Url     | Opens                 | Redirect To                                                                                     |   |
|---------|-----------------------|-------------------------------------------------------------------------------------------------|---|
| /dev    | Vs Online Editor      | https://{websiteName}.scm.azurewebsites.net/dev                                                 |   |
| /kudu   | Azure Kudu            | https://{websiteName}.scm.azurewebsites.net/                                                    |   |
| /portal | Azure Portal Web apps | https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.Web%2Fsites |   |
|         |                       |                                                                                                 |   |
