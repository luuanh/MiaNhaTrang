namespace ProjectLibrary.Config
{
    public class ReturnSmallImage
    {
        public static string GetImageSmall(string image)
        {
            return "/Files/_thumbs" + image.Substring(6, image.Length - 6);
        }
    }
}