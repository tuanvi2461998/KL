using System;
using System.Collections.Generic;
using System.Text;

namespace KhoaLuanCoreApp.Data.Interfaces
{
   public interface IHasOwner<T>
    {
     T OwnerId { set; get; }
    }
}
