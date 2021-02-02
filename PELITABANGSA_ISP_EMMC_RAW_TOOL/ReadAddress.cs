namespace PELITABANGSA_ISP_EMMC_RAW_TOOL
{
    public class ReadAddress
	{
		public bool Error
		{
			get;
			set;
		}

		public string Filename
		{
			get;
			set;
		}

		public long Length
		{
			get;
			set;
		}

		public string Partition
		{
			get;
			set;
		}

		public long StartAddress
		{
			get;
			set;
		}

		public string Status
		{
			get;
			set;
		}

		public ReadAddress()
		{
			Error = false;
			Status = string.Empty;
			Filename = string.Empty;
			StartAddress = 0L;
			Length = 0L;
			Partition = "-";
		}
	}
}
