using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tron.Logic
{
	public class UnspecifiedPlayerException : Exception
	{
		public UnspecifiedPlayerException(string message) : base(message)
		{
		    
		}
	}
}