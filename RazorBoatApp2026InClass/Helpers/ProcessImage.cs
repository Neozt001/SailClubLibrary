using Microsoft.AspNetCore.Hosting;

namespace RazorBoatApp2026InClass.Helpers
{
    public static class ProcessImage
    {
        public static string ProcessUploadedFile(IFormFile photo, string webRoothPath, string defaultPic)
        {
            string uniqueFileName = null;
            if (photo != null)
            {
                string uploadsFolder = Path.Combine(webRoothPath, "Images/MemberBoatImages");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }
            }
            else
            {
                return defaultPic;
            }
            return uniqueFileName;
        }
    }
}
