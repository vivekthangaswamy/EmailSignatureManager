using System;
using System.IO;
using System.Net.Http;




namespace ANZ.Platform.App.EmailManager.Signature.Photo
{
    public class PhotoMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
      
        public PhotoMultipartFormDataStreamProvider(string path) : base(path)
        {
            
        }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {

            headers.ContentDisposition.FileName = GetUniqueFilename(headers.ContentDisposition.FileName.Replace("\"", string.Empty), this.RootPath);
            //Make the file name URL safe and then use it & is the only disallowed url character allowed in a windows filename
            var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ? headers.ContentDisposition.FileName : "NoName";
            return name.Trim(new char[] { '"' })
                        .Replace("&", "and");
        }


        public static string GetUniqueFilename(string file, string directory)
        {

            String filename = Path.GetFileName(file);
            string fullPath = directory + "\\" + filename;

            if (File.Exists(fullPath))
            {
      
                String path = fullPath.Substring(0, fullPath.Length - filename.Length);
                String filenameWOExt = Path.GetFileNameWithoutExtension(fullPath);
                String ext = Path.GetExtension(fullPath);
                int n = 1;
                do
                {
                    fullPath = Path.Combine(path, String.Format("{0} ({1}){2}", filenameWOExt, (n++), ext));
                }
                while (File.Exists(fullPath));
            }
            return fullPath;
        }

    }
}
