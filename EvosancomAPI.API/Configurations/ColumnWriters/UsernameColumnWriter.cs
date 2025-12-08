using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace EvosancomAPI.API.Configurations.ColumnWriters
{
	public class UsernameColumnWriter : ColumnWriterBase
	{
		public UsernameColumnWriter() : base(NpgsqlDbType.Varchar)
		{
		}

		public override object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null)
		{
			// Property var mı kontrol et
			if (!logEvent.Properties.ContainsKey("user_name"))
				return "anonymous";

			var value = logEvent.Properties["user_name"];

			// ScalarValue ise içindeki değeri al
			if (value is ScalarValue scalar)
				return scalar.Value?.ToString() ?? "anonymous";

			// Değilse toString yap ve tırnakları temizle
			return value.ToString().Trim('"') ?? "anonymous";
		}
	}
}