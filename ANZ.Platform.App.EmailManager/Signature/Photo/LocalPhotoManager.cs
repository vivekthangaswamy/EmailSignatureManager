using ANZ.Platform.App.EmailManager.Objects.BusinessModels;
using ANZ.Platform.App.EmailManager.Objects.ViewModels;
using ANZ.Platform.App.EmailManager.Signature.Helpers.Validation;
using ANZ.Platform.App.EmailManager.Signature.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ANZ.Platform.App.EmailManager.Signature.Photo
{
    public class LocalPhotoManager : IPhotoManager
    {

        private string workingFolder { get; }
        private string workingFolder2 { get; }
        private Helper helper;
   
     

        public LocalPhotoManager()
        {

        }

        public LocalPhotoManager(string _workingFolder, string _workingFolder2)
        {
            this.workingFolder = _workingFolder;
            this.workingFolder2 = _workingFolder2;
            this.helper = new Helper();
         
        }

        public async Task<IEnumerable<PhotoViewModel>> Get()
        {
            List<PhotoViewModel> photos = new List<PhotoViewModel>();

            DirectoryInfo photoFolder = new DirectoryInfo(this.workingFolder);

            await Task.Factory.StartNew(() =>
            {
                photos = photoFolder.EnumerateFiles()
                                            .Where(fi => new[] { ".jpg", ".bmp", ".png", ".gif", ".tiff" }.Contains(fi.Extension.ToLower()))
                                            .Select(fi => new PhotoViewModel
                                            {
                                                Name = fi.Name,
                                                Created = fi.CreationTime,
                                                Modified = fi.LastWriteTime,
                                                Size = fi.Length / 1024
                                            })
                                            .ToList();
            });

            return photos;
        }

        public async Task<PhotoActionResult> Delete(string fileName)
        {
            try
            {
                var filePath = Directory.GetFiles(this.workingFolder, fileName)
                                .FirstOrDefault();

                await Task.Factory.StartNew(() =>
                {
                    File.Delete(filePath);
                });

                return new PhotoActionResult { Successful = true, Message = fileName + "deleted successfully" };
            }
            catch (Exception ex)
            {
                return new PhotoActionResult { Successful = false, Message = "error deleting fileName " + ex.GetBaseException().Message };
            }
        }

        public async Task<ImagesViewModel> Add(string Country,HttpRequestMessage request)
        {
            PhotoMultipartFormDataStreamProvider provider;


             if (Country == Countries.NewZealand)
            {

                provider = new PhotoMultipartFormDataStreamProvider(this.workingFolder2);

            }

            else
            {
                provider = new PhotoMultipartFormDataStreamProvider(this.workingFolder);
            }

          

            var httpRequest = HttpContext.Current.Request;


            if (httpRequest.Files.Count > 0)
            {

                foreach (string upload in httpRequest.Files)
                {

                    var filename = httpRequest.Files[upload].FileName;
                    var extension = System.IO.Path.GetExtension(filename);

                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif")
                    {

                        if (httpRequest.Files[upload].ContentLength / 1024 < 100)
                        {
                            await request.Content.ReadAsMultipartAsync(provider);
                            helper.SaveImageinDB(Country, provider);

                        }
                        else{ throw new System.InvalidOperationException("file size exceeds 100mb limit"); }

                        //End IF - file is under size limit
                    }

                    else { throw new System.InvalidOperationException("file format incorrect. Only images accepted in following format: jpg, png and gif."); }
                    //End IF - extension validation

                }//End Foreach file in request

            }//End root if


            return helper.RefreshImages();
        }

        public bool FileExists(string fileName)
        {
            var file = Directory.GetFiles(this.workingFolder, fileName)
                                .FirstOrDefault();

            return file != null;
        }

   
        private bool CheckTargetDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                throw new ArgumentException("the destination path " + this.workingFolder + " could not be found");
              
            }

            return true;
        }





  
        
   

}
}
