﻿using System.Collections.Generic;
using ClussPro.Base.Data;
using ClussPro.Base.Data.Query;

namespace WebModels.Migrations
{
    public class Migration000025 : IMigration
    {
        public int MigrationNumber => 25;

        public void Execute(ITransaction transaction)
        {
            CreateNewTables(transaction);
            UpdateTables(transaction);
        }

        private void CreateNewTables(ITransaction transaction)
        {
            ICreateTable createTable = SQLProviderFactory.GetCreateTableQuery();
            createTable.SchemaName = "company";
            createTable.TableName = "Register";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "RegisterID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "LocationID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "Identifier", new FieldSpecification(FieldSpecification.FieldTypes.UniqueIdentifier) },
                { "Name", new FieldSpecification(FieldSpecification.FieldTypes.NVarChar, 30) }
            };
            createTable.Execute(transaction);
            CreateForeignKey(createTable, transaction, "company", "Location");

            createTable.TableName = "RegisterStatus";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "RegisterStatusID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "RegisterID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "ChangeTime", new FieldSpecification(FieldSpecification.FieldTypes.DateTime2, 7) },
                { "Status", new FieldSpecification(FieldSpecification.FieldTypes.Int) },
                { "Initiator", new FieldSpecification(FieldSpecification.FieldTypes.NVarChar, 50) }
            };
            createTable.Execute(transaction);
            CreateForeignKey(createTable, transaction, "company", "Register");

            createTable.TableName = "LocationItem";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "LocationItemID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "LocationID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "ItemID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "Quantity", new FieldSpecification(FieldSpecification.FieldTypes.SmallInt) },
                { "BasePrice", new FieldSpecification(FieldSpecification.FieldTypes.Decimal, 9, 2) }
            };
            createTable.Execute(transaction);
            CreateForeignKey(createTable, transaction, "company", "Location");
            CreateForeignKey(createTable, transaction, "mesasys", "Item");

            createTable.TableName = "StoreSale";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "StoreSaleID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "RegisterID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "SaleTime", new FieldSpecification(FieldSpecification.FieldTypes.DateTime2, 7) }
            };
            createTable.Execute(transaction);
            CreateForeignKey(createTable, transaction, "company", "Register");

            createTable.TableName = "StoreSaleItem";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "StoreSaleItemID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "StoreSaleID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "LocationItemID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "RingPrice", new FieldSpecification(FieldSpecification.FieldTypes.Decimal, 9, 2) },
                { "SoldPrice", new FieldSpecification(FieldSpecification.FieldTypes.Decimal, 9, 2) },
                { "DiscountReason", new FieldSpecification(FieldSpecification.FieldTypes.NVarChar, 100) }
            };
            createTable.Execute(transaction);
            CreateForeignKey(createTable, transaction, "company", "StoreSale");
            CreateForeignKey(createTable, transaction, "company", "LocationItem");

            createTable.TableName = "Promotion";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "PromotionID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "LocationID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "StartTime", new FieldSpecification(FieldSpecification.FieldTypes.DateTime2, 7) },
                { "EndTime", new FieldSpecification(FieldSpecification.FieldTypes.DateTime2, 7) },
                { "Name", new FieldSpecification(FieldSpecification.FieldTypes.NVarChar, 50) }
            };
            createTable.Execute(transaction);
            CreateForeignKey(createTable, transaction, "company", "Location");

            createTable.TableName = "PromotionLocationItem";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "PromotionLocationItemID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "PromotionID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "LocationItemID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "PromotionPrice", new FieldSpecification(FieldSpecification.FieldTypes.Decimal, 9, 2) }
            };
            createTable.Execute(transaction);
            CreateForeignKey(createTable, transaction, "company", "Promotion");
            CreateForeignKey(createTable, transaction, "company", "LocationItem");
        }

        private void UpdateTables(ITransaction transaction)
        {
            IAlterTable alterTable = SQLProviderFactory.GetAlterTableQuery();
            alterTable.Schema = "company";
            alterTable.Table = "LocationEmployee";
            alterTable.AddColumn("ManagePrices", new FieldSpecification(FieldSpecification.FieldTypes.Bit) { DefaultValue = false }, transaction);
            alterTable.AddColumn("ManageRegisters", new FieldSpecification(FieldSpecification.FieldTypes.Bit) { DefaultValue = false }, transaction);
            alterTable.AddColumn("ManageInventory", new FieldSpecification(FieldSpecification.FieldTypes.Bit) { DefaultValue = false }, transaction);

            alterTable.Table = "Location";
            alterTable.AddColumn("AccountIDStoreRevenue", new FieldSpecification(FieldSpecification.FieldTypes.BigInt), transaction);
            alterTable.AddColumn("EmailImplementationIDRegisterOffline", new FieldSpecification(FieldSpecification.FieldTypes.BigInt), transaction);
            alterTable.AddForeignKey("FKLocation_Account_AccountIDStoreRevenue", "AccountIDStoreRevenue", "account", "Account", "AccountID", transaction);
            alterTable.AddForeignKey("FKLocation_EmailImplementation_EmailImplementationIDRegisterOffline", "EmailImplementationIDRegisterOffline", "mesasys", "EmailImplementation", "EmailImplementationIDRegisterOffline", transaction);

            alterTable.Schema = "security";
            alterTable.Table = "User";
            alterTable.AddColumn("IsImmersibrook", new FieldSpecification(FieldSpecification.FieldTypes.Bit) { DefaultValue = false }, transaction);
        }

        private void CreateForeignKey(ICreateTable createTable, ITransaction transaction, string schema, string table)
        {
            IAlterTable alterTable = SQLProviderFactory.GetAlterTableQuery();
            alterTable.Schema = createTable.SchemaName;
            alterTable.Table = createTable.TableName;
            alterTable.AddForeignKey($"FK{createTable.TableName}_{table}_{table}ID", $"{table}ID", schema, table, $"{table}ID", transaction);
        }
    }
}
