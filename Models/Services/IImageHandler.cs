

namespace communicator.Models.Services;

public interface IImageHandler{
    void SaveImage(IFormFile file,string location);
    string GetImage(string name);
}