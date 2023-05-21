# MyPetProject

1. Clone a repository
2. To add database, in VS open NuGet console and enter "Update-Database"

Also, for now, for proper work of the application, you have to insert some data to database manually. 
Here is the code you will need to execute:
1. Order statuses:
```
insert into [ShopDb].[dbo].[OrderStatus] (Id, Name)
values ('c1427939-beb3-4d2f-bfa8-ba00a2f1af01', 'Pending'), ('245b7f2b-b61b-4729-9eb9-1bfa437f7db7', 'Accepted'), 
('95b13af5-8d95-473a-b8ac-03c3022d68c0', 'Declined'), ('723f18a4-f6a0-4f19-8acf-6244a8f6e7c9', 'Shipped'), 
('b440f2e2-5220-469c-be38-a65fba5c6244', 'Arrived'), ('ef464349-c491-4467-b88a-cc08664a7596', 'Received');
```
2. User roles:
```
insert into [ShopDb].[dbo].[Role] (Id, Name)
values ('70E91DA3-4A2E-469C-B251-02B73874DF6B', 'admin'), ('9BABDB23-81BD-41F4-B0D2-B9518CE03B81', 'user');
```
