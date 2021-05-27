CREATE TABLE [dbo].[Movie]
(
  [Id] INT NOT NULL IDENTITY(1,1),
  [Title] NVARCHAR(255) NOT NULL,
  [ReleaseDate] DATETIME2, 
  [Genre] NVARCHAR(255) NOT NULL,
  [Price] DECIMAL(3,2) NOT NULL
)