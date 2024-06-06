


DROP TABLE [dbo].[PaymentSlip];







CREATE TABLE [dbo].[PaymentSlip] (
    [Id]            INT           IDENTITY(1,1) NOT NULL,
    [Date]          NVARCHAR (50)           NULL,
    [AccountNumber] NVARCHAR (50)           NULL,
    [Client]        NVARCHAR (50)           NULL,
    [Summa]         Money                   NULL,
    PRIMARY KEY ([Id] ASC)
);

-- Вставка записей без указания Id (автоинкремент)
INSERT INTO [dbo].[PaymentSlip] ([Date], [AccountNumber], [Client], [Summa])
VALUES
('2024-05-30', '123456', 'John Doe', 1000.00),
('2024-06-01', '789012', 'Jane Smith', 1500.50),
('2024-06-15', '345678', 'Alice Johnson', 2000.75),
('2024-07-01', '901234', 'Bob Brown', 300.00),
('2024-07-10', '567890', 'Charlie Davis', 450.25);