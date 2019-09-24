﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Xml;

namespace Marcusca10.Samples.BuildingAccessNet.Web.Models
{
    public class UserWhitelistAttribute : ValidationAttribute
    {
        private string _email;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = (RegisterViewModel)validationContext.ObjectInstance;
            _email = user.Email;

            if (!IsWhithelisted(_email))
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        //public string Email => _email;

        public string GetErrorMessage()
        {
            return $"Your email address '{_email}' is not yet authorized to register for this application.";
        }
        private bool IsWhithelisted(string email)
        {
            //open the whitelist xml file  
            XmlDocument doc = new XmlDocument();
            string whitelist = HostingEnvironment.IsDevelopmentEnvironment ? "whitelist.debug.xml" : "whitelist.xml";
            doc.Load(HostingEnvironment.MapPath("~/App_Data/" + whitelist));

            XmlNode user = doc.SelectSingleNode(@"/users[user='" + email.ToLower() + "']");

            return (user != null);
        }
    }
}