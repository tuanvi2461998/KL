using System;
using System.Collections.Generic;
using System.Text;

namespace KhoaLuanCoreApp.Data.Interfaces
{
    public  interface IHasSeoMetaData
    {
        string SeoAlias { set; get; }  
        string SeoDescription { set; get; }
    }
}
