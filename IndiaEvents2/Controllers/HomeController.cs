using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IndiaEvents2.Models;
using Newtonsoft.Json;
using System.IO;
using messageSystem;

namespace IndiaEvents2.Controllers
{
    
    public class clslookups
    {
        public string lookups { get; set; }
        public Guid lookupsID { get; set; }
    }

    public class subevents
    {
        public string eventName { get; set; }
        public string personName { get; set; }
        public string personMobile { get; set; }
    }

    public class response
    {
        public string message { get; set; }
        public string htmlContent { get; set; }
        public int eventID { get; set; }
    }

    public class HomeController : Controller
    {
        //private readonly MvcControllerFactoryEntities _context;
        public ActionResult Index()
        {
            //response message = new response();
            //message.message = "Logout successfully..!";
            //message.htmlContent = "<button type='button' class='btn purple-gradient btn-sm waves-effect waves-light' data-toggle='modal' data-target='#LoginModal' id='login'>Login</button>";
            //return Json(message, JsonRequestBehavior.AllowGet);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult Login(user user)
        {
            IndiaEvents2Entities ie = new IndiaEvents2Entities();
            user userDetails = (from u in ie.users
                            where u.userName == user.userName && u.password == user.password
                            select u).SingleOrDefault();
            if (userDetails != null)
            {
                FormsAuthentication.SetAuthCookie(user.userName, true);
            }
            response message = new response();
            message.message = "Logged In successfully..!";
            message.htmlContent = "<button id='logout' class='btn btn-info btn-rounded btn-sm waves-effect waves-light'>Log Out<i class='fas fa-sign-out-alt'></i></button>";
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Logout()
        {
            FormsAuthentication.SignOut();
            response message = new response();
            message.message = "Logout successfully..!";
            message.htmlContent = "<button type='button' class='btn purple-gradient btn-sm waves-effect waves-light' data-toggle='modal' data-target='#LoginModal' id='login'>Login</button>";
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult StoreFiles(string eventID)
        {
            if (Request.Files.Count > 0)
            {
                try
                { 
                    HttpFileCollectionBase files = Request.Files;
                    //string eventID = Request.Params["EventID"].ToString();
                    Event eventsDetails = new Event();
                    IndiaEvents2Entities e = new IndiaEvents2Entities();
                    int eID = Int32.Parse(eventID);
                    eventsDetails = (from ev in e.Events where (ev.EventID == eID) select ev).FirstOrDefault();

                    HttpPostedFileBase file = files[0];
                    eventsDetails.Poster = new byte[file.ContentLength]; // file1 to store image in binary formate  
                    file.InputStream.Read(eventsDetails.Poster, 0, file.ContentLength);
                    e.SaveChanges();


                    //for (int i = 0; i < files.Count; i++)
                    //{
                    //    HttpPostedFileBase file = files[i];
                    //    string fname;
                    //    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    //    {
                    //        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                    //        fname = testfiles[testfiles.Length - 1];
                    //    }
                    //    else
                    //    {
                    //        fname = file.FileName;
                    //    }
                    //    //fname = Path.Combine(Server.MapPath("~/Content/Img"), fname);
                    //    //file.SaveAs(fname);

                        
                    //}
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public JsonResult PostEvent(Evento eventDetails)
        {
            //HttpFileCollectionBase files = eventDetails.Poster;
            //HttpPostedFileBase file = files[0];
            //string fname = file.FileName;
            //fname = Path.Combine(Server.MapPath("~/Content/img"), fname);
            //file.SaveAs(fname);

            List<subevents> s = new List<subevents>();
            IndiaEvents2Entities e = new IndiaEvents2Entities();
            Event events = new Event();
            events.Events = JsonConvert.SerializeObject(eventDetails.Events);
            events.EventName = eventDetails.EventName;
            events.EventFee = eventDetails.EventFee;
            events.EventType = eventDetails.EventType;
            events.EventFromDate = eventDetails.EventFromDate;
            events.EventToDate = eventDetails.EventToDate;
            events.Website = eventDetails.Website;
            events.CollegeName = eventDetails.CollegeName;
            events.Department = eventDetails.Department;
            events.City = eventDetails.City;
            events.State = eventDetails.State;
            events.Address = eventDetails.Address;
            e.Events.Add(events);
            e.SaveChanges();

            int eventID = (from eve in e.Events
                              where (eve.EventName == eventDetails.EventName)
                              select eve.EventID).FirstOrDefault();

            response message = new response();
            //List<string> msg = new List<string>() { "Event Create Successfully..!", eventID.ToString() };
            message.message = "Event Create Successfully..!";
            message.eventID = eventID;
            message.htmlContent = "<button id='logout' class='btn btn-info btn-rounded btn-sm waves-effect waves-light'>Log Out<i class='fas fa-sign-out-alt'></i></button>";

            
            
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        

        public JsonResult getEventsTypes()
        {
            IndiaEvents2Entities e = new IndiaEvents2Entities();
            Guid lookupType = (from LookupTypes in e.LookupTypes
                               where LookupTypes.lookupType1 == "Eventtypes" 
                               select LookupTypes.lookupID).SingleOrDefault();
            List<Lookup> l = (from ls in e.Lookups where ls.lookupID == lookupType select ls).ToList();
            List<clslookups> lk = new List<clslookups>();
            if(l.Count > 0) {
                foreach(var s in l){
                    clslookups values = new clslookups();
                    values.lookups = s.lookups;
                    values.lookupsID = s.lookupID;
                    lk.Add(values);
                }
            }
            return Json(lk, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateUser(user userDetails)
        {
            IndiaEvents2Entities ie = new IndiaEvents2Entities();
            response message = new response();
            //string message = "";
            try
            {
                int mobileNumber = ie.users.Where(x => x.mobile == userDetails.mobile).Count();
                if (mobileNumber == 0)
                {
                    user u = new user();
                    u.userName = userDetails.userName;
                    u.email = userDetails.email;
                    u.mobile = userDetails.mobile;
                    u.password = userDetails.password;
                    ie.users.Add(u);
                    ie.SaveChanges();
                    message.htmlContent = "<button id='logout' class='btn btn-info btn-rounded btn-sm waves-effect waves-light'>Log Out<i class='fas fa-sign-out-alt'></i></button>";
                    message.message = "Saved Successfully..!";
                }
                else
                {
                    message.htmlContent = "<button type='button' class='btn purple-gradient btn-sm waves-effect waves-light' data-toggle='modal' data-target='#LoginModal' id='login'>Login</button>";
                    message.message = "Mobile number was already in use please try with forgot password";
                }
                
            }
            catch(Exception ex)
            {
                message.htmlContent = "";
                message.message = ex.Message.ToString();
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public JsonResult showEvents()
        {
            // public string EventName { get; set; }
            //public string EventType { get; set; }
            //public Nullable<int> EventFee { get; set; }
            //public IEnumerable<subevents> Events { get; set; }
            //public Nullable<System.DateTime> EventFromDate { get; set; }
            //public Nullable<System.DateTime> EventToDate { get; set; }
            //public string CollegeName { get; set; }
            //public string Department { get; set; }
            //public string City { get; set; }
            //public string State { get; set; }
            //public string Address { get; set; }
            //public dynamic Poster { get; set; }
            //public string Website { get; set; }
            //public int EventID { get; set; }

            IndiaEvents2Entities e = new IndiaEvents2Entities();
            List<Event> en = new List<Event>();
            List<Evento> eve = new List<Evento>();

            en = (from q in e.Events select q).OrderBy(x => x.EventFromDate).ToList();

            foreach (var ev in en)
            {
                Evento ee = new Evento();
                ee.EventName = ev.EventName;
                ee.EventType = ev.EventType;
                ee.EventFee = ev.EventFee;

                //List<subevents> se = new List<subevents>();
                //se = JsonConvert.DeserializeObject(ev.Events);
                //ee.Events = ev.Events //ev.Events.ToString();

                ee.EventFromDate = ev.EventFromDate;
                ee.EventToDate = ev.EventToDate;
                ee.CollegeName = ev.CollegeName;
                ee.Department = ev.Department;
                ee.City = ev.City;
                ee.State = ev.State;
                ee.Address = ev.Address;
                if (ev.Poster != null)
                {
                    ee.Poster = "data:image/jpeg;base64," + Convert.ToBase64String(ev.Poster);
                }
                ee.Website = ev.Website;
                ee.EventID = ev.EventID;
                eve.Add(ee);
            }
            return Json(eve, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void sendEmail()
        {
            sendMessage.SendEmail();
        }
    }
}