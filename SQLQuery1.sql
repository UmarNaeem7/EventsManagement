create database events
go
use events
go

drop table [user]

create table [user]
(
	[email] varchar(100) primary key,
	[password] varchar(100) not null,
	[fullname] varchar(100) not null unique
)

go



create table [event]
(
	[title] varchar(50),
	[startdate] varchar(30),
	[startTime] varchar(30),
	[duration] varchar(30),
	[description] varchar(200),
	[location] varchar(100),
	[isPublic] int,
	[author] varchar(100) foreign key references [user]([fullname]),
	[upcoming] int,
	primary key([title], [startdate]),
	check([isPublic] = 0 OR [isPublic] = 1)
)


go
create table [comment]
(
	[eventname] varchar(50),
	[eventdate] varchar(30),
	[author] varchar(100) foreign key references [user]([fullname]),
	[text] varchar(200),
	[commentdate] date not null,
	foreign key([eventname], [eventdate]) references [event]([title], [startdate]),
	primary key([eventname], [eventdate])
)

go




insert into [event] values('New Year 2k19', '2019-01-01', '12:00 AM', '3 hrs', 'We celebrate New Year with great zeal and zest
to reinvigorate ourselves for the next year', 'Farm House', 1, null, 0)
insert into [user] values('admin@admin.com', 'admin123', 'Admin')
---view data in tables
select * from [user]
select * from [event]
select * from [comment]


delete from [user] where [fullname] like 's%'

----stored procedures
alter procedure UserSignupProc
@email varchar(100), @password varchar(100), @fullname varchar(100), @output int output
as
begin
	if exists (select * From [User] where [fullname]=@fullname or [email] = @email)
	Begin
		set @output=0 --signup unsuccessful
	End
	else
	begin
		insert into [user]([email], [password], [fullname]) values(@email, @password, @fullname)
		set @output = 1 --signup successful
		
	end
end

alter procedure UserLoginProc
@email varchar(100), @password varchar(100), @output int output
as
begin
	if exists (select * from [user] where [email] = @email AND [password] = @password)
	begin
		set @output = 1 --login successful
	end
	else
	begin
		set @output = 0 --login unsuccessful
	end
end

alter procedure getfullname
@email varchar(100), @password varchar(100), @fullname varchar(100) output, @output int output
as
begin
	if exists(select fullname from [user] where [email] = @email AND [password] = @password)
	begin
		set @fullname = (select [fullname] from [user] where [email] = @email AND [password] = @password)
		set @output = 1
	end
	else
	begin
		set @output = 0
		set @fullname = ''
	end
end

alter procedure AddEventToDB
@title varchar(50), @startdate varchar(30), @startTime varchar(30), @duration varchar(30),
@description varchar(200), @location varchar(50), @isPublic int, @author varchar(100), @upcoming int,
@output int output
as
begin
	if exists (select * from [event] where [title] = @title AND [startdate] = @startdate)
	begin
		set @output = 0 --event already exists
	end
	else
	begin
		set @output = 1
		insert into [event] values(@title, @startdate, @startTime, @duration, @description, @location,
		@isPublic, @author, @upcoming)
	end
end

alter procedure EditEvent
@title varchar(50), @startdate varchar(30), 
@startTime varchar(30), @duration varchar(30), @description varchar(200), @location varchar(100),
@isPublic int, @author varchar(100), @upcoming int,
@output int output
as
begin
	if exists (select * from [event] where [title] = @title AND [startdate] = @startdate)
	begin
		update [event] set [startTime] = @startTime, [duration] = @duration, [description] = @description, [location] = @location, [isPublic] = @isPublic, 
		[upcoming] = @upcoming where [title] = @title AND [startdate] = @startdate AND [author] = @author
		set @output = 1 
	end
	else
	begin
		set @output = 0 --event not found
	end
end

alter procedure DeleteUserEvent
@title varchar(50), @startdate varchar(30), @author varchar(100), @output int output
as
begin
	if exists (select * from [event] where [title] = @title AND [startdate] = @startdate AND [author] = @author)
	begin
		delete from [event] where [title] = @title AND [startdate] = @startdate AND [author] = @author
		set @output = 1
	end
	else
	begin
		set @output = 0
	end
end

--admin sps
create procedure EditAdminEvent
@title varchar(50), @startdate varchar(30), 
@startTime varchar(30), @duration varchar(30), @description varchar(200), @location varchar(100),
@isPublic int, @upcoming int,
@output int output
as
begin
	if exists (select * from [event] where [title] = @title AND [startdate] = @startdate)
	begin
		update [event] set [startTime] = @startTime, [duration] = @duration, [description] = @description, [location] = @location, [isPublic] = @isPublic, 
		[upcoming] = @upcoming where [title] = @title AND [startdate] = @startdate
		set @output = 1 
	end
	else
	begin
		set @output = 0 --event not found
	end
end

create procedure DeleteAdminEvent
@title varchar(50), @startdate varchar(30), @output int output
as
begin
	if exists (select * from [event] where [title] = @title AND [startdate] = @startdate)
	begin
		delete from [event] where [title] = @title AND [startdate] = @startdate
		set @output = 1
	end
	else
	begin
		set @output = 0
	end
end