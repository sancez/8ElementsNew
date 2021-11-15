using EightElements.Showtime.CMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightElements.Showtime.CMS.Data
{
    public class Repositories
    {
        public static List<ContentEventParticipant> GetAllEventParticipant(int eventId, int eventCategoryId = 0, int startIndex = 0, int pageSize = 10, int ContentStatus = -1) //add ,int startIndex = 0, int pageSize = 10
        {
            using (var dc = new ShowtimeEntities())
            {
                var query = from ep in dc.EventParticipants
                            join ct in dc.Contents on ep.ContentId equals ct.Id
                            join pc in dc.UserDetails on ep.UserId equals pc.UserId
                            where ep.EventId == eventId
                            select new ContentEventParticipant { Content = ct, EventParticipant = ep, User = pc };
                if (eventCategoryId > 0) {
                    query = query.Where(ep => ep.EventParticipant.EventCategoryId == eventCategoryId).OrderByDescending(ep => ep.EventParticipant.Id).Skip(startIndex * pageSize).Take(pageSize);//add .OrderByDescending(pt => pt.Id).Skip(startIndex * pageSize).Take(pageSize);
                }
                if (ContentStatus > 0)
                {
                    query = query.Where(y => y.Content.Status == ContentStatus);
                }
                if (ContentStatus == 0)
                {
                    query = query.Where(cto => cto.Content.Status == null || cto.Content.Status == 0);
                }
                return query.OrderByDescending(ep => ep.EventParticipant.Id).Skip(startIndex * pageSize).Take(pageSize).ToList();//add Skip(startIndex * pageSize).Take(pageSize). 
            }
        }    
        //..................v1
        public static List<ShowcaseModel> GetAllShowcase(int startIndex = 0, int pageSize = 10, int ContentStatus = -191, string keySearch = "")// add int startIndex = 0, int pageSize = 10
        {
            using (var dc = new ShowtimeEntities())
            {
                if (keySearch == "")
                {
                    var epId = dc.EventParticipants.Select(x => x.ContentId).ToArray();
                    var query = from ep in dc.Contents
                                join pc in dc.UserDetails on ep.UserId equals pc.UserId
                                where !epId.Contains(ep.Id)
                                select new ShowcaseModel { Content = ep, User = pc };
                    if (ContentStatus > 0)
                    {
                        query = query.Where(x => x.Content.Status == ContentStatus);
                    }
                    if (ContentStatus == 0)
                    {
                        query = query.Where(y => y.Content.Status == null || y.Content.Status == 0);
                    }

                    query = query.OrderByDescending(ep => ep.Content.Id).Skip(startIndex * pageSize).Take(pageSize);//add .OrderByDescending(pt => pt.Id).Skip(startIndex * pageSize).Take(pageSize);

                    return query.ToList();
                }
                else 
                { 

                    var epId = dc.EventParticipants.Select(x => x.ContentId).ToArray();
                    var query = from ep in dc.Contents
                                join pc in dc.UserDetails on ep.UserId equals pc.UserId
                                where ep.Title.Contains(keySearch) || pc.RealName.Contains(keySearch)  /*!epId.Contains(ep.Id) */
                                select new ShowcaseModel { Content = ep, User = pc };
                    if (ContentStatus > 0)
                    {
                        query = query.Where(x => x.Content.Status == ContentStatus && x.Content.Title.Contains(keySearch));
                    }
                    if (ContentStatus == 0)
                    {
                        query = query.Where(y => y.Content.Status == null || y.Content.Status == 0 && y.Content.Title.Contains(keySearch));
                    }
                
                    query = query.OrderByDescending(ep => ep.Content.Id).Skip(startIndex * pageSize).Take(pageSize);//add .OrderByDescending(pt => pt.Id).Skip(startIndex * pageSize).Take(pageSize);
                        return query.ToList();
                }

               
            }
        }

        public static UserAdmin AddUserAdmins(UserAdmin objUserAdmin)// add int startIndex = 0, int pageSize = 10
        {
            using (var dc = new ShowtimeEntities())
            {
              
                UserAdmin varUserAdmin = dc.UserAdmins.SingleOrDefault(model => model.ID == objUserAdmin.ID) ?? new UserAdmin();
                varUserAdmin.RoleID = objUserAdmin.RoleID;
                varUserAdmin.FirstName = objUserAdmin.FirstName;
                varUserAdmin.UserName = objUserAdmin.UserName;
                varUserAdmin.Password = objUserAdmin.Password;
                varUserAdmin.FirstName = objUserAdmin.FirstName;
                varUserAdmin.Lastname = objUserAdmin.Lastname;
                varUserAdmin.CreatedDate = DateTime.Now;
                varUserAdmin.LastModifiedDate = DateTime.Now;
                varUserAdmin.IsActive = true;
                if (objUserAdmin.ID == 0)
                {
                    string Message = "Data SuccessFully Added";
                    dc.UserAdmins.Add(varUserAdmin);
                }

                dc.SaveChanges();
                return varUserAdmin;

            }
        }
        public static UserAdmin Authorize(UserAdmin userModel)
        {
            using (var dc = new ShowtimeEntities())
            {

                var userDetails = dc.UserAdmins.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                return userDetails;

            }
        }

        public static int GetCountShowcase()
        {
            using (var dc = new ShowtimeEntities())
            {
                var epId = dc.EventParticipants.Select(x => x.ContentId).ToArray();
                var query = from ep in dc.Contents
                            join pc in dc.UserDetails on ep.UserId equals pc.UserId
                            where !epId.Contains(ep.Id)
                            select new ShowcaseModel { Content = ep, User = pc };
                return query.Count();
            }
        }
        
        public static int GetCountEventParticipant()
        {
            using (var dc = new ShowtimeEntities())
            {
                var epId = dc.EventParticipants.Select(x => x.ContentId).ToArray();
                var query = from ep in dc.Contents
                            join pc in dc.UserDetails on ep.UserId equals pc.UserId
                            where !epId.Contains(ep.Id)
                            select new ContentEventParticipant { Content = ep, User = pc };
                return query.Count();
            }
        }

        //start pagination =>22/10/2021 2:00 PM
        public static int GetCountPageText(string lang)
        {
            using (var dc = new ShowtimeEntities())
            {
                if (!string.IsNullOrEmpty(lang))
                {
                    return dc.PageTexts.Where(pt => pt.LanguageCode == lang).Count();
                }
                return dc.PageTexts.Count();
            }
        }
        //end pagination


        public static List<ShowcaseModel> Test(int startIndex = 0, int pageSize = 10)// add int startIndex = 0, int pageSize = 10
        {
            using (var dc = new ShowtimeEntities())
            {
                var epId = dc.EventParticipants.Select(x => x.ContentId).ToArray();
                var query = from ep in dc.Contents
                            join pc in dc.UserDetails on ep.UserId equals pc.UserId
                            where !epId.Contains(ep.Id)
                            select new ShowcaseModel { Content = ep, User = pc };

                query = query.OrderByDescending(ep => ep.Content.Id).Skip(startIndex * pageSize).Take(pageSize);//add .OrderByDescending(pt => pt.Id).Skip(startIndex * pageSize).Take(pageSize);

                return query.ToList();
            }
        }

        public static List<ReportNewuser_Result> GetReport()
        {
            using (var dc = new ShowtimeEntities())
            {
                return dc.Database.SqlQuery<ReportNewuser_Result>("exec ReportNewUser").ToList();
            }
        }

        public static List<Event> GetAllEvent()
        {
            using (var dc = new ShowtimeEntities())
            {
                return dc.Events.ToList();
            }
        }

        public static List<EventDetail> GetEventDetailByKey(string key, int eventId)
        {
            using (var dc = new ShowtimeEntities())
            {
                return dc.EventDetails.Where(ed => ed.Key == key && ed.EventId == eventId).ToList();
            }
        }

        public static EventDetail AddEventDetail(EventDetail eventDetail)
        {
            using (var dc = new ShowtimeEntities())
            {
                var ext = dc.EventDetails.Where(ed => ed.Id == eventDetail.Id)
                    .FirstOrDefault();
                if (ext == null)
                {
                    ext = dc.EventDetails.Add(eventDetail);
                    dc.SaveChanges();
                    return ext;
                }

                ext.ContentPath = eventDetail.ContentPath;
                dc.SaveChanges();
                return ext;
            }
        }

        public static List<EventCategory> GetEventCategories()
        {
            using (var dc = new ShowtimeEntities())
            {
                return dc.EventCategories.ToList() ;
            }
        }

        public static void UpdateEventParticipant(int id,int categyId)
        {
            using (var dc = new ShowtimeEntities())
            {
                var ext = dc.EventParticipants.Where(ed => ed.Id == id)
                    .FirstOrDefault();

                ext.EventCategoryId = categyId;
                dc.SaveChanges();
            }
        }

        public static List<ContentEventParticipant> GetAllEventParticipantBySearchName(int eventId, string search,int eventCategoryId = 0)
        {
            using (var dc = new ShowtimeEntities())
            {
                var query = from ep in dc.EventParticipants
                    join ct in dc.Contents on ep.ContentId equals ct.Id
                    join pc in dc.UserDetails on ep.UserId equals pc.UserId
                    where ep.EventId == eventId && (pc.DisplayName.Contains(search) || pc.RealName.Contains(search) ||
                                                    pc.Id.ToString() == search || ct.Title.Contains(search))
                    select new ContentEventParticipant {Content = ct, EventParticipant = ep, User = pc};
                if (eventCategoryId > 0)
                {
                    query = query.Where(ep => ep.EventParticipant.EventCategoryId == eventCategoryId);
                }
                return query.ToList();
            }
        }

        public static List<ContentItem> GetContentParticipant(int contentId)
        {
            using (var dc = new ShowtimeEntities())
            {
                return dc.ContentItems.Where(ep => ep.ContentId == contentId).ToList();
            }
        }

        public static EventParticipant GetEventParticipantWhereContentId(int contentId)
        {
            using (var dc = new ShowtimeEntities())
            {
                var data = dc.EventParticipants.Where(ep => ep.ContentId == contentId).FirstOrDefault();
                return data;
            }
        }

        public static List<ContentItem> GetContentParticipantApproved(int contentId)
        {
            using (var dc = new ShowtimeEntities())
            {
                return dc.ContentItems
                    .Where(ep =>
                        ep.ContentId == contentId && ep.ContentTypeId < 3 && ep.Status == 1).ToList();
            }
        }

        public static List<PageText> GetAllPageText(string lang,int startIndex = 0, int pageSize = 10)
        {
            using (var dc = new ShowtimeEntities())
            {
                if (!string.IsNullOrEmpty(lang))
                {
                    return dc.PageTexts.Where(pt => pt.LanguageCode == lang).OrderByDescending(pt => pt.Id).Skip(startIndex * pageSize).Take(pageSize).ToList();
                }

                return dc.PageTexts.OrderByDescending(pt => pt.Id).Skip(startIndex * pageSize).Take(pageSize).ToList();
            }

        }

        public static PageText AddOrUpdatePageTexts(PageText value ,string logger)//Add And Update
        {
            using (var dc = new ShowtimeEntities())
            {
                var pageText = dc.PageTexts.Where(pt => pt.Id == value.Id).FirstOrDefault();
                if (pageText != null)
                {                    
                    pageText.Text = value.Text;
                    pageText.Key = value.Key;
                    pageText.LanguageCode = value.LanguageCode;
                    pageText.LastUpdateDate = DateTime.Now;
                    pageText.LastUpdateBy = (string)logger;
                    dc.SaveChanges();
                    return pageText;
                }

                pageText = dc.PageTexts.Add(value);
                dc.SaveChanges();
                return pageText;
            }
        }

        public static ActionTracking AddOrUpdateActionTracking(ActionTracking vl)//add and update => 10/21/2010 9:00 AM
        {
            using (var dc = new ShowtimeEntities())
            {
                var ActionTracking = dc.ActionTrackings.Where(pt => pt.Id == vl.Id).FirstOrDefault();
                if (ActionTracking != null)
                {
                 
                    ActionTracking.RefererUrl = vl.RefererUrl;
                    ActionTracking.RequestUrl = vl.RequestUrl;
                    ActionTracking.UserId = vl.UserId;
                    ActionTracking.Remarks = vl.Remarks;
                    ActionTracking.TrackingType = vl.TrackingType;
                    ActionTracking.TrafficSource = vl.TrafficSource;
                    ActionTracking.Campaign = vl.Campaign;
                    ActionTracking.PubId = vl.PubId;
                    ActionTracking.LandingPage = vl.LandingPage;
                    ActionTracking.GeolocationId = vl.GeolocationId;
                    ActionTracking.TrackingCookieId = vl.TrackingCookieId;
                    
                    dc.SaveChanges();
                    return ActionTracking;
                }
                vl.CreateDate = DateTime.Now;
                ActionTracking = dc.ActionTrackings.Add(vl);
                dc.SaveChanges();
                return ActionTracking;
            }
        }

        public static List<EventDetailModel> GetAllEventDetailByEventId(int eventId, int startIndex = 0, int pageSize = 10)
        {
            using (var dc = new ShowtimeEntities())
            {
                var data = dc.EventDetails.Where(ed => ed.EventId == eventId).OrderByDescending(pt => pt.Id).Skip(startIndex * pageSize).GroupBy(item => item.Key).Select(item =>
                    new
                    {
                        Id = item.FirstOrDefault().Id,
                        ContentPath = item.FirstOrDefault().ContentPath,
                        ContentSource = item.FirstOrDefault().ContentSource,
                        Key = item.FirstOrDefault().Key,
                        UpdateDate = item.FirstOrDefault().UpdateDate,
                        Count = item.Count()
                    }).ToList();
                return data.Select(x => new EventDetailModel()
                {
                    Id = x.Id,
                    Key = x.Key,
                    ContentPath = x.ContentPath,
                    ContentSource = x.ContentSource,
                    UpdateDate = x.UpdateDate,
                    Count = x.Count
                }).ToList();
            }
        }


        public static bool ApproveOrRejectContentDetail(int contentDetailId, int status)
        {
            using (var dc = new ShowtimeEntities())
            {
                var contentDetail = dc.ContentItems.Single(ep => ep.Id == contentDetailId);
                contentDetail.Status = status;
                dc.SaveChanges();
                return true;
            }
        }

        public static bool ApproveOrRejectContent(int contentId, int status)
        {
            using (var dc = new ShowtimeEntities())
            {
                var contentDetail = dc.Contents.Single(ep => ep.Id == contentId);
                contentDetail.Status = status;
                dc.SaveChanges();
                return true;
            }
        }

        public static List<Promotion> GetAllPromotions(int eventId)
        {
            using (var dc = new ShowtimeEntities())
            {
                return dc.Promotions.Where(p => p.EventId == eventId && p.Disabled == false).ToList();
            }
        }
        
        public static Promotion GetPromotionById(int id)
        {
            using (var dc = new ShowtimeEntities())
            {
                return dc.Promotions.FirstOrDefault(p => p.Id == id);
            }
        }        
        public static Promotion GetPromotionByTimeRange(DateTime startDate, DateTime endDate,int id)
        {
            using (var dc = new ShowtimeEntities())
            {
                return dc.Promotions.FirstOrDefault(p =>
                    ((p.StartDate >= startDate && p.StartDate <= endDate) ||
                     (p.EndDate >= startDate && p.EndDate <= endDate) ||
                     (p.StartDate <= startDate && p.EndDate >= endDate) ||
                     (p.StartDate >= startDate && p.EndDate <= endDate))
                && p.Disabled == false && p.Id != id
                    );
            }
        }        

        public static Promotion AddOrUpdatePromotion(Promotion promotion)
        {
            using (var dc = new ShowtimeEntities())
            {
                var promotionExt = dc.Promotions.SingleOrDefault(p => p.Id == promotion.Id);
                if (promotionExt == null) {
                    dc.Promotions.Add(promotion);
                    dc.SaveChanges();
                    return promotion;
                }
                PromotionHistory promotionHistory = new PromotionHistory()
                {
                    PromotionId = promotionExt.Id,
                    Rules = promotionExt.Rules,
                    StartDate = promotionExt.StartDate,
                    EndDate = promotionExt.EndDate,
                    Disabled = promotionExt.Disabled,
                    Pause = promotionExt.Pause,
                    CreateDate = DateTime.Now
                };
                promotionExt.Rules = promotion?.Rules ?? promotionExt.Rules;
                promotionExt.StartDate = promotion?.StartDate ?? promotionExt.StartDate;
                promotionExt.EndDate = promotion?.EndDate ?? promotionExt.EndDate;
                promotionExt.Disabled = promotion?.Disabled ?? promotionExt.Disabled;
                promotionExt.Pause = promotion?.Pause ?? promotionExt.Pause;
                promotionExt.UpdateDate = DateTime.Now;
                dc.PromotionHistories.Add(promotionHistory);
                dc.SaveChanges();
                return promotionExt;
            }
        }
        public static EventVote AddEventVote(EventVote Evote)
        {
            using (var dc = new ShowtimeEntities())
            {
                var EventVoteExt = dc.EventVotes.SingleOrDefault(p => p.Id == Evote.Id);
                if (EventVoteExt == null)
                {
                    dc.EventVotes.Add(Evote);
                    dc.SaveChanges();
                    return Evote;
                }
                return EventVoteExt;
               
            }
        }

        public static ActionTracking AddActionTracking(ActionTracking BB)
        {
            using (var dc = new ShowtimeEntities())
            {
                var ret = dc.ActionTrackings.SingleOrDefault(p => p.Id == BB.Id);
                if (ret == null)
                {
                    dc.ActionTrackings.Add(BB);
                    dc.SaveChanges();
                    return BB;
                }
                return ret;

            }
        }

        public static List<ContentAgeRating> GetContentAgeRatings(string keyword="")
        {
            using (var dc = new ShowtimeEntities())
            {
                var data = (from objData in dc.ContentAgeRatings orderby objData.Name descending
                            where objData.Name.Contains(keyword) 
                            select objData
                ).ToList();
                return data;
            }
        }

        public static ContentAgeRating AddContentAgeRating(string name)
        {
            using (var dc = new ShowtimeEntities()) 
            {

                var data = new ContentAgeRating()
                {
                    Name = name
                };
                dc.ContentAgeRatings.Add(data);
                dc.SaveChanges();
                return data;
            }
        }
        public static ContentAgeRating UpdateContentAgeRating(ContentAgeRating objData)
        {
            using (var dc = new ShowtimeEntities()) 
            {
                ContentAgeRating data = dc.ContentAgeRatings.SingleOrDefault(x => x.Id == objData.Id) ?? new ContentAgeRating();
                data.Id = objData.Id;
                data.Name = objData.Name;
                dc.SaveChanges();
                return data;
            }
        }
        public static ContentAgeRating DeleteContentAgeRating(int Id)
        {
            using (var dc = new ShowtimeEntities()) {
                ContentAgeRating objData = dc.ContentAgeRatings.Single(x => x.Id == Id);
                dc.ContentAgeRatings.Remove(objData);
                dc.SaveChanges();
                return objData;
            }
        }
        public static List<ContentCategory> GetContentCategory(string keyword="")
        {
            using (var dc = new ShowtimeEntities())
            {
                var data = (from objData in dc.ContentCategories
                            orderby objData.Name descending
                            where objData.Name.Contains(keyword)
                            select objData
                            ).ToList();
                return data;
            }
        }

        public static ContentCategory AddContentCategory(CategoryPlatformDTO objData) 
        {
            using (var dc = new ShowtimeEntities())
            {
                var data = new ContentCategory()
                {
                    Name = objData.Name
                };
                dc.ContentCategories.Add(data);

                dc.SaveChanges();

                //loop data yg ada di objData.platforms
                var pl = objData.Platforms;

                foreach(var p in pl)
                {
                    var map = new ContentCategoryPlatformMap()
                    {
                        CategoryId = data.Id,
                        PlatformId = p.Id
                    };
                    dc.ContentCategoryPlatformMaps.Add(map);
                }

                dc.SaveChanges();                
                return data;
            }
        
        }

        public static ContentCategory UpdateContentCategory(CategoryPlatformDTO objData)
        {
            using (var dc = new ShowtimeEntities())
            {
                ContentCategory data = dc.ContentCategories.SingleOrDefault(x=>x.Id == objData.Id) ?? new ContentCategory();
                data.Id = objData.Id;
                data.Name = objData.Name;
                dc.SaveChanges();

                //Delete Data ContentCategoryPlatformMap
                var dataPlatFormMap = dc.ContentCategoryPlatformMaps.Where(a => a.CategoryId == data.Id).ToList();
                dc.ContentCategoryPlatformMaps.RemoveRange(dataPlatFormMap);
                dc.SaveChanges();

                //Insert Data ContentCategoryPlatformMap
                var pl = objData.Platforms ?? new List<ContentPlatform>();
                foreach (var p in pl)
                {               
                    var map = new ContentCategoryPlatformMap()
                    {
                       CategoryId = data.Id,
                       PlatformId = p.Id
                    };
                    dc.ContentCategoryPlatformMaps.Add(map);
                }
                dc.SaveChanges();
                return data;
            }
        }

        public static ContentCategory DeleteContentCategory(ContentCategory objData)
        {
            using (var dc = new ShowtimeEntities())
            {
                ContentCategory data = dc.ContentCategories.SingleOrDefault(x => x.Id == objData.Id) ?? new ContentCategory();
                var getData = (from objTable in dc.ContentCategories
                               where objTable.Id == objData.Id
                               select objTable).ToList();
                var cekData = getData.Count();
                if (cekData > 0)
                {
                    dc.ContentCategories.Remove(data);
                    dc.SaveChanges();
                }
                                
                var dataMap = dc.ContentCategoryPlatformMaps.Where(y => y.CategoryId == objData.Id).ToList();
                if (dataMap.Count < 1)
                    return null;
                dc.ContentCategoryPlatformMaps.RemoveRange(dataMap);
                dc.SaveChanges();
                return objData;
            }
        }
        public static List<ContentClassification> GetContentClassification(string keyword = "")
        {
            using (var dc = new ShowtimeEntities())
            {
                var data = (from objData in dc.ContentClassifications
                            orderby objData.Name descending
                            where objData.Name.Contains(keyword)
                            select objData
                            ).ToList();
                return data;
            }
        }

        public static ContentClassification AddContentClassification(string name)
        {
            using (var dc = new ShowtimeEntities()) 
            {
                var data = new ContentClassification()
                {
                    Name = name
                };
                dc.ContentClassifications.Add(data);
                dc.SaveChanges();
                return data;
            }
        }
        public static ContentClassification UpdateContentClassification(ContentClassification objData)
        {
            using (var dc = new ShowtimeEntities()) 
            {
                ContentClassification data = dc.ContentClassifications.SingleOrDefault(x => x.Id == objData.Id) ?? new ContentClassification();
                data.Id = objData.Id;
                data.Name = objData.Name;
                dc.SaveChanges();
                return data;
            }
        }

        public static ContentClassification DeleteContentClassification(int Id)
        {
            using (var dc = new ShowtimeEntities())
            {
                ContentClassification data = dc.ContentClassifications.Single(x => x.Id == Id);
                dc.ContentClassifications.Remove(data);
                dc.SaveChanges();
                return data;
            }
        }
        public static List<ContentPlatform> GetContentPlatform(string keyword = "")
        {
            using (var dc = new ShowtimeEntities()) {
                var data = (from objData in dc.ContentPlatforms
                            orderby objData.Name descending
                            where objData.Name.Contains(keyword)
                            select objData).ToList();
                return data;
            }
        }

        public static ContentPlatform AddContentPlatform(string name)
        {
            using (var dc = new ShowtimeEntities()) 
            {
                var data = new ContentPlatform()
                {
                    Name = name
                };
                dc.ContentPlatforms.Add(data);
                dc.SaveChanges();
                return data;
            }
        }

        public static ContentPlatform UpdateContentPlatform(ContentPlatform objData)
        {
            using (var dc = new ShowtimeEntities())
            {
                ContentPlatform data = dc.ContentPlatforms.SingleOrDefault(x => x.Id == objData.Id) ?? new ContentPlatform();
                data.Id = objData.Id;
                data.Name = objData.Name;
                dc.SaveChanges();
                return data;
            }
        }
        public static ContentPlatform DeleteContentPlatform(int Id)
        {
            using (var dc = new ShowtimeEntities())
            {
                ContentPlatform data = dc.ContentPlatforms.Single(x => x.Id == Id);
                dc.ContentPlatforms.Remove(data);
                dc.SaveChanges();
                return data;
            }
        }
        public static List<ContentTag> GetContentTag(string keyword="")
        {
            using (var dc = new ShowtimeEntities()) {
                var data = (from objData in dc.ContentTags
                            orderby objData.Tag
                            where objData.Tag.Contains(keyword)
                            select objData).ToList();
                return data;
            }
        }

        public static List<Models.CategoryPlatformDTO> GetContentCategoryPlatform(string keyword = "")
        {
            using (var dc = new ShowtimeEntities())
            {
                var data = (from objData in dc.ContentCategories
                            orderby objData.Name descending
                            where objData.Name.Contains(keyword)
                            select new CategoryPlatformDTO {Id = objData.Id, Name = objData.Name }
                            ).ToList();

                var b = data.Count();                
                
                for (var a = 0; a < b; a++)
                {
                    var catId = data[a].Id;

                    var data2 = (from cMaps in dc.ContentCategoryPlatformMaps
                             join cp in dc.ContentPlatforms
                             on cMaps.PlatformId equals cp.Id
                             where cMaps.CategoryId == catId
                             select cp
                             ).ToList();

                    data[a].Platforms = data2;
                }

                return data;

            }
        }

        public static int CekContentItems(int userId = 0, int eventId = 0)
        {
            using (var dc = new ShowtimeEntities())
            {
                var CekContentId = (from objContent in dc.EventParticipants
                                    where objContent.UserId == userId && objContent.EventId == eventId 
                                    select objContent.ContentId).ToList();

                var cekContentItem = (from objContentItem in dc.ContentItems
                                      where CekContentId.Contains(objContentItem.ContentId)
                                      select objContentItem).Count();                

                return cekContentItem;
            }
        }

        /*public static List<ContentEventParticipant> ContentStatus(int eventId, int eventCategoryId = 0, int startIndex = 0, int pageSize = 10, int ContentStatus = 0)
        {
            using (var dc = new ShowtimeEntities())
            {
                var query = from ep in dc.EventParticipants
                            join ct in dc.Contents on ep.ContentId equals ct.Id
                            join pc in dc.UserDetails on ep.UserId equals pc.UserId
                            where ep.EventId == eventId 
                            select new ContentEventParticipant { Content = ct, EventParticipant = ep, User = pc };
                if (eventCategoryId > 0)
                {
                    query = query.Where(x => x.EventParticipant.EventCategoryId == eventCategoryId);
                }
                if (ContentStatus > 0)
                {
                    query = query.Where(y => y.Content.Status == ContentStatus);
                }
                if (ContentStatus == 0)
                {
                    query = query.Where(cto => cto.Content.Status == null);
                }
                return query.OrderByDescending(xy => xy.EventParticipant.Id).Skip(startIndex * pageSize).Take(pageSize).ToList();//add Skip(startIndex * pageSize).Take(pageSize). //add .OrderByDescending(pt => pt.Id).Skip(startIndex * pageSize).Take(pageSize);
            }
        }*/

        public static UpdateContentDTO GetContentDetail(int contentId)
        {
            using (var dc = new ShowtimeEntities())
            {
                var content = dc.Contents.Where(a => a.Id == contentId).FirstOrDefault();
                var items = dc.ContentItems.Where(a => a.ContentId == contentId).ToList();
                var tags = dc.ContentTags.Where(a => a.ContentId == contentId).Select(a => a.Tag).ToList();
                var user = dc.UserDetails.Where(a => a.UserId == content.UserId).FirstOrDefault();

                var data = new UpdateContentDTO()
                {
                    Id = contentId,
                    Title = content.Title,
                    Username = user.DisplayName,
                    UserId = content.UserId,
                    CategoryId = content.ContentCategoryId,
                    ClassificationId = content.ContentClassificationId,
                    AgeRatingId = content.ContentAgeRatingId,
                    IsCP = content.IsCP,
                    Tags = tags,
                    ContentItems = items,
                    LastCreatedDate = content.LastUpdatedDate,
                    LastCreatedBy = content.LastUpdatedBy
                };

                return data;
            }
        }

        public static UpdateContentDTO UpdateContentDetail(UpdateContentDTO dto, int adminId)
        {
            using (var dc = new ShowtimeEntities())
            {
                var content = dc.Contents.Where(a => a.Id == dto.Id).FirstOrDefault();
                var items = dc.ContentItems.Where(a => a.ContentId == dto.Id).ToList();
                var tags = dc.ContentTags.Where(a => a.ContentId == dto.Id).ToList();

                content.ContentCategoryId = dto.CategoryId;
                content.ContentClassificationId = dto.ClassificationId;
                content.ContentAgeRatingId = dto.AgeRatingId;
                content.LastUpdatedBy = adminId;
                content.LastUpdatedDate = DateTime.Now;

                foreach(var i in dto.ContentItems)
                {
                    var old = items.Where(a => a.Id == i.Id).FirstOrDefault();
                    old.Status = i.Status;
                }

                dc.ContentTags.RemoveRange(tags);

                var newTags = new List<ContentTag>();
                foreach(var t in dto.Tags)
                {
                    newTags.Add(new ContentTag() { ContentId = dto.Id, Tag = t });
                }
                dc.ContentTags.AddRange(newTags);

                dc.SaveChanges();

                return dto;
            }
        }
    }
}