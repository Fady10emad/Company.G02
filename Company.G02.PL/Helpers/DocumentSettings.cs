namespace Company.G02.PL.Helpers
{
    public class DocumentSettings
    {
            // upload
            // delete

        public static string UploadFile(IFormFile file , string folderName)
        {
            //string folderPath= Directory.GetCurrentDirectory() + "\\wwwroot\\files\\" + folderName; 
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);

            var fileName = $"{Guid.NewGuid()}{file.FileName}";


            var filePath = Path.Combine(folderPath, fileName);
            using var fileStream = new FileStream(filePath,FileMode.Create);
            file.CopyTo(fileStream);

            return fileName;
        }


        public static void DeleteFile(string fileName, string folderName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);
            var filePath = Path.Combine(folderPath, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
