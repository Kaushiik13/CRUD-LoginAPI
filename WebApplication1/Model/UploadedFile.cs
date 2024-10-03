namespace WebApplication1.Model
{
    public class UploadedFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public byte[] FileData { get; set; }
    }

}
