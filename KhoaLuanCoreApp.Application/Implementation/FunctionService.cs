using AutoMapper;
using AutoMapper.QueryableExtensions;
using KhoaLuanCoreApp.Application.Interface;
using KhoaLuanCoreApp.Application.ViewModels.System;
using KhoaLuanCoreApp.Data.Entities;
using KhoaLuanCoreApp.Data.Enums;
using KhoaLuanCoreApp.Data.IRepositories;
using KhoaLuanCoreApp.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaLuanCoreApp.Application.Implementation
{
    public class FunctionService : IFunctionService
    {
        IFunctionRepository _functionRepository;
        public FunctionService(IFunctionRepository functionRepository)
        {
            _functionRepository = functionRepository;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task<List<FunctionViewModel>> GetAll()
        {
            return _functionRepository.FindAll().ProjectTo<FunctionViewModel>().ToListAsync();
        }

        public List<FunctionViewModel> GetAllByPermission(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
