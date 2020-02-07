using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsManagement.Models
{
	public class Events
	{
		public string title { get; set; }
		public string date { get; set; }
		public string time { get; set; }
		public string duration { get; set; }
		public string description { get; set; }
		public string location { get; set; }
		public int isPublic { get; set; }
		public string author { get; set; }
		public int upcoming { get; set; }
	}
}