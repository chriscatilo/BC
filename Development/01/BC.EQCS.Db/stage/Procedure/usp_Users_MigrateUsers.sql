CREATE PROCEDURE [stage].[usp_Users_MigrateUsers] AS 
BEGIN

	MERGE [dbo].[ApplicationUser] AS Target
		USING (
			SELECT DISTINCT ObjectGUID, FirstName, Surname, Email, Login, [ExternalUserName], [ExternalUserPasswordHash],[ExternalUserSecurityStamp]
			FROM [stage].[Users]
			) AS source ([ObjectGUID], [FirstName],[Surname],[Email],[Login], [ExternalUserName], [ExternalUserPasswordHash],[ExternalUserSecurityStamp])
		ON (target.ObjectGUID = source.ObjectGUID)

		-- update existing user matched with stage.USer
		WHEN MATCHED THEN
			UPDATE SET
				DisplayName = source.FirstName + ' ' + source.Surname,
				FirstName = source.FirstName,
				Surname = source.Surname,
				Login = source.Login,
				Enabled = 1

		-- create new user 
		WHEN NOT MATCHED BY TARGET THEN
			INSERT ([ObjectGUID], [DisplayName], [FirstName],[Surname],[Email],[Login], [Enabled], [ExternalUserName], [ExternalUserPasswordHash],[ExternalUserSecurityStamp])
			VALUES ([ObjectGUID], [FirstName] + ' ' + [Surname], [FirstName],[Surname],[Email],[Login], 1, [ExternalUserName], [ExternalUserPasswordHash],[ExternalUserSecurityStamp])

		-- disable user when not in stage.Users
		WHEN NOT MATCHED BY SOURCE THEN
			UPDATE SET
				Enabled = 0;

END