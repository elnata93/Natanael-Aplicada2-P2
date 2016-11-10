using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;

namespace BLL
{
    public static class Utilitarios
    {
        public static void ShowToastr(this Page page, string message, string title, string type = "info")
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "toastr_message",
                  String.Format("toastr.{0}('{1}', '{2}');", type.ToLower(), message, title), addScriptTags: true);
        }

        public static int ConvertirToEntero(string numero)
        {
            int num;
            int.TryParse(numero, out num);
            return num;
        }

        //private static void Mensajes(string mensaje)
        //{
        //    HttpResponseWrapper.("<script>'" + mensaje + "' </script>");
        //}
    }
}
