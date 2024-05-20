CREATE DATABASE QuanLiNhaSach;
GO

USE QuanLiNhaSach;
GO

CREATE TABLE GenreBook (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    DisplayName NVARCHAR(MAX),
    IsDeleted BIT DEFAULT 0
);
GO

CREATE TABLE Book (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    DisplayName NVARCHAR(MAX),
    Price MONEY DEFAULT 0,
    IDGenre INT,
    Inventory INT DEFAULT 0,
    Author NVARCHAR(MAX),
    Description NVARCHAR(MAX),
    Image VARCHAR(MAX),
    IsDeleted BIT DEFAULT 0,
    PublishYear INT,
    Publisher NVARCHAR(MAX),
    CONSTRAINT FK_Book_Genre FOREIGN KEY (IDGenre) REFERENCES GenreBook(ID)
);
GO

CREATE TABLE Staff (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    DisplayName NVARCHAR(MAX),
    StartDate SMALLDATETIME DEFAULT GETDATE(),
    UserName VARCHAR(MAX),
    PassWord VARCHAR(MAX),
    PhoneNumber VARCHAR(20),
    BirthDay SMALLDATETIME,
    Wage MONEY,
    Status NVARCHAR(100) DEFAULT N'Đang làm',
    Email VARCHAR(MAX),
    Gender NCHAR(3) NOT NULL,
    Role NVARCHAR(100) DEFAULT N'Nhân Viên',
    IsDeleted BIT DEFAULT 0,
    CONSTRAINT CHECK_STAFF CHECK (
        (YEAR(StartDate) - YEAR(BirthDay)) >= 16 AND 
        Role IN (N'Nhân Viên', N'Quản lí') AND 
        Gender IN (N'Nam', N'Nữ') AND 
        Status IN (N'Đang làm', N'Xin nghỉ')
    )
);
GO

CREATE TABLE Customer (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    DisplayName NVARCHAR(MAX),
    Email NVARCHAR(MAX),
    PhoneNumber VARCHAR(20),
    Address NVARCHAR(MAX),
    Description NVARCHAR(MAX),
    Debts MONEY,
    Spend MONEY,
    IsDeleted BIT DEFAULT 0
);
GO

CREATE TABLE Bill (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    IDCus INT,
    IDStaff INT,
    CreateAt SMALLDATETIME DEFAULT GETDATE(),
    TotalPrice MONEY DEFAULT 0,
    Paid MONEY DEFAULT 0,
    IsDeleted BIT DEFAULT 0,
    CONSTRAINT FK_Bill_Customer FOREIGN KEY (IDCus) REFERENCES Customer(ID),
    CONSTRAINT FK_Bill_Staff FOREIGN KEY (IDStaff) REFERENCES Staff(ID)
);
GO

CREATE TABLE BillInfo (
    IDBill INT,
    IDBook INT,
    PriceItem MONEY DEFAULT 0,
    Quantity INT DEFAULT 0,
    IsDeleted BIT DEFAULT 0,
    PRIMARY KEY (IDBill, IDBook),
    CONSTRAINT FK_BillInfo_Bill FOREIGN KEY (IDBill) REFERENCES Bill(ID),
    CONSTRAINT FK_BillInfo_Book FOREIGN KEY (IDBook) REFERENCES Book(ID)
);
GO

CREATE TABLE GoodReceived (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    CreatAt SMALLDATETIME DEFAULT GETDATE(),
    StaffId INT,
    Total MONEY,
    IsDeleted BIT DEFAULT 0,
    CONSTRAINT FK_GoodReceived_Staff FOREIGN KEY (StaffId) REFERENCES Staff(ID)
);
GO

CREATE TABLE GoodReceivedInfo (
    IDGoodReceived INT,
    IDBook INT,
    Quantity INT DEFAULT 0,
    TotalPriceItem MONEY,
    IsDeleted BIT DEFAULT 0,
    PRIMARY KEY (IDGoodReceived, IDBook),
    CONSTRAINT FK_GoodReceivedInfo_GoodReceived FOREIGN KEY (IDGoodReceived) REFERENCES GoodReceived(ID),
    CONSTRAINT FK_GoodReceivedInfo_Book FOREIGN KEY (IDBook) REFERENCES Book(ID)
);
GO

CREATE TABLE PaymentReceipt (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    IDCus INT,
    CreatAt SMALLDATETIME DEFAULT GETDATE(),
    AmountReceived MONEY DEFAULT 0,
    IsDeleted BIT DEFAULT 0,
    CONSTRAINT FK_PaymentReceipt_Customer FOREIGN KEY (IDCus) REFERENCES Customer(ID)
);
GO

CREATE TABLE SystemValue (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    MinReceived INT DEFAULT 150,
    MaxInventory INT DEFAULT 300,
    MaxDebts MONEY DEFAULT 1000000,
    MinSaleInventory INT DEFAULT 20,
    Profit FLOAT DEFAULT 0.05,
    DebtsPolicy BIT
);
GO

