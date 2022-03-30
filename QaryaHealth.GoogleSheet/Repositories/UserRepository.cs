using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using QaryaHealth.Core.Entities;
using QaryaHealth.Core.IRepositories;
using QaryaHealth.GoogleSheet.GoogleSheetMappers;
using QaryaHealth.GoogleSheet.Helpers;
using X.PagedList;

namespace QaryaHealth.GoogleSheet.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly string sheetName;
        private readonly string lastIndexChar;
        private readonly GoogleSheetOperationsHelper _operationsHelper;

        public UserRepository(IConfiguration configuration, GoogleSheetsHelper googleSheetsHelper, string spreadSheetId)
        {
            _operationsHelper = new GoogleSheetOperationsHelper(googleSheetsHelper, spreadSheetId);
            sheetName = configuration.GetSection("sheets:user")["name"];
            lastIndexChar = configuration.GetSection("sheets:user")["lastIndexChar"];
        }

        public override void Add(User entity)
        {
            entity.Id = SheetsId.LastId[sheetName] + 1;
            entity.IsActive = true;
            _operationsHelper.Create(UserMapper.MapToRangeData(entity), sheetName, lastIndexChar);
            SheetsId.LastId[sheetName]++;
        }

        public override void Update(User entity)
        {
            _operationsHelper.Update(entity.Id, UserMapper.MapToRangeData(entity), sheetName, lastIndexChar);
        }

        public override void Delete(User entity)
        {
            _operationsHelper.HardDelete(entity.Id, sheetName, lastIndexChar);
        }

        public override async Task<User> GetAsync(int id, Expression<Func<User, object>>[] includes = null)
        {
            return UserMapper.MapFromRangeData(_operationsHelper.GetById(id, sheetName, lastIndexChar)).FirstOrDefault();
        }

        public override User Get(int id)
        {
            return UserMapper.MapFromRangeData(_operationsHelper.GetById(id, sheetName, lastIndexChar)).FirstOrDefault();
        }

        public override IEnumerable<User> GetList()
        {
            List<User> users = UserMapper.MapFromRangeData(_operationsHelper.GetAll(sheetName, lastIndexChar));
            return users.Where(r => r.IsActive).ToList();
        }

        public override Task<List<User>> GetListAsync()
        {
            List<User> users = UserMapper.MapFromRangeData(_operationsHelper.GetAll(sheetName, lastIndexChar));
            return users.Where(r => r.IsActive).ToListAsync();
        }

        public override async Task<IPagedList<User>> GetPageListAsync(Expression<Func<User, bool>> predicate, string sortColumn, string sortDirection, int page, int pageSize)
        {
            IPagedList<IList<object>> items = await _operationsHelper.GetPagedListAsync(page, pageSize, sheetName, lastIndexChar);
            List<User> users = new List<User>();
            foreach (var item in items)
            {
                if (item[0].ToString() == "id") continue;
                users.Add(UserMapper.MapFromSingleRow(item));
            }
            IPagedList<User> usersPaged = users.Where(r => r.IsActive).ToPagedList();
            return new StaticPagedList<User>(usersPaged, usersPaged.PageNumber, usersPaged.PageSize, usersPaged.TotalItemCount);
        }
    }
}