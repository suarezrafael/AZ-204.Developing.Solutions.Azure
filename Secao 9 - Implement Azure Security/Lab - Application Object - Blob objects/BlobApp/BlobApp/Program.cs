using Azure.Identity;
using Azure.Storage.Blobs;


string tenantId = "70c0f6d9-7f3b-4425-a6b6-09b47643ec58";
string clientId = "b4d0b1b0-21f6-4b57-a6cc-ca982114e340";
string clientSecret = "1ym8Q~uaRr2d5LtGSB9K36JxhJzN-MB2iMxirbyr";


string blobURI = "https://appstore500505.blob.core.windows.net/data/script.sql";
string filePath = "C:\\tmp1\\script.sql";

ClientSecretCredential clientCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);

BlobClient blobClient = new BlobClient(new Uri(blobURI),clientCredential);

await blobClient.DownloadToAsync(filePath);

Console.WriteLine("The blob is downloaded");

