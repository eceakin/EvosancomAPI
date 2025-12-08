namespace EvosancomAPI.Domain.Enums
{
	public enum OrderStatus
	{
		Pending = 0,           // Beklemede
		Confirmed = 1,         // Onaylandı
		InProduction = 2,      // Üretimde
		QualityControl = 3,    // Kalite Kontrolde
		InWarehouse = 4,       // Depoda
		Shipped = 5,           // Yola Çıktı
		Delivered = 6,         // Teslim Edildi
		Cancelled = 7          // İptal
	}
}
