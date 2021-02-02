namespace PELITABANGSA_ISP_EMMC_RAW_TOOL
{
    public class ReadFUllImage
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

		public string Status
		{
			get;
			set;
		}

		public ReadFUllImage()
		{
			Error = false;
			Status = string.Empty;
			Filename = string.Empty;
		}
	}
}
