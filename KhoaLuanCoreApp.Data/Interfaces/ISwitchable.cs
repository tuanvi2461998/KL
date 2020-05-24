using KhoaLuanCoreApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KhoaLuanCoreApp.Data.Interfaces
{
    public  interface ISwitchable
    {
        Status Status { set; get; }
    }
}