CREATE TABLE InventoryReport (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    BookId INT,
    FirstIvt INT,
    LastIvt INT,
    Arise INT,
    MonthYear VARCHAR(7),
    CONSTRAINT FK_InventoryReport_Book FOREIGN KEY (BookId) REFERENCES Book(ID)
);
GO

CREATE TABLE DebtReport (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT,
    FirstDebt MONEY,
    LastDebt MONEY,
    Arise MONEY,
    MonthYear VARCHAR(7),
    CONSTRAINT FK_DebtReport_Customer FOREIGN KEY (CustomerId) REFERENCES Customer(ID)
);
GO
--Add admin account to login(required)
INSERT INTO Staff (DisplayName, UserName, PassWord, PhoneNumber, BirthDay, Wage, Email, Gender, Role) VALUES
(N'Trần Thành Đạt', 'admin', '21232f297a57a5a743894a0e4a801fc3', '0382146455', '2004-01-04', 20000000, 'neban0444@gmail.com', 'Nam', N'Quản lí');
--Password admin
GO
INSERT INTO Staff (DisplayName, UserName, PassWord, PhoneNumber, BirthDay, Wage, Email, Gender) VALUES
(N'Hà Minh Đức', 'minhduc196', '530522dab49efdc13e4718ec3c755d87', '0944662775', '2004-06-19', 200000, 'haminhduchaminhduc@gmail.com', 'Nam');
--Password minhduc196
GO
--System value(required)
INSERT into SystemValue (MinReceived) values (150)
go
--Default customer(required)
INSERT INTO Customer (DisplayName, Spend) VALUES
(N'Khách lẻ', 0);
GO
--Create example data(not required)
--Create gerne book
INSERT INTO GenreBook (DisplayName) VALUES
(N'Thiếu nhi'),
(N'Tâm lý, tình cảm'),
(N'Tôn giáo'),
(N'Văn hóa xã hội'),
(N'Lịch sử'),
(N'Văn học viễn tưởng'),
(N'Tiểu sử, tự truyện'),
(N'Kinh dị, bí ẩn'),
(N'Kĩ năng mềm'),
(N'Khoa học công nghệ'),
(N'Truyền cảm hứng');
GO
--
INSERT INTO Customer (DisplayName, Email, PhoneNumber, Address, Description, Debts, Spend) VALUES
(N'Hà Minh Đức', 'haminhduchaminhduc@gmail.com', '0879917562', N'Thủ Đức', N'Khách vip', 0, 0);
GO
INSERT INTO Customer (DisplayName, Email, PhoneNumber, Address, Debts, Spend) VALUES
(N'Trần Thành Công', 'abc@gmail.com', '0879917567', N'Dĩ An', 0, 0);
GO
INSERT INTO Book (DisplayName, Price, IDGenre, Inventory, Author, Description, Image, PublishYear, Publisher) VALUES
(N'Thế giới cổ tích', 150000, 1, 50, N'Nguyễn Nhật Ánh', N'Bộ sưu tập các câu chuyện cổ tích nổi tiếng', 'https://product.hstatic.net/200000343865/product/chuyen-phieu-luu-vao-the-gioi-co-tich_8e7622d40455477897f3bcfb2153e43c.jpg', 2020, N'Nhà Xuất Bản Kim Đồng'),
(N'Người thầy', 200000, 9, 30, N'Nguyễn Ngọc Ký', N'Câu chuyện truyền cảm hứng của một người thầy đặc biệt', 'https://docsachcungcon.com/wp-content/uploads/2023/04/nguoithay-1.jpg', 2019, N'Nhà Xuất Bản Trẻ'),
(N'Bí mật của nước', 180000, 2, 40, N'Masaru Emoto', N'Khám phá những bí mật của nước qua các thí nghiệm khoa học', 'https://sachhoc.com/image/catalog/Khoinghiep/Bi-mat-cua-nuoc-ebook.jpg', 2018, N'Nhà Xuất Bản Văn Học'),
(N'Trại trẻ đặc biệt của cô Peregrine', 220000, 6, 25, N'Ransom Riggs', N'Một câu chuyện viễn tưởng ly kỳ về những đứa trẻ đặc biệt', 'https://salt.tikicdn.com/media/catalog/product/b/o/boxset.u84.d20161107.t170654.722703.png', 2016, N'Nhà Xuất Bản Hội Nhà Văn'),
(N'Tâm lý học đám đông', 200000, 2, 45, N'Gustave Le Bon', N'Thấu hiểu tâm lý đám đông và tác động của nó', 'https://omegaplus.vn/wp-content/uploads/2018/07/tam-ly-h%E1%BB%8Dc-dam-dong.jpg', 2017, N'Nhà Xuất Bản Lao Động'),
(N'Lịch sử thế giới', 300000, 5, 20, N'Philip Parker', N'Tổng quan về lịch sử thế giới từ cổ đại đến hiện đại', 'https://down-vn.img.susercontent.com/file/vn-11134207-7r98o-low7n5r9xpama7', 2021, N'Nhà Xuất Bản Thế Giới'),
(N'Những bài học cuộc sống', 150000, 9, 50, N'Robin Sharma', N'Những bài học giá trị về cuộc sống và thành công', 'https://cdn0.fahasa.com/media/catalog/product/i/m/image_225532.jpg', 2018, N'Nhà Xuất Bản Lao Động'),
(N'Mật mã Da Vinci', 280000, 8, 30, N'Dan Brown', N'Tiểu thuyết kinh dị nổi tiếng về những bí ẩn của lịch sử', 'https://down-vn.img.susercontent.com/file/vn-11134207-7r98o-lpkz2ttg2w1bcf', 2003, N'Nhà Xuất Bản Văn Học'),
(N'Tiểu sử Steve Jobs', 350000, 7, 15, N'Walter Isaacson', N'Tiểu sử của nhà sáng lập Apple, Steve Jobs', 'https://bizweb.dktcdn.net/100/197/269/products/sach-tieu-su-steve-jobs-tai-ban-alphabooks.jpg?v=1587629624130', 2011, N'Nhà Xuất Bản Trẻ'),
(N'Kỹ năng mềm cho người mới bắt đầu', 170000, 9, 40, N'Trần Thị Minh Thu', N'Những kỹ năng mềm cần thiết cho người mới bắt đầu', 'https://lib.vinhuni.edu.vn/data/62/upload/1301/images//2023/12/t1.jpg', 2019, N'Nhà Xuất Bản Trẻ');
GO
-- Insert 2 bills
INSERT INTO Bill (IDCus, IDStaff, CreateAt, TotalPrice, Paid) VALUES
(1, 1, '2024-05-15', 500000, 500000),
(2, 2, '2024-05-16', 300000, 300000);
GO

