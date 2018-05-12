using System;
using System.Collections.Generic;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using MicroOrm.Dapper.Repositories.Tests.Classes;
using Xunit;

namespace MicroOrm.Dapper.Repositories.Tests.SqlGeneratorTests
{
    public class MySqlGeneratorTests
    {
        private const SqlProvider _sqlConnector = SqlProvider.MySQL;

        [Fact]
        public static void Count()
        {
            ISqlGenerator<User> userSqlGenerator = new SqlGenerator<User>(_sqlConnector, true);
            var sqlQuery = userSqlGenerator.GetCount(null);
            Assert.Equal("SELECT COUNT(*) FROM `Users` WHERE `Users`.`Deleted` != 1", sqlQuery.GetSql());
        }

        [Fact]
        public static void CountWithDistinct()
        {
            ISqlGenerator<User> userSqlGenerator = new SqlGenerator<User>(_sqlConnector, true);
            var sqlQuery = userSqlGenerator.GetCount(null, user => user.AddressId);
            Assert.Equal("SELECT COUNT(DISTINCT `Users`.`AddressId`) FROM `Users` WHERE `Users`.`Deleted` != 1", sqlQuery.GetSql());
        }

        [Fact]
        public static void CountWithDistinctAndWhere()
        {
            ISqlGenerator<User> userSqlGenerator = new SqlGenerator<User>(_sqlConnector, true);
            var sqlQuery = userSqlGenerator.GetCount(x => x.PhoneId == 1, user => user.AddressId);
            Assert.Equal("SELECT COUNT(DISTINCT `Users`.`AddressId`) FROM `Users` WHERE `Users`.`PhoneId` = @PhoneId AND `Users`.`Deleted` != 1", sqlQuery.GetSql());
        }

        [Fact]
        public void BulkUpdate()
        {
            ISqlGenerator<Phone> userSqlGenerator = new SqlGenerator<Phone>(_sqlConnector);
            var phones = new List<Phone>
            {
                new Phone { Id = 10, IsActive = true, Number = "111" },
                new Phone { Id = 10, IsActive = false, Number = "222" }
            };

            var sqlQuery = userSqlGenerator.GetBulkUpdate(phones);

            Assert.Equal("UPDATE DAB.Phones SET Number = @Number0, IsActive = @IsActive0 WHERE Id = @Id0; " +
                         "UPDATE DAB.Phones SET Number = @Number1, IsActive = @IsActive1 WHERE Id = @Id1", sqlQuery.GetSql());
        }
        
        [Fact]
        public void BulkUpdate_QuoMarks()
        {
            ISqlGenerator<Phone> userSqlGenerator = new SqlGenerator<Phone>(_sqlConnector, true);
            var phones = new List<Phone>
            {
                new Phone { Id = 10, IsActive = true, Number = "111" },
                new Phone { Id = 10, IsActive = false, Number = "222" }
            };

            var sqlQuery = userSqlGenerator.GetBulkUpdate(phones);

            Assert.Equal("UPDATE `DAB`.`Phones` SET `Number` = @Number0, `IsActive` = @IsActive0 WHERE `Id` = @Id0; " +
                         "UPDATE `DAB`.`Phones` SET `Number` = @Number1, `IsActive` = @IsActive1 WHERE `Id` = @Id1", sqlQuery.GetSql());
        }

        [Fact]
        public void Insert_QuoMarks()
        {
            ISqlGenerator<Address> userSqlGenerator = new SqlGenerator<Address>(_sqlConnector, true);
            var sqlQuery = userSqlGenerator.GetInsert(new Address());

            Assert.Equal("INSERT INTO `Addresses` (`Street`, `CityId`) VALUES (@Street, @CityId); SELECT CONVERT(LAST_INSERT_ID(), SIGNED INTEGER) AS `Id`", sqlQuery.GetSql());
        }

        [Fact]
        public void IsNotNull()
        {
            ISqlGenerator<User> userSqlGenerator = new SqlGenerator<User>(_sqlConnector, true);
            var sqlQuery = userSqlGenerator.GetSelectAll(user => user.UpdatedAt != null);

            Assert.Equal("SELECT `Users`.`Id`, `Users`.`Name`, `Users`.`AddressId`, `Users`.`PhoneId`, `Users`.`OfficePhoneId`, `Users`.`Deleted`, `Users`.`UpdatedAt` FROM `Users` " +
                         "WHERE `Users`.`UpdatedAt` IS NOT NULL AND `Users`.`Deleted` != 1", sqlQuery.GetSql());
            Assert.DoesNotContain("!= NULL", sqlQuery.GetSql());
        }


