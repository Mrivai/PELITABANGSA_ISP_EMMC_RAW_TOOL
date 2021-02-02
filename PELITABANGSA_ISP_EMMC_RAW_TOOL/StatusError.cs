namespace PELITABANGSA_ISP_EMMC_RAW_TOOL
{
    public class StatusError
    {
		public bool Error
		{
			get;
			set;
		}

		public string Status
		{
			get;
			set;
		}

		public StatusError()
		{
			Error = false;
			Status = string.Empty;
		}
	}
}
