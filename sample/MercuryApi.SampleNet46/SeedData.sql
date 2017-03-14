
INSERT INTO Addresses (Id, Line1, Line2, Postcode) VALUES (1, '35 Portland Drive', 'Norwich', 'NX5 7FO');
INSERT INTO Addresses (Id, Line1, Line2, Postcode) VALUES (2, '62 West Wallaby Street', 'Lincolnshire', 'LN1 2SD');
INSERT INTO Addresses (Id, Line1, Line2, Postcode) VALUES (3, 'SuperWidget Solutions Inc.', 'PineRidge Business Park', 'JD8 EU3');
INSERT INTO Addresses (Id, Line1, Line2, Postcode) VALUES (4, 'WidgetCorp', 'Slough Industrial Estate', 'SL3 9DW');
INSERT INTO Addresses (Id, Line1, Line2, Postcode) VALUES (5, 'Flat 7A Cambridge Court', 'Lexingto', 'LX4 8SK');
INSERT INTO Addresses (Id, Line1, Line2, Postcode) VALUES (6, 'Union Aerospace Corporatio', 'Mars', 'MR5 B8S3');
INSERT INTO Addresses (Id, Line1, Line2, Postcode) VALUES (7, '19 Redbridge Park', 'York', 'YR9 5TL');
INSERT INTO Addresses (Id, Line1, Line2, Postcode) VALUES (8, '53 Yellow Lane', 'Watford', 'WT4 0RD');
INSERT INTO Addresses (Id, Line1, Line2, Postcode) VALUES (9, '1 Factory Lane', 'Peckham', 'PK4 9KC');
INSERT INTO Addresses (Id, Line1, Line2, Postcode) VALUES (10, '22 Castle Street', 'Edinburgh', 'EN62 8CH');

INSERT INTO Customers (Id, AddressId, Name) VALUES (1, 1, 'Jimbob');
INSERT INTO Customers (Id, AddressId, Name) VALUES (2, 5, 'Suzanne');
INSERT INTO Customers (Id, AddressId, Name) VALUES (3, 2, 'Wallace');

INSERT INTO Manufacturers (Id, AddressId, Name) VALUES (1, 3, 'SuperWidget Solutions');
INSERT INTO Manufacturers (Id, AddressId, Name) VALUES (2, 4, 'WidgetCorp');
INSERT INTO Manufacturers (Id, AddressId, Name) VALUES (3, 1, 'Instruments R Us');

INSERT INTO Sellers (Id, AddressId, Name) VALUES (1, 7, 'Randal');
INSERT INTO Sellers (Id, AddressId, Name) VALUES (2, 8, 'ebaySeller4U');
INSERT INTO Sellers (Id, AddressId, Name) VALUES (3, 10, 'GloboCorp Industries');

INSERT INTO Orders (Id, CustomerId, Date) VALUES (1, 1, '2017-01-01');
INSERT INTO Orders (Id, CustomerId, Date) VALUES (2, 2, '2016-12-31');
INSERT INTO Orders (Id, CustomerId, Date) VALUES (3, 3, '2015-09-25');

INSERT INTO Products (Id, ManufacturerId, Name, Price) VALUES (1, 1, 'Premium Leather Office Chair', 100);
INSERT INTO Products (Id, ManufacturerId, Name, Price) VALUES (2, 1, 'Chest of Drawers', 500);
INSERT INTO Products (Id, ManufacturerId, Name, Price) VALUES (3, 1, 'Ornamental Desk Lamp', 110);
INSERT INTO Products (Id, ManufacturerId, Name, Price) VALUES (4, 1, 'Magazine rack', 60)
INSERT INTO Products (Id, ManufacturerId, Name, Price) VALUES (5, 2, 'QWERTY Keyboard (most keys present)', 20);
INSERT INTO Products (Id, ManufacturerId, Name, Price) VALUES (6, 2, 'HDMI cable (24K solid gold)', 8500);
INSERT INTO Products (Id, ManufacturerId, Name, Price) VALUES (7, 3, 'Oboe', 120);
INSERT INTO Products (Id, ManufacturerId, Name, Price) VALUES (8, 3, 'Clarinet', 205);
INSERT INTO Products (Id, ManufacturerId, Name, Price) VALUES (9, 3, '24" Drum', 99);

INSERT INTO OrderProduct (Id, OrderId, ProductId) VALUES (1, 1, 1);
INSERT INTO OrderProduct (Id, OrderId, ProductId) VALUES (2, 2, 2);
INSERT INTO OrderProduct (Id, OrderId, ProductId) VALUES (3, 3, 9);
INSERT INTO OrderProduct (Id, OrderId, ProductId) VALUES (4, 3, 8);

INSERT INTO ProductSeller (Id, ProductId, SellerId) VALUES (1, 1, 1);
INSERT INTO ProductSeller (Id, ProductId, SellerId) VALUES (2, 5, 2);
INSERT INTO ProductSeller (Id, ProductId, SellerId) VALUES (3, 6, 2);
INSERT INTO ProductSeller (Id, ProductId, SellerId) VALUES (4, 7, 3);
INSERT INTO ProductSeller (Id, ProductId, SellerId) VALUES (5, 8, 3);
INSERT INTO ProductSeller (Id, ProductId, SellerId) VALUES (6, 9, 3);
INSERT INTO ProductSeller (Id, ProductId, SellerId) VALUES (7, 2, 3);
INSERT INTO ProductSeller (Id, ProductId, SellerId) VALUES (8, 3, 3);

INSERT INTO Reviews (Id, Body, CustomerId, ProductId, Rating) VALUES (1, 'Instead of office chair, package contained bobcat. Would not buy again. ', 1, 1, 1);
INSERT INTO Reviews (Id, Body, CustomerId, ProductId, Rating) VALUES (2, 'Saved a ton of money on a new TV on black Friday and decided to use the extra cash to get the best cable available. At a whopping 3.3 feet in length, this cable is no joke. When all my friends come over to watch football, they always say ''WOW what kind of HDMI cable do you have?'' I proudly tell them about my Audioquest Diamond and its advanced features such as its Dark Gray/Black finish. It is a great conversation piece! Not to mention it fits into my DVD player and TV perfectly.', 2, 6, 5);
INSERT INTO Reviews (Id, Body, CustomerId, ProductId, Rating) VALUES (3, 'It makes a banging noise', 3, 9, 4);