        [Fact]
        public void IsNotNullAnd()
        {
            ISqlGenerator<User> userSqlGenerator = new SqlGenerator<User>(_sqlConnector, true);
            var sqlQuery = userSqlGenerator.GetSelectAll(user => user.Name == "Frank" && user.UpdatedAt != null);

            Assert.Equal("SELECT `Users`.`Id`, `Users`.`Name`, `Users`.`AddressId`, `Users`.`PhoneId`, `Users`.`OfficePhoneId`, `Users`.`Deleted`, `Users`.`UpdatedAt` FROM `Users` " +
                         "WHERE `Users`.`Name` = @Name AND `Users`.`UpdatedAt` IS NOT NULL AND `Users`.`Deleted` != 1", sqlQuery.GetSql());
            Assert.DoesNotContain("!= NULL", sqlQuery.GetSql());

            sqlQuery = userSqlGenerator.GetSelectAll(user => user.UpdatedAt != null && user.Name == "Frank");
            Assert.Equal("SELECT `Users`.`Id`, `Users`.`Name`, `Users`.`AddressId`, `Users`.`PhoneId`, `Users`.`OfficePhoneId`, `Users`.`Deleted`, `Users`.`UpdatedAt` FROM `Users` " +
                         "WHERE `Users`.`UpdatedAt` IS NOT NULL AND `Users`.`Name` = @Name AND `Users`.`Deleted` != 1", sqlQuery.GetSql());
            Assert.DoesNotContain("!= NULL", sqlQuery.GetSql());
        }


        [Fact]
        public void SelectBetween()
        {
            ISqlGenerator<User> userSqlGenerator = new SqlGenerator<User>(_sqlConnector, true);
            var sqlQuery = userSqlGenerator.GetSelectBetween(1, 10, x => x.Id);

            Assert.Equal("SELECT `Users`.`Id`, `Users`.`Name`, `Users`.`AddressId`, `Users`.`PhoneId`, `Users`.`OfficePhoneId`, `Users`.`Deleted`, `Users`.`UpdatedAt` FROM `Users` " +
                         "WHERE `Users`.`Deleted` != 1 AND `Users`.`Id` BETWEEN '1' AND '10'", sqlQuery.GetSql());
        }

        [Fact]
        public void SelectById()
        {
            ISqlGenerator<Address> sqlGenerator = new SqlGenerator<Address>(_sqlConnector, false);
            var sqlQuery = sqlGenerator.GetSelectById(1, true);
            Assert.Equal("SELECT Addresses.Id, Addresses.Street, Addresses.CityId FROM Addresses WHERE Addresses.Id = @Id LIMIT 1", sqlQuery.GetSql());
        }

        [Fact]
        public void SelectFirst()
        {
            ISqlGenerator<City> sqlGenerator = new SqlGenerator<City>(_sqlConnector, false);
            var sqlQuery = sqlGenerator.GetSelectFirst(x => x.Identifier == Guid.Empty, true);
            Assert.Equal("SELECT Cities.Identifier, Cities.Name FROM Cities WHERE Cities.Identifier = @Identifier LIMIT 1", sqlQuery.GetSql());
        }

        [Fact]
        public void SelectFirst2()
        {
            ISqlGenerator<City> sqlGenerator = new SqlGenerator<City>(_sqlConnector, false);
            var sqlQuery = sqlGenerator.GetSelectFirst(x => x.Identifier == Guid.Empty && x.Name == "", true);
            Assert.Equal("SELECT Cities.Identifier, Cities.Name FROM Cities WHERE Cities.Identifier = @Identifier AND Cities.Name = @Name LIMIT 1", sqlQuery.GetSql());
        }

        [Fact]
        public void SelectFirst_QuoMarks()
        {
            ISqlGenerator<City> sqlGenerator = new SqlGenerator<City>(_sqlConnector, true);
            var sqlQuery = sqlGenerator.GetSelectFirst(x => x.Identifier == Guid.Empty, true);
            Assert.Equal("SELECT `Cities`.`Identifier`, `Cities`.`Name` FROM `Cities` WHERE `Cities`.`Identifier` = @Identifier LIMIT 1", sqlQuery.GetSql());
        }


        [Fact]
        public void SelectFirstWithDeleted()
        {
            ISqlGenerator<User> userSqlGenerator = new SqlGenerator<User>(_sqlConnector, true);
            var sqlQuery = userSqlGenerator.GetSelectFirst(x => x.Id == 6, true);
            Assert.Equal("SELECT `Users`.`Id`, `Users`.`Name`, `Users`.`AddressId`, `Users`.`PhoneId`, `Users`.`OfficePhoneId`, `Users`.`Deleted`, `Users`.`UpdatedAt` FROM `Users` WHERE `Users`.`Id` = @Id AND `Users`.`Deleted` != 1 LIMIT 1", sqlQuery.GetSql());
        }
    }
}