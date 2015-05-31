﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallLibrary.Communication
{
	[Serializable]
	public class CommunicationObject : object
	{
		public enum CommunicationStatus
		{
			None, MessageRecieved
		}

		public CommunicationStatus Status;

		public int ProcessID;

		public string Message;
	}
}
