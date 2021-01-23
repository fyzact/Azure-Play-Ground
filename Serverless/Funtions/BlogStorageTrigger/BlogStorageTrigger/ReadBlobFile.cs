using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace BlogStorageTrigger
{
    public static class Function1
    {
        [FunctionName("ReadBlobFile")]
        public static void Run([BlobTrigger("samples-workitems/{name}", Connection = "")] Stream myBlob, string name, ILogger log)
        {
            StreamReader streamReader = new StreamReader(myBlob);
            string fileContent = streamReader.ReadToEnd();
            var orderListCSV = fileContent.Split(Environment.NewLine.ToCharArray());
            foreach (var item in orderListCSV)
            {
                Console.WriteLine(item);
            }
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}
