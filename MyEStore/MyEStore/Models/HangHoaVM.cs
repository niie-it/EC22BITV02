namespace MyEStore.Models
{
	public class HangHoaVM
	{
		public int MaHh { get; set; }
		public string TenHh { get; set; }
		public string Hinh { get; set; }
		public double DonGia { get; set; }
		public double GiamGia { get; set; }
		public double GiaDaGiam => DonGia * (1 - GiamGia);

	}
}
