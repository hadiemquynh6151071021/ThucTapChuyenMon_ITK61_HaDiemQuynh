using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ceramics_Store.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Lấy danh sách các vai trò
            string[] roles = Roles.GetAllRoles();

            ViewBag.a = roles;

            //// Kiểm tra xem một người dùng có thuộc một vai trò hay không
            //bool isInRole = Roles.IsUserInRole("username", "rolename");

            //// Thêm một người dùng vào một vai trò
            //Roles.AddUserToRole("username", "rolename");

            //// Xóa một người dùng khỏi một vai trò
            //Roles.RemoveUserFromRole("username", "rolename");
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
        public ActionResult Introduction()
        {
            return View();
        }
    }
}