using ANZ.Platform.App.EmailManager.Objects.ViewModels;
using ANZ.Platform.App.EmailManager.Signature.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ANZ.Platform.App.EmailManager.Signature.Photo
{
    public class Helper
    {
        private ImageService imageService;

        public Helper()
        {
            this.imageService = new ImageService();

        }

     
        public static string GetUniqueFilename(string fullPath)
        {


            if (!Path.IsPathRooted(fullPath))
                fullPath = Path.GetFullPath(fullPath);
            if (File.Exists(fullPath))
            {
                String filename = Path.GetFileName(fullPath);
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




        public void SaveImageinDB(string Country,PhotoMultipartFormDataStreamProvider provider)
        {

            var photos = new List<PhotoViewModel>();

            foreach (var file in provider.FileData)
            {
                var fileInfo = new FileInfo(file.LocalFileName);

              

                    photos.Add(new PhotoViewModel
                    {
                        Name = fileInfo.Name,
                        Created = fileInfo.CreationTime,
                        Modified = fileInfo.LastWriteTime,
                        Size = fileInfo.Length / 1024
                    });

             
                    imageService.addImagetoDb(Country,photos);               

           
            }//end foreach (var file in provider.FileData)

        }


        public ImagesViewModel RefreshImages()
        {

             return imageService.GetBannerSelection();

        }


    }
}
