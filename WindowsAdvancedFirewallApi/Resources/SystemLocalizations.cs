﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestris.ResourceLib;

namespace WindowsAdvancedFirewallApi.Resources
{
	public class SystemLocalizations
	{
		private static readonly object syncLock = new object();

		private static SystemLocalizations _singleton;

		public static SystemLocalizations Singleton
		{
			get
			{
				if (_singleton == null)
				{
					lock (syncLock)
					{
						if (_singleton == null)
						{
							_singleton = new SystemLocalizations();
						}
					}
				}

				return _singleton;
			}
		}
		public static SystemLocalizations Instance => Singleton;

		private ResourceInfo FirewallAPI { get; set; }
		private ResourceInfo ICSvc { get; set; }

		public IReadOnlyDictionary<uint, string> StringsFirewallAPI { get; private set; }
		public IReadOnlyDictionary<uint, string> StringsICSvc { get; private set; }

		private SystemLocalizations()
		{
			FirewallAPI = new ResourceInfo();
			FirewallAPI.Load(Path.Combine(Environment.SystemDirectory, "FirewallAPI.dll"));
			StringsFirewallAPI = LoadStrings(FirewallAPI);
			FirewallAPI.Dispose();

			ICSvc = new ResourceInfo();
			ICSvc.Load(Path.Combine(Environment.SystemDirectory, "icsvc.dll"));
			StringsICSvc = LoadStrings(ICSvc);
			ICSvc.Dispose();
		}

		private IReadOnlyDictionary<uint, string> LoadStrings(ResourceInfo ri)
		{
			var strings = new Dictionary<uint, string>();

			foreach (var r in ri[Kernel32.ResourceTypes.RT_STRING])
			{
				if (!(r is StringResource))
				{
					continue;
				}

				var sr = r as StringResource;

				foreach (var @string in sr.Strings)
				{
					strings.Add(@string.Key, @string.Value);
				}
			}

			return strings;
		}
	}
}
