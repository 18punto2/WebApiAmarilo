// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Controllers.FileuploadController
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebApiKaeser.Controllers
{
  public class FileuploadController : ApiController
  {
    private Logger logger = LogManager.GetCurrentClassLogger();

    public async Task<bool> Upload()
    {
      try
      {
        string fileuploadPath = ConfigurationManager.AppSettings["FileUploadArcLocation"];
        MultipartFormDataStreamProvider provider = new MultipartFormDataStreamProvider(fileuploadPath);
        StreamContent content = new StreamContent(HttpContext.Current.Request.GetBufferlessInputStream(true));
        foreach (KeyValuePair<string, IEnumerable<string>> header in (HttpHeaders) this.Request.Content.Headers)
          content.Headers.TryAddWithoutValidation(header.Key, header.Value);
        MultipartFormDataStreamProvider dataStreamProvider = await content.ReadAsMultipartAsync<MultipartFormDataStreamProvider>(provider);
        string sourceFileName = provider.FileData.Select<MultipartFileData, string>((Func<MultipartFileData, string>) (x => x.LocalFileName)).FirstOrDefault<string>();
        string str = fileuploadPath + ("\\" + provider.Contents[0].Headers.ContentDisposition.FileName.Trim('"'));
        if (File.Exists(str))
          File.Delete(str);
        File.Move(sourceFileName, str);
        return true;
      }
      catch (Exception ex)
      {
        this.logger.Error(ex, "Error en Upload: " + ex.Message);
        return false;
      }
    }
  }
}
