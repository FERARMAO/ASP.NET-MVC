namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'da94bbf8-125e-4400-b718-97983e93aa54', N'guest@oussy.com', 0, N'AKDiFT5Vz5D0kg/j7q3sFHGJmwUbPbqbvEPrtzhw/3wjNxCGounhErPMwS+9/K9aYQ==', N'713ce43a-4f4a-4cb1-8f7a-32bcbfb1a5c5', NULL, 0, 0, NULL, 1, 0, N'guest@oussy.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'fe76ce37-c650-4627-b666-dff57ce95128', N'admin1@oussy.com', 0, N'AP2s8RCcUXORFBkDuhT+N2bmdwBa6NnAIF2AG6g1fy9be+69MkVCJdAJmI6E2eR8jQ==', N'980036d7-ef2b-4876-9f85-5a87f986ef57', NULL, 0, 0, NULL, 1, 0, N'admin1@oussy.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'd364386b-095b-4180-8de0-d543193d500a', N'CanManageMovie')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fe76ce37-c650-4627-b666-dff57ce95128', N'd364386b-095b-4180-8de0-d543193d500a')
");
        }
        
        public override void Down()
        {
        }
    }
}
