-- Create a new database called 'ticket_resell'
-- Create a new database called 'ticket_resell'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT [name]
        FROM sys.databases
        WHERE [name] = N'ticket_resell'
)
CREATE DATABASE ticket_resell
GO

USE ticket_resell
GO

-- Create a new table called '[roles]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[roles]', 'U') IS NOT NULL
DROP TABLE [dbo].[roles]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[roles]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- Primary Key column
    [Name] NVARCHAR(50) NOT NULL
);
GO

-- Insert rows into table 'roles' in schema '[dbo]'
INSERT INTO [dbo].[roles]
( -- Columns to insert data into
    [Name]
)
VALUES
('admin'),('staff'),('member')
-- Add more rows here
GO

-- Create a new table called '[user_status]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[user_status]', 'U') IS NOT NULL
DROP TABLE [dbo].[user_status]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[user_status]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- Primary Key column
    [Status] NVARCHAR(50) NOT NULL,
);
GO

-- Insert rows into table 'user_status' in schema '[dbo]'
INSERT INTO [dbo].[user_status]
( -- Columns to insert data into
 [Status]
)
VALUES
('active'),('removed'),('banned')
GO

-- Create a new table called '[users]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[users]', 'U') IS NOT NULL
DROP TABLE [dbo].[users]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[users]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- Primary Key column
    [Email] NVARCHAR(100) NOT NULL,
    [Username] NVARCHAR(30) NOT NULL,
    [Password] VARCHAR(255) NOT NULL,
    [Avatar] VARCHAR(255) NULL,
    [Rating] FLOAT DEFAULT 0,
    [Reputation] INT DEFAULT 100,
    [RoleId] INT NOT NULL,
    [StatusId] INT NOT NULL,
    [CreateAt] DATETIME DEFAULT GETDATE(),
    [UpdateAt] DATETIME DEFAULT GETDATE(),
    -- Foreign key
    FOREIGN KEY ([RoleId]) REFERENCES [dbo].[roles]([Id]),
    FOREIGN KEY ([StatusId]) REFERENCES [dbo].[user_status]([Id])
);
GO

CREATE TRIGGER trg_UpdateAt_OnUpdate_User
ON [dbo].[users]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[users]
    SET [UpdateAt] = GETDATE()
    FROM [dbo].[users] u
    INNER JOIN inserted i ON u.Id = i.Id;
END;
GO

-- Create a new table called '[ticket_types]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[ticket_types]', 'U') IS NOT NULL
DROP TABLE [dbo].[ticket_types]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[ticket_types]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- Primary Key column
    [Type] NVARCHAR(50) NOT NULL,
);
GO

-- Insert rows into table 'ticket_types' in schema '[dbo]'
INSERT INTO [dbo].[ticket_types]
( -- Columns to insert data into
 [Type]
)
VALUES
('movie'),('event'),('voucher'),('transportation'),('amusement'),('tourist')
GO

-- Create a new table called '[ticket_status]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[ticket_status]', 'U') IS NOT NULL
DROP TABLE [dbo].[ticket_status]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[ticket_status]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- Primary Key column
    [Status] NVARCHAR(50) NOT NULL,
);
GO

INSERT INTO [dbo].[ticket_status]
( -- Columns to insert data into
 [Status]
)
VALUES
('pending'),('approved'),('rejected'),('expired'),('sold'),('removed')
GO

-- Create a new table called '[tickets]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[tickets]', 'U') IS NOT NULL
DROP TABLE [dbo].[tickets]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[tickets]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- Primary Key column
    [Title] NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(1000) NULL,
    [Image] VARCHAR(255) NOT NULL,
    [OwnerId] INT NOT NULL,
    [TypeId] INT NOT NULL,
    [StatusId] INT NOT NULL,
    [ExpiryDate] DATETIME NOT NULL,
    [Price] INT NOT NULL,
    [Quantity] INT NOT NULL,
    [CreateAt] DATETIME DEFAULT GETDATE(),
    [UpdateAt] DATETIME DEFAULT GETDATE(),
    --Foreign key
    FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[users]([Id]),
    FOREIGN KEY ([TypeId]) REFERENCES [dbo].[ticket_types]([Id]),
    FOREIGN KEY ([StatusId]) REFERENCES [dbo].[ticket_status]([Id]),
);
GO

CREATE TRIGGER trg_UpdateAt_OnUpdate_Ticket
ON [dbo].[tickets]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[tickets]
    SET [UpdateAt] = GETDATE()
    FROM [dbo].[tickets] t
    INNER JOIN inserted i ON t.Id = i.Id;
END;
GO

-- Create a new table called '[chatbox_status]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[chatbox_status]', 'U') IS NOT NULL
DROP TABLE [dbo].[chatbox_status]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[chatbox_status]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- Primary Key column
    [Status] NVARCHAR(50) NOT NULL,
);
GO

-- Insert rows into table 'chatbox_status' in schema '[dbo]'
INSERT INTO [dbo].[chatbox_status]
( -- Columns to insert data into
    [Status]
)
VALUES
('trading'),('completed'),('cancelled')
GO

