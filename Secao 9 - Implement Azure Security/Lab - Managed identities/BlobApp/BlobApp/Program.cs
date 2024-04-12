using Azure.Core;
using Azure.Identity;
using Azure.Storage.Blobs;



string blobURI = "https://appstore500505.blob.core.windows.net/data/script.sql";
string filePath = "D:\\tmp1\\script.sql";

TokenCredential tokenCredential = new DefaultAzureCredential();

BlobClient blobClient = new BlobClient(new Uri(blobURI), tokenCredential);

await blobClient.DownloadToAsync(filePath);

Console.WriteLine("The blob is downloaded");

