using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AutoAssess.Data.Virtualbox
{
	public class VirtualboxManager : IDisposable
	{
		string _path = string.Empty;
		public VirtualboxManager (string path)
		{
			_path = path;
		}
		
		public VirtualMachine[] ListAllVirtualMachines()
		{
			string cmd = " list vms";
			string output = RunCommand(cmd);
			
			List<VirtualMachine> machines = new List<VirtualMachine>();
			foreach (string line in output.Split('\n'))
				machines.Add(new VirtualMachine(line));
			
			return machines.ToArray();
		}
		
		public VirtualMachine[] ListRunningVirtualMachines()
		{
			string cmd = " list runningvms";
			string output = RunCommand(cmd);
			
			List<VirtualMachine> machines = new List<VirtualMachine>();
			foreach (string line in output.Split('\n'))
				machines.Add(new VirtualMachine(line));
			
			return machines.ToArray();
		}
		
		public string GetVirtualMachineIP(string id)
		{
			string cmd = " guestproperty get \"" + id + "\" \"/VirtualBox/GuestInfo/Net/0/V4/IP\"";
			
			return this.RunCommand(cmd);
		}
		
		public string GetVirtualMachineIP(VirtualMachine machine)
		{
			string output = string.Empty;
			if (machine.Guid != Guid.Empty)
				output = this.GetVirtualMachineIP(machine.Guid.ToString());
			else 
				output = this.GetVirtualMachineIP(machine.Name);
			
			return output;
		}

		public void StopVirtualMachine (string id)
		{
			string cmd = " controlvm \"" + id + "\" poweroff";
			
			this.RunCommand(cmd);
		}
		
		public void StopVirtualMachine (VirtualMachine machine)
		{
			if (machine.Guid != Guid.Empty)
				this.StopVirtualMachine(machine.Guid.ToString());
			else 
				this.StopVirtualMachine(machine.Name);
		}
		
		public void ResetVirtualMachine(string id)
		{
			string cmd = " controlvm \"" + id + "\" reset";
			
			this.RunCommand(cmd);
		}
		
		public void ResetVirtualMachine(VirtualMachine machine)
		{
			if (machine.Guid != Guid.Empty)
				this.ResetVirtualMachine(machine.Guid.ToString());
			else
				this.ResetVirtualMachine(machine.Name);
		}
		
		public string ExecuteNativeCommand(string id, string command, string username, string password)
		{
			string cmd = " guestcontrol \"" + id + "\" execute --image \"" + command + "\" --username " + username + " --password " + password + " --wait-stdout";
			
			return this.RunCommand(cmd);
		}
		
		public string ExecuteNativeCommand(VirtualMachine machine, string command)
		{
			string output = string.Empty;
			if (machine.Guid != Guid.Empty)
				output = this.ExecuteNativeCommand(machine.Guid.ToString(), command, machine.Username, machine.Password);
			else 
				output = this.ExecuteNativeCommand(machine.Name, command, machine.Username, machine.Password);
			return output;
		}
		
		public void StartVirtualMachine(string id)
		{
			string cmd = " startvm \"" + id + "\"";
			
			this.RunCommand(cmd);
		}
		
		public void StartVirtualMachine(VirtualMachine machine)
		{
			if (machine.Guid != Guid.Empty)
				this.StartVirtualMachine(machine.Guid.ToString());
			else 
				this.StartVirtualMachine(machine.Name);
		}
		
		private string RunCommand(string command)
		{
			Process proc = new Process();
			proc.StartInfo = new ProcessStartInfo();
		    proc.StartInfo.FileName = _path;
		    proc.StartInfo.Arguments = command;
		    proc.StartInfo.UseShellExecute = false;
			proc.StartInfo.RedirectStandardOutput = true;
			proc.Start();
			proc.WaitForExit();
			return proc.StandardOutput.ReadToEnd();
		}
		
		public void Dispose()
		{
			_path = null;
		}
	}
}

