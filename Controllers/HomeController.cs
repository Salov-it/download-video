using download_video.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VideoLibrary;
using download_video.Models;
using System.IO;
using Microsoft.Web.Helpers;
using System.Web;
using NickBuhro.Translit;

namespace download_video.Controllers
{
    public class HomeController : Controller
    {

        [HttpPost]
        public  IActionResult Index(dowVideo dow)
        {

            if (dow.buttonDownload == "Скачать")
            {

                var youTube = YouTube.Default;
                var videoInfos = youTube.GetAllVideosAsync(dow.uril).GetAwaiter().GetResult();
                var maxResolution = videoInfos.FirstOrDefault(v => v.Resolution == 720);
                dow.fullName = maxResolution.FullName;
                dow.resolution = maxResolution.Resolution;
                //вывод информации
                ViewData["fullName"] = dow.fullName;
                ViewData["resolution"] = dow.resolution;

                string path = @"..\download-video\wwwroot\ServerFiles\videoNnfo.txt";


                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    dow.latin = Transliteration.CyrillicToLatin(dow.fullName, Language.Russian); //перевод текста на английский
                    
                    writer.WriteLineAsync(dow.latin);

                    System.IO.File.WriteAllBytes(@"..\download-video\wwwroot\ServerFiles\"+dow.latin,maxResolution.GetBytes());

                    ViewData["result2"] = "Нажмите что бы скачать";
                    

                }

            }
            
              return View();
            
        }

        //Передача файла клиенту
        private readonly IWebHostEnvironment _appEnvironment;
        public HomeController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public IActionResult GetFile()
        {
            
            


                string path2 = @"..\download-video\wwwroot\ServerFiles\videoNnfo.txt";


                using (StreamReader reader = new StreamReader(path2))
                {
                    string text = reader.ReadToEnd();

                    text = "" + text.Trim() + "";

                    string file_path = Path.Combine(_appEnvironment.ContentRootPath, @"..\download-video\wwwroot\ServerFiles\" + text);
                    string file_type = "application/mp4";
                    return PhysicalFile(file_path, file_type, text);

                }
            
            return View();

        }
       
        private IActionResult PhysicalFile(FileStream fs, string file_type)
        {
            throw new NotImplementedException();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
       
 }


