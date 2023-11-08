using CKSource.CKFinder.Connector.Config;
using CKSource.CKFinder.Connector.Core.Acl;
using CKSource.CKFinder.Connector.Core.Builders;
using CKSource.CKFinder.Connector.Core.Logs;
using CKSource.CKFinder.Connector.Host.Owin;
using CKSource.CKFinder.Connector.KeyValue.FileSystem;
using CKSource.CKFinder.Connector.Logs.NLog;
using CKSource.FileSystem.Azure;
using CKSource.FileSystem.Local;
using localshop.Infrastructures;
using Microsoft.Owin;
using Owin;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(localshop.Startup))]
namespace localshop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            /*
             * Start the logger
             */
            LoggerManager.LoggerAdapterFactory = new NLogLoggerAdapterFactory();

             /*
             * Register the "local" type backend file system.
             */
            FileSystemFactory.RegisterFileSystem<LocalStorage>();

            /*
             * Map the CKFinder connector service under a given path. By default the CKFinder JavaScript
             * client expects the ASP.NET connector to be accessible under the "/ckfinder/connector" route.
             */
            app.Map("/ckfinder/connector", SetupConnector);
        }

        private static void SetupConnector(IAppBuilder app)
        {
            /*
             * Create a connector instance using ConnectorBuilder. The call to the LoadConfig() method
             * will configure the connector using CKFinder configuration options defined in Web.config.
             */
            var connectorFactory = new OwinConnectorFactory();
            var connectorBuilder = new ConnectorBuilder();
            
            /*
             * Create an instance of authenticator implemented.
             */
            var customAuthenticator = new CKFinderAuthenticator();


            var connector = connectorBuilder
                .LoadConfig()
                .SetAuthenticator(customAuthenticator)
                .SetRequestConfiguration(
                    (request, config) =>
                    {
// config.LoadConfig();

                        var defaultBackend = config.GetBackend("default");
                        var keyValueStoreProvider = new FileSystemKeyValueStoreProvider(defaultBackend);
                        config.SetKeyValueStoreProvider(keyValueStoreProvider);

                        // Remove dummy resource type
                        config.RemoveResourceType("dummy");

                        var queryParameters = request.QueryParameters;

                        // This code lacks some input validation - make sure the user is allowed to access passed appId
                        string appId = queryParameters.ContainsKey("appId") ? Enumerable.FirstOrDefault(queryParameters["appId"]) : string.Empty;

                        // set up an array of StringMatchers for folder to hide!
                        StringMatcher[] hideFoldersMatcher = new StringMatcher[] { new StringMatcher(".*"), new StringMatcher("CVS"), new StringMatcher("thumbs"), new StringMatcher("__thumbs") };

                        // image type resource setup
                        var fileSystem_Images = new AzureStorage(account: "medcoblob",
                                                    key: "wc0yOD6lNZuBSFoYos/GK29ThHYYY6Agia+S4DmoawiplMRQRA1gQ3y+5Ug6Gu94U99CQgrp1nfY+AStp6JZZw==",
                                                    container: "medcoimg");

                        string[] allowedExtentions_Images = new string[] { "gif", "jpeg", "jpg", "png" };

                        config.AddBackend("azstore", fileSystem_Images, string.Format("https://medcoblob.blob.core.windows.net/medcoimg/", appId), false);

                        config.AddResourceType("Image", resourceBuilder => {
                            resourceBuilder.SetBackend("azstore", "/")
                            .SetAllowedExtensions(allowedExtentions_Images)
                            .SetHideFoldersMatchers(hideFoldersMatcher)
                            .SetMaxFileSize(5242880);
                        });

                        // file type resource setup
                        ////var fileSystem_Files = new AzureStorage(secret: "SECRET-HERE",
                        ////                                key: "KEY-HERE",
                        ////                                bucket: "BUCKET-HERE",
                        ////                                region: "us-east-1",
                        ////                                root: string.Format("docs/{0}/userfiles/", appId),
                        ////                                signatureVersion: "4");
                        //////var fileSystem_Files = new AzureStorage(account: "medcoblob",
                        //////                            key: "wc0yOD6lNZuBSFoYos/GK29ThHYYY6Agia+S4DmoawiplMRQRA1gQ3y+5Ug6Gu94U99CQgrp1nfY+AStp6JZZw==",
                        //////                            container: "medcoimg",
                        //////                            root: string.Format("docs/{0}/userfiles/", appId));
                        //////string[] allowedExtentions_Files = new string[] { "csv", "doc", "docx", "gif", "jpeg", "jpg", "ods", "odt", "pdf", "png", "ppt", "pptx", "rtf", "txt", "xls", "xlsx" };

                        //////config.AddBackend("azFiles", fileSystem_Files, string.Format("CDNURL-HERE/docs/{0}/userfiles/", appId), false);

                        //////config.AddResourceType("Files", resourceBuilder => {
                        //////    resourceBuilder.SetBackend("azFiles", "/")
                        //////    .SetAllowedExtensions(allowedExtentions_Files)
                        //////    .SetHideFoldersMatchers(hideFoldersMatcher)
                        //////    .SetMaxFileSize(10485760);
                        //////});

                    })
                .Build(connectorFactory);

            /*
             * Add the CKFinder connector middleware to the web application pipeline.
             */
            app.UseConnector(connector);
        }
    }
}
