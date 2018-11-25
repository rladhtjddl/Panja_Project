using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.IO;
using System.Threading.Tasks;
using Amazon;

namespace Panja_Project
{
    public struct file_list
    {
        public string Name;
        public string Id;
        public string By;
        public file_list(string name, string id, string by)
        {
            Name = name;
            Id = id;
            By = by;
        }
    }
        
    public struct file_info
    {
        public string fname;
        public string flink;
        public string fbyte;
        public string ftype;
        public string ftime;

        public file_info(string f_link)
        {
            FileInfo fileInfo = new FileInfo(f_link);
            fname = fileInfo.Name.ToString();
            flink = fileInfo.FullName.ToString();
            fbyte = fileInfo.Length.ToString();
            ftype = fileInfo.Extension.ToString();
            ftime = fileInfo.LastWriteTime.ToString();
        }
    }


    public struct folder_back
    {
        public string fname;
        public string full_link;
        public folder_back(string flink)
        {
            fname = "dd";
            full_link = "aa";
        }
    }

    class UploadFileMPUHighLevelAPITest
    {
        private const string bucketName = "*** provide bucket name ***";
        private const string keyName = "*** provide a name for the uploaded object ***";
        private const string filePath = "*** provide the full path name of the file to upload ***";
        // Specify your bucket region (an example region is shown).
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USWest2;
        private static IAmazonS3 s3Client;

      
            //s3Client = new AmazonS3Client(bucketRegion);
            //UploadFileAsync().Wait();
       

        private static async Task UploadFileAsync()
        {
            try
            {
                var fileTransferUtility =
                    new TransferUtility(s3Client);

                // Option 1. Upload a file. The file name is used as the object key name.
                await fileTransferUtility.UploadAsync(filePath, bucketName);
                Console.WriteLine("Upload 1 completed");

                // Option 2. Specify object key name explicitly.
                await fileTransferUtility.UploadAsync(filePath, bucketName, keyName);
                Console.WriteLine("Upload 2 completed");

                // Option 3. Upload data from a type of System.IO.Stream.
                using (var fileToUpload =
                    new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    await fileTransferUtility.UploadAsync(fileToUpload,
                                               bucketName, keyName);
                }
                Console.WriteLine("Upload 3 completed");

                // Option 4. Specify advanced settings.
                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = bucketName,
                    FilePath = filePath,
                    StorageClass = S3StorageClass.StandardInfrequentAccess,
                    PartSize = 6291456, // 6 MB.
                    Key = keyName,
                    CannedACL = S3CannedACL.PublicRead
                };
                fileTransferUtilityRequest.Metadata.Add("param1", "Value1");
                fileTransferUtilityRequest.Metadata.Add("param2", "Value2");

                await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
                Console.WriteLine("Upload 4 completed");
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }

        }
    }


    class Filecontrol
    {
       

    }
}
