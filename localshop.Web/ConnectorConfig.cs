namespace CKSource.CKFinder.Connector.WebApp
{
    using System.Configuration;
    using System.Linq;

    using CKSource.CKFinder.Connector.Config;
    using CKSource.CKFinder.Connector.Core.Acl;
    using CKSource.CKFinder.Connector.Core.Builders;
    using CKSource.CKFinder.Connector.Host.Owin;
    using CKSource.CKFinder.Connector.KeyValue.FileSystem;
    //using CKSource.FileSystem.Amazon;
    using CKSource.FileSystem.Azure;
    //using CKSource.FileSystem.Dropbox;
    //using CKSource.FileSystem.Ftp;
    using CKSource.FileSystem.Local;
    using global::localshop.Infrastructures;
    using Owin;

    public class localshop
    {
        public static void RegisterFileSystems()
        {
            FileSystemFactory.RegisterFileSystem<LocalStorage>();
            //FileSystemFactory.RegisterFileSystem<DropboxStorage>();
            //FileSystemFactory.RegisterFileSystem<AmazonStorage>();
            FileSystemFactory.RegisterFileSystem<AzureStorage>();
            //FileSystemFactory.RegisterFileSystem<FtpStorage>();
        }

        public static void SetupConnector(IAppBuilder builder)
        {
            var allowedRoleMatcherTemplate = ConfigurationManager.AppSettings["ckfinderAllowedRole"];
            //var authenticator = new RoleBasedAuthenticator(allowedRoleMatcherTemplate);
            var customAuthenticator = new CKFinderAuthenticator();
            var connectorFactory = new OwinConnectorFactory();
            var connectorBuilder = new ConnectorBuilder();
            var connector = connectorBuilder
                .LoadConfig()
                .SetAuthenticator(customAuthenticator)
                .SetRequestConfiguration(
                    (request, config) =>
                    {

                        config.LoadConfig();

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
                        var fileSystem_Images =  new AzureStorage(account: "medcoblob",
                                                    key: "wc0yOD6lNZuBSFoYos/GK29ThHYYY6Agia+S4DmoawiplMRQRA1gQ3y+5Ug6Gu94U99CQgrp1nfY+AStp6JZZw==",
                                                    container: "medcoimg",
                                                    root: string.Format("docs/{0}/userfiles/", appId));

                        string[] allowedExtentions_Images = new string[] { "gif", "jpeg", "jpg", "png" };

                        config.AddBackend("azstore", fileSystem_Images, string.Format("CDNURL-HERE/images/{0}/userimages/", appId), false);

                        config.AddResourceType("Images", resourceBuilder => {
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
                        var fileSystem_Files = new AzureStorage(account: "medcoblob",
                                                    key: "wc0yOD6lNZuBSFoYos/GK29ThHYYY6Agia+S4DmoawiplMRQRA1gQ3y+5Ug6Gu94U99CQgrp1nfY+AStp6JZZw==",
                                                    container: "medcoimg",
                                                    root: string.Format("docs/{0}/userfiles/", appId));
                        string[] allowedExtentions_Files = new string[] { "csv", "doc", "docx", "gif", "jpeg", "jpg", "ods", "odt", "pdf", "png", "ppt", "pptx", "rtf", "txt", "xls", "xlsx" };

                        config.AddBackend("azFiles", fileSystem_Files, string.Format("CDNURL-HERE/docs/{0}/userfiles/", appId), false);

                        config.AddResourceType("Files", resourceBuilder => {
                            resourceBuilder.SetBackend("azFiles", "/")
                            .SetAllowedExtensions(allowedExtentions_Files)
                            .SetHideFoldersMatchers(hideFoldersMatcher)
                            .SetMaxFileSize(10485760);
                        });

                    })
                .Build(connectorFactory);

            builder.UseConnector(connector);
        }
    }
}