using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FeePay.Core.Application.Enums.Notification;

namespace FeePay.Web.Areas.Common
{
    public class AreaBaseController : Controller
    {
        /// <summary>
        /// Sets the information for the system notification.
        /// </summary>
        /// <param name="notifyType">The type of notification to display to the user: Success, Error or Warning.</param>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="Timer">The time to display alert to the user default 3sec.</param>
        public void TostMessage(NotificationType NotificationType, string Message, int Timer = 3000)
        {
            TempData["TostMessage"] = "icon: '" + NotificationType.ToString() + "', title: '" + Message + "', timer: " + Timer.ToString();
        }

        /// <summary>
        /// Sets the information for the system notification.
        /// </summary>
        /// <param name="notifyType">The type of notification to display to the user: Success, Error or Warning.</param>
        /// <param name="Title">The title to display to the user.</param>
        /// <param name="Html">The message to display to the user.</param>
        /// <param name="Timer">The time to display alert to the user default 3sec.</param>
        public void AlertMessage(NotificationType NotificationType, string Title, string Html, int Timer = 3000)
        {
            TempData["AlertMessage"] = "icon: '" + NotificationType.ToString() + "', title: '" + Title + "', html: '" + Html + "', timer: " + Timer.ToString();

        }

        #region Old
        /// <summary>
        /// Sets the information for the system notification.
        /// </summary>
        /// <param name="Message">The message to display to the user.</param>
        /// <param name="NotifyType">The type of notification to display to the user: Success, Error or Warning.</param>
        public void Message(NotificationType NotifyType, string Message)
        {
            TempData["Notification2"] = Message;

            switch (NotifyType)
            {
                case NotificationType.success:
                    TempData["NotificationCSS"] = "alert-box success";
                    break;
                case NotificationType.error:
                    TempData["NotificationCSS"] = "alert-box errors";
                    break;
                case NotificationType.warning:
                    TempData["NotificationCSS"] = "alert-box warning";
                    break;

                case NotificationType.info:
                    TempData["NotificationCSS"] = "alert-box notice";
                    break;
            }
        }
        #endregion


        /// <summary>
        /// Check the return url is a local and redirect to appropriate page.
        /// </summary>
        /// <param name="returnUrl">The page to return to.</param>
        public IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
            else return RedirectToAction("Index", "Home");
        }
    }
}
