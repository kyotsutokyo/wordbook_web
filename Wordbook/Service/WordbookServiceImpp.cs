using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using NLog;
using Wordbook.Service;

namespace Wordbook
{
    public partial class WordbookThriftServiceImpl : WordbookThriftService.Iface
    {
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        SQLiteHelper database;

        public HttpContext Context
        {
            get;
            set;
        }
        public WordbookThriftServiceImpl()
        {
            database = new SQLiteHelper(HttpContext.Current.Server.MapPath("~") + "\\Database");
        }

        LoginResult WordbookThriftService.Iface.login(string userid, string password)
        {
            LoginResult lr = new LoginResult();
            try
            {
                //ユーザー有効性チェックする
                int id = database.CheckUser(userid, password);
                if (id < 0)
                {
                    lr.Success = false;
                    lr.Msg = "ユーザーIDまたはパスワードが違います！";
                    return lr;
                }
                lr.Userid = id;
                SessionUtils.SaveSession(Context, Context.Session.SessionID, id.ToString());
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            lr.Success = true;
            if (lr.Success)
            {
                lr.Msg = "ログインは成功しました。";
            }
            else
            {
                lr.Msg = "ログインは失敗しました。";
            }
            return lr;
        }

        Wordbook WordbookThriftService.Iface.getWords(string userid)
        {
            Object session = Context.Session[Context.Session.SessionID];
            if(session==null)
            {
                return null;
            }else{
                String user_id = session.ToString();
                return database.GetWords(user_id);
            }
        }

        bool WordbookThriftService.Iface.updateWords(List<Word> words, string userid)
        {
            String user_id = Context.Session[Context.Session.SessionID].ToString();
            return database.UpdateWords(words, user_id);
        }


        public void logout()
        {
            SessionUtils.Logout(Context, Context.Session.SessionID);
        }


        public int deleteWords(List<string> words)
        {
            String user_id = Context.Session[Context.Session.SessionID].ToString();
            return database.DeleteWords(words, user_id);
        }


        public int insertWord(Word word)
        {
            String user_id = Context.Session[Context.Session.SessionID].ToString();
            return database.InsertWords(word, user_id);
        }
    }
}