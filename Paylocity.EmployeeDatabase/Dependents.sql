CREATE TABLE [dbo].[Dependents]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (0, 1), 
    [EmployeeId] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Dependents_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([EmployeeId])
)
