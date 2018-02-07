using ANZ.Platform.App.EmailManager.Dal.UnitOfWork;
using ANZ.Platform.App.EmailManager.Objects.BusinessModels;
using ANZ.Platform.App.EmailManager.Objects.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace ANZ.Platform.App.EmailManager.Signature.Services
{
    public class ImageService
    {

        public ImagesViewModel GetBannerSelection()
        {
            UnitOfWork_EmailManager UnitOfWork_App = new UnitOfWork_EmailManager();
            var imgResults = UnitOfWork_App.EmailManager.GetSignatureImageSelection();
            UnitOfWork_App.Complete();
            ImagesViewModel viewModel = new ImagesViewModel();
            viewModel.Images = imgResults;
            return viewModel;


        }



        public void addImagetoDb(string Country, IEnumerable<PhotoViewModel> file)
        {
            

            UnitOfWork_EmailManager UnitOfWork_App = new UnitOfWork_EmailManager();

            foreach (var item in file)
            {
                var _SignatureImageBusinessModel = new SignatureImageBusinessModel();
                _SignatureImageBusinessModel.ImageString = item.Name;
                _SignatureImageBusinessModel.Country = Country;
                UnitOfWork_App.EmailManager.AddPhoto(_SignatureImageBusinessModel);

            }
       
            UnitOfWork_App.Complete();


        }


    }
}
