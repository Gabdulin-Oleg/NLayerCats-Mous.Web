using System;
using System.Text;

namespace NLayerCats_Mous.BLL.Hellper
{
    
    public static class Helper
    {
        public static string GetPassword(string userName)
        {
            int result = 0;
            string str = $"{userName}-spasem-mir";

            var arrayBytes = Encoding.ASCII.GetBytes(str);

            for (int i = 0; i < arrayBytes.Length; i++)
            {
                result += arrayBytes[i];
            }

            return result.ToString();
        }
        public static string GenerationOrderNumber()
        {
            Random rand = new Random();
            return rand.Next(1000000, 1000000000).ToString() + "-" + rand.Next(1000000, 1000000000).ToString() + "-" + rand.Next(1000000, 1000000000).ToString();
        }
    }
}
