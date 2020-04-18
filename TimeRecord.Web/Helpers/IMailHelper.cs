﻿using TimeRecord.Common.Models;

namespace TimeRecord.Web.Helpers
{
	public interface IMailHelper
	{
		Response SendMail(string to, string subject, string body);
	}
}
