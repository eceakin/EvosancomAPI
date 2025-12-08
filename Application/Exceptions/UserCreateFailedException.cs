using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Exceptions
{
	public class UserCreateFailedException:Exception
	{
		public UserCreateFailedException():base("User creation failed.")
		{

		}

	}	
}
