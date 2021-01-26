using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Domain.Entities.Common;

namespace FeePay.Core.Application.Services
{
    public class CommanDataServices : ICommanDataServices
    {
        public List<DropDownItem> GetStudentCategoryDDLItems()
        {
            return new List<DropDownItem>()
            {
                new DropDownItem(){ Text = "General" , Value = "GENERAL"},
                new DropDownItem(){ Text = "OBC" , Value = "OBC"},
                new DropDownItem(){ Text = "ST" , Value = "ST"},
                new DropDownItem(){ Text = "SC" , Value = "SC"},
                new DropDownItem(){ Text = "Physically Challenged" , Value = "PHYSICALLY CHALLENGED"},
                new DropDownItem(){ Text = "Special" , Value = "SPECIAL"}
            };
        }
        public List<DropDownItem> GetAdmissionTypeDDLItems()
        {
            return new List<DropDownItem>()
            {
                new DropDownItem(){ Text = "Regular" , Value = "REGULAR"},
                new DropDownItem(){ Text = "Private" , Value = "PRIVATE"},
                new DropDownItem(){ Text = "Ex-Student" , Value = "EX-STUDENT"},
                new DropDownItem(){ Text = "N/A" , Value = "N/A"}
            };
        }
        public List<DropDownItem> GetReligionDDLItems()
        {
            return new List<DropDownItem>()
            {
                new DropDownItem(){ Text = "Hindu" , Value = "HINDU"},
                new DropDownItem(){ Text = "Muslim" , Value = "MUSLIM"},
                new DropDownItem(){ Text = "Sikh" , Value = "SIKH"},
                new DropDownItem(){ Text = "Isai" , Value = "ISAI"},
                new DropDownItem(){ Text = "Buddh" , Value = "BUDDH"},
                new DropDownItem(){ Text = "Jain" , Value = "JAIN"},
                new DropDownItem(){ Text = "Zoroastrian" , Value = "ZOROASTRIAN"},
                new DropDownItem(){ Text = "Juda" , Value = "JUDA"}
            };
        }


    }
}
