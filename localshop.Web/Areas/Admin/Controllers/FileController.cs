﻿using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.Web.Mvc;
using System.Web;
using System.Configuration;
using System.Linq;

public class FileController : Controller
{
    // Get the Azure Blob Storage settings from the Web.config
    private readonly string connectionString = ConfigurationManager.AppSettings["AzureBlobStorageConnectionString"];
    private readonly string containerName = ConfigurationManager.AppSettings["AzureBlobContainerName"];

    public ActionResult Index()
    {
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
        CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
        CloudBlobContainer container = blobClient.GetContainerReference(containerName);

        var blobs = container.ListBlobs();
        var fileNames = blobs.OfType<CloudBlockBlob>().Select(b => b.Name).ToList();

        return View(fileNames);
    }

    [HttpPost]
    public ActionResult Upload(HttpPostedFileBase file)
    {
        if (file != null && file.ContentLength > 0)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            CloudBlockBlob blob = container.GetBlockBlobReference(file.FileName);
            blob.UploadFromStream(file.InputStream);
        }

        return RedirectToAction("Index");
    }

    public ActionResult Delete(string fileName)
    {
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
        CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
        CloudBlobContainer container = blobClient.GetContainerReference(containerName);

        CloudBlockBlob blob = container.GetBlockBlobReference(fileName);
        blob.Delete();

        return RedirectToAction("Index");
    }
}