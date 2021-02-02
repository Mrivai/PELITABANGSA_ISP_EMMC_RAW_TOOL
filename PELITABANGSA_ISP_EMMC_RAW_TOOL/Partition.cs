namespace PELITABANGSA_ISP_EMMC_RAW_TOOL
{
    public class Partition
	{
		public long EndAddress
		{
			get;
			set;
		}

		public string ID
		{
			get;
			set;
		}

		public string PartitionName
		{
			get;
			set;
		}

		public bool Choosen
		{
			get;
			set;
		}

		public long StartAdress
		{
			get;
			set;
		}

		public long Size
		{
			get;
			set;
		}

		public Partition()
		{
		}
	}
}