-- Insert bill information for the 2 bills
INSERT INTO BillInfo (IDBill, IDBook, PriceItem, Quantity, IsDeleted) VALUES
(1, 1, 150000, 2, 0),
(1, 2, 200000, 1, 0),
(2, 3, 180000, 1, 0),
(2, 4, 220000, 1, 0);
GO




-- các trigger và pro cần thiết

GO
USE QuanLiNhaSach
GO

GO
CREATE PROCEDURE sp_UpdateInventoryForNewMonth
AS
BEGIN
    DECLARE @CurrentMonthYear VARCHAR(7)
    SET @CurrentMonthYear = FORMAT(GETDATE(), 'MM-yyyy')

    INSERT INTO InventoryReport (BookId, FirstIvt, LastIvt, Arise, MonthYear)
    SELECT b.ID, b.Inventory, b.Inventory, 0, @CurrentMonthYear
    FROM Book b
    WHERE NOT EXISTS (
        SELECT 1
        FROM InventoryReport
        WHERE BookId = b.ID AND MonthYear = @CurrentMonthYear
    )
END
GO




GO
CREATE TRIGGER trg_UpdateInventoryReportOnReceive
ON GoodReceivedInfo
AFTER INSERT
AS
BEGIN
    DECLARE @IDBook INT, @Quantity INT, @MonthYear VARCHAR(7)

    SELECT @IDBook = inserted.IDBook, @Quantity = inserted.Quantity
    FROM inserted

    SET @MonthYear = FORMAT(GETDATE(), 'MM-yyyy')

    IF NOT EXISTS (SELECT 1 FROM InventoryReport WHERE BookId = @IDBook AND MonthYear = @MonthYear)
    BEGIN
        INSERT INTO InventoryReport (BookId, FirstIvt, LastIvt, Arise, MonthYear)
        VALUES (@IDBook, 0, @Quantity, @Quantity, @MonthYear)
    END
    ELSE
    BEGIN
        UPDATE InventoryReport
        SET LastIvt = LastIvt + @Quantity,
            Arise = Arise + @Quantity
        WHERE BookId = @IDBook AND MonthYear = @MonthYear
    END
END
GO


GO
CREATE TRIGGER trg_UpdateInventoryReportOnSale
ON BillInfo
AFTER INSERT
AS
BEGIN
    DECLARE @IDBook INT, @Quantity INT, @MonthYear VARCHAR(7)

    SELECT @IDBook = inserted.IDBook, @Quantity = inserted.Quantity
    FROM inserted

    SET @MonthYear = FORMAT(GETDATE(), 'MM-yyyy')

    UPDATE InventoryReport
    SET LastIvt = LastIvt - @Quantity
    WHERE BookId = @IDBook AND MonthYear = @MonthYear
END
GO




