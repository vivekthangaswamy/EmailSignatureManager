using ANZ.Platform.App.EmailManager.Objects.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ANZ.Platform.App.EmailManager.Signature.Photo
{
    public interface IPhotoManager
    {
        Task<IEnumerable<PhotoViewModel>> Get();
        Task<PhotoActionResult> Delete(string fileName);
        Task<ImagesViewModel> Add(string Country, System.Net.Http.HttpRequestMessage request);
        bool FileExists(string fileName);
    }
}
