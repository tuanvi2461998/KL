using KhoaLuanCoreApp.Application.ViewModels.System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KhoaLuanCoreApp.Application.Interface
{
    public interface IFunctionService : IDisposable
    {
       

        Task<List<FunctionViewModel>> GetAll();
        List<FunctionViewModel> GetAllByPermission(Guid userId  );
       
    }
}
