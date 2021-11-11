use [8eShowTime]

select top 1 * from 

select top 1000 * from [dbo].[ContentAgeRating] with (nolock) --Done with Crud Done all
where id = 6

select top 4 * from [dbo].[ContentCategoryPlatformMap] with(nolock)--Skip

select top 4000 * from [dbo].[ContentClassification] with(nolock) -- Crud  Done all
where id ='5'

select top 1900 * from [dbo].[ContentPlatform] with(nolock)
where id=17


select top 1000 * from [dbo].[ContentPlatform] with(nolock) -- Done => Done all 
where id=18

select top 4 * from  [dbo].[ContentTag] with(nolock) --Dont write
select top 4 * from  [dbo].[Content] with(nolock) --Skip

//////////
select * from [dbo].[ContentCategory] with(nolock) --Done => //....
select * from [dbo].[ContentCategoryPlatformMap] with(nolock) 
//
select * from [dbo].[ContentPlatform] with(nolock) -- Done

Select * from [dbo].[ContentCategory]
Select top 1 * from [dbo].[contentCategoryPlatformMap]

centralpart /taman aggrek

select a.CategoryId,b.* from ContentCategoryPlatformMap a
join ContentPlatform b on a.PlatformId = b.Id
where a.CategoryId = 4
Select top 1000 * from [dbo].[Event]

Select top 100 * from [dbo].[ContentAgeRating] with Noloc