using System.Data;

namespace NZBlood.ApprovalWorkflows.Blazor.Services;

internal static class SqlReaderExtensions
{
    public static string GetTrimmedString(this IDataRecord record, string name)
    {
        var value = record[name];
        return value == DBNull.Value ? string.Empty : Convert.ToString(value)?.TrimEnd() ?? string.Empty;
    }

    public static int GetInt32OrDefault(this IDataRecord record, string name)
    {
        var value = record[name];
        return value == DBNull.Value ? 0 : Convert.ToInt32(value);
    }

    public static decimal GetDecimalOrDefault(this IDataRecord record, string name)
    {
        var value = record[name];
        return value == DBNull.Value ? 0 : Convert.ToDecimal(value);
    }

    public static DateTime? GetDateTimeOrNull(this IDataRecord record, string name)
    {
        var value = record[name];
        return value == DBNull.Value ? null : Convert.ToDateTime(value);
    }
}
