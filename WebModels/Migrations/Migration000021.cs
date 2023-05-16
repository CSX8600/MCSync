﻿using ClussPro.Base.Data;
using ClussPro.Base.Data.Query;
using System.Collections.Generic;

namespace WebModels.Migrations
{
    /// <summary>
    /// Add auth.PersonalAccessToken. Add Inactivity and Notification fields to security.User
    /// </summary>
    public class Migration000021 : IMigration
    {
        public int MigrationNumber => 21;

        public void Execute(ITransaction transaction)
        {
            ICreateTable createTable = SQLProviderFactory.GetCreateTableQuery();
            createTable.SchemaName = "auth";
            createTable.TableName = "PersonalAccessToken";
            createTable.Columns = new Dictionary<string, FieldSpecification>()
            {
                { "PersonalAccessTokenID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) { IsPrimary = true } },
                { "UserID", new FieldSpecification(FieldSpecification.FieldTypes.BigInt) },
                { "Name", new FieldSpecification(FieldSpecification.FieldTypes.NVarChar, 30) },
                { "Token", new FieldSpecification(FieldSpecification.FieldTypes.NVarChar, 50) },
                { "Expiration", new FieldSpecification(FieldSpecification.FieldTypes.DateTime2, 7) },
                { "CanRefreshInactivity", new FieldSpecification(FieldSpecification.FieldTypes.Bit) { DefaultValue = false } },
                { "CanPerformNetworkPrinting", new FieldSpecification(FieldSpecification.FieldTypes.Bit) { DefaultValue = false } }
            };
            createTable.Execute(transaction);

            IAlterTable alter = SQLProviderFactory.GetAlterTableQuery();
            alter.Schema = "auth";
            alter.Table = "PersonalAccessToken";
            alter.AddForeignKey("FKPersonalAccessToken_User_UserID", "UserID", "security", "User", "UserID", transaction);

            alter.Schema = "security";
            alter.Table = "User";
            alter.AddColumn("LastActivity", new FieldSpecification(FieldSpecification.FieldTypes.DateTime2, 7), transaction);
            alter.AddColumn("InactivityWarningServed", new FieldSpecification(FieldSpecification.FieldTypes.Bit), transaction);
            alter.AddColumn("DiscordID", new FieldSpecification(FieldSpecification.FieldTypes.NVarChar, 18), transaction);
        }
    }
}