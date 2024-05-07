namespace Shop.Controller
{
    public class UploadFile
    {
        public Guid Id { get; set; }
        public List<IFormFile> images { get; set; }
        public string Name { get; set; }
    }
}
