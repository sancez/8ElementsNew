use [8eShowTime]

select count(*)
 from [dbo].[ContentItem] where
ContentId in(
select ContentId from [dbo].[EventParticipant] 
where UserId=1194
and EventId=2
)

select ContentId from [dbo].[EventParticipant] 
where UserId=1194 
and EventId=2

select top 1* from [dbo].[ContentItem] 
select top 3* from [dbo].[EventParticipant] 
where id=1052
where use





select top 100 * from EventParticipant where
userid=''


Select  * from Content
where status is null

Select top 1 * from UserDetail where UserId=1197

select * from 
[dbo].[User] where id = 1197

--1197 => 1067


select top 1000 * from EventParticipant where Even
where userID='2'
Select top 1 * from UserDetail where UserId =1197
	

select * from EventParticipant a
join content b on a.ContentId = b.id
where a.EventCategoryId = 2 and a.EventId = 1