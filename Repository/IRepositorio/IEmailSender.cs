﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositorio
{
    public interface IEmailSender
    {
        void SendEmail(string toEmail, string subject);
    }
}
