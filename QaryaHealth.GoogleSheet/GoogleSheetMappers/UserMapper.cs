using QaryaHealth.Core.Entities;
using QaryaHealth.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QaryaHealth.GoogleSheet.GoogleSheetMappers
{
    public static class UserMapper
    {
        public static User MapFromSingleRow(IList<object> row)
        {
            if (row == null) return new User();

            return GenerateUser(row);
        }

        public static List<User> MapFromRangeData(IList<IList<object>> values)
        {
            List<User> users = new List<User>();
            if (values == null) return users;

            foreach (var value in values) 
            {
                if (value.Count == 0 || value[0].ToString() == "Id") continue;
                users.Add(GenerateUser(value));
            }

            return users;
        }

        private static User GenerateUser(IList<object> row) 
        {
            return new User()
            {
                Id = Convert.ToInt32(row[0]),
                IsActive = Convert.ToBoolean(row[1]),
                Name = row[2].ToString(),
                PhoneNumber = row[3].ToString(),
                Password = row[4].ToString(),
                Role = (UserRole)Convert.ToInt32(row[5]),
                BloodType = (BloodType)Convert.ToInt32(row[6])
            };
        }

        public static IList<IList<object>> MapToRangeData(User user)
        {
            var objectList = new List<object>() { user.Id, user.IsActive, user.Name, user.PhoneNumber, user.Password, user.Role, user.BloodType };
            var rangeData = new List<IList<object>> { objectList };
            return rangeData;
        }
    }
}