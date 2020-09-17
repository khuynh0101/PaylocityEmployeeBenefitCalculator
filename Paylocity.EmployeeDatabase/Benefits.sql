CREATE TABLE [dbo].[Benefits]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (0, 1), 
    [EmployeeId] INT NOT NULL, 
    [EmployeeCost] DECIMAL NULL, 
    [DependentCost] DECIMAL NULL, 
    CONSTRAINT [FK_Benefits_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([EmployeeId])
)
