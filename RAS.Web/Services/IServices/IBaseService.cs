using RAS.Web.Models;

namespace RAS.Web.Services.IServices
{
    //27.03 - 01:47
    //CRUD işlemlerini yönetmek, veritabanı bağlantısı kurmak, hata yönetimi gibi ortak görevleri gerçekleştirmek için kullanılır.
    //ApiRequestten gelen talebi belirtiyor ve T tipinde veri döner
    public interface IBaseService : IDisposable
    {
        ResponseDto responseModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
