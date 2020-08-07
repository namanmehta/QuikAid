using Microsoft.Ajax.Utilities;
using QuikAid.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuikAid.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Dashboard()
        {

            using (LoginDBEntities db = new LoginDBEntities())
            {
                return View(db.clientTables.ToList());

            }

        }
        public ActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(clientTable c)
        {
            try {


                c.SSN = c.SSN.ToString();
                c.DOB = DateTime.Now;
                
                using (LoginDBEntities dbm = new LoginDBEntities())
                {
                    
                    dbm.clientTables.Add(c);
                    dbm.SaveChanges();
                }
                return RedirectToAction("Dashboard", "Home");

            }
            catch 
            {
                return RedirectToAction("Dashboard", "Home");

            }


        }

        public ActionResult DateRange()
        {
            using (LoginDBEntities db = new LoginDBEntities())
            {
                return View(db.clientTables.ToList());

            }
            
        }
        [HttpPost]
        public ActionResult DateRangeO(FormCollection form)
        {
            var strt = form["StartDate"];
            var end= form["EndDate"];
            LoginDBEntities dbm = new LoginDBEntities();

            //IEnumerable<clientTable> signatures = from p in dbm.clientTables
                                                  
            //                                      where p.DOB >= StartDate && p.DOB <= EndDate

            //                                      select new clientTable
            //                                      {
            //                                          fname = g.Count(),
            //                                          lName = g.Key
            //                                          userId
            //                                      };

            //IEnumerable<clientTable> signatures = 
            //var q=dbm.clientTables.Where(x=> )

            return View();
        }

        public ActionResult Edit(int id)
        {
            using (LoginDBEntities dbm = new LoginDBEntities())
            {
                return View(dbm.clientTables.Where(x => x.clientID == id).FirstOrDefault());
            }

        }
        [HttpPost]
        public ActionResult Edit(int id, clientTable c)
        {
            try
            {
                using (LoginDBEntities dbm = new LoginDBEntities())
                {
                    dbm.Entry(c).State = EntityState.Modified;
                    dbm.SaveChanges();
                }



                return RedirectToAction("Dashboard", "Home");

            }
            catch (Exception e)
            {

                return RedirectToAction("Dashboard", "Home");
            }
        }

        public ActionResult Delete(int id)
        {
            using (LoginDBEntities dbm = new LoginDBEntities())
            {
                return View(dbm.clientTables.Where(x => x.clientID == id).FirstOrDefault());
            }

        }
        [HttpPost]

        public ActionResult Delete(int id, FormCollection colection)
        {
            try
            {
                using (LoginDBEntities dbm = new LoginDBEntities())
                {
                    clientTable c = dbm.clientTables.Where(x => x.clientID == id).FirstOrDefault();
                    dbm.clientTables.Remove(c);
                    dbm.SaveChanges();
                }



                return RedirectToAction("Dashboard", "Home");

            }
            catch (Exception e)
            {

                return RedirectToAction("Dashboard", "Home");
            }
        }

        public ActionResult AddPh()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPh(phoneTable p)
        {
            {
                try
                {

                    using (LoginDBEntities dbm = new LoginDBEntities())
                    {
                        dbm.phoneTables.Add(p);
                        dbm.SaveChanges();
                    }
                    return RedirectToAction("Dashboard", "Home");

                }
                catch (Exception e)
                {
                    return RedirectToAction("Dashboard", "Home");

                }


            }
        }

        public ActionResult GetData()
        {
            LoginDBEntities dbm = new LoginDBEntities();


            //var groupData = from student in dbm.clientTables
            //                group new { student.clientID }
            //                                by student.clientID > 0 into studentGroup
            //                           select studentGroup;
            //var groupData1 = from p in dbm.phoneTables
            //                group p.phoneNumber by p.clientID into g
            //              select new { clientID = g.Key, phoneNumber = g.ToList() };

            //var groupData=from employee in dbm.phoneTables
            //              group employee by employee.clientID into depGroup
            //orderby depGroup.Key ascending
            //select depGroup;

            IEnumerable<Custom> signatures = from p in dbm.phoneTables
                                             join c in dbm.clientTables on p.clientID equals c.clientID
                                             group p.phoneNumber by c.fname into g
                                           select new Custom
                                           {
                                               CountOfClients=  g.Count(),
                                               FirstName=g.Key
                                           };


            //var materializedUser = groupData1.FirstOrDefault();


            //g.GroupData = groupData;
            //var dg= data.Select(x=> new)
            return View(signatures);
        }
        public class Grp
        {
            public Var GroupData { get; set; }
        }

        public ActionResult Index()
        {
            //string query = "SELECT c.firstName, c.lastName, p.clientID, COUNT(p.phoneNumber) TotalOrders";
            //query += " FROM phoneTable p join clientTable c GROUP BY c.firstName, c.lastName, p.clientID";

            string query = "select phoneNumber from phoneTable";
            //string constr = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            string constr = "data source=DESKTOP-UBMDLUC\\SQLEXPRESS;initial catalog=LoginDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;";
            List<phoneTable> chartData = new List<phoneTable>();

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            chartData.Add(new phoneTable
                            {
                                clientID = (int)sdr["clientID"],
                                TotalOrders = Convert.ToInt32(sdr["TotalOrders"])
                            });
                        }
                    }

                    con.Close();
                }
            }

            return View(chartData);
        }

        public ActionResult Detail(int id)
        {
            using (LoginDBEntities db = new LoginDBEntities())
            {
                return View(db.clientTables.Where(x => x.clientID == id).FirstOrDefault());

            }
        }


        public ActionResult About()
        {
            

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}