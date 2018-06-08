CREATE TABLE Client(
ClientID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
Name varchar(200) NOT NULL,
Surname varchar(200) NOT NULL,
Age int  NOT NULL, 
W_ex int  NOT NULL,
Email varchar(200) NOT NULL,
Phone nvarchar (200) NOT NULL,
[Username] varchar(255),
[Password] varchar(255),
constraint check_Client_Age check(Age > 18  and Age < 100),
constraint check_Client_W_ex check(W_ex > 1  and W_ex < 25),
constraint unique_Client_Username unique([Username]),
constraint check_Client_Phone check(Phone like '+[0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9]')
);

CREATE TABLE Flower(
FlowerID INT IDENTITY(1,1) NOT NULL primary key,
Name varchar(200) NOT NULL,
Country varchar(200) NOT NULL,
Price float not null);

CREATE TABLE Orders(
OrdersID int IDENTITY(1,1) not null primary key,
FlowerID int not null,
ClientID int not null,
Price float not null,
Quantity int not null,
Sell_Date date not null,
constraint FKOrdersFlowerID foreign key(FlowerID) references Flower(FlowerID),
CONSTRAINT FKOrdersClientID Foreign Key (ClientID) references Client(ClientID),
);

create table OrdersLogs(
	ID int,
	FlowerID int, 
	ClientID int, 
	"Sell_Date" date, 
	Quantity int,
	[Person] varchar(255),
	[Act] varchar(255)
);


go
create trigger OrdersLogsTr on Orders after insert,update,delete as
begin
	if exists(select 1 from inserted) and exists(select 1 from deleted)
		insert into OrdersLogs (ID,FlowerID,ClientID,Sell_Date, Quantity,[Act], [Person]) select OrdersID,FlowerID,ClientID,  Sell_Date, Quantity, 'Updated',(select CURRENT_USER) from deleted
	else if exists(select 1 from inserted) and not exists(select 1 from deleted)
		insert into OrdersLogs (ID,FlowerID,ClientID, Sell_Date, Quantity,[Act], [Person]) select OrdersID,FlowerID,ClientID, Sell_Date, Quantity, 'Inserted',(select CURRENT_USER) from inserted
	else 
		insert into OrdersLogs (ID,FlowerID,ClientID, Sell_Date, Quantity,[Act], [Person]) select OrdersID,FlowerID,ClientID, Sell_Date, Quantity, 'Deleted', (select CURRENT_USER) from deleted
end
go


go
	CREATE FUNCTION User_created(@Username varchar(255), @Password varchar(255))  returns bit
	as 
	begin
		if exists(select 1 from Client where [Username]=@Username and [Password]=@Password)
			return 1
		return 0
	end
go

go
	CREATE PROCEDURE New_user (@Username varchar(255), @Password varchar(255), @Surname varchar(255), @Name varchar(255), @Phone varchar(255), @Email varchar(255), 
	 @Age int, @W_ex int) as 
	begin 
		INSERT INTO Client([Username], [Password], Surname, Name, Phone,Email, Age, W_ex) 
		values (@Username, @Password, @Surname, @Name, @Phone, @Email, @Age, @W_ex)
	end
go


insert into Client values ('Said', 'Abduvaliev', '33', '3', 'sabduvaliev@gmail.com', '+998-99-777-77-77', 'Said', '123' )
insert into Client values ('Maruf', 'Nabiev', '23', '3', 'mnabiev@gmail.com', '+998-99-788-88-88', 'Maruf', '123' )
insert into Client values ('Eldor', 'Pulatov', '43', '3', 'epulatov@gmail.com', '+998-99-999-99-99', 'Eldor', '123' )
insert into Client values ('Temur', 'Amirov', '35', '3', 'tamirov@gmail.com', '+998-99-711-11-11', 'Temur', '123' )
insert into Client values ('Ozod', 'Nazarbekov', '53', '3', 'onazar@gmail.com', '+998-99-755-55-55', 'Ozod', '123' )

insert into Flower(Name, Country, Price) values('Tulip','Russia',35000);
insert into Flower(Name, Country, Price) values('Adonis','Georgia',5000);
insert into Flower(Name, Country, Price) values('Allium','Uzbekistan',55000);
insert into Flower(Name, Country, Price) values('Anturium','Kazahstan',85000);
insert into Flower(Name, Country, Price) values('Aquilegia','USA',95000);
insert into Flower(Name, Country, Price) values('Aster','Australia',25000);
insert into Flower(Name, Country, Price) values('Bluebell','Sweden',35000);
insert into Flower(Name, Country, Price) values('Buttercup','Polland',55000);
insert into Flower(Name, Country, Price) values('Calendula','Uzbekistan',25000);
insert into Flower(Name, Country, Price) values('Toadflax','Polland',56000);
insert into Flower(Name, Country, Price) values('Sneezewort ','Uzbekistan',15000);

insert into Orders (FlowerID, ClientID, Price, Quantity, Sell_Date) values ('2','1', '5000', '5','2016-12-12')
insert into Orders (FlowerID, ClientID, Price, Quantity, Sell_Date) values ('1','2', '55000', '5','2016-12-12')
insert into Orders (FlowerID, ClientID, Price, Quantity, Sell_Date) values ('2','2', '5000', '5','2016-12-12')
insert into Orders (FlowerID, ClientID, Price, Quantity, Sell_Date) values ('4','4', '85000', '5','2016-12-12')
insert into Orders (FlowerID, ClientID, Price, Quantity, Sell_Date) values ('3','3', '55000', '5','2016-12-12')
insert into Orders (FlowerID, ClientID, Price, Quantity, Sell_Date) values ('2','1', '5000', '5','2016-12-12')
insert into Orders (FlowerID, ClientID, Price, Quantity, Sell_Date) values ('2','1', '5500', '5','2016-12-12')
insert into Orders (FlowerID, ClientID, Price, Quantity, Sell_Date) values ('1','2', '55000', '5','2016-12-12')
insert into Orders (FlowerID, ClientID, Price, Quantity, Sell_Date) values ('2','2', '5050', '5','2016-12-12')
insert into Orders (FlowerID, ClientID, Price, Quantity, Sell_Date) values ('4','4', '75000', '5','2016-12-12')
insert into Orders (FlowerID, ClientID, Price, Quantity, Sell_Date) values ('3','3', '65000', '5','2016-12-12')
insert into Orders (FlowerID, ClientID, Price, Quantity, Sell_Date) values ('2','1', '7000', '5','2016-12-12')


