using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wordbook
{
    public class StringUtils
    {
        /*** 
       * ランダムな文字列を作成する
       * 
       * */
        public static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            Random random = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}
