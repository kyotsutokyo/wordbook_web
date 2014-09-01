using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;
using Thrift.Protocol;
using Thrift;
using System.Net;
using System.IO;
using Thrift.Transport;
using System.IO.Compression;
using System.Web.SessionState;

namespace Wordbook
{
    /// <summary>
    /// Wordbook の概要の説明
    /// </summary>
    public class WordbookHandler : IHttpHandler, IRequiresSessionState
    {

     /// <summary>
        /// ログ
        /// </summary>
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        public WordbookThriftServiceImpl WordbookService = new WordbookThriftServiceImpl();

        public WordbookHandler()
        {
            this.processor = new WordbookThriftService.Processor(WordbookService);
            this.inputProtocolFactory = this.outputProtocolFactory = new TJSONProtocol.Factory();
            //this.inputProtocolFactory = this.outputProtocolFactory = new TBinaryProtocol.Factory();
        }

        protected TProcessor processor;

        protected TProtocolFactory inputProtocolFactory;
        protected TProtocolFactory outputProtocolFactory;

        protected const string contentType = "application/x-thrift";
        protected System.Text.Encoding encoding = System.Text.Encoding.UTF8;

        public bool IsReusable
        {
            get { return true; }
        }

        public virtual void ProcessRequest(HttpContext context)
        {
            WordbookService.Context = context;
            context.Response.ContentType = contentType;
            context.Response.ContentEncoding = encoding;
            //ProcessRequest(context.Request.InputStream, GetCompressedResponseStreamIfSupported(context.Request, context.Response));
            ProcessRequest(context.Request.InputStream, context.Response.OutputStream);
        }
 
        public void ProcessRequest(Stream input, Stream output)
        {
            TTransport transport = new TStreamTransport(input, output);

            TProtocol inputProtocol = null;
            TProtocol outputProtocol = null;

            try
            {
                inputProtocol = inputProtocolFactory.GetProtocol(transport);
                outputProtocol = outputProtocolFactory.GetProtocol(transport);

                while (processor.Process(inputProtocol, outputProtocol)) { }
            }
            catch (TTransportException exp)
            {
                //throw exp;
                // Client died, just move on
            }
            catch (TApplicationException tx)
            {
                Console.Error.Write(tx);
            }
            catch (Exception x)
            {
                Console.Error.Write(x);
            }

            transport.Close();
        }

        public static Stream GetCompressedResponseStreamIfSupported(HttpRequest request, HttpResponse response)
        {
            string AcceptEncoding = request.Headers["Accept-Encoding"];
            if (AcceptEncoding != null && AcceptEncoding.Contains("gzip"))
            {
                logger.Info(request.UserAgent + "contain : " + "gzip");
                //response.Headers.Remove("Content-Encoding");
                response.AddHeader("Content-Encoding", "gzip");

                return new GZipStream(response.OutputStream, CompressionMode.Compress);
            }
            else
            {
                return response.OutputStream;
            }
        }

    }
}