# quick-redirects-for-azure-websites
A very quick url redirect manager for Azure WebApps directly called from your application url.
Check quick-redirects-for-azure-websites region inside Startup.cs

| Url     | Opens                 | Redirect To                                                                                     |   |
|---------|-----------------------|-------------------------------------------------------------------------------------------------|---|
| /dev    | Vs Online Editor      | https://{websiteName}.scm.azurewebsites.net/dev                                                 |   |
| /kudu   | Azure Kudu            | https://{websiteName}.scm.azurewebsites.net/                                                    |   |
| /portal | Azure Portal Web apps | https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.Web%2Fsites |   |
|         |                       |                                                                                                 |   |
