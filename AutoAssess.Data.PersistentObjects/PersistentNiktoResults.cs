using System;
using AutoAssess.Data.BusinessObjects;
using System.Collections.Generic;

namespace AutoAssess.Data.PersistentObjects
{
	
	[Serializable]
	public class PersistentNiktoResults : NiktoToolResults, IEntity
	{
		public PersistentNiktoResults ()
		{
		}
		
		public PersistentNiktoResults(NiktoToolResults results)
		{
			this.FullOutput = results.FullOutput;
			this.HostPortID = results.HostPortID;
			this.HostIPAddressV4 = results.HostIPAddressV4;
			this.HostPort = results.HostPort;
			this.IsTCP = results.IsTCP;
		}
		
		public virtual IList<PersistentNiktoItem> Items { get; set; }
		
		public virtual Guid ID { get; set; }
		
		public virtual PersistentProfile ParentProfile { get; set; }
		
		public virtual PersistentUser User { get; set; }
		
		public virtual DateTime CreatedOn { get; set; }
		
		public virtual Guid CreatedBy { get; set; }
		
		public virtual DateTime LastModifiedOn { get; set; }
		
		public virtual Guid LastModifiedBy { get; set; }
		
		public virtual bool IsActive { get; set; }
		
		public virtual void SetCreationInfo(Guid userID)
		{
			this.ID = Guid.NewGuid();
			this.CreatedBy = userID;
			this.CreatedOn = DateTime.Now;
			this.LastModifiedBy = userID;
			this.LastModifiedOn = DateTime.Now;
			this.IsActive = true;
		}
		
		public virtual void SetUpdateInfo(Guid userID, bool isActive)
		{
			this.LastModifiedBy = userID;
			this.LastModifiedOn = DateTime.Now;
			this.IsActive = isActive;
		}
		public virtual string ToPersistentXml()
		{
			return string.Empty;
		}
	}
}

