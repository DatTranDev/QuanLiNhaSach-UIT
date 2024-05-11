
Create database QuanLiNhaSach
go
use QuanLiNhaSach
go
create table GenreBook
(
	ID int identity(1,1),
	DisplayName Nvarchar(max),
	Constraint PK_Genre Primary key(ID),
)
go
create table Book
(
	ID int identity(1,1),
	DisplayName Nvarchar(max),
	Price money default 0,
	IDGenre int,
	Inventory int default 0,
	Author Nvarchar(max),
	Description Nvarchar(max),
	Image varchar(max),
	IsDeleted bit default 0,
	Constraint PK_PrD Primary key(ID),
	Constraint FK_PrD_Genre Foreign Key (IDGenre) references GenreBook(ID),
)
go
create table Staff
(
	ID int identity(1,1),
	DisplayName nvarchar(max),
	StartDate smalldatetime default GetDate(),
	UserName varchar(max),
	PassWord varchar(max),
	PhoneNumber varchar(20),
	BirthDay smalldatetime,
	Wage money,
	Status nvarchar(100) default N'Đang làm',
	Email varchar(max),
	Gender nchar(3) not null,
	Role nvarchar(100) default N'Nhân Viên',
	IsDeleted bit default 0,
	Constraint PK_ST Primary key (ID),
	Constraint CHECK_STAFF Check((Year(StartDate)-Year(BirthDay))>=16 and Role in (N'Nhân Viên', N'Quản lí') and Gender in (N'Nam', N'Nữ') and Status in (N'Đang làm', N'Xin nghỉ'))
)
go
create table Customer
(
	ID int identity(1,1),
	DisplayName nvarchar(max),
	Email nvarchar(max),
	PhoneNumber varchar(20),
	Address Nvarchar(max),
	Description nvarchar(max),
	Debts money,
	Spend money,
	IsDeleted bit default 0,
	Constraint PK_Cus Primary key (ID)
)
go
create table Bill
(
	ID int identity(1,1),
	IDCus int,
	IDStaff int,
	CreateAt smalldatetime default GetDate(),
	TotalPrice money default 0,
	Paid money default 0,
	IsDeleted bit default 0,
	Constraint PK_Bill Primary key(ID),
	Constraint FK_Bill_Cus Foreign key (IDCus) references Customer(ID),
	Constraint FK_Bill_Staff Foreign key(IDStaff) references Staff(ID)
)
go
create table BillInfo
(
	IDBill int,
	IDBook int,
	PriceItem money default 0,
	Quantity int default 0,
	IsDeleted bit default 0,
	Constraint PK_BillInfo Primary key (IDBill,IDBook),
	Constraint FK_BillInfo_Bill Foreign key(IDBill) references Bill(ID),
	Constraint FK_BillInfo_PrD Foreign key(IDBook) references Book(ID),
)
go
create table GoodReceived
(
	ID int identity(1,1),
	CreatAt smalldatetime default GetDate(),
	Constraint PK_GoodReceived Primary key (ID),
	IsDeleted bit default 0
)
go
create table GoodReceivedInfo
(
	IDGoodReceived int,
	IDBook int,
	Quantity int default 0,
	IsDeleted bit default 0,
	Constraint PK_GoodReceivedInfo Primary key (IDGoodReceived, IDBook),
	Constraint FK_GRInfo_GR Foreign key (IDGoodReceived) references GoodReceived(ID),
	Constraint FK_GRInfo_Book Foreign key (IDBook) references Book(ID)
)
go
create table PaymentReceipt 
(
	ID int identity(1,1),
	IDCus int,
	CreatAt smalldatetime default GetDate(),
	AmountReceived money default 0,
	IsDeleted bit default 0,
	Constraint PK_PaymentReceipt Primary key (ID),
	Constraint FK_PaymentReceived Foreign key (IDCus) references Customer(ID)
)
create table SystemValue
(
	MinReceived int default 150,
	MaxInventory int default 300,
	MaxDebts money default 1000000,
	MinSaleInventory int default 20,
	Profit float default 0.05,
	DebtsPolicy bit
)
go
insert into Staff (PassWord, UserName, Gender, Role) values ('admin','21232f297a57a5a743894a0e4a801fc3', 'Nam', N'Quản lí')
go
ALter table [dbo].[Systemvalue] add ID int identity(1,1)
go
Alter table [dbo].[Systemvalue] add constraint PK_SystemValue primary key (ID)
go
create table InventoryReport (
	Id int identity(1,1) primary key,
	BookId int,
	FirstIvt int,
	LastIvt int,
	Arise int,
	MonthYear varchar(7),
	constraint FK_IR_Book Foreign key (BookId) references Book(Id) 
)
go
create table DebtReport (
	Id int identity(1, 1) primary key,
	CustomerId int,
	FirstDebt money,
	LastDebt money,
	Arise money,
	MonthYear varchar(7),
	constraint FK_DR_Customer Foreign key (CustomerId) references Customer(Id)
)
go
alter table GoodReceived add
StaffId int, 
constraint FK_GR_Staff foreign key (StaffId) references Staff(Id)
go
INSERT INTO GenreBook(DisplayName) VALUES
(N'Sách thiếu nhi'),
(N'Sách tâm lý, tình cảm'),
(N'Sách tôn giáo'),
(N'Sách văn hóa xã hội'),
(N'Sách lịch sử'),
(N'Sách văn học viễn tưởng'),
(N'Sách tiểu sử, tự truyện'),
(N'Sách kinh dị, bí ẩn'),
(N'Sách dạy nấu ăn'),
(N'Sách khoa học công nghệ'),
(N'Sách truyền cảm hứng');
go
INSERT INTO Staff (DisplayName, UserName, PassWord, PhoneNumber, BirthDay, Wage, Email, Gender)
VALUES (N'Hà Minh Đức', 'minhduc196', 'minhduc196', '0944662775', '2004-06-19', 200000, 'haminhduchaminhduc@gmail.com', 'Nam');

INSERT INTO Customer(DisplayName, Email, PhoneNumber, Address, Description, Debts, Spend)
VALUES (N'Hà Minh Đức', 'haminhduchaminhduc@gmail.com', '0879917562', N'Thủ Đức', N'Không Fa', 200000, 11837848.00);

INSERT INTO Customer(DisplayName, Spend)
VALUES (N'Khách lẻ', 20000);

alter table Book add PublishYear int;
alter table Book add Publisher nvarchar(max);