-- Create a new table called '[chatboxes]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[chatboxes]', 'U') IS NOT NULL
DROP TABLE [dbo].[chatboxes]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[chatboxes]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- Primary Key column
    [BuyerId] INT NOT NULL,
    [SellerId] INT NOT NULL,
    [TicketId] INT NOT NULL,
    [StatusId] INT NOT NULL,
    [CreateAt] DATETIME DEFAULT GETDATE(),
    --FK
    FOREIGN KEY ([BuyerId]) REFERENCES [dbo].[users]([Id]),
    FOREIGN KEY ([SellerId]) REFERENCES [dbo].[users]([Id]),
    FOREIGN KEY ([TicketId]) REFERENCES [dbo].[tickets]([Id]),
    FOREIGN KEY ([StatusId]) REFERENCES [dbo].[chatbox_status]([Id]),
);
GO

-- Create a new table called '[chat_messages]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[chat_messages]', 'U') IS NOT NULL
DROP TABLE [dbo].[chat_messages]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[chat_messages]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    -- Specify more columns here
    [ChatBoxId] INT NOT NULL,
    [SenderId] INT NOT NULL,
    [Message] NVARCHAR(255) NOT NULL,
    [CreateAt] DATETIME DEFAULT GETDATE(),
    FOREIGN KEY ([ChatBoxId]) REFERENCES [dbo].[chatboxes]([Id]),
    FOREIGN KEY ([SenderId]) REFERENCES [dbo].[users]([Id]),
);
GO

-- Create a new table called '[orders]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[orders]', 'U') IS NOT NULL
DROP TABLE [dbo].[orders]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[orders]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- Primary Key column
    [ChatBoxId] INT NOT NULL,
    [Quantity] INT NOT NULL,
    [CreateAt] DATETIME DEFAULT GETDATE(),
    -- Specify more columns here
    FOREIGN KEY ([ChatBoxId]) REFERENCES [dbo].[chatboxes]([Id])
);
GO

-- Create a new table called '[feedbacks]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[feedbacks]', 'U') IS NOT NULL
DROP TABLE [dbo].[feedbacks]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[feedbacks]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- Primary Key column
    [OrderId] INT NOT NULL,
    [Message] NVARCHAR(255) NOT NULL,
    [Rating] INT NOT NULL,
    [CreateAt] DATETIME DEFAULT GETDATE(),
    -- Specify more columns here
    FOREIGN KEY ([OrderId]) REFERENCES [dbo].[orders]([Id])
);
GO

-- Create a new table called '[report_status]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[report_status]', 'U') IS NOT NULL
DROP TABLE [dbo].[report_status]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[report_status]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- Primary Key column
    [Status] NVARCHAR(50) NOT NULL,
);
GO

INSERT INTO [dbo].[report_status]
( -- Columns to insert data into
    [Status]
)
VALUES
('pending'),('approved'),('rejected')
GO

IF OBJECT_ID('[dbo].[reports]', 'U') IS NOT NULL
DROP TABLE [dbo].[reports]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[reports]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- Primary Key column
    [OrderId] INT NOT NULL,
    [SenderId] INT NOT NULL,
    [Message] NVARCHAR(255) NOT NULL,
    [Evidence] VARCHAR(255) NOT NULL,
    [StatusId] INT NOT NULL,
    [CreateAt] DATETIME DEFAULT GETDATE(),
    [UpdateAt] DATETIME DEFAULT GETDATE(),
    -- Specify more columns here
    FOREIGN KEY ([OrderId]) REFERENCES [dbo].[orders]([Id]),
    FOREIGN KEY ([SenderId]) REFERENCES [dbo].[users]([Id]),
    FOREIGN KEY ([StatusId]) REFERENCES [dbo].[report_status]([Id]),
);
GO

CREATE TRIGGER trg_UpdateAt_OnUpdate_Report
ON [dbo].[reports]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[reports]
    SET [UpdateAt] = GETDATE()
    FROM [dbo].[reports] r
    INNER JOIN inserted i ON r.Id = i.Id;
END;
GO

-- Create a new table called '[subcriptions]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[subcriptions]', 'U') IS NOT NULL
DROP TABLE [dbo].[subcriptions]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[subcriptions]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- Primary Key column
    [Name] NVARCHAR(50) NOT NULL,
    [Description] NVARCHAR(500) NOT NULL,
    [SaleLimit] INT NOT NULL,
    [Price] INT NOT NULL,
    -- Specify more columns here
);
GO

-- Create a new table called '[membership_status]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[membership_status]', 'U') IS NOT NULL
DROP TABLE [dbo].[membership_status]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[membership_status]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- Primary Key column
    [Status] NVARCHAR(50) NOT NULL,
);
GO

INSERT INTO [dbo].[membership_status]
( -- Columns to insert data into
    [Status]
)
VALUES
('pending'),('processing'),('cancelled'),('paid'),('expired')
GO

-- Create a new table called '[memberships]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[memberships]', 'U') IS NOT NULL
DROP TABLE [dbo].[memberships]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[memberships]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- Primary Key column
    [OwnerId] INT NOT NULL,
    [SupscriptionId] INT NOT NULL,
    [OrderCode] VARCHAR(50) NULL,
    [StartDate] DATETIME DEFAULT GETDATE(),
    [EndDate] DATETIME NOT NULL,
    [StatusId] INT NOT NULL,
    -- Specify more columns here
    FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[users]([Id]),
    FOREIGN KEY ([SupscriptionId]) REFERENCES [dbo].[subcriptions]([Id]),
    FOREIGN KEY ([StatusId]) REFERENCES [dbo].[membership_status]([Id]),
);
GO