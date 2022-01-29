// Decompiled with JetBrains decompiler
// Type: WebApiKaeser.Models.Areas
// Assembly: WebApiKaeser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4813AB2-B14C-45B6-A308-DF837CB2CD18
// Assembly location: D:\Clientes\Kaeser\Colombia\WebApiKaeser\bin\WebApiKaeser.dll

using System;

namespace WebApiKaeser.Models
{
  public class Areas
  {
    public Guid ARE_ID { get; set; }

    public string ARE_DESC { get; set; }

    public Guid? ARE_ARE_PARENT_ID { get; set; }

    public Guid ARE_CCO_ID { get; set; }

    public Guid ARE_TAR_ID { get; set; }

    public string ARE_OBSERVACIONES { get; set; }

    public string ARE_CIUDAD { get; set; }

    public string ARE_DIRECCION { get; set; }

    public string ARE_CODIGO_POSTAL { get; set; }

    public bool ARE_ACTIVE { get; set; }

    public string ARE_DESC_PADRE { get; set; }

    public string ARE_DESC_CENTROCOSTO { get; set; }

    public string ARE_DESC_TIPOAREA { get; set; }

    public bool SELECCIONADO { get; set; }
        public bool ES_ST { get; set; }
        public bool ARE_IS_RENTA { get; set; }
    }
}
