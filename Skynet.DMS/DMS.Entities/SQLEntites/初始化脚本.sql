
create table DMS_User(
Id bigint identity(1,1) primary key not null,
UserName nvarchar(50) not null default '',
DisplayName nvarchar(200) not null default '',
[PassWord] varchar(200) not null default '',
Email varchar(200) not null default '',
Phone varchar(50) not null default '',
[Status] bit not null default 0,
IsDelete bit not null default 0,
[ModifyDate] datetime default getdate(),
CreateDate datetime default getdate()
)

create table DMS_Role(
Id bigint identity(1,1) primary key not null,
RoleName nvarchar(200) not null default '',
MenuIds varchar(1000) not null default '',
IsDelete bit not null default 0,
[ModifyDate] datetime default getdate(),
CreateDate datetime default getdate()
)

create table DMS_Module(
Id bigint identity(1,1) primary key not null,
ModuleName nvarchar(200) not null default '',
Url varchar(200)  default '',
Icon varchar(200)  null default '',
ParentId int default 0,
IsUse bit default 0,
[Level] int default 0,
OrderBy int default 0,
IsDelete bit not null default 0,
[ModifyDate] datetime default getdate(),
CreateDate datetime default getdate()
)

create table DMS_UserRole(
Id bigint identity(1,1) primary key not null,
UserId int default 0 not null,
RoleIds varchar(2000) not null default '',
IsDelete bit not null default 0,
[ModifyDate] datetime default getdate(),
CreateDate datetime default getdate()
)

insert into DMS_User(UserName,DisplayName,[PassWord],Email,Phone) values('admin','超级管理员','admin','skynetfyah@outlook.com','15221848167')
insert into DMS_Module(ModuleName,Icon,[Level]) values('基础管理','lyphicon glyphicon-home',1)

create table DMS_Hobbites(
Id bigint identity(1,1) primary key not null,
TypeId int default 0 null,
HbName nvarchar(500) default '' not null,
HbDesc nvarchar(2000) not null default '',
ImageUrl varchar(200) default '',
IsDelete bit not null default 0,
[ModifyDate] datetime default getdate(),
CreateDate datetime default getdate()
)

create table DMS_HobbityType(
Id bigint identity(1,1) primary key not null,
TName nvarchar(500) default '' not null,
HbDesc nvarchar(2000) not null default '',
IsDelete bit not null default 0,
[ModifyDate] datetime default getdate(),
CreateDate datetime default getdate()
)


create table DMS_ChatUser
(
  Id bigint identity(1,1) primary key not null,
  UserName nvarchar(200) default '',
  ConnectionIds varchar(1000) default ''
)

create table DMS_Connection
(
  Id bigint identity(1,1) primary key not null,
  [Uid] bigint default 0,
 ConnectionId varchar(50) default '',
 UserAgent varchar(50) default '',
 Connected bit default 0
)

create table DMS_UserMessage
(
Id bigint identity(1,1) primary key not null,
[Uid] bigint default 0,
[Type] nvarchar(200) default '',
ToUid bigint default 0,
Content nvarchar(2000) not null default '',
[Status] int default '',
IsDelete bit not null default 0,
[ModifyDate] datetime default getdate(),
CreateDate datetime default getdate()
)