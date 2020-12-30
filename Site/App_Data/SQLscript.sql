drop table Answers
drop table Questions
drop table Tests

update Questions set Question='how many + in C#?' where TestId=4 and Id=1
select * from Answers

insert into Tests values (4, 'C# test')

select Question, RightAnswerId, a.Id, Answer
from Questions q
inner join Answers a
on q.Id=a.QuestionId
where (q.TestId=1 and a.TestId=1)

create table dbo.Tests
(
	Id int not null primary key,
	TestName Nvarchar(100) not null
)

create table dbo.Questions
(
	TestId int not null,
	Id int not null,
	Question Nvarchar(100) not null,
	RightAnswerId int not null
)

create table dbo.Answers
(
	TestId int not null,
	QuestionId int not null,
	Id int not null,
	Answer Nvarchar(100) not null
)

insert into Tests values (1, 'Russian-English translation test')

insert into Questions values (1, 1, N'кот', 2)
insert into Answers values (1, 1, 1, 'dog')
insert into Answers values (1, 1, 2, 'cat')
insert into Answers values (1, 1, 3, 'code')

insert into Questions values (1, 2, N'дождь', 1)
insert into Answers values (1, 2, 1, 'rain')
insert into Answers values (1, 2, 2, 'fog')
insert into Answers values (1, 2, 3, 'snow')

insert into Questions values (1, 3, N'вид', 3)
insert into Answers values (1, 3, 1, 'wild')
insert into Answers values (1, 3, 2, 'vector')
insert into Answers values (1, 3, 3, 'view')





insert into Tests values (2, 'English-Russian translation test')

insert into Questions values (2, 1, 'territory', 2)
insert into Answers values (2, 1, 1, N'кот')
insert into Answers values (2, 1, 2, N'территория')
insert into Answers values (2, 1, 3, N'ложка')

insert into Questions values (2, 2, 'season', 1)
insert into Answers values (2, 2, 1, N'сезон')
insert into Answers values (2, 2, 2, N'дождь')
insert into Answers values (2, 2, 3, N'массон')

insert into Questions values (2, 3, 'bring', 3)
insert into Answers values (2, 3, 1, N'уносить')
insert into Answers values (2, 3, 2, N'привозить')
insert into Answers values (2, 3, 3, N'приносить')

insert into Questions values (2, 4, 'view', 1)
insert into Answers values (2, 4, 1, N'вид')
insert into Answers values (2, 4, 2, N'план')
insert into Answers values (2, 4, 3, N'закат')

insert into Questions values (2, 5, 'live', 2)
insert into Answers values (2, 5, 1, N'ливень')
insert into Answers values (2, 5, 2, N'жить')
insert into Answers values (2, 5, 3, N'ленивец')





insert into Tests values (3, 'Math test')

insert into Questions values (3, 1, '2+2', 1)
insert into Answers values (3, 1, 1, '4')
insert into Answers values (3, 1, 2, '2')
insert into Answers values (3, 1, 3, '5')

insert into Questions values (3, 2, '2*2', 3)
insert into Answers values (3, 2, 1, '1')
insert into Answers values (3, 2, 2, '6')
insert into Answers values (3, 2, 3, '4')

insert into Questions values (3, 3, '2/2', 3)
insert into Answers values (3, 3, 1, '23')
insert into Answers values (3, 3, 2, '5')
insert into Answers values (3, 3, 3, '1')

insert into Questions values (3, 4, '2%2', 1)
insert into Answers values (3, 4, 1, '0')
insert into Answers values (3, 4, 2, '5')
insert into Answers values (3, 4, 3, '-2')

insert into Questions values (3, 5, '2-2', 2)
insert into Answers values (3, 5, 1, '1')
insert into Answers values (3, 5, 2, '0')
insert into Answers values (3, 5, 3, '-2')





insert into Questions values (4, 1, 'how many '+' in C#?', 3)
insert into Answers values (4, 1, 1, 'null')
insert into Answers values (4, 1, 2, '0')
insert into Answers values (4, 1, 3, '4')
insert into Answers values (4, 1, 4, 'C++ * 2')

insert into Questions values (4, 2, 'base class', 1)
insert into Answers values (4, 2, 1, 'object')
insert into Answers values (4, 2, 2, 'System')
insert into Answers values (4, 2, 3, 'Microsoft')
insert into Answers values (4, 2, 4, 'CS')





insert into Tests values (5, 'Mood test')

insert into Questions values (5, 1, 'chouse your mood', 1)
insert into Answers values (5, 1, 1, 'good')
insert into Answers values (5, 1, 2, 'bad')