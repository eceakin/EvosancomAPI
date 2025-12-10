using EvosancomAPI.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.CustomAttributes
{
	public class AuthorizeDefinitionAttribute :Attribute
	{
        public string Menu { get; set; }
		public string Definition { get; set; }
        public ActionType ActionType { get; set; } //reading mi writing mi 
    }
}

/* 
 * controller bir menu olarak değerlendirilebilir 
 * 
 * 
 */ 
