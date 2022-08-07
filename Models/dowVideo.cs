namespace download_video.Models
{
    public class dowVideo
    {
        public string uril { get; set; }
        public string buttonDownload { get; set; }
        public string result { get; set; }
  
        public string fullName { get; set; } //имя файла

        public int resolution { get; set; } //разрешающая способность

        public string maxResolution { get; set; } // Максимальное разрешения видео
        
        public string latin { get; set; } //транслит
        


    }
}
