using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wordbook.Service
{
    public class SessionUtils
    {
        public static void SaveSession(HttpContext context,String sessionID,string user_id)
        {
            context.Session[sessionID] = user_id;
        }

        public static bool CheckSession(HttpSessionStateBase session, string sessionID)
        {
            if (session[sessionID] == null)
            {
                return false;
            }
            return true;
        }

        public static void Logout(HttpContext context, String sessionID)
        {
            context.Session[sessionID] = null;
        }
    }
}